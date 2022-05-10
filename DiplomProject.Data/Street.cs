using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomProject.Data
{
	public class Street
	{
		public long id { get; set; }
		public string name { get; set; }
		public int numberOfHouses { get; set; }
		public string typeOfHouses { get; set; }
		public List<Area> areas { get; set; }
	}
	public class Area
	{
		public long id { get; set; }
		public int placesAmount { get; set; } //Количество парковочных мест
		public int rentPrice { get; set; }
		public int parkingPlaces { get; set; }
	}
}

