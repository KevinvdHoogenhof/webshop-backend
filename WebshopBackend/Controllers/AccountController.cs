using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
        private readonly ILogger _logger;
        public AccountController(IWebshopContext context, IConfiguration config, ILogger<AccountController> logger)
        {
            _service = new AccountService(context);
            _jwt = new JWTService(config);
            _logger = logger;
        }
        //
        //Testing only
        //
        ///*
        [HttpGet]
        public IEnumerable<AccountViewModel> Get()
        {
            _logger.LogInformation($"Endpoint called, additional information: {GetRequestDetails()}");
            List<Account> accounts = _service.GetAccounts().ToList();
            List<AccountViewModel> avms = new();
            for (int i = 0; i < accounts.Count(); i++)
            {
                avms.Add(new(accounts[i]));
            }
            return avms.ToArray();
        }
        //*/
        //
        //
        //
        [HttpPost("Register")]
        public TokenViewModel Register(RegisterAccountViewModel account)
        {
            if(!ModelState.IsValid)
                throw new Exception("Invalid modelstate");
            if(!IsEmailValid(account.Email))
                throw new Exception("Invalid email");
            if(!IsPasswordValid(account.Password))
                throw new Exception("Invalid password");

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
            _logger.LogInformation($"Endpoint called, additional information: {GetRequestDetails()}");
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
        private bool IsEmailValid(string email)
        {
            for (int i = 0; i < email.Length; i++)
            {
                if (email[i] == '@')
                {
                    for (int z = i+2; z < email.Length; z++)
                    {
                        if (email[z] == '.' && email.Length > z)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool IsPasswordValid(string password)
        {
            string lowercase = "abcdefghijklmnopqrstuvwxyz";
            string uppercase = lowercase.ToUpper();
            string numbers = "0123456789";
            string special = "!_@";
            bool lower = false;
            bool upper = false;
            bool num = false;
            bool spec = false;
            for (int z = 0; z < password.Length; z++)
            {
                for (int i = 0; i < lowercase.Length; i++)
                {
                    if (password[z].Equals(lowercase[i]))
                    {
                        lower = true;
                    }
                }
                for (int i = 0; i < uppercase.Length; i++)
                {
                    if (password[z].Equals(uppercase[i]))
                    {
                        upper = true;
                    }
                }
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (password[z].Equals(numbers[i]))
                    {
                        num = true;
                    }
                }
                for (int i = 0; i < special.Length; i++)
                {
                    if (password[z].Equals(special[i]))
                    {
                        spec = true;
                    }
                }
            }

            if(lower && upper && num && spec)
            {
                return true;
            }            
            return false;
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
