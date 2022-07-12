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
    public class CategoryController : Controller
    {

        private readonly StoreDbContext storeDbContext;

        public CategoryController(StoreDbContext storeDbContext)
        {
            this.storeDbContext = storeDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await storeDbContext.Category.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategoryByID([FromRoute] Guid id)
        {
            var prodCat = await storeDbContext.Category.FindAsync(id);
            if (prodCat == null)
            {
                return NotFound();
            }
            return Ok(prodCat);
        }
        [HttpPost]
        [ActionName("GetCategoryByID")]
        public async Task<IActionResult> AddProduct(CategoryModel categoryModel)
        {
            categoryModel.id = Guid.NewGuid();
            await storeDbContext.Category.AddAsync(categoryModel);
            await storeDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoryByID), new { id = categoryModel.id }, categoryModel);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] CategoryModel updatecategory)
        {
            var existingCat = await storeDbContext.Category.FindAsync(id);

            if (existingCat == null)
            {
                return NotFound();
            }

            existingCat.CategoryName = updatecategory.CategoryName != null ? updatecategory.CategoryName : existingCat.CategoryName;

            await storeDbContext.SaveChangesAsync();

            return Ok(existingCat);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCat(Guid id)
        {
            var existingCat = await storeDbContext.Category.FindAsync(id);

            if (existingCat == null)
            {
                return NotFound();
            }
            storeDbContext.Category.Remove(existingCat);
            await storeDbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
