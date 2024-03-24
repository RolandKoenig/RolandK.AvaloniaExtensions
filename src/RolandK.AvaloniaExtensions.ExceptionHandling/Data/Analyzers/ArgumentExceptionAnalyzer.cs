using System;
using System.Collections.Generic;

namespace RolandK.AvaloniaExtensions.ExceptionHandling.Data.Analyzers;

public class ArgumentExceptionAnalyzer : IExceptionAnalyzer
{
    /// <inheritdoc />
    public IEnumerable<ExceptionProperty>? ReadExceptionInfo(Exception ex)
    {
        if (ex is not ArgumentException argumentException) { yield break; }
        
        yield return new ExceptionProperty("ParamName", argumentException.ParamName ?? string.Empty);
    }

    /// <inheritdoc />
    public IEnumerable<Exception>? GetInnerExceptions(Exception ex)
    {
        return null;
    }
}