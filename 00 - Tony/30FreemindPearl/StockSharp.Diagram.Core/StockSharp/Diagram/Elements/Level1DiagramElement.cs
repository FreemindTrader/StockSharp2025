// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.Level1DiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>The Level1 element.</summary>
  [DescriptionLoc("Level1", false)]
  [CategoryLoc("Sources")]
  [DisplayNameLoc("Level1")]
  [Doc("topics/Designer_Level_1.html")]
  public class Level1DiagramElement : SubscriptionDiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260197690).To<Guid>();
    
    private readonly string _iconName = nameof(-1260197699);
    
    private readonly DiagramElementParam<Level1Fields?> \u0023\u003DzUSgM8rI\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.Level1DiagramElement" />.
    /// </summary>
    public Level1DiagramElement()
    {
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str3154, DiagramSocketType.Unit, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzUSgM8rI\u003D = this.AddParam<Level1Fields?>(nameof(-1260197747), new Level1Fields?()).SetDisplay<DiagramElementParam<Level1Fields?>>(LocalizedStrings.Str3131, LocalizedStrings.Str3099, LocalizedStrings.Str3099, 10).SetOnValueChangedHandler<Level1Fields?>(new Action<Level1Fields?>(this.\u0023\u003DzoJ2_obkV8LapK2wTh2g_qJI\u003D));
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

    /// <summary>Level1 field.</summary>
    public Level1Fields? ValueType
    {
      get
      {
        return this.\u0023\u003DzUSgM8rI\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzUSgM8rI\u003D.Value = value;
      }
    }

    /// <inheritdoc />
    protected override Subscription OnCreateSubscription()
    {
      Level1DiagramElement.\u0023\u003Dz17XT8W4AeeK4wzQz1hDkdvw\u003D aeeK4wzQz1hDkdvw = new Level1DiagramElement.\u0023\u003Dz17XT8W4AeeK4wzQz1hDkdvw\u003D();
      aeeK4wzQz1hDkdvw._diagramElement = this;
      aeeK4wzQz1hDkdvw.\u0023\u003Dzv10ueLU\u003D = DataType.Level1.ToSubscription();
      aeeK4wzQz1hDkdvw.\u0023\u003Dzv10ueLU\u003D.WhenLevel1Received((ISubscriptionProvider) this.Strategy).Do(new Action<Level1ChangeMessage>(aeeK4wzQz1hDkdvw.\u0023\u003Dzf3cAGPy66XOs\u0024\u00243fUw\u003D\u003D)).Apply<Subscription, Level1ChangeMessage>((IMarketRuleContainer) this.Strategy);
      return aeeK4wzQz1hDkdvw.\u0023\u003Dzv10ueLU\u003D;
    }

    private void \u0023\u003DzoJ2_obkV8LapK2wTh2g_qJI\u003D(Level1Fields? _param1)
    {
      this.SetElementName(_param1.HasValue ? _param1.GetValueOrDefault().GetDisplayName() : (string) null);
    }

    private sealed class \u0023\u003Dz17XT8W4AeeK4wzQz1hDkdvw\u003D
    {
      public Level1DiagramElement _diagramElement;
      public Subscription \u0023\u003Dzv10ueLU\u003D;

      internal void \u0023\u003Dzf3cAGPy66XOs\u0024\u00243fUw\u003D\u003D(
        Level1ChangeMessage _param1)
      {
        bool flag = false;
        foreach (KeyValuePair<Level1Fields, object> change in (IEnumerable<KeyValuePair<Level1Fields, object>>) _param1.Changes)
        {
          Level1Fields? valueType = this._diagramElement.ValueType;
          Level1Fields key = change.Key;
          if (valueType.GetValueOrDefault() == key & valueType.HasValue)
          {
            flag = true;
            this._diagramElement.RaiseProcessOutput(_param1.ServerTime, change.Value, (DiagramSocketValue) null, this.\u0023\u003Dzv10ueLU\u003D);
          }
        }
        if (!flag)
          return;
        this._diagramElement.Strategy.Flush();
      }
    }
  }
}
