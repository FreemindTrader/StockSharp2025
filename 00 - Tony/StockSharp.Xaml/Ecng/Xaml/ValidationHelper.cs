// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.ValidationHelper
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Validation;
using Ecng.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// </summary>
        public static readonly DependencyProperty ValidationRulesProperty = DependencyProperty.RegisterAttached( "ValidationRules", typeof( ValidationRulesCollection ), typeof( ValidationHelper ), new PropertyMetadata( ( PropertyChangedCallback )null ) );
        /// <summary>
        /// </summary>
        public static readonly DependencyProperty BaseEditProperty = DependencyProperty.RegisterAttached( "BaseEdit", typeof( BaseEdit ), typeof( ValidationHelper ), new PropertyMetadata( ( object )null, new PropertyChangedCallback( OnBaseEditPropertyChangedCallback ) ) );

        private static readonly Dictionary<BaseEditSettings, BaseEdit> _edits = new Dictionary<BaseEditSettings, BaseEdit>();

        /// <summary>
        /// </summary>
        public static ValidationRulesCollection GetValidationRules(
          DependencyObject obj )
        {
            return ( ValidationRulesCollection )obj.GetValue( ValidationHelper.ValidationRulesProperty );
        }

        /// <summary>
        /// </summary>
        public static void SetValidationRules( DependencyObject obj, ValidationRulesCollection value )
        {
            obj.SetValue( ValidationHelper.ValidationRulesProperty, ( object )value );
        }

        /// <summary>
        /// </summary>
        public static BaseEdit GetBaseEdit( BaseEditSettings settings )
        {
            return ( BaseEdit )( ( DependencyObject )settings ).GetValue( ValidationHelper.BaseEditProperty );
        }

        /// <summary>
        /// </summary>
        public static void SetBaseEdit( BaseEditSettings settings, BaseEdit edit )
        {
            ( ( DependencyObject )settings ).SetValue( ValidationHelper.BaseEditProperty, ( object )edit );
        }

        private static void OnBaseEditPropertyChangedCallback( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BaseEditSettings settings = d as BaseEditSettings;
            if ( settings == null )
                return;

            var editSetting = _edits.TryGetValue<BaseEditSettings, BaseEdit>( settings );

            editSetting.Validate += OnEditSettingsValidation;

            if ( e.NewValue == null )
                return;
            BaseEdit newValue = ( BaseEdit )e.NewValue;
            ValidationHelper._edits[settings] = newValue;

            newValue.Validate += OnEditSettingsValidation;
        }

        private static void OnEditSettingsValidation( object o, ValidationEventArgs _param1 )
        {
            List<ValidationRule> rules = ValidationHelper.GetValidationRules( ( DependencyObject )( ValidationHelper._edits ).FirstOrDefault( p => object.Equals( p.Value, o ) ).Key);
            


            foreach ( ValidationRule validationRule in rules )
            {
                ValidationResult validationResult = validationRule.Validate( _param1.Value, CultureInfo.CurrentCulture );
                if ( !validationResult.IsValid )
                {
                    _param1.IsValid = ( false );
                    _param1.ErrorContent = ( validationResult.ErrorContent );
                    break;
                }
            }
        }        
    }
}
