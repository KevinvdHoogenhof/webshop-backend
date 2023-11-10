using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebshopBackend.Data;
using WebshopBackend.Models;
using WebshopBackend.Services;
using WebshopBackend.ViewModels;

namespace WebshopBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IJWTService _jwt;
        public ProductController(IWebshopContext context, IConfiguration config)
        {
            _service = new AccountService(context);
            _jwt = new JWTService(config);
        }
        [HttpGet]
        public IEnumerable<ProductViewModel> Get()
        {
            
        }
    }
}
