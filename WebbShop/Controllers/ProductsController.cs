using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbShop.Models;

namespace WebbShop.Controllers
{
    public class ProductsController : Controller
    {
        OrderViewModel ViewModel = new OrderViewModel();
        [HttpGet]
        public IActionResult IndexProducts()
        {
            // Get the list of values
            ViewModel.Productlist = Data.GetList();

            return View(ViewModel);
        }
        [HttpGet]
        public IActionResult ViewProduct(int ID)
        {
            ViewModel.Release = DateTime.Now;
            ViewModel.Productlist = Data.GetList();

            var Filterproduct = from e in ViewModel.Productlist
                                where e.ID == ID
                                select e;

            var Product = Filterproduct.ToList()[0];
            ViewModel.CartList.Add(Product);

            return View(ViewModel);
        }
    }
    class Data
    {
        public static List<Products> GetList()
        {
            List<Products> ListOfProducts = new List<Products>()
            {
                new Products() { ID = 1, Name = "PC", Description = "Deskop", Price = 14999M, Maker = "Acer" },
                new Products() { ID = 2, Name = "TV", Description = "55 Tum", Price = 8999M, Maker = "Philips" },
                new Products() { ID = 3, Name = "Headphones", Description = "Iphone", Price = 799M, Maker = "Apple"},
                new Products() { ID = 4, Name = "Keyboard", Description = "Gaming", Price = 1050M, Maker = "Razor"}
            };

            return ListOfProducts;
        }
    }
}
//Vid refresh läggs en ny produkt till i cookiestringen - försök fixa det
//CSS (klar just nu 2020-05-02)
// Efter du kollat på microservices (kolla vad det är)
//TODO (senare) lägg till admin sida för att skapa/editera/ta bort produkter
// - skapa en tabell med admin
// - controllera om inputsen från fältet matchar med adminen i databasen
// - när man är inloggad som admin ska man kunna lägga till nya admins
//Modifiering varukorg (+-)
