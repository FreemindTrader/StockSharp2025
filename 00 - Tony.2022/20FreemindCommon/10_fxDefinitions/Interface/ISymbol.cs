using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface ISymbol
    {
        string      Source        { get; set; }    
        string      SymbolString  { get; set; }
        SymbolGroup SymbolGroup    { get; set; }
        bool        IsForexPair   { get; set; }
        string      FirstOfCross  { get; set; }

        SymbolsEnum SymbolEnum { get; set; }
        string      SecondOfCross { get; set; }

        void FixSymbol( );
    }

    public enum SymbolsEnum
    {
        NA,
        EURUSD,
        CHFJPY,
        GBPCHF,
        EURAUD,
        EURCAD,
        AUDCAD,
        CADJPY,
        NZDJPY,
        GBPCAD,
        AUDNZD,
        USDSEK,
        USDDDK,
        EURSEK,
        EURNOK,
        USDNOK,
        USDMXN,
        AUDCHF,
        EURNZD,
        EURPLN,
        USDPLN,
        EURCZK,
        USDCZK,
        USDZAR,
        USDSGD,
        USDHKD,
        EURDKK,
        GBPSEK,
        NOKJPY,
        SEKJPY,
        SGDJPY,
        HKDJPY,
        ZARJPY,
        USDTRY,
        EURTRY,
        NZDCHF,
        CADCHF,
        NZDCAD,
        CHFSEK,
        CHFNOK,
        EURHUF,
        USDHUF,
        TRYJPY,        
        GBPUSD,        
        USDCNH,
        EURJPY,
        USDJPY,
        GBPJPY,
        AUDJPY,
        USDCHF,
        AUDUSD,
        EURCHF,
        EURGBP,
        NZDUSD,
        USDCAD,
        GBPAUD,
        XAGUSD,
        XAUUSD,
        UK100,
        USDOLLAR,
        GER30,
        FRA40,
        AUS200,
        ESP35,
        HKG33,
        ITA40,
        JPN225,
        NAS100,
        SPX500,
        SUI20,
        COPPER,
        EUSTX50,
        US30,
        USOIL,
        UKOIL,
        NGAS,
        XPDUSD,
        XPTUSD,
        BUND,
        USDILS,
        CHN50,
        SOYF,
        US2000, 
        WHEATF,
        CORNF,
        BTCUSD,
        ETHUSD,
        LTCUSD
    }
}
