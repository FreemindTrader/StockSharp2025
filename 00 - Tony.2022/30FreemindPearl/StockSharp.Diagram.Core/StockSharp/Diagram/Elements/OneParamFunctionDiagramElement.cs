// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OneParamFunctionDiagramElement
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
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Formula with one argument element.</summary>
  [DisplayNameLoc("Str3118")]
  [Obsolete]
  [CategoryLoc("Math")]
  [DescriptionLoc("Str3119", false)]
  public class OneParamFunctionDiagramElement : DiagramElement
  {
    
    private static readonly Dictionary<string, Func<Unit, Unit>> \u0023\u003DzS\u0024EOTjk\u003D = new Dictionary<string, Func<Unit, Unit>>() { { nameof(-1260194477), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz1WyHEfgs1yYQnq6Nj3AhnyE\u003D) }, { nameof(-1260194524), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzPN5D_U\u0024uRI0xSAt1PlQ6CHg\u003D) }, { nameof(-1260194506), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzNU1\u0024xMh\u0024Pj9JXE9EHlSbC6c\u003D) }, { nameof(-1260194549), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzAw4kCy3MrrcbYUfpDoBwwpc\u003D) }, { nameof(-1260194532), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz73ngFgQELA3nghscqsTyCRY\u003D) }, { nameof(-1260194543), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzA_62gg2oA7vbDDd\u00245_PSI5w\u003D) }, { nameof(-1260194333), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzeBrSUYRDk6Ot3ADj4PkQyHs\u003D) }, { nameof(-1260194315), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dzpq5g3wXe1yZPFo0R2oP7nVc\u003D) }, { nameof(-1260194361), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz_0HuWEJ7yiC33eThhbRtTn8\u003D) }, { nameof(-1260194346), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzLO6ScqSt5KHbTnRemVmD\u0024hw\u003D) }, { nameof(-1260194393), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzcPyJeR4902TatT8vtWJkzGZrxHil) }, { nameof(-1260194379), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzRsk5D6OIXNmuqPKVrcj3Y9aIYu97) }, { nameof(-1260194426), new Func<Unit, Unit>(OneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzcBMv2kGZvIuLrZATWbQCi1PPsiYI) } };
    
    private readonly Guid _typeId = nameof(-1260194724).To<Guid>();
    
    private readonly string _iconName = nameof(-1260197008);
    
    private readonly DiagramElementParam<string> \u0023\u003DzS5f5Jg0\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OneParamFunctionDiagramElement" />.
    /// </summary>
    public OneParamFunctionDiagramElement()
    {
      this.AddInput(StaticSocketIds.Input, nameof(-1260197053), DiagramSocketType.Any, new Action<DiagramSocketValue>(this.\u0023\u003DzHMHk24A\u003D), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str1738, DiagramSocketType.Unit, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzS5f5Jg0\u003D = this.AddParam<string>(nameof(-1260197008), (string) null).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Str3115, LocalizedStrings.Str3115, LocalizedStrings.Str3120, 10).SetEditor<DiagramElementParam<string>>((Attribute) new ItemsSourceAttribute(typeof (OneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW))).SetOnValueChangedHandler<string>(new Action<string>(((DiagramElement) this).SetElementName));
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

    /// <summary>Formula with one argument element.</summary>
    public string Function
    {
      get
      {
        return this.\u0023\u003DzS5f5Jg0\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzS5f5Jg0\u003D.Value = value;
      }
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      if (this.Function == null)
        throw new InvalidOperationException(LocalizedStrings.NotInitializedParams.Put((object[]) new object[1]{ (object) LocalizedStrings.Str3115 }));
      base.OnStart();
    }

    private void \u0023\u003DzHMHk24A\u003D(DiagramSocketValue _param1)
    {
      this.RaiseProcessOutput(_param1.Time, (object) OneParamFunctionDiagramElement.\u0023\u003DzS\u0024EOTjk\u003D[this.Function](_param1.GetValue<Unit>()), _param1, (Subscription) null);
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly OneParamFunctionDiagramElement.LamdaShit _lamdaShit = new OneParamFunctionDiagramElement.LamdaShit();

      internal Unit \u0023\u003Dz1WyHEfgs1yYQnq6Nj3AhnyE\u003D(Unit _param1)
      {
        return (Unit) (Decimal) _param1.Abs();
      }

      internal Unit \u0023\u003DzPN5D_U\u0024uRI0xSAt1PlQ6CHg\u003D(Unit _param1)
      {
        return (Unit) (Decimal) _param1.Sign();
      }

      internal Unit \u0023\u003DzNU1\u0024xMh\u0024Pj9JXE9EHlSbC6c\u003D(Unit _param1)
      {
        return (Unit) Math.Cos((double) _param1);
      }

      internal Unit \u0023\u003DzAw4kCy3MrrcbYUfpDoBwwpc\u003D(Unit _param1)
      {
        return (Unit) Math.Sin((double) _param1);
      }

      internal Unit \u0023\u003Dz73ngFgQELA3nghscqsTyCRY\u003D(Unit _param1)
      {
        return (Unit) Math.Tan((double) _param1);
      }

      internal Unit \u0023\u003DzA_62gg2oA7vbDDd\u00245_PSI5w\u003D(Unit _param1)
      {
        return (Unit) Math.Acos((double) _param1);
      }

      internal Unit \u0023\u003DzeBrSUYRDk6Ot3ADj4PkQyHs\u003D(Unit _param1)
      {
        return (Unit) Math.Asin((double) _param1);
      }

      internal Unit \u0023\u003Dzpq5g3wXe1yZPFo0R2oP7nVc\u003D(Unit _param1)
      {
        return (Unit) Math.Atan((double) _param1);
      }

      internal Unit \u0023\u003Dz_0HuWEJ7yiC33eThhbRtTn8\u003D(Unit _param1)
      {
        return (Unit) Math.Floor((Decimal) _param1);
      }

      internal Unit \u0023\u003DzLO6ScqSt5KHbTnRemVmD\u0024hw\u003D(Unit _param1)
      {
        return (Unit) Math.Ceiling((Decimal) _param1);
      }

      internal Unit \u0023\u003DzcPyJeR4902TatT8vtWJkzGZrxHil(Unit _param1)
      {
        return (Unit) Math.Truncate((Decimal) _param1);
      }

      internal Unit \u0023\u003DzRsk5D6OIXNmuqPKVrcj3Y9aIYu97(Unit _param1)
      {
        return (Unit) Math.Exp((double) _param1);
      }

      internal Unit \u0023\u003DzcBMv2kGZvIuLrZATWbQCi1PPsiYI(Unit _param1)
      {
        return (Unit) Math.Sqrt((double) _param1);
      }
    }

    private sealed class \u0023\u003DzoSP2oQYu\u0024prW : ItemsSourceBase<string>
    {
      protected override IEnumerable<string> GetValues()
      {
        return OneParamFunctionDiagramElement.\u0023\u003DzS\u0024EOTjk\u003D.Select<KeyValuePair<string, Func<Unit, Unit>>, string>(OneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit.\u0023\u003DzR\u0024W2ED4AYgT3CXTmug\u003D\u003D ?? (OneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit.\u0023\u003DzR\u0024W2ED4AYgT3CXTmug\u003D\u003D = new Func<KeyValuePair<string, Func<Unit, Unit>>, string>(OneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit._lamdaShit.\u0023\u003Dzp85CEVEP0LOfjp0\u0024QQ\u003D\u003D)));
      }

      [Serializable]
      private sealed class LamdaShit
      {
        public static readonly OneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit _lamdaShit = new OneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit();
        public static Func<KeyValuePair<string, Func<Unit, Unit>>, string> \u0023\u003DzR\u0024W2ED4AYgT3CXTmug\u003D\u003D;

        internal string \u0023\u003Dzp85CEVEP0LOfjp0\u0024QQ\u003D\u003D(
          KeyValuePair<string, Func<Unit, Unit>> _param1)
        {
          return _param1.Key;
        }
      }
    }
  }
}
