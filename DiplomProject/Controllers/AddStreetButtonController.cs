using DiplomProject.Data;
using DiplomProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var x = DropList(); //DropList from db
            ViewBag.Street = new SelectList(x); //DropList from db
            return View();
        }

        public IEnumerable<string> DropList() //DropList from db \/
        {
            IEnumerable<string> streets = new List<string>();
            foreach (var a in context.streets)
            {
                streets.Append(a.name);
            };
            return streets; //DropList from db /\
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
