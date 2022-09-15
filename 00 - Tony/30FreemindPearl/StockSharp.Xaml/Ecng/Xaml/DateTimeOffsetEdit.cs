using DevExpress.Xpf.Editors;

namespace Ecng.Xaml
{
    /// <summary>
    /// <see cref="T:DevExpress.Xpf.Editors.DateEdit" /> for <see cref="T:System.DateTimeOffset" />.
    ///     </summary>
    public class DateTimeOffsetEdit : DateEdit
    {
        static DateTimeOffsetEdit()
        {
            DateTimeOffsetEditor.RegisterCustomEdit();
        }

        /// <summary>
        /// </summary>
        public DateTimeOffsetEdit()
        {            
            ValidateOnTextInput = false;
        }

        /// <inheritdoc />
        protected override EditStrategyBase CreateEditStrategy()
        {
            return ( EditStrategyBase )new DateTimeOffsetEditStrategy( this );
        }
    }
}
