using WebshopBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;

namespace WebshopBackend.Services
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();
        public Product GetProduct(int id);
        public Product InsertProduct(Product product);
        public Product UpdateProduct(Product product);
        public bool DeleteProduct(int id);
    }
}