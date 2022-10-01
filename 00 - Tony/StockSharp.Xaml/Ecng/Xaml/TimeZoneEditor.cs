using DevExpress.Xpf.Editors;
using System;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    public class TimeZoneEditor : ComboBoxEdit
    {
        /// <summary>
        /// </summary>
        public TimeZoneEditor()
        {
            this.ItemsSource = ( object )TimeZoneInfo.GetSystemTimeZones();
            this.DisplayMember = "DisplayName";
            this.IsTextEditable = false;
            ( ( ButtonEdit )this ).AddClearButton( null );
        }

        /// <summary>
        /// </summary>
        public TimeZoneInfo TimeZone
        {
            get
            {
                return ( TimeZoneInfo )this.EditValue;
            }
            set
            {
                this.EditValue = ( object )value;
            }
        }
    }
}
