using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {

        private IProductsRepository repository;

        private IOrderProcessor orderProcessor;

        public CartController(IProductsRepository repo, IOrderProcessor proc)
        {
            repository = repo; 
            orderProcessor = proc;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                // Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                // GetCart().AddItem(product, 1);
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                //GetCart().RemoveLine(product);
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //private Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear(); return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}