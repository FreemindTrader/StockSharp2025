using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.TimePeriod;

namespace fx.Algorithm
{
    public interface IPivotPointIndicator
    {
        TimeSpan PivotTimeSpan { get; set; }

        double Pivot { get; }

        //DateTime BarTime { get; }

        double R1 { get;  }
        double R2 { get; }
        double R3 { get; }
        double S1 { get; }
        double S2 { get; }
        double S3 { get; }
        double M0 { get;  }
        double M1 { get; }
        double M2 { get; }
        double M3 { get; }
        double M4 { get; }
        double M5 { get; }

        bool DoneInitialDataLoad { get;  }

        //double Mdn { get;  }

        TimeBlockEx GetTimeBlock( DateTime barTime );
        PivotPointsInfo GetPivotPointsAt( long barTime, out TimeBlockEx responsibleBlock );        
        PivotPointsInfo GetPivotPointsAt( DateTime barTime, out TimeBlockEx responsibleBlock );

        //int GetPreviousTimeBlockAndIndex( TimeBlock lastTimeBlock, out TimeBlock previousTimeBlock );
        //int GetLatestPivotIndex( );

        //long GetIndexByTime( DateTime theDate );
        //DateTime GetPivotBartime( );

    }
}
