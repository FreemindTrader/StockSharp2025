// Decompiled with JetBrains decompiler
// Type: #=zmkbc_sXEtSFVpZIzumPO5ikOwVZ3
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Diagram;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.ComponentModel.DataAnnotations;

internal sealed class DiagramSocketBreakpointEx2<X> : DiagramSocketBreakpoint
  where X : struct, IComparable
{

    private X? _minValue;

    private X? _maxValue;

    public DiagramSocketBreakpointEx2( DiagramSocket _param1 )
      : base( _param1 )
    {
    }

    [Display( Description = "Str3508", GroupName = "Common", Name = "Str3508", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
    public X? MinValue
    {
        get
        {
            return this._minValue;
        }
        set
        {
            this._minValue = value;
        }
    }

    [Display( Description = "Str3510", GroupName = "Common", Name = "Str3510", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
    public X? MaxValue
    {
        get
        {
            return this._maxValue;
        }
        set
        {
            this._maxValue = value;
        }
    }

    protected override bool OnNeedBreak( object _param1 )
    {
        Type type = typeof( X );
        if ( type == typeof( Decimal ) )
        {
            if ( ( object )( _param1 as Unit ) == null && !( _param1 is IIndicatorValue ) && !_param1.GetType().IsNumeric() )
                return false;
        }
        else if ( type.IsDateTime() )
        {
            if ( _param1?.GetType() != type )
                return false;
        }
        else if ( type == typeof( TimeSpan ) && !( _param1 is TimeSpan ) )
            return false;
        IComparable comparable1 = ( IComparable )_param1;
        if ( this.MinValue.HasValue )
        {
            IComparable comparable2 = ( IComparable )this.MinValue.Value;
            if ( !type.IsDateOrTime() )
                CompositionHelper.Convert( ref comparable1, ref comparable2 );
            if ( comparable1.Compare( ( object )comparable2 ) < 0 )
                return false;
        }
        X? maxValue = this.MaxValue;
        if ( maxValue.HasValue )
        {
            maxValue = this.MaxValue;
            IComparable comparable2 = ( IComparable )maxValue.Value;
            if ( !type.IsDateOrTime() )
                CompositionHelper.Convert( ref comparable1, ref comparable2 );
            if ( comparable1.Compare( ( object )comparable2 ) > 0 )
                return false;
        }
        return true;
    }

    public override void Load( SettingsStorage _param1 )
    {
        base.Load( _param1 );
        this.MinValue = _param1.GetValue<X?>( nameof( -1260192581 ), this.MinValue );
        this.MaxValue = _param1.GetValue<X?>( nameof( -1260192630 ), this.MaxValue );
    }

    public override void Save( SettingsStorage _param1 )
    {
        base.Save( _param1 );
        _param1.SetValue<X?>( nameof( -1260192581 ), this.MinValue );
        _param1.SetValue<X?>( nameof( -1260192630 ), this.MaxValue );
    }
}
