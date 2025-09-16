using System;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Data;

namespace RolandK.AvaloniaExtensions.TestApp.Views;

public partial class ResponsiveTwoColumnView : UserControl
{
    public ResponsiveTwoColumnView()
    {
        InitializeComponent();
    }
    
    private void OnResponsiveGrid_CurrentBreakpointChanged(object? sender, EventArgs e)
    {
        BindingOperations.GetBindingExpressionBase(
            PseudoClassesText, Run.TextProperty)?.UpdateTarget();
    }
}