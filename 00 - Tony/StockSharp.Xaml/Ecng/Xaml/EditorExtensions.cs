using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    public static class EditorExtensions
    {
        /// <summary>
        /// </summary>
        public static void AddClearButton( this ButtonEdit edit, object editValue = null )
        {
            if ( edit == null )
            {
                throw new ArgumentNullException( "edit == null" );
            }

            var btnInfo = new ButtonInfo();
            btnInfo.GlyphKind = GlyphKind.Cancel;
            btnInfo.Content = LocalizedStrings.XamlStr440;
            
            btnInfo.SetBindings( ContentElement.IsEnabledProperty, edit, "IsReadOnly", BindingMode.TwoWay, ( IValueConverter )new InverseBooleanConverter(), null );
            btnInfo.Click += ( p, e ) =>
            {
                if ( edit.IsReadOnly )
                    return;

                edit.SetCurrentValue( ( DependencyProperty )BaseEdit.EditValueProperty, editValue );
            };

            edit.Buttons.Add( btnInfo );
        }

        /// <summary>
        /// </summary>
        public static void RemoveClearButton( this ButtonEdit edit )
        {
            if ( edit == null )
            {
                throw new ArgumentNullException( "edit == null" );
            }

            var btnInfo = ( ( IEnumerable<ButtonInfo> )edit.Buttons ).FirstOrDefault<ButtonInfo>( p => p.GlyphKind == GlyphKind.Cancel );
            if ( btnInfo == null )
                return;

            edit.Buttons.Remove( btnInfo );
        }

        /// <summary>
        /// </summary>
        public static void AddClearButton( this ComboBoxEditSettings editSettings, object editValue = null )
        {
            if ( editSettings == null )
            {
                throw new ArgumentNullException( "editSettings == null" );
            }


            var btnInfo = new ButtonInfo();
            btnInfo.GlyphKind = GlyphKind.Cancel;
            btnInfo.Content = LocalizedStrings.XamlStr440;

            btnInfo.Click += ( p, e ) =>
            {
                BaseEdit o = BaseEdit.GetOwnerEdit( ( DependencyObject )p );
                if ( o == null || o.IsReadOnly )
                    return;
                ( ( DependencyObject )o ).SetCurrentValue( ( DependencyProperty )BaseEdit.EditValueProperty, editValue );
            };

            editSettings.Buttons.Add( btnInfo );
        }

        /// <summary>
        /// </summary>
        public static IItemsSource ToItemsSource( this object val, Type itemValueType, bool? excludeObsolete = null, ListSortDirection? sortOrder = null, Func<IItemsSourceItem, bool> filter = null, Func<object, string> getName = null, Func<object, string> getDescription = null )
        {
            return ItemsSourceBase.Create( val, itemValueType, excludeObsolete, sortOrder, filter, getName, getDescription );
        }

        /// <summary>
        /// </summary>
        public static IItemsSource ToItemsSource<T>( this IEnumerable<T> val, bool excludeObsolete = true, ListSortDirection? sortOrder = null, Func<IItemsSourceItem, bool> filter = null, Func<T, string> getName = null, Func<T, string> getDescription = null )
        {
            return ( IItemsSource )new ItemsSourceBase<T>( ( IEnumerable )val, excludeObsolete, sortOrder, filter, getName, getDescription );
        }

        /// <summary>
        /// </summary>
        public static void SetItemsSource<T>( this ComboBoxEditEx cb ) where T : Enum
        {
            cb.SetItemsSource( typeof( T ) );
        }

        /// <summary>
        /// </summary>
        public static void SetItemsSource( this ComboBoxEditEx cb, Type enumType )
        {
            if ( cb == null )
            {
                throw new ArgumentNullException( "cb == null" );
            }

            if ( !enumType.IsEnum )
            {
                throw new ArgumentException( enumType.FullName + "Is Not an Enum" );
            }

            cb.SetItemsSource( enumType.GetValues().ToItemsSource( enumType, new bool?(), new ListSortDirection?(), ( Func<IItemsSourceItem, bool> )null, ( Func<object, string> )null, ( Func<object, string> )null ) );
        }

        /// <summary>
        /// </summary>
        public static void SetItemsSource<T>( this ComboBoxEditEx cb, IEnumerable<T> values, Func<T, string> getName = null, Func<T, string> getDescription = null )
        {
            if ( cb == null )
            {
                throw new ArgumentNullException( "cb == null" );
            }


            ListSortDirection? sortOrder = new ListSortDirection?();

            var itemsSource = values.ToItemsSource<T>( true, sortOrder, ( Func<IItemsSourceItem, bool> )null, getName, getDescription );

            cb.SetItemsSource( itemsSource );
        }

        /// <summary>
        /// </summary>
        public static void SetItemsSource( this ComboBoxEditEx cb, IItemsSource source )
        {
            if ( cb == null )
            {
                throw new ArgumentNullException( "cb == null" );
            }

            cb.ItemsSource = source;
        }

        /// <summary>
        /// </summary>
        public static T GetSelected<T>( this ComboBoxEditEx cb )
        {
            return ( T )cb.Value;
        }

        /// <summary>
        /// </summary>
        public static IEnumerable<T> GetSelecteds<T>( this ComboBoxEditEx cb )
        {
            return cb.GetSelected<IEnumerable<T>>();
        }

        /// <summary>
        /// </summary>
        public static void SetSelected<T>( this ComboBoxEditEx cb, T value )
        {
            cb.Value = ( object )value;
        }
    }
}
