using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbShop.Data;
using WebbShop.Services;

namespace WebbShop.Controllers
{
    public class AccountController : Controller
    {
        readonly ApplicationDbContext _context;
        OrderService _orderService = new OrderService();
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult DeleteAccount()
        {
            var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var account = _context.Users.Where(e => e.Id == UserID).SingleOrDefault();
            _context.Users.Remove(account);
            _context.SaveChanges();


            return RedirectToAction("Index", "Home");
        }
    }
}