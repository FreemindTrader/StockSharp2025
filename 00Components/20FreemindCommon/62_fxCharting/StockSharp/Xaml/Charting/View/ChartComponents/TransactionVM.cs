using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using SciChart.Charting.Visuals.Annotations;
using Ecng.Xaml.Converters;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

internal sealed class TransactionVM<T> : ChartCompentWpfBaseViewModel<T> where T : TransactionUI<T>, new()
{
    private readonly ConcurrentQueue<ChartDrawDataEx.sTrade> _concurrentQueue = new ConcurrentQueue<ChartDrawDataEx.sTrade>( );

    public TransactionVM( T chartElment ) : base( chartElment )
    {
    }

    protected override void Clear( )
    {
    }

    protected override void UpdateUi( )
    {
        PerformUiAction( new Action( Remove ), true );
    }

    public override bool Draw( IEnumerableEx<ChartDrawDataEx.IDrawValue> drawValues )
    {
        return EnqueDrawData( drawValues.Cast<ChartDrawDataEx.sTrade>( ).ToEx( drawValues.Count ) );
    }



    public bool EnqueDrawData( IEnumerableEx<ChartDrawDataEx.sTrade> strades )
    {
        if ( strades == null )
        {
            return false;
        }

        bool flag = false;
        foreach ( ChartDrawDataEx.sTrade strade in strades )
        {
            flag = true;
            _concurrentQueue.Enqueue( strade );
        }
        return flag;
    }

    private void AddTradeCustomAnnotation( ref ChartDrawDataEx.sTrade strade )
    {
        Tuple<long, string> tuple = Tuple.Create( strade.TradeId, strade.TradeStringId );

        if ( ( UltraChartCustomAnnotation )DrawingSurface.GetAxisMakerAnnotation( RootElem, tuple ) != null )
        {
            if ( !CompareHelper.IsDefault( strade.UtcTime ) )
            {
                return;
            }

            DrawingSurface.RemoveAnnotation( RootElem, tuple );
        }
        else
        {
            string str = strade.TradeId == 0L ? strade.TradeStringId : strade.TradeId.To<string>( );
            string msg = string.Format( "{0} N{1}\r\n{2} = {3}\r\n{4} = {5}\r\n{6} = {7}", strade.OrderSide.GetDisplayName( ), str, LocalizedStrings.Price, strade.Price, LocalizedStrings.Volume, strade.Volume, LocalizedStrings.Time, strade.UtcTime );

            var custom = strade.OrderSide == Sides.Buy ? new UltrachartBuymakerAnnotation( msg, ChartComponentView ) : ( UltraChartCustomAnnotation )new UltrachartSellmarkerAnnotation( msg, ChartComponentView );

            custom.X1 = strade.UtcTime;
            custom.Y1 = strade.Price;

            custom.SetBindings( Control.BackgroundProperty,      ChartComponentView, strade.OrderSide == Sides.Buy ? "BuyColor" : "SellColor", BindingMode.TwoWay, new ColorToBrushConverter( ), null );
            custom.SetBindings( Control.ForegroundProperty,      ChartComponentView, strade.OrderSide == Sides.Buy ? "BuyStrokeColor" : "SellStrokeColor", BindingMode.TwoWay, new ColorToBrushConverter( ), null );
            custom.SetBindings( AnnotationBase.XAxisIdProperty,  ChartComponentView, "XAxisId", BindingMode.TwoWay, null, null );
            custom.SetBindings( AnnotationBase.YAxisIdProperty,  ChartComponentView, "YAxisId", BindingMode.TwoWay, null, null );
            custom.SetBindings( AnnotationBase.IsHiddenProperty, ChartComponentView, "IsVisible", BindingMode.TwoWay, new InverseBooleanConverter( ), null );

            DrawingSurface.AddAxisMakerAnnotation( RootElem, custom, tuple );
        }
    }

    public override void PerformPeriodicalAction( )
    {
        base.PerformPeriodicalAction( );

        if ( _concurrentQueue.IsEmpty )
        {
            return;
        }

        int count = _concurrentQueue.Count;

        for ( int index = 0; index < count; ++index )
        {
            ChartDrawDataEx.sTrade result;
            if ( _concurrentQueue.TryDequeue( out result ) )
            {
                AddTradeCustomAnnotation( ref result );
            }
        }
    }

    private void Remove( )
    {
        DrawingSurface.RemoveAnnotation( RootElem, null );
    }


}
