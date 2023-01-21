using System.Collections.Generic;
using RolandK.AvaloniaExtensions.TestApp.Data;

namespace RolandK.AvaloniaExtensions.TestApp.Services;

public interface ITestDataGenerator
{
    public IEnumerable<UserData> GenerateUserData(int countRows);
}