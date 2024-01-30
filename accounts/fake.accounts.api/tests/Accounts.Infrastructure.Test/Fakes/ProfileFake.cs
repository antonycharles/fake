using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Bogus;

namespace Accounts.Infrastructure.Test.Fakes
{
    public class ProfileFake
    {
        public static Faker<Profile> Create()
        {
            var profiles = new string[] {"Admin","Developer","Moderator","Seller"};
            return new Faker<Profile>()
                .RuleFor(r => r.Id, f => f.Random.Int(1,300))
                .RuleFor(r => r.Name, f => f.PickRandom(profiles));
        }
            

    }
}