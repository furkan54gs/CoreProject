using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using core.business.Abstract;
using core.entity;
using core.webui.Identity;
using core.webui.Models;
using core.webui.Extensions;
using System.Threading.Tasks;
using System.IO;
using Stripe;

namespace core.webui.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private IOrderService _orderService;
        private IProductService _productservice;
        private ICommentService _commentservice;
        private UserManager<User> _userManager;
        public CartController(IOrderService orderService, ICartService cartService, IProductService productservice,ICommentService commentservice, UserManager<User> userManager)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
            _productservice = productservice;
            _commentservice = commentservice;
        }
        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));
            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (double)i.Product.Price,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity

                }).ToList()
            });
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, string productUrl, int quantity)
        {
            var userId = _userManager.GetUserId(User);

            if (_cartService.AddToCart(userId, productId, quantity))
            {

                return RedirectToAction("Index");
            }
            else
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Başarısız",
                    Message = "Stokta " + quantity + "adet ürün bulunmamaktadır.",
                    AlertType = "danger"
                });

                return Redirect("~/" + productUrl);
            }


        }

        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.DeleteFromCart(userId, productId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));


            var orderModel = new OrderModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                Address = user.Address,
                Phone = user.PhoneNumber,
                Email = user.Email,
                CartModel = new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(i => new CartItemModel()
                    {
                        CartItemId = i.Id,
                        ProductId = i.ProductId,
                        Name = i.Product.Name,
                        Price = (double)i.Product.Price,
                        ImageUrl = i.Product.ImageUrl,
                        Quantity = i.Quantity

                    }).ToList()
                }
            };

            return View(orderModel);
        }

        [HttpPost]
        public IActionResult Checkout(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

                model.CartModel = new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(i => new CartItemModel()
                    {
                        CartItemId = i.Id,
                        ProductId = i.ProductId,
                        Name = i.Product.Name,
                        Price = (double)i.Product.Price,
                        ImageUrl = i.Product.ImageUrl,
                        Quantity = i.Quantity
                    }).ToList()
                };
            }

            var obj = JsonConvert.SerializeObject(model);
            TempData["cart"] = obj;

            return RedirectToAction("Payment");
        }

        public IActionResult GetOrders()
        {
            var userId = _userManager.GetUserId(User);
            var orders = _orderService.GetOrders(userId);

            var orderListModel = new List<OrderListModel>();
            OrderListModel orderModel;
            foreach (var order in orders)
            {
                orderModel = new OrderListModel();

                orderModel.OrderId = order.Id;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.Phone = order.Phone;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.Email = order.Email;
                orderModel.Address = order.Address;
                orderModel.City = order.City;
                orderModel.OrderState = order.OrderState;
                orderModel.PaymentType = order.PaymentType;

                orderModel.OrderItems = order.OrderItems.Select(i => new OrderItemModel()
                {
                    OrderItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (double)i.Price,
                    Quantity = i.Quantity,
                    ImageUrl = i.Product.ImageUrl

                }).ToList();

                orderListModel.Add(orderModel);
            }

            return View("Orders", orderListModel);
        }

        private void ClearCart(int cartId)
        {
            _cartService.ClearCart(cartId);
        }

        private void SaveOrder(OrderModel model, string userId, string paymentId)
        {
            var order = new entity.Order();

            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.OrderState = EnumOrderState.completed;
            order.PaymentType = EnumPaymentType.CreditCard;
            order.PaymentId = paymentId;
            //    order.ConversationId = payment.ConversationId;
            order.OrderDate = DateTime.Now;
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.UserId = userId;
            order.Address = model.Address;
            order.Phone = model.Phone;
            order.Email = model.Email;
            order.City = model.City;
            order.Note = model.Note;

            order.OrderItems = new List<entity.OrderItem>();

            Cart cart = _cartService.GetCartByUserId(userId);

            foreach (var item in cart.CartItems)
            {
                var orderItem = new core.entity.OrderItem()
                {
                    Price = (double)item.Product.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                };
                _productservice.StockUpdate(item.ProductId, item.Quantity);
                order.OrderItems.Add(orderItem);
            }
            _orderService.Create(order);
        }


        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public void Success(OrderModel Cmodel, string paymentId)
        {
            string userId = _userManager.GetUserId(User);
            var cart = _cartService.GetCartByUserId(userId);
            SaveOrder(Cmodel, userId, paymentId);
            ClearCart(cart.Id);
            var msg = new AlertMessage()
            {
                Message = "Sipariş alındı :)",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

        }

        public IActionResult Payment()
        {
            return View();
        }


        public IActionResult Rate(int id)
        {

            //dönen itemi döndür. view de foto ve ad kısmında göster. form inputları ekle ve post işlemini gerçekleştir.
            entity.Product product = _productservice.GetById(id);
            ProductModel productview = new ProductModel()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                ImageUrl = product.ImageUrl
            };
            return View(productview);
        }

        [HttpPost]
        public IActionResult Rate(CommentModel commentFromView)
        {
            //if you want more secure; get all orderitems bought by user and check productId
            Comment comment = new Comment()
            {
                UserId = _userManager.GetUserId(User),
                ProductId = commentFromView.ProductId,
                Title = commentFromView.Title,
                Description = commentFromView.Description,
                Rate = commentFromView.Rate,
                DateAdded = DateTime.Now,
                IsApproved = false
            };
            _commentservice.Create(comment);
            _productservice.CommentUpdate(commentFromView.ProductId,(double)commentFromView.Rate);

            var msg = new AlertMessage()
            {
                Message = "Yorumunuz onaylanmak üzere gönderildi :) ",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("Success");
        }



        /*
                      private Payment PaymentProcess(OrderModel model)
                      {
                          Options options = new Options();
                          options.ApiKey = "sandbox-RStpAR6sBz86RpFTuWPyF7hjh8c8g9E4";
                          options.SecretKey = "sandbox-0zu0oxpXipwFsSGu1FrSofDVhTrWig2p";
                          options.BaseUrl = "https://sandbox-api.iyzipay.com";

                          CreatePaymentRequest request = new CreatePaymentRequest();
                          request.Locale = Locale.TR.ToString();
                          request.ConversationId = new Random().Next(111111111,999999999).ToString();
                          request.Price = model.CartModel.TotalPrice().ToString();
                          request.PaidPrice = model.CartModel.TotalPrice().ToString();
                          request.Currency = Currency.TRY.ToString();
                          request.Installment = 1;
                          request.BasketId = "B67832";
                          request.PaymentChannel = PaymentChannel.WEB.ToString();
                          request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

                          PaymentCard paymentCard = new PaymentCard();
                          paymentCard.CardHolderName = model.CardName;
                          paymentCard.CardNumber = model.CardNumber;
                          paymentCard.ExpireMonth = model.ExpirationMonth;
                          paymentCard.ExpireYear = model.ExpirationYear;
                          paymentCard.Cvc = model.Cvc;
                          paymentCard.RegisterCard = 0;
                          request.PaymentCard = paymentCard;

                          //  paymentCard.CardNumber = "5528790000000008";
                          // paymentCard.ExpireMonth = "12";
                          // paymentCard.ExpireYear = "2030";
                          // paymentCard.Cvc = "123";

                          Buyer buyer = new Buyer();
                          buyer.Id = "BY789";
                          buyer.Name = model.FirstName;
                          buyer.Surname = model.LastName;
                          buyer.GsmNumber = "+905350000000";
                          buyer.Email = "email@email.com";
                          buyer.IdentityNumber = "74300864791";
                          buyer.LastLoginDate = "2015-10-05 12:43:35";
                          buyer.RegistrationDate = "2013-04-21 15:12:09";
                          buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
                          buyer.Ip = "85.34.78.112";
                          buyer.City = "Istanbul";
                          buyer.Country = "Turkey";
                          buyer.ZipCode = "34732";
                          request.Buyer = buyer;

                          Address shippingAddress = new Address();
                          shippingAddress.ContactName = "Jane Doe";
                          shippingAddress.City = "Istanbul";
                          shippingAddress.Country = "Turkey";
                          shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
                          shippingAddress.ZipCode = "34742";
                          request.ShippingAddress = shippingAddress;

                          Address billingAddress = new Address();
                          billingAddress.ContactName = "Jane Doe";
                          billingAddress.City = "Istanbul";
                          billingAddress.Country = "Turkey";
                          billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
                          billingAddress.ZipCode = "34742";
                          request.BillingAddress = billingAddress;

                          List<BasketItem> basketItems = new List<BasketItem>();
                          BasketItem basketItem;

                          foreach (var item in model.CartModel.CartItems)
                          {
                              basketItem = new BasketItem();
                              basketItem.Id = item.ProductId.ToString();
                              basketItem.Name = item.Name;
                              basketItem.Category1 = "Telefon";
                              basketItem.Price = item.Price.ToString();
                              basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                              basketItems.Add(basketItem);
                          }
                          request.BasketItems = basketItems;
                          return Payment.Create(request, options);
                      }

              */

    }
}