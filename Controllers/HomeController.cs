using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using WebApp.Filters;

using WebApp.Models;

namespace WebApp.Controllers
{

    
    //[ResultDiagnostics]
    //[GuidResponse]
    //[GuidResponse]
    //[HttpsOnly]
    [AutoValidateAntiforgeryToken]

    public class HomeController : Controller
    {
        private DataContext context;
        private IEnumerable<Category> Categories => context.Categories;
        private IEnumerable<Supplier> Suppliers => context.Suppliers;
        public HomeController(DataContext data)
        {
            context = data;
        }
        public IActionResult Index()
        {
            return View(context.Products.Include(p => p.Category).Include(p => p.Supplier));
        }
        public async Task<IActionResult> Details(long id)
        {
            Product p = await context.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefaultAsync(p => p.ProductId == id);
            ProductViewModel model = ViewModelFactory.Details(p);
            return View("ProductEditor", model);
        }
        public IActionResult Create()
        {
            return View("ProductEditor",ViewModelFactory.Create(new Product(), Categories,Suppliers));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductId = default;
                product.Category = default;
                product.Supplier = default;
                context.Products.Add(product);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("ProductEditor", ViewModelFactory.Create(product, Categories,Suppliers));
        }
        public async Task<IActionResult> Edit(long id)
        {
            Product p = await context.Products.FindAsync(id);
            ProductViewModel model = ViewModelFactory.Edit(p,Categories, Suppliers); 
            return View("ProductEditor", model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm]Product product)
        {
            if (ModelState.IsValid)
            {
                product.Category = default;
                product.Supplier = default;
                context.Products.Update(product);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("ProductEditor",ViewModelFactory.Edit(product, Categories, Suppliers));
        }

        public async Task<IActionResult> Delete(long id)
        {
            ProductViewModel model = ViewModelFactory.Delete(
            await context.Products.FindAsync(id), Categories,Suppliers);
            return View("ProductEditor", model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{


        //        return View("Message", "This is the Index action on the Home controller");

        //}

        //public IActionResult Secure()
        //{

        //        return View("Message","This is the Secure action on the Home controller");

        //}
        //[ChangeArg]
        //public IActionResult Messages(string message1, string message2 = "None")
        //{
        //    return View("Message", $"{message1}, {message2}");
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        //private DataContext context;
        //public HomeController(DataContext ctx)
        //{
        //    context = ctx;
        //}
        //public async Task<IActionResult> Index(long id = 1)
        //{
        //    ViewBag.AveragePrice = await context.Products.AverageAsync(p => p.Price);
        //    return View(await context.Products.FindAsync(id));
        //    //Product prod = await context.Products.FindAsync(id);
        //    //if (prod.CategoryId == 1)
        //    //{
        //    //    return View("Watersports", prod);
        //    //}
        //    //else
        //    //{
        //    //    return View(prod);
        //    //}
        //}
        //public IActionResult Common()
        //{
        //    return View();
        //}
        //public IActionResult List()
        //{
        //    return View(context.Products);
        //}
        //public IActionResult Html()
        //{
        //    return View((object)"This is a <h3><i>string</i></h3>");
        //}

    }

}

