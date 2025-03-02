using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace fx.DefinitionsWnd
{
    public interface IBarExUI : IBarEx, ICandlestickBarUI
    {

    }

    public interface ICandlestickBarUI
    {
        Color? GetCandleStickPatternColor( bool border );
    }
}
