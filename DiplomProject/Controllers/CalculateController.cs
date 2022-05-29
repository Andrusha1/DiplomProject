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

        public static List<double> targetFunction = new List<double>();

        
        public CalculateController(DatabaseContext context)
        {
            this.context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var maxtf = targetFunction.IndexOf(targetFunction.Max()) + 2;
            ViewBag.TarFuncStreetName = context.streets.Where(a => a.id == maxtf).Select(t => t.name).First();
            ViewBag.TarFuncMax = targetFunction.Max();
            return View();
        }

        public void Algorythm()
        {
            

            string streetName;
            int numberOfHouses;
            string typeOfHouses;
            int placesAmount;
            int rentPrice;
            int parkingPlaces;
            long Streetid; //id улицы в таблице areas 
            long streetsId; //id улицы в таблице Streets
            double coefType; //Коэффицент неликвидности


            int amountOfStreets = context.streets.Select(a => a.id).Count() - 1;

                for (int i = 2; i <= context.streets.Select(a => a.id).Count(); i++)
                {
                    streetsId = context.streets.Where(a => a.id == i).Select(s => s.id).First();
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

                        if (Streetid == streetsId)
                        {
                            var result = ((placesAmount * parkingPlaces) - rentPrice) / (numberOfHouses * coefType);
                        
                            targetFunction.Add(Math.Round(result, 2));
                        }
                    }
                }
            Response.Redirect("/Calculate/Index");
            }
        }
    }