using System.Windows;

namespace StockSharp.Xaml.Charting;
#nullable disable
public sealed class FreezeHelper2025
{
    public static readonly DependencyProperty FreezeProperty = DependencyProperty.RegisterAttached("Freeze", typeof(bool), typeof(FreezeHelper2025), new PropertyMetadata((object)false, new PropertyChangedCallback(FreezeHelper2025.OnFreezePropertyChanged)));

    private static void OnFreezePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if ( !( d is Freezable freezable ) || !true.Equals(e.NewValue) || !freezable.CanFreeze )
            return;
        freezable.Freeze();
    }

    public static void SetFreeze(DependencyObject _param0, bool _param1)
    {
        _param0.SetValue(FreezeHelper2025.FreezeProperty, (object)_param1);
    }

    public static bool GetFreeze(DependencyObject _param0)
    {
        return (bool)_param0.GetValue(FreezeHelper2025.FreezeProperty);
    }
}
