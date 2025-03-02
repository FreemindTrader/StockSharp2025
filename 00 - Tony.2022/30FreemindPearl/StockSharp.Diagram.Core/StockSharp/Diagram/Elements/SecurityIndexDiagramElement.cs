// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.SecurityIndexDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Algo.Expressions;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// Security index based on <see cref="T:StockSharp.Algo.Expressions.ExpressionIndexSecurity" /> diagram element.
  /// </summary>
  [DisplayNameLoc("Index")]
  [DescriptionLoc("Str728", false)]
  [CategoryLoc("Sources")]
  [Doc("topics/Designer_Index.html")]
  public class SecurityIndexDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260195295).To<Guid>();
    
    private readonly string _iconName = nameof(-1260195308);
    
    private ExpressionIndexSecurity \u0023\u003Dz4D6Sb8SyOFKc;
    
    private readonly DiagramElementParam<string> \u0023\u003DzEokSU0I\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzYmeOxibGALAE;
    
    private readonly DiagramElementParam<bool> \u0023\u003Dza1iMTRTYGzwrgTntNFEVJt0\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.SecurityIndexDiagramElement" />.
    /// </summary>
    public SecurityIndexDiagramElement()
    {
      this.AddOutput(StaticSocketIds.Security, LocalizedStrings.Security, DiagramSocketType.Security, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzEokSU0I\u003D = this.AddParam<string>(nameof(-1260197387), (string) null).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Index, LocalizedStrings.Index, string.Concat(LocalizedStrings.Index, nameof(-1260196070)), 10);
      this.\u0023\u003DzYmeOxibGALAE = this.AddParam<bool>(nameof(-1260195095), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Index, LocalizedStrings.IgnoreErrors, string.Concat(LocalizedStrings.IgnoreErrors, nameof(-1260196070)), 20);
      this.\u0023\u003Dza1iMTRTYGzwrgTntNFEVJt0\u003D = this.AddParam<bool>(nameof(-1260195084), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Index, LocalizedStrings.CalculateExtended, string.Concat(LocalizedStrings.CalculateExtended, nameof(-1260196070)), 30);
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

    /// <summary>Index.</summary>
    public string Index
    {
      get
      {
        return this.\u0023\u003DzEokSU0I\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzEokSU0I\u003D.Value = value;
      }
    }

    /// <summary>Ignore calculation errors.</summary>
    public bool IgnoreErrors
    {
      get
      {
        return this.\u0023\u003DzYmeOxibGALAE.Value;
      }
      set
      {
        this.\u0023\u003DzYmeOxibGALAE.Value = value;
      }
    }

    /// <summary>Calculate extended information.</summary>
    public bool CalculateExtended
    {
      get
      {
        return this.\u0023\u003Dza1iMTRTYGzwrgTntNFEVJt0\u003D.Value;
      }
      set
      {
        this.\u0023\u003Dza1iMTRTYGzwrgTntNFEVJt0\u003D.Value = value;
      }
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      SecurityIndexDiagramElement.\u0023\u003DzmBcIfU2D1f2SdVx7CrKd0oc\u003D d1f2SdVx7CrKd0oc = new SecurityIndexDiagramElement.\u0023\u003DzmBcIfU2D1f2SdVx7CrKd0oc\u003D();
      ExpressionIndexSecurity expressionIndexSecurity = new ExpressionIndexSecurity();
      expressionIndexSecurity.Expression = this.Index;
      expressionIndexSecurity.IgnoreErrors = this.IgnoreErrors;
      expressionIndexSecurity.CalculateExtended = this.CalculateExtended;
      expressionIndexSecurity.Board = ExchangeBoard.Associated;
      this.\u0023\u003Dz4D6Sb8SyOFKc = expressionIndexSecurity;
      d1f2SdVx7CrKd0oc.\u0023\u003DzlLVgf00\u003D = this.\u0023\u003Dz4D6Sb8SyOFKc.InnerSecurityIds.Select<SecurityId, SecurityId>(SecurityIndexDiagramElement.LamdaShit.\u0023\u003DzCo7T0pqBBQWSyR3wKg\u003D\u003D ?? (SecurityIndexDiagramElement.LamdaShit.\u0023\u003DzCo7T0pqBBQWSyR3wKg\u003D\u003D = new Func<SecurityId, SecurityId>(SecurityIndexDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz\u0024jGxHgX\u00249Q3GerYUQA\u003D\u003D))).Distinct<SecurityId>();
      string[] array = this.\u0023\u003Dz4D6Sb8SyOFKc.Formula.Identifiers.Where<string>(new Func<string, bool>(d1f2SdVx7CrKd0oc.\u0023\u003DzHcVd0FDi_kxF5fgVHw\u003D\u003D)).ToArray<string>();
      if (array.Length != 0)
        throw new InvalidOperationException(LocalizedStrings.SecuritiesNotFound.Put((object[]) new object[1]{ (object) ((IEnumerable<string>) array).JoinCommaSpace() }));
      foreach (SecurityId securityId in d1f2SdVx7CrKd0oc.\u0023\u003DzlLVgf00\u003D)
      {
        DiagramStrategy strategy = this.Strategy;
        SecurityLookupMessage criteria = new SecurityLookupMessage();
        criteria.SecurityId = securityId;
        strategy.LookupSecurities(criteria);
      }
      this.RaiseProcessOutput((object) this.\u0023\u003Dz4D6Sb8SyOFKc, (DiagramSocketValue) null, (Subscription) null);
      base.OnStart();
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly SecurityIndexDiagramElement.LamdaShit _lamdaShit = new SecurityIndexDiagramElement.LamdaShit();
      public static Func<SecurityId, SecurityId> \u0023\u003DzCo7T0pqBBQWSyR3wKg\u003D\u003D;

      internal SecurityId \u0023\u003Dz\u0024jGxHgX\u00249Q3GerYUQA\u003D\u003D(
        SecurityId _param1)
      {
        return _param1;
      }
    }

    private sealed class \u0023\u003DzmBcIfU2D1f2SdVx7CrKd0oc\u003D
    {
      public IEnumerable<SecurityId> \u0023\u003DzlLVgf00\u003D;

      internal bool \u0023\u003DzHcVd0FDi_kxF5fgVHw\u003D\u003D(string _param1)
      {
        return !this.\u0023\u003DzlLVgf00\u003D.Contains<SecurityId>(_param1.ToSecurityId((SecurityIdGenerator) null));
      }
    }
  }
}
