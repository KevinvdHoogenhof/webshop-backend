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
        public Product InsertProduct(Product product)
        {
            try
            {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
            }
            catch
            {
                throw new InvalidOperationException($"Error posting product");
            }
        }
        public Product UpdateProduct(Product product)
        {
            try
            {
                Product p = _context.Products.Find(product.Id);
                p.Name = product.Name;
                p.Description = product.Description;
                p.Image = product.Image;
                _context.SaveChanges();
                return p;
            }
            catch
            {
                throw new InvalidOperationException($"Error updating product with id {product.Id}");
            }
        }
        public bool DeleteProduct(int id)
        {
            try
            {
                Product product = _context.Products.Find(id);
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"Error deleting product with id {id}");
            }
        }
    }
}