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

#nullable disable
internal static class SettingsStorageHelper
{
    private static readonly List<IDisposable> _myList = new List<IDisposable>();

    public static IDisposable AddPropertyListener( this DependencyObject dpo, DependencyProperty prop, Action<DependencyPropertyChangedEventArgs> e )
    {
        SettingsStorageHelper.ProxyDependencyPropertyClass proxy = new SettingsStorageHelper.ProxyDependencyPropertyClass(dpo, prop, e);
        SettingsStorageHelper._myList.Add( proxy );
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

        if ( !( myBrush is SolidColorBrush solidColorBrush ) )
        {
            if ( !( myBrush is LinearGradientBrush linearGradientBrush ) )
            {
                if ( !( myBrush is RadialGradientBrush radialGradientBrush ) )
                    throw new ArgumentOutOfRangeException( "brush", ( object ) myBrush, LocalizedStrings.InvalidValue );
                store.SetValue<string>( "Type", TypeHelper.GetTypeName( typeof( RadialGradientBrush ), true ) );
                store.SetValue<ColorInterpolationMode>( "ColorInterpolationMode", radialGradientBrush.ColorInterpolationMode );
                store.SetValue<double>( "Opacity", radialGradientBrush.Opacity );
                store.SetValue<Point>( "Center", radialGradientBrush.Center );
                store.SetValue<Point>( "GradientOrigin", radialGradientBrush.GradientOrigin );
                store.SetValue<GradientSpreadMethod>( "SpreadMethod", radialGradientBrush.SpreadMethod );
                store.SetValue<SettingsStorage[ ]>( "GradientStops", radialGradientBrush.GradientStops.SaveGradientStop() );
                store.SetValue<double>( "RadiusX", radialGradientBrush.RadiusX );
                store.SetValue<double>( "RadiusY", radialGradientBrush.RadiusY );
                store.SetValue<BrushMappingMode>( "MappingMode", radialGradientBrush.MappingMode );
            }
            else
            {
                store.SetValue<string>( "Type", TypeHelper.GetTypeName( typeof( LinearGradientBrush ), true ) );
                store.SetValue<ColorInterpolationMode>( "ColorInterpolationMode", linearGradientBrush.ColorInterpolationMode );
                store.SetValue<double>( "Opacity", linearGradientBrush.Opacity );
                store.SetValue<Point>( "StartPoint", linearGradientBrush.StartPoint );
                store.SetValue<Point>( "EndPoint", linearGradientBrush.EndPoint );
                store.SetValue<GradientSpreadMethod>( "SpreadMethod", linearGradientBrush.SpreadMethod );
                store.SetValue<SettingsStorage[ ]>( "GradientStops", linearGradientBrush.GradientStops.SaveGradientStop() );
            }
        }
        else
        {
            store.SetValue<string>( "Type", TypeHelper.GetTypeName( typeof( SolidColorBrush ), true ) );
            store.SetValue<int>( "Color", solidColorBrush.Color.ToInt() );
            store.SetValue<double>( "Opacity", solidColorBrush.Opacity );
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
        if ( ( object ) actualValue == null )
            return ( Brush ) null;
        if ( actualValue == typeof( SolidColorBrush ) )
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = store.GetValue<object>( "Color", ( object ) null ).ToColor();
            solidColorBrush.Opacity = store.GetValue<double>( "Opacity", 0.0 );
            return ( Brush ) solidColorBrush;
        }

        if ( actualValue == typeof( LinearGradientBrush ) )
        {
            LinearGradientBrush linear = new LinearGradientBrush();
            linear.ColorInterpolationMode = store.GetValue<ColorInterpolationMode>( "ColorInterpolationMode", linear.ColorInterpolationMode );
            linear.Opacity = store.GetValue<double>( "Opacity", linear.Opacity );
            linear.StartPoint = store.GetValue<Point>( "StartPoint", linear.StartPoint );
            linear.EndPoint = store.GetValue<Point>( "EndPoint", linear.EndPoint );
            linear.SpreadMethod = store.GetValue<GradientSpreadMethod>( "SpreadMethod", linear.SpreadMethod );
            linear.SaveSettings( store.GetValue<IEnumerable<SettingsStorage>>( "GradientStops", ( IEnumerable<SettingsStorage> ) null ) );
            return ( Brush ) linear;
        }

        if ( !( actualValue == typeof( RadialGradientBrush ) ) )
            throw new ArgumentOutOfRangeException( "storage", ( object ) actualValue, LocalizedStrings.InvalidValue );

        RadialGradientBrush radial = new RadialGradientBrush();
        radial.ColorInterpolationMode = store.GetValue<ColorInterpolationMode>( "ColorInterpolationMode", radial.ColorInterpolationMode );
        radial.Opacity = store.GetValue<double>( "Opacity", radial.Opacity );
        radial.Center = store.GetValue<Point>( "Center", radial.Center );
        radial.GradientOrigin = store.GetValue<Point>( "GradientOrigin", radial.GradientOrigin );
        radial.SpreadMethod = store.GetValue<GradientSpreadMethod>( "SpreadMethod", radial.SpreadMethod );
        radial.RadiusX = store.GetValue<double>( "RadiusX", radial.RadiusX );
        radial.RadiusY = store.GetValue<double>( "RadiusY", radial.RadiusY );
        radial.MappingMode = store.GetValue<BrushMappingMode>( "MappingMode", radial.MappingMode );
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
            Color color = settingsStorage.GetValue<object>("Color",  null).ToColor();
            double offset = settingsStorage.GetValue<double>("Offset", 0.0);
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

        private static readonly DependencyProperty ProxyProperty = DependencyProperty.Register("Proxy", typeof(object) , typeof (SettingsStorageHelper.ProxyDependencyPropertyClass), new PropertyMetadata( null, new PropertyChangedCallback(SettingsStorageHelper.ProxyDependencyPropertyClass.DpoChangedEventArgsCallBack)));

        private readonly Action<DependencyPropertyChangedEventArgs> dpoChangedEventArgs;

        private bool _disposing;

        public ProxyDependencyPropertyClass( DependencyObject dpo, DependencyProperty prop, Action<DependencyPropertyChangedEventArgs> e )
        {
            this.SetBindings( SettingsStorageHelper.ProxyDependencyPropertyClass.ProxyProperty, dpo, new PropertyPath( prop ), BindingMode.OneWay );
            this.dpoChangedEventArgs = e;
        }

        void IDisposable.Dispose()
        {
            if ( this._disposing )
                return;
            this._disposing = true;
            BindingOperations.ClearBinding( ( DependencyObject ) this, SettingsStorageHelper.ProxyDependencyPropertyClass.ProxyProperty );
        }

        private static void DpoChangedEventArgsCallBack( DependencyObject dpo, DependencyPropertyChangedEventArgs e )
        {
            SettingsStorageHelper.ProxyDependencyPropertyClass zxYtXibpnAml0 = (SettingsStorageHelper.ProxyDependencyPropertyClass) dpo;
            if ( zxYtXibpnAml0._disposing )
                return;
            Action<DependencyPropertyChangedEventArgs> z7CxThCs = zxYtXibpnAml0.dpoChangedEventArgs;
            if ( z7CxThCs == null )
                return;
            z7CxThCs( e );
        }
    }
}
