using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting;

[TypeConverter( typeof( ExpandableObjectConverter ) )]
public class ChartAxis :
  NotifiableObject,
  INotifyPropertyChanged,
  IChartAxis,
  INotifyPropertyChanging,
  IPersistable,
  INotifyPropertyChangedEx
{
    
    private IChartArea _chartArea;
    
    private string _id;
    
    private bool _isVisible = true;
    
    private string _title;
    
    private string _group;
    
    private bool _switchAxisLocation;
    
    private ChartAxisType _axisType;
    
    private bool _autoRange = true;
    
    private bool _flipCoordinates;
    
    private bool _drawMajorTicks = true;
    
    private bool _drawMajorGridLines = true;
    
    private bool _drawMinorTicks;
    
    private bool _drawMinorGridLines;
    
    private bool _drawLabels = true;
    
    private string _textFormatting;
    
    private string _cursorTextFormatting;
    
    private string _subDayTextFormatting;
    
    private TimeZoneInfo _timeZone;
    
    private double _dataPointWidth = double.NaN;

    [Browsable( false )]
    public IChartArea ChartArea
    {
        get => this._chartArea;
        internal set => this._chartArea = value;
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Identifier", Description = "UniqueId", GroupName = "Parameters", Order = 10 )]
    [Browsable( false )]
    public string Id
    {
        get => this._id;
        set
        {
            this._id = value;
            this.NotifyChanged( nameof( Id ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Show", Description = "ShowDot", GroupName = "Parameters", Order = 15 )]
    public bool IsVisible
    {
        get => this._isVisible;
        set
        {
            this._isVisible = value;
            this.NotifyChanged( nameof( IsVisible ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Header", Description = "AxisHeader", GroupName = "Parameters", Order = 20 )]
    public string Title
    {
        get => this._title;
        set
        {
            this._title = value;
            this.NotifyChanged( nameof( Title ) );
        }
    }

    [Browsable( false )]
    public string Group
    {
        get => this._group;
        set
        {
            if ( this._group == value )
                return;
            if ( this.ChartArea != null )
                throw new InvalidOperationException( LocalizedStrings.ErrorChangingGroupName );
            this._group = value;
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "SwitchAxisLocation", Description = "SwitchAxisLocation", GroupName = "Parameters", Order = 40 )]
    public bool SwitchAxisLocation
    {
        get => this._switchAxisLocation;
        set
        {
            this._switchAxisLocation = value;
            this.NotifyChanged( nameof( SwitchAxisLocation ) );
        }
    }

    [Browsable( false )]
    public ChartAxisType AxisType
    {
        get => this._axisType;
        set
        {
            if ( this._axisType != value && this.ChartArea != null )
                throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
            this._axisType = value;
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "AutoRange", Description = "AutoRangeDot", GroupName = "Parameters", Order = 60 )]
    public bool AutoRange
    {
        get => this._autoRange;
        set
        {
            this._autoRange = value;
            this.NotifyChanged( nameof( AutoRange ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "FlipCoords", Description = "FlipCoordsDot", GroupName = "Parameters", Order = 70 )]
    public bool FlipCoordinates
    {
        get => this._flipCoordinates;
        set
        {
            this._flipCoordinates = value;
            this.NotifyChanged( nameof( FlipCoordinates ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "LinesOnAxis", Description = "MainGridLinesOnAxis", GroupName = "Parameters", Order = 80 /*0x50*/)]
    public bool DrawMajorTicks
    {
        get => this._drawMajorTicks;
        set
        {
            this._drawMajorTicks = value;
            this.NotifyChanged( nameof( DrawMajorTicks ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "GridLines", Description = "ShowMainGridLines", GroupName = "Parameters", Order = 90 )]
    public bool DrawMajorGridLines
    {
        get => this._drawMajorGridLines;
        set
        {
            this._drawMajorGridLines = value;
            this.NotifyChanged( nameof( DrawMajorGridLines ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "ExtraLinesOnAxis", Description = "ShowExtraLinesOnAxis", GroupName = "Parameters", Order = 100 )]
    public bool DrawMinorTicks
    {
        get => this._drawMinorTicks;
        set
        {
            this._drawMinorTicks = value;
            this.NotifyChanged( nameof( DrawMinorTicks ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "ExtraGridLines", Description = "ShowExtraGridLines", GroupName = "Parameters", Order = 110 )]
    public bool DrawMinorGridLines
    {
        get => this._drawMinorGridLines;
        set
        {
            this._drawMinorGridLines = value;
            this.NotifyChanged( nameof( DrawMinorGridLines ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "AxisLabels", Description = "ShowAxisLabels", GroupName = "Parameters", Order = 120 )]
    public bool DrawLabels
    {
        get => this._drawLabels;
        set
        {
            this._drawLabels = value;
            this.NotifyChanged( nameof( DrawLabels ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "LabelsFormat", Description = "LabelsFormatDot", GroupName = "Parameters", Order = 130 )]
    public string TextFormatting
    {
        get
        {
            string z5wsolu6uzY0L = this._textFormatting;
            if ( z5wsolu6uzY0L != null )
                return z5wsolu6uzY0L;
            return this.AxisType == ChartAxisType.Numeric ? "#.#########" : "d";
        }
        set
        {
            this._textFormatting = value;
            this.NotifyChanged( nameof( TextFormatting ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Cursor", Description = "CursorTextFormat", GroupName = "Parameters", Order = 131 )]
    public string CursorTextFormatting
    {
        get
        {
            string z6SqmY60jwVyp = this._cursorTextFormatting;
            if ( z6SqmY60jwVyp != null )
                return z6SqmY60jwVyp;
            return this.AxisType == ChartAxisType.Numeric ? "#.####" : "d";
        }
        set
        {
            this._cursorTextFormatting = value;
            this.NotifyChanged( nameof( CursorTextFormatting ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "LabelsFormatIntraday", Description = "LabelsFormatIntradayDescDot", GroupName = "Parameters", Order = 132 )]
    public string SubDayTextFormatting
    {
        get => this._subDayTextFormatting ?? "T";
        set
        {
            this._subDayTextFormatting = value;
            this.NotifyChanged( nameof( SubDayTextFormatting ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "TimeZone", Description = "TimeZoneDot", GroupName = "Parameters", Order = 133 )]
    public TimeZoneInfo TimeZone
    {
        get => this._timeZone;
        set
        {
            this._timeZone = value;
            this.NotifyChanged( nameof( TimeZone ) );
        }
    }

    [Browsable( false )]
    public double DataPointWidth
    {
        get => this._dataPointWidth;
        internal set
        {
            if ( Math.Abs( this._dataPointWidth - value ) < 0.3 )
                return;
            this._dataPointWidth = value;
            this.NotifyChanged( nameof( DataPointWidth ) );
        }
    }

    public void Load( SettingsStorage storage )
    {
        this.Id = storage.GetValue<string>( "Id", ( string ) null ) ?? storage.GetValue<string>( "Name", ( string ) null );
        this.Title = storage.GetValue<string>( "Title", ( string ) null );
        this.IsVisible = storage.GetValue<bool>( "IsVisible", this.IsVisible );
        this.Group = storage.GetValue<string>( "Group", this.Group );
        this.AutoRange = storage.GetValue<bool>( "AutoRange", this.AutoRange );
        this.DrawMinorTicks = storage.GetValue<bool>( "DrawMinorTicks", this.DrawMinorTicks );
        this.DrawMajorTicks = storage.GetValue<bool>( "DrawMajorTicks", this.DrawMajorTicks );
        this.DrawMajorGridLines = storage.GetValue<bool>( "DrawMajorGridLines", this.DrawMajorGridLines );
        this.DrawMinorGridLines = storage.GetValue<bool>( "DrawMinorGridLines", this.DrawMinorGridLines );
        this.DrawLabels = storage.GetValue<bool>( "DrawLabels", this.DrawLabels );
        this.TextFormatting = storage.GetValue<string>( "TextFormatting", this.TextFormatting );
        this.CursorTextFormatting = storage.GetValue<string>( "CursorTextFormatting", this.CursorTextFormatting );
        this.SubDayTextFormatting = storage.GetValue<string>( "SubDayTextFormatting", this.SubDayTextFormatting );
        this.SwitchAxisLocation = storage.GetValue<bool>( "SwitchAxisLocation", this.SwitchAxisLocation );
        this.AxisType = storage.GetValue<ChartAxisType>( "AxisType", ChartAxisType.DateTime );
        this.TimeZone = Converter.To<TimeZoneInfo>( ( object ) storage.GetValue<string>( "TimeZone", ( string ) null ) ) ?? TimeZoneInfo.Local;
    }

    public void Save( SettingsStorage storage )
    {
        storage.SetValue<string>( "Id", this.Id );
        storage.SetValue<string>( "Title", this.Title );
        storage.SetValue<bool>( "IsVisible", this.IsVisible );
        storage.SetValue<string>( "Group", this.Group );
        storage.SetValue<bool>( "AutoRange", this.AutoRange );
        storage.SetValue<bool>( "DrawMinorTicks", this.DrawMinorTicks );
        storage.SetValue<bool>( "DrawMajorTicks", this.DrawMajorTicks );
        storage.SetValue<bool>( "DrawMajorGridLines", this.DrawMajorGridLines );
        storage.SetValue<bool>( "DrawMinorGridLines", this.DrawMinorGridLines );
        storage.SetValue<bool>( "DrawLabels", this.DrawLabels );
        storage.SetValue<string>( "TextFormatting", this.TextFormatting );
        storage.SetValue<string>( "CursorTextFormatting", this.CursorTextFormatting );
        storage.SetValue<string>( "SubDayTextFormatting", this.SubDayTextFormatting );
        storage.SetValue<bool>( "SwitchAxisLocation", this.SwitchAxisLocation );
        storage.SetValue<ChartAxisType>( "AxisType", this.AxisType );
        storage.SetValue<string>( "TimeZone", this.TimeZone?.Id );
    }
}
