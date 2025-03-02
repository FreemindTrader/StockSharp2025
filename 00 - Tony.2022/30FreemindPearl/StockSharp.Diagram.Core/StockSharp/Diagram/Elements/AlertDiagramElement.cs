// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.AlertDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Alerts;
using StockSharp.Localization;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// Notification element (sound, window etc.) for specific market events.
  /// </summary>
  [DescriptionLoc("Str3163", false)]
  [CategoryLoc("Inform")]
  [Doc("topics/Designer_Notice.html")]
  [DisplayNameLoc("Str3162")]
  public sealed class AlertDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260196747).To<Guid>();
    
    private readonly string _iconName = nameof(-1260196824);
    
    private readonly DiagramElementParam<AlertNotifications> _type;
    
    private readonly DiagramElementParam<string> \u0023\u003DzukUJIPw\u003D;
    
    private readonly DiagramElementParam<string> \u0023\u003DzO561bj0\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.AlertDiagramElement" />.
    /// </summary>
    public AlertDiagramElement()
    {
      this.AddInput(StaticSocketIds.Flag, LocalizedStrings.Flag, DiagramSocketType.Bool, new Action<DiagramSocketValue>(this.\u0023\u003DzHMHk24A\u003D), 1, int.MaxValue, false, new bool?());
      this._type = this.AddParam<AlertNotifications>(nameof(-1260198464), AlertNotifications.Popup).SetDisplay<DiagramElementParam<AlertNotifications>>(LocalizedStrings.Common, LocalizedStrings.Type, LocalizedStrings.Str3164, 10);
      this.\u0023\u003DzukUJIPw\u003D = this.AddParam<string>(nameof(-1260196801), string.Empty).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Common, LocalizedStrings.Str215, LocalizedStrings.Str3165, 20);
      this.\u0023\u003DzO561bj0\u003D = this.AddParam<string>(nameof(-1260196815), string.Empty).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Common, LocalizedStrings.Str3166, LocalizedStrings.Str3167, 30);
    }

    /// <inheritdoc />
    public override Guid TypeId
    {
      get
      {
        return this._typeId;
      }
    }

    /// <inheritdoc />
    public override string IconName
    {
      get
      {
        return this._iconName;
      }
    }

    /// <summary>Alert type.</summary>
    public AlertNotifications Type
    {
      get
      {
        return this._type.Value;
      }
      set
      {
        this._type.Value = value;
      }
    }

    /// <summary>Signal header.</summary>
    public string Caption
    {
      get
      {
        return this.\u0023\u003DzukUJIPw\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzukUJIPw\u003D.Value = value;
      }
    }

    /// <summary>Alert text.</summary>
    public string Message
    {
      get
      {
        return this.\u0023\u003DzO561bj0\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzO561bj0\u003D.Value = value;
      }
    }

    private void \u0023\u003DzHMHk24A\u003D(DiagramSocketValue _param1)
    {
      if (!_param1.GetValue<bool>())
        return;
      AlertServicesRegistry.NotificationService.Notify(this.Type, this.Caption, this.Message, _param1.Time);
    }
  }
}
