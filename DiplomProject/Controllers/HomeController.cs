using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DiplomProject.Models;
using DiplomProject.Data;
using Microsoft.EntityFrameworkCore;
using DiplomProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            id = s.id,
            name = s.name,
            numbersOfHouses = s.numbersOfHouses,
            typesOfHouses = s.typeOfHouses,
            areas = string.Join(',', s.areas.Select(a => a.parkingPlaces))
        });
        return View(streets);
    }
    
    public IActionResult Privacy()
    {
        var x = DropList(); //DropList from db
        ViewBag.Street = new SelectList(x); //DropList from db
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public void RemoveStreet(string SelectedStreet)
    {
        long id = context.streets.Where(a => a.name == SelectedStreet).Select(s => s.id).First();
        Street street = context.streets.Find(id);
        if(street != null) 
        {
            context.streets.Remove(street);
            context.SaveChanges();
        }
        Response.Redirect("/Home/Index");
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
}

