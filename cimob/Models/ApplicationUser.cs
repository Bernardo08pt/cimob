using System;
using Microsoft.AspNetCore.Identity;

namespace cimob.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int Numero { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
