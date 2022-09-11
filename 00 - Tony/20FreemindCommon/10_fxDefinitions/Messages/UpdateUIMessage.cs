using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fx.Definitions
{
    public class UpdateUIMessage
    {
        public enum UiUpdateType
        {
            WindowTitle = 1,
            StatusBar = 2,
            Skin = 3,
            MenuBar = 4
        }

        public UiUpdateType UpdateType
        {
            get;
            set;
        }

        public string UpdateString
        {
            get;
            set;
        }

        public object OtherParameter
        {
            get;
            set;
        }

        public UpdateUIMessage( UiUpdateType updateType, string updateString, object otherParameter )
        {
            UpdateType     = updateType;
            UpdateString   = updateString;
            OtherParameter = otherParameter;
        }
    }
}

