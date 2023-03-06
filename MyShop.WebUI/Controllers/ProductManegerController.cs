using MyShop.Core.Models;
using MyShop.DataAccess.Memory;
using System;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManegerController : Controller
    {
        ProductRepository context;

        public IActionResult HttpException()
        {
            throw new NotImplementedException();
        }

        public ProductManegerController()
        {
            context = new ProductRepository();
        }

        public IActionResult Index()
        {
            List<Product> products = context.Collection().ToList(); 
            
            return View(products);
        }

        public IActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(string ID)
        {
            Product product = context.Find(ID);
            if (product == null)
            {
                return HttpException();
            }
            else{
            
                return View(product);            
            }
        }

        [HttpPost]
        public IActionResult Edit(Product product, string ID)
        {
            Product productToEdit = context.Find(ID);
            if (productToEdit == null)
            {
                return HttpException();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();
                
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(string ID)
        {
            Product product = context.Find(ID);
            if (product == null)
            {
                return HttpException();
            }
            else
            {

                return View(product);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string ID)
        {
            Product productToDelete = context.Find(ID);
            if (productToDelete == null)
            {
                return HttpException();
            }
            else
            {

                context.Delete(ID);
                return RedirectToAction("Index");
            }
        }
    }
}
