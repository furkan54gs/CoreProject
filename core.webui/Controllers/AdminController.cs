using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using core.business.Abstract;
using core.entity;
using core.webui.Extensions;
using core.webui.Identity;
using core.webui.Models;

namespace core.webui.Controllers
{

    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private ICommentService _commentService;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        private IImageService _imageService;
        public AdminController(IProductService productService,
                               ICategoryService categoryService,
                               ICommentService commentService,
                               RoleManager<IdentityRole> roleManager,
                               UserManager<User> userManager,
                               IImageService imageService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _commentService = commentService;
            _roleManager = roleManager;
            _userManager = userManager;
            _imageService = imageService;
        }


        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i => i.Name);

                ViewBag.Roles = roles;
                return View(new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    City = user.City,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                });
            }
            return Redirect("~/admin/user/list");
        }


        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailsModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Address = model.Address;
                    user.City = model.City;
                    user.Email = model.Email;
                    user.PhoneNumber = model.Phone;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());

                        return Redirect("/admin/user/list");
                    }
                }
                return Redirect("/admin/user/list");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserDelete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);

                TempData.Put("message", new AlertMessage()
                {
                    Title = "Ba??ar??l??",
                    Message = "Kullan??c?? silindi",
                    AlertType = "success"
                });
            }
            return RedirectToAction("UserList");
        }

        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }

        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var members = new List<User>();
            var nonmembers = new List<User>();

            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name)
                                ? members : nonmembers;
                list.Add(user);
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            return Redirect("/admin/role/" + model.RoleId);
        }

        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult RoleCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }


        public IActionResult ProductList()
        {
            return View(new ProductListViewModel()
            {
                Products = _productService.GetAll()
            });
        }
        public IActionResult CategoryList()
        {
            return View(new CategoryListViewModel()
            {
                Categories = _categoryService.GetAll()
            });
        }
        public IActionResult ProductCreate()
        {
            ViewBag.Categories = _categoryService.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model, int[] categoryIds, IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.Any())
                {
                    List<string> imagesName = new List<string>();
                    foreach (var item in files)
                    {
                        var extention = Path.GetExtension(item.FileName);
                        string randomName = string.Format($"{Guid.NewGuid()}{extention}");
                        imagesName.Add(randomName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }

                    }
                    var entity = new Product()
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Rate = 0,
                        Stock = model.Stock,
                        Description = model.Description,
                        IsHome = model.IsHome,
                        IsApproved = model.IsApproved,
                    };
                    _productService.Create(entity, categoryIds, imagesName);

                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "kay??t eklendi",
                        Message = "kay??t eklendi",
                        AlertType = "success"
                    });
                    return RedirectToAction("ProductList");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "hata",
                    Message = _productService.ErrorMessage,
                    AlertType = "danger"
                });
            }

            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }

        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryCreate(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category()
                {
                    Name = model.Name,
                };

                _categoryService.Create(entity);

                TempData.Put("message", new AlertMessage()
                {
                    Title = "kay??t eklendi.",
                    Message = $"{entity.Name} isimli category eklendi.",
                    AlertType = "success"
                });

                return RedirectToAction("CategoryList");
            }
            return View(model);
        }
        public IActionResult ProductEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _productService.GetByIdWithAll((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Url = entity.Url,
                Price = entity.Price,
                Rate = entity.Rate,
                Stock = entity.Stock,
                Description = entity.Description,
                IsApproved = entity.IsApproved,
                IsHome = entity.IsHome,
                SelectedCategories = entity.ProductCategories.Select(i => i.Category).ToList(),
                Images = entity.Images

            };

            ViewBag.Categories = _categoryService.GetAll();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductModel model, int[] categoryIds, IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                var entity = _productService.GetById(model.ProductId); //override edip image ile gelcek
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Rate = model.Rate;
                entity.Url = model.Url;
                entity.Description = model.Description;
                entity.IsHome = model.IsHome;
                entity.IsApproved = model.IsApproved;

                if (files != null && files.Any())
                {

                    foreach (var item in files)
                    {
                        var extention = Path.GetExtension(item.FileName);
                        var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                        _imageService.Create(new Image
                        {
                            ProductId = model.ProductId,
                            Name = randomName,
                        });


                        // if (entity.Images != null && entity.Images.Any())  // E??er image i??in bo?? kontol?? yaparsak
                        // {

                        //     entity.Images.Add(new Image
                        //     {
                        //         ProductId = model.ProductId,
                        //         Name = randomName,
                        //     });
                        // }
                        // else
                        // {
                        //     entity.Images = new List<Image>{new Image(){
                        //     ProductId = model.ProductId,
                        //     Name = randomName,
                        // }};
                        // }

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }
                    }

                }

                if (_productService.Update(entity, categoryIds))
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "kay??t g??ncellendi",
                        Message = "kay??t g??ncellendi",
                        AlertType = "success"
                    });
                    return RedirectToAction("ProductList");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "hata",
                    Message = _productService.ErrorMessage,
                    AlertType = "danger"
                });
            }
            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }
        public IActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _categoryService.GetByIdWithProducts((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                Products = entity.ProductCategories.Select(p => p.Product).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CategoryEdit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _categoryService.GetById(model.CategoryId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Url = model.Url;

                _categoryService.Update(entity);

                var msg = new AlertMessage()
                {
                    Message = $"{entity.Name} isimli category g??ncellendi.",
                    AlertType = "success"
                };

                TempData["message"] = JsonConvert.SerializeObject(msg);

                return RedirectToAction("CategoryList");
            }
            return View(model);
        }
        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetById(productId);

            if (entity != null)
            {
                _productService.Delete(entity);
            }

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ??r??n silindi.",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }
        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);

            if (entity != null)
            {
                _categoryService.Delete(entity);
            }

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli category silindi.",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int productId, int categoryId)
        {
            _categoryService.DeleteFromCategory(productId, categoryId);
            return Redirect("/admin/categories/" + categoryId);
        }

        public IActionResult CommentList(string isApproved)
        {
            List<Comment> comment = _commentService.GetAll();

            if (isApproved == "approved")
                return View(comment.Where(p => p.IsApproved == true));
            else if (isApproved == "notapproved")
                return View(comment.Where(p => p.IsApproved == false));
            else
                return View(comment);
        }

        [HttpPost]
        public JsonResult CommentApproved(int commentId)
        {

            Comment comment = _commentService.GetById(commentId);
            var data = "";

            if (comment != null)
            {
                if (comment.IsApproved == true)
                    comment.IsApproved = false;
                else
                    comment.IsApproved = true;
                _commentService.Update(comment);
                data = "i??lem ba??ar??l?? " + "id= " + commentId;
                return Json(data);
            }
            else
            {
                data = "yorum bulunamad?? " + "id= " + commentId;
                return Json(data);
            }
        }




    }
}