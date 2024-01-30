using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Seeds
{
    public class AppSeeder
    {
        private static List<App> apps = new List<App>
        {
            new App { 
                Id = new Guid("bef292cf-f1b7-4370-b0a3-e00ff099daa3"), 
                Name = "Accounts Api", 
                Slug = "acapi",
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow
            },
            new App {
                Id = new Guid("264454c9-691d-4c36-8540-1db1c46e198c"),
                Name = "Accounts Login",
                Slug = "aclog",
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow
            },
            new App {
                Id = new Guid("d582144d-ee4f-4249-be65-38d8836c1036"),
                Name = "Store",
                Slug = "stvit",
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow
            }
        };

        public static void Seed(AccountsContext context)
        {
            var appsDb = context.Apps.ToList();
            
            foreach(var app in apps){
                var appDb = appsDb.FirstOrDefault(s => s.Id == app.Id);
                if(appDb == null)
                {
                    context.Apps.Add(app);
                }
                else
                {
                    appDb.Name = app.Name;
                    appDb.Slug = app.Slug;
                    appDb.Status = app.Status;
                    appDb.UpdatedAt = DateTime.UtcNow;
                    context.Entry(appDb).State = EntityState.Modified;
                }
            }

            foreach(var app in appsDb.Where(s => !apps.Select(s => s.Id).Contains(s.Id)))
                context.Apps.Remove(app);


            context.SaveChanges();
        }
    }
}