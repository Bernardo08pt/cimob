using cimob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimob
{
    [Route("api/APICreateUser")]
    public class APICreateUser
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public APICreateUser(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> APILogin(string email, string password)
        {


            return await _signInManager.CheckPasswordSignInAsync(new ApplicationUser(), password, false);
        }
    }
}
