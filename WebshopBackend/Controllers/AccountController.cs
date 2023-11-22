using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [EnableCors("cors")]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
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
        public TokenViewModel Register(RegisterAccountViewModel account)
        {
            if(!ModelState.IsValid)
                throw new Exception("Invalid modelstate");
            bool registered = _service.RegisterAccount(account.Name, account.Email, account.Password);
            if (registered)
            {
                Account a = _service.GetAccount(account.Email);
                string token = _jwt.GenerateToken(a);
                return new(token);
            }
            else
            {
                throw new InvalidOperationException("Invalid info");
            }
        }
        [HttpPost("Login")]
        public TokenViewModel Login(LoginAccountViewModel account)
        {
            if(!ModelState.IsValid)
                throw new Exception("Invalid modelstate");
            if (!_service.LoginAccount(account.Email, account.Password))
            {
                throw new InvalidOperationException("Invalid info");
            }
            Account a = _service.GetAccount(account.Email);
            string token = _jwt.GenerateToken(a); 
            return new(token);
        }
        [Authorize]
        [HttpPost("Info")]
        public AccountInfoViewModel Info()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToList();
            return new(claims[0].Value, claims[1].Value, claims[2].Value);
        }
    }
}
