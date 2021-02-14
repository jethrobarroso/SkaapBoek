using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkaapBoek.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Look for any sheep
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

            var genders = new Gender[]
            {
                new Gender { Type = "Male"},
                new Gender { Type = "Female"}
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
                new SheepStatus { Name = "Healthy" },
                new SheepStatus { Name = "Ill" },
                new SheepStatus { Name = "Quarantined" },
                new SheepStatus { Name = "Pregnant" },
                new SheepStatus { Name = "Inactive" },
            };

            context.SheepStateSet.AddRange(states);
            context.SaveChanges();

            var status = new Status[]
            {
                new Status { Name = "Not Started", Color = "FC7150"},
                new Status { Name = "In Progress", Color = "5185FC"},
                new Status { Name = "In Review", Color = "5185FC"},
                new Status { Name = "Delayed", Color = "D7CF00"},
                new Status { Name = "Completed", Color = "5185FC"},
                new Status { Name = "Canceled", Color = "5185FC"}
            };

            context.StatusSet.AddRange(status);
            context.SaveChanges();

            var priorities = new Priority[]
            {
                new Priority { Name = "Critical", Color = "DC143C"},
                new Priority { Name = "High", Color = "FF5B5A"},
                new Priority { Name = "Medium", Color = "98C14A"},
                new Priority { Name = "Low", Color = "7FD9FF"}
            };
            
            context.PrioritySet.AddRange(priorities);
            context.SaveChanges();

            var sheep = new HerdMember[]
            {
                new HerdMember 
                { 
                    TagNumber = "tagParent1", CostPrice = 1240.12M,
                    GenderId = genders.Single(i => i.Type == "Male").Id, 
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 1").Id,
                    SalePrice = 2201.99M, Weight = 1105.23f,
                    BirthDate = new DateTime(2011,5,4), 
                    AcquireDate = new DateTime(2015,1,1),
                    ColorId = colors.Single(c => c.Name == "Red").Id
                },
                new HerdMember
                {
                    TagNumber = "tagParent2", CostPrice = 1230.61M,
                    GenderId = genders.Single(i => i.Type == "Female").Id,
                    SheepStatusId = states.Single(s => s.Name == "Inactive").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 2").Id,
                    SalePrice = 2113M, Weight = 1105.23f,
                    BirthDate = new DateTime(2011,5,4),
                    AcquireDate = new DateTime(2015,1,1),
                    ColorId = colors.Single(c => c.Name == "Red").Id
                },
                new HerdMember
                {
                    TagNumber = "tagParent3",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 3").Id,
                    SalePrice = 2291.99M, Weight = 105.23f,
                    BirthDate = new DateTime(2017,5,4),
                    AcquireDate = new DateTime(2019,1,1),
                    ColorId = colors.Single(c => c.Name == "Yellow").Id
                },
                new HerdMember
                {
                    TagNumber = "tagParent4",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 4").Id,
                    SalePrice = 2301.99M, Weight = 105.23f,
                    BirthDate = new DateTime(2019,5,4),
                    AcquireDate = new DateTime(2019,1,1),
                    ColorId = colors.Single(c => c.Name == "Black").Id
                },
            };

            context.HerdMemberSet.AddRange(sheep);
            context.SaveChanges();

            var children = new Child[]
            {
                new Child
                {
                    TagNumber = "tagChild1",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 4").Id,
                    SalePrice = 2301.99M, Weight = 55,
                    BirthDate = new DateTime(2019,5,4),
                    ColorId = colors.Single(c => c.Name == "Black").Id
                },
                new Child
                {
                    TagNumber = "tagChild2",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 4").Id,
                    SalePrice = 2301.99M, Weight = 44,
                    BirthDate = new DateTime(2019,5,4),
                    ColorId = colors.Single(c => c.Name == "Black").Id
                },
                new Child
                {
                    TagNumber = "tagChild3",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 4").Id,
                    SalePrice = 2301.99M, Weight = 66,
                    BirthDate = new DateTime(2019,5,4),
                    ColorId = colors.Single(c => c.Name == "Black").Id
                },
                new Child
                {
                    TagNumber = "tagChild4",
                    GenderId = genders.Single(i => i.Type == "Male").Id,
                    SheepStatusId = states.Single(s => s.Name == "Healthy").Id,
                    FeedId = feed.Single(f => f.Name == "Feed 4").Id,
                    SalePrice = 2301.99M, Weight = 62,
                    BirthDate = new DateTime(2019,5,4),
                    ColorId = colors.Single(c => c.Name == "Black").Id
                }
            };

            context.ChildSet.AddRange(children);
            context.SaveChanges();

            var relationships = new Relationship[]
            {
                new Relationship 
                { 
                    SheepId = sheep[1].Id,
                    ChildId = children[0].Id
                },
                new Relationship
                {
                    SheepId = sheep[1].Id,
                    ChildId = children[1].Id
                },
                new Relationship
                {
                    SheepId = sheep[1].Id,
                    ChildId = children[2].Id
                },
                new Relationship
                {
                    SheepId = sheep[1].Id,
                    ChildId = children[3].Id
                },
            };

            context.RelationshipSet.AddRange(relationships);
            context.SaveChanges();
        }
    }
}
