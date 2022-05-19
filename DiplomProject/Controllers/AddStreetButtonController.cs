using DiplomProject.Data;
using DiplomProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomProject.Controllers
{
    public class AddStreetButtonController : Controller
    {
        private readonly DatabaseContext context;
        public AddStreetButtonController(DatabaseContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddStreet(string name, int numberOfHouses, string typeOfHouses, int placesAmount, int rentPrice, int parkingPlaces)
        {
            context.streets.Add(new Street
            {
                name = name,
                numbersOfHouses = numberOfHouses,
                typeOfHouses = typeOfHouses,
                areas = new List<Area>
                {
                     new Area{parkingPlaces = parkingPlaces, rentPrice = rentPrice, placesAmount = placesAmount},
                }
            });
            
            return View();
        }
    }
}
