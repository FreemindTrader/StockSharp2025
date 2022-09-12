using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Docking;
using Ecng.Common;
using Ecng.Interop;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace StockSharp.Xaml
{
    public static class Extensions
    {
        public static void LoadWindowSettings( this Window window, SettingsStorage storage )
        {
            if ( window == null )
                throw new ArgumentNullException( nameof( window ) );
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );

            window.Top         = storage.GetValue<double>( "Top", window.Top );
            window.Left        = storage.GetValue<double>( "Left", window.Left );
            window.Width       = storage.GetValue<double>( "Width", window.Width );
            window.Height      = storage.GetValue<double>( "Height", window.Height );
            window.WindowState = storage.GetValue<WindowState>( "WindowState", window.WindowState );
        }

        public static void SaveWindowSettings( this Window window, SettingsStorage storage )
        {
            if ( window == null )
                throw new ArgumentNullException( nameof( window ) );
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );

            storage.SetValue<double>( "Top", window.Top );
            storage.SetValue<double>( "Left", window.Left );
            storage.SetValue<double>( "Width", window.Width );
            storage.SetValue<double>( "Height", window.Height );
            storage.SetValue<WindowState>( "WindowState", window.WindowState );
        }

        public static string SaveDevExpressControl( this DependencyObject obj )
        {
            if ( obj == null )
                throw new ArgumentNullException( nameof( obj ) );
            using ( MemoryStream memoryStream = new MemoryStream( ) )
            {
                DXSerializer.Serialize( obj, ( Stream )memoryStream, "Designer", ( DXOptionsLayout )null );
                return Encoding.UTF8.GetString( memoryStream.ToArray( ) );
            }
        }

        public static void LoadDevExpressControl( this DependencyObject obj, string settings )
        {
            if ( obj == null )
                throw new ArgumentNullException( nameof( obj ) );
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );
            using ( MemoryStream memoryStream = new MemoryStream( Encoding.UTF8.GetBytes( settings ) ) )
                DXSerializer.Deserialize( obj, ( Stream )memoryStream, "Designer", ( DXOptionsLayout )null );
        }

        public static void TryOpenLink( this string url, DependencyObject owner )
        {
            if ( url.OpenLink( false ) )
                return;
            url.CopyToClipboard<string>( );

            new MessageBoxBuilder( ).Error( ).Text( LocalizedStrings.CannotOpenLink.Put( ( object )url ) ).Owner( owner ).Show( );
        }

        internal static ICollection<Tuple<DateTime, Decimal>> AveragePriceByCount(
          this ICollection<Tuple<DateTime, Decimal>> timeToPriceCollection,
          int count )
        {
            if ( timeToPriceCollection == null )
                throw new ArgumentNullException( "source" );
            if ( count >= timeToPriceCollection.Count )
                return timeToPriceCollection;
            List<Tuple<DateTime, Decimal>> tupleList = new List<Tuple<DateTime, Decimal>>( );
            int avg = timeToPriceCollection.Count / count;
            Decimal tickSum = new Decimal( );
            Decimal priceSum = new Decimal( );

            int counter = 0;
            foreach ( Tuple<DateTime, Decimal> tuple in ( IEnumerable<Tuple<DateTime, Decimal>> )timeToPriceCollection )
            {
                tickSum += ( Decimal )tuple.Item1.Ticks;
                priceSum += tuple.Item2;
                ++counter;

                if ( counter >= avg )
                {
                    tupleList.Add( Tuple.Create<DateTime, Decimal>( new DateTime( ( long )( tickSum / ( Decimal )counter ) ), priceSum / ( Decimal )counter ) );
                    tickSum = new Decimal( );
                    priceSum = new Decimal( );
                    counter = 0;
                }
            }
            if ( counter > 0 )
                tupleList.Add( Tuple.Create<DateTime, Decimal>( new DateTime( ( long )( tickSum / ( Decimal )counter ) ), priceSum / ( Decimal )counter ) );
            return ( ICollection<Tuple<DateTime, Decimal>> )tupleList;
        }

        public static string SaveLayout( this DockLayoutManager manager )
        {
            if ( manager == null )
            {
                throw new ArgumentNullException( nameof( manager ) );
            }
                
            using ( MemoryStream memoryStream = new MemoryStream( ) )
            {
                manager.SaveLayoutToStream( ( Stream )memoryStream );
                return Encoding.UTF8.GetString( memoryStream.ToArray( ) );
            }
        }

        // Tony: Somehow loading the layout is not working.
        public static void LoadLayout( this DockLayoutManager manager, string layout )
        {
            if ( manager == null )
                throw new ArgumentNullException( nameof( manager ) );
            if ( layout.IsEmpty( ) )
                return;
            using ( MemoryStream memoryStream = new MemoryStream( Encoding.UTF8.GetBytes( layout ) ) )
            {
                manager.RestoreLayoutFromStream( ( Stream )memoryStream );
            }
                
        }

        public static string GetStrategyProcessStateIconName( this Strategy strategy )
        {
            if ( strategy == null )
                return "Stop";
            switch ( strategy.ProcessState )
            {
                case ProcessStates.Stopped:
                    return "Stop";
                case ProcessStates.Stopping:
                case ProcessStates.Started:
                    return "Start";
                default:
                    throw new ArgumentOutOfRangeException( );
            }
        }

        public static string GetPriceTextFormat( this Security security )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            if ( !security.PriceStep.HasValue && !security.Decimals.HasValue )
                return "0.00";
            Decimal? priceStep = security.PriceStep;
            ref Decimal? local = ref priceStep;
            string str = new string( '0', local.HasValue ? local.GetValueOrDefault( ).GetCachedDecimals( ) : security.Decimals.Value );
            return "0" + ( str.Length == 0 ? string.Empty : "." + str );
        }

        public static string GetVolumeTextFormat( this Security security )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            if ( !security.VolumeStep.HasValue )
                return "0";
            string str = new string( '0', security.VolumeStep.Value.GetCachedDecimals( ) );
            return "0" + ( str.Length == 0 ? string.Empty : "." + str );
        }
    }
}
