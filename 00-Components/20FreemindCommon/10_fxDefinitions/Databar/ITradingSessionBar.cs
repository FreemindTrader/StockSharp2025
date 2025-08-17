using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IBasicBar
    {
        long LinuxTime { get; }
        TimeSpan BarPeriod { get; }

        DateTime BarTime { get; }
    }
    public interface ITradingSessionBar : IAskBidBar   
    {        
        
        
        double          Average                             { get;      }
        
        DateTime        LondonTime                          { get;      }
        DateTime        ChinaTime                           { get;      }
        DateTime        FrankfurtTime                       { get;      }
        DateTime        NewYorkTime                         { get;      }  
        DateTime        WellingtonTime                      { get;      }  
        DateTime        TokyoTime                           { get;      }  

        bool            IsFalling                           { get;      }
        bool            IsRising                            { get;      }
        bool            IsActiveHour                        { get;      }
        
        Security     Symbol                              { get; set; }
        
        bool            HasDataValues                       { get;      }
        

        
        long             BarIndex                            { get; set; }

        int Index { get; }

        SessionEnum  BarSession { get;      }

        double          GetValue( DataBarProperty valueSource );
        bool            GetNotActiveHours( out DateTime nonActiveBegin, out DateTime nonActiveEnd );
        
        string          ToString( );
    }

    public enum BarPeriod
    {
        NA,
        t1,
        S1,
        m1,
        m4,
        m5,
        m15,
        m30,
        H1,
        H2,
        H3,
        H4,
        H6,
        H8,
        D1,
        W1,
        M1
    }

    public enum DataBarProperty
    {
        High,
        Low,
        Open,
        Close,
        Average,
        Volume,    
        BodyHeight,
        BarTime
    }

    

    //public struct TradingSession
    //{        
    //    public int SessionBeginIndex;

    //    public int SessionEndIndex;
    //}

    
}
