using System;
using System.ComponentModel;

namespace StockSharp.Charting.Themes;

public interface IAxisPanel : INotifyPropertyChanged
{
    void ClearLabels();

    void Invalidate();

    void DrawTicks( TickCoordinates _param1, float _param2 );

    void AddTickLabels( Action<AxisCanvas> _param1 );
}
