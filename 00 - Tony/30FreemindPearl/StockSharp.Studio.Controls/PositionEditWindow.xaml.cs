
using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    public partial class PositionEditWindow : ThemedWindow, IComponentConnector
    {
        private bool _isCreating;
        private Position _position;

        public PositionEditWindow()
        {
            InitializeComponent();
        }

        public Position Position
        {
            get
            {
                return _position;
            }
            set
            {
                Position position = value;
                if ( position == null )
                    throw new ArgumentNullException( nameof( value ) );
                _position = position;
                HashSet<string> stringSet = new HashSet<string>();
                Portfolio portfolio = value as Portfolio;
                if ( portfolio != null )
                {
                    _isCreating = portfolio.Name.IsEmpty();
                    Title = _isCreating ? LocalizedStrings.Str3246 : LocalizedStrings.Str3247;
                    if ( !_isCreating )
                        stringSet.Add( "Name" );
                }
                else
                {
                    _isCreating = _position.Security == null;
                    Title = _isCreating ? LocalizedStrings.Str3248 : LocalizedStrings.Str3249;
                    if ( !_isCreating )
                    {
                        stringSet.Add( "Portfolio" );
                        stringSet.Add( "Security" );
                    }
                }
                PropertyGrid.SelectedObject = new ReadOnlyWrapper( _position, stringSet );
            }
        }

        public IPositionStorage PositionStorage { get; set; }

        private void OkButton_Click( object sender, RoutedEventArgs e )
        {
            Portfolio position1 = Position as Portfolio;
            if ( position1 != null )
            {
                position1.Name = position1.Name?.Trim();
                if ( position1.Name.IsEmpty() )
                {
                    int num = ( int )new MessageBoxBuilder().Owner( this ).Warning().Text( LocalizedStrings.Str3251 ).Show();
                    return;
                }
            }
            else
            {
                Position position2 = Position;
                if ( position2 != null )
                {
                    if ( position2.Security == null )
                    {
                        int num = ( int )new MessageBoxBuilder().Owner( this ).Warning().Text( LocalizedStrings.Str3252 ).Show();
                        return;
                    }
                    if ( position2.Portfolio == null )
                    {
                        int num = ( int )new MessageBoxBuilder().Owner( this ).Warning().Text( LocalizedStrings.Str3253 ).Show();
                        return;
                    }
                }
            }
            if ( _isCreating )
            {
                Portfolio position2 = _position as Portfolio;
                if ( position2 != null )
                {
                    if ( PositionStorage.LookupByPortfolioName( position2.Name ) != null )
                    {
                        int num = ( int )new MessageBoxBuilder().Owner( this ).Warning().Text( LocalizedStrings.PortfolioAlreadyExist.Put( position2.Name ) ).Show();
                        return;
                    }
                }
                else
                {
                    Position position3 = _position;
                    if ( PositionStorage.GetPosition( position3.Portfolio, position3.Security, position3.StrategyId, position3.Side, string.Empty, string.Empty, new TPlusLimits?() ) != null )
                    {
                        int num = ( int )new MessageBoxBuilder().Owner( this ).Warning().Text( LocalizedStrings.PositionAlreadyExist.Put( position3.Security.Id + "-" + position3.Portfolio.Name ) ).Show();
                        return;
                    }
                }
            }
            DialogResult = new bool?( true );
        }



        private sealed class ReadOnlyWrapper : ICustomTypeDescriptor
        {
            private readonly Position _position;
            private readonly IEnumerable<string> _readOnlyProperties;

            public ReadOnlyWrapper( Position position, IEnumerable<string> readOnlyProperties )
            {
                Position position1 = position;
                if ( position1 == null )
                    throw new ArgumentNullException( nameof( position ) );
                _position = position1;
                IEnumerable<string> strings = readOnlyProperties;
                if ( strings == null )
                    throw new ArgumentNullException( nameof( readOnlyProperties ) );
                _readOnlyProperties = strings;
            }

            AttributeCollection ICustomTypeDescriptor.GetAttributes()
            {
                return TypeDescriptor.GetAttributes( _position );
            }

            string ICustomTypeDescriptor.GetClassName()
            {
                return TypeDescriptor.GetClassName( _position );
            }

            string ICustomTypeDescriptor.GetComponentName()
            {
                return TypeDescriptor.GetComponentName( _position );
            }

            TypeConverter ICustomTypeDescriptor.GetConverter()
            {
                return TypeDescriptor.GetConverter( _position );
            }

            EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
            {
                return TypeDescriptor.GetDefaultEvent( _position );
            }

            PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
            {
                return TypeDescriptor.GetDefaultProperty( _position );
            }

            object ICustomTypeDescriptor.GetEditor( Type editorBaseType )
            {
                return TypeDescriptor.GetAttributes( _position );
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
            {
                return TypeDescriptor.GetEvents( _position );
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents(
              Attribute[ ] attributes )
            {
                return TypeDescriptor.GetEvents( _position, attributes );
            }

            private PropertyDescriptorCollection GetProperties()
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties( _position );
                if ( !_readOnlyProperties.Any() )
                    return properties;
                return new PropertyDescriptorCollection( properties.OfType<PropertyDescriptor>().Select( pd =>
                    {
                        if ( _readOnlyProperties.Contains( pd.Name ) )
                            return new ReadOnlyPropertyDescriptor( pd );
                        return pd;
                    } ).ToArray() );
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
            {
                return GetProperties();
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
              Attribute[ ] attributes )
            {
                return GetProperties();
            }

            object ICustomTypeDescriptor.GetPropertyOwner( PropertyDescriptor pd )
            {
                return _position;
            }

            private class ReadOnlyPropertyDescriptor : PropertyDescriptor
            {
                private readonly PropertyDescriptor _propertyDescriptor;

                public override Type ComponentType
                {
                    get
                    {
                        return _propertyDescriptor.ComponentType;
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
                        return _propertyDescriptor.PropertyType;
                    }
                }

                public ReadOnlyPropertyDescriptor( PropertyDescriptor propertyDescriptor )
                  : base( propertyDescriptor )
                {
                    PropertyDescriptor propertyDescriptor1 = propertyDescriptor;
                    if ( propertyDescriptor1 == null )
                        throw new ArgumentNullException( nameof( propertyDescriptor ) );
                    _propertyDescriptor = propertyDescriptor1;
                }

                public override bool CanResetValue( object component )
                {
                    return false;
                }

                public override object GetValue( object component )
                {
                    return _propertyDescriptor.GetValue( component );
                }

                public override void ResetValue( object component )
                {
                }

                public override void SetValue( object component, object value )
                {
                }

                public override bool ShouldSerializeValue( object component )
                {
                    return _propertyDescriptor.ShouldSerializeValue( component );
                }
            }
        }
    }
}
