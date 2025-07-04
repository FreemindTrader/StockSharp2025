using SciChart.Charting.Common.Helpers;
using fx.Charting;
using System;
using System.Collections.Generic;
using System.Windows.Input;
namespace fx.Charting
{
    public sealed class LegendModifierVM : BaseVM
    {
        private bool _allowToHide = true;
        private LegendModifierEx _legendModifier;
        private bool _allowToRemove;
        private IEnumerable<ParentVM> _elements;
        private readonly IScichartSurfaceVM _chartPaneViewModel;
        private readonly ICommand _removeElementCommand;
        public event Action<IChartElement> RemoveElmentEvent;

        public LegendModifierVM( IScichartSurfaceVM surfaceVM )
        {
            if ( surfaceVM == null )
            {
                throw new ArgumentNullException( "pane" );
            }
            _chartPaneViewModel = surfaceVM;

            ChartViewModel parentViewModel = surfaceVM.ChartViewModel;

            if ( parentViewModel != null )
            {
                Xaml.DoHelper.AddPropertyListener( parentViewModel, ChartViewModel.IsInteractedProperty, e => NotifyChanged( "AllowToRemove" ) );
            }

            Elements = surfaceVM.LegendElements;
            _removeElementCommand = new ActionCommand<ParentVM>( vm =>
            {
                if ( RemoveElmentEvent == null )
                {
                    return;
                }

                RemoveElmentEvent.Invoke( vm.ChartElement );
            },
                                                                                         p => AllowToRemove
                                                                                        );
        }

        public IScichartSurfaceVM Pane
        {
            get
            {
                return _chartPaneViewModel;
            }
        }

        public ICommand RemoveElementCommand
        {
            get
            {
                return _removeElementCommand;
            }
        }

        public IEnumerable<ParentVM> Elements
        {
            get
            {
                return _elements;
            }
            set
            {
                SetField( ref _elements, value, nameof( Elements ) );
            }
        }

        public LegendModifierEx LegendModifier
        {
            get
            {
                return _legendModifier;
            }
            set
            {
                SetField( ref _legendModifier, value, nameof( LegendModifier ) );
            }
        }

        public bool AllowToHide
        {
            get
            {
                return _allowToHide;
            }
            set
            {
                SetField( ref _allowToHide, value, nameof( AllowToHide ) );
            }
        }

        public bool AllowToRemove
        {
            get
            {
                ChartViewModel parentViewModel = Pane.ChartViewModel;
                ChartExViewModel parentExViewModel = Pane.ChartExViewModel;

                if ( parentViewModel == null && parentExViewModel == null )
                {
                    return _allowToRemove;
                }

                if ( parentExViewModel != null )
                {
                    return parentExViewModel.IsInteracted;
                }
                else
                {
                    return parentViewModel.IsInteracted;
                }
            }
            set
            {
                if ( Pane.ChartViewModel != null )
                {
                    throw new InvalidOperationException( "you must use Chart.IsInteracted property instead" );
                }
                SetField( ref _allowToRemove, value, nameof( AllowToRemove ) );
            }
        }
    }
}