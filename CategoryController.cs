using bookWeb.Data;
using bookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace bookWeb.Controllers
{
    public class CategoryController : Controller
    {

    private readonly ApplicationDbContext _db;


        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                //ModelState.AddModelError("Custom Error", "The DisplayOrder cannot exactly match the Name."); // Custom validation
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name."); // This one also puts it under the fields.
                // One thing to remember here is all of the error checking is on the server side, which causes a page reload.  We can put this 
                // on the client side to make it faster.
            }
            if(ModelState.IsValid) // What this does is make sure that all required entries are filled.  We said that name and key are reqiured.
                // If we don't provide those when we hit create we don't want to write to the database, and we want to display an error message
                // to the user.
            {
                _db.Categories.Add(obj); // Adds to the database but doesn't push to database right now
                _db.SaveChanges(); // This pushes to the database
                return RedirectToAction("Index");  // Returns to view from same controller, you can specify a different controller.
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); // Here is the change to update the property instead of writing a new one. 
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }





        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj); // Here is the change to update the property instead of writing a new one. 
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
