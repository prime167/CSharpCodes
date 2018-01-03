using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using Moq;
using Ninject;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {

        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            var mock = new Mock<IProductsRepository>();

            mock.Setup(m => m.Products).Returns(new List<Product> {
                    new Product { Name = "Football", Price = 25 },
                    new Product { Name = "Surf board", Price = 179 },
                    new Product { Name = "Running shoes", Price = 95 }
                }.AsQueryable());

            //ninjectKernel.Bind<IProductsRepository>().ToConstant(mock.Object);
            ninjectKernel.Bind<IProductsRepository>().To<EFProductRepository>();

            var emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings); 
        }
    }
}