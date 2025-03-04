using DevExpress.Xpf.Docking;
using FreemindAITrade.ViewModels;
using System;
using System.Linq;

namespace FreemindAITrade
{
    /// <summary>
    /// https://supportcenter.devexpress.com/ticket/details/t515213/documentpanels-side-by-side-when-using-itemssource-on-maindocklayoutmanager
    /// </summary>
    public class CustomLayoutAdapter : ILayoutAdapter
    {
        protected int counter = 0;

        public string Resolve( DockLayoutManager owner, object item )
        {
            var testerVM = item as BackTesterViewModel;

            if ( testerVM != null )
            {
                string parentName = "DocumentGroup" + counter++;

                var root = owner.LayoutRoot.Items[0] as LayoutGroup;
                var newDoc = new DocumentGroup() { Name = parentName };

                root.Add( newDoc );

                return parentName;
            }

            var liveVM = item as LiveTradeViewModel;

            if ( liveVM != null )
            {
                string parentName = "DocumentGroup";

                var newDocGroup = new DocumentGroup() { Name = parentName };
                newDocGroup.CaptionLocation = CaptionLocation.Bottom;

                ( owner.LayoutRoot.Items[0] as LayoutGroup ).Add( newDocGroup );
                return parentName;
            }

            return "DocumentsGroup";
        }
    }
}
