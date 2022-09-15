using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using StockSharp.Algo.Statistics;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    internal sealed class CellValueConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == null )
            {
                return ( object ) null;
            }

            if ( !TypeHelper.IsNumeric( value.GetType() ) )
            {
                return value;
            }

            return ( object ) string.Format( "{0:0.##}", value );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }

    public partial class StatisticParameterGrid : BaseGridControl
    {
        private readonly IList<IStatisticParameter> _parameters;
        private StatisticManager _manager;        

        public StatisticParameterGrid( )
        {
            InitializeComponent();
            ItemsSource =  _parameters = ( IList<IStatisticParameter> ) new ObservableCollection<IStatisticParameter>();
        }

        public IList<IStatisticParameter> Parameters
        {
            get
            {
                return _parameters;
            }
        }

        public StatisticManager StatisticManager
        {
            get
            {
                return _manager;
            }
            set
            {
                if ( value == StatisticManager )
                {
                    return;
                }

                _manager = value;
                Reset();
            }
        }

        public void Reset( )
        {
            ( ( ICollection<IStatisticParameter> ) Parameters ).Clear();
            if ( StatisticManager == null )
            {
                return;
            }

            Parameters.AddRange<IStatisticParameter>( StatisticManager.Parameters );
        }

    }
}
