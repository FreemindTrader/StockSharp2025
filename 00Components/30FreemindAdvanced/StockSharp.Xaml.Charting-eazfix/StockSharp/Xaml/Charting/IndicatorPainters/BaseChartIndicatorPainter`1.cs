using Ecng.Collections;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <summary>
/// The indicator renderer base class on the chart (for example, lines, histograms, etc.).
/// </summary>
/// <typeparam name="TIndicator">Type of <see cref="T:StockSharp.Algo.Indicators.IIndicator" />.</typeparam>
public abstract class BaseChartIndicatorPainter<TIndicator> : ChartBaseViewModel, IPersistable, IChartIndicatorPainter
    where TIndicator : IIndicator
{
    private readonly List<IChartElement> _innerElements = new List<IChartElement>();

    private IChartIndicatorElement _indicatorElement;

    public static IIndicatorColorProvider GetColorProvider()
    {
        return ChartHelper.EnsureColorProvider();
    }


    public IChartIndicatorElement Element
    {
        get => _indicatorElement;
        private set => _indicatorElement = value;
    }


    public IReadOnlyList<IChartElement> InnerElements
    {
        get => ( IReadOnlyList<IChartElement> ) _innerElements;
    }

    /// <summary>
    /// Whether this painter is currently attached to chart element.
    /// </summary>
    protected bool IsAttached => Element != null;

    private ChartIndicatorElement GetIndicatorElement() => ( ChartIndicatorElement ) Element;

    private void StartDrawing()
    {
        CollectionHelper.ForEach( GetIndicatorElement().ChildElements.OfType<IDrawableChartElement>(), p => p.StartDrawing() );
    }

    /// <summary>Draw values on chart.</summary>
    /// <param name="indicator">Indicator.</param>
    /// <param name="data">Indicator values to draw on chart.</param>
    /// <returns>
    /// <see langword="true" /> if the data was successfully drawn, otherwise, returns <see langword="false" />.</returns>
    protected abstract bool OnDraw( TIndicator indicator, IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data );


    public struct IndicatorDrawDataStruct
    {
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        public Dictionary<IIndicator, IList<ChartDrawData.IndicatorData>> _indicatorDrawDataList;
    }

    public virtual bool Draw( IChartDrawData data )
    {
        if ( !IsAttached )
            return false;
        var indicatorDatas = ((ChartDrawData) data).GetCandleRelatedData(Element);

        if ( indicatorDatas == null || CollectionHelper.IsEmpty( indicatorDatas ) )
        {
            StartDrawing();
            return false;
        }
        BaseChartIndicatorPainter<TIndicator>.IndicatorDrawDataStruct indStruct;
        indStruct._indicatorDrawDataList = new Dictionary<IIndicator, IList<ChartDrawData.IndicatorData>>();
        foreach ( var indicatorData in indicatorDatas )
            BaseChartIndicatorPainter<TIndicator>.SomeIntenalStaticVoidMethod333(
                indicatorData.Value.Indicator,
                indicatorData.Time,
                indicatorData.Value,
                ref indStruct );

        return OnDraw( ( TIndicator ) indicatorDatas[ 0 ].Value.Indicator, indStruct._indicatorDrawDataList );
    }



    public static void SomeIntenalStaticVoidMethod333( IIndicator i, DateTime t, IIndicatorValue v, ref BaseChartIndicatorPainter<TIndicator>.IndicatorDrawDataStruct s )
    {
        CollectionHelper.SafeAdd( s._indicatorDrawDataList, i, p => new List<ChartDrawData.IndicatorData>() ).Add( new ChartDrawData.IndicatorData( t, v ) );

        if ( !( v is ComplexIndicatorValue complexIndicatorValue ) )
            return;

        foreach ( var innerValue in complexIndicatorValue.InnerValues )
        {
            BaseChartIndicatorPainter<TIndicator>.SomeIntenalStaticVoidMethod333(
                innerValue.Key,
                t,
                innerValue.Value,
                ref s );

        }

    }

    private static bool SomePrivateStaticBool0858(
        IChartElement _param0,
        int _param1,
        Func<int, DateTime> _param2,
        Func<int, double> _param3,
        Func<int, double> _param4,
        Func<int, int> _param5 )
    {
        BaseChartIndicatorPainter<TIndicator>.SomeClass7237 doDcwiev7trI4Ny0 = new BaseChartIndicatorPainter<TIndicator>.SomeClass7237(
            );
        doDcwiev7trI4Ny0._variable3432Some34343214 = _param2;
        doDcwiev7trI4Ny0._variable3432Some098374 = _param3;
        doDcwiev7trI4Ny0._variable984573 = _param4;
        doDcwiev7trI4Ny0._variable987333 = _param5;
        if ( !( _param0 is IDrawableChartElement uuxsVv2V6fHz4Vm4X ) )
            throw new InvalidOperationException( "invalid chart element" );
        return uuxsVv2V6fHz4Vm4X.StartDrawing(
            CollectionHelper.ToEx<ChartDrawData.IDrawValue>(
                Enumerable.Range( 0, _param1 )
                    .Select<int, ChartDrawData.sxTuple<DateTime>>(
                        new Func<int, ChartDrawData.sxTuple<DateTime>>( doDcwiev7trI4Ny0.SomeMehtod31513 ) )
                    .Cast<ChartDrawData.IDrawValue>(),
                _param1 ) );
    }

    public virtual void Reset()
    {
    }

    public void OnAttached( IChartIndicatorElement element )
    {
        Element = element;
        CollectionHelper.ForEach<IChartElement>(
            ( IEnumerable<IChartElement> ) InnerElements,
            new Action<IChartElement>( AddChildElement ) );
    }

    public void OnDetached()
    {
        if ( Element == null )
            return;
        CollectionHelper.ForEach<IChartElement>(
            ( IEnumerable<IChartElement> ) InnerElements,
            new Action<IChartElement>( RemoveChildElement ) );
        Element = ( IChartIndicatorElement ) null;
    }

    protected bool DrawValues(
        IList<ChartDrawData.IndicatorData> vals,
        IChartElement element,
        Func<ChartDrawData.IndicatorData, double> getValue )
    {
        BaseChartIndicatorPainter<TIndicator>.SomeClass7654 lvtppRwsYcyoelU8 = new BaseChartIndicatorPainter<TIndicator>.SomeClass7654(
            );
        lvtppRwsYcyoelU8._IList03843 = vals;
        lvtppRwsYcyoelU8._Func_ChartDrawData_IndicatorData_0303 = getValue;
        if ( lvtppRwsYcyoelU8._IList03843 == null )
            throw new ArgumentNullException( nameof( vals ) );
        return BaseChartIndicatorPainter<TIndicator>.SomePrivateStaticBool0858(
            element,
            lvtppRwsYcyoelU8._IList03843.Count,
            new Func<int, DateTime>( lvtppRwsYcyoelU8.SomeDateTimeMEthod03843 ),
            new Func<int, double>( lvtppRwsYcyoelU8.SomeDateTimeMEthod00984 ),
            ( Func<int, double> ) null,
            ( Func<int, int> ) null );
    }

    protected bool DrawValues( IList<ChartDrawData.IndicatorData> vals, IChartElement element )
    {
        BaseChartIndicatorPainter<TIndicator>.SomeClass5555 kiaDl76b0Nyu42rxJq = new BaseChartIndicatorPainter<TIndicator>.SomeClass5555(
            );
        kiaDl76b0Nyu42rxJq._IList03843 = vals;
        kiaDl76b0Nyu42rxJq._variableSome3535 = this;
        if ( kiaDl76b0Nyu42rxJq._IList03843 == null )
            throw new ArgumentNullException( nameof( vals ) );
        return BaseChartIndicatorPainter<TIndicator>.SomePrivateStaticBool0858(
            element,
            kiaDl76b0Nyu42rxJq._IList03843.Count,
            new Func<int, DateTime>( kiaDl76b0Nyu42rxJq.SomeDateTimeMEthod03843 ),
            new Func<int, double>( kiaDl76b0Nyu42rxJq.SomeDateTimeMEthod00984 ),
            ( Func<int, double> ) null,
            new Func<int, int>( kiaDl76b0Nyu42rxJq.SomeMehtod96863485 ) );
    }

    protected bool DrawValues( IList<ChartDrawData.IndicatorData> valOne, IList<ChartDrawData.IndicatorData> valTwo, IChartElement comp )
    {
        if ( valOne == null )
            throw new ArgumentNullException( nameof( valOne ) );

        BaseChartIndicatorPainter<TIndicator>.SomeClass6409 v4vdZv8GtEzAmB0rzFq = new BaseChartIndicatorPainter<TIndicator>.SomeClass6409(
            );
        v4vdZv8GtEzAmB0rzFq._variable0384 = valOne;
        v4vdZv8GtEzAmB0rzFq._variableSome3535 = this;
        v4vdZv8GtEzAmB0rzFq._variable3432Some3535 = valTwo;


        return BaseChartIndicatorPainter<TIndicator>.SomePrivateStaticBool0858(
            comp,
            v4vdZv8GtEzAmB0rzFq._variable0384.Count,
            new Func<int, DateTime>( v4vdZv8GtEzAmB0rzFq.SomeDateTimeMEthod03843 ),
            new Func<int, double>( v4vdZv8GtEzAmB0rzFq.SomeDateTimeMEthod00984 ),
            new Func<int, double>( v4vdZv8GtEzAmB0rzFq.SomeMehtod96863485 ),
            ( Func<int, int> ) null );
    }

    protected bool DrawValues(
        IList<ChartDrawData.IndicatorData> vals1,
        IList<ChartDrawData.IndicatorData> vals2,
        IChartElement element,
        Func<double, double, double> op )
    {
        BaseChartIndicatorPainter<TIndicator>.SomeClass398 jq9Llz3ahZ2LrQl4 = new BaseChartIndicatorPainter<TIndicator>.SomeClass398(
            );
        jq9Llz3ahZ2LrQl4._variable0384 = vals1;
        jq9Llz3ahZ2LrQl4.\u0023\u003D
        z6BEwh7k\u003D  = op;
        jq9Llz3ahZ2LrQl4._variableSome3535 = this;
        jq9Llz3ahZ2LrQl4._variable3432Some3535 = vals2;
        if ( jq9Llz3ahZ2LrQl4._variable0384 == null )
            throw new ArgumentNullException( nameof( vals1 ) );
        if ( jq9Llz3ahZ2LrQl4.\u0023\u003D
        z6BEwh7k\u003D  == null)
        throw new ArgumentNullException( nameof( op ) );
        return BaseChartIndicatorPainter<TIndicator>.SomePrivateStaticBool0858(
            element,
            jq9Llz3ahZ2LrQl4._variable0384.Count,
            new Func<int, DateTime>( jq9Llz3ahZ2LrQl4.SomeDateTimeMEthod03843 ),
            new Func<int, double>( jq9Llz3ahZ2LrQl4.SomeDateTimeMEthod00984 ),
            ( Func<int, double> ) null,
            ( Func<int, int> ) null );
    }

    public virtual void Load( SettingsStorage storage )
    {
    }

    public virtual void Save( SettingsStorage storage )
    {
    }

    private sealed class SomeClass6409
    {
        public IList<ChartDrawData.IndicatorData> _variable0384;
        public BaseChartIndicatorPainter<T> _variableSome3535;
        public IList<ChartDrawData.IndicatorData> _variable3432Some3535;

        public DateTime SomeDateTimeMEthod03843( int _param1 )
        {
            return _variable0384[ _param1 ].Time;
        }

        public double SomeDateTimeMEthod00984( int _param1 )
        {
            return _variableSome3535.SomeMehtod03485( _variable0384, _param1 );
        }

        public double SomeMehtod96863485( int _param1 )
        {
            return _variableSome3535.SomeMehtod03485( _variable3432Some3535, _param1 );
        }
    }

    private sealed class SomeClass7237
    {
        public Func<int, DateTime> _variable3432Some34343214;
        public Func<int, double> _variable3432Some098374;
        public Func<int, double> _variable984573;
        public Func<int, int> _variable987333;

        public ChartDrawData.sxTuple<DateTime> SomeMehtod31513( int _param1 )
        {
            DateTime dateTime = _variable3432Some34343214(_param1);
            double num1 = _variable3432Some098374(_param1);
            Func<int, double> z5Kb6DbUnfYsy = _variable984573;
            double num2 = z5Kb6DbUnfYsy != null ? z5Kb6DbUnfYsy(_param1) : double.NaN;
            Func<int, int> zSd3FqrQ = _variable987333;
            int num3 = zSd3FqrQ != null ? zSd3FqrQ(_param1) : 0;
            return ChartDrawData.sxTuple<DateTime>.CreateSxTuple( dateTime, num1, num2, num3 );
        }
    }


    private sealed class SomeClass5555
    {
        public IList<ChartDrawData.IndicatorData> _IList03843;
        public BaseChartIndicatorPainter<T> _variableSome3535;

        public DateTime SomeDateTimeMEthod03843( int _param1 )
        {
            return _IList03843[ _param1 ].Time;
        }

        public double SomeDateTimeMEthod00984( int _param1 )
        {
            return _variableSome3535.SomeMehtod03485( _IList03843, _param1 );
        }

        public int SomeMehtod96863485( int _param1 )
        {
            return BaseChartIndicatorPainter<T>.GetShiftedIndicatorValue( _IList03843, _param1 );
        }
    }

    private sealed class SomeClass7654
    {
        public IList<ChartDrawData.IndicatorData> _IList03843;
        public Func<ChartDrawData.IndicatorData, double> _Func_ChartDrawData_IndicatorData_0303;

        public DateTime SomeDateTimeMEthod03843( int _param1 )
        {
            return _IList03843[ _param1 ].Time;
        }

        public double SomeDateTimeMEthod00984( int _param1 )
        {
            return _Func_ChartDrawData_IndicatorData_0303( _IList03843[ _param1 ] );
        }
    }


    [Serializable]
    private new sealed class SomeClass34343383
    {
        public static readonly BaseChartIndicatorPainter<T>.SomeClass34343383 SomeMethond0343 = new BaseChartIndicatorPainter<T>.SomeClass34343383(
            );
        public static Action<IDrawableChartElement> SomeIntenalMethod003D;
        public static Func<IIndicator, IList<ChartDrawData.IndicatorData>> _func_indicator_IList_data_00038;

        public void SomeI0398389ntenalMethod003D( IDrawableChartElement p )
        {
            p.StartDrawing();
        }

        public IList<ChartDrawData.IndicatorData> GetChartDrawData_IndicatorData_0099( IIndicator _param1 )
        {
            return ( IList<ChartDrawData.IndicatorData> ) new List<ChartDrawData.IndicatorData>();
        }
    }







    private double SomeMehtod03485( IList<ChartDrawData.IndicatorData> _param1, int _param2 )
    {
        if ( _param1 == null || _param2 >= _param1.Count )
            return double.NaN;
        IIndicatorValue indicatorValue = _param1[_param2].Value;
        if ( indicatorValue != null && !indicatorValue.IsEmpty )
        {
            IChart chart = Element.TryGetChart();
            if ( chart == null )
                throw new InvalidOperationException( $"Chart is not set for '{Element}'." );
            if ( chart.ShowNonFormedIndicators || indicatorValue.IsFormed )
                return ( double ) indicatorValue.GetValue<Decimal>();
        }
        return double.NaN;
    }

    private static int GetShiftedIndicatorValue( IList<ChartDrawData.IndicatorData> _param0, int _param1 )
    {
        return _param0 == null ||
                _param1 >= _param0.Count ||
                !( _param0[ _param1 ].Value is ShiftedIndicatorValue shiftedIndicatorValue ) ||
                shiftedIndicatorValue.IsEmpty
            ? 0
            : shiftedIndicatorValue.Shift;
    }

    protected void AddChildElement( IChartElement element )
    {
        BaseChartIndicatorPainter<TIndicator>.SomeClass343 vqd1Qhu2nAw1nzwT0 = new BaseChartIndicatorPainter<TIndicator>.SomeClass343(
            );
        vqd1Qhu2nAw1nzwT0._variableSome3535 = this;
        vqd1Qhu2nAw1nzwT0.\u0023\u003D
        z_i6sZDg\u003D  = element;
        if ( !CollectionHelper.TryAdd<IChartElement>(
            ( ICollection<IChartElement> ) _innerElements,
            vqd1Qhu2nAw1nzwT0.\u0023\u003Dz_i6sZDg\u003D) )
            throw new ArgumentException( nameof( element ) );
        if ( !IsAttached )
            return;
        GuiDispatcher.GlobalDispatcher.AddSyncAction( new Action( vqd1Qhu2nAw1nzwT0.Method01) );
    }

    protected void RemoveChildElement( IChartElement element )
    {
        if ( !_innerElements.Remove( element ) )
            throw new ArgumentException( nameof( element ) );
        if ( !IsAttached )
            return;
        GetIndicatorElement().RemoveChildElement( element );
    }




    private void AddChildElement( IChartElement _param1 )
    {
        GetIndicatorElement().AddChildElement( _param1 );
    }

    private void RemoveChildElement( IChartElement _param1 )
    {
        GetIndicatorElement().RemoveChildElement( _param1 );
    }


    private sealed class SomeClass343
    {
        public BaseChartIndicatorPainter<T> _variableSome3535;
        public IChartElement \u0023\u003Dz_i6sZDg\u003D;

    public void Method01()
    {
      _variableSome3535.GetIndicatorElement().

        AddChildElement( \u0023\u003Dz_i6sZDg\u003D);
        // ISSUE: explicit non-virtual call
        ScichartSurfaceMVVM tdnKj06Uu87Wzk09Wj = ((ChartArea) __nonvirtual(_variableSome3535.Element)
            .PersistentChartArea).ViewModel;
        ChartComponentViewModel a4VgOpCeDiqsTdzB;
      if(!tdnKj06Uu87Wzk09Wj.GetViewModelFromCache

        ((IChartComponent) _variableSome3535.GetIndicatorElement(), out a4VgOpCeDiqsTdzB
            ))
            return
            ;
        a4VgOpCeDiqsTdzB.
            InitializeChildElements((IEnumerable<DrawableChartElementBaseViewModel>) new List<DrawableChartElementBaseViewModel>

        (((IDrawableChartElement) \u0023\u003Dz_i6sZDg\u003D
            ).
            CreateViewModel( tdnKj06Uu87Wzk09Wj)));
    }
}

private sealed class SomeClass398
{
    public IList<ChartDrawData.IndicatorData> _variable0384;
    public Func<double, double, double> _variable0384334;
    public BaseChartIndicatorPainter<T> _variableSome3535;
    public IList<ChartDrawData.IndicatorData> _variable3432Some3535;

    public DateTime SomeDateTimeMEthod03843( int _param1 )
    {
        return _variable0384[ _param1 ].Time;
    }

    public double SomeDateTimeMEthod00984( int _param1 )
    {
        return \u0023\u003D
        z6BEwh7k\u003D
        (_variableSome3535.SomeMehtod03485( _variable0384, _param1 ), _variableSome3535
        .SomeMehtod03485( _variable3432Some3535, _param1 ));
    }
}


[StructLayout( LayoutKind.Auto )]
private struct IndicatorDrawDataStruct
{
    public Dictionary<IIndicator, IList<ChartDrawData.IndicatorData>> _indicatorDrawDataList;
}


}
