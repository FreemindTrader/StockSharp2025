using Ecng.Collections;
using StockSharp.BusinessEntities;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class PortfolioGrid : BaseGridControl, IComponentConnector
    {
        private readonly GridScheduledExecutorService<BasePosition, PortfolioPropertyChangeHandler> _portfolioItems;        

        public PortfolioGrid( )
        {
            InitializeComponent( );
            _portfolioItems = new GridScheduledExecutorService<BasePosition, PortfolioPropertyChangeHandler>( this, ( p, h ) => new PortfolioPropertyChangeHandler( p, h ) );
        }

        public IListEx<BasePosition> Portfolios
        {
            get
            {
                return _portfolioItems.Source;
            }
        }

        public IListEx<BasePosition> Positions
        {
            get
            {
                return _portfolioItems.Source;
            }
        }

        public BasePosition SelectedPosition
        {
            get
            {
                return SelectedPositions.FirstOrDefault<BasePosition>( );
            }
        }

        public IEnumerable<BasePosition> SelectedPositions
        {
            get
            {
                return SelectedItems.Cast<PortfolioPropertyChangeHandler>( ).Select( h => h.Position );
            }
        }        
    }
}
