using StockSharp.BusinessEntities;
using System.Collections.Generic;

namespace StockSharp.Xaml.PropertyGrid
{
    public interface ISecuritiesSelectWindow
    {
        ISecurityProvider SecurityProvider { get; set; }

        IEnumerable<Security> SelectedSecurities { get; set; }
    }
}