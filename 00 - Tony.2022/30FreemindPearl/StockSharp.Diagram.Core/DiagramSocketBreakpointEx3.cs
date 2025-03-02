using Ecng.Serialization;
using StockSharp.Diagram;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

internal sealed class DiagramSocketBreakpointEx3<T> : DiagramSocketBreakpoint where T : struct
{

    private T? _value;

    public DiagramSocketBreakpointEx3( DiagramSocket _param1 ) : base( _param1 )
    {
    }

    [Display( Description = "Str3099", GroupName = "Common", Name = "Str3099", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
    public T? Value
    {
        get
        {
            return this._value;
        }
        set
        {
            this._value = value;
        }
    }

    protected override bool OnNeedBreak( object _param1 )
    {
        if ( !this.Value.HasValue )
            return true;
        T? nullable = _param1 as T?;
        ref T? local = ref nullable;
        if ( !local.HasValue )
            return false;
        return local.GetValueOrDefault().Equals( ( object )this.Value );
    }

    public override void Load( SettingsStorage _param1 )
    {
        base.Load( _param1 );
        this.Value = _param1.GetValue<T?>( nameof( Value), this.Value );
    }

    public override void Save( SettingsStorage _param1 )
    {
        base.Save( _param1 );
        _param1.SetValue<T?>( nameof( Value), this.Value );
    }
}
