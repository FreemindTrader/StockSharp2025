using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public class FxBindingList<T> : BindingList< T >
    {
        public event ListChangedEventHandler Removing;

        protected void OnRemoving( ListChangedEventArgs e )
        {
            Removing?.Invoke( this, e );
        }

        protected override void RemoveItem( int index )
        {
            if ( index > -1 && index < Count )
            {
                OnRemoving(( new ListChangedEventArgs( ListChangedType.ItemDeleted, index )));
            }

            base.RemoveItem( index );
        }
    }
}
