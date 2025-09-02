using DevExpress.Dialogs.Core.View;
using Ecng.Collections;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting;

/// <summary>The chart element representing active orders.</summary>

[Display( ResourceType = typeof( LocalizedStrings ), Name = "ActiveOrders" )]
public class ChartActiveOrdersElement : ChartComponentViewModel<ChartActiveOrdersElement>, 
                                        IChartElement, 
                                        IChartPart<IChartElement>, 
                                        INotifyPropertyChanged, 
                                        INotifyPropertyChanging, 
                                        IPersistable, 
                                        IChartActiveOrdersElement, 
                                        IChartComponent, 
                                        IChartElementUiDomain
{
    
    private System.Windows.Media.Color _buyPendingColor;
    
    private System.Windows.Media.Color _buyColor;
    
    private System.Windows.Media.Color _buyBlinkColor;
    
    private System.Windows.Media.Color _sellPendingColor;
    
    private System.Windows.Media.Color _sellColor;
    
    private System.Windows.Media.Color _sellBlinkColor;
    
    private System.Windows.Media.Color _cancelButtonColor;
    
    private System.Windows.Media.Color _cancelButtonBackground;
    
    private System.Windows.Media.Color _foregroundColor;
    
    private bool _isAnimationEnabled;
    
    private ChartElementUiDomain _baseViewModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartActiveOrdersElement" />.
    /// </summary>
    public ChartActiveOrdersElement()
    {
        SellColor              = Colors.DarkRed;
        SellBlinkColor         = System.Windows.Media.Color.FromRgb( byte.MaxValue, ( byte ) 151, ( byte ) 50 );
        SellPendingColor       = BuyPendingColor = Colors.Gray;
        BuyColor               = Colors.DarkGreen;
        BuyBlinkColor          = System.Windows.Media.Color.FromRgb( ( byte ) 162, ( byte ) 204, ( byte ) 45 );
        ForegroundColor        = Colors.White;
        CancelButtonColor      = Colors.Black;
        CancelButtonBackground = Colors.DarkGray;
        IsAnimationEnabled     = true;
    }

    /// <summary>Color of Buy order in non-active state.</summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "BuyPendingColor", Description = "BuyPendingColorDot", Order = 1 )]
    public System.Windows.Media.Color BuyPendingColor
    {
        get => _buyPendingColor;
        set
        {
            SetField<System.Windows.Media.Color>( ref _buyPendingColor, value, nameof( BuyPendingColor ) );
        }
    }

    /// <summary>Color of Buy order in active state.</summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "BuyColor", Description = "BuyColorDot", Order = 2 )]
    public System.Windows.Media.Color BuyColor
    {
        get => _buyColor;
        set => SetField<System.Windows.Media.Color>( ref _buyColor, value, nameof( BuyColor ) );
    }

    /// <summary>Color of blinking in partially filled state (Buy).</summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "BuyBlinkColor", Description = "BuyBlinkColorDot", Order = 3 )]
    public System.Windows.Media.Color BuyBlinkColor
    {
        get => _buyBlinkColor;
        set
        {
            SetField<System.Windows.Media.Color>( ref _buyBlinkColor, value, nameof( BuyBlinkColor ) );
        }
    }

    /// <summary>Color of Sell order in non-active state.</summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "SellPendingColor", Description = "SellPendingColorDot", Order = 4 )]
    public System.Windows.Media.Color SellPendingColor
    {
        get => _sellPendingColor;
        set
        {
            SetField<System.Windows.Media.Color>( ref _sellPendingColor, value, nameof( SellPendingColor ) );
        }
    }

    /// <summary>Color of Sell order in active state.</summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "SellColor", Description = "SellColorDot", Order = 5 )]
    public System.Windows.Media.Color SellColor
    {
        get => _sellColor;
        set => SetField<System.Windows.Media.Color>( ref _sellColor, value, nameof( SellColor ) );
    }

    /// <summary>Color of blinking in partially filled state (Sell).</summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "SellBlinkColor", Description = "SellBlinkColorDot", Order = 6 )]
    public System.Windows.Media.Color SellBlinkColor
    {
        get => _sellBlinkColor;
        set
        {
            SetField<System.Windows.Media.Color>( ref _sellBlinkColor, value, nameof( SellBlinkColor ) );
        }
    }

    /// <summary>Cancel order button color.</summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "CancelButtonColor", Description = "CancelButtonColorDot", Order = 7 )]
    public System.Windows.Media.Color CancelButtonColor
    {
        get => _cancelButtonColor;
        set
        {
            SetField<System.Windows.Media.Color>( ref _cancelButtonColor, value, nameof( CancelButtonColor ) );
        }
    }


    /// <summary>Cancel order button background color.</summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "CancelButtonBgColor", Description = "CancelButtonBgColorDot", Order = 8 )]
    public System.Windows.Media.Color CancelButtonBackground
    {
        get => _cancelButtonBackground;
        set
        {
            SetField<System.Windows.Media.Color>( ref _cancelButtonBackground, value, nameof( CancelButtonBackground ) );
        }
    }

    /// <summary>Text color.</summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "FontColor", Description = "FontColorDot", Order = 9 )]
    public System.Windows.Media.Color ForegroundColor
    {
        get => _foregroundColor;
        set
        {
            SetField<System.Windows.Media.Color>( ref _foregroundColor, value, nameof( ForegroundColor ) );
        }
    }


    /// <summary>Show chart element.</summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Animation", Description = "AnimationDot", Order = 10 )]
    public bool IsAnimationEnabled
    {
        get => _isAnimationEnabled;
        set
        {
            SetField<bool>( ref _isAnimationEnabled, value, nameof( IsAnimationEnabled ) );
        }
    }

    public System.Windows.Media.Color Color
    {
        get
        {
            return Colors.Transparent;
        }
    }

    

    System.Drawing.Color IChartActiveOrdersElement.BuyPendingColor
    {
        get => BuyPendingColor.FromWpf();
        set => BuyPendingColor = value.ToWpf();
    }

    System.Drawing.Color IChartActiveOrdersElement.BuyColor
    {
        get => BuyColor.FromWpf();
        set => BuyColor = value.ToWpf();
    }

    System.Drawing.Color IChartActiveOrdersElement.BuyBlinkColor
    {
        get => BuyBlinkColor.FromWpf();
        set => BuyBlinkColor = value.ToWpf();
    }

    System.Drawing.Color IChartActiveOrdersElement.SellPendingColor
    {
        get => SellPendingColor.FromWpf();
        set => SellPendingColor = value.ToWpf();
    }

    System.Drawing.Color IChartActiveOrdersElement.SellColor
    {
        get => SellColor.FromWpf();
        set => SellColor = value.ToWpf();
    }

    System.Drawing.Color IChartActiveOrdersElement.SellBlinkColor
    {
        get => SellBlinkColor.FromWpf();
        set => SellBlinkColor = value.ToWpf();
    }

    System.Drawing.Color IChartActiveOrdersElement.CancelButtonColor
    {
        get => CancelButtonColor.FromWpf();
        set => CancelButtonColor = value.ToWpf();
    }

    System.Drawing.Color IChartActiveOrdersElement.CancelButtonBackground
    {
        get => CancelButtonBackground.FromWpf();
        set => CancelButtonBackground = value.ToWpf();
    }

    System.Drawing.Color IChartActiveOrdersElement.ForegroundColor
    {
        get => ForegroundColor.FromWpf();
        set => ForegroundColor = value.ToWpf();
    }
    Func<IComparable, System.Drawing.Color?> StockSharp.Charting.IChartElement.Colorer
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public IChartArea PersistentChartArea => throw new NotImplementedException();

    //DrawableChartComponentBaseViewModel IChartElementUiDomain.CreateViewModel( ScichartSurfaceMVVM _param1 )
    //{
    //    return _baseViewModel = ( DrawableChartComponentBaseViewModel ) new ChartActiveOrdersElementVM( this );
    //}

    ChartElementUiDomain IChartElementUiDomain.CreateViewModel( IDrawingSurfaceVM viewModel )
    {
        return _baseViewModel = new ChartActiveOrdersElementUiDomain( this );
    }

    bool IChartElementUiDomain.StartDrawing(
      IEnumerableEx<ChartDrawData.IDrawValue> _param1 )
    {
        return _baseViewModel.Draw( _param1 );
    }

    void IChartElementUiDomain.StartDrawing()
    {
        _baseViewModel.Draw( CollectionHelper.ToEx<ChartDrawData.IDrawValue>( Enumerable.Empty<ChartDrawData.IDrawValue>(), 0 ) );
    }

    protected override bool OnDraw( ChartDrawData data )
    {
        var source = data.GetActiveOrderMap( );
        return source != null && !CollectionHelper.IsEmpty<ChartDrawData.sActiveOrder>( ( ICollection<ChartDrawData.sActiveOrder> ) source ) && ( ( IChartElementUiDomain ) this ).StartDrawing( CollectionHelper.ToEx<ChartDrawData.IDrawValue>( source.Cast<ChartDrawData.IDrawValue>(), source.Count ) );
    }

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        BuyColor = storage.GetValue<int>( "BuyColor", 0 ).ToColor();
        BuyBlinkColor = storage.GetValue<int>( "BuyBlinkColor", 0 ).ToColor();
        BuyPendingColor = storage.GetValue<int>( "BuyPendingColor", 0 ).ToColor();
        SellColor = storage.GetValue<int>( "SellColor", 0 ).ToColor();
        SellBlinkColor = storage.GetValue<int>( "SellBlinkColor", 0 ).ToColor();
        SellPendingColor = storage.GetValue<int>( "SellPendingColor", 0 ).ToColor();
        ForegroundColor = storage.GetValue<int>( "ForegroundColor", 0 ).ToColor();
        CancelButtonColor = storage.GetValue<int>( "CancelButtonColor", 0 ).ToColor();
        CancelButtonBackground = storage.GetValue<int>( "CancelButtonBackground", 0 ).ToColor();
        IsAnimationEnabled = storage.GetValue<bool>( "IsAnimationEnabled", false );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.Set<int>( "BuyColor", BuyColor.ToInt() ).Set<int>( "BuyBlinkColor", BuyBlinkColor.ToInt() ).Set<int>( "BuyPendingColor", BuyPendingColor.ToInt() ).Set<int>( "SellColor", SellColor.ToInt() ).Set<int>( "SellBlinkColor", SellBlinkColor.ToInt() ).Set<int>( "SellPendingColor", SellPendingColor.ToInt() ).Set<int>( "CancelButtonColor", CancelButtonColor.ToInt() ).Set<int>( "ForegroundColor", ForegroundColor.ToInt() ).Set<int>( "CancelButtonBackground", CancelButtonBackground.ToInt() ).Set<bool>( "IsAnimationEnabled", IsAnimationEnabled );
    }

    internal override ChartActiveOrdersElement Clone(
      ChartActiveOrdersElement _param1 )
    {
        _param1 = base.Clone( _param1 );
        _param1.BuyColor = BuyColor;
        _param1.SellColor = SellColor;
        _param1.CancelButtonColor = CancelButtonColor;
        _param1.ForegroundColor = ForegroundColor;
        _param1.CancelButtonBackground = CancelButtonBackground;
        _param1.BuyPendingColor = BuyPendingColor;
        _param1.SellPendingColor = SellPendingColor;
        _param1.IsAnimationEnabled = IsAnimationEnabled;
        return _param1;
    }
}
