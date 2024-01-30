using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Seeds
{
    public class ClientProfileSeeder
    {
        public static void Seed(AccountsContext context)
        {
            var listDb = context.ClientsProfiles.ToList();
            
            foreach(var item in list){
                var itemDb = listDb.FirstOrDefault(s => s.ProfileId == item.ProfileId && s.ClientId == item.ClientId);
                if(itemDb == null)
                {
                    context.ClientsProfiles.Add(item);
                }
                else
                {
                    itemDb.ProfileId = item.ProfileId;
                    itemDb.ClientId = item.ClientId;
                    context.Entry(itemDb).State = EntityState.Modified;
                }
            }

            foreach(var item in listDb.Where(s => !list.Select(s => s.ProfileId).Contains(s.ProfileId) && !list.Select(s => s.ClientId).Contains(s.ClientId)))
                context.ClientsProfiles.Remove(item);


            context.SaveChanges();
        }
        
        private static List<ClientProfile> list = new List<ClientProfile>
        {
            new ClientProfile { 
                ProfileId = 1, 
                ClientId = 2
            }
        }; 
    }
}