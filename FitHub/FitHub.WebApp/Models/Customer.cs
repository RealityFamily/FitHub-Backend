using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.WebApp.Models
{
    public class Customer : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] Avatar { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
    }
}
