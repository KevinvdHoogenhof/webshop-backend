using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopBackend.Data;
using WebshopBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace WebshopBackend.Services
{
    public class ProductService : IProductService
    {
        private readonly IWebshopContext _context;
        public ProductService(IWebshopContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToArray();
        }
        public Product GetProduct(int id)
        {
            try
            {
                return _context.Products.Where(p => p.Id == id).Include(p => p.Comments).Single();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"Could not find product with id {id}");
            }
        }
        public int InsertProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.Id;
        }
        public bool DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
        public bool UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}