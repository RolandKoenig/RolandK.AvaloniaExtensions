using Avalonia.Controls;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Mvvm;

internal static class InternalExtensions
{
    public static IViewServiceHost? TryGetParentViewServiceHost(this IControl control)
    {
        var actParent = control.Parent;
        while (actParent != null)
        {
            if (actParent is IViewServiceHost actParentHost) { return actParentHost; }
            actParent = actParent.Parent;
        }
        return null;
    }
}