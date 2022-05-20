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

        public void AddStreet(string name, int numberOfHouses, string typeOfHouses, int placesAmount, int rentPrice, int parkingPlaces)
        {
            if (name == null)
            {
                //Selected Street in droplist
                context.areas.Add(new Area
                {
                    parkingPlaces = parkingPlaces,
                    rentPrice = rentPrice,
                    placesAmount = placesAmount,
                });
            }
            else
            {
                context.streets.Add(new Street
                {
                    name = name,
                    numbersOfHouses = numberOfHouses,
                    typeOfHouses = typeOfHouses,
                });
                context.areas.Add(new Area
                {
                    parkingPlaces = parkingPlaces,
                    rentPrice = rentPrice,
                    placesAmount = placesAmount,
                });
            }
            context.SaveChanges();
        }
    }
}
