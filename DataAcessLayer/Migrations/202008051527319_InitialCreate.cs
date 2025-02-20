﻿namespace DataAcessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleMake",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Abrv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Abrv = c.String(),
                        VehicleMakeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleMake", t => t.VehicleMakeID, cascadeDelete: true)
                .Index(t => t.VehicleMakeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleModel", "VehicleMakeID", "dbo.VehicleMake");
            DropIndex("dbo.VehicleModel", new[] { "VehicleMakeID" });
            DropTable("dbo.VehicleModel");
            DropTable("dbo.VehicleMake");
        }
    }
}
