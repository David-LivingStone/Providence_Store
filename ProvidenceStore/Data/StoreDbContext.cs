using Microsoft.EntityFrameworkCore;
using ProvidenceStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvidenceStore.Data
{
    public class StoreDbContext: DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<OrderModel> Order { get; set; }
        public DbSet<ProductModel> Product { get; set; }
    }
}
