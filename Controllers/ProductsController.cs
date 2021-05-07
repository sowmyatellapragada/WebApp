using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private DataContext context;
        public ProductsController(DataContext ctx)
        {
            context = ctx;
        }
        [HttpGet]
        public IAsyncEnumerable<Product> GetProducts()
        {
            return context.Products;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetProduct(long id)
        {
            Product p = await context.Products.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId
            });
        }
        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            //return Redirect("/api/products/1");
            return RedirectToAction(nameof(GetProduct), new { Id = 1 });
        }

        //public async Task<Product> GetProduct(long id)
        //{
        //    return await context.Products.FindAsync(id);
        //}

        // public Product GetProduct(long id,[FromServices] ILogger<ProductsController> logger)
        // {
        //     logger.LogDebug("GetProduct Action Invoked");
        //     //return context.Products.FirstOrDefault();
        //     return context.Products.Find(id);
        // }
        [HttpPost]
        //public void SaveProduct([FromBody] Product product)
        //{
        //    context.Products.Add(product);
        //    context.SaveChanges();
        //}
        //public async Task SaveProduct([FromBody] ProductBindingTarget target)
        //{
        //    await context.Products.AddAsync(target.ToProduct());
        //    await context.SaveChangesAsync();
        //}
        //    public async Task<IActionResult> SaveProduct([FromBody] ProductBindingTarget target)
        //    {
        //    if (ModelState.IsValid)
        //    {
        //        Product p = target.ToProduct();
        //        await context.Products.AddAsync(p);
        //        await context.SaveChangesAsync();
        //    return Ok(p);
        //    }
        //    return BadRequest(ModelState);
        //}
        public async Task<IActionResult> SaveProduct(ProductBindingTarget target)
        {
            Product p = target.ToProduct(); await context.Products.AddAsync(p);
            await context.SaveChangesAsync();
            return Ok(p);
        }

            [HttpPut]
        public async Task UpdateProduct(Product product)
        {
            //public void UpdateProduct([FromBody] Product product)

            //context.Products.Update(product);
            //context.SaveChanges();
            context.Update(product);
            await context.SaveChangesAsync();
        }
        //public async Task UpdateProduct([FromBody] Product product)
        //{
        //    context.Update(product);
        //    await context.SaveChangesAsync();
        //}

        [HttpDelete("{id}")]
        //public void DeleteProduct(long id)
        //{
        //    context.Products.Remove(new Product() { ProductId = id });
        //    context.SaveChanges();
        //}
        public async Task DeleteProduct(long id)
        {
            context.Products.Remove(new Product() { ProductId = id });
            await context.SaveChangesAsync();
        }
    }
}