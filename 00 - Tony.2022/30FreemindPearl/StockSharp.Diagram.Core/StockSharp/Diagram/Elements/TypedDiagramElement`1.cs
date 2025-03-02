// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.TypedDiagramElement`1
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>The diagram element with the changeable data type.</summary>
  /// <typeparam name="T">Type of element.</typeparam>
  public abstract class TypedDiagramElement<T> : DiagramElement where T : TypedDiagramElement<T>
  {
    
    private readonly DiagramElementParam<DiagramSocketType> _type;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.TypedDiagramElement`1" />.
    /// </summary>
    /// <param name="typeParamCategory">The category of the diagram element parameter.</param>
    protected TypedDiagramElement(string typeParamCategory)
    {
      TypedDiagramElement<T>.\u0023\u003DzxYUfbJVbnhPNPE45Zg\u003D\u003D yufbJvbnhPnpE45Zg = new TypedDiagramElement<T>.\u0023\u003DzxYUfbJVbnhPNPE45Zg\u003D\u003D();
      // ISSUE: explicit constructor call
      base.\u002Ector();
      yufbJvbnhPnpE45Zg._diagramElement = this;
      yufbJvbnhPnpE45Zg.\u0023\u003Dzp2zV3w_YXf8F = this.AddInput(StaticSocketIds.Input, LocalizedStrings.Input, DiagramSocketType.Any, new Action<DiagramSocketValue>(this.OnProcess), int.MaxValue, int.MaxValue, false, new bool?());
      DiagramSocket diagramSocket = this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Output, DiagramSocketType.Any, int.MaxValue, int.MaxValue, new bool?());
      this._type = this.AddParam<DiagramSocketType>(nameof(-1260198464), DiagramSocketType.Any).SetDisplay<DiagramElementParam<DiagramSocketType>>(typeParamCategory, LocalizedStrings.Type, LocalizedStrings.Str3157, 10).SetEditor<DiagramElementParam<DiagramSocketType>>((Attribute) new ItemsSourceAttribute(typeof (TypedDiagramElement<T>.\u0023\u003DzcOcMJICla_I5))).SetOnValueChangedHandler<DiagramSocketType>(new Action<DiagramSocketType>(yufbJvbnhPnpE45Zg.\u0023\u003DzFhH1jy_AOH_q\u0024zdIBw\u003D\u003D)).SetSaveLoadHandlers<DiagramSocketType>(new Func<DiagramSocketType, SettingsStorage>(TypedDiagramElement<T>.\u0023\u003DzzcVdP8E\u003D), new Func<SettingsStorage, DiagramSocketType>(TypedDiagramElement<T>.\u0023\u003Dz\u00244x87xg\u003D));
      yufbJvbnhPnpE45Zg.\u0023\u003Dzp2zV3w_YXf8F.Connected += new Action<DiagramSocket, DiagramSocket>(this.\u0023\u003DzXrreYewRUpa0);
      yufbJvbnhPnpE45Zg.\u0023\u003Dzp2zV3w_YXf8F.Disconnected += new Action<DiagramSocket, DiagramSocket>(this.\u0023\u003DzmexxOSisvjJN);
      Action<DiagramSocket, DiagramSocket> action = new Action<DiagramSocket, DiagramSocket>(this.\u0023\u003Dztfk\u0024JuuSfvjd);
      diagramSocket.Connected += action;
    }

    /// <summary>Data type.</summary>
    public DiagramSocketType Type
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

    private void \u0023\u003DzXrreYewRUpa0(DiagramSocket _param1, DiagramSocket _param2)
    {
      this.\u0023\u003DzmEb7Dl29zcwc(_param1, _param2);
      this.OnInputSocketConnected(_param1, _param2);
    }

    private void \u0023\u003DzmexxOSisvjJN(DiagramSocket _param1, DiagramSocket _param2)
    {
      this.OnInputSocketDisconnected(_param1, _param2);
    }

    private void \u0023\u003Dztfk\u0024JuuSfvjd(DiagramSocket _param1, DiagramSocket _param2)
    {
      this.\u0023\u003DzmEb7Dl29zcwc(_param1, _param2);
    }

    private void \u0023\u003DzmEb7Dl29zcwc(DiagramSocket _param1, DiagramSocket _param2)
    {
      if ((Equatable<DiagramSocketType>) this.Type != (DiagramSocketType) null && (Equatable<DiagramSocketType>) this.Type != DiagramSocketType.Any)
        return;
      if ((Equatable<DiagramSocketType>) _param1.Type != _param2.Type)
        _param1.Type = _param2.Type;
      this.Type = _param2.Type;
      this.TypeChanged();
      this.RaisePropertiesChanged();
    }

    /// <summary>To set available data types.</summary>
    /// <param name="types">Data type.</param>
    protected void SetTypes(IEnumerable<DiagramSocketType> types)
    {
      TypedDiagramElement<T>.\u0023\u003DzcOcMJICla_I5.\u0023\u003DzpTFPjkc\u003D(types);
    }

    /// <summary>The method is called when the data type is changed.</summary>
    protected virtual void TypeChanged()
    {
    }

    /// <summary>
    /// The method is called when the input socket is connected.
    /// </summary>
    /// <param name="socket">The diagram element socket.</param>
    /// <param name="source">The source diagram element socket.</param>
    protected virtual void OnInputSocketConnected(DiagramSocket socket, DiagramSocket source)
    {
    }

    /// <summary>
    /// The method is called when the input socket is disconnected.
    /// </summary>
    /// <param name="socket">The diagram element socket.</param>
    /// <param name="source">The source diagram element socket.</param>
    protected virtual void OnInputSocketDisconnected(DiagramSocket socket, DiagramSocket source)
    {
    }

    /// <summary>
    /// The method is called at the processing of the new incoming value.
    /// </summary>
    /// <param name="value">The processed value.</param>
    protected abstract void OnProcess(DiagramSocketValue value);

    /// <summary>To change the output socket type.</summary>
    protected void UpdateOutputSocketType()
    {
      DiagramSocket socket = this.OutputSockets.First<DiagramSocket>();
      if ((Equatable<DiagramSocketType>) socket.Type == this.Type)
        return;
      socket.Type = this.Type;
      this.RaiseSocketChanged(socket);
    }

    private static SettingsStorage \u0023\u003DzzcVdP8E\u003D(
      DiagramSocketType _param0)
    {
      _param0 = _param0 ?? DiagramSocketType.Any;
      SettingsStorage settingsStorage = new SettingsStorage();
      settingsStorage.SetValue<string>(nameof(-1260198464), _param0.Type.GetTypeName(false));
      return settingsStorage;
    }

    private static DiagramSocketType \u0023\u003Dz\u00244x87xg\u003D(
      SettingsStorage _param0)
    {
      TypedDiagramElement<T>.\u0023\u003Dzn7aG_Fg35uaG656yrmZ4agM\u003D fg35uaG656yrmZ4agM = new TypedDiagramElement<T>.\u0023\u003Dzn7aG_Fg35uaG656yrmZ4agM\u003D();
      fg35uaG656yrmZ4agM.\u0023\u003Dz7cIKYGY\u003D = _param0.GetValue<System.Type>(nameof(-1260198464), (System.Type) null);
      if (fg35uaG656yrmZ4agM.\u0023\u003Dz7cIKYGY\u003D == (System.Type) null)
        return DiagramSocketType.Any;
      return DiagramSocketType.AllTypes.FirstOrDefault<DiagramSocketType>(new Func<DiagramSocketType, bool>(fg35uaG656yrmZ4agM.\u0023\u003Dzc2fzXliT_ZgIwswuLg\u003D\u003D)) ?? DiagramSocketType.Any;
    }

    private sealed class \u0023\u003DzcOcMJICla_I5 : ItemsSourceBase<DiagramSocketType>
    {
      
      private static readonly List<DiagramSocketType> \u0023\u003Dz3RdZI8k\u003D = new List<DiagramSocketType>();

      public static void \u0023\u003DzpTFPjkc\u003D(IEnumerable<DiagramSocketType> _param0)
      {
        TypedDiagramElement<T>.\u0023\u003DzcOcMJICla_I5.\u0023\u003Dz3RdZI8k\u003D.Clear();
        TypedDiagramElement<T>.\u0023\u003DzcOcMJICla_I5.\u0023\u003Dz3RdZI8k\u003D.AddRange(_param0);
      }

      protected override string GetName(DiagramSocketType _param1)
      {
        return _param1.Name;
      }

      protected override IEnumerable<DiagramSocketType> GetValues()
      {
        return (IEnumerable<DiagramSocketType>) TypedDiagramElement<T>.\u0023\u003DzcOcMJICla_I5.\u0023\u003Dz3RdZI8k\u003D;
      }
    }

    private sealed class \u0023\u003Dzn7aG_Fg35uaG656yrmZ4agM\u003D
    {
      public System.Type \u0023\u003Dz7cIKYGY\u003D;

      internal bool \u0023\u003Dzc2fzXliT_ZgIwswuLg\u003D\u003D(DiagramSocketType _param1)
      {
        return _param1.Type == this.\u0023\u003Dz7cIKYGY\u003D;
      }
    }

    private sealed class \u0023\u003DzxYUfbJVbnhPNPE45Zg\u003D\u003D
    {
      public TypedDiagramElement<T> _diagramElement;
      public DiagramSocket \u0023\u003Dzp2zV3w_YXf8F;

      internal void \u0023\u003DzFhH1jy_AOH_q\u0024zdIBw\u003D\u003D(DiagramSocketType _param1)
      {
        _param1 = _param1 ?? DiagramSocketType.Any;
        this._diagramElement.SetElementName(((object) _param1).ToString());
        DiagramSocket zp2zV3wYxf8F = this.\u0023\u003Dzp2zV3w_YXf8F;
        if ((Equatable<DiagramSocketType>) _param1 == DiagramSocketType.Unit)
          zp2zV3wYxf8F.AddDiagramSocketTypes();
        else
          zp2zV3wYxf8F.Reset();
        if ((Equatable<DiagramSocketType>) zp2zV3wYxf8F.Type == _param1)
          return;
        zp2zV3wYxf8F.Type = _param1;
        this._diagramElement.RaiseSocketChanged(zp2zV3wYxf8F);
        this._diagramElement.TypeChanged();
        this._diagramElement.RaisePropertiesChanged();
      }
    }
  }
}
