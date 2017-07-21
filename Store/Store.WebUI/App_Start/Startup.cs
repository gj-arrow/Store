using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Store.Domain.Concrete;
using Store.Domain.Entities;
using Store.Domain.Abstract;
using Store.WebUI.Controllers;

[assembly: OwinStartup(typeof(Store.WebUI.App_Start.Startup))]

namespace Store.WebUI.App_Start
{
        public class Startup
        {
            public void Configuration(IAppBuilder app)
            {
            IProductRepository repo = new EFProductRepository();
           // new ProductController(repo).List(null, "Sort", 1);

            //Category deletedCategory = repo.DeleteCategory(10);
            // настраиваем контекст и менеджер
            app.CreatePerOwinContext<ApplicationDbContext>(ApplicationDbContext.Create);
                app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
                app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/Account/Login"),
                });
            }
        }
    }

