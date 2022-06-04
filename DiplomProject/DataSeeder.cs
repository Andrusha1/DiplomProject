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
				id = 0,
				name = "Пример улицы",
				numbersOfHouses = 20,
				typeOfHouses = "Сталинки",
				areas = new List<Area>
				 {
					 new Area
					 {
						 id = 0,
						 parkingPlaces = 10,
						 rentPrice = 10000,
						 placesAmount = 10
					 },
				 }
			}) ;
			context.SaveChanges();
		}
    }
}

