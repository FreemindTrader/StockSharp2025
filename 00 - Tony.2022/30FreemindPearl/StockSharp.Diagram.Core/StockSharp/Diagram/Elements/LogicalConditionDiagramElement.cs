// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.LogicalConditionDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Logical condition element.</summary>
  [CategoryLoc("Common")]
  [Doc("topics/Designer_Logical_condition.html")]
  [DisplayNameLoc("LogicalCondition")]
  [DescriptionLoc("Str3108", false)]
  public class LogicalConditionDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260197076).To<Guid>();
    
    private readonly string _iconName = nameof(-1260196220);
    
    private readonly HashSet<string> \u0023\u003DzIQmUGxhx1BZxal5xdw\u003D\u003D = new HashSet<string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
    
    private readonly DiagramElementParam<LogicalConditionDiagramElement.Condition?> \u0023\u003DzqebnDAw\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.LogicalConditionDiagramElement" />.
    /// </summary>
    public LogicalConditionDiagramElement()
    {
      this.AddOutput(StaticSocketIds.Signal, LocalizedStrings.Signal, DiagramSocketType.Bool, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzqebnDAw\u003D = this.AddParam<LogicalConditionDiagramElement.Condition?>(nameof(-1260197550), new LogicalConditionDiagramElement.Condition?()).SetDisplay<DiagramElementParam<LogicalConditionDiagramElement.Condition?>>(LocalizedStrings.Str154, LocalizedStrings.Operator, LocalizedStrings.Str3112, 20).SetOnValueChangedHandler<LogicalConditionDiagramElement.Condition?>(new Action<LogicalConditionDiagramElement.Condition?>(this.\u0023\u003DzlTHsqK3bfXy2EZ8N0eQygbI\u003D));
      this.\u0023\u003DzoC_vNTAe_NJ66fDq1Q\u003D\u003D();
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

    /// <summary>Operator.</summary>
    public LogicalConditionDiagramElement.Condition? Operator
    {
      get
      {
        return this.\u0023\u003DzqebnDAw\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzqebnDAw\u003D.Value = value;
      }
    }

    /// <inheritdoc />
    public override void Save(SettingsStorage storage)
    {
      base.Save(storage);
      storage.SetValue<string[]>(nameof(-1260197117), this.InputSockets.Select<DiagramSocket, string>(LogicalConditionDiagramElement.LamdaShit.\u0023\u003Dz_DK5chQHRccr3G\u0024mEA\u003D\u003D ?? (LogicalConditionDiagramElement.LamdaShit.\u0023\u003Dz_DK5chQHRccr3G\u0024mEA\u003D\u003D = new Func<DiagramSocket, string>(LogicalConditionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzT7vypZxrWR_gEQKh7A\u003D\u003D))).ToArray<string>());
    }

    /// <inheritdoc />
    public override void Load(SettingsStorage storage)
    {
      base.Load(storage);
      this.RemoveSockets(false);
      this.\u0023\u003DzIQmUGxhx1BZxal5xdw\u003D\u003D.Clear();
      this.\u0023\u003DzIQmUGxhx1BZxal5xdw\u003D\u003D.AddRange<string>((IEnumerable<string>) storage.GetValue<string[]>(nameof(-1260197117), Array.Empty<string>()));
      this.\u0023\u003DzoC_vNTAe_NJ66fDq1Q\u003D\u003D();
    }

    private void \u0023\u003DzoC_vNTAe_NJ66fDq1Q\u003D\u003D()
    {
      bool flag = false;
      LogicalConditionDiagramElement.Condition? nullable = this.\u0023\u003DzqebnDAw\u003D.Value;
      int val1;
      int num1;
      if (nullable.HasValue)
      {
        switch (nullable.GetValueOrDefault())
        {
          case LogicalConditionDiagramElement.Condition.And:
          case LogicalConditionDiagramElement.Condition.Or:
            val1 = 2;
            num1 = int.MaxValue;
            flag = true;
            break;
          case LogicalConditionDiagramElement.Condition.Xor:
            val1 = num1 = 2;
            break;
          case LogicalConditionDiagramElement.Condition.Not:
            val1 = num1 = 1;
            break;
          default:
            throw new InvalidOperationException(this.\u0023\u003DzqebnDAw\u003D.Value.To<string>());
        }
      }
      else
        val1 = num1 = 0;
      int num2 = Math.Max(val1, this.\u0023\u003DzIQmUGxhx1BZxal5xdw\u003D\u003D.Count);
      if (this.\u0023\u003DzIQmUGxhx1BZxal5xdw\u003D\u003D.Count > 0)
        ((IEnumerable<DiagramSocket>) this.InputSockets.ToArray<DiagramSocket>()).Where<DiagramSocket>(new Func<DiagramSocket, bool>(this.\u0023\u003DzQ_KNz3aevGyzgDFT_dolSlbKoLaU)).ForEach<DiagramSocket>(new Action<DiagramSocket>(((DiagramElement) this).RemoveSocket));
      while (this.InputSockets.Count > num1)
      {
        DiagramSocket socket = this.InputSockets.FirstOrDefault<DiagramSocket>(new Func<DiagramSocket, bool>(this.\u0023\u003DzBTDRvKiIzQ\u0024cbriGu_kbcJuicOB0));
        if (socket != null)
          this.RemoveSocket(socket);
        else
          this.RemoveSocket(this.InputSockets.Last<DiagramSocket>());
      }
      if (flag && this.InputSockets.All<DiagramSocket>(new Func<DiagramSocket, bool>(this.\u0023\u003DzwxagvDFDGtTeZrfd4B5z64aa3lAS)) && num2 <= this.InputSockets.Count)
        ++num2;
      HashSet<string> source = new HashSet<string>((IEnumerable<string>) this.\u0023\u003DzIQmUGxhx1BZxal5xdw\u003D\u003D, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
      while (this.InputSockets.Count < num2)
      {
        string id;
        if (source.Count > 0)
        {
          id = source.First<string>();
          source.Remove(id);
          if (this.InputSockets.FindById(id) != null)
            continue;
        }
        else
          id = Guid.NewGuid().ToString(nameof(-1260199423));
        this.DoSomething0323(string.Format(nameof(-1260196884), (object) LocalizedStrings.Input, (object) (this.InputSockets.Count + 1)), id);
      }
    }

    /// <inheritdoc />
    protected override void OnProcess(
      DateTimeOffset time,
      IDictionary<DiagramSocket, DiagramSocketValue> values,
      DiagramSocketValue source)
    {
      bool[] array = values.Select<KeyValuePair<DiagramSocket, DiagramSocketValue>, bool>(LogicalConditionDiagramElement.LamdaShit.\u0023\u003Dz94u2wyFl3ujyxkYyiQ\u003D\u003D ?? (LogicalConditionDiagramElement.LamdaShit.\u0023\u003Dz94u2wyFl3ujyxkYyiQ\u003D\u003D = new Func<KeyValuePair<DiagramSocket, DiagramSocketValue>, bool>(LogicalConditionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzPpRJquJoa9hrcEplPg\u003D\u003D))).ToArray<bool>();
      if (array.Length != 0)
      {
        LogicalConditionDiagramElement.Condition? nullable = this.Operator;
        if (nullable.HasValue)
        {
          bool flag;
          switch (nullable.GetValueOrDefault())
          {
            case LogicalConditionDiagramElement.Condition.And:
              if (array.Length < 2)
                return;
              flag = ((IEnumerable<bool>) array).All<bool>(LogicalConditionDiagramElement.LamdaShit.\u0023\u003DzKrjLN2c0QhKO2SB3pg\u003D\u003D ?? (LogicalConditionDiagramElement.LamdaShit.\u0023\u003DzKrjLN2c0QhKO2SB3pg\u003D\u003D = new Func<bool, bool>(LogicalConditionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzZhAM3gzXGl6iymrwtQ\u003D\u003D)));
              break;
            case LogicalConditionDiagramElement.Condition.Or:
              if (array.Length < 2)
                return;
              flag = ((IEnumerable<bool>) array).Any<bool>(LogicalConditionDiagramElement.LamdaShit.\u0023\u003DzU4M0bLGhYuIHwxfSEA\u003D\u003D ?? (LogicalConditionDiagramElement.LamdaShit.\u0023\u003DzU4M0bLGhYuIHwxfSEA\u003D\u003D = new Func<bool, bool>(LogicalConditionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzSf4fUJMZ8KipmG5TLg\u003D\u003D)));
              break;
            case LogicalConditionDiagramElement.Condition.Xor:
              if (array.Length < 2)
                return;
              flag = ((IEnumerable<bool>) array).Aggregate<bool>(LogicalConditionDiagramElement.LamdaShit.\u0023\u003DzZBLNKvK3uw4VDKVegQ\u003D\u003D ?? (LogicalConditionDiagramElement.LamdaShit.\u0023\u003DzZBLNKvK3uw4VDKVegQ\u003D\u003D = new Func<bool, bool, bool>(LogicalConditionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzUspMgG\u0024N2Xw0Xjdflw\u003D\u003D)));
              break;
            case LogicalConditionDiagramElement.Condition.Not:
              flag = !array[0];
              break;
            default:
              goto label_14;
          }
          this.RaiseProcessOutput(time, (object) flag, source, (Subscription) null);
          return;
        }
label_14:
        throw new ArgumentOutOfRangeException(this.Operator.To<string>());
      }
    }

    private void DoSomething0323(string _param1, string _param2)
    {
      DiagramSocket diagramSocket = this.AddInput(_param2, _param1, DiagramSocketType.Bool, (Action<DiagramSocketValue>) null, 1, int.MaxValue, false, new bool?());
      diagramSocket.Connected += new Action<DiagramSocket, DiagramSocket>(this.\u0023\u003DzH\u0024oqHVWvzHMo);
      diagramSocket.Disconnected += new Action<DiagramSocket, DiagramSocket>(this.\u0023\u003DzK9Z\u0024XzJuY_Cy);
    }

    private void \u0023\u003DzH\u0024oqHVWvzHMo(DiagramSocket _param1, DiagramSocket _param2)
    {
      if (this.IsUndoRedoing)
        return;
      using (this.SaveUndoState((object) null))
        this.\u0023\u003DzoC_vNTAe_NJ66fDq1Q\u003D\u003D();
    }

    private void \u0023\u003DzK9Z\u0024XzJuY_Cy(DiagramSocket _param1, DiagramSocket _param2)
    {
      if (_param1.IsOutput || this.IsUndoRedoing)
        return;
      using (this.SaveUndoState((object) null))
      {
        this.\u0023\u003DzIQmUGxhx1BZxal5xdw\u003D\u003D.Remove(_param1.Id);
        this.RemoveSocket(_param1);
        this.\u0023\u003DzoC_vNTAe_NJ66fDq1Q\u003D\u003D();
      }
    }

    private void \u0023\u003DzlTHsqK3bfXy2EZ8N0eQygbI\u003D(
      LogicalConditionDiagramElement.Condition? _param1)
    {
      this.SetElementName(_param1.HasValue ? ((object) _param1.GetValueOrDefault()).GetDisplayName() : (string) null);
      this.\u0023\u003DzoC_vNTAe_NJ66fDq1Q\u003D\u003D();
    }

    private bool \u0023\u003DzQ_KNz3aevGyzgDFT_dolSlbKoLaU(DiagramSocket _param1)
    {
      if (!this.\u0023\u003DzIQmUGxhx1BZxal5xdw\u003D\u003D.Contains(_param1.Id))
        return this.GetNumConnections(_param1) == 0;
      return false;
    }

    private bool \u0023\u003DzBTDRvKiIzQ\u0024cbriGu_kbcJuicOB0(DiagramSocket _param1)
    {
      return this.GetNumConnections(_param1) == 0;
    }

    private bool \u0023\u003DzwxagvDFDGtTeZrfd4B5z64aa3lAS(DiagramSocket _param1)
    {
      return this.GetNumConnections(_param1) > 0;
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly LogicalConditionDiagramElement.LamdaShit _lamdaShit = new LogicalConditionDiagramElement.LamdaShit();
      public static Func<DiagramSocket, string> \u0023\u003Dz_DK5chQHRccr3G\u0024mEA\u003D\u003D;
      public static Func<KeyValuePair<DiagramSocket, DiagramSocketValue>, bool> \u0023\u003Dz94u2wyFl3ujyxkYyiQ\u003D\u003D;
      public static Func<bool, bool> \u0023\u003DzKrjLN2c0QhKO2SB3pg\u003D\u003D;
      public static Func<bool, bool> \u0023\u003DzU4M0bLGhYuIHwxfSEA\u003D\u003D;
      public static Func<bool, bool, bool> \u0023\u003DzZBLNKvK3uw4VDKVegQ\u003D\u003D;

      internal string \u0023\u003DzT7vypZxrWR_gEQKh7A\u003D\u003D(DiagramSocket _param1)
      {
        return _param1.Id;
      }

      internal bool \u0023\u003DzPpRJquJoa9hrcEplPg\u003D\u003D(
        KeyValuePair<DiagramSocket, DiagramSocketValue> _param1)
      {
        return _param1.Value.GetValue<bool>();
      }

      internal bool \u0023\u003DzZhAM3gzXGl6iymrwtQ\u003D\u003D(bool _param1)
      {
        return _param1;
      }

      internal bool \u0023\u003DzSf4fUJMZ8KipmG5TLg\u003D\u003D(bool _param1)
      {
        return _param1;
      }

      internal bool \u0023\u003DzUspMgG\u0024N2Xw0Xjdflw\u003D\u003D(bool _param1, bool _param2)
      {
        return _param1 ^ _param2;
      }
    }

    /// <summary>The logical condition type.</summary>
    public enum Condition
    {
      /// <summary>AND.</summary>
      [Display(Name = "Str3109", ResourceType = typeof (LocalizedStrings))] And,
      /// <summary>OR.</summary>
      [Display(Name = "Str3110", ResourceType = typeof (LocalizedStrings))] Or,
      /// <summary>Exclusive OR.</summary>
      [Display(Name = "Str3111", ResourceType = typeof (LocalizedStrings))] Xor,
      /// <summary>NOT.</summary>
      [Display(Name = "NOT", ResourceType = typeof (LocalizedStrings))] Not,
    }
  }
}
