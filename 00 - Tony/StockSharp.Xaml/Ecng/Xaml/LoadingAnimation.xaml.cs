using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ecng.Xaml
{
    public partial class LoadingAnimation : UserControl
    {
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register( nameof( IsBusy ), typeof( bool ), typeof( LoadingAnimation ), new PropertyMetadata( ( object )false ) );

        public LoadingAnimation()
        {
            InitializeComponent();
        }

        public bool IsBusy
        {
            get
            {
                return ( bool )this.GetValue( LoadingAnimation.IsBusyProperty );
            }
            set
            {
                this.SetValue( LoadingAnimation.IsBusyProperty, ( object )value );
            }
        }

        public string AnimationText
        {
            get
            {
                return this.AnimationTextBlock.Text;
            }
            set
            {
                this.AnimationTextBlock.Text = value;
            }
        }
    }
}
