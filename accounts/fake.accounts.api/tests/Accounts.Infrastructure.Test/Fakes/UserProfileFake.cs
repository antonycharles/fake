using System;
using System.Collections.Generic;
using Accounts.Core.Entities;
using Bogus;

namespace Accounts.Infrastructure.Test.Fakes
{
    public class UserProfileFake
    {
        public static UserProfile Create(Profile profile, User user)
        {
            return new UserProfile{
                User = user,
                UserId = user.Id,
                Profile = profile,
                ProfileId = profile.Id
            };
        }
    }
}