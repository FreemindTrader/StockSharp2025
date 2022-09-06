
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
            this.InitializeComponent();
        }

        public Position Position
        {
            get
            {
                return this._position;
            }
            set
            {
                Position position = value;
                if ( position == null )
                    throw new ArgumentNullException( nameof( value ) );
                this._position = position;
                HashSet<string> stringSet = new HashSet<string>();
                Portfolio portfolio = value as Portfolio;
                if ( portfolio != null )
                {
                    this._isCreating = portfolio.Name.IsEmpty();
                    this.Title = this._isCreating ? LocalizedStrings.Str3246 : LocalizedStrings.Str3247;
                    if ( !this._isCreating )
                        stringSet.Add( "Name" );
                }
                else
                {
                    this._isCreating = this._position.Security == null;
                    this.Title = this._isCreating ? LocalizedStrings.Str3248 : LocalizedStrings.Str3249;
                    if ( !this._isCreating )
                    {
                        stringSet.Add( "Portfolio" );
                        stringSet.Add( "Security" );
                    }
                }
                this.PropertyGrid.SelectedObject = ( object )new PositionEditWindow.ReadOnlyWrapper( this._position, ( IEnumerable<string> )stringSet );
            }
        }

        public IPositionStorage PositionStorage { get; set; }

        private void OkButton_Click( object sender, RoutedEventArgs e )
        {
            Portfolio position1 = this.Position as Portfolio;
            if ( position1 != null )
            {
                position1.Name = position1.Name?.Trim();
                if ( position1.Name.IsEmpty() )
                {
                    int num = ( int )new MessageBoxBuilder().Owner( ( Window )this ).Warning().Text( LocalizedStrings.Str3251 ).Show();
                    return;
                }
            }
            else
            {
                Position position2 = this.Position;
                if ( position2 != null )
                {
                    if ( position2.Security == null )
                    {
                        int num = ( int )new MessageBoxBuilder().Owner( ( Window )this ).Warning().Text( LocalizedStrings.Str3252 ).Show();
                        return;
                    }
                    if ( position2.Portfolio == null )
                    {
                        int num = ( int )new MessageBoxBuilder().Owner( ( Window )this ).Warning().Text( LocalizedStrings.Str3253 ).Show();
                        return;
                    }
                }
            }
            if ( this._isCreating )
            {
                Portfolio position2 = this._position as Portfolio;
                if ( position2 != null )
                {
                    if ( this.PositionStorage.LookupByPortfolioName( position2.Name ) != null )
                    {
                        int num = ( int )new MessageBoxBuilder().Owner( ( Window )this ).Warning().Text( LocalizedStrings.PortfolioAlreadyExist.Put( ( object )position2.Name ) ).Show();
                        return;
                    }
                }
                else
                {
                    Position position3 = this._position;
                    if ( this.PositionStorage.GetPosition( position3.Portfolio, position3.Security, position3.StrategyId, position3.Side, string.Empty, string.Empty, new TPlusLimits?() ) != null )
                    {
                        int num = ( int )new MessageBoxBuilder().Owner( ( Window )this ).Warning().Text( LocalizedStrings.PositionAlreadyExist.Put( ( object )( position3.Security.Id + "-" + position3.Portfolio.Name ) ) ).Show();
                        return;
                    }
                }
            }
            this.DialogResult = new bool?( true );
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
                this._position = position1;
                IEnumerable<string> strings = readOnlyProperties;
                if ( strings == null )
                    throw new ArgumentNullException( nameof( readOnlyProperties ) );
                this._readOnlyProperties = strings;
            }

            AttributeCollection ICustomTypeDescriptor.GetAttributes()
            {
                return TypeDescriptor.GetAttributes( ( object )this._position );
            }

            string ICustomTypeDescriptor.GetClassName()
            {
                return TypeDescriptor.GetClassName( ( object )this._position );
            }

            string ICustomTypeDescriptor.GetComponentName()
            {
                return TypeDescriptor.GetComponentName( ( object )this._position );
            }

            TypeConverter ICustomTypeDescriptor.GetConverter()
            {
                return TypeDescriptor.GetConverter( ( object )this._position );
            }

            EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
            {
                return TypeDescriptor.GetDefaultEvent( ( object )this._position );
            }

            PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
            {
                return TypeDescriptor.GetDefaultProperty( ( object )this._position );
            }

            object ICustomTypeDescriptor.GetEditor( Type editorBaseType )
            {
                return ( object )TypeDescriptor.GetAttributes( ( object )this._position );
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
            {
                return TypeDescriptor.GetEvents( ( object )this._position );
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents(
              Attribute[ ] attributes )
            {
                return TypeDescriptor.GetEvents( ( object )this._position, attributes );
            }

            private PropertyDescriptorCollection GetProperties()
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties( ( object )this._position );
                if ( !this._readOnlyProperties.Any<string>() )
                    return properties;
                return new PropertyDescriptorCollection( properties.OfType<PropertyDescriptor>().Select<PropertyDescriptor, PropertyDescriptor>( ( Func<PropertyDescriptor, PropertyDescriptor> )( pd =>
                    {
                        if ( this._readOnlyProperties.Contains<string>( pd.Name ) )
                            return ( PropertyDescriptor )new PositionEditWindow.ReadOnlyWrapper.ReadOnlyPropertyDescriptor( pd );
                        return pd;
                    } ) ).ToArray<PropertyDescriptor>() );
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
            {
                return this.GetProperties();
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
              Attribute[ ] attributes )
            {
                return this.GetProperties();
            }

            object ICustomTypeDescriptor.GetPropertyOwner( PropertyDescriptor pd )
            {
                return ( object )this._position;
            }

            private class ReadOnlyPropertyDescriptor : PropertyDescriptor
            {
                private readonly PropertyDescriptor _propertyDescriptor;

                public override Type ComponentType
                {
                    get
                    {
                        return this._propertyDescriptor.ComponentType;
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
                        return this._propertyDescriptor.PropertyType;
                    }
                }

                public ReadOnlyPropertyDescriptor( PropertyDescriptor propertyDescriptor )
                  : base( ( MemberDescriptor )propertyDescriptor )
                {
                    PropertyDescriptor propertyDescriptor1 = propertyDescriptor;
                    if ( propertyDescriptor1 == null )
                        throw new ArgumentNullException( nameof( propertyDescriptor ) );
                    this._propertyDescriptor = propertyDescriptor1;
                }

                public override bool CanResetValue( object component )
                {
                    return false;
                }

                public override object GetValue( object component )
                {
                    return this._propertyDescriptor.GetValue( component );
                }

                public override void ResetValue( object component )
                {
                }

                public override void SetValue( object component, object value )
                {
                }

                public override bool ShouldSerializeValue( object component )
                {
                    return this._propertyDescriptor.ShouldSerializeValue( component );
                }
            }
        }
    }
}
