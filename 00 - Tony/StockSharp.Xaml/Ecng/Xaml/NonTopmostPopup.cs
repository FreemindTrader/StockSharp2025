using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;

namespace Ecng.Xaml
{
    /// <summary>Non topmost popup.</summary>
    /// <remarks>http://chriscavanagh.wordpress.com/2008/08/13/non-topmost-wpf-popup/</remarks>
    public class NonTopmostPopup : Popup
    {
        /// <summary>
        /// </summary>
        public static DependencyProperty TopmostProperty = Window.TopmostProperty.AddOwner( typeof( NonTopmostPopup ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )false, new PropertyChangedCallback( OnTopmostPropertyChanged ) ) );
        /// <summary>
        /// </summary>
        public static readonly DependencyProperty NoActivateProperty = DependencyProperty.Register( nameof( NoActivate ), typeof( bool ), typeof( NonTopmostPopup ), new PropertyMetadata( ( object )false, new PropertyChangedCallback( OnTopmostPropertyChanged ) ) );

        /// <summary>
        /// </summary>
        public bool Topmost
        {
            get
            {
                return ( bool )this.GetValue( NonTopmostPopup.TopmostProperty );
            }
            set
            {
                this.SetValue( NonTopmostPopup.TopmostProperty, ( object )value );
            }
        }

        /// <summary>
        /// </summary>
        public bool NoActivate
        {
            get
            {
                return ( bool )this.GetValue( NonTopmostPopup.NoActivateProperty );
            }
            set
            {
                this.SetValue( NonTopmostPopup.NoActivateProperty, ( object )value );
            }
        }

        private static void OnTopmostPropertyChanged(
          DependencyObject d,
          DependencyPropertyChangedEventArgs e )
        {
            ( ( NonTopmostPopup )d ).UpdateWindow();
        }

        /// <inheritdoc />
        protected override void OnOpened( EventArgs e )
        {
            this.UpdateWindow();
            base.OnOpened( e );
        }

        private void UpdateWindow()
        {
            if ( this.Child == null )
                return;
            PresentationSource presentationSource = PresentationSource.FromVisual( ( Visual )this.Child );
            if ( presentationSource == null )
                return;
            IntPtr handle = ( ( HwndSource )presentationSource ).Handle;
            NonTopmostPopup.RECT lpRect;
            if ( !NonTopmostPopup.GetWindowRect( handle, out lpRect ) )
                return;
            NonTopmostPopup.SetWindowPos( handle, this.Topmost ? -1 : -2, lpRect.Left, lpRect.Top, ( int )this.Width, ( int )this.Height, this.NoActivate ? 16 : 0 );
        }

        [DllImport( "user32.dll", EntryPoint = "GetWindowRect" )]
        [return: MarshalAs( UnmanagedType.Bool )]
        private static extern bool GetWindowRect( IntPtr hWnd, out NonTopmostPopup.RECT lpRect );

        [DllImport( "user32", EntryPoint = "SetWindowPos" )]
        private static extern int SetWindowPos( IntPtr hWnd, int hwndInsertAfter, int x, int y, int cx, int cy, int wFlags );

        /// <summary>
        /// </summary>
        public struct RECT
        {
            /// <summary>
            /// </summary>
            public int Left;
            /// <summary>
            /// </summary>
            public int Top;
            /// <summary>
            /// </summary>
            public int Right;
            /// <summary>
            /// </summary>
            public int Bottom;
        }
    }
}
