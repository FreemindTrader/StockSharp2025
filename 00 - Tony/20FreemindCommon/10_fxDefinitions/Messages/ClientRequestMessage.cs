using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fx.Definitions
{
    public enum CommandMessages
    {
        RelocateWave = 1,
        HasHewFunctions = 2,
        SelectMultiple = 3
    }


    public enum SystemAudioAlertType
    {
        Support    = 1,
        Resistance = 2,
        Long       = 3,
        Short      = 4,
        Stop       = 5,
        Profit     = 6,
        Hedge      = 7,
        News = 8
    }

    public class SystemAudioAlertMessage
    {
        public SystemAudioAlertType AlertType { get; set; }

        public string Parameter { get; set; }


        public SystemAudioAlertMessage( SystemAudioAlertType alertType, string parameter )
        {
            AlertType = alertType;
            Parameter = parameter;

        }
    }

    public class ClientRequestMessage
    {// Fields...
        private string _MessageString;



        public string MessageString
        {
            get { return _MessageString; }
            set { _MessageString = value; }
        }

        public ClientRequestMessage( string message )
        {
            _MessageString = message;            
        }
    }

    public class PerformingWorkMessage
    {// Fields...
        private string _MessageString;

        private TimeSpan _periodStarted;



        public string MessageString
        {
            get { return _MessageString; }
            set { _MessageString = value; }
        }

        public TimeSpan PeriodStarted
        {
            get { return _periodStarted; }
            set { _periodStarted = value; }
        }

        public PerformingWorkMessage( string message )
        {
            _MessageString = message;
            _periodStarted = TimeSpan.Zero;
        }

        public PerformingWorkMessage( string message, TimeSpan periodStarted )
        {
            _MessageString = message;
            _periodStarted = periodStarted;
        }
    }

    public class WorkDoneMessage
    {// Fields...
        private int _Percentage;
        private string _MessageString;
        


        public string MessageString
        {
            get { return _MessageString; }
            set { _MessageString = value; }
        }


        public int Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; }
        }

        

        public WorkDoneMessage( string message )
        {
            _MessageString = message;
            _Percentage = -1;
        }

        public WorkDoneMessage( int percentage )
        {
            _MessageString = string.Empty;
            _Percentage = percentage;
        }
    }

    public class CritialErrorMessage
    {// Fields...        
        private string _MessageString;

        private TimeSpan _periodStarted;



        public string MessageString
        {
            get { return _MessageString; }
            set { _MessageString = value; }
        }

        public TimeSpan PeriodStarted
        {
            get { return _periodStarted; }
            set { _periodStarted = value; }
        }
        
        public CritialErrorMessage( string message, TimeSpan period  )
        {
            _MessageString = message;

            _periodStarted = period;
        }        
    }

    public class DynamicCustomObjectDrawnMessage
    {
        

        public DynamicCustomObjectDrawnMessage(  )
        {
            
        }
    }



    

    public class HewMessage
    {// Fields...
        private ElliottWaveCycle _waveCycle;



        public ElliottWaveCycle WaveCycle
        {
            get { return _waveCycle; }
            set { _waveCycle = value; }
        }

        public HewMessage( ElliottWaveCycle cycle )
        {
            _waveCycle = cycle;
        }
    }

    public class ToggleCommandMessage
    {// Fields...
        private CommandMessages _messageType;

        private bool _showOrNot = false;


        public CommandMessages MessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }

        public bool ShowOrNot
        {
            get { return _showOrNot; }
            set { _showOrNot = value; }
        }

        public ToggleCommandMessage( CommandMessages messageType, bool showOrNot )
        {
            _messageType = messageType;
            _showOrNot = showOrNot;
        }
    }


    

    public class PositionAddedMessage
    {
        public IDetailedPosition NewPosition { get; set; }

        public PositionAddedMessage( IDetailedPosition newOne )
        {
            NewPosition = newOne;
        }
    }

    public class LocateBarMessage
    {// Fields...
        bool _switchPeriod = false;
        private long _barIndex;

        private TimeSpan _responsibleTimeFrame;

        private long _barFileTimeUTC;

        
        public bool SwitchPeriod
        {
            get { return _switchPeriod; }
            set
            {
                _switchPeriod = value;
            }
        }
        

        public long BarIndex
        {
            get { return _barIndex; }
            set { _barIndex = value; }
        }

        public TimeSpan ResponsibleTimeFrame
        {
            get { return _responsibleTimeFrame; }
            set { _responsibleTimeFrame = value; }
        }

        public long LinuxTime
        {
            get { return _barFileTimeUTC; }
            set { _barFileTimeUTC = value; }
        }

        public LocateBarMessage( long barIndex, long rawBarTime, TimeSpan responsibleTimeFrame, bool switchPeriod )
        {
            _barIndex             = barIndex;
            _barFileTimeUTC       = rawBarTime;
            _responsibleTimeFrame = responsibleTimeFrame;
            _switchPeriod         = switchPeriod;
        }

        public LocateBarMessage( DateTime barTime, TimeSpan responsibleTimeFrame )
        {
            _barIndex             = -1;
            _barFileTimeUTC       = barTime.ToLinuxTime();
            _responsibleTimeFrame = responsibleTimeFrame;
            _switchPeriod         = false;
        }
    }



    public class DataTickMessage{// Fields...

        public DataTickMessage( string symbol )
        {
            _symbol = symbol;
        }
        
        string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                _symbol = value;
            }
        }
        
    }


    public class fxStationMessage
    {// Fields...
        private string _workProgressMessage;

        private string _barInfoMessage;

        private int _progress;



        public string WorkProgressMessage
        {
            get { return _workProgressMessage; }
            set { _workProgressMessage = value; }
        }

        public string BarInfoMessage
        {
            get { return _barInfoMessage; }
            set { _barInfoMessage = value; }
        }

        public int Progess
        {
            get { return _progress; }
            set { _progress = value; }
        }

        public fxStationMessage( string workProgressMessage, string barInfoMessage )
        {
            _workProgressMessage = workProgressMessage;
            BarInfoMessage = barInfoMessage;
        }

        public fxStationMessage( int progress )
        {
            _progress = progress;            
        }

    }


    public class AccountsInfoReadyMessage
    {
        string _mainLoginName;
        public string MainLoginName
        {
            get { return _mainLoginName; }
            set
            {
                _mainLoginName = value;
            }
        }
        

        public AccountsInfoReadyMessage( string mainLoginName)
        {
            _mainLoginName = mainLoginName;
        }



    }


    
}
