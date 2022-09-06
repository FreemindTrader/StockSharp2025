using DevExpress.Xpf.PropertyGrid;
using Ecng.ComponentModel;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace fx.Charting.Ultrachart
{
    public abstract class ChartSettingsObjectBase< T > : NotifiableObject, ICustomTypeDescriptor
        where T : class
    {
        private CategoriesShowMode _categoriesShowMode = CategoriesShowMode.Visible;
        private PropertyDescriptorCollection _propertyDescriptorCollection;
        private readonly T _chartElement;

        protected ChartSettingsObjectBase( T orig )
        {
            T obj = orig;
            if( obj == null )
            {
                throw new ArgumentNullException( nameof( orig ) );
            }
            _chartElement = obj;
        }

        public T Orig
        {
            get
            {
                return _chartElement;
            }
        }

        public CategoriesShowMode CategoriesMode
        {
            get
            {
                return _categoriesShowMode;
            }
            protected set
            {
                _categoriesShowMode = value;
            }
        }

        object ICustomTypeDescriptor.GetPropertyOwner( PropertyDescriptor pd )
        {
            return Orig;
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes( )
        {
            return TypeDescriptor.GetAttributes( Orig, true );
        }

        string ICustomTypeDescriptor.GetClassName( )
        {
            return TypeDescriptor.GetClassName( Orig, true );
        }

        string ICustomTypeDescriptor.GetComponentName( )
        {
            return TypeDescriptor.GetComponentName( Orig, true );
        }

        TypeConverter ICustomTypeDescriptor.GetConverter( )
        {
            return TypeDescriptor.GetConverter( Orig, true );
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent( )
        {
            return TypeDescriptor.GetDefaultEvent( Orig, true );
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty( )
        {
            return TypeDescriptor.GetDefaultProperty( Orig, true );
        }

        object ICustomTypeDescriptor.GetEditor( Type editorBaseType )
        {
            return TypeDescriptor.GetEditor( Orig, editorBaseType, true );
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents( Attribute[ ] attributes )
        {
            return TypeDescriptor.GetEvents( Orig, attributes, true );
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents( )
        {
            return TypeDescriptor.GetEvents( Orig, true );
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties( Attribute[ ] attributes )
        {
            return ( ( ICustomTypeDescriptor )this ).GetProperties( );
        }

        protected abstract PropertyDescriptor[ ] OnGetProperties( T obj );

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties( )
        {
            return _propertyDescriptorCollection = new PropertyDescriptorCollection( ( OnGetProperties( Orig ) ?? Enumerable.Empty< PropertyDescriptor >( ) ).ToArray( ) );
        }

        public override string ToString( )
        {
            return string.Empty;
        }

        public delegate bool PdSelector( T obj, PropertyDescriptor pd );

        protected abstract class ProxyDescriptor : PropertyDescriptor
        {
            private readonly ChartSettingsObjectBase< T > _chartElementSetting;
            private readonly object _owner;
            private readonly AttributeCollection _attributeCollection;

            protected ProxyDescriptor( string name, object owner, T origObj, IEnumerable< Attribute > attributes, PdSelector selector = null ) : base( name, null )
            {
                _owner = owner;
                _chartElementSetting = CreateWrapper( origObj, selector );

                var list = attributes.ToList( );
                list.RemoveAll( a =>
                {
                    if( !( a is DisplayNameAttribute ) )
                    {
                        return a is DisplayAttribute;
                    }

                    return true;
                } );

                _attributeCollection = new AttributeCollection( list.ToArray( ) );
            }

            protected abstract ChartSettingsObjectBase< T > CreateWrapper( T obj, PdSelector selector = null );

            public object Owner
            {
                get
                {
                    return _owner;
                }
            }

            public override object GetValue( object component )
            {
                return _chartElementSetting;
            }

            public override void SetValue( object component, object value )
            {
                throw new NotSupportedException( );
            }

            public override bool CanResetValue( object component )
            {
                return false;
            }

            public override void ResetValue( object component )
            {
                throw new NotSupportedException( );
            }

            public override bool ShouldSerializeValue( object component )
            {
                return false;
            }

            public override AttributeCollection Attributes
            {
                get
                {
                    return _attributeCollection;
                }
            }

            public override Type ComponentType
            {
                get
                {
                    return Owner.GetType( );
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            public override Type PropertyType
            {
                get
                {
                    return _chartElementSetting.GetType( );
                }
            }
        }
    }
}
