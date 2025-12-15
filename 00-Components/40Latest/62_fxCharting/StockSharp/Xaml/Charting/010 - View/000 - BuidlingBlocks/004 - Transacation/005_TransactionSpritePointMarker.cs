using Ecng.Xaml;
using SciChart.Charting.Visuals.PointMarkers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

//
// Summary:
//     Allows any WPF UIElement to be rendered as a Sprite (bitmap) at each data-point
//     location using the following XAML syntax
//
// Remarks:
//     SciChart.Charting.Visuals.PointMarkers.BasePointMarker derived types use fast
//     bitmap rendering to draw data-points to the screen. This means that traditional
//     WPF style tooltips won't work. For that we need to use the HitTest API. Please
//     see the HitTest sections of the user manual for more information
internal sealed class TransactionSpritePointMarker : SpritePointMarker
{

    private static readonly Lazy<ControlTemplate> _lazyTemplate = new Lazy<ControlTemplate>(new Func<ControlTemplate>(LazyControlTemplate._myInstance.GetControlTemplate));

    private readonly Point _myPoint;

    private readonly Lazy<ImageSource> _lazyImage;

    public TransactionSpritePointMarker( string iconKey, Point pt, Color stroke, Color fill, double width )
    {
        var myImage         = XamlHelper.GetImage( iconKey, new SolidColorBrush( fill ), new SolidColorBrush( stroke ) );       
        _lazyImage          = new Lazy<ImageSource>( new Func<ImageSource>( () => myImage ) );
        _myPoint            = pt;
        Width               = width;
        Height              = width;
        PointMarkerTemplate = _lazyTemplate.Value;
    }


    public static ComponentResourceKey ComponentResourceKey
    {
        get
        {
            return new ComponentResourceKey( typeof( TransactionSpritePointMarker ), ( object ) "SvgMarkerTemplate" );
        }
    }

    public static ComponentResourceKey GetComponentResourceKey( )
    {
        return new ComponentResourceKey( typeof( TransactionSpritePointMarker ), ( object ) "SvgMarkerTemplate" );
    }

    public ImageSource MarkerImageSrc => _lazyImage.Value;

    
    protected Point Center( )
    {
        return new Point( ( ( IPointMarker ) this ).Width * _myPoint.X, ( ( IPointMarker ) this ).Height * _myPoint.Y );
    }

    [Serializable]
    private new sealed class LazyControlTemplate
    {
        public static readonly LazyControlTemplate _myInstance = new LazyControlTemplate();

        internal ControlTemplate GetControlTemplate( )
        {
            return ( ControlTemplate ) Application.Current.FindResource( ( object ) GetComponentResourceKey( ) );
        }
    }    
}
