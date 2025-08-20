using Ecng.Xaml;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Common.Helpers;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;

namespace StockSharp.Xaml.Charting;


/// <summary>
/// Since LegendModifier is part of Scichart, I have developed my own version of LegendModifierEx.
/// 
/// I don't remember why I did that, will update the document later.
/// </summary>
public sealed class LegendModifierVM : ChartBaseViewModel
{
    private LegendModifierEx _legendModifier;

    private bool _allowToHide = true;

    private IEnumerable<ChartComponentViewModel> _componentsCache;

    private readonly ScichartSurfaceMVVM _scichartSurfaceVM;

    private readonly ICommand _removeElementCommand;

    public event Action<IChartElement> RemoveElementEvent;

    public LegendModifierVM( ScichartSurfaceMVVM vm )
    {
        _scichartSurfaceVM = vm ?? throw new ArgumentNullException( "pane" );
        Elements = ( IEnumerable<ChartComponentViewModel> ) vm.LegendElements;

        // BUG: need to work on ChartComponentViewModel first
        //_removeElementCommand = new ActionCommand<ChartComponentViewModel>(
        //    vm => RemoveElementEvent?.Invoke( vm.ChartComponent ),
        //    p => p.AllowToRemove );
    }

    public ScichartSurfaceMVVM Pane
    {
        get => _scichartSurfaceVM;
    }

    public ICommand RemoveElementCommand => _removeElementCommand;

    public IEnumerable<ChartComponentViewModel> Elements
    {
        get => _componentsCache;
        set
        {
            SetField<IEnumerable<ChartComponentViewModel>>( ref _componentsCache, value, nameof( Elements ) );
        }
    }

    public LegendModifierEx LegendModifier
    {
        get => _legendModifier;
        set
        {
            SetField<LegendModifierEx>( ref _legendModifier, value, nameof( LegendModifier ) );
        }
    }

    public bool AllowToHide
    {
        get => _allowToHide;
        set
        {
            SetField<bool>( ref _allowToHide, value, nameof( AllowToHide ) );
        }
    }
}