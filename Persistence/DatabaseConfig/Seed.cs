using Domain.Entities;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.DatabaseConfig
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context,
           UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Id = "a",
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new AppUser
                    {
                        Id = "b",
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new AppUser
                    {
                        Id = "c",
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (!context.ActivityCategories.Any())
            {
                var activityCategories = new List<ActivityCategory>
                {
                    new ActivityCategory
                    {
               //         Id=1,
                        Title="Culture"
                    },
                      new ActivityCategory
                    {
                 //       Id=2,
                        Title="History"
                    },
                        new ActivityCategory
                    {
                   //     Id=3,
                        Title="Art"
                    },
                          new ActivityCategory
                    {
                     //   Id=4,
                        Title="Dance"
                    },
                            new ActivityCategory
                    {
                       // Id=5,
                        Title="Drink"
                    }

                };

                await context.ActivityCategories.AddRangeAsync(activityCategories);
                await context.SaveChangesAsync();


            }
            if (!context.Activities.Any())
            {
                var activities = new List<Activity>
                {
                    new Activity
                    {
                        Title = "Past Activity 1",
                        HoldingDate = DateTime.Now.AddMonths(-2),
                        Description = "Activity 2 months ago",
                        CategoryId = 1,
                        City = "London",
                        Venue = "Pub",
                        AppUserId="a",
                        Attendees = new List<Attendee>
                        {
                            new Attendee
                            {
                                AppUserId = "a",
                                DateJoined = DateTime.Now.AddMonths(-2)
                            }
                        }
                    },
                    new Activity
                    {
                        Title = "Past Activity 2",
                        HoldingDate = DateTime.Now.AddMonths(-1),
                        Description = "Activity 1 month ago",
                        CategoryId = 1,
                        City = "Paris",
                        Venue = "The Louvre",
                        AppUserId="a",
                                                Attendees = new List<Attendee>
                        {
                            new Attendee
                            {
                                AppUserId = "b",
                                DateJoined = DateTime.Now.AddMonths(-1)
                            },
                            new Attendee
                            {
                                AppUserId = "a",
                                DateJoined = DateTime.Now.AddMonths(-3)
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Future Activity 1",
                        HoldingDate = DateTime.Now.AddMonths(1),
                        Description = "Activity 1 month in future",
                        CategoryId = 2,
                        City = "London",
                        Venue = "Wembly Stadium",
                        AppUserId="b",
                        Attendees = new List<Attendee>
                        {
                            new Attendee
                            {
                                AppUserId = "b",
                                DateJoined = DateTime.Now.AddMonths(-8)
                            },
                            new Attendee
                            {
                                AppUserId = "a",
                                DateJoined = DateTime.Now.AddMonths(-4)
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Future Activity 2",
                        HoldingDate = DateTime.Now.AddMonths(2),
                        Description = "Activity 2 months in future",
                        CategoryId= 3,
                        City = "London",
                        Venue = "Jamies Italian",
                        AppUserId="c",
                        Attendees = new List<Attendee>
                        {
                            new Attendee
                            {
                                AppUserId = "c",
                                DateJoined = DateTime.Now.AddMonths(-6)
                            },
                            new Attendee
                            {
                                AppUserId = "a",
                                DateJoined = DateTime.Now.AddMonths(-3)
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Future Activity 3",
                        HoldingDate = DateTime.Now.AddMonths(3),
                        Description = "Activity 3 months in future",
                        CategoryId = 4,
                        City = "London",
                        Venue = "Pub",
                        AppUserId="b",
                        Attendees = new List<Attendee>
                        {
                            new Attendee
                            {
                                AppUserId = "b",
                                DateJoined = DateTime.Now.AddMonths(-1)
                            },
                            new Attendee
                            {
                                AppUserId = "c",
                                DateJoined = DateTime.Now.AddMonths(-6)
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Future Activity 4",
                        HoldingDate = DateTime.Now.AddMonths(4),
                        Description = "Activity 4 months in future",
                        CategoryId = 4,
                        City = "London",
                        Venue = "British Museum",
                        AppUserId="a",
                       Attendees = new List<Attendee>
                        {

                            new Attendee
                            {
                                AppUserId = "a",
                                DateJoined = DateTime.Now.AddMonths(-3)
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Future Activity 5",
                        HoldingDate = DateTime.Now.AddMonths(5),
                        Description = "Activity 5 months in future",
                        CategoryId= 3,
                        City = "London",
                        Venue = "Punch and Judy",
                        AppUserId="c",
                        Attendees = new List<Attendee>
                        {
                            new Attendee
                            {
                                AppUserId = "b",
                                DateJoined = DateTime.Now.AddMonths(-1)
                            },
                            new Attendee
                            {
                                AppUserId = "c",
                                DateJoined = DateTime.Now.AddMonths(-3)
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Future Activity 6",
                        HoldingDate = DateTime.Now.AddMonths(6),
                        Description = "Activity 6 months in future",
                        CategoryId = 2,
                        City = "London",
                        Venue = "O2 Arena",
                        AppUserId="a",
                        Attendees = new List<Attendee>
                        {
                            new Attendee
                            {
                                AppUserId = "b",
                                DateJoined = DateTime.Now.AddMonths(-1)
                            },
                            new Attendee
                            {
                                AppUserId = "a",
                                DateJoined = DateTime.Now.AddMonths(-3)
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Future Activity 7",
                        HoldingDate = DateTime.Now.AddMonths(7),
                        Description = "Activity 7 months in future",
                        CategoryId = 4,
                        City = "Berlin",
                        Venue = "All",
                        AppUserId="c",
                       Attendees = new List<Attendee>
                        {
                            new Attendee
                            {
                                AppUserId = "c",
                                DateJoined = DateTime.Now.AddMonths(-1)
                            },
                            new Attendee
                            {
                                AppUserId = "a",
                                DateJoined = DateTime.Now.AddMonths(-3)
                            },
                        }
                    },
                    new Activity
                    {
                        Title = "Future Activity 8",
                        HoldingDate = DateTime.Now.AddMonths(8),
                        Description = "Activity 8 months in future",
                        CategoryId = 1,
                        City = "London",
                        Venue = "Pub",
                        AppUserId="b",
                     Attendees = new List<Attendee>
                        {
                            new Attendee
                            {
                                AppUserId = "b",
                                DateJoined = DateTime.Now.AddMonths(-1)
                            },
                            new Attendee
                            {
                                AppUserId = "c",
                                DateJoined = DateTime.Now.AddMonths(-3)
                            },
                        }
                    }
                };
                await context.Activities.AddRangeAsync(activities);
                await context.SaveChangesAsync();
            }
        }
    }
}
