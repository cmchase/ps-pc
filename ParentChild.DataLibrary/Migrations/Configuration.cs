namespace ParentChild.DataLibrary.Migrations
{
    using ParentChild.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ParentChild.DataLibrary.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ParentChild.DataLibrary.SalesContext context)
        {
            context.SalesOrders.AddOrUpdate(
                so => so.CustomerName,
                new SalesOrder { CustomerName = "Cory Chase", PoNumber = "1234", SalesOrderItems =
                                                                               {
                                                                                   new SalesOrderItem
                                                                                       {
                                                                                           ProductCode = "Abc123",
                                                                                           Quantity = 10,
                                                                                           UnitPrice = 1.23m
                                                                                       },
                                                                                    new SalesOrderItem
                                                                                        {
                                                                                           ProductCode = "Xyz456",
                                                                                           Quantity = 2,
                                                                                           UnitPrice = 3.50m  
                                                                                        },
                                                                                    new SalesOrderItem
                                                                                        {
                                                                                           ProductCode = "123Abc",
                                                                                           Quantity = 15,
                                                                                           UnitPrice = 2.50m  
                                                                                        },
                                                                                    new SalesOrderItem
                                                                                        {
                                                                                           ProductCode = "456Xyz",
                                                                                           Quantity = 25,
                                                                                           UnitPrice = 1.50m  
                                                                                        }
                                                                               }
                },
                new SalesOrder { CustomerName = "Samantha Chase", PoNumber = "3456", SalesOrderItems =
                                                                               {
                                                                                   new SalesOrderItem
                                                                                       {
                                                                                           ProductCode = "Abc123",
                                                                                           Quantity = 10,
                                                                                           UnitPrice = 1.23m
                                                                                       },
                                                                                    new SalesOrderItem
                                                                                        {
                                                                                           ProductCode = "Xyz456",
                                                                                           Quantity = 2,
                                                                                           UnitPrice = 3.50m  
                                                                                        },
                                                                                    new SalesOrderItem
                                                                                        {
                                                                                           ProductCode = "123Abc",
                                                                                           Quantity = 15,
                                                                                           UnitPrice = 2.50m  
                                                                                        },
                                                                                    new SalesOrderItem
                                                                                        {
                                                                                           ProductCode = "456Xyz",
                                                                                           Quantity = 25,
                                                                                           UnitPrice = 1.50m  
                                                                                        }
                                                                               }
                });
        }
    }
}
