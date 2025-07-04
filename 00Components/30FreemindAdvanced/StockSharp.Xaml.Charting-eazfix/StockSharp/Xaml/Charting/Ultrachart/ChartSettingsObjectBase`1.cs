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

    private readonly T _chartElement;

    private CategoriesShowMode _categoriesShowMode;

    protected ChartSettingsObjectBase( T orig )
    {
        this._chartElement = orig ?? throw new ArgumentNullException( nameof( orig ) );
        this._categoriesShowMode = CategoriesShowMode.Visible;
    }

    public T Orig => this._chartElement;

    public CategoriesShowMode CategoriesMode
    {
        get => this._categoriesShowMode;
        protected set => this._categoriesShowMode = value;
    }

    object ICustomTypeDescriptor.GetPropertyOwner( PropertyDescriptor pd ) => ( object ) this.Orig;

    AttributeCollection ICustomTypeDescriptor.GetAttributes()
    {
        return TypeDescriptor.GetAttributes( ( object ) this.Orig, true );
    }

    string ICustomTypeDescriptor.GetClassName()
    {
        return TypeDescriptor.GetClassName( ( object ) this.Orig, true );
    }

    string ICustomTypeDescriptor.GetComponentName()
    {
        return TypeDescriptor.GetComponentName( ( object ) this.Orig, true );
    }

    TypeConverter ICustomTypeDescriptor.GetConverter()
    {
        return TypeDescriptor.GetConverter( ( object ) this.Orig, true );
    }

    EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
    {
        return TypeDescriptor.GetDefaultEvent( ( object ) this.Orig, true );
    }

    PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
    {
        return TypeDescriptor.GetDefaultProperty( ( object ) this.Orig, true );
    }

    object ICustomTypeDescriptor.GetEditor( Type editorBaseType )
    {
        return TypeDescriptor.GetEditor( ( object ) this.Orig, editorBaseType, true );
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents( Attribute[ ] attributes )
    {
        return TypeDescriptor.GetEvents( ( object ) this.Orig, attributes, true );
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
    {
        return TypeDescriptor.GetEvents( ( object ) this.Orig, true );
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties( Attribute[ ] attributes )
    {
        return ( ( ICustomTypeDescriptor ) this ).GetProperties();
    }

    protected abstract PropertyDescriptor[ ] OnGetProperties( T obj );

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
    {
        IEnumerable<PropertyDescriptor> collection = (IEnumerable<PropertyDescriptor>) this.OnGetProperties(this.Orig) ?? Enumerable.Empty<PropertyDescriptor>();
        List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
        propertyDescriptorList.AddRange( collection );
        return new PropertyDescriptorCollection( propertyDescriptorList.ToArray() );
    }

    public override string ToString() => string.Empty;

    protected abstract class ProxyDescriptor : NamedPropertyDescriptor
    {

        private readonly ChartSettingsObjectBase<T> _chartElementSetting;

        private readonly object _owner;

        private readonly AttributeCollection _attributeCollection;

        protected ProxyDescriptor( string name, object owner, T origObj, IEnumerable<Attribute> attributes, Func<T, PropertyDescriptor, bool> selector = null ) : base( name, null )
        {
            _owner = owner;
            _chartElementSetting = CreateWrapper( origObj, selector );

            var list = attributes.ToList( );
            list.RemoveAll( a =>
            {
                if ( !( a is DisplayNameAttribute ) )
                {
                    return a is DisplayAttribute;
                }

                return true;
            } );

            _attributeCollection = new AttributeCollection( list.ToArray() );
        }

        
        protected abstract ChartSettingsObjectBase<T> CreateWrapper(
          T obj,
          Func<T, PropertyDescriptor, bool> selector = null );

        public object Owner => this._owner;

        public override object GetValue( object c ) => ( object ) this._chartElementSetting;

        public override void SetValue( object c, object value ) => throw new NotSupportedException();

        public override bool CanResetValue( object c ) => false;

        public override void ResetValue( object c ) => throw new NotSupportedException();

        public override bool ShouldSerializeValue( object c ) => false;

        public override AttributeCollection Attributes
        {
            get => this._attributeCollection;
        }

        public override Type ComponentType => this.Owner.GetType();

        public override bool IsReadOnly => false;

        public override Type PropertyType => ( ( object ) this._chartElementSetting ).GetType();

        
    }
}

