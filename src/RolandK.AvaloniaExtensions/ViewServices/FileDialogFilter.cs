using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolandK.AvaloniaExtensions.ViewServices;

public class FileDialogFilter
{
    public string Name { get; set; }

    public List<string> Extensions { get; } = new();

    public FileDialogFilter(string name, params string[] extensions)
    {
        this.Name = name;
        this.Extensions.AddRange(extensions);
    }

    public static string BuildFilterString(IEnumerable<FileDialogFilter> filters)
    {
        var strBuilder = new StringBuilder(128);

        var isFirstFilter = true;
        foreach(var actFilter in filters)
        {
            if (!isFirstFilter)
            {
                strBuilder.Append('|');
            }
            isFirstFilter = false;

            actFilter.BuildFilterString(strBuilder);
        }

        return strBuilder.ToString();
    }

    public string BuildFilterString()
    {
        var strBuilder = new StringBuilder(128);

        this.BuildFilterString(strBuilder);

        return strBuilder.ToString();
    }

    public void BuildFilterString(StringBuilder strBuilder)
    {
        strBuilder.Append(this.Name);
        strBuilder.Append(' ');
        strBuilder.Append('(');
        for (var loop = 0; loop < this.Extensions.Count; loop++)
        {
            if (loop > 0)
            {
                strBuilder.Append(',');
                strBuilder.Append(' ');
            }

            strBuilder.Append('*');
            strBuilder.Append('.');
            strBuilder.Append(this.Extensions[loop]);
        }
        strBuilder.Append(')');

        strBuilder.Append('|');
        for (var loop = 0; loop < this.Extensions.Count; loop++)
        {
            if (loop > 0)
            {
                strBuilder.Append(',');
                strBuilder.Append(' ');
            }

            strBuilder.Append('*');
            strBuilder.Append('.');
            strBuilder.Append(this.Extensions[loop]);
        }
    }
}