
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.EditStrategy;
using DevExpress.Xpf.Editors.Services;
using DevExpress.Xpf.Editors.Validation.Native;
using Ecng.Common;
using System;
using System.Windows;

namespace Ecng.Xaml
{
    /// <summary>
    /// <see cref="T:DevExpress.Xpf.Editors.DateEditStrategy" /> for <see cref="T:System.DateTimeOffset" />.
    ///     </summary>
    public class DateTimeOffsetEditStrategy : DateEditStrategy
    {
        /// <summary>
        /// </summary>
        public DateTimeOffsetEditStrategy( DateTimeOffsetEdit edit ) : base( ( DateEdit )edit )
        {

        }

        /// <inheritdoc />
        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();

            this.PropertyUpdater.Register( ( DependencyProperty )BaseEdit.EditValueProperty, new PropertyCoercionHandler( OnProvideEditValueMethod ), new PropertyCoercionHandler( OnProvideEditValueMethod ) );
        }

        public override bool ProvideEditValue( object value, out object provideValue, UpdateEditorSource updateSource )
        {
            ( ( RangeEditorStrategy<DateTime> )this ).ProvideEditValue( value, out provideValue, updateSource );
            provideValue = OnProvideEditValueMethod( value );
            return true;
        }

        public static object OnProvideEditValueMethod( object p )
        {
            if ( p is DateTimeOffset )
                return p;
            if ( p is DateTime )
                return ( object )new DateTimeOffset( ( DateTime )p );
            string str = p as string;
            if ( str.IsEmpty() )
                return ( object )null;
            DateTimeOffset result;
            if ( DateTimeOffset.TryParse( str, out result ) )
                return ( object )result;
            return ( object )null;
        }



        /// <inheritdoc />
        protected override EditorSpecificValidator CreateEditorValidatorService()
        {
            base.CreateEditorValidatorService();
            return ( EditorSpecificValidator )new EditorSpecificValidator( this.Editor );
        }
    }
}
