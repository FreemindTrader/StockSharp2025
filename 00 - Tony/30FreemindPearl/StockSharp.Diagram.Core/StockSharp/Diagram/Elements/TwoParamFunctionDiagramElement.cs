// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.TwoParamFunctionDiagramElement
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
using System.Runtime.InteropServices;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Formula with two arguments element.</summary>
  [Obsolete]
  [DescriptionLoc("Str3122", false)]
  [CategoryLoc("Math")]
  [DisplayNameLoc("Str3121")]
  public class TwoParamFunctionDiagramElement : DiagramElement
  {
    
    private static readonly Dictionary<string, Func<Unit, Unit, Unit>> \u0023\u003DzS\u0024EOTjk\u003D = new Dictionary<string, Func<Unit, Unit, Unit>>() { { nameof(-1260197163), new Func<Unit, Unit, Unit>(TwoParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzAVoL6F9PH5b0Ofuss4x7gBw\u003D) }, { nameof(-1260197207), new Func<Unit, Unit, Unit>(TwoParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dzv2_ZAbqLnVp61Bb\u0024IVYV5lQ\u003D) }, { nameof(-1260197187), new Func<Unit, Unit, Unit>(TwoParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz33jo3qOaGP1_jp56UAlmCpI\u003D) }, { nameof(-1260197199), new Func<Unit, Unit, Unit>(TwoParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dzn6LbdwFlOz1yKfqTmrdyqpk\u003D) }, { nameof(-1260192709), new Func<Unit, Unit, Unit>(TwoParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzaBpBtxpv0gT2JXM3N4433j0\u003D) }, { nameof(-1260197227), new Func<Unit, Unit, Unit>(TwoParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzF19Okze_hM3OCJJjDq2uAbQ\u003D) }, { nameof(-1260194459), new Func<Unit, Unit, Unit>(TwoParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzjN9TjiuS6p3BslLfwzK02Xg\u003D) }, { nameof(-1260194443), new Func<Unit, Unit, Unit>(TwoParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz3T39ZRtt\u0024r9LMHAreeO_zww\u003D) }, { nameof(-1260194491), new Func<Unit, Unit, Unit>(TwoParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzvPm6yTKyh\u0024iYcnsEW4wk4Ys\u003D) } };
    
    private readonly Guid _typeId = nameof(-1260192692).To<Guid>();
    
    private readonly string _iconName = nameof(-1260197008);
    
    private readonly DiagramElementParam<string> \u0023\u003DzS5f5Jg0\u003D;
    
    private readonly DiagramSocket \u0023\u003DzHZsRghzs9YpY;
    
    private readonly DiagramSocket \u0023\u003DzsBenVf6K8O57;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.TwoParamFunctionDiagramElement" />.
    /// </summary>
    public TwoParamFunctionDiagramElement()
    {
      this.\u0023\u003DzHZsRghzs9YpY = this.AddInput(StaticSocketIds.Input, nameof(-1260197053), DiagramSocketType.Any, (Action<DiagramSocketValue>) null, 1, int.MaxValue, false, new bool?());
      this.\u0023\u003DzsBenVf6K8O57 = this.AddInput(StaticSocketIds.SecondInput, nameof(-1260192733), DiagramSocketType.Any, (Action<DiagramSocketValue>) null, 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str1738, DiagramSocketType.Any, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzS5f5Jg0\u003D = this.AddParam<string>(nameof(-1260197008), (string) null).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Str3115, LocalizedStrings.Str3115, LocalizedStrings.Str3121, 10).SetEditor<DiagramElementParam<string>>((Attribute) new ItemsSourceAttribute(typeof (TwoParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW))).SetOnValueChangedHandler<string>(new Action<string>(((DiagramElement) this).SetElementName));
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

    /// <summary>Function.</summary>
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
      if (this.\u0023\u003DzS5f5Jg0\u003D.Value == null)
        throw new InvalidOperationException(LocalizedStrings.NotInitializedParams.Put((object[]) new object[1]{ (object) LocalizedStrings.Str3115 }));
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnProcess(
      DateTimeOffset time,
      IDictionary<DiagramSocket, DiagramSocketValue> values,
      DiagramSocketValue source)
    {
      DiagramSocketValue diagramSocketValue1 = values[this.\u0023\u003DzHZsRghzs9YpY];
      DiagramSocketValue diagramSocketValue2 = values[this.\u0023\u003DzsBenVf6K8O57];
      Unit unit1 = diagramSocketValue1.GetValue<Unit>();
      Unit unit2 = diagramSocketValue2.GetValue<Unit>();
      TwoParamFunctionDiagramElement.\u0023\u003DzmRRVSL_3dBZVZrjC3S0COMk\u003D obj1;
      obj1.\u0023\u003DzTKoe0_E\u003D = TwoParamFunctionDiagramElement.\u0023\u003DzS\u0024EOTjk\u003D[this.\u0023\u003DzS5f5Jg0\u003D.Value](unit1, unit2);
      object obj2 = (object) obj1.\u0023\u003DzTKoe0_E\u003D;
      object obj3 = diagramSocketValue1.Value;
      if (obj3 is DateTimeOffset)
      {
        obj2 = (object) TwoParamFunctionDiagramElement.\u0023\u003Dqkzd9ndY00DWjDrHTGR2mpHSBJOAD4YamryycqkhGkpY\u003D((DateTimeOffset) obj3, ref obj1);
      }
      else
      {
        object obj4 = diagramSocketValue2.Value;
        if (obj4 is DateTimeOffset)
          obj2 = (object) TwoParamFunctionDiagramElement.\u0023\u003Dqkzd9ndY00DWjDrHTGR2mpHSBJOAD4YamryycqkhGkpY\u003D((DateTimeOffset) obj4, ref obj1);
        else if (diagramSocketValue1.Value is TimeSpan || diagramSocketValue2.Value is TimeSpan)
          obj2 = (object) (long) obj1.\u0023\u003DzTKoe0_E\u003D.Value.To<TimeSpan>();
      }
      this.RaiseProcessOutput(time, obj2, source, (Subscription) null);
    }

    internal static DateTimeOffset \u0023\u003Dqkzd9ndY00DWjDrHTGR2mpHSBJOAD4YamryycqkhGkpY\u003D(
      DateTimeOffset _param0,
      ref TwoParamFunctionDiagramElement.\u0023\u003DzmRRVSL_3dBZVZrjC3S0COMk\u003D _param1)
    {
      return new DateTimeOffset((long) _param1.\u0023\u003DzTKoe0_E\u003D.Value.To<DateTimeOffset>().Ticks, _param0.Offset);
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly TwoParamFunctionDiagramElement.LamdaShit _lamdaShit = new TwoParamFunctionDiagramElement.LamdaShit();

      internal Unit \u0023\u003DzAVoL6F9PH5b0Ofuss4x7gBw\u003D(Unit _param1, Unit _param2)
      {
        return _param1 + _param2;
      }

      internal Unit \u0023\u003Dzv2_ZAbqLnVp61Bb\u0024IVYV5lQ\u003D(Unit _param1, Unit _param2)
      {
        return _param1 - _param2;
      }

      internal Unit \u0023\u003Dz33jo3qOaGP1_jp56UAlmCpI\u003D(Unit _param1, Unit _param2)
      {
        return _param1 * _param2;
      }

      internal Unit \u0023\u003Dzn6LbdwFlOz1yKfqTmrdyqpk\u003D(Unit _param1, Unit _param2)
      {
        return _param1 / _param2;
      }

      internal Unit \u0023\u003DzaBpBtxpv0gT2JXM3N4433j0\u003D(Unit _param1, Unit _param2)
      {
        return (Unit) (Decimal) _param1.Pow((Decimal) _param2);
      }

      internal Unit \u0023\u003DzF19Okze_hM3OCJJjDq2uAbQ\u003D(Unit _param1, Unit _param2)
      {
        return (Unit) (Decimal) _param1.Log((Decimal) _param2);
      }

      internal Unit \u0023\u003DzjN9TjiuS6p3BslLfwzK02Xg\u003D(Unit _param1, Unit _param2)
      {
        return (Unit) (Decimal) _param1.Max((Decimal) _param2);
      }

      internal Unit \u0023\u003Dz3T39ZRtt\u0024r9LMHAreeO_zww\u003D(Unit _param1, Unit _param2)
      {
        return (Unit) (Decimal) _param1.Min((Decimal) _param2);
      }

      internal Unit \u0023\u003DzvPm6yTKyh\u0024iYcnsEW4wk4Ys\u003D(Unit _param1, Unit _param2)
      {
        return (Unit) MathHelper.Round((Decimal) _param1, (int) (Decimal) _param2);
      }
    }

    [StructLayout(LayoutKind.Auto)]
    private struct \u0023\u003DzmRRVSL_3dBZVZrjC3S0COMk\u003D
    {
      
      public Unit \u0023\u003DzTKoe0_E\u003D;
    }

    private sealed class \u0023\u003DzoSP2oQYu\u0024prW : ItemsSourceBase<string>
    {
      protected override IEnumerable<string> GetValues()
      {
        return TwoParamFunctionDiagramElement.\u0023\u003DzS\u0024EOTjk\u003D.Select<KeyValuePair<string, Func<Unit, Unit, Unit>>, string>(TwoParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit.\u0023\u003DzR\u0024W2ED4AYgT3CXTmug\u003D\u003D ?? (TwoParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit.\u0023\u003DzR\u0024W2ED4AYgT3CXTmug\u003D\u003D = new Func<KeyValuePair<string, Func<Unit, Unit, Unit>>, string>(TwoParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit._lamdaShit.\u0023\u003Dzp85CEVEP0LOfjp0\u0024QQ\u003D\u003D)));
      }

      [Serializable]
      private sealed class LamdaShit
      {
        public static readonly TwoParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit _lamdaShit = new TwoParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit();
        public static Func<KeyValuePair<string, Func<Unit, Unit, Unit>>, string> \u0023\u003DzR\u0024W2ED4AYgT3CXTmug\u003D\u003D;

        internal string \u0023\u003Dzp85CEVEP0LOfjp0\u0024QQ\u003D\u003D(
          KeyValuePair<string, Func<Unit, Unit, Unit>> _param1)
        {
          return _param1.Key;
        }
      }
    }
  }
}
