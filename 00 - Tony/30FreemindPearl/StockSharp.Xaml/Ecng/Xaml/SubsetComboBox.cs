// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.SubsetComboBox
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

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
        
        private readonly Dictionary<object, int> \u0023\u003DzMOpu79KlRI1M = new Dictionary<object, int>();
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
                return new ComponentResourceKey( typeof( ComboBoxEditEx ), "SubsetComboBoxStyleKey") );
            }
        }

        /// <inheritdoc />
        protected override void UpdateBindings()
        {
            base.UpdateBindings();
            this.\u0023\u003DzMOpu79KlRI1M.Clear();
            IList itemsSource = ( ( LookUpEditBase )this ).get_ItemsSource() as IList;
            if ( itemsSource == null )
                return;
            for ( int index = 0; index < itemsSource.Count; ++index )
            {
                object valueFromItem = ( ( LookUpEditSettingsBase )this.get_Settings() ).GetValueFromItem( itemsSource[index] );
                if ( valueFromItem != null )
                    this.\u0023\u003DzMOpu79KlRI1M[valueFromItem] = index;
            }
        }

        /// <inheritdoc />
        protected virtual object CoerceEditValue( DependencyObject d, object value )
        {
            Type valueType = this.Source?.ValueType;
            if ( value == null && valueType != ( Type )null )
                value = typeof( List<> ).Make( valueType ).CreateInstance();
            return ( ( BaseEdit )this ).CoerceEditValue( d, value );
        }

        /// <inheritdoc />
        protected override object TryConvertEditValue( object ev )
        {
            SubsetComboBox.\u0023\u003DzotccE3NUKIPTq1tNYza9wtI\u003D nukipTq1tNyza9wtI = new SubsetComboBox.\u0023\u003DzotccE3NUKIPTq1tNYza9wtI\u003D();
            nukipTq1tNyza9wtI.\u0023\u003DzIxf8wCc\u003D = this.Source?.ValueType;
            if ( nukipTq1tNyza9wtI.\u0023\u003DzIxf8wCc\u003D == ( Type )null)
        return ( object )null;
            IList instance = ( IList )typeof( List<> ).Make( nukipTq1tNyza9wtI.\u0023\u003DzIxf8wCc\u003D).CreateInstance();
            IEnumerable source = ev as IEnumerable;
            if ( source == null )
                return ( object )instance;
            object[ ] array = source.Cast<object>().ToArray<object>();
            if ( ( ( IEnumerable<object> )array ).All<object>( new Func<object, bool>( nukipTq1tNyza9wtI.\u0023\u003DzSf_4WQTZnrl\u0024vNo_5hrb7pc\u003D) ) )
            {
                foreach ( object obj in array )
                    instance.Add( obj );
                return ( object )instance;
            }
            if ( !( ( IEnumerable<object> )array ).All<object>( SubsetComboBox.SomeShit.\u0023\u003DzcK_qtiOD6nZ_G9q6Jw\u003D\u003D ?? ( SubsetComboBox.SomeShit.\u0023\u003DzcK_qtiOD6nZ_G9q6Jw\u003D\u003D = new Func<object, bool>( SubsetComboBox.SomeShit.ShitMethod02.\u0023\u003Dz39aGTFGGJNpC1ZUMlLsov7Y\u003D) ) ))
        return ( object )instance;
            foreach ( object obj in array )
                instance.Add( ( ( IItemsSourceItem )obj ).Value );
            return ( object )instance;
        }

        /// <inheritdoc />
        protected override object TryConvertBaseValue(
          SelectorPropertiesCoercionHelper helper,
          object baseValue )
        {
            IEnumerable source = baseValue as IEnumerable;
            if ( source != null && !( baseValue is IList ) )
                baseValue = ( object )source.Cast<object>().ToList<object>();
            return base.TryConvertBaseValue( helper, baseValue );
        }

        /// <inheritdoc />
        protected virtual string GetDisplayText( object editValue, bool applyFormatting )
        {
            IList source = editValue as IList;
            if ( source != null )
            {
                if ( this.DisplaySelectedItemsCount )
                    return LocalizedStrings.Str1852Params.Put( ( object )source.Count );
                editValue = ( object )source.Cast<object>().OrderBy<object, int>( new Func<object, int>( this.\u0023\u003DzaQs\u0024JG9l\u0024W\u0024B ) ).ToList<object>();
            }
            return ( ( BaseEdit )this ).GetDisplayText( editValue, applyFormatting );
        }

        /// <inheritdoc />
        protected override BaseEditSettings CreateEditorSettings()
        {
            return ( BaseEditSettings )new SubsetComboBoxSettings();
        }

        private int \u0023\u003DzaQs\u0024JG9l\u0024W\u0024B( object _param1 )
        {
            int num;
            if ( _param1 != null && this.\u0023\u003DzMOpu79KlRI1M.TryGetValue( _param1, out num ))
        return num;
            return -1;
        }

        [Serializable]
        private sealed class SomeShit
    {
      public static readonly SubsetComboBox.SomeShit ShitMethod02 = new SubsetComboBox.SomeShit();
      public static Func<object, bool> \u0023\u003DzcK_qtiOD6nZ_G9q6Jw\u003D\u003D;

      internal bool \u0023\u003Dz39aGTFGGJNpC1ZUMlLsov7Y\u003D(object _param1)
      {
        return _param1 is IItemsSourceItem;
      }
}

private sealed class \u0023\u003DzotccE3NUKIPTq1tNYza9wtI\u003D
    {
      public Type \u0023\u003DzIxf8wCc\u003D;

internal bool \u0023\u003DzSf_4WQTZnrl\u0024vNo_5hrb7pc\u003D( object _param1)
      {
    return this.\u0023\u003DzIxf8wCc\u003D.IsInstanceOfType( _param1 );
}
    }
  }
}
