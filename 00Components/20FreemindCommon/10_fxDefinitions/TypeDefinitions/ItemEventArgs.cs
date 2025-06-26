using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public class ItemEventArgs<T> : EventArgs
    {
        public ItemEventArgs( T item )
        {
            Item = item;
        }

        public T Item { get; protected set; }

        public static implicit operator ItemEventArgs<T>( T item )
        {
            return new ItemEventArgs<T>( item );
        }
    }

    public delegate void ItemEventHandler<T>( object sender, ItemEventArgs<T> e );
    /// <summary>
    /// A general helper class, contains all kinds of routines that help the general operation of an application.
    /// </summary>
}
