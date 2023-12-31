using System.Runtime.CompilerServices;
using Avalonia.Metadata;

[assembly: XmlnsDefinition(
    "https://github.com/RolandK.AvaloniaExtensions", 
    "RolandK.AvaloniaExtensions.Controls")]
[assembly: XmlnsDefinition(
    "https://github.com/RolandK.AvaloniaExtensions", 
    "RolandK.AvaloniaExtensions.Mvvm.Controls")]

[assembly: InternalsVisibleTo("RolandK.AvaloniaExtensions.Tests")]    