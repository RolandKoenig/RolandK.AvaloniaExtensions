using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using RolandK.AvaloniaExtensions.Mvvm;

namespace RolandK.AvaloniaExtensions.Views;

public class MainWindowFrame : MvvmUserControl
{
    public static readonly StyledProperty<MainWindowFrameStatus> StatusProperty =
        AvaloniaProperty.Register<MainWindowFrame, MainWindowFrameStatus>(
            nameof(Status), MainWindowFrameStatus.Hidden,
            notifying: OnStatusChanged);

    private Window? _mainWindow;
    private Grid _ctrlFullWindowGrid;
    private Grid _ctrlMainGrid;
    private StackPanel _ctrlCustomTitleArea;
    private Panel _ctrlHeaderMenuArea;
    private Panel _ctrlMainContentArea;
    private Panel _ctrlFooterArea;
    private Panel _ctrlStatusBar;

    public Controls CustomTitleArea => _ctrlCustomTitleArea.Children;

    public Controls HeaderMenuArea => _ctrlHeaderMenuArea.Children;

    public Controls MainContentArea => _ctrlMainContentArea.Children;

    public Controls FooterArea => _ctrlFooterArea.Children;

    public DialogHostControl Overlay { get; }

    public MainWindowFrameStatus Status
    {
        get => this.GetValue(StatusProperty);
        set => this.SetValue(StatusProperty, value);
    }

    public bool IsAttachedToWindow => _mainWindow != null;

    public MainWindowFrame()
    {
        AvaloniaXamlLoader.Load(this);

        _ctrlFullWindowGrid = this.Find<Grid>("CtrlFullWindowGrid");
        _ctrlMainGrid = this.Find<Grid>("CtrlMainGrid");
        _ctrlCustomTitleArea = this.Find<StackPanel>("CtrlCustomTitleArea");
        _ctrlHeaderMenuArea = this.Find<Panel>("CtrlHeaderMenuArea");
        _ctrlMainContentArea = this.Find<Panel>("CtrlMainContentArea");
        _ctrlFooterArea = this.Find<Panel>("CtrlFooterArea");
        _ctrlStatusBar = this.Find<Panel>("CtrlStatusBar");
        this.Overlay = this.Find<DialogHostControl>("CtrlOverlay");
    }

    public MainWindowFrame(IControl initialChild)
        : this()
    {
        _ctrlMainContentArea.Children.Add(initialChild);
    }

    private static void OnStatusChanged(IAvaloniaObject sender, bool beforeChanging)
    {
        if (beforeChanging) { return; }
        if (!(sender is MainWindowFrame windowFrame)) { return; }

        windowFrame.UpdateFrameState();
    }

    private void UpdateFrameState()
    {
        if (_mainWindow == null) { return; }

        // Configure window frame
        var useFullWindowMargin = false;
        var useTitlePanel = false;
        var useCenteredTitle = false;
        if (_mainWindow.IsExtendedIntoWindowDecorations)
        {
            useTitlePanel = true;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                switch (_mainWindow.WindowState)
                {
                    case WindowState.FullScreen:
                    case WindowState.Maximized:
                        useFullWindowMargin = true;
                        break;
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                useTitlePanel = _mainWindow.WindowState != WindowState.FullScreen;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                useCenteredTitle = true;
            }
        }

        // Apply settings for content margin
        var titleBarMargin = 0.0;
        if (useFullWindowMargin)
        {
            _ctrlMainGrid.Margin = new Thickness(7.0, 0.0, 7.0, 7.0);
            _ctrlFullWindowGrid.Margin = new Thickness(0.0, 7.0, 0.0, 0.0);
            titleBarMargin = 7.0;
        }
        else
        {
            _ctrlMainGrid.Margin = new Thickness(0.0);
            _ctrlFullWindowGrid.Margin = new Thickness(0.0);
        }

        // Apply settings for title panel
        if (useTitlePanel)
        {
            _ctrlCustomTitleArea.IsVisible = true;
            _ctrlFullWindowGrid.RowDefinitions[0].Height = new GridLength(30.0);

            if (useCenteredTitle)
            {
                _ctrlCustomTitleArea.Margin = new Thickness(0.0);
                _ctrlCustomTitleArea.HorizontalAlignment = HorizontalAlignment.Center;
                _ctrlCustomTitleArea.VerticalAlignment = VerticalAlignment.Center;
            }
            else
            {
                _ctrlCustomTitleArea.Margin = new Thickness(7.0 + titleBarMargin, 0.0);
                _ctrlCustomTitleArea.HorizontalAlignment = HorizontalAlignment.Left;
                _ctrlCustomTitleArea.VerticalAlignment = VerticalAlignment.Center;
            }
        }
        else
        {
            _ctrlCustomTitleArea.IsVisible = false;
            _ctrlFullWindowGrid.RowDefinitions[0].Height = new GridLength(0.0);
        }

        switch (this.Status)
        {
            case MainWindowFrameStatus.Hidden:
                _ctrlStatusBar.IsVisible = false;
                break;

            case MainWindowFrameStatus.NeutralBlue:
                _ctrlStatusBar.IsVisible = true;
                _ctrlStatusBar.Background = Brush.Parse("#0975C6");
                break;

            case MainWindowFrameStatus.NeutralGray:
                _ctrlStatusBar.IsVisible = true;
                _ctrlStatusBar.Background = Brushes.Gray;
                break;

            case MainWindowFrameStatus.Green:
                _ctrlStatusBar.IsVisible = true;
                _ctrlStatusBar.Background = Brushes.Green;
                break;

            case MainWindowFrameStatus.Yellow:
                _ctrlStatusBar.IsVisible = true;
                _ctrlStatusBar.Background = Brushes.Yellow;
                break;

            case MainWindowFrameStatus.Red:
                _ctrlStatusBar.IsVisible = true;
                _ctrlStatusBar.Background = Brushes.Red;
                break;

            default:
                throw new InvalidOperationException(
                    $"Enum member {this.Status} from enum {nameof(MainWindowFrameStatus)} is not supported!");
        }
    }

    /// <inheritdoc />
    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnAttachedToLogicalTree(e);

        var newMainWindow = e.Parent as Window;
        if (!ReferenceEquals(newMainWindow, _mainWindow))
        {
            if (_mainWindow != null)
            {
                _mainWindow.PropertyChanged -= this.OnMainWindow_PropertyChanged;
            }
            _mainWindow = newMainWindow;
            if (_mainWindow != null)
            {
                _mainWindow.PropertyChanged += this.OnMainWindow_PropertyChanged;
            }
        }

        // Trigger update of this control's state
        Dispatcher.UIThread.Post(this.UpdateFrameState);
    }

    private void OnMainWindow_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        this.UpdateFrameState();
    }
}