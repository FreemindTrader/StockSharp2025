// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.LogicOneParamFunctionDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Logical function with one argument element.</summary>
  [CategoryLoc("Math")]
  [DescriptionLoc("Str3114", false)]
  [Obsolete]
  [DisplayNameLoc("Str3113")]
  public class LogicOneParamFunctionDiagramElement : DiagramElement
  {
    
    private static readonly Dictionary<string, Func<bool, bool>> \u0023\u003DzS\u0024EOTjk\u003D = new Dictionary<string, Func<bool, bool>>() { { nameof(-1260197029), new Func<bool, bool>(LogicOneParamFunctionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzyPi663YV3gSmfrx97Wy4tkg\u003D) } };
    
    private readonly Guid _typeId = nameof(-1260197731).To<Guid>();
    
    private readonly string _iconName = nameof(-1260197008);
    
    private readonly DiagramElementParam<string> \u0023\u003DzS5f5Jg0\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.LogicOneParamFunctionDiagramElement" />.
    /// </summary>
    public LogicOneParamFunctionDiagramElement()
    {
      this.AddInput(StaticSocketIds.Input, nameof(-1260197053), DiagramSocketType.Bool, new Action<DiagramSocketValue>(this.\u0023\u003DzHMHk24A\u003D), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str1738, DiagramSocketType.Bool, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzS5f5Jg0\u003D = this.AddParam<string>(nameof(-1260197008), (string) null).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Str3115, LocalizedStrings.Str3115, LocalizedStrings.Str3116, 10).SetEditor<DiagramElementParam<string>>((Attribute) new ItemsSourceAttribute(typeof (LogicOneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW))).SetOnValueChangedHandler<string>(new Action<string>(((DiagramElement) this).SetElementName));
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

    /// <summary>Logical function with one argument element.</summary>
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

    private void \u0023\u003DzHMHk24A\u003D(DiagramSocketValue _param1)
    {
      this.RaiseProcessOutput(_param1.Time, (object) LogicOneParamFunctionDiagramElement.\u0023\u003DzS\u0024EOTjk\u003D[this.Function](_param1.GetValue<bool>()), _param1, (Subscription) null);
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly LogicOneParamFunctionDiagramElement.LamdaShit _lamdaShit = new LogicOneParamFunctionDiagramElement.LamdaShit();

      internal bool \u0023\u003DzyPi663YV3gSmfrx97Wy4tkg\u003D(bool _param1)
      {
        return !_param1;
      }
    }

    private sealed class \u0023\u003DzoSP2oQYu\u0024prW : ItemsSourceBase<string>
    {
      protected override IEnumerable<string> GetValues()
      {
        return LogicOneParamFunctionDiagramElement.\u0023\u003DzS\u0024EOTjk\u003D.Select<KeyValuePair<string, Func<bool, bool>>, string>(LogicOneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit.\u0023\u003DzR\u0024W2ED4AYgT3CXTmug\u003D\u003D ?? (LogicOneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit.\u0023\u003DzR\u0024W2ED4AYgT3CXTmug\u003D\u003D = new Func<KeyValuePair<string, Func<bool, bool>>, string>(LogicOneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit._lamdaShit.\u0023\u003Dzp85CEVEP0LOfjp0\u0024QQ\u003D\u003D)));
      }

      [Serializable]
      private sealed class LamdaShit
      {
        public static readonly LogicOneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit _lamdaShit = new LogicOneParamFunctionDiagramElement.\u0023\u003DzoSP2oQYu\u0024prW.LamdaShit();
        public static Func<KeyValuePair<string, Func<bool, bool>>, string> \u0023\u003DzR\u0024W2ED4AYgT3CXTmug\u003D\u003D;

        internal string \u0023\u003Dzp85CEVEP0LOfjp0\u0024QQ\u003D\u003D(
          KeyValuePair<string, Func<bool, bool>> _param1)
        {
          return _param1.Key;
        }
      }
    }
  }
}
