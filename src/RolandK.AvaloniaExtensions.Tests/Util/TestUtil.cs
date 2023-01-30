using Avalonia.LogicalTree;

namespace RolandK.AvaloniaExtensions.Tests.Util;

internal static class TestUtil
{
    public static IEnumerable<TControl> FindLogicalDescendantsOfType<TControl>(ILogical root, bool includeRoot = false)
    {
        if (root is TControl foundControl) { yield return foundControl; }

        foreach (var actLogicalChild in root.LogicalChildren)
        {
            foreach (var actDescendantOfType in FindLogicalDescendantsOfType<TControl>(actLogicalChild, includeRoot:true))
            {
                yield return actDescendantOfType;
            }
        }
    }
}