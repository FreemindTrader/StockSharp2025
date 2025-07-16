using System.Windows.Controls;

namespace StockSharp.Charting.Visuals;
#nullable disable
public interface IAnnotationCanvas : IHitTestable
{
    UIElementCollection Children
    {
    
    get; }
}
