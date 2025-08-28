using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;


/// <summary>
/// This is the Storage Helper class which helps to save some settings to and from the storage. Mainly the following
/// 
///     1) Brush
///     2) Color
///     3) GradientStops
///     
/// </summary>
#nullable disable
internal static class SettingsStorageHelper
{
    private static readonly List<IDisposable> _myList = new List<IDisposable>();

    public static IDisposable AddPropertyListener( this DependencyObject dpo, DependencyProperty prop, Action<DependencyPropertyChangedEventArgs> e )
    {
        ProxyDependencyPropertyClass proxy = new ProxyDependencyPropertyClass(dpo, prop, e);
        _myList.Add( proxy );
        return ( IDisposable ) proxy;
    }

    public static Color ToColor( this object obj )
    {
        switch ( obj )
        {
            case null:
                throw new ArgumentNullException( "value" );

            case long longColor:
                return ( ( int ) longColor ).ToColor();

            case int intColor:
                return intColor.ToColor();

            default:
                SettingsStorage store = (SettingsStorage) obj;
                return Color.FromArgb(
                                        store.GetValue<byte>( "A", 0 ),
                                        store.GetValue<byte>( "R", 0 ),
                                        store.GetValue<byte>( "G", 0 ),
                                        store.GetValue<byte>( "B", 0 )
                                     );
        }
    }

    public static SettingsStorage SaveSettings( this Brush myBrush )
    {
        if ( myBrush == null )
        {
            throw new ArgumentNullException( "brush" );
        }

        var store = new SettingsStorage();

        if ( !( myBrush is SolidColorBrush solid ) )
        {
            if ( !( myBrush is LinearGradientBrush linear ) )
            {
                if ( !( myBrush is RadialGradientBrush radial ) )
                    throw new ArgumentOutOfRangeException( "brush",  myBrush, LocalizedStrings.InvalidValue );
                store.SetValue<string>( "Type", TypeHelper.GetTypeName( typeof( RadialGradientBrush ), true ) );
                store.SetValue<ColorInterpolationMode>( "ColorInterpolationMode", radial.ColorInterpolationMode );
                store.SetValue<double>( "Opacity", radial.Opacity );
                store.SetValue<Point>( "Center", radial.Center );
                store.SetValue<Point>( "GradientOrigin", radial.GradientOrigin );
                store.SetValue<GradientSpreadMethod>( "SpreadMethod", radial.SpreadMethod );
                store.SetValue<SettingsStorage[ ]>( "GradientStops", radial.GradientStops.SaveGradientStop() );
                store.SetValue<double>( "RadiusX", radial.RadiusX );
                store.SetValue<double>( "RadiusY", radial.RadiusY );
                store.SetValue<BrushMappingMode>( "MappingMode", radial.MappingMode );
            }
            else
            {
                store.SetValue<string>( "Type", TypeHelper.GetTypeName( typeof( LinearGradientBrush ), true ) );
                store.SetValue<ColorInterpolationMode>( "ColorInterpolationMode", linear.ColorInterpolationMode );
                store.SetValue<double>( "Opacity", linear.Opacity );
                store.SetValue<Point>( "StartPoint", linear.StartPoint );
                store.SetValue<Point>( "EndPoint", linear.EndPoint );
                store.SetValue<GradientSpreadMethod>( "SpreadMethod", linear.SpreadMethod );
                store.SetValue<SettingsStorage[ ]>( "GradientStops", linear.GradientStops.SaveGradientStop() );
            }
        }
        else
        {
            store.SetValue<string>( "Type", TypeHelper.GetTypeName( typeof( SolidColorBrush ), true ) );
            store.SetValue<int>( "Color", solid.Color.ToInt() );
            store.SetValue<double>( "Opacity", solid.Opacity );
        }

        return store;
    }

    public static SettingsStorage[ ] SaveGradientStop( this GradientStopCollection stops )
    {
        if ( stops == null )
            throw new ArgumentNullException( "items" );

        var store = new List<SettingsStorage>();

        foreach ( GradientStop gradientStop in stops )
        {
            SettingsStorage settingsStorage = new SettingsStorage();
            settingsStorage.SetValue<int>( "Color", gradientStop.Color.ToInt() );
            settingsStorage.SetValue<double>( "Offset", gradientStop.Offset );
            store.Add( settingsStorage );
        }
        return store.ToArray();
    }

    public static Brush GetBrush( this SettingsStorage store )
    {
        Type actualValue = store != null ? Converter.To<Type>( store.GetValue<string>("Type", (string) null)) : throw new ArgumentNullException("storage");

        if (  actualValue == null )
            return ( Brush ) null;

        if ( actualValue == typeof( SolidColorBrush ) )
        {
            var solidColorBrush     = new SolidColorBrush();
            solidColorBrush.Color   = store.GetValue<object>( "Color",  null ).ToColor();
            solidColorBrush.Opacity = store.GetValue<double>( "Opacity", 0.0 );
            return ( Brush ) solidColorBrush;
        }

        if ( actualValue == typeof( LinearGradientBrush ) )
        {
            var linear                    = new LinearGradientBrush();
            linear.ColorInterpolationMode = store.GetValue<ColorInterpolationMode>( "ColorInterpolationMode", linear.ColorInterpolationMode );
            linear.Opacity                = store.GetValue<double>( "Opacity", linear.Opacity );
            linear.StartPoint             = store.GetValue<Point>( "StartPoint", linear.StartPoint );
            linear.EndPoint               = store.GetValue<Point>( "EndPoint", linear.EndPoint );
            linear.SpreadMethod           = store.GetValue<GradientSpreadMethod>( "SpreadMethod", linear.SpreadMethod );
            linear.SaveSettings( store.GetValue<IEnumerable<SettingsStorage>>( "GradientStops", ( IEnumerable<SettingsStorage> ) null ) );
            return ( Brush ) linear;
        }

        if ( !( actualValue == typeof( RadialGradientBrush ) ) )
            throw new ArgumentOutOfRangeException( "storage",  actualValue, LocalizedStrings.InvalidValue );

        var radial                    = new RadialGradientBrush();
        radial.ColorInterpolationMode = store.GetValue<ColorInterpolationMode>( "ColorInterpolationMode", radial.ColorInterpolationMode );
        radial.Opacity                = store.GetValue<double>( "Opacity", radial.Opacity );
        radial.Center                 = store.GetValue<Point>( "Center", radial.Center );
        radial.GradientOrigin         = store.GetValue<Point>( "GradientOrigin", radial.GradientOrigin );
        radial.SpreadMethod           = store.GetValue<GradientSpreadMethod>( "SpreadMethod", radial.SpreadMethod );
        radial.RadiusX                = store.GetValue<double>( "RadiusX", radial.RadiusX );
        radial.RadiusY                = store.GetValue<double>( "RadiusY", radial.RadiusY );
        radial.MappingMode            = store.GetValue<BrushMappingMode>( "MappingMode", radial.MappingMode );
        radial.SaveSettings( store.GetValue<IEnumerable<SettingsStorage>>( "GradientStops", ( IEnumerable<SettingsStorage> ) null ) );
        return ( Brush ) radial;
    }

    public static void SaveSettings( this GradientBrush myBrush, IEnumerable<SettingsStorage> store )
    {
        if ( myBrush == null )
            throw new ArgumentNullException( "brush" );

        if ( store == null )
            throw new ArgumentNullException( "storages" );

        foreach ( SettingsStorage settingsStorage in store )
        {
            var color  = settingsStorage.GetValue<object>("Color",  null).ToColor();
            var offset = settingsStorage.GetValue<double>("Offset", 0.0);
            myBrush.GradientStops.Add( new GradientStop( color, offset ) );
        }
    }

    public static Thickness CreateThickness( this SettingsStorage store )
    {
        return new Thickness( store.GetValue<double>( "Left", 0.0 ), store.GetValue<double>( "Top", 0.0 ), store.GetValue<double>( "Right", 0.0 ), store.GetValue<double>( "Bottom", 0.0 ) );
    }

    public static SettingsStorage SaveSettings( this Thickness thick )
    {
        return new SettingsStorage().Set<double>( "Left", thick.Left ).Set<double>( "Top", thick.Top ).Set<double>( "Right", thick.Right ).Set<double>( "Bottom", thick.Bottom );
    }

    private sealed class ProxyDependencyPropertyClass : DependencyObject, IDisposable
    {

        private static readonly DependencyProperty ProxyProperty = DependencyProperty.Register("Proxy", typeof(object) , typeof (ProxyDependencyPropertyClass), new PropertyMetadata( null, new PropertyChangedCallback(ProxyDependencyPropertyClass.DpoChangedEventArgsCallBack)));

        private readonly Action<DependencyPropertyChangedEventArgs> dpoChangedEventArgs;

        private bool _disposing;

        public ProxyDependencyPropertyClass( DependencyObject dpo, DependencyProperty prop, Action<DependencyPropertyChangedEventArgs> e )
        {
            Ecng.Xaml.XamlHelper.SetBindings(dpo, ProxyDependencyPropertyClass.ProxyProperty, new PropertyPath( prop ), "Proxy", BindingMode.OneWay );
            dpoChangedEventArgs = e;
        }

        void IDisposable.Dispose()
        {
            if ( _disposing )
                return;
            _disposing = true;
            BindingOperations.ClearBinding( ( DependencyObject ) this, ProxyDependencyPropertyClass.ProxyProperty );
        }

        private static void DpoChangedEventArgsCallBack( DependencyObject dpo, DependencyPropertyChangedEventArgs e )
        {
            ProxyDependencyPropertyClass proxyDpo = (ProxyDependencyPropertyClass) dpo;
            
            if ( proxyDpo._disposing )
                return;
            
            Action<DependencyPropertyChangedEventArgs> myEvent = proxyDpo.dpoChangedEventArgs;
            
            if ( myEvent == null )
                return;
            myEvent( e );
        }
    }
}



//using Ecng.Common;
//using Ecng.Serialization;
//using Ecng.Xaml;
//using fx.Collections;
//using System;
//using System.Collections.Generic;
//using System.Windows;
//using System.Windows.Media;

//namespace StockSharp.Xaml.Charting;

//#pragma warning disable CA1416

//internal static class SettingsStorageHelper
//{


//    public static SettingsStorage ToARGB( this Color color )
//    {
//        SettingsStorage settingsStorage = new SettingsStorage();
//        settingsStorage.SetValue( "A", color.A );
//        settingsStorage.SetValue( "R", color.R );
//        settingsStorage.SetValue( "G", color.G );
//        settingsStorage.SetValue( "B", color.B );
//        return settingsStorage;
//    }

//    public static Color FromARGB( this SettingsStorage setting )
//    {
//        if ( setting == null )
//            throw new ArgumentNullException( "setting == null" );
//        return Color.FromArgb( setting.GetValue<byte>( "A", 0 ),
//                                                    setting.GetValue<byte>( "R", 0 ),
//                                                    setting.GetValue<byte>( "G", 0 ),
//                                                    setting.GetValue<byte>( "B", 0 ) );
//    }

//    public static Color ToColor( this object o )
//    {
//        if ( o is int )
//            return ( ( int )o ).ToColor();
//        if ( o is long )
//            return ( ( int )( long )o ).ToColor();

//        return ( ( SettingsStorage )o ).FromARGB();
//    }

//    public static SettingsStorage SaveSettings( this Brush myBrush )
//    {
//        SettingsStorage settingsStorage = new SettingsStorage();
//        switch ( myBrush )
//        {
//            case SolidColorBrush solidColorBrush:
//                settingsStorage.SetValue( "Type", typeof( SolidColorBrush ).GetTypeName( true ) );
//                settingsStorage.SetValue( "Color", solidColorBrush.Color.ToARGB() );
//                settingsStorage.SetValue( "Opacity", solidColorBrush.Opacity );
//                goto case null;
//            case LinearGradientBrush linearGradientBrush:
//                settingsStorage.SetValue( "Type", typeof( LinearGradientBrush ).GetTypeName( true ) );
//                settingsStorage.SetValue( "ColorInterpolationMode", linearGradientBrush.ColorInterpolationMode );
//                settingsStorage.SetValue( "Opacity", linearGradientBrush.Opacity );
//                settingsStorage.SetValue( "StartPoint", linearGradientBrush.StartPoint );
//                settingsStorage.SetValue( "EndPoint", linearGradientBrush.EndPoint );
//                settingsStorage.SetValue( "SpreadMethod", linearGradientBrush.SpreadMethod );
//                settingsStorage.SetValue( "GradientStops", linearGradientBrush.GradientStops.SaveStops() );
//                goto case null;
//            case RadialGradientBrush radialGradientBrush:
//                settingsStorage.SetValue( "Type", typeof( RadialGradientBrush ).GetTypeName( true ) );
//                settingsStorage.SetValue( "ColorInterpolationMode", radialGradientBrush.ColorInterpolationMode );
//                settingsStorage.SetValue( "Opacity", radialGradientBrush.Opacity );
//                settingsStorage.SetValue( "Center", radialGradientBrush.Center );
//                settingsStorage.SetValue( "GradientOrigin", radialGradientBrush.GradientOrigin );
//                settingsStorage.SetValue( "SpreadMethod", radialGradientBrush.SpreadMethod );
//                settingsStorage.SetValue( "GradientStops", radialGradientBrush.GradientStops.SaveStops() );
//                settingsStorage.SetValue( "RadiusX", radialGradientBrush.RadiusX );
//                settingsStorage.SetValue( "RadiusY", radialGradientBrush.RadiusY );
//                settingsStorage.SetValue( "MappingMode", radialGradientBrush.MappingMode );
//                goto case null;
//            case null:
//                return settingsStorage;
//            default:
//                throw new ArgumentOutOfRangeException( "brush", myBrush.GetType().GetTypeName( false ), "Unsupported brush type." );
//        }
//    }

//    public static SettingsStorage SaveSettings( this Thickness _param0 )
//  {
//        return new SettingsStorage().Set<double>( "Left", _param0.Left ).Set<double>( "Top", _param0.Top ).Set<double>( "Right", _param0.Right ).Set<double>( "Bottom", _param0.Bottom );
//    }

//    public static SettingsStorage SaveBrushNew( this Brush myBrush )
//    {
//        SettingsStorage settingsStorage = new SettingsStorage();
//        SolidColorBrush solidColorBrush = myBrush as SolidColorBrush;
//        if ( solidColorBrush == null )
//        {
//            LinearGradientBrush linearGradientBrush = myBrush as LinearGradientBrush;
//            if ( linearGradientBrush == null )
//            {
//                RadialGradientBrush radialGradientBrush = myBrush as RadialGradientBrush;
//                if ( radialGradientBrush == null )
//                {
//                    if ( myBrush != null )
//                        throw new ArgumentOutOfRangeException( "radialGradientBrush == null" );
//                }
//                else
//                {
//                    settingsStorage.SetValue( "Type", typeof( RadialGradientBrush ).GetTypeName( true ) );
//                    settingsStorage.SetValue( "ColorInterpolationMode", radialGradientBrush.ColorInterpolationMode );
//                    settingsStorage.SetValue( "Opacity", radialGradientBrush.Opacity );
//                    settingsStorage.SetValue( "Center", radialGradientBrush.Center );
//                    settingsStorage.SetValue( "GradientOrigin", radialGradientBrush.GradientOrigin );
//                    settingsStorage.SetValue( "SpreadMethod", radialGradientBrush.SpreadMethod );
//                    settingsStorage.SetValue( "GradientStops", radialGradientBrush.GradientStops.SaveGradientStop() );
//                    settingsStorage.SetValue( "RadiusX", radialGradientBrush.RadiusX );
//                    settingsStorage.SetValue( "RadiusY", radialGradientBrush.RadiusY );
//                    settingsStorage.SetValue( "MappingMode", radialGradientBrush.MappingMode );
//                }
//            }
//            else
//            {
//                settingsStorage.SetValue( "Type", typeof( LinearGradientBrush ).GetTypeName( true ) );
//                settingsStorage.SetValue( "ColorInterpolationMode", linearGradientBrush.ColorInterpolationMode );
//                settingsStorage.SetValue( "Opacity", linearGradientBrush.Opacity );
//                settingsStorage.SetValue( "StartPoint", linearGradientBrush.StartPoint );
//                settingsStorage.SetValue( "EndPoint", linearGradientBrush.EndPoint );
//                settingsStorage.SetValue( "SpreadMethod", linearGradientBrush.SpreadMethod );
//                settingsStorage.SetValue( "GradientStops", linearGradientBrush.GradientStops.SaveGradientStop() );
//            }
//        }
//        else
//        {
//            settingsStorage.SetValue( "Type", typeof( SolidColorBrush ).GetTypeName( true ) );
//            settingsStorage.SetValue( "ARGB", solidColorBrush.Color.ToARGB() );
//            settingsStorage.SetValue( "Opacity", solidColorBrush.Opacity );
//        }
//        return settingsStorage;
//    }

//    public static SettingsStorage[ ] SaveGradientStop(

//     this GradientStopCollection stops)
//  {
//        if ( stops == null )
//            throw new ArgumentNullException( "stops == null " );

//        List<SettingsStorage> settingsStorageList = new List<SettingsStorage>();
//        foreach ( GradientStop gradientStop in stops )
//        {
//            SettingsStorage settingsStorage = new SettingsStorage();
//            settingsStorage.SetValue( "ARGB", gradientStop.Color.ToARGB() );
//            settingsStorage.SetValue( "Offset", gradientStop.Offset );
//            settingsStorageList.Add( settingsStorage );
//        }
//        return settingsStorageList.ToArray();
//    }




//    public static SettingsStorage[ ] SaveStops(
//      this GradientStopCollection gradientStopCollection_0 )
//    {
//        PooledList<SettingsStorage> settingsStorageList = new PooledList<SettingsStorage>();
//        foreach ( GradientStop gradientStop in gradientStopCollection_0 )
//        {
//            SettingsStorage settingsStorage = new SettingsStorage();
//            settingsStorage.SetValue( "Color", gradientStop.Color.ToInt() );
//            settingsStorage.SetValue( "Offset", gradientStop.Offset );
//            settingsStorageList.Add( settingsStorage );
//        }
//        return settingsStorageList.ToArray();
//    }

//    public static Brush GetBrush( this SettingsStorage setting )
//    {
//        Type type = setting.GetValue( "Type", ( string )null ).To<Type>();

//        if ( type == typeof( SolidColorBrush ) )
//        {
//            var sBrush = new SolidColorBrush();
//            sBrush.Color = setting.GetValue( "Color", ( SettingsStorage )null ).FromARGB();
//            sBrush.Opacity = setting.GetValue( "Opacity", 0.0 );

//            return sBrush;
//        }

//        if ( type == typeof( LinearGradientBrush ) )
//        {
//            var brush = new LinearGradientBrush();
//            brush.ColorInterpolationMode = setting.GetValue( "ColorInterpolationMode", brush.ColorInterpolationMode );
//            brush.Opacity = setting.GetValue( "Opacity", brush.Opacity );
//            brush.StartPoint = setting.GetValue( "StartPoint", brush.StartPoint );
//            brush.EndPoint = setting.GetValue( "EndPoint", brush.EndPoint );
//            brush.SpreadMethod = setting.GetValue( "SpreadMethod", brush.SpreadMethod );
//            GetStops( brush, setting.GetValue( "GradientStops", ( IEnumerable<SettingsStorage> )null ) );

//            return brush;
//        }

//        if ( !( type == typeof( RadialGradientBrush ) ) )
//            return null;

//        var gBrush = new RadialGradientBrush();
//        gBrush.ColorInterpolationMode = setting.GetValue( "ColorInterpolationMode", gBrush.ColorInterpolationMode );
//        gBrush.Opacity = setting.GetValue( "Opacity", gBrush.Opacity );
//        gBrush.Center = setting.GetValue( "Center", gBrush.Center );
//        gBrush.GradientOrigin = setting.GetValue( "GradientOrigin", gBrush.GradientOrigin );
//        gBrush.SpreadMethod = setting.GetValue( "SpreadMethod", gBrush.SpreadMethod );
//        gBrush.RadiusX = setting.GetValue( "RadiusX", gBrush.RadiusX );
//        gBrush.RadiusY = setting.GetValue( "RadiusY", gBrush.RadiusY );
//        gBrush.MappingMode = setting.GetValue( "MappingMode", gBrush.MappingMode );
//        GetStops( gBrush, setting.GetValue( "GradientStops", ( IEnumerable<SettingsStorage> )null ) );
//        return gBrush;
//    }

//    public static void GetStops( GradientBrush gradientBrush_0, IEnumerable<SettingsStorage> ienumerable_0 )
//    {
//        foreach ( SettingsStorage settingsStorage in ienumerable_0 )
//        {
//            Color color = settingsStorage.GetValue( "Color", ( SettingsStorage )null ).FromARGB();
//            double offset = settingsStorage.GetValue( "Offset", 0.0 );
//            gradientBrush_0.GradientStops.Add( new GradientStop( color, offset ) );
//        }
//    }
//}

