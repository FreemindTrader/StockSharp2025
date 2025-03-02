﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IDetailedPosition : ITrade 
    {        
        double  UsedMargin          { get; }       
        IDetailedPosition Clone( );
    }
}
