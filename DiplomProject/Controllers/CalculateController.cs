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
        public static List<long> StreetID = new List<long>();
        public static List<string> StreetsNames = new List<string>();
        public static List<int> OptimalPrice = new List<int>();
        public static List<double> placeRate = new List<double>();

        public CalculateController(DatabaseContext context)
        {
            this.context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            StreetID.Clear();
            StreetsNames.Clear();
            foreach (var x in context.streets)
            {
                StreetID.Add(x.id);
            }
            foreach (var x in context.streets)
            {
                StreetsNames.Add(x.name);
            }
            var targid = targetFunction.IndexOf(targetFunction.Max());
            var strid = StreetID.ElementAt(targid);
            var optPrice = OptimalPrice.ElementAt(targid);
            ViewBag.TarFuncStreetName = context.streets.Where(a => a.id == strid).Select(s => s.name).First();
            ViewBag.TarFuncMax = targetFunction.Max(); //Максимальная целевая функция
            ViewBag.Allstreets = StreetsNames.ToArray(); //Для графиков
            ViewBag.Alltarfunc = targetFunction.ToArray(); //Для графиков
            ViewBag.optPrice = OptimalPrice.ElementAt(targid); //Оптимальная стоимость
            ViewBag.placeRate = placeRate.ElementAt(targid); //Рейтинг парковочных мест
            targetFunction.Clear();
            OptimalPrice.Clear();
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
            for (long i = context.streets.Select(a => a.id).First(); i <= context.streets.Select(a => a.id).Max(); i++)
            {
                if (context.streets.Where(a => a.id == i).Select(s => s.id).FirstOrDefault() != 0)
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

                    for (long j = context.areas.Select(a => a.id).First(); j <= context.areas.Select(a => a.id).Max(); j++)
                    {
                        if (context.areas.Where(a => a.id == j).Select(s => s.placesAmount).FirstOrDefault() != 0)
                        {
                            placesAmount = context.areas.Where(a => a.id == j).Select(s => s.placesAmount).FirstOrDefault(); //Выбираем переменные из таблицы areas
                            rentPrice = context.areas.Where(a => a.id == j).Select(s => s.rentPrice).FirstOrDefault();
                            parkingPlaces = context.areas.Where(a => a.id == j).Select(s => s.parkingPlaces).FirstOrDefault();
                            Streetid = context.areas.Where(a => a.id == j).Select(s => s.Streetid).FirstOrDefault();

                            double result;
                            double payrate = numberOfHouses / coefType;
                            double newPlaceAmount;

                            if (Streetid == streetsId)
                            {
                                if (coefType == 10 && placesAmount < 5000 && (placesAmount * parkingPlaces - rentPrice) > 50000)
                                {
                                    newPlaceAmount = placesAmount + (payrate * 2);
                                    result = ((newPlaceAmount * parkingPlaces - rentPrice) / payrate / 100);
                                    targetFunction.Add(Math.Round(result, 2));
                                    int op = (int)Math.Round(newPlaceAmount, 0);
                                    OptimalPrice.Add(op);
                                    placeRate.Add(payrate);
                                }
                                else if (coefType == 20 && placesAmount < 3000 && (placesAmount * parkingPlaces - rentPrice) > 30000)
                                {
                                    newPlaceAmount = placesAmount + (payrate * 3);
                                    result = ((newPlaceAmount * parkingPlaces - rentPrice) / payrate / 100);
                                    targetFunction.Add(Math.Round(result, 2));
                                    int op = (int)Math.Round(newPlaceAmount, 0);
                                    OptimalPrice.Add(op);
                                    placeRate.Add(payrate);
                                }
                                else if (coefType == 30 && placesAmount < 2000 && (placesAmount * parkingPlaces - rentPrice) > 20000)
                                {

                                    newPlaceAmount = placesAmount + (payrate * 4);
                                    result = ((newPlaceAmount * parkingPlaces - rentPrice) / payrate / 100);
                                    targetFunction.Add(Math.Round(result, 2));
                                    int op = (int)Math.Round(newPlaceAmount, 0);
                                    OptimalPrice.Add(op);
                                    placeRate.Add(payrate);
                                }
                                else if (coefType == 50 && placesAmount < 1000 && (placesAmount * parkingPlaces - rentPrice) > 10000)
                                {
                                    newPlaceAmount = placesAmount + (payrate * 5);
                                    result = ((newPlaceAmount * parkingPlaces - rentPrice) / payrate / 100);
                                    targetFunction.Add(Math.Round(result, 2));
                                    int op = (int)Math.Round(newPlaceAmount, 0);
                                    OptimalPrice.Add(op);
                                    placeRate.Add(payrate);
                                }
                                else
                                {
                                    result = ((placesAmount * parkingPlaces - rentPrice) / ((numberOfHouses * coefType)));
                                    targetFunction.Add(Math.Round(result, 2));
                                    OptimalPrice.Add(placesAmount);
                                    placeRate.Add(payrate);
                                }
                            }
                        }
                    }
                }
            }
            Response.Redirect("/Calculate/Index");
        }
    }
}