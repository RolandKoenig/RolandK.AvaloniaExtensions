using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolandK.AvaloniaExtensions.ExceptionHandling.Data;

public class ExceptionProperty(string name, string value)
{
    public string Name { get; } = name;
    public string Value { get; } = value;

    public override string ToString()
    {
        return $"{this.Name}: {this.Value}";
    }
}