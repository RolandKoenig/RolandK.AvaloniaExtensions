using System;
using System.Collections.Generic;
using System.Text;

namespace RolandK.AvaloniaExtensions.Mvvm;

public class CloseWindowRequestEventArgs
{
    public object? DialogResult { get; }

    public CloseWindowRequestEventArgs(object? dialogResult)
    {
        this.DialogResult = dialogResult;
    }
}