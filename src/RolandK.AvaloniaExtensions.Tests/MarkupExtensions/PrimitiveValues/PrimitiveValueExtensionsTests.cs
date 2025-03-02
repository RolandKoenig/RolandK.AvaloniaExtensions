using Avalonia.Headless.XUnit;
using RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;
using RolandK.AvaloniaExtensions.Tests.Util;

namespace RolandK.AvaloniaExtensions.Tests.MarkupExtensions.PrimitiveValues;

public class PrimitiveValueExtensionsTests
{
    [AvaloniaFact]
    public void ValueExtensions_Xaml_assignment_tests()
    {
        // Arrange
        var testControl = new PrimitiveValueUiTestControl();
        var testWindow = TestRootWindow.CreateAndShow(testControl);
        
        // Assert
        var viewModel = testControl.DataContext as PrimitiveValueUiTestControlViewModel;
        Assert.NotNull(viewModel);
        
        Assert.Equal(true, viewModel.BoolValueTrue);
        Assert.Equal(false, viewModel.BoolValueFalse);
        Assert.Equal((byte)128, viewModel.ByteValue);
        Assert.Equal('A', viewModel.CharValue);
        Assert.Equal((decimal)128.12, viewModel.DecimalValue);
        Assert.Equal(128.12, viewModel.DoubleValue);
        Assert.Equal((float)128.12, viewModel.FloatValue);
        Assert.Equal((long)128, viewModel.LongValue);
        Assert.Equal(128, viewModel.IntValue);
        Assert.Equal((nint)128, viewModel.NIntValue);
        Assert.Equal((sbyte)64, viewModel.SByteValue);
        Assert.Equal((short)128, viewModel.ShortValue);
        Assert.Equal((uint)128, viewModel.UIntValue);
        Assert.Equal((ulong)128, viewModel.ULongValue);
        Assert.Equal((ushort)128, viewModel.UShortValue);
        
        GC.KeepAlive(testWindow);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void BoolValueExtension_returns_expected_value(bool value)
    {
        // Property setter
        var boolValueExtension1 = new BoolValueExtension();
        boolValueExtension1.Value = value;
        Assert.Equal(value, boolValueExtension1.Value);

        // Constructor
        var boolValueExtension2 = new BoolValueExtension(value);
        Assert.Equal(value, boolValueExtension2.Value);
    }

    [Fact]
    public void CharValueExtension_returns_expected_value()
    {
        // Property setter
        var charValueExtension1 = new CharValueExtension();
        charValueExtension1.Value = 'A';
        Assert.Equal('A', charValueExtension1.Value);

        // Constructor
        var charValueExtension2 = new CharValueExtension('A');
        Assert.Equal('A', charValueExtension2.Value);
    }

    [Fact]
    public void DoubleValueExtension_returns_expected_value()
    {
        // Property setter
        var doubleValueExtension1 = new DoubleValueExtension();
        doubleValueExtension1.Value = 123.45;
        Assert.Equal(123.45, doubleValueExtension1.Value);

        // Constructor
        var doubleValueExtension2 = new DoubleValueExtension(123.45);
        Assert.Equal(123.45, doubleValueExtension2.Value);
    }

    [Fact]
    public void FloatValueExtension_returns_expected_value()
    {
        // Property setter
        var floatValueExtension1 = new FloatValueExtension();
        floatValueExtension1.Value = 123.45f;
        Assert.Equal(123.45f, floatValueExtension1.Value);

        // Constructor
        var floatValueExtension2 = new FloatValueExtension(123.45f);
        Assert.Equal(123.45f, floatValueExtension2.Value);
    }

    [Fact]
    public void LongValueExtension_returns_expected_value()
    {
        // Property setter
        var longValueExtension1 = new LongValueExtension();
        longValueExtension1.Value = 123L;
        Assert.Equal(123L, longValueExtension1.Value);

        // Constructor
        var longValueExtension2 = new LongValueExtension(123L);
        Assert.Equal(123L, longValueExtension2.Value);
    }

    [Fact]
    public void NIntValueExtension_returns_expected_value()
    {
        // Property setter
        var nIntValueExtension1 = new NIntValueExtension();
        nIntValueExtension1.Value = 123;
        Assert.Equal(123, nIntValueExtension1.Value);

        // Constructor
        var nIntValueExtension2 = new NIntValueExtension(123);
        Assert.Equal(123, nIntValueExtension2.Value);
    }

    [Fact]
    public void SByteValueExtension_returns_expected_value()
    {
        // Property setter
        var sByteValueExtension1 = new SByteValueExtension();
        sByteValueExtension1.Value = 123;
        Assert.Equal((sbyte)123, sByteValueExtension1.Value);

        // Constructor
        var sByteValueExtension2 = new SByteValueExtension(123);
        Assert.Equal((sbyte)123, sByteValueExtension2.Value);
    }

    [Fact]
    public void ShortValueExtension_returns_expected_value()
    {
        // Property setter
        var shortValueExtension1 = new ShortValueExtension();
        shortValueExtension1.Value = 123;
        Assert.Equal((short)123, shortValueExtension1.Value);

        // Constructor
        var shortValueExtension2 = new ShortValueExtension(123);
        Assert.Equal((short)123, shortValueExtension2.Value);
    }

    [Fact]
    public void UIntValueExtension_returns_expected_value()
    {
        // Property setter
        var uIntValueExtension1 = new UIntValueExtension();
        uIntValueExtension1.Value = 123u;
        Assert.Equal(123u, uIntValueExtension1.Value);

        // Constructor
        var uIntValueExtension2 = new UIntValueExtension(123u);
        Assert.Equal(123u, uIntValueExtension2.Value);
    }

    [Fact]
    public void ULongValueExtension_returns_expected_value()
    {
        // Property setter
        var uLongValueExtension1 = new ULongValueExtension();
        uLongValueExtension1.Value = 123UL;
        Assert.Equal(123UL, uLongValueExtension1.Value);

        // Constructor
        var uLongValueExtension2 = new ULongValueExtension(123UL);
        Assert.Equal(123UL, uLongValueExtension2.Value);
    }

    [Fact]
    public void UShortValueExtension_returns_expected_value()
    {
        // Property setter
        var uShortValueExtension1 = new UShortValueExtension();
        uShortValueExtension1.Value = 123;
        Assert.Equal((ushort)123, uShortValueExtension1.Value);

        // Constructor
        var uShortValueExtension2 = new UShortValueExtension(123);
        Assert.Equal((ushort)123, uShortValueExtension2.Value);
    }
}