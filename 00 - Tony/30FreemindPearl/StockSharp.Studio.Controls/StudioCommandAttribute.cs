
using System;

namespace StockSharp.Studio.Controls
{
    public class StudioCommandAttribute : Attribute
    {
        public Type CommandType { get; }

        public StudioCommandAttribute( Type commandType )
        {
            Type type = commandType;
            if ( ( object )type == null )
                throw new ArgumentNullException( nameof( commandType ) );
            this.CommandType = type;
        }
    }
}
