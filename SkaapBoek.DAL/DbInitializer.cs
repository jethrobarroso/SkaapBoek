using Microsoft.AspNetCore.Identity;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkaapBoek.DAL
{
    public static class DbInitializer
    {
        public static void SeedUserAndRoles(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("jethro@crybit.co.za").Result == null)
            {
                var adminuser = new IdentityUser
                {
                    UserName = "sbadmin",
                    Email = "jethro@crybit.co.za"
                };

                var rud = new IdentityUser
                {
                    UserName = "rudadmin",
                    Email = "rudolf@a-i-solutions.co.za"
                };

                var corne = new IdentityUser
                {
                    UserName = "corne@threei.co.za",
                    Email = "corne@threei.co.za"
                };

                var result = userManager.CreateAsync(adminuser, "4nqrFWHKwx9$KtlJE").Result;
                var result2 = userManager.CreateAsync(rud, "Rud&Jet123").Result;
                var result3 = userManager.CreateAsync(corne, "tjd0sIEfW*7!ji").Result;
                if (result.Succeeded)
                    _ = userManager.AddToRoleAsync(adminuser, "admin").Result;
                if (result2.Succeeded)
                    _ = userManager.AddToRoleAsync(rud, "admin").Result;
                if (result3.Succeeded)
                    _ = userManager.AddToRoleAsync(corne, "admin").Result;
            }

            if (userManager.FindByEmailAsync("rudolf@a-i-solutions.co.za").Result == null)
            {
                var rud = new IdentityUser
                {
                    UserName = "rudadmin",
                    Email = "rudolf@a-i-solutions.co.za"
                };

                var result2 = userManager.CreateAsync(rud, "Rud&Jet123").Result;
                if (result2.Succeeded)
                    _ = userManager.AddToRoleAsync(rud, "admin").Result;
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "admin"
                };

                var result = roleManager.CreateAsync(role).Result;
            }
        }

        public static void Initialize(AppDbContext context)
        {
            // Look for any entries
            if (context.GenderSet.Any())
            {
                return; // DB has been seeded
            }

            var colors = new Color[]
            {
                new Color { Name = "Black", HexValue = "000000" },
                new Color { Name = "Red", HexValue = "FF0000" },
                new Color { Name = "Blue", HexValue = "00FF00" },
                new Color { Name = "Green", HexValue = "0000FF" },
                new Color { Name = "Yellow", HexValue = "FFFF00" },
                new Color { Name = "Pink", HexValue = "FF1493" },
                new Color { Name = "White", HexValue = "FFFFFF" }
            };

            context.ColorSet.AddRange(colors);
            context.SaveChanges();

            var sheepCategories = new SheepCategory[]
            {
                new SheepCategory { Id = 1, Name = "Herd" },
                new SheepCategory { Id = 2, Name = "Feedlot" }
            };
            context.SheepCategorySet.AddRange(sheepCategories);
            context.SaveChanges();

            var genders = new Gender[]
            {
                new Gender { Id = 1, Type = "Male"},
                new Gender { Id = 2, Type = "Female"}
            };

            context.GenderSet.AddRange(genders);
            context.SaveChanges();

            var feed = new Feed[]
            {
                new Feed { Name = "Feed 1", Description = "Sample feed 1"
                    , ProductCode = "prod1", Supplier = "FoodCorp", CostPrice = 112.55M},
                new Feed { Name = "Feed 2", Description = "Sample feed 2"
                    , ProductCode = "prod2", Supplier = "FoodCorp", CostPrice = 111M},
                new Feed { Name = "Feed 3", Description = "Sample feed 3"
                    , ProductCode = "prod3", Supplier = "FoodCorp", CostPrice = 172},
                new Feed { Name = "Feed 4", Description = "Sample feed 4"
                    , ProductCode = "prod4", Supplier = "FoodCorp", CostPrice = 122.65M}
            };

            context.FeedSet.AddRange(feed);
            context.SaveChanges();

            var states = new SheepStatus[]
            {
                new SheepStatus { Id = 1, Name = "Healthy" },
                new SheepStatus { Id = 2, Name = "Ill" },
                new SheepStatus { Id = 3, Name = "Quarantined" },
                new SheepStatus { Id = 4, Name = "Pregnant" },
                new SheepStatus { Id = 5, Name = "Inactive" },
            };

            context.SheepStateSet.AddRange(states);
            context.SaveChanges();

            var status = new Status[]
            {
                new Status { Id = 1, Name = "Not Started", Color = "FC7150"},
                new Status { Id = 2, Name = "In Progress", Color = "5185FC"},
                new Status { Id = 3, Name = "Completed", Color = "5185FC"},
            };

            context.StatusSet.AddRange(status);
            context.SaveChanges();

            var priorities = new Priority[]
            {
                new Priority { Id = 1, Name = "Critical", Color = "DC143C"},
                new Priority { Id = 2, Name = "High", Color = "FF5B5A"},
                new Priority { Id = 3, Name = "Medium", Color = "98C14A"},
                new Priority { Id = 4, Name = "Low", Color = "7FD9FF"}
            };
            
            context.PrioritySet.AddRange(priorities);
            context.SaveChanges();

            var sheep = new Sheep[]
            {
                new Sheep 
                { 
                    TagNumber = "tagParent1", CostPrice = 1240.12M,
                    GenderId = genders.Single(i => i.Type == "Male").Id, 
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 1").Id,
                    SalePrice = 2201.99M, Weight = 1105.23f,
                    BirthDate = new DateTime(2011,5,4), 
                    AcquireDate = new DateTime(2015,1,1),
                    ColorId = colors.Single(c => c.Name == "Red").Id,
                    SheepCategoryId = sheepCategories[0].Id
                },
                new Sheep
                {
                    TagNumber = "tagParent2", CostPrice = 1230.61M,
                    GenderId = genders.Single(i => i.Type == "Female").Id,
                    SheepStatusId = states.Single(s => s.Name == "Inactive").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 2").Id,
                    SalePrice = 2113M, Weight = 1105.23f,
                    BirthDate = new DateTime(2011,5,4),
                    AcquireDate = new DateTime(2015,1,1),
                    ColorId = colors.Single(c => c.Name == "Red").Id,
                    SheepCategoryId = sheepCategories[0].Id
                },
                new Sheep
                {
                    TagNumber = "tagParent3",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 3").Id,
                    SalePrice = 2291.99M, Weight = 105.23f,
                    BirthDate = new DateTime(2017,5,4),
                    AcquireDate = new DateTime(2019,1,1),
                    ColorId = colors.Single(c => c.Name == "Yellow").Id,
                    SheepCategoryId = sheepCategories[0].Id
                },
                new Sheep
                {
                    TagNumber = "tagParent4",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 4").Id,
                    SalePrice = 2301.99M, Weight = 105.23f,
                    BirthDate = new DateTime(2019,5,4),
                    AcquireDate = new DateTime(2019,1,1),
                    ColorId = colors.Single(c => c.Name == "Black").Id,
                    SheepCategoryId = sheepCategories[0].Id,
                    
                },
            };

            context.SheepSet.AddRange(sheep);
            context.SaveChanges();

            var children = new Sheep[]
            {
                new Sheep
                {
                    TagNumber = "tagChild1",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 4").Id,
                    SalePrice = 2301.99M, Weight = 55,
                    BirthDate = new DateTime(2019,5,4),
                    AcquireDate = new DateTime(2019,5,4),
                    ColorId = colors.Single(c => c.Name == "Black").Id,
                    SheepCategoryId = sheepCategories[1].Id
                },
                new Sheep
                {
                    TagNumber = "tagChild2",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 4").Id,
                    SalePrice = 2301.99M, Weight = 44,
                    BirthDate = new DateTime(2019,5,4),
                    AcquireDate = new DateTime(2019,5,4),
                    ColorId = colors.Single(c => c.Name == "Black").Id,
                    SheepCategoryId = sheepCategories[1].Id
                },
                new Sheep
                {
                    TagNumber = "tagChild3",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 4").Id,
                    SalePrice = 2301.99M, Weight = 66,
                    BirthDate = new DateTime(2019,5,4),
                    AcquireDate = new DateTime(2019,5,4),
                    ColorId = colors.Single(c => c.Name == "Black").Id,
                    SheepCategoryId = sheepCategories[1].Id,
                },
                new Sheep
                {
                    TagNumber = "tagChild4",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 4").Id,
                    SalePrice = 2301.99M, Weight = 62,
                    BirthDate = new DateTime(2019,5,4),
                    AcquireDate = new DateTime(2019,5,4),
                    ColorId = colors.Single(c => c.Name == "Black").Id,
                    SheepCategoryId = sheepCategories[1].Id
                }
            };

            context.SheepSet.AddRange(children);
            context.SaveChanges();
        }
    }
}
