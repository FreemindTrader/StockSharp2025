// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.PositionDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// Position element (for security and money) for the specified portfolio.
  /// </summary>
  [CategoryLoc("Common")]
  [DescriptionLoc("Str3137", false)]
  [Doc("topics/Designer_Position.html")]
  [DisplayNameLoc("Str862")]
  public class PositionDiagramElement : StrategyInputDiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260195667).To<Guid>();
    
    private readonly string _iconName = nameof(-1260195712);
    
    private DiagramSocket \u0023\u003DzUiy2ZEijW\u00242h;
    
    private DiagramSocket \u0023\u003DzYKozG5q\u0024phYr;
    
    private Security \u0023\u003DzmirAKT8\u003D;
    
    private Portfolio \u0023\u003DzDrO_6FqVYqxoTfK8gA\u003D\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzXp40NjoKvYHB;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzBJNEfQMDbVf8;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.PositionDiagramElement" />.
    /// </summary>
    public PositionDiagramElement()
      : base(LocalizedStrings.Str862)
    {
      this.\u0023\u003DzNciMOiPBixCj(true);
      this.AddInput(StaticSocketIds.Portfolio, LocalizedStrings.Portfolio, DiagramSocketType.Portfolio, new Action<DiagramSocketValue>(this.\u0023\u003Dzh1ZZsRCwpEAT), 1, 1, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str862, DiagramSocketType.Unit, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzXp40NjoKvYHB = this.AddParam<bool>(nameof(-1260195692), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Str862, LocalizedStrings.Str1543, LocalizedStrings.Str3139, 30).SetOnValueChangedHandler<bool>(new Action<bool>(this.\u0023\u003Dz1DFoGRVGMJirY7xLCgN5OQM\u003D));
      this.\u0023\u003DzBJNEfQMDbVf8 = this.AddParam<bool>(nameof(-1260194970), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Str862, LocalizedStrings.ShowPositionSocket, LocalizedStrings.ShowPositionSocket, 40).SetOnValueChangedHandler<bool>(new Action<bool>(this.\u0023\u003DzBDoPpJYcKwFBr\u0024D5W6wRV6U\u003D));
      this.\u0023\u003DzA_hm06A_kQJ\u0024();
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

    /// <summary>Money position.</summary>
    public bool IsMoney
    {
      get
      {
        return this.\u0023\u003DzXp40NjoKvYHB.Value;
      }
      set
      {
        this.\u0023\u003DzXp40NjoKvYHB.Value = value;
      }
    }

    /// <summary>Show position socket.</summary>
    public bool ShowPosition
    {
      get
      {
        return this.\u0023\u003DzBJNEfQMDbVf8.Value;
      }
      set
      {
        this.\u0023\u003DzBJNEfQMDbVf8.Value = value;
      }
    }

    private void \u0023\u003DzNciMOiPBixCj(bool _param1)
    {
      if (this.\u0023\u003DzUiy2ZEijW\u00242h != null)
      {
        this.RemoveSocket(this.\u0023\u003DzUiy2ZEijW\u00242h);
        this.\u0023\u003DzUiy2ZEijW\u00242h = (DiagramSocket) null;
      }
      if (!_param1 && this.\u0023\u003DzXp40NjoKvYHB.Value)
        return;
      this.\u0023\u003DzUiy2ZEijW\u00242h = this.AddInput(DiagramElement.GenerateSocketId(nameof(-1260194955)), LocalizedStrings.Security, DiagramSocketType.Security, new Action<DiagramSocketValue>(this.\u0023\u003DzECIEXk\u0024A5Mms), 1, 0, false, new bool?());
    }

    private void \u0023\u003DzA_hm06A_kQJ\u0024()
    {
      if (this.\u0023\u003DzYKozG5q\u0024phYr != null)
      {
        this.RemoveSocket(this.\u0023\u003DzYKozG5q\u0024phYr);
        this.\u0023\u003DzYKozG5q\u0024phYr = (DiagramSocket) null;
      }
      if (!this.\u0023\u003DzBJNEfQMDbVf8.Value)
        return;
      this.\u0023\u003DzYKozG5q\u0024phYr = this.AddOutput(DiagramElement.GenerateSocketId(nameof(-1260195004)), LocalizedStrings.Str862, DiagramSocketType.Unit, int.MaxValue, 1, new bool?());
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      if (this.IsMoney)
      {
        DiagramStrategy strategy = this.Strategy;
        ((IPortfolioProvider) strategy).NewPortfolio += new Action<Portfolio>(this.\u0023\u003DzGOKRVck\u003D);
        ((IPortfolioProvider) strategy).PortfolioChanged += new Action<Portfolio>(this.\u0023\u003DzGOKRVck\u003D);
        ((IPortfolioProvider) strategy).Portfolios.ForEach<Portfolio>(new Action<Portfolio>(this.\u0023\u003DzGOKRVck\u003D));
      }
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnStop()
    {
      if (this.IsMoney)
      {
        DiagramStrategy strategy = this.Strategy;
        ((IPortfolioProvider) strategy).NewPortfolio -= new Action<Portfolio>(this.\u0023\u003DzGOKRVck\u003D);
        ((IPortfolioProvider) strategy).PortfolioChanged -= new Action<Portfolio>(this.\u0023\u003DzGOKRVck\u003D);
      }
      base.OnStop();
    }

    /// <inheritdoc />
    protected override void OnSubscribe(Strategy strategy, DiagramSocketValue source)
    {
      if (this.IsMoney)
        return;
      DiagramStrategy strategy1 = this.Strategy;
      ((IPositionProvider) strategy1).NewPosition += new Action<Position>(this.\u0023\u003Dzf7CsHxc\u003D);
      ((IPositionProvider) strategy1).PositionChanged += new Action<Position>(this.\u0023\u003Dzf7CsHxc\u003D);
    }

    /// <inheritdoc />
    protected override void OnUnSubscribe(Strategy strategy, DiagramSocketValue source)
    {
      if (this.IsMoney)
        return;
      DiagramStrategy strategy1 = this.Strategy;
      ((IPositionProvider) strategy1).NewPosition -= new Action<Position>(this.\u0023\u003Dzf7CsHxc\u003D);
      ((IPositionProvider) strategy1).PositionChanged -= new Action<Position>(this.\u0023\u003Dzf7CsHxc\u003D);
    }

    /// <inheritdoc />
    public override void Load(SettingsStorage storage)
    {
      base.Load(storage);
      this.\u0023\u003DzNciMOiPBixCj(false);
      this.\u0023\u003DzA_hm06A_kQJ\u0024();
    }

    private void \u0023\u003Dzf7CsHxc\u003D(Position _param1)
    {
      this.\u0023\u003Dzf7CsHxc\u003D(_param1, (DiagramSocketValue) null);
    }

    private void \u0023\u003Dzf7CsHxc\u003D(Position _param1, DiagramSocketValue _param2)
    {
      if (this.\u0023\u003DzDrO_6FqVYqxoTfK8gA\u003D\u003D != null && _param1.Portfolio != this.\u0023\u003DzDrO_6FqVYqxoTfK8gA\u003D\u003D || !this.IsMoney && _param1.Security != this.\u0023\u003DzmirAKT8\u003D)
        return;
      if (this.\u0023\u003DzYKozG5q\u0024phYr != null)
        this.RaiseProcessOutput(this.\u0023\u003DzYKozG5q\u0024phYr, _param1.LastChangeTime, (object) _param1, _param2, (Subscription) null);
      Decimal? currentValue = _param1.CurrentValue;
      if (!currentValue.HasValue)
        return;
      currentValue = _param1.CurrentValue;
      this.RaiseProcessOutput((object) (Unit) currentValue.Value, _param2, (Subscription) null);
    }

    private void \u0023\u003DzGOKRVck\u003D(Portfolio _param1)
    {
      if (_param1 != this.\u0023\u003DzDrO_6FqVYqxoTfK8gA\u003D\u003D)
        return;
      if (this.\u0023\u003DzYKozG5q\u0024phYr != null)
        this.RaiseProcessOutput(this.\u0023\u003DzYKozG5q\u0024phYr, _param1.LastChangeTime, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
      Decimal? currentValue = _param1.CurrentValue;
      if (!currentValue.HasValue)
      {
        if (this.\u0023\u003DzYKozG5q\u0024phYr == null)
          return;
        this.Strategy.Flush();
      }
      else
      {
        this.RaiseProcessOutput((object) (Unit) currentValue.Value, (DiagramSocketValue) null, (Subscription) null);
        this.Strategy.Flush();
      }
    }

    private void \u0023\u003DzECIEXk\u0024A5Mms(DiagramSocketValue _param1)
    {
      PositionDiagramElement.\u0023\u003DzlxRSxmHKc_u5nRhtprQ50zg\u003D hkcU5nRhtprQ50zg = new PositionDiagramElement.\u0023\u003DzlxRSxmHKc_u5nRhtprQ50zg\u003D();
      hkcU5nRhtprQ50zg._diagramElement = this;
      hkcU5nRhtprQ50zg._dsv = _param1;
      if (this.IsMoney)
        return;
      this.\u0023\u003DzmirAKT8\u003D = hkcU5nRhtprQ50zg._dsv.GetValue<Security>();
      if (this.\u0023\u003DzmirAKT8\u003D == null)
        return;
      Position[] array = this.Strategy.Positions.ToArray<Position>();
      if (array.Length != 0)
        ((IEnumerable<Position>) array).ForEach<Position>(new Action<Position>(hkcU5nRhtprQ50zg.\u0023\u003DzCnuuljROb6z3mv6ipw\u003D\u003D));
      else
        this.RaiseProcessOutput(hkcU5nRhtprQ50zg._dsv.Time, (object) new Unit(Decimal.Zero), hkcU5nRhtprQ50zg._dsv, (Subscription) null);
    }

    private void \u0023\u003Dzh1ZZsRCwpEAT(DiagramSocketValue _param1)
    {
      this.\u0023\u003DzDrO_6FqVYqxoTfK8gA\u003D\u003D = _param1.GetValue<Portfolio>();
    }

    private void \u0023\u003Dz1DFoGRVGMJirY7xLCgN5OQM\u003D(bool _param1)
    {
      IDiagramElementParam diagramElementParam = this.Parameters.First<IDiagramElementParam>(PositionDiagramElement.LamdaShit.\u0023\u003Dzwvfh52r5Xbj9IRrQKA\u003D\u003D ?? (PositionDiagramElement.LamdaShit.\u0023\u003Dzwvfh52r5Xbj9IRrQKA\u003D\u003D = new Func<IDiagramElementParam, bool>(PositionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzDJoxRNEPdQzsMRK6wuQLkrc\u003D)));
      this.\u0023\u003DzNciMOiPBixCj(false);
      if (_param1)
      {
        this.ShowStrategySocket = false;
        diagramElementParam.Attributes.Add((Attribute) new ReadOnlyAttribute(true));
      }
      else
        diagramElementParam.Attributes.RemoveWhere<Attribute>(PositionDiagramElement.LamdaShit.\u0023\u003DzYCppL3biYs6iJ2niiA\u003D\u003D ?? (PositionDiagramElement.LamdaShit.\u0023\u003DzYCppL3biYs6iJ2niiA\u003D\u003D = new Func<Attribute, bool>(PositionDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz00JjrmrbffUfZF55e2D5owA\u003D)));
      this.RaisePropertiesChanged();
    }

    private void \u0023\u003DzBDoPpJYcKwFBr\u0024D5W6wRV6U\u003D(bool _param1)
    {
      this.\u0023\u003DzA_hm06A_kQJ\u0024();
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly PositionDiagramElement.LamdaShit _lamdaShit = new PositionDiagramElement.LamdaShit();
      public static Func<IDiagramElementParam, bool> \u0023\u003Dzwvfh52r5Xbj9IRrQKA\u003D\u003D;
      public static Func<Attribute, bool> \u0023\u003DzYCppL3biYs6iJ2niiA\u003D\u003D;

      internal bool \u0023\u003DzDJoxRNEPdQzsMRK6wuQLkrc\u003D(IDiagramElementParam _param1)
      {
        return _param1.Name == nameof(-1260196612);
      }

      internal bool \u0023\u003Dz00JjrmrbffUfZF55e2D5owA\u003D(Attribute _param1)
      {
        return _param1 is ReadOnlyAttribute;
      }
    }

    private sealed class \u0023\u003DzlxRSxmHKc_u5nRhtprQ50zg\u003D
    {
      public PositionDiagramElement _diagramElement;
      public DiagramSocketValue _dsv;

      internal void \u0023\u003DzCnuuljROb6z3mv6ipw\u003D\u003D(Position _param1)
      {
        this._diagramElement.\u0023\u003Dzf7CsHxc\u003D(_param1, this._dsv);
      }
    }
  }
}
