using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using System;

namespace Ecng.Xaml
{
    /// <summary>
    /// <see cref="T:DevExpress.Xpf.Editors.Settings.DateEditSettings" /> for <see cref="T:System.DateTimeOffset" />.
    ///     </summary>
    public class DateTimeOffsetEditor : DateEditSettings
    {
        static DateTimeOffsetEditor()
        {
            DateTimeOffsetEditor.RegisterCustomEdit();
        }

        public DateTimeOffsetEditor()
        {
        }

        /// <summary>
        /// </summary>
        public static void RegisterCustomEdit()
        {
            EditorSettingsProvider.Default.RegisterUserEditor( typeof( DateTimeOffsetEdit ),
                                                                typeof( DateTimeOffsetEditor ),
                                                                () => new DateTimeOffsetEdit(),
                                                                () => new DateTimeOffsetEditor() );
        }
    }
}
