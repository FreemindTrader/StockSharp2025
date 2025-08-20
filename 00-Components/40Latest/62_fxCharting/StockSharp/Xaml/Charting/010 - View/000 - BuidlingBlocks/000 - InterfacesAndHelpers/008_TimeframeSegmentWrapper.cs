namespace StockSharp.Xaml.Charting;

/// <summary>
/// Just some class to hold the datasegment and X value
/// </summary>
/// <param name="_param1"></param>
/// <param name="_param2"></param>
public sealed class TimeframeSegmentWrapper(TimeframeDataSegment _param1, double _param2) : IPoint
{
    private readonly TimeframeDataSegment _dataSegment = _param1;
    private readonly double _x = _param2;

    public TimeframeDataSegment Segment => this._dataSegment;

    public double X => this._x;

    public double Y => this.Segment.Y;

}
