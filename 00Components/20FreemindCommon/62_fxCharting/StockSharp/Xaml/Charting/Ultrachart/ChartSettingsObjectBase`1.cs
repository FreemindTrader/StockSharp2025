using DevExpress.Xpf.PropertyGrid;
using Ecng.ComponentModel;
using System;
using System.Collections.Generic;
using fx.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StockSharp.Xaml.Charting.Ultrachart;

/// <summary>
/// Base proxy object to edit chart element in property grid.
/// </summary>
/// <typeparam name="T">Value type.</typeparam>
/// <remarks>Create instance.</remarks>
/// <param name="orig">Parent chart element or indicator.</param>
public abstract class ChartSettingsObjectBase<T> : NotifiableObject, ICustomTypeDescriptor where T : class
{

    private readonly T _chartElement;

    private CategoriesShowMode _categoriesShowMode;

    /// <summary>
    /// Base proxy object to edit chart element in property grid.
    /// </summary>
    /// <typeparam name="T">Value type.</typeparam>
    /// <remarks>Create instance.</remarks>
    /// <param name="orig">Parent chart element or indicator.</param>
    protected ChartSettingsObjectBase( T orig )
    {
        _chartElement = orig ?? throw new ArgumentNullException( nameof( orig ) );
        _categoriesShowMode = CategoriesShowMode.Visible;
    }

    /// <summary>Parent chart element or indicator.</summary>
    public T Orig => _chartElement;

    /// <summary>Categories mode of property grid.</summary>
    public CategoriesShowMode CategoriesMode
    {
        get => _categoriesShowMode;
        protected set => _categoriesShowMode = value;
    }

    object ICustomTypeDescriptor.GetPropertyOwner( PropertyDescriptor pd ) => ( object ) Orig;

    AttributeCollection ICustomTypeDescriptor.GetAttributes()
    {
        return TypeDescriptor.GetAttributes( ( object ) Orig, true );
    }

    string ICustomTypeDescriptor.GetClassName()
    {
        return TypeDescriptor.GetClassName( ( object ) Orig, true );
    }

    string ICustomTypeDescriptor.GetComponentName()
    {
        return TypeDescriptor.GetComponentName( ( object ) Orig, true );
    }

    TypeConverter ICustomTypeDescriptor.GetConverter()
    {
        return TypeDescriptor.GetConverter( ( object ) Orig, true );
    }

    EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
    {
        return TypeDescriptor.GetDefaultEvent( ( object ) Orig, true );
    }

    PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
    {
        return TypeDescriptor.GetDefaultProperty( ( object ) Orig, true );
    }

    object ICustomTypeDescriptor.GetEditor( Type editorBaseType )
    {
        return TypeDescriptor.GetEditor( ( object ) Orig, editorBaseType, true );
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents( Attribute[ ] attributes )
    {
        return TypeDescriptor.GetEvents( ( object ) Orig, attributes, true );
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
    {
        return TypeDescriptor.GetEvents( ( object ) Orig, true );
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties( Attribute[ ] attributes )
    {
        return ( ( ICustomTypeDescriptor ) this ).GetProperties();
    }

    /// <summary>Get property list from wrapped object.</summary>
    protected abstract PropertyDescriptor[ ] OnGetProperties( T obj );

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
    {
        IEnumerable<PropertyDescriptor> collection = (IEnumerable<PropertyDescriptor>) OnGetProperties(Orig) ?? Enumerable.Empty<PropertyDescriptor>();
        List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
        propertyDescriptorList.AddRange( collection );
        return new PropertyDescriptorCollection( propertyDescriptorList.ToArray() );
    }

    /// <inheritdoc />
    public override string ToString() => string.Empty;


    /// <summary>
    /// Specialization of <see cref="T:System.ComponentModel.PropertyDescriptor" /> class for chart element properties.
    /// </summary>
    protected abstract class ProxyDescriptor : NamedPropertyDescriptor
    {

        private readonly ChartSettingsObjectBase<T> _chartElementSetting;

        private readonly object _owner;

        private readonly AttributeCollection _attributes;

        /// <summary>Create instance.</summary>
        protected ProxyDescriptor( string name, object owner, T origObj, IEnumerable<Attribute> attributes, Func<T, PropertyDescriptor, bool> selector = null )
          : base( name, ( Attribute[ ] ) null )
        {
            _owner = owner;
            _chartElementSetting = CreateWrapper( origObj, selector );

            var list = attributes.ToList<Attribute>();
            list.RemoveAll( p => p is DisplayNameAttribute || p is DisplayAttribute );
            _attributes = new AttributeCollection( list.ToArray() );
        }

        /// <summary>Create chart settings object wrapper for an object.</summary>
        protected abstract ChartSettingsObjectBase<T> CreateWrapper(
          T obj,
          Func<T, PropertyDescriptor, bool> selector = null );

        /// <summary>Parent object.</summary>
        public object Owner => _owner;

        /// <inheritdoc />
        public override object GetValue( object c ) => ( object ) _chartElementSetting;

        /// <inheritdoc />
        public override void SetValue( object c, object value ) => throw new NotSupportedException();

        /// <inheritdoc />
        public override bool CanResetValue( object c ) => false;

        /// <inheritdoc />
        public override void ResetValue( object c ) => throw new NotSupportedException();

        /// <inheritdoc />
        public override bool ShouldSerializeValue( object c ) => false;

        /// <inheritdoc />
        public override AttributeCollection Attributes
        {
            get => _attributes;
        }

        /// <inheritdoc />
        public override Type ComponentType => Owner.GetType();

        /// <inheritdoc />
        public override bool IsReadOnly => false;

        /// <inheritdoc />
        public override Type PropertyType => ( ( object ) _chartElementSetting ).GetType();


    }
}
