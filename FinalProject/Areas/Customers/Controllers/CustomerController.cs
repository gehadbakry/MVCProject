using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using FinalProject.Services;
 
using Microsoft.AspNetCore.Identity;


namespace FinalProject.Controllers
{
    [Area("Customers")]
    public class CustomerController : Controller
    {
        //public ICustomerRepos CustomerRepos { get; set; }
        public IItemRepos ItemRepos { get; set; }
        public ICategoryRepos CategoryRepos { get; set; }
        public IOrderRepos OrderRepos { get; set; }
        public IOrdersItemsRepos OrdersItemsRepos { get; }

        public static List<Item> CartItems=new List<Item>();

        private readonly UserManager<IdentityUser> _userManager;
        //public static IdentityUser Customer = new()
        //{
        //    ID = 1,
        //    FirstName = "Ahmed",
        //    LastName = "Ali",
        //    Email = "ASD1@gmail.com",
        //    Address = "Cairo",
        //    Password = "12345678",
        //    PhoneNumber = "012345678"
        //};

        public CustomerController(UserManager<IdentityUser> usermang, IItemRepos itemRepos,
                               ICategoryRepos categoryRepos, IOrderRepos orderRepos, IOrdersItemsRepos ordersItemsRepos)
        {
            _userManager = usermang;
            //CustomerRepos = customerRepos;
            ItemRepos = itemRepos;
            CategoryRepos = categoryRepos;
            OrderRepos = orderRepos;
            OrdersItemsRepos = ordersItemsRepos;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            //CartItems = ItemRepos.GetAll();

            ViewBag.Cats = CategoryRepos.GetAll();
            var rand = new Random();
            List<Item> Items = ItemRepos.GetAll();
            List<Item> NItems = new List<Item>();
            for(int i=0;i<CategoryRepos.GetAll().Count();i++)
            {
               
                for(int j=0;j<3;j++)
                {
                    NItems.Add( Items.FindAll(e => e.CategoryID == i+1).ElementAt(rand.Next(Items.FindAll(e => e.CategoryID == i + 1).Count())));
                }
            }


            return View(NItems);
        }

        // GET: CustomerController/Details/5
        public ActionResult details(int id)
        {
            Item item = ItemRepos.GetDetails(id);
            ViewBag.Cat = CategoryRepos.GetDetails(item.CategoryID);
            ViewBag.Cats = CategoryRepos.GetAll();
            ViewBag.Mes = null;
            return View(item);
        }
        [HttpPost]
        public ActionResult details(int id, int num, string mes="Added")
        {
            Item item = ItemRepos.GetDetails(id);
            for (int i = 0; i < num + 1; i++)
            { CartItems.Add(item); }
            ViewBag.Cat = CategoryRepos.GetDetails(item.CategoryID);
            ViewBag.Cats = CategoryRepos.GetAll();
            ViewBag.Mes = mes;
            
            return View(item);
        }

        //public ActionResult CartAdd(int id)
        //{
        //    Item item = ItemRepos.GetDetails(id);
        //    CartItems.Add(item);
            
        //    return RedirectToAction("details",new {id=item.ID,mes="Added"});
        //}

        // GET: CustomerController/Create
        public ActionResult products()
        {
            ViewBag.Cats = CategoryRepos.GetAll();

            return View(ItemRepos.GetAll());
        }

        public ActionResult Catproducts(int id)
        {
            ViewBag.Cats = CategoryRepos.GetAll();
            ViewBag.Cat = CategoryRepos.GetDetails(id);

            return View(ItemRepos.GetAll().Where(e => e.CategoryID == id));
        }
        public ActionResult AddToCart()
        {
            //CartItems = ItemRepos.GetAll();

            return View(CartItems);
        }

        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            //CartItems = ItemRepos.GetAll();

            var item = CartItems.Find(e => e.ID == id);
            CartItems.Remove(item);

            return View(CartItems);
        }
        
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
       
        public async Task<ActionResult> Checkout()
        {
            var user = await GetCurrentUserAsync();
            //var user = user;
            var userId = user?.Id;
            ViewBag.Cust = user;
            return View(CartItems);
        }

        [HttpPost]
        public async Task<ActionResult> Checkout(List<Item> checkItems)
        {
            var user = await GetCurrentUserAsync();
            CartItems = checkItems;
            ViewBag.Cust = user;
            return View(CartItems);
        }

        [HttpPost]
        public async Task<ActionResult> CheckoutConfirmed(decimal total)
        {
            //CartItems = checkItems;
            //ViewBag.Cust = customer;
            //ViewBag.totalCost = total;

            var user = await GetCurrentUserAsync();
            //var user = user;
            var userId = user?.Id;
            Order order = new()
            {
                CustomerID = userId,
                Date = DateTime.Now,
                TotalCost = total,
                details = "Details"
            };

            OrderRepos.Insert(order);

            var allOrd = OrderRepos.GetAll().ToList();

            order = allOrd.Last();

            foreach (var item in CartItems)
            {
                OrdersItemsRepos.Insert(new OrderItem() { OrderId = order.ID, ItemId = item.ID }); 
            }

            return View();
        }


    }
}
