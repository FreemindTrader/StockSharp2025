using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public interface IMutltiTimeFrameSessionDataRepo
    {
        //fxHistoricBarsRepo GetDatabarRepo( TimeSpan period );

        string Symbol
        {
            get;
        }

        Security Security
        {
            get;
        }
    }
}
