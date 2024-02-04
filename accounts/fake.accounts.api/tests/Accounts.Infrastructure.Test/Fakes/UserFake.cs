using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Bogus;

namespace Accounts.Infrastructure.Test.Fakes
{
    public class UserFake
    {
        public static Faker<User> Create()
            => new Faker<User>()
                .RuleFor(r => r.Id, f => Guid.NewGuid())
                .RuleFor(r => r.Name, f => f.Person.FirstName)
                .RuleFor(r => r.Email, f => f.Person.Email)
                .RuleFor(r => r.PasswordHash, f => Guid.NewGuid().ToString())
                .RuleFor(r => r.Salt, f => Guid.NewGuid().ToString());
    }
}