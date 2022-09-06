using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Bars
{
    public interface IAdvancedAnalysisManager
    {
        IPeriodXTaManager GetPeriodXTa( TimeSpan period );

        IHewManager HewManager { get; }

        void RaiseMacdValueChange( Security security, TimeSpan period, (double macd, double macdSig) value, DateTime valueTime );
    }
}
