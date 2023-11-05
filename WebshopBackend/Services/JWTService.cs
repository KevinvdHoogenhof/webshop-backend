﻿using WebshopBackend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebshopBackend.Services
{
    public class JWTService : IJWTService
    {
        private IConfiguration _config;
        public static readonly string ClaimsName = "Name";
        public static readonly string ClaimsEmail = "Email";
        public static readonly string ClaimsRole = "Role";
        public JWTService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Account account)
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }

        public string GetClaim(string token, string claimType)
        {
            throw new NotImplementedException();
        }
    }
}