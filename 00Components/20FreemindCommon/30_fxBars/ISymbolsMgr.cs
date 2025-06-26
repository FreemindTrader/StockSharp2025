using fx.Definitions;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Bars
{
    public interface ISymbolsMgr
    {
        //BarInfoManager CreateBarInfoManager( SymbolEx symbol );

        //BarInfoManager CreateBarInfoManager( Security symbol );

        int GetOfferId( Security instrument );

        IAdvancedAnalysisManager GetOrCreateAdvancedAnalysis( Security instructment );

        IAdvancedAnalysisManager GetOrCreateAdvancedAnalysis( SymbolEx instructment );

        IAdvancedAnalysisManager GetOrCreateAdvancedAnalysis( string instructment );
    }
}
