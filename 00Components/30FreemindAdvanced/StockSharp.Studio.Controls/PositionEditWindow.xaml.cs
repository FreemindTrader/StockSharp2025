// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PositionEditWindow
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class PositionEditWindow : ThemedWindow, IComponentConnector
{
    private bool _isCreating;
    private Position _position;

    public PositionEditWindow() => this.InitializeComponent();

    public Position Position
    {
        get => this._position;
        set
        {
            this._position = value ?? throw new ArgumentNullException(nameof(value));
            HashSet<string> readOnlyProperties = new HashSet<string>();
            HashSet<string> hideProperties = new HashSet<string>();
            if (value is Portfolio portfolio)
            {
                this._isCreating = StringHelper.IsEmpty(portfolio.Name);
                this.Title = this._isCreating ? LocalizedStrings.CreatingPortfolio : LocalizedStrings.PortfolioEditing;
                if (!this._isCreating)
                    readOnlyProperties.Add("Name");
                hideProperties.Add("Portfolio");
            }
            else
            {
                this._isCreating = this._position.Security == null;
                this.Title = this._isCreating ? LocalizedStrings.CreatingPosition : LocalizedStrings.PositionEditing;
                if (!this._isCreating)
                {
                    readOnlyProperties.Add("Portfolio");
                    readOnlyProperties.Add("Security");
                }
            }
            this.PropertyGrid.SelectedObject = (object)new PositionEditWindow.ReadOnlyWrapper(this._position, (ISet<string>)readOnlyProperties, (ISet<string>)hideProperties);
        }
    }

    public IPositionStorage PositionStorage { get; set; }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        if (this.Position is Portfolio position1)
        {
            position1.Name = position1.Name?.Trim();
            if (StringHelper.IsEmpty(position1.Name))
            {
                int num = (int)new MessageBoxBuilder().Owner((Window)this).Warning().Text(LocalizedStrings.PortfolioNotSpecified).Show();
                return;
            }
        }
        else
        {
            Position position = this.Position;
            if (position != null)
            {
                if (position.Security == null)
                {
                    int num = (int)new MessageBoxBuilder().Owner((Window)this).Warning().Text(LocalizedStrings.SecurityNotSpecified).Show();
                    return;
                }
                if (position.Portfolio == null)
                {
                    int num = (int)new MessageBoxBuilder().Owner((Window)this).Warning().Text(LocalizedStrings.PortfolioNotSpecified).Show();
                    return;
                }
            }
        }
        if (this._isCreating)
        {
            if (this._position is Portfolio position3)
            {
                if (this.PositionStorage.LookupByPortfolioName(position3.Name) != null)
                {
                    int num = (int)new MessageBoxBuilder().Owner((Window)this).Warning().Text(StringHelper.Put(LocalizedStrings.PortfolioAlreadyExist, new object[1]
                    {
            (object) position3.Name
                    })).Show();
                    return;
                }
            }
            else
            {
                Position position2 = this._position;
                if (this.PositionStorage.GetPosition(position2.Portfolio, position2.Security, position2.StrategyId, position2.Side) != null)
                {
                    int num = (int)new MessageBoxBuilder().Owner((Window)this).Warning().Text(StringHelper.Put(LocalizedStrings.PositionAlreadyExist, new object[1]
                    {
            (object) $"{position2.Security.Id}-{position2.Portfolio.Name}"
                    })).Show();
                    return;
                }
            }
        }
        this.DialogResult = new bool?(true);
    }



    private sealed class ReadOnlyWrapper(
      Position position,
      ISet<string> readOnlyProperties,
      ISet<string> hideProperties) : ICustomTypeDescriptor
    {
        private readonly Position _position = position ?? throw new ArgumentNullException(nameof(position));
        private readonly ISet<string> _readOnlyProperties = readOnlyProperties ?? throw new ArgumentNullException(nameof(readOnlyProperties));
        private readonly ISet<string> _hideProperties = hideProperties ?? throw new ArgumentNullException(nameof(hideProperties));

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes((object)this._position);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName((object)this._position);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName((object)this._position);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter((object)this._position);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent((object)this._position);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty((object)this._position);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return (object)TypeDescriptor.GetAttributes((object)this._position);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents((object)this._position);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents((object)this._position, attributes);
        }

        private PropertyDescriptorCollection GetProperties()
        {
            return new PropertyDescriptorCollection(TypeDescriptor.GetProperties((object)this._position).OfType<PropertyDescriptor>().Select<PropertyDescriptor, PropertyDescriptor>((Func<PropertyDescriptor, PropertyDescriptor>)(pd => this._readOnlyProperties.Contains(pd.Name) ? (PropertyDescriptor)new PositionEditWindow.ReadOnlyWrapper.ReadOnlyPropertyDescriptor(pd) : pd)).Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>)(pd => !this._hideProperties.Contains(pd.Name))).ToArray<PropertyDescriptor>());
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() => this.GetProperties();

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            return this.GetProperties();
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => (object)this._position;

        private class ReadOnlyPropertyDescriptor : NamedPropertyDescriptor
        {
            private readonly PropertyDescriptor _propertyDescriptor;

            public ReadOnlyPropertyDescriptor(PropertyDescriptor propertyDescriptor) : base(propertyDescriptor)
            {
                this._propertyDescriptor = propertyDescriptor ?? throw new ArgumentNullException(nameof(propertyDescriptor));
            }

            public override Type ComponentType => this._propertyDescriptor.ComponentType;

            public override bool IsReadOnly => true;

            public override Type PropertyType => this._propertyDescriptor.PropertyType;

            public override bool CanResetValue(object component) => false;

            public override object GetValue(object component)
            {
                return this._propertyDescriptor.GetValue(component);
            }

            public override void ResetValue(object component)
            {
            }

            public override void SetValue(object component, object value)
            {
            }

            public override bool ShouldSerializeValue(object component)
            {
                return this._propertyDescriptor.ShouldSerializeValue(component);
            }
        }
    }
}
