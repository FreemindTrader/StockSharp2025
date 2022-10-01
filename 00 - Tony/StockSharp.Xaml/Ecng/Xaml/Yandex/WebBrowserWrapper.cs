
//using Disk.SDK;
//using System;
//using System.Diagnostics;
//using System.Windows.Controls;
//using System.Windows.Navigation;

//namespace Ecng.Xaml.Yandex
//{
//    /// <summary>
//    /// Represents wrapper for platform specific WebBrowser component.
//    /// </summary>
//    public class WebBrowserWrapper : IBrowser
//    {

//        private readonly WebBrowser _browser;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="T:Ecng.Xaml.Yandex.WebBrowserWrapper" /> class.
//        /// </summary>
//        /// <param name="browser">The browser.</param>
//        public WebBrowserWrapper( WebBrowser browser )
//        {
//            this._browser = browser;
//            this._browser.Navigating += new NavigatingCancelEventHandler( this.OnBrowserNavigating );
//        }

//        /// <summary>Navigates to the specified URL.</summary>
//        /// <param name="url">The URL.</param>
//        public void Navigate( string url )
//        {
//            this._browser.Navigate( new Uri( url ) );
//        }

//        /// <summary>Occurs just before navigation to a document.</summary>
//        public event EventHandler<GenericSdkEventArgs<string>> Navigating;

//        private void OnBrowserNavigating( object _param1, NavigatingCancelEventArgs _param2 )
//        {
//            EventHandler<GenericSdkEventArgs<string>> zX5CurS0 = this.Navigating;
//            if ( zX5CurS0 == null )
//                return;
//            zX5CurS0( ( object )this, new GenericSdkEventArgs<string>( _param2.Uri.ToString() ) );
//        }
//    }
//}
