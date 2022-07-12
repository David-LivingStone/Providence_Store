using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvidenceStore.Data;
using ProvidenceStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvidenceStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly StoreDbContext storeDbContext;

        public ProductController(StoreDbContext storeDbContext)
        {
            this.storeDbContext = storeDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            return Ok(await storeDbContext.Product.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductByID([FromRoute] Guid id)
        {
            var prod = await storeDbContext.Product.FindAsync(id);
            if (prod == null)
            {
                return NotFound();
            }
            return Ok(prod);
        }

        [HttpPost]
        [ActionName("GetPrductByID")]
        public async Task<IActionResult> AddProduct(ProductModel productModel)
        {
            productModel.id = Guid.NewGuid();
            productModel.Date = DateTime.Now.ToShortDateString().ToString();
            await storeDbContext.Product.AddAsync(productModel);
            await storeDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductByID), new { id = productModel.id }, productModel);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] ProductModel updateproduct)
        {
            var existingProd = await storeDbContext.Product.FindAsync(id);

            if (existingProd == null)
            {
                return NotFound();
            }

            existingProd.ProductName = updateproduct.ProductName != null ? updateproduct.ProductName : existingProd.ProductName;
            existingProd.Quantity = updateproduct.Quantity != 0 ? updateproduct.Quantity : existingProd.Quantity;
            existingProd.Price = updateproduct.Price != 0 ? updateproduct.Price : existingProd.Price;
            existingProd.Description = updateproduct.Description != null ? updateproduct.Description : existingProd.Description;
            existingProd.Category = updateproduct.Category != null ? updateproduct.Category : existingProd.Category;
            existingProd.Date = updateproduct.Date != null ? updateproduct.Date : existingProd.Date;

            await storeDbContext.SaveChangesAsync();

            return Ok(existingProd);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProd( Guid id)
        {
            var existingProd = await storeDbContext.Product.FindAsync(id);

            if (existingProd == null)
            {
                return NotFound();
            }
            storeDbContext.Product.Remove(existingProd);
            await storeDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("productcount")]
        public IActionResult CountAllProducts()
        {
            
            var count = storeDbContext.Product.Count();

            return Ok(new { count = count });
        }

    }
}
