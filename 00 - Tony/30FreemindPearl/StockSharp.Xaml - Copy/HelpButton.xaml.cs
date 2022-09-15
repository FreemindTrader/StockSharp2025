using Ecng.Common;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class HelpButton : Button    
    {
        /// <summary>
		/// <see cref="DependencyProperty"/> for <see cref="HelpButton.DocUrl"/>.
		/// </summary>
		public static readonly DependencyProperty DocUrlProperty =
            DependencyProperty.Register("DocUrl", typeof(string), typeof(HelpButton), new PropertyMetadata(null, (o, args) =>
            {
                var btn = (HelpButton)o;
                btn.IsEnabled = !((string)args.NewValue).IsEmpty();
            }));

        /// <summary>
		/// <see cref="DependencyProperty"/> for <see cref="HelpButton.ShowText"/>.
		/// </summary>
		public static readonly DependencyProperty ShowTextProperty =
            DependencyProperty.Register("ShowText", typeof(bool), typeof(HelpButton), new PropertyMetadata(false, (o, args) =>
            {
                var btn = (HelpButton)o;
                var showText = (bool)args.NewValue;

                btn.ImgCtrl.Visibility = showText ? Visibility.Collapsed : Visibility.Visible;
                btn.TextCtrl.Visibility = !showText ? Visibility.Collapsed : Visibility.Visible;
            }));

        public HelpButton( )
        {
            InitializeComponent( );
        }

        public string DocUrl
        {
            get
            {
                return ( string ) this.GetValue( HelpButton.DocUrlProperty );
            }
            set
            {
                this.SetValue( HelpButton.DocUrlProperty, ( object ) value );
            }
        }

        public bool ShowText
        {
            get
            {
                return ( bool ) this.GetValue( HelpButton.ShowTextProperty );
            }
            set
            {
                this.SetValue( HelpButton.ShowTextProperty, ( object ) value );
            }
        }

        protected override void OnClick( )
        {
            HelpButton.CreateUrl( this.DocUrl ).TryOpenLink( ( DependencyObject ) this );
            base.OnClick( );
        }

        public static string CreateUrl( string docUrl )
        {
            return string.Format( "https://doc.stocksharp.{0}/html/{1}", ( object ) LocalizedStrings.Domain, ( object ) docUrl );
        }
    }
}
