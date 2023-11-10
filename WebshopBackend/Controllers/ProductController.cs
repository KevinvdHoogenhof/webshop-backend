using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebshopBackend.Data;
using WebshopBackend.Models;
using WebshopBackend.Services;
using WebshopBackend.ViewModels;

namespace WebshopBackend.Controllers
{
    [Authorize(Roles = "Admin")]
    [EnableCors("cors")]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IJWTService _jwt;
        public ProductController(IWebshopContext context, IConfiguration config)
        {
            _service = new ProductService(context);
            _jwt = new JWTService(config);
        }
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<ProductViewModel> Get()
        {
            List<Product> products = _service.GetProducts().ToList();
            List<ProductViewModel> pvm = new();
            for (int i = 0; i < products.Count(); i++)
            {
                pvm.Add(new(products[i]));
            }
            return pvm.ToArray();
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ProductViewModel Get(int id)
        {
            Product p = _service.GetProduct(id);
            return new(p);
        }
        [HttpPost]
        public ProductViewModel Post( string name, string description, string image)
        {
            Product p = new(){Name = name, Description = description, Image = image};
            return new(_service.InsertProduct(p));
        }
        [HttpPut("{id}")]
        public ProductViewModel Put(int id, string name, string description, string image)
        {
            Product p = new(){Id = id, Name = name, Description = description, Image = image};
            return new(_service.UpdateProduct(p));
        }
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _service.DeleteProduct(id);
        }

    }
}
