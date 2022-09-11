using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using fx.Collections;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

#pragma warning disable CA1416

internal static class ExtensionHelper3
{
    

    public static SettingsStorage ToARGB( this Color color )
    {
        SettingsStorage settingsStorage = new SettingsStorage();
        settingsStorage.SetValue( "A", color.A );
        settingsStorage.SetValue( "R", color.R );
        settingsStorage.SetValue( "G", color.G );
        settingsStorage.SetValue( "B", color.B );
        return settingsStorage;
    }

    public static Color FromARGB( this SettingsStorage setting )
    {
        if ( setting == null )
            throw new ArgumentNullException( "setting == null" );
        return Color.FromArgb( setting.GetValue<byte>( "A", 0 ),
                                                    setting.GetValue<byte>( "R", 0 ),
                                                    setting.GetValue<byte>( "G", 0 ),
                                                    setting.GetValue<byte>( "B", 0 ) );
    }

    public static Color ToColor( this object o )
    {
        if ( o is int )
            return ( ( int )o ).ToColor();
        if ( o is long )
            return ( ( int )( long )o ).ToColor();

        return ( ( SettingsStorage )o ).FromARGB();
    }

    public static SettingsStorage SaveBrush( this Brush myBrush )
    {
        SettingsStorage settingsStorage = new SettingsStorage();
        switch ( myBrush )
        {
            case SolidColorBrush solidColorBrush:
                settingsStorage.SetValue( "Type", typeof( SolidColorBrush ).GetTypeName( true ) );
                settingsStorage.SetValue( "Color", solidColorBrush.Color.ToARGB() );
                settingsStorage.SetValue( "Opacity", solidColorBrush.Opacity );
                goto case null;
            case LinearGradientBrush linearGradientBrush:
                settingsStorage.SetValue( "Type", typeof( LinearGradientBrush ).GetTypeName( true ) );
                settingsStorage.SetValue( "ColorInterpolationMode", linearGradientBrush.ColorInterpolationMode );
                settingsStorage.SetValue( "Opacity", linearGradientBrush.Opacity );
                settingsStorage.SetValue( "StartPoint", linearGradientBrush.StartPoint );
                settingsStorage.SetValue( "EndPoint", linearGradientBrush.EndPoint );
                settingsStorage.SetValue( "SpreadMethod", linearGradientBrush.SpreadMethod );
                settingsStorage.SetValue( "GradientStops", linearGradientBrush.GradientStops.SaveStops() );
                goto case null;
            case RadialGradientBrush radialGradientBrush:
                settingsStorage.SetValue( "Type", typeof( RadialGradientBrush ).GetTypeName( true ) );
                settingsStorage.SetValue( "ColorInterpolationMode", radialGradientBrush.ColorInterpolationMode );
                settingsStorage.SetValue( "Opacity", radialGradientBrush.Opacity );
                settingsStorage.SetValue( "Center", radialGradientBrush.Center );
                settingsStorage.SetValue( "GradientOrigin", radialGradientBrush.GradientOrigin );
                settingsStorage.SetValue( "SpreadMethod", radialGradientBrush.SpreadMethod );
                settingsStorage.SetValue( "GradientStops", radialGradientBrush.GradientStops.SaveStops() );
                settingsStorage.SetValue( "RadiusX", radialGradientBrush.RadiusX );
                settingsStorage.SetValue( "RadiusY", radialGradientBrush.RadiusY );
                settingsStorage.SetValue( "MappingMode", radialGradientBrush.MappingMode );
                goto case null;
            case null:
                return settingsStorage;
            default:
                throw new ArgumentOutOfRangeException( "brush", myBrush.GetType().GetTypeName( false ), "Unsupported brush type." );
        }
    }

    public static SettingsStorage SaveBrushNew( this Brush myBrush )
    {
        SettingsStorage settingsStorage = new SettingsStorage();
        SolidColorBrush solidColorBrush = myBrush as SolidColorBrush;
        if ( solidColorBrush == null )
        {
            LinearGradientBrush linearGradientBrush = myBrush as LinearGradientBrush;
            if ( linearGradientBrush == null )
            {
                RadialGradientBrush radialGradientBrush = myBrush as RadialGradientBrush;
                if ( radialGradientBrush == null )
                {
                    if ( myBrush != null )
                        throw new ArgumentOutOfRangeException( "radialGradientBrush == null" );
                }
                else
                {
                    settingsStorage.SetValue( "Type", typeof( RadialGradientBrush ).GetTypeName( true ) );
                    settingsStorage.SetValue( "ColorInterpolationMode", radialGradientBrush.ColorInterpolationMode );
                    settingsStorage.SetValue( "Opacity", radialGradientBrush.Opacity );
                    settingsStorage.SetValue( "Center", radialGradientBrush.Center );
                    settingsStorage.SetValue( "GradientOrigin", radialGradientBrush.GradientOrigin );
                    settingsStorage.SetValue( "SpreadMethod", radialGradientBrush.SpreadMethod );
                    settingsStorage.SetValue( "GradientStops", radialGradientBrush.GradientStops.SaveGradientStop() );
                    settingsStorage.SetValue( "RadiusX", radialGradientBrush.RadiusX );
                    settingsStorage.SetValue( "RadiusY", radialGradientBrush.RadiusY );
                    settingsStorage.SetValue( "MappingMode", radialGradientBrush.MappingMode );
                }
            }
            else
            {
                settingsStorage.SetValue( "Type", typeof( LinearGradientBrush ).GetTypeName( true ) );
                settingsStorage.SetValue( "ColorInterpolationMode", linearGradientBrush.ColorInterpolationMode );
                settingsStorage.SetValue( "Opacity", linearGradientBrush.Opacity );
                settingsStorage.SetValue( "StartPoint", linearGradientBrush.StartPoint );
                settingsStorage.SetValue( "EndPoint", linearGradientBrush.EndPoint );
                settingsStorage.SetValue( "SpreadMethod", linearGradientBrush.SpreadMethod );
                settingsStorage.SetValue( "GradientStops", linearGradientBrush.GradientStops.SaveGradientStop() );
            }
        }
        else
        {
            settingsStorage.SetValue( "Type", typeof( SolidColorBrush ).GetTypeName( true ) );
            settingsStorage.SetValue( "ARGB", solidColorBrush.Color.ToARGB() );
            settingsStorage.SetValue( "Opacity", solidColorBrush.Opacity );
        }
        return settingsStorage;
    }

    public static SettingsStorage[ ] SaveGradientStop(
 
     this GradientStopCollection stops)
  {
        if ( stops == null )
            throw new ArgumentNullException( "stops == null " );

        List<SettingsStorage> settingsStorageList = new List<SettingsStorage>();
        foreach ( GradientStop gradientStop in stops )
        {
            SettingsStorage settingsStorage = new SettingsStorage();
            settingsStorage.SetValue( "ARGB", gradientStop.Color.ToARGB() );
            settingsStorage.SetValue( "Offset", gradientStop.Offset );
            settingsStorageList.Add( settingsStorage );
        }
        return settingsStorageList.ToArray();
    }


    

    public static SettingsStorage[ ] SaveStops(
      this GradientStopCollection gradientStopCollection_0 )
    {
        PooledList<SettingsStorage> settingsStorageList = new PooledList<SettingsStorage>();
        foreach ( GradientStop gradientStop in gradientStopCollection_0 )
        {
            SettingsStorage settingsStorage = new SettingsStorage();
            settingsStorage.SetValue( "Color", gradientStop.Color.ToInt() );
            settingsStorage.SetValue( "Offset", gradientStop.Offset );
            settingsStorageList.Add( settingsStorage );
        }
        return settingsStorageList.ToArray();
    }

    public static Brush GetBrush( this SettingsStorage setting )
    {
        Type type = setting.GetValue( "Type", ( string )null ).To<Type>();

        if ( type == typeof( SolidColorBrush ) )
        {
            var sBrush = new SolidColorBrush();
            sBrush.Color = setting.GetValue( "Color", ( SettingsStorage )null ).FromARGB();
            sBrush.Opacity = setting.GetValue( "Opacity", 0.0 );

            return sBrush;
        }

        if ( type == typeof( LinearGradientBrush ) )
        {
            var brush = new LinearGradientBrush();
            brush.ColorInterpolationMode = setting.GetValue( "ColorInterpolationMode", brush.ColorInterpolationMode );
            brush.Opacity = setting.GetValue( "Opacity", brush.Opacity );
            brush.StartPoint = setting.GetValue( "StartPoint", brush.StartPoint );
            brush.EndPoint = setting.GetValue( "EndPoint", brush.EndPoint );
            brush.SpreadMethod = setting.GetValue( "SpreadMethod", brush.SpreadMethod );
            GetStops( brush, setting.GetValue( "GradientStops", ( IEnumerable<SettingsStorage> )null ) );

            return brush;
        }

        if ( !( type == typeof( RadialGradientBrush ) ) )
            return null;

        var gBrush = new RadialGradientBrush();
        gBrush.ColorInterpolationMode = setting.GetValue( "ColorInterpolationMode", gBrush.ColorInterpolationMode );
        gBrush.Opacity = setting.GetValue( "Opacity", gBrush.Opacity );
        gBrush.Center = setting.GetValue( "Center", gBrush.Center );
        gBrush.GradientOrigin = setting.GetValue( "GradientOrigin", gBrush.GradientOrigin );
        gBrush.SpreadMethod = setting.GetValue( "SpreadMethod", gBrush.SpreadMethod );
        gBrush.RadiusX = setting.GetValue( "RadiusX", gBrush.RadiusX );
        gBrush.RadiusY = setting.GetValue( "RadiusY", gBrush.RadiusY );
        gBrush.MappingMode = setting.GetValue( "MappingMode", gBrush.MappingMode );
        GetStops( gBrush, setting.GetValue( "GradientStops", ( IEnumerable<SettingsStorage> )null ) );
        return gBrush;
    }

    public static void GetStops( GradientBrush gradientBrush_0, IEnumerable<SettingsStorage> ienumerable_0 )
    {
        foreach ( SettingsStorage settingsStorage in ienumerable_0 )
        {
            Color color = settingsStorage.GetValue( "Color", ( SettingsStorage )null ).FromARGB();
            double offset = settingsStorage.GetValue( "Offset", 0.0 );
            gradientBrush_0.GradientStops.Add( new GradientStop( color, offset ) );
        }
    }
}
