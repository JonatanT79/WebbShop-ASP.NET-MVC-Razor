using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebbShop.Data;
using WebbShop.Models;

namespace WebbShop.Areas.Identity.Pages.Account.Manage
{
    public class AddressModel : PageModel
    {
        readonly ApplicationDbContext _context;
        public UserAddress userAddress = new UserAddress();
        public AddressModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Address = _context.UserAddress.Where(e => e.UserID == UserID).SingleOrDefault();
            userAddress = Address;

            return Page();
        }

        public IActionResult OnPost(string Address, string City, string PostalCode)
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var GetAddress = _context.UserAddress.Where(e => e.UserID == UserID).SingleOrDefault();

            GetAddress.Address = Address;
            GetAddress.City = City;
            GetAddress.PostalCode = PostalCode;
            _context.SaveChanges();

            return RedirectToPage();
        }
    }
}
