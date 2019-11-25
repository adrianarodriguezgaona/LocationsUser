using B4.PE3.RodriguezA.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject1
{
    class TestData
    {
         public static LocationUser[] TestLocationsUser => new[] {
            new LocationUser
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Name = "Restaurants",
                Color ="red",
                Items = new List<LocationItem>()
            },
            new LocationUser
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Name = "Cocktails",
                Color ="green",
                Items = new List<LocationItem>()
            },
            new LocationUser
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Name = "Another Site",
                Color ="aqua",
                Items = new List<LocationItem>()
            },            
         };

        public static LocationItem[] TestLocationItems => new[]
        {
            new LocationItem
            {
                 Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                 ItemName ="Stoepa",
                 LocationUserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                 VisitDate = DateTime.Now

           },
             new LocationItem
            {
                 Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                 ItemName ="Grill",
                 LocationUserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                 VisitDate = DateTime.Now

           },


        };
    }
}
