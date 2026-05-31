using System.Collections.Generic;
using RolandK.AvaloniaExtensions.TestAppMobile.Models;

namespace RolandK.AvaloniaExtensions.TestAppMobile.Services;

public interface ITestDataGenerator
{
    public IEnumerable<UserData> GenerateUserData(int countRows);
}