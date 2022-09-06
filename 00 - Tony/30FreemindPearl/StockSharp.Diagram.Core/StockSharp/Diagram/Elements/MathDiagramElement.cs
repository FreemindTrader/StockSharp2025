// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.MathDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.ComponentModel.Expressions;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Formula with two arguments element.</summary>
  [DisplayNameLoc("Str3115")]
  [DescriptionLoc("MathFormulaDesc", false)]
  [CategoryLoc("Common")]
  [Doc("topics/Designer_Universal_formula.html")]
  public class MathDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260194408).To<Guid>();
    
    private readonly string _iconName = nameof(-1260197008);
    
    private ExpressionFormula \u0023\u003DzIhhzL\u0024KF4OCeq2_ArA\u003D\u003D;
    
    private readonly DiagramElementParam<string> \u0023\u003DzsSnDDdM\u003D;
    
    private readonly DiagramSocket \u0023\u003DzLDhqhrI\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.MathDiagramElement" />.
    /// </summary>
    public MathDiagramElement()
    {
      this.\u0023\u003DzLDhqhrI\u003D = this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str1738, DiagramSocketType.Any, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzsSnDDdM\u003D = this.AddParam<string>(nameof(-1260194737), (string) null).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Str3115, LocalizedStrings.Str3115, LocalizedStrings.MathFormulaDesc, 10).SetEditor<DiagramElementParam<string>>((Attribute) new ItemsSourceAttribute(typeof (MathDiagramElement.\u0023\u003DzJ6DaDRM\u003D))
      {
        IsEditable = true
      }).SetOnValueChangedHandler<string>(new Action<string>(this.OnDiagramElementsAdded));
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

    /// <summary>Expression.</summary>
    public string Expression
    {
      get
      {
        return this.\u0023\u003DzsSnDDdM\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzsSnDDdM\u003D.Value = value;
      }
    }

    private void \u0023\u003Dzjvmg9\u0024qRBeo9(DiagramSocket _param1, DiagramSocket _param2)
    {
      if ((Equatable<DiagramSocketType>) _param1.Type == _param2.Type)
        return;
      _param1.Type = _param2.Type;
      DiagramSocketType diagramSocketType = !this.InputSockets.Any<DiagramSocket>(MathDiagramElement.LamdaShit.\u0023\u003DzVuKaiaMoOJXJKoI_yw\u003D\u003D ?? (MathDiagramElement.LamdaShit.\u0023\u003DzVuKaiaMoOJXJKoI_yw\u003D\u003D = new Func<DiagramSocket, bool>(MathDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzVykAvmn9QkXF9z2rRrrjEIg\u003D))) ? (!this.InputSockets.Any<DiagramSocket>(MathDiagramElement.LamdaShit.\u0023\u003Dzo1GraqqmgVzeQx7h9w\u003D\u003D ?? (MathDiagramElement.LamdaShit.\u0023\u003Dzo1GraqqmgVzeQx7h9w\u003D\u003D = new Func<DiagramSocket, bool>(MathDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzCMOWu1J8iKofnAIKE0UwLww\u003D))) ? DiagramSocketType.Unit : DiagramSocketType.Time) : DiagramSocketType.Date;
      if (!((Equatable<DiagramSocketType>) this.\u0023\u003DzLDhqhrI\u003D.Type != diagramSocketType))
        return;
      this.\u0023\u003DzLDhqhrI\u003D.Type = diagramSocketType;
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      if (this.\u0023\u003DzIhhzL\u0024KF4OCeq2_ArA\u003D\u003D == null)
        throw new InvalidOperationException(LocalizedStrings.NotInitializedParams.Put((object[]) new object[1]{ (object) LocalizedStrings.Str3115 }));
      if (!this.\u0023\u003DzIhhzL\u0024KF4OCeq2_ArA\u003D\u003D.Error.IsEmpty())
        throw new InvalidOperationException(this.\u0023\u003DzIhhzL\u0024KF4OCeq2_ArA\u003D\u003D.Error);
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnProcess(
      DateTimeOffset time,
      IDictionary<DiagramSocket, DiagramSocketValue> values,
      DiagramSocketValue source)
    {
      Decimal num = this.\u0023\u003DzIhhzL\u0024KF4OCeq2_ArA\u003D\u003D.Calculate(this.\u0023\u003DzIhhzL\u0024KF4OCeq2_ArA\u003D\u003D.Identifiers.Select<string, Decimal>(new Func<string, Decimal>(new MathDiagramElement.\u0023\u003DzmRRVSL_3dBZVZrjC3S0COMk\u003D() { \u0023\u003DzGOiSdFVikGo9 = values.ToDictionary<KeyValuePair<DiagramSocket, DiagramSocketValue>, string, Decimal>(MathDiagramElement.LamdaShit.\u0023\u003DzjoktXev7b9_FCwjetg\u003D\u003D ?? (MathDiagramElement.LamdaShit.\u0023\u003DzjoktXev7b9_FCwjetg\u003D\u003D = new Func<KeyValuePair<DiagramSocket, DiagramSocketValue>, string>(MathDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz8DnjaQy9jKwmezcE\u0024w\u003D\u003D)), MathDiagramElement.LamdaShit.\u0023\u003Dzhlsj6tjm7aKTmu6QBA\u003D\u003D ?? (MathDiagramElement.LamdaShit.\u0023\u003Dzhlsj6tjm7aKTmu6QBA\u003D\u003D = new Func<KeyValuePair<DiagramSocket, DiagramSocketValue>, Decimal>(MathDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzWwQaFdco8N7f1PZ5\u0024A\u003D\u003D))) }.\u0023\u003DzcXFqjg\u0024g_K\u0024qMuJ88A\u003D\u003D)).ToArray<Decimal>());
      if ((Equatable<DiagramSocketType>) this.\u0023\u003DzLDhqhrI\u003D.Type == DiagramSocketType.Date)
      {
        DateTimeOffset dateTimeOffset = values.Values.Select<DiagramSocketValue, object>(MathDiagramElement.LamdaShit.\u0023\u003DzKX6BLOhjpx7h8jUWvA\u003D\u003D ?? (MathDiagramElement.LamdaShit.\u0023\u003DzKX6BLOhjpx7h8jUWvA\u003D\u003D = new Func<DiagramSocketValue, object>(MathDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzLQjA0z6xztlO_yoMDA\u003D\u003D))).OfType<DateTimeOffset>().FirstOrDefault<DateTimeOffset>();
        TimeSpan offset = dateTimeOffset.IsDefault<DateTimeOffset>() ? TimeSpan.Zero : dateTimeOffset.Offset;
        this.RaiseProcessOutput(time, (object) new DateTimeOffset(num.To<long>(), TimeSpan.Zero).ToOffset(offset), source, (Subscription) null);
      }
      else if ((Equatable<DiagramSocketType>) this.\u0023\u003DzLDhqhrI\u003D.Type == DiagramSocketType.Time)
        this.RaiseProcessOutput(time, (object) new TimeSpan(num.To<long>()), source, (Subscription) null);
      else
        this.RaiseProcessOutput(time, (object) num, source, (Subscription) null);
    }

    private void OnDiagramElementsAdded(string _param1)
    {
      MathDiagramElement.\u0023\u003Dz6jf84YmjB5QXiZQFSdqRLa0\u003D ymjB5QxiZqfSdqRla0 = new MathDiagramElement.\u0023\u003Dz6jf84YmjB5QXiZQFSdqRLa0\u003D();
      this.SetElementName(_param1);
      this.\u0023\u003DzIhhzL\u0024KF4OCeq2_ArA\u003D\u003D = (ExpressionFormula) null;
      if (_param1 == null)
      {
        foreach (DiagramSocket socket in this.InputSockets.ToArray<DiagramSocket>())
        {
          socket.Connected -= new Action<DiagramSocket, DiagramSocket>(this.\u0023\u003Dzjvmg9\u0024qRBeo9);
          this.RemoveSocket(socket);
        }
      }
      else
      {
        this.\u0023\u003DzIhhzL\u0024KF4OCeq2_ArA\u003D\u003D = _param1.Compile(false);
        if (!this.\u0023\u003DzIhhzL\u0024KF4OCeq2_ArA\u003D\u003D.Error.IsEmpty())
          return;
        ymjB5QxiZqfSdqRla0.\u0023\u003Dz_0Z2Khqig_It = new List<string>();
        foreach (string identifier in this.\u0023\u003DzIhhzL\u0024KF4OCeq2_ArA\u003D\u003D.Identifiers)
        {
          MathDiagramElement.\u0023\u003DzAdKHvLJVYAicMiLWYUUKKKM\u003D ljvyAicMiLwyuukkkm = new MathDiagramElement.\u0023\u003DzAdKHvLJVYAicMiLWYUUKKKM\u003D();
          ljvyAicMiLwyuukkkm.\u0023\u003DzUZnu2cE\u003D = DiagramElement.GenerateSocketId(identifier);
          ymjB5QxiZqfSdqRla0.\u0023\u003Dz_0Z2Khqig_It.Add(ljvyAicMiLwyuukkkm.\u0023\u003DzUZnu2cE\u003D);
          if (this.InputSockets.FirstOrDefault<DiagramSocket>(new Func<DiagramSocket, bool>(ljvyAicMiLwyuukkkm.\u0023\u003Dzbpd38YmorB1leJi82A\u003D\u003D)) == null)
          {
            DiagramSocket diagramSocket = this.AddInput(ljvyAicMiLwyuukkkm.\u0023\u003DzUZnu2cE\u003D, identifier, DiagramSocketType.Any, (Action<DiagramSocketValue>) null, 1, int.MaxValue, false, new bool?());
            diagramSocket.AddDiagramSocketTypes();
            diagramSocket.AvailableTypes.Add(DiagramSocketType.Date);
            diagramSocket.AvailableTypes.Add(DiagramSocketType.Time);
            diagramSocket.Connected += new Action<DiagramSocket, DiagramSocket>(this.\u0023\u003Dzjvmg9\u0024qRBeo9);
          }
        }
        ((IEnumerable<DiagramSocket>) this.InputSockets.Where<DiagramSocket>(new Func<DiagramSocket, bool>(ymjB5QxiZqfSdqRla0.\u0023\u003DzMaKRJ7RtJ2KLu0Htug\u003D\u003D)).ToArray<DiagramSocket>()).ForEach<DiagramSocket>(new Action<DiagramSocket>(((DiagramElement) this).RemoveSocket));
      }
    }

    private sealed class \u0023\u003Dz6jf84YmjB5QXiZQFSdqRLa0\u003D
    {
      public List<string> \u0023\u003Dz_0Z2Khqig_It;

      internal bool \u0023\u003DzMaKRJ7RtJ2KLu0Htug\u003D\u003D(DiagramSocket _param1)
      {
        return !this.\u0023\u003Dz_0Z2Khqig_It.Contains(_param1.Id);
      }
    }

    private sealed class \u0023\u003DzAdKHvLJVYAicMiLWYUUKKKM\u003D
    {
      public string \u0023\u003DzUZnu2cE\u003D;

      internal bool \u0023\u003Dzbpd38YmorB1leJi82A\u003D\u003D(DiagramSocket _param1)
      {
        return _param1.Id == this.\u0023\u003DzUZnu2cE\u003D;
      }
    }

    private sealed class \u0023\u003DzJ6DaDRM\u003D : ItemsSourceBase<string>
    {
      
      private static readonly HashSet<string> \u0023\u003DzS\u0024EOTjk\u003D = new HashSet<string>() { nameof(-1260197163), nameof(-1260197207), nameof(-1260197187), nameof(-1260197199), nameof(-1260197243), nameof(-1260197227), nameof(-1260194459), nameof(-1260194443), nameof(-1260194491), nameof(-1260194477), nameof(-1260194524), nameof(-1260194506), nameof(-1260194549), nameof(-1260194532), nameof(-1260194543), nameof(-1260194333), nameof(-1260194315), nameof(-1260194361), nameof(-1260194346), nameof(-1260194393), nameof(-1260194379), nameof(-1260194426) };

      protected override IEnumerable<string> GetValues()
      {
        return (IEnumerable<string>) MathDiagramElement.\u0023\u003DzJ6DaDRM\u003D.\u0023\u003DzS\u0024EOTjk\u003D;
      }
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly MathDiagramElement.LamdaShit _lamdaShit = new MathDiagramElement.LamdaShit();
      public static Func<DiagramSocket, bool> \u0023\u003DzVuKaiaMoOJXJKoI_yw\u003D\u003D;
      public static Func<DiagramSocket, bool> \u0023\u003Dzo1GraqqmgVzeQx7h9w\u003D\u003D;
      public static Func<KeyValuePair<DiagramSocket, DiagramSocketValue>, string> \u0023\u003DzjoktXev7b9_FCwjetg\u003D\u003D;
      public static Func<KeyValuePair<DiagramSocket, DiagramSocketValue>, Decimal> \u0023\u003Dzhlsj6tjm7aKTmu6QBA\u003D\u003D;
      public static Func<DiagramSocketValue, object> \u0023\u003DzKX6BLOhjpx7h8jUWvA\u003D\u003D;

      internal bool \u0023\u003DzVykAvmn9QkXF9z2rRrrjEIg\u003D(DiagramSocket _param1)
      {
        return (Equatable<DiagramSocketType>) _param1.Type == DiagramSocketType.Date;
      }

      internal bool \u0023\u003DzCMOWu1J8iKofnAIKE0UwLww\u003D(DiagramSocket _param1)
      {
        return (Equatable<DiagramSocketType>) _param1.Type == DiagramSocketType.Time;
      }

      internal string \u0023\u003Dz8DnjaQy9jKwmezcE\u0024w\u003D\u003D(
        KeyValuePair<DiagramSocket, DiagramSocketValue> _param1)
      {
        return _param1.Key.Name;
      }

      internal Decimal \u0023\u003DzWwQaFdco8N7f1PZ5\u0024A\u003D\u003D(
        KeyValuePair<DiagramSocket, DiagramSocketValue> _param1)
      {
        return _param1.Value.GetValue<Decimal>();
      }

      internal object \u0023\u003DzLQjA0z6xztlO_yoMDA\u003D\u003D(DiagramSocketValue _param1)
      {
        return _param1.Value;
      }
    }

    private sealed class \u0023\u003DzmRRVSL_3dBZVZrjC3S0COMk\u003D
    {
      public Dictionary<string, Decimal> \u0023\u003DzGOiSdFVikGo9;

      internal Decimal \u0023\u003DzcXFqjg\u0024g_K\u0024qMuJ88A\u003D\u003D(string _param1)
      {
        Decimal num;
        if (this.\u0023\u003DzGOiSdFVikGo9.TryGetValue(_param1, out num))
          return num;
        throw new InvalidOperationException(LocalizedStrings.ValueForWasNotPassed.Put((object[]) new object[1]{ (object) _param1 }));
      }
    }
  }
}
