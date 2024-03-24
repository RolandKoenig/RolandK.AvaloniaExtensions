using System;
using System.Collections.Generic;
using System.Text;

namespace RolandK.AvaloniaExtensions.ExceptionHandling.Data.Analyzers;

public class DefaultExceptionAnalyzer : IExceptionAnalyzer
{
    /// <inheritdoc />
    public IEnumerable<ExceptionProperty>? ReadExceptionInfo(Exception ex)
    {
        yield return new ExceptionProperty("Type", ex.GetType().FullName ?? string.Empty);
        yield return new ExceptionProperty("Message", ex.Message);
        yield return new ExceptionProperty("HResult", ex.HResult.ToString());
        yield return new ExceptionProperty("HelpLink", ex.HelpLink ?? string.Empty);
        yield return new ExceptionProperty("Source", ex.Source ?? string.Empty);

        if (ex.TargetSite != null)
        {
            var sourceMethod = ex.TargetSite;
            yield return new ExceptionProperty("SourceMethod.Name", sourceMethod.Name);
            yield return new ExceptionProperty("SourceMethod.IsStatic", sourceMethod.IsStatic.ToString());
            yield return new ExceptionProperty(
                "SourceMethod.Type", 
                sourceMethod.DeclaringType?.FullName ?? string.Empty);
        }

        yield return new ExceptionProperty("StackTrace", ex.StackTrace ?? string.Empty);
    }

    /// <inheritdoc />
    public IEnumerable<Exception>? GetInnerExceptions(Exception ex)
    {
        if(ex.InnerException != null)
        {
            yield return ex.InnerException;
        }
    }
}