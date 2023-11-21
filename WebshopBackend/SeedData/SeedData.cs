using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebshopBackend.Data;
using WebshopBackend.Models;
using WebshopBackend.Services;

namespace WebshopBackend.SeedData
{
    public static class SeedData
    {
        //Remove later
        private static IEncryptionService _encrypt = new EncryptionService();
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WebshopContext(serviceProvider.GetRequiredService<DbContextOptions<WebshopContext>>()))
            {
                if (!context.Roles.Any())
                {
                    context.Roles.Add(new Role() { Name = "Standard" });
                    context.SaveChanges();
                    context.Roles.Add(new Role() { Name = "Admin" });
                    context.SaveChanges();
                }
                if (!context.Accounts.Any())
                {
                    ////
                    var hashsalt = _encrypt.EncryptPassword("pw1");
                    context.Accounts.Add(new Account() { Name = "name1", Email = "email1@gmail.com", Password = hashsalt.Hash, StoredSalt = hashsalt.Salt, Role = context.Roles.Find(1) });
                    context.SaveChanges();

                    ////
                    var hashsalt2 = _encrypt.EncryptPassword("securepassword");
                    context.Accounts.Add(new Account() { Name = "admin", Email = "admin@gmail.com", Password = hashsalt2.Hash, StoredSalt = hashsalt2.Salt, Role = context.Roles.Find(2) });
                    context.SaveChanges();

                    ////
                    var hashsalt3 = _encrypt.EncryptPassword("pw3");
                    context.Accounts.Add(new Account() { Name = "name3", Email = "email3@gmail.com", Password = hashsalt3.Hash, StoredSalt = hashsalt3.Salt, Role = context.Roles.Find(1) });
                    context.SaveChanges();
                }
                if(!context.Products.Any())
                {
                    context.Products.Add(new Product() { Name = "Cheese", Description = "Nice", Image = "https://nl.wikipedia.org/wiki/Kaas#/media/Bestand:Kaas_op_planken.jpg"});
                    context.SaveChanges();
                    context.Products.Add(new Product() { Name = "Tree", Description = "Green", Image = "https://visie-eo.cdn.eo.nl/w_2520/s3-visie-eo/77069d90-e7e1-47a9-b5e0-41d70e45a956.jpg"});
                    context.SaveChanges();
                    context.Products.Add(new Product() { Name = "Product3", Description = "Buying this can change your life!", Image = ""});
                    context.SaveChanges();
                    context.Products.Add(new Product() { Name = "Product4", Description = "A simple description", Image = ""});
                    context.SaveChanges();
                    context.Products.Add(new Product() { Name = "Product5", Description = "A simple description", Image = ""});
                    context.SaveChanges();
                    context.Products.Add(new Product() { Name = "Product6", Description = "A simple description", Image = ""});
                    context.SaveChanges();
                    context.Products.Add(new Product() { Name = "Product7", Description = "A simple description", Image = ""});
                    context.SaveChanges();
                    context.Products.Add(new Product() { Name = "Product8", Description = "A simple description", Image = ""});
                    context.SaveChanges();
                }
            }
        }
    }
}