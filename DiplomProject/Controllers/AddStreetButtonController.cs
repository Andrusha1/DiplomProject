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

        public List<string> DropList() //DropList from db \/
        {
            List<string> streets = new List<string>();
            foreach (var a in context.streets)
            {
                streets.Add(a.name);
            };
            return streets; //DropList from db /\
        }

        public void AddStreet(string StreetDropList, string name, int numberOfHouses, string typeOfHouses, int placesAmount, int rentPrice, int parkingPlaces)
        {
            if (name == null)
            {
                name = StreetDropList;
                var idDb = context.streets.Where(c => c.name == StreetDropList).Select(a => a.id);

                context.areas.Add(new Area
                {
                    Streetid = idDb.First(),
                    parkingPlaces = parkingPlaces,
                    rentPrice = rentPrice,
                    placesAmount = placesAmount,
                });
                context.SaveChanges();
            }
            else
            {
                context.streets.Add(new Street //вот тут
                {
                    name = name,
                    numbersOfHouses = numberOfHouses,
                    typeOfHouses = typeOfHouses,
                });
                context.SaveChanges();

                var idCreated = context.streets.Where(c => c.name == name).Select(a => a.id);

                context.areas.Add(new Area
                {
                    Streetid = idCreated.First(), //id = id созданной улице вон там сверху
                    parkingPlaces = parkingPlaces,
                    rentPrice = rentPrice,
                    placesAmount = placesAmount,
                });

                context.SaveChanges();
            }
            Response.Redirect("/Home/Index");
        }
    }
}
