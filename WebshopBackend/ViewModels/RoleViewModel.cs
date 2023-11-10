using WebshopBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopBackend.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleViewModel(Role role)
        {
            Id = role.Id;
            Name = role.Name;
        }
    }
}
