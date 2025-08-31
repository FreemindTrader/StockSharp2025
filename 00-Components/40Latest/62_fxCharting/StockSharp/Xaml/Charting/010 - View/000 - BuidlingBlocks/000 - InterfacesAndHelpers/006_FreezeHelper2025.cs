using System.Windows;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// This attachable DPO defines an object that has a modifiable state and a read-only (frozen) state.
/// Classes that derive from System.Windows.Freezable provide detailed change notification,
/// can be made immutable, and can clone themselves.
/// </summary>
public sealed class FreezeHelper2025
{
    public static readonly DependencyProperty FreezeProperty = DependencyProperty.RegisterAttached("Freeze", typeof(bool), typeof(FreezeHelper2025), new PropertyMetadata(false, new PropertyChangedCallback(OnFreezePropertyChanged)));

    private static void OnFreezePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
        //
        // Summary:
        //     Gets a value that indicates whether the object can be made unmodifiable.
        //
        // Returns:
        //     true if the current object can be made unmodifiable or is already unmodifiable;
        //     otherwise, false.

        if ( !( d is Freezable freezable ) || !true.Equals( e.NewValue ) || !freezable.CanFreeze )
            return;

        //
        // Summary:
        //     Makes the current object unmodifiable and sets its System.Windows.Freezable.IsFrozen
        //     property to true.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     The System.Windows.Freezable cannot be made unmodifiable.
        freezable.Freeze();
    }

    public static void SetFreeze( DependencyObject dpo, bool isFreezable )
    {
        dpo.SetValue( FreezeProperty, isFreezable );
    }

    public static bool GetFreeze( DependencyObject dpo )
    {
        return ( bool ) dpo.GetValue( FreezeProperty );
    }
}
