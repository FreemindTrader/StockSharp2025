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

/// <summary>
/// The chart axis.
/// </summary>
[TypeConverter( typeof( ExpandableObjectConverter ) )]
public class ChartAxis : NotifiableObject,
                          IChartAxis,
                          IPersistable,
                          INotifyPropertyChangedEx,
                          INotifyPropertyChanged,
                          INotifyPropertyChanging
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

    /// <inheritdoc />
    [Browsable( false )]
    public IChartArea ChartArea
    {
        get => _chartArea;
        internal set => _chartArea = value;
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Identifier", Description = "UniqueId", GroupName = "Parameters", Order = 10 )]
    [Browsable( false )]
    public string Id
    {
        get => _id;
        set
        {
            _id = value;
            NotifyChanged( nameof( Id ) );
        }
    }



    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Show", Description = "ShowDot", GroupName = "Parameters", Order = 15 )]
    public bool IsVisible
    {
        get
        {
            return _isVisible;
        }
        set
        {
            _isVisible = value;
            NotifyChanged( nameof( IsVisible ) );
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Header", Description = "AxisHeader", GroupName = "Parameters", Order = 20 )]
    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            _title = value;
            NotifyChanged( nameof( Title ) );
        }
    }

    /// <inheritdoc />
    [Browsable( false )]
    public string Group
    {
        get => _group;
        set
        {
            if ( _group == value )
                return;
            if ( ChartArea != null )
                throw new InvalidOperationException( LocalizedStrings.ErrorChangingGroupName );
            _group = value;
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "SwitchAxisLocation", Description = "SwitchAxisLocation", GroupName = "Parameters", Order = 40 )]
    public bool SwitchAxisLocation
    {
        get
        {
            return _switchAxisLocation;
        }
        set
        {
            _switchAxisLocation = value;
            NotifyChanged( nameof( SwitchAxisLocation ) );
        }
    }



    /// <inheritdoc />
    [Browsable( false )]
    public ChartAxisType AxisType
    {
        get => _axisType;
        set
        {
            if ( _axisType != value && ChartArea != null )
                throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
            _axisType = value;
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "AutoRange", Description = "AutoRangeDot", GroupName = "Parameters", Order = 60 )]
    public bool AutoRange
    {
        get
        {
            return _autoRange;
        }
        set
        {
            _autoRange = value;
            NotifyChanged( nameof( AutoRange ) );
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "FlipCoords", Description = "FlipCoordsDot", GroupName = "Parameters", Order = 70 )]
    public bool FlipCoordinates
    {
        get
        {
            return _flipCoordinates;
        }
        set
        {
            _flipCoordinates = value;
            NotifyChanged( nameof( FlipCoordinates ) );
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "LinesOnAxis", Description = "MainGridLinesOnAxis", GroupName = "Parameters", Order = 80 /*0x50*/)]
    public bool DrawMajorTicks
    {
        get
        {
            return _drawMajorTicks;
        }
        set
        {
            _drawMajorTicks = value;
            NotifyChanged( nameof( DrawMajorTicks ) );
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "GridLines", Description = "ShowMainGridLines", GroupName = "Parameters", Order = 90 )]
    public bool DrawMajorGridLines
    {
        get
        {
            return _drawMajorGridLines;
        }
        set
        {
            _drawMajorGridLines = value;
            NotifyChanged( nameof( DrawMajorGridLines ) );
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "ExtraLinesOnAxis", Description = "ShowExtraLinesOnAxis", GroupName = "Parameters", Order = 100 )]
    public bool DrawMinorTicks
    {
        get
        {
            return _drawMinorTicks;
        }
        set
        {
            _drawMinorTicks = value;
            NotifyChanged( nameof( DrawMinorTicks ) );
        }
    }


    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "ExtraGridLines", Description = "ShowExtraGridLines", GroupName = "Parameters", Order = 110 )]
    public bool DrawMinorGridLines
    {
        get
        {
            return _drawMinorGridLines;
        }
        set
        {
            _drawMinorGridLines = value;
            NotifyChanged( nameof( DrawMinorGridLines ) );
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "AxisLabels", Description = "ShowAxisLabels", GroupName = "Parameters", Order = 120 )]
    public bool DrawLabels
    {
        get
        {
            return _drawLabels;
        }
        set
        {
            _drawLabels = value;
            NotifyChanged( nameof( DrawLabels ) );
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "LabelsFormat", Description = "LabelsFormatDot", GroupName = "Parameters", Order = 130 )]
    public string TextFormatting
    {
        get
        {
            if ( _textFormatting != null )
            {
                return _textFormatting;
            }
            return AxisType == ChartAxisType.Numeric ? "#.####" : "d";
        }
        set
        {
            _textFormatting = value;
            NotifyChanged( nameof( TextFormatting ) );
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Cursor", Description = "CursorTextFormat", GroupName = "Parameters", Order = 131 )]
    public string CursorTextFormatting
    {
        get
        {
            if ( _cursorTextFormatting != null )
                return _cursorTextFormatting;
            return AxisType == ChartAxisType.Numeric ? "#.####" : "d";
        }
        set
        {
            _cursorTextFormatting = value;
            NotifyChanged( nameof( CursorTextFormatting ) );
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "LabelsFormatIntraday", Description = "LabelsFormatIntradayDescDot", GroupName = "Parameters", Order = 132 )]
    public string SubDayTextFormatting
    {
        get
        {
            return _subDayTextFormatting ?? "T";
        }
        set
        {
            _subDayTextFormatting = value;
            NotifyChanged( nameof( SubDayTextFormatting ) );
        }
    }

    /// <inheritdoc />
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "TimeZone", Description = "TimeZoneDot", GroupName = "Parameters", Order = 133 )]
    public TimeZoneInfo TimeZone
    {
        get
        {
            return _timeZone;
        }
        set
        {
            _timeZone = value;
            NotifyChanged( nameof( TimeZone ) );
        }
    }

    /// <summary>Current data point with for X axis.</summary>
    [Browsable( false )]
    public double DataPointWidth
    {
        get
        {
            return _dataPointWidth;
        }
        internal set
        {
            if ( Math.Abs( _dataPointWidth - value ) < 0.3 )
            {
                return;
            }
            _dataPointWidth = value;
            NotifyChanged( nameof( DataPointWidth ) );
        }
    }



    /// <summary>Load settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public void Load( SettingsStorage storage )
    {
        Id                   = storage.GetValue<string>( "Id", ( string ) null ) ?? storage.GetValue<string>( "Name", ( string ) null );
        Title                = storage.GetValue<string>( "Title", ( string ) null );
        IsVisible            = storage.GetValue<bool>( "IsVisible", IsVisible );
        Group                = storage.GetValue<string>( "Group", Group );
        AutoRange            = storage.GetValue<bool>( "AutoRange", AutoRange );
        DrawMinorTicks       = storage.GetValue<bool>( "DrawMinorTicks", DrawMinorTicks );
        DrawMajorTicks       = storage.GetValue<bool>( "DrawMajorTicks", DrawMajorTicks );
        DrawMajorGridLines   = storage.GetValue<bool>( "DrawMajorGridLines", DrawMajorGridLines );
        DrawMinorGridLines   = storage.GetValue<bool>( "DrawMinorGridLines", DrawMinorGridLines );
        DrawLabels           = storage.GetValue<bool>( "DrawLabels", DrawLabels );
        TextFormatting       = storage.GetValue<string>( "TextFormatting", TextFormatting );
        CursorTextFormatting = storage.GetValue<string>( "CursorTextFormatting", CursorTextFormatting );
        SubDayTextFormatting = storage.GetValue<string>( "SubDayTextFormatting", SubDayTextFormatting );
        SwitchAxisLocation   = storage.GetValue<bool>( "SwitchAxisLocation", SwitchAxisLocation );
        AxisType             = storage.GetValue<ChartAxisType>( "AxisType", ChartAxisType.DateTime );

        if ( Id == "Y" )
        {
            AxisType = ChartAxisType.Numeric;
        }

        TimeZone = Converter.To<TimeZoneInfo>( ( object ) storage.GetValue<string>( "TimeZone", ( string ) null ) ) ?? TimeZoneInfo.Local;
    }

    /// <summary>Save settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public void Save( SettingsStorage storage )
    {
        storage.SetValue<string>( "Id", Id );
        storage.SetValue<string>( "Title", Title );
        storage.SetValue<bool>( "IsVisible", IsVisible );
        storage.SetValue<string>( "Group", Group );
        storage.SetValue<bool>( "AutoRange", AutoRange );
        storage.SetValue<bool>( "DrawMinorTicks", DrawMinorTicks );
        storage.SetValue<bool>( "DrawMajorTicks", DrawMajorTicks );
        storage.SetValue<bool>( "DrawMajorGridLines", DrawMajorGridLines );
        storage.SetValue<bool>( "DrawMinorGridLines", DrawMinorGridLines );
        storage.SetValue<bool>( "DrawLabels", DrawLabels );
        storage.SetValue<string>( "TextFormatting", TextFormatting );
        storage.SetValue<string>( "CursorTextFormatting", CursorTextFormatting );
        storage.SetValue<string>( "SubDayTextFormatting", SubDayTextFormatting );
        storage.SetValue<bool>( "SwitchAxisLocation", SwitchAxisLocation );
        storage.SetValue<ChartAxisType>( "AxisType", AxisType );
        storage.SetValue<string>( "TimeZone", TimeZone?.Id );
    }
}



//using Ecng.Common;
//using Ecng.ComponentModel;
//using Ecng.Serialization;
//using SciChart.Charting.Common;
//using StockSharp.Localization;
//using StockSharp.Xaml.Charting.Definitions;
//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using DevExpress.Mvvm.Native;

//namespace StockSharp.Xaml.Charting
//{
//    [TypeConverter( typeof( ExpandableObjectConverter ) )]
//    public class ChartAxis : NotifiableObject, IPersistable
//    {
//        private bool _isVisible = true;
//        private bool _autoRange = true;
//        private bool _drawMajorTicks = true;
//        private bool _drawMinorTicks = true;
//        private bool _drawMajorGridLines = false;
//        private bool _drawMinorGridLines = false;
//        private bool _drawMajorBands = false;
//        private bool _drawLabels = true;
//        private double _dataPointWidth = double.NaN;

//        private ChartArea area;
//        private string _id;
//        private string _title;
//        private string _group;
//        //private ChartAxisAlignment _axisAlignment = ChartAxisAlignment.Default;
//        private ChartAxisType _axisType;
//        private bool _flipCoordinates;


//        private string string_3;
//        private string _subDayTextFormatting;
//        private TimeZoneInfo _timeZone;

//        [Browsable( false )]
//        public ChartArea ChartArea
//        {
//            get
//            {
//                return area;
//            }
//            internal set
//            {
//                area = value;
//            }
//        }

//        [Browsable( false )]
//        public bool IsDefault
//        {
//            get
//            {
//                if( !( Id == ChartArea.XAxisId ) )
//                {
//                    return Id == ChartArea.YAxisId;
//                }
//                return true;
//            }
//        }

//        [Display( Description = "Str1917", GroupName = "Str225", Name = "Str361", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
//        [Browsable( false )]
//        public string Id
//        {
//            get
//            {
//                return _id;
//            }
//            set
//            {
//                _id = value;
//                NotifyChanged( nameof( Id ) );
//            }
//        }

//        [Display( Description = "Str2933Dot", GroupName = "Str225", Name = "Str2933", Order = 15, ResourceType = typeof( LocalizedStrings ) )]
//        public bool IsVisible
//        {
//            get
//            {
//                return _isVisible;
//            }
//            set
//            {
//                _isVisible = value;
//                NotifyChanged( nameof( IsVisible ) );
//            }
//        }

//        [Display( Description = "Str1918", GroupName = "Str225", Name = "Str215", Order = 20, ResourceType = typeof( LocalizedStrings ) )]
//        public string Title
//        {
//            get
//            {
//                return _title;
//            }
//            set
//            {
//                _title = value;
//                NotifyChanged( nameof( Title ) );
//            }
//        }

//        [Display( Description = "Str1920", GroupName = "Str225", Name = "Group", Order = 30, ResourceType = typeof( LocalizedStrings ) )]
//        public string Group
//        {
//            get
//            {
//                return _group;
//            }
//            set
//            {
//                if ( _group == value)
//                    return;

//                if ( ChartArea != null )
//                {
//                    throw new InvalidOperationException( LocalizedStrings.ErrorChangingGroupName );
//                }

//                _group = value;

//                //_group = value;
//                //NotifyChanged( nameof( Group ) );
//            }
//        }

//        [Display( Description = "Str1922", GroupName = "Str225", Name = "Str1921", Order = 40, ResourceType = typeof( LocalizedStrings ) )]
//        [Obsolete]
//        public ChartAxisAlignment AxisAlignment
//        {
//            get
//            {
//                return !SwitchAxisLocation ? ChartAxisAlignment.Default : ChartAxisAlignment.Left;
//            }
//            set
//            {
//                SwitchAxisLocation = ( uint ) value > 0U;
//                NotifyChanged( nameof( AxisAlignment ) );
//            }
//        }

//        private bool _switchAxisLocation;
//        /// <summary>Whether to use alternative axis alignment.</summary>
//        [Display( Description = "SwitchAxisLocation", GroupName = "Str225", Name = "SwitchAxisLocation", Order = 40, ResourceType = typeof( LocalizedStrings ) )]
//        public bool SwitchAxisLocation
//        {
//            get
//            {
//                return _switchAxisLocation;
//            }
//            set
//            {
//                _switchAxisLocation = value;
//                NotifyChanged( nameof( SwitchAxisLocation ) );
//            }
//        }

//        //[Display( Description = "Str1922", GroupName = "Str225", Name = "Str1921", Order = 40, ResourceType = typeof( LocalizedStrings ) )]
//        //public ChartAxisAlignment AxisAlignment
//        //{
//        //    get
//        //    {
//        //        return _axisAlignment;
//        //    }
//        //    set
//        //    {
//        //        _axisAlignment = value;
//        //        NotifyChanged( nameof( AxisAlignment ) );
//        //    }
//        //}

//        [Browsable( false )]
//        public ChartAxisType AxisType
//        {
//            get
//            {
//                return _axisType;
//            }
//            set
//            {
//                if( _axisType != value && ChartArea != null )
//                {
//                    throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
//                }
//                _axisType = value;
//            }
//        }

//        [Display( Description = "Str1924Dot", GroupName = "Str225", Name = "Str1924", Order = 60, ResourceType = typeof( LocalizedStrings ) )]
//        public bool AutoRange
//        {
//            get
//            {
//                return _autoRange;
//            }
//            set
//            {
//                _autoRange = value;
//                NotifyChanged( nameof( AutoRange ) );
//            }
//        }

//        [Display( Description = "Str1926Dot", GroupName = "Str225", Name = "Str1926", Order = 70, ResourceType = typeof( LocalizedStrings ) )]
//        public bool FlipCoordinates
//        {
//            get
//            {
//                return _flipCoordinates;
//            }
//            set
//            {
//                _flipCoordinates = value;
//                NotifyChanged( nameof( FlipCoordinates ) );
//            }
//        }

//        [Display( Description = "Str1929", GroupName = "Str225", Name = "Str1928", Order = 80, ResourceType = typeof( LocalizedStrings ) )]
//        public bool DrawMajorTicks
//        {
//            get
//            {
//                return _drawMajorTicks;
//            }
//            set
//            {
//                _drawMajorTicks = value;
//                NotifyChanged( nameof( DrawMajorTicks ) );
//            }
//        }

//        [Display( Description = "Str1931", GroupName = "Str225", Name = "Str1930", Order = 90, ResourceType = typeof( LocalizedStrings ) )]
//        public bool DrawMajorGridLines
//        {
//            get
//            {
//                return _drawMajorGridLines;
//            }
//            set
//            {
//                _drawMajorGridLines = value;
//                NotifyChanged( nameof( DrawMajorGridLines ) );
//            }
//        }


//        public bool DrawMajorBands
//        {
//            get
//            {
//                return _drawMajorBands;
//            }
//            set
//            {
//                _drawMajorBands = value;
//                NotifyChanged( nameof( DrawMajorBands ) );
//            }
//        }

//        [Display( Description = "Str1933", GroupName = "Str225", Name = "Str1932", Order = 100, ResourceType = typeof( LocalizedStrings ) )]
//        public bool DrawMinorTicks
//        {
//            get
//            {
//                return _drawMinorTicks;
//            }
//            set
//            {
//                _drawMinorTicks = value;
//                NotifyChanged( nameof( DrawMinorTicks ) );
//            }
//        }

//        [Display( Description = "Str1935", GroupName = "Str225", Name = "Str1934", Order = 110, ResourceType = typeof( LocalizedStrings ) )]
//        public bool DrawMinorGridLines
//        {
//            get
//            {
//                return _drawMinorGridLines;
//            }
//            set
//            {
//                _drawMinorGridLines = value;
//                NotifyChanged( nameof( DrawMinorGridLines ) );
//            }
//        }

//        [Display( Description = "Str1937", GroupName = "Str225", Name = "Str1936", Order = 120, ResourceType = typeof( LocalizedStrings ) )]
//        public bool DrawLabels
//        {
//            get
//            {
//                return _drawLabels;
//            }
//            set
//            {
//                _drawLabels = value;
//                NotifyChanged( nameof( DrawLabels ) );
//            }
//        }

//        [Display( Description = "Str1938Dot", GroupName = "Str225", Name = "Str1938", Order = 130, ResourceType = typeof( LocalizedStrings ) )]
//        public string TextFormatting
//        {
//            get
//            {
//                string string3 = string_3;
//                if( string3 != null )
//                {
//                    return string3;
//                }
//                return AxisType == ChartAxisType.Numeric ? "#.####" : "d";
//            }
//            set
//            {
//                string_3 = value;
//                NotifyChanged( nameof( TextFormatting ) );
//            }
//        }

//        [Display( Description = "LabelsFormatIntradayDescDot", GroupName = "Str225", Name = "LabelsFormatIntraday", Order = 131, ResourceType = typeof( LocalizedStrings ) )]
//        public string SubDayTextFormatting
//        {
//            get
//            {
//                return _subDayTextFormatting ?? "T";
//            }
//            set
//            {
//                _subDayTextFormatting = value;
//                NotifyChanged( nameof( SubDayTextFormatting ) );
//            }
//        }

//        [Display( Description = "TimeZoneDot", GroupName = "Str225", Name = "TimeZone", Order = 132, ResourceType = typeof( LocalizedStrings ) )]
//        public TimeZoneInfo TimeZone
//        {
//            get
//            {
//                return _timeZone;
//            }
//            set
//            {
//                _timeZone = value;
//                NotifyChanged( nameof( TimeZone ) );
//            }
//        }

//        [Browsable( false )]
//        public double DataPointWidth
//        {
//            get
//            {
//                return _dataPointWidth;
//            }
//            internal set
//            {
//                if( Math.Abs( _dataPointWidth - value ) < 0.3 )
//                {
//                    return;
//                }
//                _dataPointWidth = value;
//                NotifyChanged( nameof( DataPointWidth ) );
//            }
//        }        

//        public void Load( SettingsStorage storage )
//        {
//            Id                   = storage.GetValue( "Id", ( string )null ) ?? storage.GetValue( "Name", ( string )null );
//            Title                = storage.GetValue( "Title", ( string )null );
//            IsVisible            = storage.GetValue( "IsVisible", false );
//            Group                = storage.GetValue( "Group", ( string )null );
//            AutoRange            = storage.GetValue( "AutoRange", false );
//            DrawMinorTicks       = storage.GetValue( "DrawMinorTicks", false );
//            DrawMajorTicks       = storage.GetValue( "DrawMajorTicks", false );
//            DrawMajorGridLines   = storage.GetValue( "DrawMajorGridLines", false );
//            DrawMinorGridLines   = storage.GetValue( "DrawMinorGridLines", false );
//            DrawLabels           = storage.GetValue( "DrawLabels", false );
//            TextFormatting       = storage.GetValue( "TextFormatting", ( string )null );
//            SubDayTextFormatting = storage.GetValue( "SubDayTextFormatting", SubDayTextFormatting );
//            SwitchAxisLocation = storage.GetValue( nameof( SwitchAxisLocation ), false );            
//            AxisType             = storage.GetValue( "AxisType", ChartAxisType.DateTime );

//            if ( Id == "Y" )
//            {
//                AxisType = ChartAxisType.Numeric;
//            }

//            TimeZone             = MayBe.With(storage.GetValue("TimeZone", (string)null), new Func< string, TimeZoneInfo >( TimeZoneInfo.FindSystemTimeZoneById ) ) ?? TimeZoneInfo.Local;
//        }

//        public void Save( SettingsStorage storage )
//        {
//            storage.SetValue( "Id", Id );
//            storage.SetValue( "Title", Title );
//            storage.SetValue( "IsVisible", IsVisible );
//            storage.SetValue( "Group", Group );
//            storage.SetValue( "AutoRange", AutoRange );
//            storage.SetValue( "DrawMinorTicks", DrawMinorTicks );
//            storage.SetValue( "DrawMajorTicks", DrawMajorTicks );
//            storage.SetValue( "DrawMajorGridLines", DrawMajorGridLines );
//            storage.SetValue( "DrawMinorGridLines", DrawMinorGridLines );
//            storage.SetValue( "DrawLabels", DrawLabels );
//            storage.SetValue( "TextFormatting", TextFormatting );
//            storage.SetValue( "SubDayTextFormatting", SubDayTextFormatting );
//            storage.SetValue( nameof( SwitchAxisLocation ), SwitchAxisLocation );            
//            storage.SetValue( "AxisType", AxisType );
//            storage.SetValue( "TimeZone", TimeZone?.Id );
//        }

//        //public event Action< SciChart.Charting.Visuals.Events.VisibleRangeChangedEventArgs > VisibleRangeChangedEvent;

//        //private void VisibleRangeChanged( object sender, SciChart.Charting.Visuals.Events.VisibleRangeChangedEventArgs e )
//        //{
//        //    VisibleRangeChangedEvent?.Invoke( e );
//        //}
//    }
//}

// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartAxis
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

