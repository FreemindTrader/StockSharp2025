using System;

namespace StockSharp.Studio.Core.Commands
{
    public class SelectCommand<T> : SelectCommand
    {
        new public T Instance
        {
            get
            {
                return ( T )base.Instance;
            }
        }

        public SelectCommand( T instance, bool canEdit )
          : base( typeof( T ), instance, canEdit )
        {
        }

        public SelectCommand( T instance, Func<bool> canEdit )
          : base( typeof( T ), instance, canEdit )
        {
        }
    }
}
