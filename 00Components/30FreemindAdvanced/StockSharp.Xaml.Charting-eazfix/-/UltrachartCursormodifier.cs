using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using StockSharp.Charting;

#nullable disable
namespace StockSharp.Xaml.Charting;

public sealed class UltrachartCursormodifier :
  CursorModifier,
  IComponentConnector
{

    private readonly ControlTemplate _tooltipLabelTemplate;

    public static readonly DependencyProperty InPlaceTooltipProperty = DependencyProperty.Register(nameof (InPlaceTooltip), typeof (bool), typeof (UltrachartCursormodifier), new PropertyMetadata((object) true, new PropertyChangedCallback(UltrachartCursormodifier.OnInPlaceTooltipPropertyChanged)));

    private bool _someBoolean;

    public UltrachartCursormodifier()
    {
        this.InitializeComponent();
        this._tooltipLabelTemplate = this.TooltipLabelTemplate;

    }

    private static void OnInPlaceTooltipPropertyChanged( DependencyObject _param0, DependencyPropertyChangedEventArgs _param1 )
    {
        ( ( UltrachartCursormodifier ) _param0 ).OnInPlaceTooltipChanged( ( bool ) _param1.NewValue );
    }

    public bool InPlaceTooltip
    {
        get
        {
            return ( bool ) this.GetValue( UltrachartCursormodifier.InPlaceTooltipProperty );
        }
        set
        {
            this.SetValue( UltrachartCursormodifier.InPlaceTooltipProperty, ( object ) value );
        }
    }

    private void OnInPlaceTooltipChanged( bool _param1 )
    {
        this.TooltipLabelTemplate = _param1 ? this._tooltipLabelTemplate : ( ControlTemplate ) null;
    }
}
