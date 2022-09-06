using fx.Collections;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class SelectSecurityMessage
    {
        public Security Symbol { get; set; }      
        
        public PooledList<TimeSpan> SelectedTF { get; set; }

        public SelectSecurityMessage( Security symbol )
        {
            this.Symbol = symbol;
            SelectedTF = null;
        }

        public SelectSecurityMessage( Security symbol, PooledList<TimeSpan> selected )
        {
            this.Symbol = symbol;
            SelectedTF = selected;
        }
    }
}
