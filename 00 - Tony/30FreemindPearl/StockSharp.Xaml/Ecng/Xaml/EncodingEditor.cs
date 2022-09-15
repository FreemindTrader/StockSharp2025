using DevExpress.Xpf.Editors.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecng.Xaml
{
    /// <summary>
    /// <see cref="T:DevExpress.Xpf.Editors.Settings.ComboBoxEditSettings" /> for <see cref="T:System.Text.Encoding" />.
    ///     </summary>
    public class EncodingEditor : ComboBoxEditSettings
    {
        /// <summary>
        /// </summary>
        public EncodingEditor()
        {
            DisplayMember = ( nameof( 2127281049 ) );
            ValueMember = ( nameof( 2127281045 ) );
            ItemsSource = Encoding.GetEncodings().Select<EncodingInfo, Tuple<Encoding, string>>( p => new Tuple<Encoding, string>( p.GetEncoding(), p.DisplayName ) ).ToArray<Tuple<Encoding, string>>();
            this.AddClearButton( ( object )null );
        }

    }
}
