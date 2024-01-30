using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Seeds
{
    public class RoleSeeder
    {
        public static void Seed(AccountsContext context)
        {
            var listDb = context.Roles.ToList();
            
            foreach(var item in list){
                var itemDb = listDb.FirstOrDefault(s => s.Id == item.Id);
                if(itemDb == null)
                {
                    context.Roles.Add(item);
                }
                else
                {
                    itemDb.Name = item.Name;
                    itemDb.Slug = item.Slug;
                    itemDb.AppId = item.AppId;
                    itemDb.IsSystem = item.IsSystem;
                    itemDb.FatherId = item.FatherId;
                    itemDb.Status = item.Status;
                    itemDb.UpdatedAt = DateTime.UtcNow;
                    context.Entry(itemDb).State = EntityState.Modified;
                }
            }

            foreach(var item in listDb.Where(s => !list.Select(s => s.Id).Contains(s.Id)))
                context.Roles.Remove(item);


            context.SaveChanges();
        }

        private static List<Role> list = new List<Role>
        {
            //Accounts
            new Role { 
                Id = 1, 
                Name = "Token Key",
                Slug = "token.key",
                AppId = new Guid("bef292cf-f1b7-4370-b0a3-e00ff099daa3"),
                IsSystem = true,
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow
            },

            new Role { 
                Id = 2, 
                Name = "User Authentication",
                Slug = "user.authentication",
                AppId = new Guid("bef292cf-f1b7-4370-b0a3-e00ff099daa3"),
                IsSystem = true,
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow
            },

            //Store
            new Role { 
                Id = 3, 
                Name = "User panel",
                Slug = "user.panel",
                AppId = new Guid("d582144d-ee4f-4249-be65-38d8836c1036"),
                IsSystem = false,
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}