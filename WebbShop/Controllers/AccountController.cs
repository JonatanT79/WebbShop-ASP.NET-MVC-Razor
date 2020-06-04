using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebbShop.Data;
using WebbShop.Services;

namespace WebbShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        readonly ApplicationDbContext _context;
        OrderService _orderService = new OrderService();
        public AccountController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> DeleteAccount()
        {
            var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var account = _context.Users.Where(e => e.Id == UserID).SingleOrDefault();
            _context.Users.Remove(account);
            _context.SaveChanges();

            await _orderService.DeleteAllUserOrderAsync(UserID);
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}