namespace StockSharp.Xaml.Charting;

#nullable disable
internal readonly struct TransactionDataSegmentWrapper( TransactionDataSegment tds, int xValue ) : IPoint
{

    private readonly TransactionDataSegment _transSegment = tds;

    private readonly double _xValue = (double) xValue;

    public TransactionDataSegment TransactionDataSegment => this._transSegment;
    
    public double X => this._xValue;

    public double Y
    {
        get
        {
            TransactionDataSegment tds = this.TransactionDataSegment;
            return tds == null ? double.NaN : tds.Y;
        }

    }
}
