using fx.Bars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public class BackResearchUpdateMessage
    {

        SBar _selectedBar;

        public ref SBar SelectedBar
        {
            get { return ref _selectedBar; }
            
        }
        

        public BackResearchUpdateMessage( ref SBar selectedBar )
        {
            _selectedBar = selectedBar;
        }
    }    
}
