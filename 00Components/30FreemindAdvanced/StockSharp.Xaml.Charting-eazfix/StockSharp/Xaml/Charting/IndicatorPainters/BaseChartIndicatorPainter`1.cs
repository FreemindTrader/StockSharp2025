// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.BaseChartIndicatorPainter`1
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

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

public abstract class BaseChartIndicatorPainter<TIndicator> : ChartBaseViewModel,
  IPersistable,
  IChartIndicatorPainter
  where TIndicator : IIndicator
{
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

        internal DateTime SomeDateTimeMEthod03843( int _param1 )
        {
            return this._variable0384[ _param1 ].Time;
        }

        internal double SomeDateTimeMEthod00984( int _param1 )
        {
            return this._variableSome3535.SomeMehtod03485( this._variable0384, _param1 );
        }

        internal double SomeMehtod96863485( int _param1 )
        {
            return this._variableSome3535.SomeMehtod03485( this._variable3432Some3535, _param1 );
        }
    }

    private sealed class SomeClass7237
    {
        public Func<int, DateTime> _variable3432Some34343214;
    public Func<int, double> _variable3432Some098374;
    public Func<int, double> _variable984573;
    public Func<int, int> _variable987333;

    internal ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime> SomeMehtod31513(
      int _param1)
    {
      DateTime dateTime = this._variable3432Some34343214(_param1);
      double num1 = this._variable3432Some098374( _param1);
        Func<int, double> z5Kb6DbUnfYsy = this._variable984573;
      double num2 = z5Kb6DbUnfYsy != null ? z5Kb6DbUnfYsy(_param1) : double.NaN;
        Func<int, int> zSd3FqrQ = this._variable987333;
      int num3 = zSd3FqrQ != null ? zSd3FqrQ(_param1) : 0;
      return ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<DateTime>.\u0023\u003DzpxJeWbQ\u003D(dateTime, num1, num2, num3);
    }
}


private sealed class SomeClass5555
{
    public IList<ChartDrawData.IndicatorData> _IList03843;
    public BaseChartIndicatorPainter<T> _variableSome3535;

    internal DateTime SomeDateTimeMEthod03843( int _param1 )
    {
        return this._IList03843[ _param1 ].Time;
    }

    internal double SomeDateTimeMEthod00984( int _param1 )
    {
        return this._variableSome3535.SomeMehtod03485( this._IList03843, _param1 );
    }

    internal int SomeMehtod96863485( int _param1 )
    {
        return BaseChartIndicatorPainter<T>.SomeTemplateMehtod8888( this._IList03843, _param1 );
    }
}

private sealed class SomeClass7654
{
    public IList<ChartDrawData.IndicatorData> _IList03843;
    public Func<ChartDrawData.IndicatorData, double> _Func_ChartDrawData_IndicatorData_0303;

    internal DateTime SomeDateTimeMEthod03843( int _param1 )
    {
        return this._IList03843[ _param1 ].Time;
    }

    internal double SomeDateTimeMEthod00984( int _param1 )
    {
        return this._Func_ChartDrawData_IndicatorData_0303( this._IList03843[ _param1 ] );
    }
}


[Serializable]
private new sealed class SomeClass34343383
{
    public static readonly BaseChartIndicatorPainter<T>.SomeClass34343383 SomeMethond0343 = new BaseChartIndicatorPainter<T>.SomeClass34343383();
    public static Action<IDrawableChartElement> SomeIntenalMethod003D;
    public static Func<IIndicator, IList<ChartDrawData.IndicatorData>> _func_indicator_IList_data_00038;

    internal void SomeI0398389ntenalMethod003D( IDrawableChartElement _param1 )
    {
        _param1.StartDrawing();
    }

    internal IList<ChartDrawData.IndicatorData> GetChartDrawData_IndicatorData_0099(
      IIndicator _param1 )
    {
        return ( IList<ChartDrawData.IndicatorData> ) new List<ChartDrawData.IndicatorData>();
    }
    }



    private readonly List<IChartElement> _innerElements = new List<IChartElement>();

    private IChartIndicatorElement _indicatorElement;

    internal static IIndicatorColorProvider GetColorProvider()
    {
        return ChartHelper.EnsureColorProvider();
    }


    public IChartIndicatorElement Element
    {
        get => this._indicatorElement;
        private set => this._indicatorElement = value;
    }


    public IReadOnlyList<IChartElement> InnerElements
    {
        get => ( IReadOnlyList<IChartElement> ) this._innerElements;
    }

    protected bool IsAttached => this.Element != null;

    private ChartIndicatorElement GetIndicatorElement() => ( ChartIndicatorElement ) this.Element;

    private void StartDrawing()
    {
        CollectionHelper.ForEach<IDrawableChartElement>( this.GetIndicatorElement().ChildElements.OfType<IDrawableChartElement>(), BaseChartIndicatorPainter<TIndicator>.SomeClass34343383.SomeIntenalMethod003D ?? ( BaseChartIndicatorPainter<TIndicator>.SomeClass34343383.SomeIntenalMethod003D = new Action<IDrawableChartElement>( BaseChartIndicatorPainter<TIndicator>.SomeClass34343383.SomeMethond0343.SomeI0398389ntenalMethod003D ) ) );
    }

    protected abstract bool OnDraw( TIndicator indicator, IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data );

    public virtual bool Draw( IChartDrawData data )
    {
        if ( !this.IsAttached )
            return false;
        List<ChartDrawData.IndicatorData> indicatorDataList = ((ChartDrawData) data).GetActiveOrders(this.Element);
        if ( indicatorDataList == null || CollectionHelper.IsEmpty<ChartDrawData.IndicatorData>( ( ICollection<ChartDrawData.IndicatorData> ) indicatorDataList ) )
        {
            this.StartDrawing();
            return false;
        }
        BaseChartIndicatorPainter<TIndicator>.DictionaryStruct03894 dkfA7SK9Zsjh7b7evY;
        dkfA7SK9Zsjh7b7evY._dictionaryStruct03894 = new Dictionary<IIndicator, IList<ChartDrawData.IndicatorData>>();
        foreach ( ChartDrawData.IndicatorData indicatorData in indicatorDataList )
            BaseChartIndicatorPainter<TIndicator>.SomeIntenalStaticVoidMethod333( indicatorData.Value.Indicator, indicatorData.Time, indicatorData.Value, ref dkfA7SK9Zsjh7b7evY );
        return this.OnDraw( ( TIndicator ) indicatorDataList[ 0 ].Value.Indicator, ( IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> ) dkfA7SK9Zsjh7b7evY._dictionaryStruct03894 );
    }

    public virtual void Reset()
    {
    }

    public void OnAttached( IChartIndicatorElement element )
    {
        this.Element = element;
        CollectionHelper.ForEach<IChartElement>( ( IEnumerable<IChartElement> ) this.InnerElements, new Action<IChartElement>( this.AddChildElement ) );
    }

    public void OnDetached()
    {
        if ( this.Element == null )
            return;
        CollectionHelper.ForEach<IChartElement>( ( IEnumerable<IChartElement> ) this.InnerElements, new Action<IChartElement>( this.RemoveChildElement ) );
        this.Element = ( IChartIndicatorElement ) null;
    }

    protected bool DrawValues(
      IList<ChartDrawData.IndicatorData> vals,
      IChartElement element,
      Func<ChartDrawData.IndicatorData, double> getValue )
    {
        BaseChartIndicatorPainter<TIndicator>.SomeClass7654 lvtppRwsYcyoelU8 = new BaseChartIndicatorPainter<TIndicator>.SomeClass7654();
        lvtppRwsYcyoelU8._IList03843 = vals;
        lvtppRwsYcyoelU8._Func_ChartDrawData_IndicatorData_0303 = getValue;
        if ( lvtppRwsYcyoelU8._IList03843 == null )
            throw new ArgumentNullException( nameof( vals ) );
        return BaseChartIndicatorPainter<TIndicator>.SomePrivateStaticBool0858( element, lvtppRwsYcyoelU8._IList03843.Count, new Func<int, DateTime>( lvtppRwsYcyoelU8.SomeDateTimeMEthod03843 ), new Func<int, double>( lvtppRwsYcyoelU8.SomeDateTimeMEthod00984 ), ( Func<int, double> ) null, ( Func<int, int> ) null );
    }

    protected bool DrawValues( IList<ChartDrawData.IndicatorData> vals, IChartElement element )
    {
        BaseChartIndicatorPainter<TIndicator>.SomeClass5555 kiaDl76b0Nyu42rxJq = new BaseChartIndicatorPainter<TIndicator>.SomeClass5555();
        kiaDl76b0Nyu42rxJq._IList03843 = vals;
        kiaDl76b0Nyu42rxJq._variableSome3535 = this;
        if ( kiaDl76b0Nyu42rxJq._IList03843 == null )
            throw new ArgumentNullException( nameof( vals ) );
        return BaseChartIndicatorPainter<TIndicator>.SomePrivateStaticBool0858( element, kiaDl76b0Nyu42rxJq._IList03843.Count, new Func<int, DateTime>( kiaDl76b0Nyu42rxJq.SomeDateTimeMEthod03843 ), new Func<int, double>( kiaDl76b0Nyu42rxJq.SomeDateTimeMEthod00984 ), ( Func<int, double> ) null, new Func<int, int>( kiaDl76b0Nyu42rxJq.SomeMehtod96863485 ) );
    }

    protected bool DrawValues(
      IList<ChartDrawData.IndicatorData> vals1,
      IList<ChartDrawData.IndicatorData> vals2,
      IChartElement element )
    {
        BaseChartIndicatorPainter<TIndicator>.SomeClass6409 v4vdZv8GtEzAmB0rzFq = new BaseChartIndicatorPainter<TIndicator>.SomeClass6409();
        v4vdZv8GtEzAmB0rzFq._variable0384 = vals1;
        v4vdZv8GtEzAmB0rzFq._variableSome3535 = this;
        v4vdZv8GtEzAmB0rzFq._variable3432Some3535 = vals2;
        if ( v4vdZv8GtEzAmB0rzFq._variable0384 == null )
            throw new ArgumentNullException( nameof( vals1 ) );
        return BaseChartIndicatorPainter<TIndicator>.SomePrivateStaticBool0858( element, v4vdZv8GtEzAmB0rzFq._variable0384.Count, new Func<int, DateTime>( v4vdZv8GtEzAmB0rzFq.SomeDateTimeMEthod03843 ), new Func<int, double>( v4vdZv8GtEzAmB0rzFq.SomeDateTimeMEthod00984 ), new Func<int, double>( v4vdZv8GtEzAmB0rzFq.SomeMehtod96863485 ), ( Func<int, int> ) null );
    }

    protected bool DrawValues(
      IList<ChartDrawData.IndicatorData> vals1,
      IList<ChartDrawData.IndicatorData> vals2,
      IChartElement element,
      Func<double, double, double> op )
    {
        BaseChartIndicatorPainter<TIndicator>.SomeClass398 jq9Llz3ahZ2LrQl4 = new BaseChartIndicatorPainter<TIndicator>.SomeClass398();
        jq9Llz3ahZ2LrQl4._variable0384 = vals1;
        jq9Llz3ahZ2LrQl4.\u0023\u003Dz6BEwh7k\u003D = op;
        jq9Llz3ahZ2LrQl4._variableSome3535 = this;
        jq9Llz3ahZ2LrQl4._variable3432Some3535 = vals2;
        if ( jq9Llz3ahZ2LrQl4._variable0384 == null )
            throw new ArgumentNullException( nameof( vals1 ) );
        if ( jq9Llz3ahZ2LrQl4.\u0023\u003Dz6BEwh7k\u003D == null)
      throw new ArgumentNullException( nameof( op ) );
        return BaseChartIndicatorPainter<TIndicator>.SomePrivateStaticBool0858( element, jq9Llz3ahZ2LrQl4._variable0384.Count, new Func<int, DateTime>( jq9Llz3ahZ2LrQl4.SomeDateTimeMEthod03843 ), new Func<int, double>( jq9Llz3ahZ2LrQl4.SomeDateTimeMEthod00984 ), ( Func<int, double> ) null, ( Func<int, int> ) null );
    }

    private static bool SomePrivateStaticBool0858(
      IChartElement _param0,
      int _param1,
      Func<int, DateTime> _param2,
      Func<int, double> _param3,
      Func<int, double> _param4,
      Func<int, int> _param5 )
    {
        BaseChartIndicatorPainter<TIndicator>.SomeClass7237 doDcwiev7trI4Ny0 = new BaseChartIndicatorPainter<TIndicator>.SomeClass7237();
        doDcwiev7trI4Ny0._variable3432Some34343214 = _param2;
        doDcwiev7trI4Ny0._variable3432Some098374 = _param3;
        doDcwiev7trI4Ny0._variable984573 = _param4;
        doDcwiev7trI4Ny0._variable987333 = _param5;
        if ( !( _param0 is IDrawableChartElement uuxsVv2V6fHz4Vm4X ) )
            throw new InvalidOperationException( "invalid chart element" );
        return uuxsVv2V6fHz4Vm4X.StartDrawing( CollectionHelper.ToEx<ChartDrawData.IDrawValue>( Enumerable.Range( 0, _param1 ).Select < int, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244 < DateTime >> ( new Func<int, ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244 < DateTime >> ( doDcwiev7trI4Ny0.SomeMehtod31513) ).Cast<ChartDrawData.IDrawValue>(), _param1));
    }

    private double SomeMehtod03485( IList<ChartDrawData.IndicatorData> _param1, int _param2 )
    {
        if ( _param1 == null || _param2 >= _param1.Count )
            return double.NaN;
        IIndicatorValue indicatorValue = _param1[_param2].Value;
        if ( indicatorValue != null && !indicatorValue.IsEmpty )
        {
            IChart chart = this.Element.TryGetChart();
            if ( chart == null )
                throw new InvalidOperationException( $"Chart is not set for '{this.Element}'." );
            if ( chart.ShowNonFormedIndicators || indicatorValue.IsFormed )
                return ( double ) indicatorValue.GetValue<Decimal>();
        }
        return double.NaN;
    }

    private static int SomeTemplateMehtod8888(
      IList<ChartDrawData.IndicatorData> _param0,
      int _param1 )
    {
        return _param0 == null || _param1 >= _param0.Count || !( _param0[ _param1 ].Value is ShiftedIndicatorValue shiftedIndicatorValue ) || shiftedIndicatorValue.IsEmpty ? 0 : shiftedIndicatorValue.Shift;
    }

    protected void AddChildElement( IChartElement element )
    {
        BaseChartIndicatorPainter<TIndicator>.SomeClass343 vqd1Qhu2nAw1nzwT0 = new BaseChartIndicatorPainter<TIndicator>.SomeClass343();
        vqd1Qhu2nAw1nzwT0._variableSome3535 = this;
        vqd1Qhu2nAw1nzwT0.\u0023\u003Dz_i6sZDg\u003D = element;
        if ( !CollectionHelper.TryAdd<IChartElement>( ( ICollection<IChartElement> ) this._innerElements, vqd1Qhu2nAw1nzwT0.\u0023\u003Dz_i6sZDg\u003D) )
            throw new ArgumentException( nameof( element ) );
        if ( !this.IsAttached )
            return;
        GuiDispatcher.GlobalDispatcher.AddSyncAction( new Action( vqd1Qhu2nAw1nzwT0.\u0023\u003Dzcq6kjuER1auFcNKkPQ\u003D\u003D) );
    }

    protected void RemoveChildElement( IChartElement element )
    {
        if ( !this._innerElements.Remove( element ) )
            throw new ArgumentException( nameof( element ) );
        if ( !this.IsAttached )
            return;
        this.GetIndicatorElement().RemoveChildElement( element );
    }



    internal static void SomeIntenalStaticVoidMethod333(
      IIndicator _param0,
      DateTime _param1,
      IIndicatorValue _param2,
      ref BaseChartIndicatorPainter<TIndicator>.DictionaryStruct03894 _param3 )
    {
        CollectionHelper.SafeAdd<IIndicator, IList<ChartDrawData.IndicatorData>>( ( IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> ) _param3._dictionaryStruct03894, _param0, BaseChartIndicatorPainter<TIndicator>.SomeClass34343383._func_indicator_IList_data_00038 ?? ( BaseChartIndicatorPainter<TIndicator>.SomeClass34343383._func_indicator_IList_data_00038 = new Func<IIndicator, IList<ChartDrawData.IndicatorData>>( BaseChartIndicatorPainter<TIndicator>.SomeClass34343383.SomeMethond0343.GetChartDrawData_IndicatorData_0099 ) ) ).Add( new ChartDrawData.IndicatorData( _param1, _param2 ) );
        if ( !( _param2 is ComplexIndicatorValue complexIndicatorValue ) )
            return;
        foreach ( KeyValuePair<IIndicator, IIndicatorValue> innerValue in ( IEnumerable<KeyValuePair<IIndicator, IIndicatorValue>> ) complexIndicatorValue.InnerValues )
            BaseChartIndicatorPainter<TIndicator>.SomeIntenalStaticVoidMethod333( innerValue.Key, _param1, innerValue.Value, ref _param3 );
    }

    private void AddChildElement( IChartElement _param1 )
    {
        this.GetIndicatorElement().AddChildElement( _param1 );
    }

    private void RemoveChildElement( IChartElement _param1 )
    {
        this.GetIndicatorElement().RemoveChildElement( _param1 );
    }



  private sealed class SomeClass343
{
    public BaseChartIndicatorPainter<T> _variableSome3535;
    public IChartElement \u0023\u003Dz_i6sZDg\u003D;

    internal void \u0023\u003Dzcq6kjuER1auFcNKkPQ\u003D\u003D()
    {
      this._variableSome3535.GetIndicatorElement().AddChildElement( this.\u0023\u003Dz_i6sZDg\u003D);
    // ISSUE: explicit non-virtual call
    ScichartSurfaceMVVM tdnKj06Uu87Wzk09Wj = ((ChartArea) __nonvirtual (this._variableSome3535.Element).PersistentChartArea).ViewModel;
    ChartCompentViewModel a4VgOpCeDiqsTdzB;
      if (!tdnKj06Uu87Wzk09Wj.\u0023\u003DzKDbpj6zM462r( (IChartComponent) this._variableSome3535.GetIndicatorElement(), out a4VgOpCeDiqsTdzB))
        return;
      a4VgOpCeDiqsTdzB.InitializeChildElements( (IEnumerable<DrawableChartElementBaseViewModel>) new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<DrawableChartElementBaseViewModel>(((IDrawableChartElement) this.\u0023\u003Dz_i6sZDg\u003D).CreateViewModel(tdnKj06Uu87Wzk09Wj)));
    }
  }

  private sealed class SomeClass398
{
    public IList<ChartDrawData.IndicatorData> _variable0384;
    public Func<double, double, double> \u0023\u003Dz6BEwh7k\u003D;
    public BaseChartIndicatorPainter<T> _variableSome3535;
    public IList<ChartDrawData.IndicatorData> _variable3432Some3535;

    internal DateTime SomeDateTimeMEthod03843( int _param1 )
    {
        return this._variable0384[ _param1 ].Time;
    }

    internal double SomeDateTimeMEthod00984( int _param1 )
    {
        return this.\u0023\u003Dz6BEwh7k\u003D(this._variableSome3535.SomeMehtod03485( this._variable0384, _param1 ), this._variableSome3535.SomeMehtod03485( this._variable3432Some3535, _param1 ));
    }
}



[StructLayout( LayoutKind.Auto )]
private struct DictionaryStruct03894
{
    
    public Dictionary<IIndicator, IList<ChartDrawData.IndicatorData>> _dictionaryStruct03894;
}


  
}
