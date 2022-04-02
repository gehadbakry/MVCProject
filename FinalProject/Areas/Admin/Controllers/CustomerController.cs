using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Areas.Admin.Models;
using FinalProject.Services;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        public ICustomerRepos CustomerRepos { get; set; }
        public IItemRepos ItemRepos { get; set; }
        public ICategoryRepos CategoryRepos { get; set; }

        public CustomerController(ICustomerRepos customerRepos,IItemRepos itemRepos,ICategoryRepos categoryRepos)
        {
            CustomerRepos=customerRepos;
            ItemRepos = itemRepos;
            CategoryRepos=categoryRepos;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            var rand = new Random();
            List<Item> Items = ItemRepos.GetAll();
            List<Item> NItems = new List<Item>();
            for(int i=0;i<CategoryRepos.GetAll().Count();i++)
            {
               
                for(int j=0;j<3;j++)
                {

                    NItems.Add( Items.Where(e => e.CategoryID == i+1).ElementAt(rand.Next(Items.Where(e => e.CategoryID == i + 1).Count())));

                }
            }


            return View(NItems);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
