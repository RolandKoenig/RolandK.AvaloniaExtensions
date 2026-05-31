namespace RolandK.AvaloniaExtensions.DependencyInjection;

public class DuplicateCallToUseDependencyInjectionException()
    : AvaloniaExtensionsException("Duplicate call to UseDependencyInjection detected");