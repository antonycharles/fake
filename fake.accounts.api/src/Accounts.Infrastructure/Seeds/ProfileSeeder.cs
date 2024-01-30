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
    public class ProfileSeeder
    {
        public static void Seed(AccountsContext context)
        {
            var listDb = context.Profiles.ToList();
            
            foreach(var item in list){
                var itemDb = listDb.FirstOrDefault(s => s.Id == item.Id);
                if(itemDb == null)
                {
                    context.Profiles.Add(item);
                }
                else
                {
                    itemDb.Name = item.Name;
                    itemDb.Description = item.Description;
                    itemDb.IsDefault = item.IsDefault;
                    itemDb.IsSystem = item.IsSystem;
                    itemDb.AppId = item.AppId;
                    itemDb.Status = item.Status;
                    itemDb.UpdatedAt = DateTime.UtcNow;
                    context.Entry(itemDb).State = EntityState.Modified;
                }
            }

            foreach(var item in listDb.Where(s => !list.Select(s => s.Id).Contains(s.Id)))
                context.Profiles.Remove(item);


            context.SaveChanges();
        }
        
        private static List<Profile> list = new List<Profile>
        {
            //Accounts
            new Profile { 
                Id = 1, 
                Name = "Accounts",
                Description = "Accounts",
                IsDefault = false,
                IsSystem = true,
                AppId = new Guid("bef292cf-f1b7-4370-b0a3-e00ff099daa3"),
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow
            },

            //Store
            new Profile { 
                Id = 2, 
                Name = "User",
                Description = "User",
                IsDefault = true,
                IsSystem = false,
                AppId = new Guid("d582144d-ee4f-4249-be65-38d8836c1036"),
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow
            },
        };  
    }
}