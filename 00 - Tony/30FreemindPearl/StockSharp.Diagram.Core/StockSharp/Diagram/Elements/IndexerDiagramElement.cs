// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.IndexerDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Localization;
using System;
using System.Collections;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>The element of collection or dictionary.</summary>
  [DisplayNameLoc("Indexer")]
  [CategoryLoc("Converter")]
  [Doc("topics/Designer_Indexer.html")]
  [DescriptionLoc("CollectionOrDictElem", false)]
  public class IndexerDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260197582).To<Guid>();
    
    private readonly string _iconName = nameof(-1260197399);
    
    private readonly DiagramElementParam<string> \u0023\u003DzEokSU0I\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.IndexerDiagramElement" />.
    /// </summary>
    public IndexerDiagramElement()
    {
      this.AddInput(StaticSocketIds.Input, LocalizedStrings.Input, DiagramSocketType.Any, new Action<DiagramSocketValue>(this.\u0023\u003DzHMHk24A\u003D), 1, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str3099, DiagramSocketType.Any, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzEokSU0I\u003D = this.AddParam<string>(nameof(-1260197387), nameof(-1260197431)).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Str3131, LocalizedStrings.Index, LocalizedStrings.IndexValue, 20).SetOnValueChangedHandler<string>(new Action<string>(this.\u0023\u003DzoJ2_obkV8LapK2wTh2g_qJI\u003D));
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

    private void \u0023\u003DzHMHk24A\u003D(DiagramSocketValue _param1)
    {
      IList list = _param1.Value as IList;
      if (list != null)
      {
        int index = this.Index.To<int>();
        if (index >= 0 && index < list.Count)
          this.RaiseProcessOutput(_param1.Time, list[index], _param1, (Subscription) null);
        else
          this.RaiseProcessOutput(_param1.Time, (object) null, _param1, (Subscription) null);
      }
      else
      {
        IDictionary dictionary = _param1.Value as IDictionary;
        if (dictionary == null)
          return;
        object index = (object) this.Index;
        Type type = ((object) dictionary).GetType();
        if (type.IsGenericType)
        {
          Type[] genericArguments = type.GetGenericArguments();
          index = index.To(genericArguments[0]);
        }
        this.RaiseProcessOutput(_param1.Time, dictionary.Contains(index) ? dictionary[index] : (object) null, _param1, (Subscription) null);
      }
    }

    private void \u0023\u003DzoJ2_obkV8LapK2wTh2g_qJI\u003D(string _param1)
    {
      this.SetElementName(string.Concat(LocalizedStrings.Input, nameof(-1260197439), _param1, nameof(-1260197415)));
    }
  }
}
