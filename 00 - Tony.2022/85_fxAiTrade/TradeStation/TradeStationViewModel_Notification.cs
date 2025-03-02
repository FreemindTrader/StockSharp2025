﻿using DevExpress.Mvvm;
using fx.Algorithm;
using fx.Collections;
using fx.Definitions;
using StockSharp.Logging;
using System;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    public partial class TradeStationViewModel : BaseLogReceiver, IMutltiTimeFrameSessionDataRepo
    {
        PooledList<double> _soundAlertPrice = new PooledList<double>();

        TrendDirection _alertDirection = TrendDirection.NoTrend;

        PooledList<double> _spokenPrice = new PooledList<double>();

        public void AddSoundAlert()
        {
            var selectedLines = _selectedViewModel.ChartVM.GetSelectedLinesForSoundAlert();

            if ( selectedLines.Count > 0 )
            {
                _soundAlertPrice.AddRange( selectedLines );

                Messenger.Default.Register<QuoteMessage>( this, x => OnSoundAlerts( x ) );
            }

        }

        private void OnSoundAlerts( QuoteMessage quote )
        {
            if ( _soundAlertPrice.Count == 0 )
                return;

            if ( quote.Security.Code != _security.Code )
                return;


            var average = ( quote.Ask + quote.Bid ) / 2;

            if ( _alertDirection == TrendDirection.NoTrend )
            {
                foreach ( double price in _soundAlertPrice )
                {
                    if ( price > average )
                    {
                        _alertDirection = TrendDirection.Uptrend;
                    }
                    else
                    {
                        _alertDirection = TrendDirection.DownTrend;
                    }
                }
            }

            if ( _alertDirection == TrendDirection.Uptrend )
            {
                foreach ( double price in _soundAlertPrice )
                {
                    if ( average > price )
                    {
                        if ( _spokenPrice.FindIndex( x => x == price ) == -1 )
                        {
                            SpeakBrokenPrice( price );

                            _spokenPrice.Add( price );
                        }

                    }
                }
            }
            else if ( _alertDirection == TrendDirection.DownTrend )
            {
                foreach ( double price in _soundAlertPrice )
                {
                    if ( average > price )
                    {
                        if ( _spokenPrice.FindIndex( x => x == price ) == -1 )
                        {
                            SpeakBrokenPrice( price );

                            _spokenPrice.Add( price );
                        }

                    }
                }
            }
        }

        public void SpeakBrokenPrice( double price )
        {
            //using ( SpeechSynthesizer synthesizer = new SpeechSynthesizer( ) )
            //{
            //    // show installed voices
            //    foreach ( var v in synthesizer.GetInstalledVoices( ).Select( v => v.VoiceInfo ) )
            //    {
            //        Console.WriteLine( "Name:{0}, Gender:{1}, Age:{2}",
            //          v.Description, v.Gender, v.Age );
            //    }

            //    // select male senior (if it exists)
            //    synthesizer.SelectVoiceByHints( VoiceGender.Female, VoiceAge.Teen );

            //    // select audio device
            //    synthesizer.SetOutputToDefaultAudioDevice( );

            //    // build and speak a prompt
            //    PromptBuilder builder = new PromptBuilder();

            //    string priceStr = string.Format( "Price {0} has been broken", price );

            //    builder.AppendText( priceStr );
            //    synthesizer.Speak( builder );
            //}            
        }


    }
}
