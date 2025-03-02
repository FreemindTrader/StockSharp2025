
using Ecng.Serialization;
using StockSharp.Diagram;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

internal sealed class DiagramSocketBreakpointEx : DiagramSocketBreakpoint
{
    
    private bool? _value;

    public DiagramSocketBreakpointEx( DiagramSocket _param1 )
      : base( _param1 )
    {
    }

    [Display( Description = "Str3099", GroupName = "Common", Name = "Str3099", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
    public bool? Value
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
        bool? nullable1 = _param1 as bool?;
        bool? nullable2 = this.Value;
        return nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue;
    }

    public override void Load( SettingsStorage _param1 )
    {
        base.Load( _param1 );
        this.Value = _param1.GetValue<bool?>( nameof( Value ), this.Value );
    }

    public override void Save( SettingsStorage _param1 )
    {
        base.Save( _param1 );
        _param1.SetValue<bool?>( nameof( Value ), this.Value );
    }
}
