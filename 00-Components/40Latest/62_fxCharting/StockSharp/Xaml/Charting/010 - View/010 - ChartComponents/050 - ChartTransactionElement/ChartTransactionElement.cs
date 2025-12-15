using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

public abstract class ChartTransactionElement<T> : ChartComponentViewModel<T>,
                                                      IChartElement,
                                                      IChartPart<IChartElement>,
                                                      INotifyPropertyChanged,
                                                      INotifyPropertyChanging,
                                                      IPersistable,
                                                      IChartTransactionElement,
                                                      IChartComponent,
                                                      IChartElementUiDomain
  where T : ChartTransactionElement<T>, new()
{
    private System.Windows.Media.Color _buyColor;
    private System.Windows.Media.Color _buyStrokeColor;
    private System.Windows.Media.Color _sellColor;
    private System.Windows.Media.Color _sellStrokeColor;
    private bool _useAltIcon;
    private double _drawSize;

    private ChartElementUiDomain _uiBusinessLogic;

    protected ChartTransactionElement( )
    {
        BuyColor = BuyStrokeColor = Colors.Lime;
        SellColor = SellStrokeColor = Colors.HotPink;
    }

    [Browsable( false )]
    [Obsolete( "Use FullTitle property." )]
    public string Title
    {
        get => FullTitle;
        set => FullTitle = value;
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "BuyColor", Description = "BuyColorDesc", GroupName = "Style", Order = 30 )]
    public System.Windows.Media.Color BuyColor
    {
        get => _buyColor;
        set
        {
            if ( _buyColor == value )
                return;
            _buyColor = value;
            RaisePropertyChanged( nameof( BuyColor ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "BuyBorderColor", Description = "BuyBorderColorDesc", GroupName = "Style", Order = 40 )]
    public System.Windows.Media.Color BuyStrokeColor
    {
        get => _buyStrokeColor;
        set
        {
            if ( _buyStrokeColor == value )
                return;
            _buyStrokeColor = value;
            RaisePropertyChanged( nameof( BuyStrokeColor ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "SellColor", Description = "SellColorDesc", GroupName = "Style", Order = 50 )]
    public System.Windows.Media.Color SellColor
    {
        get => _sellColor;
        set
        {
            if ( _sellColor == value )
                return;
            _sellColor = value;
            RaisePropertyChanged( nameof( SellColor ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "SellBorderColor", Description = "SellBorderColorDesc", GroupName = "Style", Order = 60 )]
    public System.Windows.Media.Color SellStrokeColor
    {
        get => _sellStrokeColor;
        set
        {
            if ( _sellStrokeColor == value )
                return;
            _sellStrokeColor = value;
            RaisePropertyChanged( nameof( SellStrokeColor ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "UseAltIcon", Description = "UseAltIcon", GroupName = "Style", Order = 70 )]
    public bool UseAltIcon
    {
        get => _useAltIcon;
        set
        {
            if ( _useAltIcon == value )
                return;
            _useAltIcon = value;
            RaisePropertyChanged( nameof( UseAltIcon ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "DrawSize", Description = "DrawSize", GroupName = "Style", Order = 80 /*0x50*/)]
    public double DrawSize
    {
        get => _drawSize;
        set
        {
            if ( value < 0.0 )
                throw new ArgumentOutOfRangeException( nameof( value ) );
            if ( Math.Abs( _drawSize - value ) < 1E-05 )
                return;
            _drawSize = value;
            RaisePropertyChanged( nameof( DrawSize ) );
        }
    }

    protected override string GetGeneratedTitle( )
    {
        var subscription = ChartHelper2025.TryGetSubscription( this );
        
        if ( subscription == null )
            return null;

        string elementTitleParams = LocalizedStrings.ChartTranElementTitleParams;
        object[] stringParam = new object[2];
        SecurityId? securityId = ((SubscriptionBase<Subscription>) subscription).SecurityId;
        
        string secCode;
        if ( !securityId.HasValue )
        {
            secCode = null;
        }
        else
        {
            SecurityId valueOrDefault = securityId.GetValueOrDefault();
            secCode = ( ( SecurityId ) valueOrDefault ).SecurityCode;
        }
        stringParam[0] = secCode;
        stringParam[1] = Ecng.ComponentModel.Extensions.GetDisplayName( ( ICustomAttributeProvider ) ( ( object ) this ).GetType( ), ( string ) null ).ToLower( );
        
        return StringHelper.Put( elementTitleParams, stringParam );
    }

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );

        BuyColor        = XamlHelper.ToColor( storage.GetValue<int>( "BuyColor", XamlHelper.ToInt( BuyColor ) ) );
        BuyStrokeColor  = XamlHelper.ToColor( storage.GetValue<int>( "BuyStrokeColor", XamlHelper.ToInt( BuyStrokeColor ) ) );
        SellColor       = XamlHelper.ToColor( storage.GetValue<int>( "SellColor", XamlHelper.ToInt( SellColor ) ) );
        SellStrokeColor = XamlHelper.ToColor( storage.GetValue<int>( "SellStrokeColor", XamlHelper.ToInt( SellStrokeColor ) ) );
        UseAltIcon      = storage.GetValue<bool>( "UseAltIcon", UseAltIcon );
        DrawSize        = storage.GetValue<double>( "DrawSize", DrawSize );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<int>( "BuyColor", XamlHelper.ToInt( BuyColor ) );
        storage.SetValue<int>( "BuyStrokeColor", XamlHelper.ToInt( BuyStrokeColor ) );
        storage.SetValue<int>( "SellColor", XamlHelper.ToInt( SellColor ) );
        storage.SetValue<int>( "SellStrokeColor", XamlHelper.ToInt( SellStrokeColor ) );
        storage.SetValue<bool>( "UseAltIcon", UseAltIcon );
        storage.SetValue<double>( "DrawSize", DrawSize );
    }

    internal override T Clone( T _param1 )
    {
        _param1.BuyColor = BuyColor;
        _param1.BuyStrokeColor = BuyStrokeColor;
        _param1.SellColor = SellColor;
        _param1.SellStrokeColor = SellStrokeColor;
        _param1.UseAltIcon = UseAltIcon;
        _param1.DrawSize = DrawSize;
        return base.Clone( _param1 );
    }

    System.Windows.Media.Color IChartElementUiDomain.Color => Colors.Transparent;
    

    System.Drawing.Color IChartTransactionElement.BuyColor
    {
        get => XamlHelper.FromWpf( BuyColor );
        set => BuyColor = XamlHelper.ToWpf( value );
    }

    System.Drawing.Color IChartTransactionElement.BuyStrokeColor
    {
        get => XamlHelper.FromWpf( BuyStrokeColor );
        set => BuyStrokeColor = XamlHelper.ToWpf( value );
    }

    System.Drawing.Color IChartTransactionElement.SellColor
    {
        get => XamlHelper.FromWpf( SellColor );
        set => SellColor = XamlHelper.ToWpf( value );
    }

    System.Drawing.Color IChartTransactionElement.SellStrokeColor
    {
        get => XamlHelper.FromWpf( SellStrokeColor );
        set => SellStrokeColor = XamlHelper.ToWpf( value );
    }

    ChartElementUiDomain IChartElementUiDomain.CreateViewModel( IDrawingSurfaceVM _param1 )
    {
        return _uiBusinessLogic = ( ChartElementUiDomain ) new \u0023\u003Dzboj3ckhISv7k6koCkTeIf5PCtYd46lRwlYVUyDC59V3Pkk_zmE1no4ED3cPT<T>( ( T ) this );
    }

    bool IChartElementUiDomain.StartDrawing(
      IEnumerableEx<ChartDrawData.IDrawValue> _param1 )
    {
        return _uiBusinessLogic.Draw( _param1 );
    }

    void IChartElementUiDomain.StartDrawing( )
    {
        _uiBusinessLogic.Draw( CollectionHelper.ToEx<ChartDrawData.IDrawValue>( Enumerable.Empty<ChartDrawData.IDrawValue>( ), 0 ) );
    }
}
