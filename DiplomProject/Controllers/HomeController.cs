using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DiplomProject.Models;
using DiplomProject.Data;
using Microsoft.EntityFrameworkCore;
using DiplomProject.ViewModels;

namespace DiplomProject.Controllers;

public class HomeController : Controller
{
    private readonly DatabaseContext context;

    public HomeController(DatabaseContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var streets = this.context.streets.Include(s => s.areas).Select(s => new StreetsViewModel
        {
            name = s.name,
            numberOfHouses = s.numberOfHouses,
            typesOfHouses = s.typeOfHouses,
            areas = string.Join(',', s.areas.Select(a => a.parkingPlaces))
        });
        return View(streets);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

