using StockSharp.Studio.Core;

namespace StockSharp.Studio.Controls
{
    public interface IStudioContainer
    {
        CloseAction? GetClosingBehavior( IStudioControl child, CloseReason reason );
    }
}
