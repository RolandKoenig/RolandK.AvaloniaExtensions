using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;

namespace RolandK.AvaloniaExtensions.Tests.Util;

internal static class TestUtil
{
    public static IEnumerable<TControl> FindLogicalDescendantsOfType<TControl>(ILogical root, bool includeRoot = false)
    {
        if ((includeRoot) &&
            (root is TControl foundControl))
        {
            yield return foundControl;
        }

        foreach (var actLogicalChild in root.LogicalChildren)
        {
            foreach (var actDescendantOfType in FindLogicalDescendantsOfType<TControl>(actLogicalChild, includeRoot:true))
            {
                yield return actDescendantOfType;
            }
        }
    }

    public static TControl? TryFindLogicalDescendantWithName<TControl>(ILogical root, string searchName, bool includeRoot = false)
        where TControl : class, IControl
    {
        if ((includeRoot) &&
            (root is TControl foundControl))
        {
            var name = foundControl.GetValue(StyledElement.NameProperty);
            if(string.Equals(name, searchName, StringComparison.InvariantCulture))
            {
                return foundControl;
            }
        }

        foreach (var actLogicalChild in root.LogicalChildren)
        {
            var innerResult = TryFindLogicalDescendantWithName<TControl>(actLogicalChild, searchName, true);
            if (innerResult != null)
            {
                return innerResult;
            }
        }

        return null;
    }
}