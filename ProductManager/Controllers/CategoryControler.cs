using Microsoft.AspNetCore.Mvc;
using ProductManager.Data;
using ProductManager.Models;

namespace ProductManager.Controllers
{
    public class CategoryControler : Controller
    {
        private readonly ApplicationDBContext _db;
        public CategoryControler(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var objCategoryList = _db.Categories.ToList();
            IEnumerable<Category> CategoriesList = _db.Categories;
            return View(CategoriesList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The order can not be the same as the name.");
            }
            else if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created succesfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Not found!";
                return NotFound();
            }
            var categoryFromDB = _db.Categories.Find(id);
            if (categoryFromDB == null)
            {
                TempData["error"] = "Not found!";
                return NotFound();
            }
            return View(categoryFromDB);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The name can not be the same as the order.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated succesfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Not found!";
                return NotFound();
            }
            var categoryFromDB = _db.Categories.Find(id);
            if (categoryFromDB == null)
            {
                TempData["error"] = "Not found!";
                return NotFound();
            }
            return View(categoryFromDB);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj != null)
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category deleted succesfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Not found!";
            return NotFound();
        }
    }
}