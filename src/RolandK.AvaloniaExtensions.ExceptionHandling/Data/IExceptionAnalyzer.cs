using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolandK.AvaloniaExtensions.ExceptionHandling.Data;

/// <summary>
/// This interface is used by the error-reporting framework.
/// It queries for all information provided by an exception which will be presented to
/// the user / developer.
/// </summary>
public interface IExceptionAnalyzer
{
    /// <summary>
    /// Reads all exception information from the given exception object.
    /// </summary>
    /// <param name="ex">The exception to be analyzed.</param>
    IEnumerable<ExceptionProperty>? ReadExceptionInfo(Exception ex);

    /// <summary>
    /// Gets all inner exceptions provided by the given exception object.
    /// </summary>
    /// <param name="ex">The exception to be analyzed.</param>
    IEnumerable<Exception>? GetInnerExceptions(Exception ex);
}