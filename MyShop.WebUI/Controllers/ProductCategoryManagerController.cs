using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.Memory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;

        public IActionResult HttpException()
        {
            throw new NotImplementedException();
        }

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }

        public IActionResult Index()
        {
            List<ProductCategory> productsCategories = context.Collection().ToList();

            return View(productsCategories);
        }

        public IActionResult Create()
        {
            ProductCategory productCateory = new ProductCategory();
            return View(productCateory);
        }
        [HttpPost]
        public IActionResult Create(ProductCategory productCateory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCateory);
            }
            else
            {
                context.Insert(productCateory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(string ID)
        {
            ProductCategory productCateory = context.Find(ID);
            if (productCateory == null)
            {
                return HttpException();
            }
            else
            {

                return View(productCateory);
            }
        }

        [HttpPost]
        public IActionResult Edit(ProductCategory productCateory, string ID)
        {
            ProductCategory productCategoryToEdit = context.Find(ID);
            if (productCategoryToEdit == null)
            {
                return HttpException();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCateory);
                }

                productCategoryToEdit.Category = productCateory.Category;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(string ID)
        {
            ProductCategory productCategory = context.Find(ID);
            if (productCategory == null)
            {
                return HttpException();
            }
            else
            {

                return View(productCategory);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string ID)
        {
            ProductCategory productCategoryToDelete = context.Find(ID);
            if (productCategoryToDelete == null)
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
