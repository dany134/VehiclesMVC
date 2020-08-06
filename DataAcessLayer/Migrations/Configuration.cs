namespace DataAcessLayer.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DataAcessLayer.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<DataAcessLayer.Context.VehicleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAcessLayer.Context.VehicleContext context)
        {
            var makes = new List<VehicleMake>
           {
               new VehicleMake {Name= "Bavarian Motor Works", Abrv="BMW"},
               new VehicleMake {Name="Mercedes-Benz", Abrv="Mercedes"}
           };
            makes.ForEach(m => context.VehicleMakes.Add(m));
            context.SaveChanges();
            var models = new List<VehicleModel>
            {
                new VehicleModel{VehicleMakeID= 1, Name="3-Series 330i", Abrv="330i"},
                new VehicleModel{VehicleMakeID= 1, Name="5-Series 530d", Abrv="530d"},
                new VehicleModel{VehicleMakeID=2, Name="E-Class 220", Abrv="E220"}
            };
            models.ForEach(m => context.VehicleModels.Add(m));
            context.SaveChanges();
        }
    }
}
