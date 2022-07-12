using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvidenceStore.Data;
using ProvidenceStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProvidenceStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly StoreDbContext storeDbContext;

        public OrderController(StoreDbContext storeDbContext)
        {
            this.storeDbContext = storeDbContext;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            return Ok(await storeDbContext.Order.ToListAsync());
        }

        [HttpGet("ordercount")]
        public IActionResult CountAllOrders()
        {
           var sum = storeDbContext.Order.Sum(e=>e.TotalAmount);
           var count = storeDbContext.Order.Count();

            return Ok(new { sum=sum, count=count });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderByID([FromRoute] Guid id)
        {
            var prodOrder = await storeDbContext.Order.FindAsync(id);
            if (prodOrder == null)
            {
                return NotFound();
            }
            return Ok(prodOrder);
        }



        [HttpPost]
        [ActionName("PostOrder")]
        public async Task<IActionResult> AddOrder(OrderModel orderModel)
        {
            try
            {

                var prod = storeDbContext.Product.FirstOrDefault(er => er.id.ToString() == orderModel.ProductId);

                if(prod.Quantity < orderModel.Quantity)
                {
                    return BadRequest("Insuficient Products in store");
                }

                orderModel.id = Guid.NewGuid();
                var response = await storeDbContext.Order.AddAsync(orderModel);

                var reduce = prod.Quantity - orderModel.Quantity;

                prod.ProductName = prod.ProductName;
                prod.Quantity = reduce;
                prod.Price = prod.Price;
                prod.Description = prod.Description;
                prod.Category = prod.Category;
                prod.Date = prod.Date;

                await storeDbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOrderByID), new { id = orderModel.id }, orderModel);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return NotFound();
            }
        }

        [HttpGet ("Dashboard")]
        public IActionResult Dashboard()
        {
            var Pcount = storeDbContext.Product.Count();
            var Ordercount = storeDbContext.Order.Count();
            var sum = storeDbContext.Order.Sum(e => e.TotalAmount);
            var catCount = storeDbContext.Product.Select(er=> new { er.Category, er.Quantity }).ToList();

            return Ok(new { Pcount =Pcount, Ordercount = Ordercount, sum=sum, allCount = catCount });
        }



    }
}
