using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirstApp
{
    public class DataBase : DbContext
    {
        public DbSet<Car> Car { get; set; }
        public DbSet<Airplane> Airplane { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Table Per Type

            //modelBuilder.Entity<Vehicle>().ToTable("Vehicles");
            //modelBuilder.Entity<Car>().ToTable("Car");
            //modelBuilder.Entity<Airplane>().ToTable("Airplane");

            //Table Per Concrete Class
            modelBuilder.Entity<Car>().Map(car =>
                {
                    car.MapInheritedProperties();
                    car.ToTable("Car");
                });

            modelBuilder.Entity<Airplane>().Map(airplane =>
            {
                airplane.MapInheritedProperties();
                airplane.ToTable("Airplane");
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
