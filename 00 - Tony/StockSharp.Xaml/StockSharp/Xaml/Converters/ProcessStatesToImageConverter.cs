using StockSharp.Algo;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace StockSharp.Xaml.Converters
{
    public class ProcessStatesToImageConverter : IValueConverter
    {
        private ImageSource _startImage;
        private ImageSource _stoppingImage;
        private ImageSource _stoppedImage;

        public ImageSource StartedImage
        {
            get
            {
                return this._startImage;
            }
            set
            {
                this._startImage = value;
            }
        }

        public ImageSource StoppingImage
        {
            get
            {
                return this._stoppingImage;
            }
            set
            {
                this._stoppingImage = value;
            }
        }

        public ImageSource StoppedImage
        {
            get
            {
                return this._stoppedImage;
            }
            set
            {
                this._stoppedImage = value;
            }
        }

        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == DependencyProperty.UnsetValue )
                return Binding.DoNothing;

            ProcessStates? currentState = value as ProcessStates?;

            if ( value == null )
                return Binding.DoNothing;

            if ( currentState.HasValue )
            {
                switch ( ( ProcessStates ) currentState.GetValueOrDefault() )
                {
                    case ProcessStates.Stopped:
                        return ( object ) this.StoppedImage;

                    case ProcessStates.Stopping:
                        return ( object ) this.StoppingImage;

                    case ProcessStates.Started:
                        return ( object ) this.StartedImage;
                }
            }
            return DependencyProperty.UnsetValue;
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
