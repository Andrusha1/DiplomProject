using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomProject.Data
{
	public class Street
	{
		public long id { get; set; }
		public string name { get; set; }
		public int numbersOfHouses { get; set; }
		public string typeOfHouses { get; set; }
		public List<Area> areas { get; set; }
	} 
}

