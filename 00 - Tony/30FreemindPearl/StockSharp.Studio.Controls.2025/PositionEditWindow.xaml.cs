// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PositionEditWindow
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace StockSharp.Studio.Controls
{
    public partial class PositionEditWindow : ThemedWindow
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
                HashSet<string> stringSet1 = new HashSet<string>();
                HashSet<string> stringSet2 = new HashSet<string>();
                Portfolio portfolio = value as Portfolio;
                if ( portfolio != null )
                {
                    this._isCreating = StringHelper.IsEmpty( portfolio.Name );
                    this.Title = this._isCreating ? LocalizedStrings.CreatingPortfolio : LocalizedStrings.PortfolioEditing;
                    if ( !this._isCreating )
                        stringSet1.Add( "Name" );
                    stringSet2.Add( "Portfolio" );
                }
                else
                {
                    this._isCreating = this._position.Security == null;
                    this.Title = this._isCreating ? LocalizedStrings.CreatingPosition : LocalizedStrings.PositionEditing;
                    if ( !this._isCreating )
                    {
                        stringSet1.Add( "Portfolio" );
                        stringSet1.Add( "Security" );
                    }
                }
                this.PropertyGrid.SelectedObject =  new PositionEditWindow.ReadOnlyWrapper( this._position, ( ISet<string> ) stringSet1, ( ISet<string> ) stringSet2 );
            }
        }

        public IPositionStorage PositionStorage { get; set; }

        private void OkButton_Click( object sender, RoutedEventArgs e )
        {
            Portfolio position1 = this.Position as Portfolio;
            if ( position1 != null )
            {
                position1.Name = position1.Name?.Trim();
                if ( StringHelper.IsEmpty( position1.Name ) )
                {
                    int num = (int) new MessageBoxBuilder().Owner( this).Warning().Text(LocalizedStrings.PortfolioNotSpecified).Show();
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
                        int num = (int) new MessageBoxBuilder().Owner( this).Warning().Text(LocalizedStrings.SecurityNotSpecified).Show();
                        return;
                    }
                    if ( position2.Portfolio == null )
                    {
                        int num = (int) new MessageBoxBuilder().Owner( this).Warning().Text(LocalizedStrings.PortfolioNotSpecified).Show();
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
                        int num = (int) new MessageBoxBuilder().Owner( this).Warning().Text(StringHelper.Put(LocalizedStrings.PortfolioAlreadyExist, new object[1]
            {
              (object) position2.Name
            })).Show();
                        return;
                    }
                }
                else
                {
                    Position position3 = this._position;
                    if ( this.PositionStorage.GetPosition( position3.Portfolio, position3.Security, position3.StrategyId, position3.Side, "", "", new TPlusLimits?() ) != null )
                    {
                        int num = (int) new MessageBoxBuilder().Owner( this).Warning().Text(StringHelper.Put(LocalizedStrings.PositionAlreadyExist, new object[1]
            {
              (object) (position3.Security.Id + "-" + position3.Portfolio.Name)
            })).Show();
                        return;
                    }
                }
            }
            this.DialogResult = new bool?( true );
        }

                private sealed class ReadOnlyWrapper : ICustomTypeDescriptor
        {
            private readonly Position _position;
            private readonly ISet<string> _readOnlyProperties;
            private readonly ISet<string> _hideProperties;

            public ReadOnlyWrapper(
              Position position,
              ISet<string> readOnlyProperties,
              ISet<string> hideProperties )
            {
                Position position1 = position;
                if ( position1 == null )
                    throw new ArgumentNullException( nameof( position ) );
                this._position = position1;
                ISet<string> stringSet1 = readOnlyProperties;
                if ( stringSet1 == null )
                    throw new ArgumentNullException( nameof( readOnlyProperties ) );
                this._readOnlyProperties = stringSet1;
                ISet<string> stringSet2 = hideProperties;
                if ( stringSet2 == null )
                    throw new ArgumentNullException( nameof( hideProperties ) );
                this._hideProperties = stringSet2;
            }

            AttributeCollection ICustomTypeDescriptor.GetAttributes()
            {
                return TypeDescriptor.GetAttributes(  this._position );
            }

            string ICustomTypeDescriptor.GetClassName()
            {
                return TypeDescriptor.GetClassName(  this._position );
            }

            string ICustomTypeDescriptor.GetComponentName()
            {
                return TypeDescriptor.GetComponentName(  this._position );
            }

            TypeConverter ICustomTypeDescriptor.GetConverter()
            {
                return TypeDescriptor.GetConverter(  this._position );
            }

            EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
            {
                return TypeDescriptor.GetDefaultEvent(  this._position );
            }

            PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
            {
                return TypeDescriptor.GetDefaultProperty(  this._position );
            }

            object ICustomTypeDescriptor.GetEditor( Type editorBaseType )
            {
                return  TypeDescriptor.GetAttributes(  this._position );
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
            {
                return TypeDescriptor.GetEvents(  this._position );
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents(
              Attribute [ ] attributes )
            {
                return TypeDescriptor.GetEvents(  this._position, attributes );
            }

            private PropertyDescriptorCollection GetProperties()
            {
                return new PropertyDescriptorCollection( TypeDescriptor.GetProperties(  this._position ).OfType<PropertyDescriptor>().Select<PropertyDescriptor, PropertyDescriptor>( ( Func<PropertyDescriptor, PropertyDescriptor> ) ( pd =>
                {
                    if ( this._readOnlyProperties.Contains( pd.Name ) )
                        return ( PropertyDescriptor ) new PositionEditWindow.ReadOnlyWrapper.ReadOnlyPropertyDescriptor( pd );
                    return pd;
                } ) ).Where<PropertyDescriptor>( ( Func<PropertyDescriptor, bool> ) ( pd => !this._hideProperties.Contains( pd.Name ) ) ).ToArray<PropertyDescriptor>() );
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
            {
                return this.GetProperties();
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
              Attribute [ ] attributes )
            {
                return this.GetProperties();
            }

            object ICustomTypeDescriptor.GetPropertyOwner( PropertyDescriptor pd )
            {
                return  this._position;
            }

            private class ReadOnlyPropertyDescriptor : NamedPropertyDescriptor
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

                public ReadOnlyPropertyDescriptor( PropertyDescriptor propertyDescriptor ) : base( propertyDescriptor )
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
