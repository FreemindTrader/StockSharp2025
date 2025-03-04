using DevExpress.Mvvm;
using System;
using System.Linq;
using System.Windows.Threading;

namespace FreemindTrader
{
    public class WorldClockViewModel : ViewModelBase
    {
        public event Action<DateTime> TimerTick;

        private bool _visible = true;
        private int animationLockCounterCore = 0;
        private readonly DispatcherTimer _mintimer;
        private static readonly TimeSpan _scrollTimerInterval = TimeSpan.FromMilliseconds( 1000.0 );

        bool IsAnimationLocked { get { return animationLockCounterCore > 0; } }

        public WorldClockViewModel()
        {
            _mintimer = new DispatcherTimer()
            {
                Interval = _scrollTimerInterval
            };
            _mintimer.Tick += new EventHandler( OnTimerTick );

            _mintimer.Start();
        }


        public bool Visible
        {
            get { return _visible; }
            set
            {
                if ( _visible == value )
                    return;
                _visible = value;
                RaisePropertyChanged( nameof( Visible ) );
            }
        }


        private void OnTimerTick( object sender, EventArgs e )
        {
            var currentTime = DateTime.UtcNow;

            TimerTick?.Invoke( currentTime );
        }

        void LockAnimation()
        {
            animationLockCounterCore++;
        }

        void UnlockAnimation()
        {
            animationLockCounterCore--;
        }

        void DoAnimation()
        {
            var currentTime = DateTime.UtcNow;

            TimerTick?.Invoke( currentTime );

            var tz = TimeZoneInfo.FindSystemTimeZoneById( "China Standard Time" );
            var chinaTime = TimeZoneInfo.ConvertTimeFromUtc( currentTime, tz );

            var tz1 = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
            var newYorkTime = TimeZoneInfo.ConvertTimeFromUtc( currentTime, tz1 );

            var tz2 = TimeZoneInfo.FindSystemTimeZoneById( "GMT Standard Time" );
            var londonTime = TimeZoneInfo.ConvertTimeFromUtc( currentTime, tz2 );

            UpdateClock( newYorkTime /* _newYorkArcScaleBackgroundLayer, _newYorkHourNeedle, _newYorkMinuteNeedle, _newYorkSecondNeedle */ );
            UpdateClock( chinaTime   /* _chinaArcScaleBackgroundLayer, _chinaTimeHourNeedle, _chinaTimeMinuteNeedle, _chinaTimeSecondNeedle*/ );
            UpdateClock( londonTime  /*_londonArcScaleBackgroundLayer, _londonTimeHourNeedle, _londonTimeMinuteNeedle, _londonTimeSecondNeedle*/ );

        }

        void UpdateClock( DateTime dt )
        {
            try
            {
                if ( dt.Hour >= 8 && dt.Hour < 17 )
                {
                    //if ( bg?.ShapeType != BackgroundLayerShapeType.CircularFull_Style18 )
                    //{
                    //    bg.ShapeType = BackgroundLayerShapeType.CircularFull_Style18;
                    //}
                }
                else
                {
                    if ( dt.Hour == 17 && dt.Minute <= 30 )
                    {
                        //if ( bg?.ShapeType != BackgroundLayerShapeType.CircularFull_Style18 )
                        //{
                        //    bg.ShapeType = BackgroundLayerShapeType.CircularFull_Style18;
                        //}
                    }
                    else
                    {
                        //if ( bg?.ShapeType != BackgroundLayerShapeType.CircularFull_Style27 )
                        //{
                        //    bg.ShapeType = BackgroundLayerShapeType.CircularFull_Style27;
                        //}
                    }
                }
            }
            catch
            {

            }


            int hour = dt.Hour <= 12 ? dt.Hour : dt.Hour - 12;
            int min = dt.Minute;
            int sec = dt.Second;
            //h.Value = ( float )hour + ( float )( min ) / 60.0f;
            //m.Value = ( ( float )min + ( float )( sec ) / 60.0f ) / 5f;
            //s.Value = sec / 5.0f;
        }
    }
}
