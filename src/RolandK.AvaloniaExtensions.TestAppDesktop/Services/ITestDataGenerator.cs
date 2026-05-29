using System.Collections.Generic;
using RolandK.AvaloniaExtensions.TestAppDesktop.Data;

namespace RolandK.AvaloniaExtensions.TestAppDesktop.Services;

public interface ITestDataGenerator
{
    public IEnumerable<UserData> GenerateUserData(int countRows);
}