using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace RolandK.AvaloniaExtensions.DependencyInjection;

public class CreateUsingDependencyInjectionExtension : MarkupExtension
{
    public Type? Type { get; set; }

    public CreateUsingDependencyInjectionExtension()
    {
        
    }

    public CreateUsingDependencyInjectionExtension(Type type)
    {
        this.Type = type;
    }

    /// <inheritdoc />
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        if (this.Type == null) { return null; }
        if (Design.IsDesignMode) { return null; }

        var rootObjProvider = serviceProvider.GetService<IRootObjectProvider>();

        if (rootObjProvider?.RootObject is not IControl rootObj) { return null; }
        
        var appServiceProvider = 
            rootObj.FindResource(DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY) as IServiceProvider;
        if (appServiceProvider == null)
        {
            var targetPropertyAccessor = serviceProvider.GetService<IProvideValueTarget>();
            if (targetPropertyAccessor == null) { return null; }
            
            var targetProperty = targetPropertyAccessor.TargetProperty as AvaloniaProperty;
            var targetControl = targetPropertyAccessor.TargetObject as IControl;
            if ((targetControl == null) ||
                (targetProperty == null))
            {
                return null;
            }

            EventHandler<LogicalTreeAttachmentEventArgs>? eventHandler = null;
            eventHandler = (_, _) =>
            {
                rootObj.AttachedToLogicalTree -= eventHandler;
                
                appServiceProvider = 
                    rootObj.FindResource(DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY) as IServiceProvider;
                if (appServiceProvider != null)
                {
                    targetControl.SetValue(targetProperty, appServiceProvider.GetService(this.Type));
                }
            };
            rootObj.AttachedToLogicalTree += eventHandler;
        }
        else
        {
            return appServiceProvider?.GetRequiredService(this.Type);   
        }

        return null;
    }
}