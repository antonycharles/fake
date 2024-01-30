using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Accounts.Core.Providers;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Seeds
{
    public class ClientSeeder
    {
    
        public static void Seed(AccountsContext context, IPasswordProvider passwordProvider)
        {
            var listDb = context.Clients.ToList();
            var list = GetClients(passwordProvider);
            
            foreach(var item in list){
                var itemDb = listDb.FirstOrDefault(s => s.Id == item.Id);
                if(itemDb == null)
                {
                    context.Clients.Add(item);
                }
                else
                {
                    itemDb.Name = item.Name;
                    itemDb.Salt = item.Salt;
                    itemDb.SecretHash = item.SecretHash;
                    itemDb.Status = item.Status;
                    itemDb.UpdatedAt = DateTime.UtcNow;
                    context.Entry(itemDb).State = EntityState.Modified;
                }
            }

            foreach(var item in listDb.Where(s => !list.Select(s => s.Id).Contains(s.Id)))
                context.Clients.Remove(item);


            context.SaveChanges();
        }

        private static List<Client> GetClients(IPasswordProvider passwordProvider)
        {
            List<Client> list = new List<Client>();


            var salt = "$2a$11$Dk0KhiUnJdN/IaH3WvdKKe";
            string passwordHash = passwordProvider.HashPassword("f67f982d-c99d-4fc0-9d54-535f0788ca09", salt);

            list.Add(new Client { 
                Id = 1, 
                Name = "Store.Front", 
                SecretHash = passwordHash,
                Salt =  salt,
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow
            });


            salt = "$2a$11$WGShYdaDQCAF41b8iGg4TO";
            passwordHash = passwordProvider.HashPassword("3f068b21-baa4-45ff-8a76-48a23fee665c", salt);

            list.Add(new Client { 
                Id = 2, 
                Name = "Accounts.Login.WebApp", 
                SecretHash = passwordHash,
                Salt =  salt,
                Status = StatusEnum.Active,
                CreatedAt = DateTime.UtcNow
            });

            return list;
        }
        
    }

    
}