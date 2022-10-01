using System.Windows;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    public class BindingProxy : Freezable
    {
        /// <summary>
        /// </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register( nameof( Data ), typeof( object ), typeof( BindingProxy ), ( PropertyMetadata )new UIPropertyMetadata( ( PropertyChangedCallback )null ) );

        public BindingProxy()
        {

        }

        /// <summary>
        /// </summary>
        public object Data
        {
            get
            {
                return GetValue( BindingProxy.DataProperty );
            }
            set
            {
                SetValue( BindingProxy.DataProperty, value );
            }
        }

        /// <summary>
        /// </summary>
        protected override Freezable CreateInstanceCore()
        {
            return ( Freezable )new BindingProxy();
        }
    }
}
