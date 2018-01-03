using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.App_Start;
using SportsStore.WebUI.Binders;
using SportsStore.WebUI.Infrastructure;

namespace SportsStore.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {

            Database.SetInitializer(new CreateDatabaseIfNotExists<EFDbContext>()); //For Changes
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //We need to tell MVC that we want to use the NinjectController 
            //class to create controller objects
            ControllerBuilder.Current.SetControllerFactory(new
                         NinjectControllerFactory());

            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            //Added end

            AuthConfig.RegisterAuth();
        }
    }
}
