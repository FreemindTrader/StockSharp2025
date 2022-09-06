using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    /// <summary>
	/// The Technical Analysis Signal Provider
	/// </summary>
    public interface ITechnicalAnalysisSignalProvider
    {
        /// <summary>
        /// MACD signal has crossed either up or down
        /// </summary>
        event EventHandler< MacdCrossEventArgs >       MacdCrossEvent;

        event EventHandler< OscillatorCrossEventArgs > OscillatorCrossEvent;

        event EventHandler< EmaCrossEventArgs >        EmaCrossEvent;
        /// <summary>
        /// Positive, Negative, Hidden Divergence
        /// </summary>
        event EventHandler< DivergenceEventArgs >      DivergenceEvent;

        /// <summary>
        /// SMA latest Value
        /// </summary>
        event EventHandler< TaValueEventArgs >         SmaValueChangedEvent;

        /// <summary>
        /// Macd indicator latest Value
        /// </summary>
        event EventHandler< MacdValueEventArgs >       MacdValueChangedEvent;

        /// <summary>
        /// SMA signal has crossed either up or down
        /// </summary>
        event EventHandler< SmaCrossEventArgs > SmaCrossEvent;


        /// <summary>
        /// Daily, Weekly, Monthly Pivot Points Change
        /// </summary>
        event EventHandler< PPChangedEventArgs >       PivotPointChangedEvent;
    }
}
