using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using core.business.Abstract;
using core.business.Concrete;
using core.data.Abstract;
using core.data.Concrete.EfCore;
using core.webui.EmailServices;
using core.webui.Identity;
using Pomelo.EntityFrameworkCore;
using Stripe;
using core.webui.Extensions;

namespace core.webui
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
            services.AddDbContext<ApplicationContext>(options => options.UseMySql(_configuration.GetConnectionString("MySqlConnection"), serverVersion));
            services.AddDbContext<CoreContext>(options => options.UseMySql(_configuration.GetConnectionString("MySqlConnection"), serverVersion));

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;

                // lockout
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                // options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".CoreApp.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IImageService, ImageManager>();

            services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
                 new SmtpEmailSender(
                     _configuration["EmailSender:Host"],
                     _configuration.GetValue<int>("EmailSender:Port"),
                     _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                     _configuration["EmailSender:UserName"],
                     _configuration["EmailSender:Password"])
                );
            services.AddControllersWithViews();

            services.Configure<StripeSettings>(_configuration.GetSection("Stripe"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ICartService cartService)
        {
            app.UseStaticFiles(); // wwwroot

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                RequestPath = "/modules"      //set a call name.ex: in layout href="modules/bootstrap"
            });

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "rateitem",
                    pattern: "orders/rate-it/{id?}",
                    defaults: new { controller = "Cart", action = "Rate" }
                );

                endpoints.MapControllerRoute(
                    name: "success",
                    pattern: "success",
                    defaults: new { controller = "Cart", action = "Success" }
                );

                endpoints.MapControllerRoute(
                    name: "payment",
                    pattern: "payment",
                    defaults: new { controller = "Cart", action = "Payment" }
                );

                endpoints.MapControllerRoute(
                    name: "orders",
                    pattern: "orders",
                    defaults: new { controller = "Cart", action = "GetOrders" }
                );

                endpoints.MapControllerRoute(
                  name: "checkout",
                  pattern: "checkout",
                  defaults: new { controller = "Cart", action = "Checkout" }
                );

                endpoints.MapControllerRoute(
                    name: "cart",
                    pattern: "cart",
                    defaults: new { controller = "Cart", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "accountmanage",
                    pattern: "account/manage",
                    defaults: new { controller = "Account", action = "Manage" }
                );

                endpoints.MapControllerRoute(
                    name: "admincomments",
                    pattern: "admin/comment/list/{isApproved?}",
                    defaults: new { controller = "Admin", action = "CommentList" }
               );

                endpoints.MapControllerRoute(
                   name: "adminusers",
                   pattern: "admin/user/list",
                   defaults: new { controller = "Admin", action = "UserList" }
                );

                  endpoints.MapControllerRoute(
                    name: "adminroles",
                    pattern: "admin/user/delete",
                    defaults: new { controller = "Admin", action = "UserDelete" }
                );

                endpoints.MapControllerRoute(
                   name: "adminuseredit",
                   pattern: "admin/user/{id?}",
                   defaults: new { controller = "Admin", action = "UserEdit" }
                );


                endpoints.MapControllerRoute(
                    name: "adminroles",
                    pattern: "admin/role/list",
                    defaults: new { controller = "Admin", action = "RoleList" }
                );

                endpoints.MapControllerRoute(
                    name: "adminrolecreate",
                    pattern: "admin/role/create",
                    defaults: new { controller = "Admin", action = "RoleCreate" }
                );


                endpoints.MapControllerRoute(
                    name: "adminroleedit",
                    pattern: "admin/role/{id?}",
                    defaults: new { controller = "Admin", action = "RoleEdit" }
                );

                endpoints.MapControllerRoute(
                    name: "adminproducts",
                    pattern: "admin/products",
                    defaults: new { controller = "Admin", action = "ProductList" }
                );

                endpoints.MapControllerRoute(
                    name: "adminproductcreate",
                    pattern: "admin/products/create",
                    defaults: new { controller = "Admin", action = "ProductCreate" }
                );

                endpoints.MapControllerRoute(
                    name: "adminproductedit",
                    pattern: "admin/products/{id?}",
                    defaults: new { controller = "Admin", action = "ProductEdit" }
                );

                endpoints.MapControllerRoute(
                   name: "admincategories",
                   pattern: "admin/categories",
                   defaults: new { controller = "Admin", action = "CategoryList" }
                );

                endpoints.MapControllerRoute(
                    name: "admincategorycreate",
                    pattern: "admin/categories/create",
                    defaults: new { controller = "Admin", action = "CategoryCreate" }
                );

                endpoints.MapControllerRoute(
                    name: "admincategoryedit",
                    pattern: "admin/categories/{id?}",
                    defaults: new { controller = "Admin", action = "CategoryEdit" }
                );

                // localhost/search
                endpoints.MapControllerRoute(
                    name: "search",
                    pattern: "search",
                    defaults: new { controller = "Shop", action = "Search" }
                );

                endpoints.MapControllerRoute(
                    name: "productdetails",
                    pattern: "{url}",
                    defaults: new { controller = "Shop", action = "Details" }
                );

                endpoints.MapControllerRoute(
                    name: "productsbycategories",
                    pattern: "products/{category?}/{orderby?}",
                    defaults: new { controller = "Shop", action = "List" }
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });




            SeedIdentity.Seed(userManager, roleManager, cartService, configuration).Wait();

        }
    }
}
