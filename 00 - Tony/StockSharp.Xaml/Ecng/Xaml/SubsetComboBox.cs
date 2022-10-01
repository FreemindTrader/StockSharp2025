using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Reflection;
using StockSharp.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Ecng.Xaml
{
    /// <summary>The drop-down list to select a set of fields.</summary>
    public class SubsetComboBox : ComboBoxEditEx
    {

        private readonly Dictionary<object, int> _subnetPositionsIndex = new Dictionary<object, int>();
        /// <summary>Display selected items count.</summary>
        public static readonly DependencyProperty DisplaySelectedItemsCountProperty = DependencyProperty.Register( nameof( DisplaySelectedItemsCount ), typeof( bool ), typeof( SubsetComboBox ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )false ) );

        static SubsetComboBox()
        {
            SubsetComboBoxSettings.RegisterDefaultUserEditor();
        }

        /// <inheritdoc />
        public SubsetComboBox()
        {
            ( ( FrameworkElement )this ).Style = ( Style )Application.Current.FindResource( ( object )SubsetComboBox.SubsetComboBoxStyleKey );
        }

        /// <summary>Display selected items count.</summary>
        public bool DisplaySelectedItemsCount
        {
            get
            {
                return ( bool )( ( DependencyObject )this ).GetValue( SubsetComboBox.DisplaySelectedItemsCountProperty );
            }
            set
            {
                ( ( DependencyObject )this ).SetValue( SubsetComboBox.DisplaySelectedItemsCountProperty, ( object )value );
            }
        }

        /// <summary>
        /// </summary>
        public static ComponentResourceKey SubsetComboBoxStyleKey
        {
            get
            {
                return new ComponentResourceKey( typeof( ComboBoxEditEx ), "SubsetComboBoxStyle" );
            }
        }

        /// <inheritdoc />
        protected override void UpdateBindings()
        {
            base.UpdateBindings();
            this._subnetPositionsIndex.Clear();

            IList subnetItemSource = ( ( LookUpEditBase )this ).ItemsSource as IList;
            
            if ( subnetItemSource == null )
            {
                return;
            }
                
            for ( int index = 0; index < subnetItemSource.Count; ++index )
            {
                object subnetValue = ( ( LookUpEditSettingsBase )this.Settings ).GetValueFromItem( subnetItemSource[index] );
                
                if ( subnetValue != null )
                {
                    this._subnetPositionsIndex[subnetValue] = index;
                }
                    
            }
        }

        /// <inheritdoc />
        protected override object CoerceEditValue( DependencyObject d, object value )
        {
            Type valueType = this.Source?.ValueType;
            
            if ( value == null && valueType != null )
            {
                value = ReflectionHelper.CreateInstance( TypeHelper.Make( typeof( List<> ), valueType ) );
            }

            return base.CoerceEditValue( d, value );
        }

        /// <inheritdoc />
        protected override object TryConvertEditValue( object ev )
        {
            if ( this.Source?.ValueType == null )
            {
                return null;
            }
                
            IList instance = ( IList ) ReflectionHelper.CreateInstance( TypeHelper.Make( typeof( List<> ), new Type[1] { this.Source?.ValueType } ) );
            IEnumerable source = ev as IEnumerable;
            
            if ( source == null )
            {
                return instance;
            }
                
            object[ ] array = source.Cast<object>().ToArray<object>();
            
            if ( array.All<object>( p => this.Source.ValueType.IsInstanceOfType( p ) ) )
            {
                foreach ( object obj in array )
                {
                    instance.Add( obj );
                }
                    
                return instance;
            }

            if ( ! array.All<object>( p => p is IItemsSourceItem ) )
            {
                return instance;
            }
                

            foreach ( object obj in array )
            {
                instance.Add( ( ( IItemsSourceItem )obj ).Value );
            }
                
            return instance;
        }


        /// <inheritdoc />
        protected override object TryConvertBaseValue( SelectorPropertiesCoercionHelper helper, object baseValue )
        {
            IEnumerable source = baseValue as IEnumerable;
            
            if ( source != null && !( baseValue is IList ) )
            {
                baseValue = ( object )source.Cast<object>().ToList<object>();
            }
                
            return base.TryConvertBaseValue( helper, baseValue );
        }

        /// <inheritdoc />
        protected override string GetDisplayText( object editValue, bool applyFormatting )
        {
            IList editValueList = editValue as IList;

            if ( editValueList != null )
            {
                if ( this.DisplaySelectedItemsCount )
                {
                    return LocalizedStrings.Str1852Params.Put( editValueList.Count );
                }

                editValue = editValueList.Cast<object>().OrderBy<object, int>( p => {
                                                                                        int num;
                                                                                        if ( p != null && this._subnetPositionsIndex.TryGetValue( p, out num ) )
                                                                                        {
                                                                                            return num;
                                                                                        }
                        
                                                                                        return -1;
                                                                                    } 
                                                                             ).ToList<object>();
                
            }
            return base.GetDisplayText( editValue, applyFormatting );
        }

        /// <inheritdoc />
        protected override BaseEditSettings CreateEditorSettings()
        {
            return ( BaseEditSettings )new SubsetComboBoxSettings();
        }
    }
}
