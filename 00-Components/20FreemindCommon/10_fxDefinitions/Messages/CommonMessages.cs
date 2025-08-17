using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum AdapterMessageType
    {
        DownloadFrom,
        DownloadOnly

    }

    public class AdapterMessage
    {

        /// <summary>
        /// The Adapter state change notification type.
        /// </summary>
        public AdapterMessageType MessageType { get; private set; }

        public DateTime StartingDate { get; private set; }

        public TimeSpan Period { get; private set; }

        public AdapterMessage( AdapterMessageType messageType, DateTime fromDate, TimeSpan timeSpan )
        {
            MessageType = messageType;
            StartingDate = fromDate;
            Period = timeSpan;
        }
    }

    public enum SplashScreenMessageType
    {
        ShowMessage,

        HideMessage,
        SetProgressMessage,

        SetStateMessage
    }

    public class ShowSplashScreenMessages
    {
    }

    public class HideSplashScreenMessages
    {
    }

    public class IndicatorResultsReceivedMessage
    {
    }
}
