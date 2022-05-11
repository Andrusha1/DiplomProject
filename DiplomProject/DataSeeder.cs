using System;
using DiplomProject.Data;

namespace DiplomProject
{
	public static class DataSeeder
	{
		public static void Seed(this IHost host)
		{
			using var scope = host.Services.CreateScope();
			using var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
			context.Database.EnsureCreated();
			AddStreets(context);
		}

        private static void AddStreets(DatabaseContext context)
        {
			var street = context.streets.FirstOrDefault();
			if (street != null) return;
			context.streets.Add(new Street
			{
				id = 1,
				name = "Партизанская",
				numbersOfHouses = 246,
				typeOfHouses = "Хрущевки",
				areas = new List<Area>
				 {
					 new Area{id = 1 ,parkingPlaces = 20, rentPrice = 20000, placesAmount = 1000 },
					 new Area{id = 2, parkingPlaces = 35, rentPrice = 40000, placesAmount = 1200 }
				 }
			}) ;

			context.SaveChanges();
        }
    }
}

