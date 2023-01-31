using System.Runtime.CompilerServices;
using Avalonia.Metadata;

[assembly: XmlnsDefinition(
    "https://github.com/RolandK.AvaloniaExtensions", 
    "RolandK.AvaloniaExtensions.Views")]
[assembly: XmlnsDefinition(
    "https://github.com/RolandK.AvaloniaExtensions", 
    "RolandK.AvaloniaExtensions.Mvvm.Markup")]

[assembly: InternalsVisibleTo("RolandK.AvaloniaExtensions.Tests")]    