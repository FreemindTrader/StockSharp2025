// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.VariableDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Value storage element.</summary>
  [DisplayNameLoc("Str3159")]
  [DescriptionLoc("Str3160", false)]
  [CategoryLoc("Sources")]
  [Doc("topics/Designer_Variable.html")]
  public class VariableDiagramElement : TypedDiagramElement<VariableDiagramElement>
  {
    
    private readonly HashSet<DiagramSocket> \u0023\u003DzoZYamzwTSxvB = new HashSet<DiagramSocket>();
    
    private readonly HashSet<DiagramSocket> \u0023\u003Dzo_Mf_Or7eqNc = new HashSet<DiagramSocket>();
    
    private readonly Guid _typeId = nameof(-1260192753).To<Guid>();
    
    private readonly string _iconName = nameof(-1260192542);
    
    private bool \u0023\u003DzCPJmzzXg4AKX;
    
    private bool \u0023\u003Dz7jiJtePQYbum;
    
    private object \u0023\u003DzWt_nGa8Qvvu_;
    
    private readonly VariableDiagramElement.\u0023\u003DzLeRkRK4\u003D _value;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzAZID5fI5DLzW;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.VariableDiagramElement" />.
    /// </summary>
    public VariableDiagramElement()
      : base(LocalizedStrings.Str3159)
    {
      DiagramSocket diagramSocket = this.AddInput(StaticSocketIds.Trigger, LocalizedStrings.Trigger, DiagramSocketType.Any, new Action<DiagramSocketValue>(this.\u0023\u003DzGjIGpXOf0Oqj), int.MaxValue, int.MaxValue, false, new bool?());
      diagramSocket.Connected += new Action<DiagramSocket, DiagramSocket>(this.\u0023\u003DzGX_82QwWW8vE);
      diagramSocket.Disconnected += new Action<DiagramSocket, DiagramSocket>(this.\u0023\u003Dzq1USSTSKfMZN);
      VariableDiagramElement.\u0023\u003DzLeRkRK4\u003D zLeRkRk4 = new VariableDiagramElement.\u0023\u003DzLeRkRK4\u003D();
      zLeRkRk4.Category = LocalizedStrings.Str3159;
      zLeRkRk4.DisplayName = LocalizedStrings.Str3099;
      zLeRkRk4.Description = LocalizedStrings.Str3161;
      zLeRkRk4.Name = nameof(-1260198550);
      zLeRkRk4.\u0023\u003DzDxNTGJc\u003D(typeof (object));
      zLeRkRk4.Attributes.Add((Attribute) new DisplayAttribute()
      {
        Order = 10
      });
      this._value = zLeRkRk4;
      this._value.\u0023\u003DzbkxfZTvrISYM(new Action(this.\u0023\u003Dz64JtKCo\u003D));
      this.AddParam((IDiagramElementParam) this._value);
      this.SetTypes(DiagramSocketType.AllTypes);
      this.\u0023\u003DzAZID5fI5DLzW = this.AddParam<bool>(nameof(-1260192517), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Str3159, LocalizedStrings.InputAsTrigger, LocalizedStrings.InputAsTriggerDesc, 20);
      this.ShowParameters = true;
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

    /// <summary>The variable value.</summary>
    public object Value
    {
      get
      {
        return this._value.Value;
      }
      set
      {
        this._value.Value = value;
      }
    }

    /// <summary>Raise output value when input updated.</summary>
    public bool InputAsTrigger
    {
      get
      {
        return this.\u0023\u003DzAZID5fI5DLzW.Value;
      }
      set
      {
        this.\u0023\u003DzAZID5fI5DLzW.Value = value;
      }
    }

    private void \u0023\u003Dz64JtKCo\u003D()
    {
      if (this.\u0023\u003Dz7jiJtePQYbum || this._value.IgnoreOnSave)
        return;
      object obj = this.Value;
      if (obj is DateTime)
        this.SetElementName(((DateTime) obj).ToString(nameof(-1260192572)));
      else if (obj is DateTimeOffset)
        this.SetElementName(((DateTimeOffset) obj).ToString(nameof(-1260192572)));
      else
        this.SetElementName(((object) this.Value)?.ToString() ?? ((object) this.Type)?.ToString());
    }

    /// <inheritdoc />
    protected override void TypeChanged()
    {
      this.UpdateOutputSocketType();
      Type type = this.Type.Type;
      if (type.IsValueType)
        type = typeof (Nullable<>).Make(type);
      this._value.\u0023\u003DzDxNTGJc\u003D(type);
      this.Value = (object) null;
      if ((Equatable<DiagramSocketType>) this.Type == DiagramSocketType.Security)
        this.ShowParameters = true;
      else if ((Equatable<DiagramSocketType>) this.Type == DiagramSocketType.Portfolio)
        this.ShowParameters = true;
      if (this.Type.IsEditable())
        this._value.Attributes.RemoveWhere<Attribute>(VariableDiagramElement.LamdaShit.\u0023\u003DzRG8MF4pL2MT_Mjdt2g\u003D\u003D ?? (VariableDiagramElement.LamdaShit.\u0023\u003DzRG8MF4pL2MT_Mjdt2g\u003D\u003D = new Func<Attribute, bool>(VariableDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz9EhnRv9fdX7o56IXog\u003D\u003D)));
      else
        this._value.Attributes.Add((Attribute) new BrowsableAttribute(false));
    }

    /// <inheritdoc />
    protected override void OnInputSocketConnected(DiagramSocket socket, DiagramSocket source)
    {
      this.ShowParameters = false;
      if (socket.IsOutput)
        return;
      this.\u0023\u003Dzo_Mf_Or7eqNc.Add(source);
    }

    /// <inheritdoc />
    protected override void OnInputSocketDisconnected(DiagramSocket socket, DiagramSocket source)
    {
      this.ShowParameters = true;
      if (socket.IsOutput || source == null)
        return;
      this.\u0023\u003Dzo_Mf_Or7eqNc.Remove(source);
    }

    /// <inheritdoc />
    protected override void OnReseted()
    {
      this.\u0023\u003DzWt_nGa8Qvvu_ = (object) null;
      this.\u0023\u003DzCPJmzzXg4AKX = false;
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      base.OnStart();
      this.\u0023\u003Dz7jiJtePQYbum = true;
      if (this.\u0023\u003DzCPJmzzXg4AKX)
        return;
      this.\u0023\u003DzWt_nGa8Qvvu_ = this.Value;
      if (this.\u0023\u003DzWt_nGa8Qvvu_ == null)
      {
        if ((Equatable<DiagramSocketType>) this.Type == DiagramSocketType.Connector)
        {
          this.\u0023\u003DzWt_nGa8Qvvu_ = (object) this.Connector;
        }
        else
        {
          if (!((Equatable<DiagramSocketType>) this.Type == DiagramSocketType.Strategy))
            return;
          this.\u0023\u003DzWt_nGa8Qvvu_ = (object) this.Strategy;
        }
      }
      else
      {
        if ((Equatable<DiagramSocketType>) this.Type == DiagramSocketType.Security)
        {
          Security zWtNGa8Qvvu = (Security) this.\u0023\u003DzWt_nGa8Qvvu_;
          if (zWtNGa8Qvvu == null)
            throw new InvalidOperationException(LocalizedStrings.Str1380);
          this.Strategy.LookupSecurities(zWtNGa8Qvvu.ToLookupMessage());
        }
        else if ((Equatable<DiagramSocketType>) this.Type == DiagramSocketType.Portfolio && (Portfolio) this.\u0023\u003DzWt_nGa8Qvvu_ == null)
          throw new InvalidOperationException(LocalizedStrings.Str1381);
        if (this.NeedFlush != 0)
          return;
        this.RaiseProcessOutput(this.\u0023\u003DzWt_nGa8Qvvu_, (DiagramSocketValue) null, (Subscription) null);
      }
    }

    /// <inheritdoc />
    protected override void OnStop()
    {
      this.\u0023\u003Dz7jiJtePQYbum = false;
      base.OnStop();
    }

    /// <inheritdoc />
    protected override void OnProcess(DiagramSocketValue value)
    {
      this.\u0023\u003DzCPJmzzXg4AKX = true;
      this.\u0023\u003DzWt_nGa8Qvvu_ = value.Value;
      if (!this.InputAsTrigger)
        return;
      this.RaiseProcessOutput(value.Time, this.\u0023\u003DzWt_nGa8Qvvu_, value, (Subscription) null);
    }

    /// <inheritdoc />
    public override int NeedFlush
    {
      get
      {
        return this.\u0023\u003DzWt_nGa8Qvvu_ == null || !this.\u0023\u003DzoZYamzwTSxvB.IsEmpty<DiagramSocket>() || !this.\u0023\u003Dzo_Mf_Or7eqNc.IsEmpty<DiagramSocket>() ? -1 : 0;
      }
    }

    /// <inheritdoc />
    public override void Flush()
    {
      this.RaiseProcessOutput(this.Strategy.CurrentTime, this.\u0023\u003DzWt_nGa8Qvvu_, (DiagramSocketValue) null, (Subscription) null);
    }

    private void \u0023\u003DzGjIGpXOf0Oqj(DiagramSocketValue _param1)
    {
      if (!this.\u0023\u003DzCPJmzzXg4AKX && this.\u0023\u003DzWt_nGa8Qvvu_ == null)
        return;
      object obj = _param1.Value;
      if (obj is bool && !(bool) obj)
        return;
      this.RaiseProcessOutput(_param1.Time, this.\u0023\u003DzWt_nGa8Qvvu_, _param1, (Subscription) null);
    }

    private void \u0023\u003DzGX_82QwWW8vE(DiagramSocket _param1, DiagramSocket _param2)
    {
      if (_param1.IsOutput)
        return;
      this.\u0023\u003DzoZYamzwTSxvB.Add(_param2);
    }

    private void \u0023\u003Dzq1USSTSKfMZN(DiagramSocket _param1, DiagramSocket _param2)
    {
      if (_param1.IsOutput || _param2 == null)
        return;
      this.\u0023\u003DzoZYamzwTSxvB.Remove(_param2);
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly VariableDiagramElement.LamdaShit _lamdaShit = new VariableDiagramElement.LamdaShit();
      public static Func<Attribute, bool> \u0023\u003DzRG8MF4pL2MT_Mjdt2g\u003D\u003D;

      internal bool \u0023\u003Dz9EhnRv9fdX7o56IXog\u003D\u003D(Attribute _param1)
      {
        return _param1 is BrowsableAttribute;
      }
    }

    private sealed class \u0023\u003DzLeRkRK4\u003D : NotifiableObject, IDiagramElementParam, IPersistable, INotifyPropertyChanging, INotifyPropertyChanged
    {
      
      private readonly List<Attribute> _attributesList = new List<Attribute>();
      
      private string \u0023\u003DzGEMIaGl7fNMN;
      
      private bool \u0023\u003Dzlt70XM6Bpo90oSUkwh9WK\u0024I\u003D;
      
      private bool \u0023\u003DzhX3sdPAmQeLT;
      
      private bool _isDefault;
      
      private string _name;
      
      private string \u0023\u003DzoywCOdVHX0X2;
      
      private string _description;
      
      private string _category;
      
      private object _value;
      
      private Type _type;
      
      private bool _bIgnoreOnSave;
      
      private Action \u0023\u003DzFADTPL8\u003D;

      public string Name
      {
        get
        {
          return this._name;
        }
        set
        {
          this._name = value;
          this.NotifyChanged(nameof(-1260199487));
        }
      }

      public string DisplayName
      {
        get
        {
          return this.\u0023\u003DzoywCOdVHX0X2;
        }
        set
        {
          this.\u0023\u003DzoywCOdVHX0X2 = value;
          this.NotifyChanged(nameof(-1260199334));
        }
      }

      public string Description
      {
        get
        {
          return this._description;
        }
        set
        {
          this._description = value;
          this.NotifyChanged(nameof(-1260198608));
        }
      }

      public string Category
      {
        get
        {
          return this._category;
        }
        set
        {
          this._category = value;
          this.NotifyChanged(nameof(-1260198623));
        }
      }

      public object Value
      {
        get
        {
          return this._value;
        }
        set
        {
          if (!this.\u0023\u003DzhX3sdPAmQeLT)
            this.NotifyChanging(nameof(-1260198550));
          this._value = value;
          this._isDefault = value != null;
          Action zFadtpL8 = this.\u0023\u003DzFADTPL8\u003D;
          if (zFadtpL8 != null)
            zFadtpL8();
          if (this.\u0023\u003DzhX3sdPAmQeLT)
            return;
          this.NotifyChanged(nameof(-1260198550));
        }
      }

      public Type Type
      {
        get
        {
          return this._type;
        }
      }

      public void \u0023\u003DzDxNTGJc\u003D(Type _param1)
      {
        this._type = _param1;
        this.NotifyChanged(nameof(-1260198464));
      }

      public IList<Attribute> Attributes
      {
        get
        {
          return (IList<Attribute>) this._attributesList;
        }
      }

      public bool IsDefault
      {
        get
        {
          return !this._isDefault;
        }
      }

      public bool IsParam
      {
        get
        {
          return true;
        }
        set
        {
          throw new NotSupportedException();
        }
      }

      public bool IgnoreOnSave
      {
        get
        {
          return this._bIgnoreOnSave;
        }
        set
        {
          this._bIgnoreOnSave = value;
        }
      }

      bool IDiagramElementParam.\u0023\u003DzS9q7ZOPomDyI6vCxJXhIj8CXIPqEx_1QSw\u003D\u003D()
      {
        throw new NotSupportedException();
      }

      void IDiagramElementParam.\u0023\u003DzXT2NjMJ3b2V5WV63HCubNiI60BVst7ObPQ\u003D\u003D(
        bool _param1)
      {
        throw new NotSupportedException();
      }

      public void \u0023\u003DzbkxfZTvrISYM(Action _param1)
      {
        Action action = this.\u0023\u003DzFADTPL8\u003D;
        Action comparand;
        do
        {
          comparand = action;
          action = Interlocked.CompareExchange<Action>(ref this.\u0023\u003DzFADTPL8\u003D, comparand + _param1, comparand);
        }
        while (action != comparand);
      }

      public void \u0023\u003DzUrAlQxz7uWAn(Action _param1)
      {
        Action action = this.\u0023\u003DzFADTPL8\u003D;
        Action comparand;
        do
        {
          comparand = action;
          action = Interlocked.CompareExchange<Action>(ref this.\u0023\u003DzFADTPL8\u003D, comparand - _param1, comparand);
        }
        while (action != comparand);
      }

      public void SetValueWithIgnoreOnSave(object _param1)
      {
        this.IgnoreOnSave = true;
        this.Value = _param1;
      }

      public void Load(SettingsStorage _param1)
      {
        try
        {
          if (this.Type == typeof (Strategy) || this.Type == typeof (IConnector))
            return;
          if (this.Type == typeof (Security))
            this.\u0023\u003DzDvx4D6rowesQ(_param1.GetValue<string>(nameof(-1260198550), (string) null));
          else if (this.Type == typeof (Portfolio))
            this.\u0023\u003DzzPBWbYHpzM4t(_param1.GetValue<string>(nameof(-1260198550), (string) null));
          else if (!this.Type.IsPersistable())
          {
            object obj;
            if (!_param1.TryGetValue(nameof(-1260198550), out obj))
              return;
            this.Value = obj.To(this.Type);
          }
          else
          {
            object obj = _param1.TryGetValue<string, object>(nameof(-1260198550));
            SettingsStorage storage = obj as SettingsStorage;
            this.Value = storage != null ? (object) storage.LoadEntire<IPersistable>() : obj;
          }
        }
        catch (InvalidCastException ex)
        {
          LogManager logManager = ServicesRegistry.LogManager;
          if (logManager != null)
            logManager.Application.AddDebugLog(LocalizedStrings.LoadingVariableErrorParams, (object[]) new object[1]
            {
              (object) ex
            });
          this.Value = (object) null;
        }
      }

      public void Save(SettingsStorage _param1)
      {
        object obj = this.Value;
        if (obj == null || obj is IConnector || obj is Strategy)
          return;
        Security security = obj as Security;
        if (security == null)
        {
          Portfolio portfolio = obj as Portfolio;
          if (portfolio == null)
          {
            IPersistable persistable = obj as IPersistable;
            if (persistable != null)
              _param1.SetValue<SettingsStorage>(nameof(-1260198550), persistable.SaveEntire(false));
            else
              _param1.SetValue<object>(nameof(-1260198550), this.Value);
          }
          else
            _param1.SetValue<string>(nameof(-1260198550), portfolio.GetUniqueId());
        }
        else
          _param1.SetValue<string>(nameof(-1260198550), security.Id);
      }

      private void \u0023\u003DzDvx4D6rowesQ(string _param1)
      {
        VariableDiagramElement.\u0023\u003DzLeRkRK4\u003D.\u0023\u003DzzhXz8TA22deF_xlJjVonSgA\u003D ta22deFXlJjVonSgA = new VariableDiagramElement.\u0023\u003DzLeRkRK4\u003D.\u0023\u003DzzhXz8TA22deF_xlJjVonSgA\u003D();
        ta22deFXlJjVonSgA._diagramElement = this;
        if (_param1.IsEmpty())
        {
          this.\u0023\u003DzaA\u0024G_HFnTqSt((object) null);
        }
        else
        {
          this.\u0023\u003DzGEMIaGl7fNMN = _param1;
          ta22deFXlJjVonSgA.\u0023\u003DzeuZad3mmF0NA = ServicesRegistry.SecurityProvider;
          IExchangeInfoProvider exchangeInfoProvider = ServicesRegistry.ExchangeInfoProvider;
          Security security = ta22deFXlJjVonSgA.\u0023\u003DzeuZad3mmF0NA.LookupById(this.\u0023\u003DzGEMIaGl7fNMN);
          if (security != null)
          {
            this.\u0023\u003DzaA\u0024G_HFnTqSt((object) security);
          }
          else
          {
            SecurityId securityId = this.\u0023\u003DzGEMIaGl7fNMN.ToSecurityId((SecurityIdGenerator) null);
            this.\u0023\u003DzaA\u0024G_HFnTqSt((object) new Security()
            {
              Id = this.\u0023\u003DzGEMIaGl7fNMN,
              Code = securityId.SecurityCode,
              Board = exchangeInfoProvider.GetOrCreateBoard(securityId.BoardCode, (Func<string, ExchangeBoard>) null)
            });
            if (this.\u0023\u003Dzlt70XM6Bpo90oSUkwh9WK\u0024I\u003D)
              return;
            ta22deFXlJjVonSgA.\u0023\u003DzeuZad3mmF0NA.Added += new Action<IEnumerable<Security>>(ta22deFXlJjVonSgA.\u0023\u003DqzEQgGacUSp1Yhf842MRzNDJOzLPZbjZsW7PHwTbPZJmJ1vBDLFV8yODSHoRdETLd);
            this.\u0023\u003Dzlt70XM6Bpo90oSUkwh9WK\u0024I\u003D = true;
          }
        }
      }

      private void \u0023\u003DzzPBWbYHpzM4t(string _param1)
      {
        this.\u0023\u003DzaA\u0024G_HFnTqSt(_param1.IsEmpty() ? (object) (Portfolio) null : (object) ServicesRegistry.PortfolioProvider.LookupByPortfolioName(_param1));
      }

      private void \u0023\u003DzaA\u0024G_HFnTqSt(object _param1)
      {
        this.\u0023\u003DzhX3sdPAmQeLT = true;
        this.Value = _param1;
        this.\u0023\u003DzhX3sdPAmQeLT = false;
      }

      private sealed class \u0023\u003DzzhXz8TA22deF_xlJjVonSgA\u003D
      {
        public VariableDiagramElement.\u0023\u003DzLeRkRK4\u003D _diagramElement;
        public ISecurityProvider \u0023\u003DzeuZad3mmF0NA;

        internal void \u0023\u003DqzEQgGacUSp1Yhf842MRzNDJOzLPZbjZsW7PHwTbPZJmJ1vBDLFV8yODSHoRdETLd(
          IEnumerable<Security> _param1)
        {
          Security security = _param1.FirstOrDefault<Security>(new Func<Security, bool>(this.\u0023\u003Dz\u00245_dK82Q_SGv1e5H5Q\u003D\u003D));
          if (security == null)
            return;
          this._diagramElement.\u0023\u003DzaA\u0024G_HFnTqSt((object) security);
          this.\u0023\u003DzeuZad3mmF0NA.Added -= new Action<IEnumerable<Security>>(this.\u0023\u003DqzEQgGacUSp1Yhf842MRzNDJOzLPZbjZsW7PHwTbPZJmJ1vBDLFV8yODSHoRdETLd);
          this._diagramElement.\u0023\u003Dzlt70XM6Bpo90oSUkwh9WK\u0024I\u003D = false;
        }

        internal bool \u0023\u003Dz\u00245_dK82Q_SGv1e5H5Q\u003D\u003D(Security _param1)
        {
          return _param1.Id.EqualsIgnoreCase(this._diagramElement.\u0023\u003DzGEMIaGl7fNMN);
        }
      }
    }
  }
}
