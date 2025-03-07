using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace RolandK.AvaloniaExtensions.DependencyInjection.Markup;

public abstract class MarkupExtensionWithDependencyInjection : MarkupExtension
{
    /// <inheritdoc />
    public sealed override object ProvideValue(IServiceProvider xamlServiceProvider)
    {
        if (Design.IsDesignMode)
        {
            return ProvideDefaultValue(xamlServiceProvider);
        }

        var rootObjProvider = xamlServiceProvider.GetService<IRootObjectProvider>();
        if (rootObjProvider?.RootObject is not Control rootObj)
        {
            return ProvideDefaultValue(xamlServiceProvider);
        }
        
        var appServiceProvider = 
            rootObj.FindResource(DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY) as IServiceProvider;
        if (appServiceProvider == null)
        {
            var targetPropertyAccessor = xamlServiceProvider.GetService<IProvideValueTarget>();
            if (targetPropertyAccessor == null) { return null!; }

            var targetProperty = targetPropertyAccessor.TargetProperty as AvaloniaProperty;
            var targetControl = targetPropertyAccessor.TargetObject as Control;
            if ((targetControl == null) ||
                (targetProperty == null))
            {
                return ProvideDefaultValue(xamlServiceProvider);
            }

            EventHandler<LogicalTreeAttachmentEventArgs>? eventHandler = null;
            eventHandler = (_, _) =>
            {
                rootObj.AttachedToLogicalTree -= eventHandler;

                appServiceProvider =
                    rootObj.FindResource(DependencyInjectionConstants
                        .SERVICE_PROVIDER_RESOURCE_KEY) as IServiceProvider;
                if (appServiceProvider != null)
                {
                    targetControl.SetValue(
                        targetProperty, 
                        ProvideValue(xamlServiceProvider, appServiceProvider));
                }
            };
            rootObj.AttachedToLogicalTree += eventHandler;
        }
        else
        {
            return this.ProvideValue(appServiceProvider, appServiceProvider);
        }

        return ProvideDefaultValue(xamlServiceProvider);
    }
    
    /// <summary>
    /// The first call during loading process of the XAML file
    /// </summary>
    /// <param name="xamlServiceProvider">The <see cref="IServiceProvider"/> from base <see cref="MarkupExtension"/></param>
    protected abstract object ProvideDefaultValue(IServiceProvider xamlServiceProvider);

    /// <summary>
    /// Called the first time when the target element is attached to the logical tree.
    /// </summary>
    /// <param name="xamlServiceProvider">The <see cref="IServiceProvider"/> from base <see cref="MarkupExtension"/></param>
    /// <param name="appServiceProvider">The <see cref="IServiceProvider"/> from the application's dependency injection mechanism.</param>
    protected abstract object ProvideValue(
        IServiceProvider xamlServiceProvider, 
        IServiceProvider appServiceProvider);
}