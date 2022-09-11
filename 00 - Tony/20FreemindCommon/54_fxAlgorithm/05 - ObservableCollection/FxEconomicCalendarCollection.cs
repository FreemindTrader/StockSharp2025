using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Definitions;
using DevExpress.Mvvm;

using Ecng.Collections;
using System.Collections.ObjectModel;
using fx.Collections;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.AlgoEx;
using Ecng.ComponentModel;

namespace fx.Algorithm
{
    public class FxEconomicCalendarBindingList : ThreadSafeObservableCollection< FxNewsEvent >
    {        
        public FxEconomicCalendarBindingList( IListEx< FxNewsEvent > items ) : base( items )
        {
        }

        private ThreadSafeDictionary< FxNewsEvent, string > _sentNewsEvents = new ThreadSafeDictionary< FxNewsEvent, string >( );

        public void ClearNewsEvent( )
        {
            Clear( );
            _sentNewsEvents.Clear( );
        }

        public void AddNews( IList< FxNewsEvent > newsEventList )
        {
            var newsList = new List< FxNewsEvent >();
            foreach( var fxNewsEvent in newsEventList )
            {
                if( fxNewsEvent.Impact == EventImpact.High )
                {
                    _sentNewsEvents.Add( fxNewsEvent, "" );
                }

                if ( !Items.Contains( fxNewsEvent ) )
                {
                    newsList.Add( fxNewsEvent );
                }
                
            }

            if ( newsList.Count > 0 )
            {
                var a = newsList.OrderBy( i => i.EventTimeUTC );

                Items.AddRange( a );
            }

                        
        }

        public void CalculateCountDown( Security monitoringSymbol )
        {
            if( monitoringSymbol == null )
            {
                return;
            }

            InternalUpdateCountDown( monitoringSymbol );
        }

        public void InternalUpdateCountDown( Security monitoringSymbol )
        {
            PooledList< FxNewsEvent > temp = new PooledList< FxNewsEvent >( Items.Count );

            DateTime currentTime = DateTime.UtcNow;

            bool needToRemoved = false;

            foreach( var fxNewsEvent in Items )
            {
                if ( monitoringSymbol.IsRelatedNews( fxNewsEvent.Country ) )                    
                {
                    TimeSpan difference = fxNewsEvent.EventTimeUTC - currentTime;

                    string differstr = "";

                    if( difference.TotalMinutes < -5 )
                    {
                        needToRemoved = true;
                        continue;
                    }
                    else if( difference.TotalMinutes < 0 )
                    {
                        differstr = string.Format( "{0} min passed", difference.Minutes );
                    }
                    else if( difference.Days == 1 )
                    {
                        differstr = string.Format( "{0} day {1} hr {2} min", difference.Days, difference.Hours, difference.Minutes );
                    }
                    else if( difference.Days > 1 )
                    {
                        differstr = string.Format( "{0} days {1} hr {2} min", difference.Days, difference.Hours, difference.Minutes );
                    }
                    else if( difference.Days == 0 )
                    {
                        differstr = string.Format( "{0} hr {1} min", difference.Hours, difference.Minutes );
                    }

                    if( fxNewsEvent.Impact == EventImpact.High && difference.TotalMinutes <= 15 )
                    {
                        if (_sentNewsEvents.ContainsKey(fxNewsEvent) )
                        {
                            var message = _sentNewsEvents[fxNewsEvent];

                            if (message != differstr)
                            {
                                Messenger.Default.Send(new ImportantNewsComingMessage(fxNewsEvent));

                                _sentNewsEvents[fxNewsEvent] = differstr;
                            }
                        }                        
                    }

                    fxNewsEvent.CountDown = differstr;

                    temp.Add( fxNewsEvent );
                }
            }

            if( needToRemoved )
            {
                var itemSource = new ObservableCollectionEx< FxNewsEvent >( );
                var newEconomicBinding = new FxEconomicCalendarBindingList( itemSource );

                newEconomicBinding.AddNews( temp );

                //Messenger.Default.Send( new EconomicCalenderDataSourceUpdateMessage( monitoringSymbol.SymbolString, newEconomicBinding ) );

                //newEconomicBinding.UiControl = oldEconomicBinding.UiControl;

                //SymbolsMgr.Instance.SetEconomicCalenderBindingList( monitoringSymbol.SymbolString, newEconomicBinding );

                //this.Items.Clear( );

                //foreach ( var fxNewsEvent in temp )
                //{
                //    Add( fxNewsEvent );
                //}
            }
        }

        public void UpdateCountDown( Security monitoringSymbol )
        {
            if( monitoringSymbol == null )
            {
                return;
            }

            InternalUpdateCountDown( monitoringSymbol );
        }
    }
}