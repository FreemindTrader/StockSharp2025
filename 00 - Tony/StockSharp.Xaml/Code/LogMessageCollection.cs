using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StockSharp.Xaml
{
    public class LogMessageCollection : ThreadSafeObservableCollection<LogMessage>, INotifyPropertyChanged
    {
        int _verboseCount;
        int _debugCount;
        int _errorCount;
        int _warningCount;
        int _infoCount=  0;
        public static readonly int DefaultMaxItemsCount = Environment.Is64BitProcess ? 10000 : 1000;

        private PropertyChangedEventHandler _propertyChanged;


        public int InfoCount
        {
            get { return _infoCount; }

            private set
            {
                if ( _infoCount == value )
                    return;
                _infoCount = value;
            }
        }


        public int WarningCount
        {
            get { return _warningCount; }
            private set
            {
                if ( _warningCount == value )
                    return;
                _warningCount = value;
                OnPropertyChanged( nameof( WarningCount ) );
            }
        }


        public int ErrorCount
        {
            get { return _errorCount; }
            private set
            {
                if ( _errorCount == value )
                    return;
                _errorCount = value;

                OnPropertyChanged( nameof( ErrorCount ) );
            }
        }


        public int DebugCount
        {
            get { return _debugCount; }
            private set
            {
                if ( _debugCount == value )
                    return;
                _debugCount = value;

                OnPropertyChanged( nameof( DebugCount ) );
            }
        }

        
        public int VerboseCount
        {
            get { return _verboseCount; }
            private set
            {
                if ( _verboseCount == value )
                    return;
                _verboseCount = value;

                OnPropertyChanged( nameof( VerboseCount ) );              
            }
        }
        




        public LogMessageCollection( ) : base( new ObservableCollectionEx<LogMessage>() )
        {
            this.ErrorCount   = 0;
            this.WarningCount = 0;
            this.InfoCount    = 0;
            this.DebugCount   = 0;
            this.VerboseCount = 0;
        }

        /// <summary>
		/// Number of messages of type <see cref="LogLevels.Info"/>.
		/// </summary>
		

        /// <summary>
        /// Number of messages of type <see cref="LogLevels.Warning"/>.
        /// </summary>
        

        /// <summary>
        /// Number of messages of type <see cref="LogLevels.Error"/>.
        /// </summary>
        

        /// <summary>
        /// Number of messages of type <see cref="LogLevels.Debug"/>.
        /// </summary>
        

        

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }

        public override void Add( LogMessage item )
        {
            AddRange( new[ ] { item } );
        }

        private void OnPropertyChanged( string propName )
        {
            _propertyChanged?.Invoke( this, propName );
        }

        public override int RemoveRange( int index, int count )
        {
            CheckItems( this.Skip( index ).Take( count ), -1 );
            return base.RemoveRange( index, count );
        }


        public override void Clear( )
        {
            this.ErrorCount   = 0;
            this.WarningCount = 0;
            this.InfoCount    = 0;
            this.DebugCount   = 0;
            this.VerboseCount = 0;
            OnPropertyChanged( "VerboseCount" );
            OnPropertyChanged( "DebugCount" );
            OnPropertyChanged( "InfoCount" );
            OnPropertyChanged( "WarningCount" );
            OnPropertyChanged( "ErrorCount" );
            base.Clear();
        }
        /// <summary>
		/// To add items.
		/// </summary>
		/// <param name="items">New items.</param>
		public override void AddRange( IEnumerable<LogMessage> items )
        {
            base.AddRange( items );
            CheckItems( items, 1 );
        }

        private void CheckItems( IEnumerable<LogMessage> items, int step )
        {
            bool isVerbose = false,isDebug = false,isInfo = false,isWarning = false,isError = false;

            items.ForEach( ( Action<LogMessage> ) (message =>
            {
                switch ( message.Level )
                {
                    case LogLevels.Verbose:
                        this.VerboseCount += step;
                        isVerbose = true;
                        break;

                    case LogLevels.Debug:
                        DebugCount += step;
                        isDebug = true;
                        break;

                    case LogLevels.Info:
                        InfoCount += step;
                        isInfo = true;
                        break;

                    case LogLevels.Warning:
                        WarningCount += step;
                        isWarning = true;
                        break;

                    case LogLevels.Error:
                        ErrorCount += step;
                        isError = true;
                        break;
                }
            }) );

            if ( isVerbose )
                OnPropertyChanged( "VerboseCount" );

            if ( isDebug )
                OnPropertyChanged( "DebugCount" );

            if ( isInfo )
                OnPropertyChanged( "InfoCount" );

            if ( isWarning )
                OnPropertyChanged( "WarningCount" );

            if ( isError )
                OnPropertyChanged( "ErrorCount" );
        }
    }
}
