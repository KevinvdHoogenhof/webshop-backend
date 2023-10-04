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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IJWTService _jwt;
        public AccountController(IWebshopContext context, IConfiguration config)
        {
            _service = new AccountService(context);
            _jwt = new JWTService(config);
        }
        
        [HttpPost("Register")]
        public TokenViewModel Register(string name, string email, string password)
        {
            bool registered = true;
            if (registered)
            {
                return new($"Register test + {name} + {email}");
            }
            else
            {
                throw new InvalidOperationException("Invalid info");
            }
        }
        [HttpPost("Login")]
        public TokenViewModel Login(string email, string password)
        {
            return new($"Login test + {email}");
        }
    }
}
