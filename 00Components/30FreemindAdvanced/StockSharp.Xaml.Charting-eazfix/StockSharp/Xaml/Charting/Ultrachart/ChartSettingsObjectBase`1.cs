// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Ultrachart.ChartSettingsObjectBase`1
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.PropertyGrid;
using Ecng.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace StockSharp.Xaml.Charting.Ultrachart;

public abstract class ChartSettingsObjectBase<T> : NotifiableObject, ICustomTypeDescriptor where T : class
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly T \u0023\u003DzospwsbhzrQArHP5ofw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private CategoriesShowMode \u0023\u003DzTWg19Vn1u7pTvAS8raKaWTM\u003D;

  protected ChartSettingsObjectBase(T orig)
  {
    this.\u0023\u003DzospwsbhzrQArHP5ofw\u003D\u003D = orig ?? throw new ArgumentNullException(nameof (orig));
    this.\u0023\u003DzTWg19Vn1u7pTvAS8raKaWTM\u003D = CategoriesShowMode.Visible;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public T Orig => this.\u0023\u003DzospwsbhzrQArHP5ofw\u003D\u003D;

  public CategoriesShowMode CategoriesMode
  {
    get => this.\u0023\u003DzTWg19Vn1u7pTvAS8raKaWTM\u003D;
    protected set => this.\u0023\u003DzTWg19Vn1u7pTvAS8raKaWTM\u003D = value;
  }

  object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => (object) this.Orig;

  AttributeCollection ICustomTypeDescriptor.GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this.Orig, true);
  }

  string ICustomTypeDescriptor.GetClassName()
  {
    return TypeDescriptor.GetClassName((object) this.Orig, true);
  }

  string ICustomTypeDescriptor.GetComponentName()
  {
    return TypeDescriptor.GetComponentName((object) this.Orig, true);
  }

  TypeConverter ICustomTypeDescriptor.GetConverter()
  {
    return TypeDescriptor.GetConverter((object) this.Orig, true);
  }

  EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this.Orig, true);
  }

  PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this.Orig, true);
  }

  object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this.Orig, editorBaseType, true);
  }

  EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this.Orig, attributes, true);
  }

  EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this.Orig, true);
  }

  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
  {
    return ((ICustomTypeDescriptor) this).GetProperties();
  }

  protected abstract PropertyDescriptor[] OnGetProperties(T obj);

  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
  {
    IEnumerable<PropertyDescriptor> collection = (IEnumerable<PropertyDescriptor>) this.OnGetProperties(this.Orig) ?? Enumerable.Empty<PropertyDescriptor>();
    List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
    propertyDescriptorList.AddRange(collection);
    return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
  }

  public virtual string ToString() => string.Empty;

  protected abstract class ProxyDescriptor : NamedPropertyDescriptor
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ChartSettingsObjectBase<T> \u0023\u003Dz\u0024KTWtGTYNAk9;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly object \u0023\u003DzKwdqDML77LmCBT9NQQ\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly AttributeCollection \u0023\u003Dz0RkMCSqBkh1BIjKldQ\u003D\u003D;

    protected ProxyDescriptor(
      string name,
      object owner,
      T origObj,
      IEnumerable<Attribute> attributes,
      Func<T, PropertyDescriptor, bool> selector = null)
      : base(name, (Attribute[]) null)
    {
      this.\u0023\u003DzKwdqDML77LmCBT9NQQ\u003D\u003D = owner;
      this.\u0023\u003Dz\u0024KTWtGTYNAk9 = this.CreateWrapper(origObj, selector);
      List<Attribute> list = attributes.ToList<Attribute>();
      list.RemoveAll(ChartSettingsObjectBase<T>.ProxyDescriptor.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzMJsSoL5rX24oRR6rOw\u003D\u003D ?? (ChartSettingsObjectBase<T>.ProxyDescriptor.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzMJsSoL5rX24oRR6rOw\u003D\u003D = new Predicate<Attribute>(ChartSettingsObjectBase<T>.ProxyDescriptor.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz4rQcnrkgVpOd4_6eJw\u003D\u003D)));
      this.\u0023\u003Dz0RkMCSqBkh1BIjKldQ\u003D\u003D = new AttributeCollection(list.ToArray());
    }

    protected abstract ChartSettingsObjectBase<T> CreateWrapper(
      T obj,
      Func<T, PropertyDescriptor, bool> selector = null);

    public object Owner => this.\u0023\u003DzKwdqDML77LmCBT9NQQ\u003D\u003D;

    public virtual object GetValue(object c) => (object) this.\u0023\u003Dz\u0024KTWtGTYNAk9;

    public virtual void SetValue(object c, object value) => throw new NotSupportedException();

    public virtual bool CanResetValue(object c) => false;

    public virtual void ResetValue(object c) => throw new NotSupportedException();

    public virtual bool ShouldSerializeValue(object c) => false;

    public virtual AttributeCollection Attributes
    {
      get => this.\u0023\u003Dz0RkMCSqBkh1BIjKldQ\u003D\u003D;
    }

    public virtual Type ComponentType => this.Owner.GetType();

    public virtual bool IsReadOnly => false;

    public virtual Type PropertyType => ((object) this.\u0023\u003Dz\u0024KTWtGTYNAk9).GetType();

    [Serializable]
    private sealed class \u0023\u003Dz7qOdpi4\u003D
    {
      public static readonly ChartSettingsObjectBase<T>.ProxyDescriptor.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new ChartSettingsObjectBase<T>.ProxyDescriptor.\u0023\u003Dz7qOdpi4\u003D();
      public static Predicate<Attribute> \u0023\u003DzMJsSoL5rX24oRR6rOw\u003D\u003D;

      internal bool \u0023\u003Dz4rQcnrkgVpOd4_6eJw\u003D\u003D(Attribute _param1)
      {
        return _param1 is DisplayNameAttribute || _param1 is DisplayAttribute;
      }
    }
  }
}
