using Ecng.Common;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Ecng.Xaml
{
    /// <summary>
    /// Extended version <see cref="T:System.Windows.Documents.Hyperlink" /> that opens link automatically.
    /// </summary>
    public class HyperlinkEx : Hyperlink
    {

        private static readonly Uri _defaultLink = new Uri( ".", UriKind.RelativeOrAbsolute );

        static HyperlinkEx()
        {
            Hyperlink.NavigateUriProperty.OverrideMetadata( typeof( HyperlinkEx ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )HyperlinkEx._defaultLink ) );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Ecng.Xaml.HyperlinkEx" />.
        /// </summary>
        public HyperlinkEx()
        {
            this.RequestNavigate += new RequestNavigateEventHandler( this.Hyperlink_OnRequestNavigate );
        }

        private void Hyperlink_OnRequestNavigate(
          object _param1,
          RequestNavigateEventArgs _param2 )
        {
            if ( this.NavigateUri == HyperlinkEx._defaultLink )
                return;
            IOHelper.OpenLink( this.NavigateUri.ToString(), false );
            _param2.Handled = true;
        }
    }
}
