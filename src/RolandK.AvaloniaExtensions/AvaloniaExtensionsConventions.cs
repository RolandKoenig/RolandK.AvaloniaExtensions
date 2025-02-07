namespace RolandK.AvaloniaExtensions;

public delegate bool IsViewForViewModelDelegate(Type viewType, Type viewModelType);

public static class AvaloniaExtensionsConventions
{
    /// <summary>
    /// A convention how to detect whether a View belongs to a given ViewModel.
    /// By default, we expect a ViewModel has the same type name with ViewModel at the end.
    /// Examples:
    ///  MyCustomView (View) + MyCustomViewModel (ViewModel),
    ///  MainWindow (View) + MainWindowViewModel (ViewModel)
    /// </summary>
    public static IsViewForViewModelDelegate? IsViewForViewModelFunc { get; set; } =
        (viewType, viewModelType) =>
        {
            var rawViewName = viewType.Name.EndsWith("View", StringComparison.OrdinalIgnoreCase) && (viewType.Name.Length > 4) 
                ? viewType.Name.Substring(0, viewType.Name.Length - 4)
                : viewType.Name;
            var expectedViewModelName = $"{rawViewName}ViewModel";

            return viewModelType.Name.Equals(expectedViewModelName, StringComparison.OrdinalIgnoreCase);
        };
}