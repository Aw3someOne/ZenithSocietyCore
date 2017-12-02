using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenithWebSite.Models;
using ZenithWebSite.Models.ZenithModel;

namespace ZenithWebSite.Data
{
    public class DummyData
    {
        static string Admin { get => "admin"; }
        static string Member { get => "member"; }
        static string Password { get => "P@$$w0rd"; }

        public static async void Initialize(ApplicationDbContext db)
        {
            if (!db.Users.Any())
            {
                RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(db);
                if (!db.Roles.Any(r => r.Name == Admin))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = Admin, NormalizedName = Admin });
                    await db.SaveChangesAsync();
                }
                if (!db.Roles.Any(r => r.Name == Member))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = Member, NormalizedName = Member });
                    await db.SaveChangesAsync();
                }

                UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
                PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
                string[] usernames = { "a", "m" };
                string[] emails = { "a@a.a", "m@m.m" };
                string[] firstnames = { "Pride", "Accomplishment" };
                string[] lastnames = { "AAAA", "MMMM" };

                ApplicationUser adminUser = new ApplicationUser()
                {
                    FirstName = firstnames[0],
                    LastName = lastnames[0],
                    Email = emails[0],
                    NormalizedEmail = emails[0].ToUpper(),
                    UserName = usernames[0],
                    NormalizedUserName = usernames[0].ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                adminUser.PasswordHash = hasher.HashPassword(adminUser, Password);
                await userStore.CreateAsync(adminUser);
                await userStore.AddToRoleAsync(adminUser, Admin);

                ApplicationUser memberUser = new ApplicationUser()
                {
                    FirstName = firstnames[1],
                    LastName = lastnames[1],
                    Email = emails[1],
                    NormalizedEmail = emails[1].ToUpper(),
                    UserName = usernames[1],
                    NormalizedUserName = usernames[1].ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                memberUser.PasswordHash = hasher.HashPassword(memberUser, Password);
                await userStore.CreateAsync(memberUser);
                await userStore.AddToRoleAsync(memberUser, Member);
            }
            if (!db.ActivityCategories.Any())
            {
                db.ActivityCategories.AddRange(GetActivityCategories());
                db.SaveChanges();
            }
            if (!db.Events.Any())
            {
                for (int i = 0; i < 3; i++)
                {
                    db.Events.AddRange(GetEventsWithOffset(db, i * 7));
                }
                db.SaveChanges();
            }
        }

        private static List<ActivityCategory> GetActivityCategories()
        {
            List<ActivityCategory> list = new List<ActivityCategory>()
            {
                new ActivityCategory()
                {
                    ActivityDescription = "Senior's Golf Tournament",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "Leadership General Assembly Meeting",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "Youth Bowling Tournament",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "Young ladies cooking lessons",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "Youth craft lessons",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "Youth choir practice",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "Lunch",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "Pancake Breakfast",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "Swimming Lessons for the youth",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "Swimming Exercise for parents",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "Bingo Tournament",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "BBQ Lunch",
                    CreationDate = DateTime.Now,
                },
                new ActivityCategory()
                {
                    ActivityDescription = "Garage Sale",
                    CreationDate = DateTime.Now,
                },
            };
            return list;
        }

        private static List<Event> GetEventsWithOffset(ApplicationDbContext context, int offset)
        {
            List<Event> events = new List<Event>()
            {
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Senior's Golf Tournament").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 17, 8, 30, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 17, 10, 30, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Leadership General Assembly Meeting").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 18, 8, 30, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 18, 10, 30, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Youth Bowling Tournament").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 20, 17, 30, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 20, 18, 15, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Young ladies cooking lessons").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 20, 19, 0, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 20, 20, 0, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Youth craft lessons").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 21, 8, 30, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 21, 10, 30, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Youth choir practice").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 21, 10, 30, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 21, 12, 0, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Lunch").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 21, 12, 0, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 21, 13, 30, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Pancake Breakfast").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 22, 7, 30, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 22, 8, 30, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Swimming Lessons for the youth").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 22, 8, 30, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 22, 10, 30, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Swimming Exercise for parents").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 22, 8, 30, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 22, 10, 30, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Bingo Tournament").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 22, 10, 30, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 22, 12, 0, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "BBQ Lunch").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 22, 12, 0, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 22, 13, 0, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
                new Event()
                {
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(a => a.ActivityDescription == "Garage Sale").ActivityCategoryId,
                    CreationDate = DateTime.Now,
                    StartTime = new DateTime(2017, 11, 22, 13, 0, 0).AddDays(offset),
                    EndTime = new DateTime(2017, 11, 22, 18, 0, 0).AddDays(offset),
                    CreatorName = "SeedData",
                    IsActive = true,
                },
            };
            return events;
        }
    }
}
