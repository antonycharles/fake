using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Bogus;

namespace Accounts.Infrastructure.Test.Fakes
{
    public class UserProfileFake
    {
        public static Faker<UserProfile> Create()
        {
            var userFake = UserFake.Create();
            var profileFake = ProfileFake.Create();
            return Faker<UserProfile>()
                .RuleFor(r => r.UserId, f  => f)
        }
    }
}