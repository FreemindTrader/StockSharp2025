#nullable disable
using StockSharp.Xaml.Charting.Utility.Mouse;

public interface IMouseManager
{
    void AddPropertyEvents( IPublishMouseEvents _param1, IReceiveMouseEvents _param2 );

    void RemovePropertyEvents( IPublishMouseEvents _param1 );

    void RemovePropertyEvents( IReceiveMouseEvents _param1 );
}
