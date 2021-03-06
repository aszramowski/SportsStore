using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Product> Products => this.context.Products;

        public Product DeleteProduct(int productId)
        {
            Product p = context.Products.Where(x => x.ProductId == productId).FirstOrDefault();
            if (p != null)
            {
                context.Products.Remove(p);        
                context.SaveChanges();
            };

            return p;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
                if (dbEntry != null)                    
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.Description = product.Description;
                };
            }
            context.SaveChanges();
        }
    }
}
