using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Seeds
{
    public class ProfileRoleSeeder
    {
        public static void Seed(AccountsContext context)
        {
            var listDb = context.ProfilesRoles.ToList();
            
            foreach(var item in list){
                var itemDb = listDb.FirstOrDefault(s => s.ProfileId == item.ProfileId && s.RoleId == item.RoleId);
                if(itemDb == null)
                {
                    context.ProfilesRoles.Add(item);
                }
                else
                {
                    itemDb.ProfileId = item.ProfileId;
                    itemDb.RoleId = item.RoleId;
                    context.Entry(itemDb).State = EntityState.Modified;
                }
            }

            foreach(var item in listDb.Where(s => !list.Select(s => s.ProfileId).Contains(s.ProfileId) && !list.Select(s => s.RoleId).Contains(s.RoleId)))
                context.ProfilesRoles.Remove(item);


            context.SaveChanges();
        }
        
        private static List<ProfileRole> list = new List<ProfileRole>
        {
            new ProfileRole { 
                ProfileId = 1, 
                RoleId = 1
            },
            new ProfileRole {
                ProfileId = 1,
                RoleId = 2
            },

            new ProfileRole { 
                ProfileId = 2, 
                RoleId = 3
            },
        };  
    }
}