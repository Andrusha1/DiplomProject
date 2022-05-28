using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace DiplomProject.Controllers
{
    public class CalculateController : Controller
    {
        private readonly DatabaseContext context;

        public CalculateController(DatabaseContext context)
        {
            this.context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public void Algorythm()
        {
            List<int> targetFunction = new List<int>();

            string streetName;
            int numberOfHouses;
            string typeOfHouses;
            int placesAmount;
            int rentPrice;
            int parkingPlaces;
            long Streetid;
            double coefType; //Коэффицент неликвидности

            int amountOfStreets = context.streets.Select(a => a.id).Count() - 1;

            for (int x = 1; x <= context.streets.Select(a => a.id).Count(); x++)
            {

                for (int i = 2; i <= context.streets.Select(a => a.id).Count(); i++)
                {
                    streetName = context.streets.Where(a => a.id == i).Select(s => s.name).First(); //Выбираем переменные из таблицы streets
                    numberOfHouses = context.streets.Where(a => a.id == i).Select(s => s.numbersOfHouses).First();
                    typeOfHouses = context.streets.Where(a => a.id == i).Select(s => s.typeOfHouses).First();

                    switch (typeOfHouses) //Ловим тип домов
                    {
                        case "Частный сектор":
                            coefType = 50;
                            break;
                        case "Сталинки":
                            coefType = 30;
                            break;
                        case "Хрущевки":
                            coefType = 20;
                            break;
                        case "Высотки":
                            coefType = 10;
                            break;
                        default:
                            coefType = 10;
                            break;
                    }

                    for (int j = 2; j <= context.areas.Select(a => a.id).Count(); j++)
                    {
                        placesAmount = context.areas.Where(a => a.id == j).Select(s => s.placesAmount).First(); //Выбираем переменные из таблицы areas
                        rentPrice = context.areas.Where(a => a.id == j).Select(s => s.rentPrice).First();
                        parkingPlaces = context.areas.Where(a => a.id == j).Select(s => s.parkingPlaces).First();
                        Streetid = context.areas.Where(a => a.id == j).Select(s => s.Streetid).First();

                        var result = ((placesAmount * parkingPlaces) - rentPrice) / (numberOfHouses * coefType);
                        targetFunction.Add((int)result);
                    }
                }
            }
        }
    }
}