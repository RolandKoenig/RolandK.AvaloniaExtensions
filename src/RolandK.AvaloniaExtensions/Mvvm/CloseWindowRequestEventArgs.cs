using System;
using System.Collections.Generic;
using System.Text;

namespace RolandK.AvaloniaExtensions.Mvvm;

public class CloseWindowRequestEventArgs(object? dialogResult) : EventArgs
{
    public object? DialogResult { get; } = dialogResult;
}