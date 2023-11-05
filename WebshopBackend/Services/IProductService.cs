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
        public int InsertProduct(Product product);
        public bool DeleteProduct(int id);
        public bool UpdateProduct(Product product);
    }
}