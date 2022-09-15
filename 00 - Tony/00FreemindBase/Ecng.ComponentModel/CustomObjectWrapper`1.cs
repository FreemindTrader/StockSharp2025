// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.CustomObjectWrapper`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Ecng.ComponentModel
{
  public abstract class CustomObjectWrapper<T> : Disposable, INotifyPropertyChanged, ICustomTypeDescriptor
    where T : class
  {
    private EventDescriptorCollection _eventCollection;
    private PropertyDescriptorCollection _propCollection;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string name)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(name));
    }

    public T Obj { get; }

    protected CustomObjectWrapper(T obj)
    {
      T obj1 = obj;
      if ((object) obj1 == null)
        throw new ArgumentNullException(nameof (obj));
      this.Obj = obj1;
    }

    object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
    {
      return (object) this.Obj;
    }

    AttributeCollection ICustomTypeDescriptor.GetAttributes()
    {
      return TypeDescriptor.GetAttributes((object) this.Obj, true);
    }

    string ICustomTypeDescriptor.GetClassName()
    {
      return TypeDescriptor.GetClassName((object) this.Obj, true);
    }

    string ICustomTypeDescriptor.GetComponentName()
    {
      return TypeDescriptor.GetComponentName((object) this.Obj, true);
    }

    TypeConverter ICustomTypeDescriptor.GetConverter()
    {
      return TypeDescriptor.GetConverter((object) this.Obj, true);
    }

    EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
    {
      return TypeDescriptor.GetDefaultEvent((object) this.Obj, true);
    }

    PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
    {
      return TypeDescriptor.GetDefaultProperty((object) this.Obj, true);
    }

    object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
    {
      return TypeDescriptor.GetEditor((object) this.Obj, editorBaseType, true);
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents(
      Attribute[] attributes)
    {
      return ((ICustomTypeDescriptor) this).GetEvents();
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
      Attribute[] attributes)
    {
      return ((ICustomTypeDescriptor) this).GetProperties();
    }

    protected virtual IEnumerable<EventDescriptor> OnGetEvents()
    {
      if ((object) this.Obj != null)
        return (IEnumerable<EventDescriptor>) TypeDescriptor.GetEvents((object) this.Obj, true).OfType<EventDescriptor>().Select<EventDescriptor, CustomObjectWrapper<T>.ProxyEventDescriptor>((Func<EventDescriptor, CustomObjectWrapper<T>.ProxyEventDescriptor>) (ed => new CustomObjectWrapper<T>.ProxyEventDescriptor(ed, (object) this)));
      return (IEnumerable<EventDescriptor>) null;
    }

    protected virtual IEnumerable<PropertyDescriptor> OnGetProperties()
    {
      if ((object) this.Obj != null)
        return (IEnumerable<PropertyDescriptor>) TypeDescriptor.GetProperties((object) this.Obj, true).OfType<PropertyDescriptor>().Select<PropertyDescriptor, CustomObjectWrapper<T>.ProxyPropDescriptor>((Func<PropertyDescriptor, CustomObjectWrapper<T>.ProxyPropDescriptor>) (pd => new CustomObjectWrapper<T>.ProxyPropDescriptor(pd, (object) this)));
      return (IEnumerable<PropertyDescriptor>) null;
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
    {
      if (this._eventCollection != null)
        return this._eventCollection;
      IEnumerable<EventDescriptor> events = this.OnGetEvents();
      this._eventCollection = new EventDescriptorCollection((events != null ? events.ToArray<EventDescriptor>() : (EventDescriptor[]) null) ?? new EventDescriptor[0]);
      return this._eventCollection;
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
    {
      if (this._propCollection != null)
        return this._propCollection;
      IEnumerable<PropertyDescriptor> properties = this.OnGetProperties();
      this._propCollection = new PropertyDescriptorCollection((properties != null ? properties.ToArray<PropertyDescriptor>() : (PropertyDescriptor[]) null) ?? new PropertyDescriptor[0]);
      return this._propCollection;
    }

    public override string ToString()
    {
      return this.Obj?.ToString();
    }

    protected class ProxyPropDescriptor : PropertyDescriptor
    {
      private readonly PropertyDescriptor _orig;

      public ProxyPropDescriptor(PropertyDescriptor orig, object owner)
        : base((MemberDescriptor) orig)
      {
        this.Owner = owner;
        this._orig = orig;
      }

      public object Owner { get; }

      public override object GetValue(object c)
      {
        return this._orig.GetValue(c);
      }

      public override void SetValue(object c, object value)
      {
        throw new NotSupportedException();
      }

      public override bool CanResetValue(object c)
      {
        return false;
      }

      public override void ResetValue(object c)
      {
        throw new NotSupportedException();
      }

      public override bool ShouldSerializeValue(object c)
      {
        return false;
      }

      public override Type ComponentType
      {
        get
        {
          return this.Owner.GetType();
        }
      }

      public override bool IsReadOnly
      {
        get
        {
          return true;
        }
      }

      public override Type PropertyType
      {
        get
        {
          return this._orig.PropertyType;
        }
      }
    }

    protected class ProxyEventDescriptor : EventDescriptor
    {
      private readonly EventDescriptor _orig;

      public ProxyEventDescriptor(EventDescriptor orig, object owner)
        : base((MemberDescriptor) orig)
      {
        this.Owner = owner;
        this._orig = orig;
      }

      public object Owner { get; }

      public override void AddEventHandler(object component, Delegate value)
      {
        this._orig.AddEventHandler(component, value);
      }

      public override void RemoveEventHandler(object component, Delegate value)
      {
        this._orig.RemoveEventHandler(component, value);
      }

      public override Type ComponentType
      {
        get
        {
          return this.Owner.GetType();
        }
      }

      public override Type EventType
      {
        get
        {
          return this._orig.EventType;
        }
      }

      public override bool IsMulticast
      {
        get
        {
          return this._orig.IsMulticast;
        }
      }
    }
  }
}
