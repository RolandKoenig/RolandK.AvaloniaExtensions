using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolandK.AvaloniaExtensions.ExceptionHandling.Data.Analyzers;

namespace RolandK.AvaloniaExtensions.ExceptionHandling.Data;

public class ExceptionInfo
{
    /// <summary>
    /// Gets a collection containing all child nodes.
    /// </summary>
    public List<ExceptionInfoNode> ChildNodes { get; set; } = new();

    /// <summary>
    /// Gets or sets the main message.
    /// </summary>
    public string MainMessage
    {
        get;
        set;
    } = string.Empty;

    public string Description
    {
        get;
        set;
    } = string.Empty;

    public ExceptionInfo()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionInfo"/> class.
    /// </summary>
    public ExceptionInfo(Exception ex, IEnumerable<IExceptionAnalyzer>? exceptionAnalyzers = null)
    {
        exceptionAnalyzers ??= CreateDefaultAnalyzers();

        this.MainMessage = "Unexpected Error";
        this.Description = ex.Message;

        // Analyze the given exception 
        ExceptionInfoNode newNode = new(ex);
        this.ChildNodes.Add(newNode);

        AnalyzeException(ex, newNode, exceptionAnalyzers);
    }

    public static IEnumerable<IExceptionAnalyzer> CreateDefaultAnalyzers()
    {
        yield return new DefaultExceptionAnalyzer();
        yield return new SystemIOExceptionAnalyzer();
        yield return new AggregateExceptionAnalyzer();
        yield return new ArgumentExceptionAnalyzer();
    }

    /// <summary>
    /// Analyzes the given exception.
    /// </summary>
    /// <param name="ex">The exception to be analyzed.</param>
    /// <param name="targetNode">The target node where to put all data to.</param>
    /// <param name="exceptionAnalyzers">All loaded analyzer objects.</param>
    private static void AnalyzeException(Exception ex, ExceptionInfoNode targetNode, IEnumerable<IExceptionAnalyzer> exceptionAnalyzers)
    {
        // Query over all exception data
        var analyzedInnerExceptions = new HashSet<Exception>(2);
        foreach(IExceptionAnalyzer actAnalyzer in exceptionAnalyzers)
        {
            // Read all properties of the current exception
            var exceptionInfos = actAnalyzer.ReadExceptionInfo(ex);
            if (exceptionInfos != null)
            {
                foreach (ExceptionProperty actProperty in exceptionInfos)
                {
                    if (string.IsNullOrEmpty(actProperty.Name)) { continue; }

                    ExceptionInfoNode propertyNode = new(actProperty);

                    targetNode.ChildNodes ??= new List<ExceptionInfoNode>();
                    targetNode.ChildNodes.Add(propertyNode);
                }
            }

            // Read all inner exception information
            var innerExceptions = actAnalyzer.GetInnerExceptions(ex);
            if (innerExceptions == null) { continue; }
            
            foreach (Exception actInnerException in innerExceptions)
            {
                if(analyzedInnerExceptions.Contains(actInnerException)){ continue; }
                analyzedInnerExceptions.Add(actInnerException);
                
                ExceptionInfoNode actInfoNode = new(actInnerException);
                AnalyzeException(actInnerException, actInfoNode, exceptionAnalyzers);
                
                targetNode.ChildNodes ??= new List<ExceptionInfoNode>();
                targetNode.ChildNodes.Add(actInfoNode);
            }
        }

        // Sort all generated nodes
        if (targetNode.ChildNodes?.Count > 0)
        {
            targetNode.ChildNodes.Sort();
        }
    }
}