using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly ILogger _logger;
        public ProductController(IWebshopContext context, IConfiguration config, ILogger<ProductController> logger)
        {
            _service = new ProductService(context);
            _jwt = new JWTService(config);
            _logger = logger;
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
            _logger.LogInformation($"Endpoint called, additional information: {GetRequestDetails()}");
            if(id < 0)
                throw new Exception("Invalid parameter");
            Product p = _service.GetProduct(id);
            return new(p);
        }
        [HttpPost]
        public ProductViewModel Post(ProductPostViewModel product)
        {
            if(!ModelState.IsValid)
                throw new Exception("Invalid modelstate");
            Product p = new(){Name = product.Name, Description = product.Description, Image = product.Image};
            return new(_service.InsertProduct(p));
        }
        [HttpPut("{id}")]
        public ProductViewModel Put(ProductUpdateViewModel product)
        {
            if(!ModelState.IsValid)
                throw new Exception("Invalid modelstate");
            Product p = new(){Id = product.Id, Name = product.Name, Description = product.Description, Image = product.Image};
            return new(_service.UpdateProduct(p));
        }
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if(id < 0)
                throw new Exception("Invalid parameter");
            return _service.DeleteProduct(id);
        }
        private string GetRequestDetails()
        {
            StringBuilder details = new StringBuilder();

            // Request path and query
            details.AppendLine($"Path: {HttpContext.Request.Path}");
            details.AppendLine($"Query: {HttpContext.Request.QueryString}");

            return details.ToString();
        }
    }
}
