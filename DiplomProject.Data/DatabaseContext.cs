using System;
using Microsoft.EntityFrameworkCore;

namespace DiplomProject.Data
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext>options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<Street> streets { get; set; }
		public DbSet<Area> areas { get; set; }
	}
}

