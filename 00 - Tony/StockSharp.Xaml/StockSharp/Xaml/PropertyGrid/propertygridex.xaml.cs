using DevExpress.Data.Mask;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace StockSharp.Xaml.PropertyGrid
{
    // Tony : Fix a problem with dxprg not recognized namesapce
    // https://www.devexpress.com/Support/Center/Question/Details/Q502525/typenameparserexception-prefix-dxg-does-not-map-to-a-namespace
    public partial class PropertyGridEx : PropertyGridControl
    {
        public static readonly DependencyProperty SecurityProviderProperty     = DependencyProperty.Register( nameof( SecurityProvider ), typeof( ISecurityProvider ), typeof( PropertyGridEx ) );
        public static readonly DependencyProperty ExchangeInfoProviderProperty = DependencyProperty.Register( nameof( ExchangeInfoProvider ), typeof( IExchangeInfoProvider ), typeof( PropertyGridEx ) );
        public static readonly DependencyProperty PortfoliosProperty           = DependencyProperty.Register( nameof( Portfolios ), typeof( PortfolioDataSource ), typeof( PropertyGridEx ) );        

        public PropertyGridEx( )
        {
            InitializeComponent( );

            if ( this.IsDesignMode( ) )
                return;

            Intialize( );
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return ( ISecurityProvider )GetValue( SecurityProviderProperty );
            }
            set
            {
                SetValue( SecurityProviderProperty, value );
            }
        }

        public IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return ( IExchangeInfoProvider )GetValue( ExchangeInfoProviderProperty );
            }
            set
            {
                SetValue( ExchangeInfoProviderProperty, value );
            }
        }

        public PortfolioDataSource Portfolios
        {
            get
            {
                return ( PortfolioDataSource )GetValue( PortfoliosProperty );
            }
            set
            {
                SetValue( PortfoliosProperty, value );
            }
        }

        protected override void OnSelectedObjectChanged( object oldValue, object newValue )
        {
            base.OnSelectedObjectChanged( oldValue, newValue );

            INotifyPropertiesChanged changed = oldValue as INotifyPropertiesChanged;
            if ( changed != null )
            {
                changed.PropertiesChanged -= OnPropertiesChanged;
            }

            changed = newValue as INotifyPropertiesChanged;
            if ( changed == null )
            {
                return;
            }

            changed.PropertiesChanged += OnPropertiesChanged;
        }

        private void OnPropertiesChanged( )
        {
            GuiDispatcher.GlobalDispatcher.AddAction( ( ) =>
            {
                var value = SelectedObject;

                SelectedObject = null;
                SelectedObject = value;
            } );
        }

        private void Intialize( )
        {
            SecurityProvider     = ServicesRegistry.TrySecurityProvider;
            ExchangeInfoProvider = ServicesRegistry.TryExchangeInfoProvider;
            Portfolios           = ConfigManager.TryGetService<PortfolioDataSource>( );

            ConfigManager.ServiceRegistered += new Action<Type, object>( OnServiceRegistered );
        }

        private void PropertyGridControl_ShownEditor( object sender, PropertyGridEditorEventArgs args )
        {
            SpinEdit spinEdit = LayoutTreeHelper.GetVisualChildren( sender as FrameworkElement ).OfType<SpinEdit>( ).FirstOrDefault<SpinEdit>( e => e.Name == string.Empty );

            if ( spinEdit == null )
            {
                return;
            }

            spinEdit.EditValueChanged += new EditValueChangedEventHandler( SpinEdit_EditValueChanged );
        }

        private void SpinEdit_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( e.NewValue == null || !e.NewValue.ToString( ).CompareIgnoreCase( "0" ) )
                return;
            ( ( BaseEdit )sender ).EditValue = null;
        }
              

        private void OnServiceRegistered( Type t, object s )
        {
            var sp = s as ISecurityProvider;

            if ( sp != null )
            {
                this.GuiAsync( () => 
                {
                    if ( SecurityProvider != null )
                        return;
                    SecurityProvider = sp;
                } );
            }

            var ep = s as IExchangeInfoProvider;

            if ( ep != null )
            {
                this.GuiAsync( ( ) =>
                {
                    if ( ExchangeInfoProvider != null )
                        return;
                    ExchangeInfoProvider = ep;
                } );
            }

            var portfolios = s as PortfolioDataSource;

            if ( portfolios != null )
            {
                this.GuiAsync( ( ) =>
                {
                    if ( Portfolios == null )
                        Portfolios = portfolios;
                } );
            }            
        }       
    }

    internal sealed class TonyPropertyGridCellTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate( object item, DependencyObject container )
        {            
            RowData rowItem = ( RowData )item;

            if ( rowItem == null )
            {
                return base.SelectTemplate( null, container );
            }
                
            PropertyGridEx propertyGrid = ( PropertyGridEx )PropertyGridHelper.GetPropertyGrid( rowItem.Definition );

            if ( propertyGrid == null )
            {
                return base.SelectTemplate( item, container );
            }
                
            var fullPath = rowItem.FullPath;
            var hasDot   = fullPath.Contains( "." );
            var pathName = hasDot ? fullPath.Substring( fullPath.LastIndexOf( ".", StringComparison.Ordinal ) + 1 ) : fullPath;
            var hasDotPd = hasDot ? propertyGrid.GetRowValueByRowPath( fullPath.Substring( 0, fullPath.LastIndexOf( ".", StringComparison.Ordinal ) ) ) : propertyGrid.SelectedObject;

            var pd = TypeDescriptor.GetProperties( hasDotPd ).Cast<PropertyDescriptor>( ).FirstOrDefault( p => p.Name == pathName );

            if ( pd == null )
            {
                return null;
            }
                

            TypeConverterAttribute cnvtAttr = pd.Attributes[ typeof( TypeConverterAttribute ) ] as TypeConverterAttribute;
            bool expandable = false;

            if ( cnvtAttr != null && cnvtAttr.ConverterTypeName == typeof( ExpandableObjectConverter ).AssemblyQualifiedName )
            {
                rowItem.EditSettings = new TextEditSettings( );
                rowItem.IsReadOnly   = true;
                expandable           = true;
            }

            var isa = pd.Attributes[ typeof( ItemsSourceAttribute ) ] as ItemsSourceAttribute;

            if ( isa != null )
            {
                return AddItemsSourceEditSettings( isa, rowItem, propertyGrid );
            }

            var ea = pd.Attributes[ typeof( EditorAttribute ) ] as EditorAttribute;

            if ( ea != null )
            {
                return GetEditorTemplate( item, container, ea, rowItem, expandable, propertyGrid.ReadOnly );
            }            
                
            if ( pd.PropertyType == typeof( EndPoint ) )
            {
                return GetEndPointEditorTemplate( propertyGrid, item, container, "EndPointEditorDataTemplate" );
            }
                
            if ( typeof( IEnumerable<EndPoint> ).IsAssignableFrom( pd.PropertyType ) )
            {
                return GetEndPointEditorTemplate( propertyGrid, item, container, "EndPointListEditorDataTemplate" );
            }
                
            if ( !( pd.PropertyType == typeof( TimeSpan ) ) && !( pd.PropertyType == typeof( TimeSpan? ) ) )
            {
                return base.SelectTemplate( item, container );
            }
                
            return GetTimeSpanTemplate( pd, rowItem );
        }

        private DataTemplate GetEndPointEditorTemplate(
                                                              FrameworkElement propetyGrid,
                                                              object rowDataItem,
                                                              DependencyObject container,
                                                              string templateName 
                                                     )
        {
            return ( DataTemplate )propetyGrid.FindResource( templateName ) ?? base.SelectTemplate( rowDataItem, container );
        }

        private DataTemplate GetEditorTemplate(
                                                  object rowDataItem,
                                                  DependencyObject container,
                                                  EditorAttribute editAttr,
                                                  RowData rowItem,
                                                  bool expandable,
                                                  bool isReadOnly )
        {
            Type type = editAttr.EditorTypeName.To<Type>( );

            if ( type.IsSubclassOf( typeof( BaseEditSettings ) ) )
            {
                rowItem.EditSettings = type.CreateInstance<BaseEditSettings>( );
                rowItem.IsReadOnly   = isReadOnly;
                return null;
            }

            if ( !type.IsSubclassOf( typeof( BaseEdit ) ) )
            {
                return base.SelectTemplate( rowDataItem, container );
            }
                
            FrameworkElementFactory newXaml = new FrameworkElementFactory( type );

            newXaml.SetBinding( BaseEdit.EditValueProperty, new Binding( )
                                                                            {
                                                                                Path                = new PropertyPath( "Value", new object[ 0 ] ),
                                                                                Mode                = BindingMode.TwoWay,
                                                                                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                                                                            } );
            newXaml.SetValue( BaseEdit.ShowBorderProperty, false );
            newXaml.SetValue( BaseEdit.EditModeProperty, EditMode.InplaceActive );
            newXaml.SetValue( BaseEdit.IsReadOnlyProperty, isReadOnly );

            DataTemplate t = new DataTemplate( );
            t.VisualTree = newXaml;
            t.Seal( );
            SetRowExpandable( rowItem, expandable );

            return t;
        }

        private static DataTemplate GetTimeSpanTemplate(
                                                          PropertyDescriptor pd,
                                                          RowData rowItem 
                                                      )
        {
            FrameworkElementFactory newXaml = new FrameworkElementFactory( typeof( TimeSpanEditor ) );

            newXaml.SetBinding( TimeSpanEditor.ValueProperty, new Binding( )
                                                                            {
                                                                                Path = new PropertyPath( "Value", new object[ 0 ] ),
                                                                                Mode = BindingMode.TwoWay
                                                                            } );

            newXaml.SetValue( TimeSpanEditor.IsNullableProperty, pd.PropertyType.IsNullable( ) );

            if ( pd.Attributes[ typeof( TimeSpanEditorAttribute ) ] is TimeSpanEditorAttribute attribute )
            {
                newXaml.SetValue( TimeSpanEditor.MaskProperty, attribute.Mask );
            }
                
            DataTemplate t = new DataTemplate( );
            t.VisualTree = newXaml;
            t.Seal( );

            SetRowExpandable( rowItem, false );
            return t;
        }

        private static DataTemplate AddItemsSourceEditSettings(
                                                                ItemsSourceAttribute isaAttr,
                                                                RowData rowItem,
                                                                PropertyGridEx grid 
                                                          )
        {
            ItemsSourceEditSettings isEdit = new ItemsSourceEditSettings( );
            isEdit.ComboBoxItems.AddRange( isaAttr.Type.CreateInstance<IItemsSource>( ).GetValues( ) );
            rowItem.SetBindings( RowData.IsReadOnlyProperty, grid, "ReadOnly", BindingMode.TwoWay, null, null );
            rowItem.EditSettings = isEdit;
            return null;
        }

        private static void SetRowExpandable( RowData rowItem, bool bool_0 )
        {
            rowItem.CanExpand                  = bool_0;
            rowItem.IsExpanded                 = false;
            rowItem.IsCollectionRow            = false;
            rowItem.IsModifiableCollectionItem = false;
        }        
    }

    internal sealed class MySpinEdit : SpinEdit
    {
        protected override TextInputSettingsBase CreateTextInputSettings( )
        {
            return new MyTextInputMaskSettings( this );
        }
    }

    internal sealed class MyTextInputMaskSettings : TextInputMaskSettings
    {
        public MyTextInputMaskSettings( TextEdit _param1 )
          : base( _param1 )
        {
        }

        protected override MaskManager CreateDefaultMaskManager( )
        {
            CultureInfo cultureInfo = ( ( TextEdit )OwnerEdit ).MaskCulture ?? CultureInfo.CurrentCulture;
            return new MyNumericeMaskManager( ( ( TextEdit )OwnerEdit ).Mask ?? string.Empty, cultureInfo, OwnerEdit.AllowNullInput );
        }
    }

    internal sealed class MyNumericeMaskManager : NumericMaskManager
    {
        public MyNumericeMaskManager( string _param1, CultureInfo _param2, bool _param3 )
          : base( _param1, _param2, _param3 )
        {
        }

        public override bool Backspace( )
        {
            string displayText = DisplayText;
            bool flag = base.Backspace( );

            if ( DisplayText == "0" && !string.IsNullOrEmpty( displayText ) )
            {
                flag = base.Backspace( );
            }

            return flag;
        }
    }

}
