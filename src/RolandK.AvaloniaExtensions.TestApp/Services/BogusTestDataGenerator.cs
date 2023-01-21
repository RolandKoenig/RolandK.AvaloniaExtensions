using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using RolandK.AvaloniaExtensions.TestApp.Data;

namespace RolandK.AvaloniaExtensions.TestApp.Services;

public class BogusTestDataGenerator : ITestDataGenerator
{
    /// <inheritdoc />
    public IEnumerable<UserData> GenerateUserData(int countRows)
    {
        var testUserGenerator = new Faker<UserData>()
            .RuleFor(u => u.Gender, f => f.PickRandom<Name.Gender>().ToString())
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(Enum.Parse<Name.Gender>(u.Gender)))
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(Enum.Parse<Name.Gender>(u.Gender)))
            .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .RuleFor(u => u.EMail, (f, u) => f.Internet.Email(u.FirstName, u.LastName));

        return Enumerable.Range(0, countRows).Select(_ => testUserGenerator.Generate());
    }
}