using System;

namespace fx.Definitions
{    
    public enum SelectionModeEnum
    {
        Select,
        RectangleZoom,
        HorizontalZoom,
        VerticalZoom,
        SelectWavesOrLines,
        None
    }

    public enum ScrollModeEnum
    {
        HorizontalScroll = 1,
        HorizontalScrollAndFit = 2,
        VerticalScroll = 3,
        HorizontalZoom = 4,
        VerticalZoom = 5,
        ZoomToMouse = 6,
        None
    }

    public enum AppearanceModeEnum
    {
        Normal,
        Compact, // Compact mode removes allot of the additional space and items around the chart pane.
        SuperCompact // Same like compact, only more.
    }

    public enum AppearanceSchemeEnum
    {
        Default,
        Custom,
        Fast,
        Trade,
        TradeWhite,
        Dark,
        DarkNatural,
        Light,
        LightNatural,
        LightNaturalFlat,
        Alfonsina,
        Ground
    }

    public enum YAxisLabelPosition
    {
        Left,
        Right,
        Both,
        None
    }
}
