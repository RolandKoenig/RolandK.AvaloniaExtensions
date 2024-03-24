using System;
using System.Reflection;
using System.Collections.Generic;

namespace RolandK.AvaloniaExtensions.ExceptionHandling.Data;

public class ExceptionInfoNode : IComparable<ExceptionInfoNode>
{
    /// <summary>
    /// Gets a collection containing all child nodes.
    /// </summary>
    public List<ExceptionInfoNode>? ChildNodes { get; set; }

    public bool IsExceptionNode { get; set; }

    public string PropertyName { get; set; } = string.Empty;

    public string PropertyValue { get; set; } = string.Empty;

    public ExceptionInfoNode()
    {
        
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionInfoNode"/> class.
    /// </summary>
    public ExceptionInfoNode(Exception ex)
    {
        this.IsExceptionNode = true;
        this.PropertyName = ex.GetType().GetTypeInfo().Name;
        this.PropertyValue = ex.Message;
    }

    public ExceptionInfoNode(ExceptionProperty property)
    {
        this.PropertyName = property.Name;
        this.PropertyValue = property.Value;
    }

    public int CompareTo(ExceptionInfoNode? other)
    {
        if (other == null) { return -1; }
        if(this.IsExceptionNode != other.IsExceptionNode)
        {
            if (this.IsExceptionNode) { return 1; }
            else { return -1; }
        }

        return 0;
    }

    public override string ToString()
    {
        return $"{this.PropertyName}: {this.PropertyValue}";
    }
}