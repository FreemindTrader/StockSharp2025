using System;

using System.Linq;
using System.Windows.Forms;

namespace FreemindTrader
{
    public static class ScreenHelper
    {
        public static Screen GetPrimaryScreen()
        {
            var allScreen = Screen.AllScreens;
            var mainScreen = allScreen.ElementAt( 0 );
            return mainScreen;
        }

        public static bool HasSecondMonitor()
        {
            var screens = Screen.AllScreens;

            if ( screens.Count() > 1 )
            {
                return true;
            }

            return false;
        }

        public static Screen GetSecondaryScreen()
        {
            if ( HasSecondMonitor() )
            {
                var allScreen = Screen.AllScreens;
                var mainScreen = allScreen.ElementAt( 1 );
                return mainScreen;
            }

            throw new NotImplementedException();
        }
    }
}
