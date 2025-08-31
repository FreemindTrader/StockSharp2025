namespace StockSharp.Xaml.Charting;

/// <summary>
/// Just some class to hold the datasegment and X value
/// </summary>
/// <param name="ds"></param>
/// <param name="index"></param>
public sealed class TimeframeIndexedSegment( TimeframeDataSegment ds, double index ) : IPoint
{
    private readonly TimeframeDataSegment _dataSegment = ds;
    private readonly double _index = index;

    public TimeframeDataSegment Segment => this._dataSegment;

    public double X => this._index;

    public double Y => this.Segment.Y;

}
