using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Gauges;
using System;
using System.Linq;

namespace FreemindTrader
{
    [POCOViewModel]
    public class LondonTimeViewModel : ISupportParentViewModel
    {
        WorldClockViewModel _parent;

        DigitalRedClockModel _activeClock;
        DigitalProgressiveModel _offhour;
        DigitalYellowSubmarineModel _beforeOffHour;

        public LondonTimeViewModel()
        {
            _activeClock = new DigitalRedClockModel();
            _offhour = new DigitalProgressiveModel();
            _beforeOffHour = new DigitalYellowSubmarineModel();
        }



        public virtual object ParentViewModel { get; set; }

        protected void OnParentViewModelChanged()
        {
            _parent = ( ( WorldClockViewModel )ParentViewModel );

            _parent.TimerTick += _parent_TimerTick;

            //this.RaisePropertyChanged( ( x ) => x.SelectedCustomerName );
        }

        private void _parent_TimerTick( DateTime currentTime )
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById( "GMT Standard Time" );
            var dt = TimeZoneInfo.ConvertTimeFromUtc( currentTime, tz );

            CurrenTimeText = string.Format( "{0:H:mm:ss}", dt );

            if ( dt.Hour >= 8 && dt.Hour < 17 )
            {
                if ( Model != _activeClock )
                {
                    Model = _activeClock;
                }
            }
            else
            {
                if ( dt.Hour == 17 && dt.Minute <= 30 )
                {
                    if ( Model != _beforeOffHour ) Model = _beforeOffHour;
                }
                else
                {
                    if ( Model != _offhour ) Model = _offhour;
                }
            }
        }





        public virtual string CurrenTimeText { get; set; }
        public virtual DigitalGaugeModel Model { get; set; }
    }
}


