using cimob.Data;
using cimob.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cimob.Services
{
    public class ApplicationStatus
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationStatus(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        internal void HasCandidatura(ClaimsPrincipal user)
        {
            //return _context.Candidaturas.Where(c => c.UtilizadorID == user).Count > 0;
        }
    }
}