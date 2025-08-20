using SciChart.Charting.Visuals.PointMarkers;
using fx.Definitions;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml.Charting
{
    public enum TASignalSymbol : byte
    {
        Ellipse,
        MACD_CROSS_DOWN,
        MACD_CROSS_UP,
        ExitOverBrought,
        ExitOverSold,
        PivotPoint,
        CurrentPrice_TimeElapsed,
        PriorTrendTime_CurrentTrendRange,
        PriorPriceRange_CurrentTrendTime,
        PriorEndingPrice_CurrentTrendTime,
        CurrentTrendRange_CurrentTrendTime,
        WaveRotation_HHLL,
        WaveRotation_HLLH,
        WaveRotation_Correction,
        WaveRotation_BarCount,
        GannImportance,
        WaveImportance,
        PositiveDivergence,
        ImportantPositive,
        NegativeDivergence,
        ImportantNegative,
        HiddenNegDiv,
        HiddenPosDiv,
        ImportantHiddenNegDiv,
        ImportantHiddenPosDiv,
    }

    static class FreemindTaXaml
    {
        public static ControlTemplate GetSpriteControlTemplate( TASignalSymbol symbol, int width, int colorInt )
        {
            string xaml = "";
            switch ( symbol )
            {
                case TASignalSymbol.GannImportance:
                xaml = GetGannImportance( width, colorInt );
                break;

                case TASignalSymbol.WaveImportance:
                xaml = GetWaveImportance( width, colorInt );
                break;

                case TASignalSymbol.PositiveDivergence:
                xaml = GetPositiveDivergence( width, colorInt );
                break;

                case TASignalSymbol.ImportantPositive:
                xaml = GetImportantPositiveDivergence( width, colorInt, colorInt );
                break;

                case TASignalSymbol.ImportantHiddenPosDiv:
                xaml = GetImptHiddenPositiveDivergence( width, colorInt );
                break;

                case TASignalSymbol.HiddenPosDiv:
                xaml = GetHiddenPositiveDivergence( width, colorInt );
                break;

                case TASignalSymbol.NegativeDivergence:
                xaml = GetNegativeDivergence( width, colorInt );
                break;

                case TASignalSymbol.ImportantNegative:
                xaml = GetImportantNegativeDivergence( width, colorInt, colorInt );
                break;

                case TASignalSymbol.ImportantHiddenNegDiv:
                xaml = GetImptHiddenNegativeDivergence( width, colorInt );
                break;

                case TASignalSymbol.HiddenNegDiv:
                xaml = GetHiddenNegativeDivergence( width, colorInt );
                break;

                case TASignalSymbol.WaveRotation_HHLL:
                xaml = GetHighHighF( width, colorInt );
                break;

                case TASignalSymbol.WaveRotation_HLLH:
                xaml = GetHighLowF( width, colorInt );
                break;

                case TASignalSymbol.WaveRotation_Correction:
                xaml = GetCorrection( width, colorInt );
                break;

                case TASignalSymbol.WaveRotation_BarCount:
                xaml = GetBarSum( width, colorInt );
                break;                
            }

            return GetControlTemplateFromString( xaml );
        }

        public static FxSpritePointMarker GetPivotPointMarker( int width, int colorInt )
        {
            var sprite = new FxSpritePointMarker( );

            sprite.PointMarkerTemplate = GetControlTemplateFromString( GetPivotPoint( width, colorInt ) );

            return sprite;
        }

        public static FxSpritePointMarker GetBuyPointMarker( int width )
        {
            var sprite = new FxSpritePointMarker( );

            sprite.PointMarkerTemplate = GetControlTemplateFromString( GetBuy( width ) );

            return sprite;
        }

        public static FxSpritePointMarker GetSellPointMarker( int width )
        {
            var sprite = new FxSpritePointMarker( );

            sprite.PointMarkerTemplate = GetControlTemplateFromString( GetSell( width ) );

            return sprite;
        }

        public static FxSpritePointMarker GetExitOverboughPointMarker( int width )
        {
            var sprite = new FxSpritePointMarker( );

            int colorOne = 0xF44336;
            //int colorTwo = 0xBF360C;

            sprite.PointMarkerTemplate = GetControlTemplateFromString( GetExitOverBought( width, colorOne ) );

            return sprite;
        }

        public static FxSpritePointMarker GetExitOverSoldPointMarker( int width )
        {
            var sprite = new FxSpritePointMarker( );

            int colorOne = 0x26B99A;
            //int colorTwo = 0xBF360C;

            sprite.PointMarkerTemplate = GetControlTemplateFromString( GetExitOverSold( width, colorOne ) );

            return sprite;
        }

        public static FxSpritePointMarker GetTAToppingSignalPointMarker( TAToppingSignal topSignal, int ptSize )
        {
            FxSpritePointMarker pointMarker = null;

            if ( topSignal == TAToppingSignal.MACD_CROSS_DOWN )
            {
                pointMarker = GetSellPointMarker( ptSize );
            }
            else if ( topSignal == TAToppingSignal.ExitOverBought )
            {
                pointMarker = GetExitOverboughPointMarker( ptSize );
            }
            else if ( topSignal == TAToppingSignal.OscillatorCrossDown )
            {

            }
            else if ( topSignal == TAToppingSignal.OscNegativeDivergence )
            {

            }
            
            else if ( topSignal == TAToppingSignal.ComasTurnDown )
            {

            }
            else if ( topSignal == TAToppingSignal.ComasCrossDown )
            {

            }
            else if ( topSignal == TAToppingSignal.OscillatorSmoothDown )
            {

            }

            return pointMarker;
        }

        public static FxSpritePointMarker GetTABottomingSignalPointMarker( TABottomingSignal bottomSignal, int ptSize )
        {
            FxSpritePointMarker pointMarker = null;

            if ( bottomSignal == TABottomingSignal.MACD_CROSS_UP )
            {
                pointMarker = GetBuyPointMarker( ptSize );
            }            
            else if ( bottomSignal == TABottomingSignal.ExitOverSold )
            {
                pointMarker = GetExitOverSoldPointMarker( ptSize );
            }
                        

            return pointMarker;
        }

        public static FxSpritePointMarker GetPriceTimePointMarker( TaGannPriceTimeType rot, GannDegreesType waveTimeType, int width, int colorInt )
        {
            var sprite = new FxSpritePointMarker( );

            if ( rot == TaGannPriceTimeType.CurrentPrice_TimeElapsed_Major || 
                 rot == TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_Major || 
                 rot == TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_Major || 
                 rot == TaGannPriceTimeType.PriorEndingPrice_CurrentTrendTime_Major || 
                 rot == TaGannPriceTimeType.CurrentTrendRange_CurrentTrendTime_Major )
            {
                switch ( waveTimeType )
                {
                    case GannDegreesType.Gann_45Dg_Multiple:
                    case GannDegreesType.Gann_45Dg_Calendar:
                    case GannDegreesType.Gann_45Dg_Nearby:
                    case GannDegreesType.Gann_45Dg_Nearby_Calendar:
                    {                        
                        sprite.PointMarkerTemplate = GetControlTemplateFromString( Get90Deg( width, colorInt ) );                        
                    }                    
                    break;

                    case GannDegreesType.Gann_30Dg_Multiple:
                    case GannDegreesType.Gann_30Dg_Calendar:
                    case GannDegreesType.Gann_30Dg_Nearby:
                    case GannDegreesType.Gann_30Dg_Nearby_Calendar:
                    {                        
                        sprite.PointMarkerTemplate = GetControlTemplateFromString( Get60Deg( width, colorInt ) );
                    }                    
                    break;

                    case GannDegreesType.Gann_SpecialSeq:
                    case GannDegreesType.Gann_SpecialSeq_Cal:
                    {                        
                        sprite.PointMarkerTemplate = GetControlTemplateFromString( GetSpecialDeg( width, colorInt ) );
                    }                    
                    break;
                }
            }
            else if ( rot == TaGannPriceTimeType.CurrentPrice_TimeElapsed_RedDot || 
                      rot == TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_RedDot || 
                      rot == TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_RedDot || 
                      rot == TaGannPriceTimeType.PriorEndingPrice_CurrentTrendTime_RedDot || 
                      rot == TaGannPriceTimeType.CurrentTrendRange_CurrentTrendTime_RedDot )
            {
                switch ( waveTimeType )
                {
                    case GannDegreesType.Gann_45Dg_Multiple:
                    case GannDegreesType.Gann_45Dg_Calendar:
                    case GannDegreesType.Gann_45Dg_Nearby:
                    case GannDegreesType.Gann_45Dg_Nearby_Calendar:
                    {
                        sprite.PointMarkerTemplate = GetControlTemplateFromString( Get90Deg( width - 2, colorInt ) );
                    }
                    
                    break;

                    case GannDegreesType.Gann_30Dg_Multiple:
                    case GannDegreesType.Gann_30Dg_Calendar:
                    case GannDegreesType.Gann_30Dg_Nearby:
                    case GannDegreesType.Gann_30Dg_Nearby_Calendar:
                    {
                        sprite.PointMarkerTemplate = GetControlTemplateFromString( Get60Deg( width - 2, colorInt ) );
                    }                    
                    break;

                    case GannDegreesType.Gann_SpecialSeq:
                    case GannDegreesType.Gann_SpecialSeq_Cal:
                    {
                        sprite.PointMarkerTemplate = GetControlTemplateFromString( GetSpecialDeg( width-2, colorInt ) );
                    }                    
                    break;
                }
            }
            else if ( rot == TaGannPriceTimeType.CurrentPrice_TimeElapsed_Local || 
                      rot == TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_Local || 
                      rot == TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_Local || 
                      rot == TaGannPriceTimeType.PriorTrendEndPrice_CurrentTrendTime_Local || 
                      rot == TaGannPriceTimeType.CurrentTrendRange_CurrentTrendTime_Local )
            {
                switch ( waveTimeType )
                {
                    case GannDegreesType.Gann_45Dg_Multiple:
                    case GannDegreesType.Gann_45Dg_Calendar:
                    case GannDegreesType.Gann_45Dg_Nearby:
                    case GannDegreesType.Gann_45Dg_Nearby_Calendar:
                    {
                        sprite.PointMarkerTemplate = GetControlTemplateFromString( Get90Deg( width - 4, colorInt ) );
                    }                    
                    break;

                    case GannDegreesType.Gann_30Dg_Multiple:
                    case GannDegreesType.Gann_30Dg_Calendar:
                    case GannDegreesType.Gann_30Dg_Nearby:
                    case GannDegreesType.Gann_30Dg_Nearby_Calendar:
                    {
                        sprite.PointMarkerTemplate = GetControlTemplateFromString( Get60Deg( width - 4, colorInt ) );
                    }                    
                    break;

                    case GannDegreesType.Gann_SpecialSeq:
                    case GannDegreesType.Gann_SpecialSeq_Cal:
                    {
                        sprite.PointMarkerTemplate = GetControlTemplateFromString( GetSpecialDeg( width - 4, colorInt ) );
                    }                    
                    break;
                }
            }

            return sprite;
        }

        public static FxSpritePointMarker CreateSpritePointMarker( TASignalSymbol signal, int pointSize )
        {
            var sprite = new FxSpritePointMarker( );

            switch ( signal )
            {
                case TASignalSymbol.GannImportance:
                {
                    int colorInt = 0x00FF00;
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt );
                }
                break;

                case TASignalSymbol.WaveImportance:
                {
                    int colorInt = 0x00FF00;
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt );
                }
                break;

                case TASignalSymbol.PositiveDivergence:
                {
                    int positiveDiv = 0x4CAF50;
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, positiveDiv );
                }
                break;

                case TASignalSymbol.ImportantHiddenPosDiv:
                {
                    int importantHiddenPosDiv = 0x00FF00;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, importantHiddenPosDiv );
                }
                break;

                case TASignalSymbol.HiddenPosDiv:
                {
                    int hiddenPosDiv = 0x1E88E5;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, hiddenPosDiv );
                }
                break;

                case TASignalSymbol.ImportantPositive:
                {
                    int imptPosDivergence = 0x44C4A1;
                    int imptPosDivergence2 = 0x3EA69B;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, imptPosDivergence, imptPosDivergence2 );
                }
                break;

                case TASignalSymbol.NegativeDivergence:
                {
                    int negativeDiv = 0xF44336;
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, negativeDiv );
                }
                break;

                case TASignalSymbol.ImportantNegative:
                {
                    int imptNegDivergence = 0xBF360C;
                    int imptNegDivergence2 = 263238;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, imptNegDivergence, imptNegDivergence2 );
                }
                break;

                case TASignalSymbol.ImportantHiddenNegDiv:
                {
                    int imptNegDivergence = 0xFF0000;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, imptNegDivergence );
                }
                break;

                case TASignalSymbol.HiddenNegDiv:
                {
                    int hiddenNegDiv = 0xBA00FF;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, hiddenNegDiv );
                }
                break;

                case TASignalSymbol.WaveRotation_HHLL:
                {
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, 0 );
                }
                
                break;

                case TASignalSymbol.WaveRotation_HLLH:
                {
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, 0 );
                }
                break;

                case TASignalSymbol.WaveRotation_Correction:
                {
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, 0 );
                }

                break;

                case TASignalSymbol.WaveRotation_BarCount:
                {
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, 0 );
                }
                break;
            }

            return sprite;
        }

        public static FxSpritePointMarker CreateSpritePointMarker( TASignalSymbol signal, int pointSize, int colorInt )
        {
            var sprite = new FxSpritePointMarker( );            

            switch ( signal )
            {
                case TASignalSymbol.GannImportance:
                {                    
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt );
                }
                break;

                case TASignalSymbol.WaveImportance:
                {                    
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt );
                }
                break;

                case TASignalSymbol.PositiveDivergence:
                {
                    //int positiveDiv = 0x4CAF50;
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt );
                }
                break;

                case TASignalSymbol.ImportantHiddenPosDiv:
                {
                    //int importantHiddenPosDiv = 0x00FF00;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt );
                }
                break;

                case TASignalSymbol.HiddenPosDiv:
                {
                    //int hiddenPosDiv = 0x1E88E5;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt );
                }
                break;

                case TASignalSymbol.ImportantPositive:
                {
                    //int imptPosDivergence = 0x44C4A1;
                    int imptPosDivergence2 = 0x3EA69B;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt, imptPosDivergence2 );
                }
                break;

                case TASignalSymbol.NegativeDivergence:
                {
                    //int negativeDiv = 0xF44336;
                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt );
                }
                break;

                case TASignalSymbol.ImportantNegative:
                {
                    //int imptNegDivergence = 0xBF360C;
                    int imptNegDivergence2 = 263238;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt, imptNegDivergence2 );
                }
                break;

                case TASignalSymbol.ImportantHiddenNegDiv:
                {
                    //int imptNegDivergence = 0xFF0000;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt );
                }
                break;

                case TASignalSymbol.HiddenNegDiv:
                {
                    //int hiddenNegDiv = 0xBA00FF;

                    sprite.PointMarkerTemplate = GetSpriteControlTemplate( signal, pointSize, colorInt );
                }
                break;
            }

            return sprite;
        }

        public static ControlTemplate GetSpriteControlTemplate( TASignalSymbol symbol, int width, int colorInt, int colorInt2 )
        {
            string xaml = "";
            switch ( symbol )
            {
                case TASignalSymbol.WaveImportance:
                xaml = GetWaveImportance( width, colorInt );
                break;

                case TASignalSymbol.PositiveDivergence:
                xaml = GetPositiveDivergence( width, colorInt );
                break;

                case TASignalSymbol.ImportantPositive:
                xaml = GetImportantPositiveDivergence( width, colorInt, colorInt2 );
                break;

                case TASignalSymbol.ImportantHiddenPosDiv:
                xaml = GetImptHiddenPositiveDivergence( width, colorInt );
                break;

                case TASignalSymbol.HiddenPosDiv:
                xaml = GetHiddenPositiveDivergence( width, colorInt );
                break;

                case TASignalSymbol.NegativeDivergence:
                xaml = GetNegativeDivergence( width, colorInt );
                break;

                
                case TASignalSymbol.ImportantNegative:
                xaml = GetImportantNegativeDivergence( width, colorInt, colorInt2 );
                break;

                case TASignalSymbol.ImportantHiddenNegDiv:
                xaml = GetImptHiddenNegativeDivergence( width, colorInt );
                break;

                case TASignalSymbol.HiddenNegDiv:
                xaml = GetHiddenNegativeDivergence( width, colorInt );
                break;
            }

            return GetControlTemplateFromString( xaml );
        }

        public static ControlTemplate GetControlTemplateFromString( string xaml )
        {
            var sr = new MemoryStream( Encoding.ASCII.GetBytes( xaml ) );
            var pc = new ParserContext( );

            pc.XmlnsDictionary.Add( "", "http://schemas.microsoft.com/winfx/2006/xaml/presentation" );
            pc.XmlnsDictionary.Add( "x", "http://schemas.microsoft.com/winfx/2006/xaml" );

            var templ = XamlReader.Load( sr, pc );

            return ( ControlTemplate )templ;
        }

        public static string RgbToString( int colorInt )
        {
            var r = ( ( colorInt >> 16 ) & 0xff );
            var g = ( ( colorInt >> 8 ) & 0xff );
            var b = ( colorInt & 0xff );

            string colorStr = string.Format( "#{0:X2}{1:X2}{2:X2}", r, g, b );

            return colorStr;
        }

        public static string GetWaveImportance( int width, int colorInt )
        {
            string xaml = "<ControlTemplate x:Key='WaveImportance' >";
            xaml += "<Grid Canvas.Left ='0' Canvas.Top ='0' Width ='" + width + "' Height ='" + width + "' >";
            xaml += "     <Grid.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Grid.RenderTransform >";
            xaml += "     <Grid.Resources />";
            xaml += "     <Polygon Stretch='Uniform' Points='26.9,1.3 35.3,18.2 53.9,20.9 40.4,34 43.6,52.5 26.9,43.8 10.3,52.5 13.5,34 0,20.9   18.6,18.2 ' Name = 'Red' FillRule = 'NonZero' Fill = '" + RgbToString( colorInt ) + "' />";
            xaml += "</Grid>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetGannImportance( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='PositiveDivergence' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='PositiveDivergenceCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path x:Name='path2' Fill='" + colorStr + "' Data='M255 0C114.75 0 0 114.75 0 255s114.75 255 255 255s255-114.75 255-255S395.25 0 255 0z'/>";            
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;

            //string xaml = "<ControlTemplate x:Key='GannImportance' >";
            //xaml += "<Grid Canvas.Left ='0' Canvas.Top ='0' Width ='" + width + "' Height ='" + width + "' >";
            //xaml += "     <Grid.RenderTransform >";
            //xaml += "         <TranslateTransform X = '0' Y = '0' />";
            //xaml += "     </Grid.RenderTransform >";
            //xaml += "     <Grid.Resources />";
            //xaml += "     <Path Data='M255 0C114.75 0 0 114.75 0 255s114.75 255 255 255s255-114.75 255-255S395.25 0 255 0z' Fill = '" + RgbToString( colorInt ) + "' />";
            //xaml += "</Grid>";
            //xaml += "</ControlTemplate>";

            //return xaml;
        }

        public static string GetPositiveDivergence( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='PositiveDivergence' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='PositiveDivergenceCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path x:Name='path2' Fill='" + colorStr + "' Data='M44 24c0 11.045-8.955 20-20 20S4 35.045 4 24S12.955 4 24 4S44 12.955 44 24z'/>";
            xaml += "                   <Path x:Name='path4' Fill='#FFFFFFFF' Data='M21 14h6v20h-6V14z' Margin='0,0,17.142,9.714'/>";
            xaml += "                   <Path x:Name='path6' Fill='#FFFFFFFF' Data='M14 21h20v6H14V21z' Margin='0,0,9.714,17.142'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetNegativeDivergence( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='NegativeDivergence' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='NegativeDivergenceCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path x:Name='path2' Fill='" + colorStr + "' Data='M44 24c0 11.045-8.955 20-20 20S4 35.045 4 24S12.955 4 24 4S44 12.955 44 24z'/>";
            xaml += "                   <Path x:Name='path4' Fill='#FFFFFFFF' Data='M29.656 15.516l2.828 2.828l-14.14 14.14l-2.828-2.828L29.656 15.516z'/>";
            xaml += "                   <Path x:Name='path6' Fill='#FFFFFFFF' Data='M32.484 29.656l-2.828 2.828l-14.14-14.14l2.828-2.828L32.484 29.656z'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetHighHighF( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='HighLowF' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='HighLowFCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Name='path4' Fill='#000000' Data='M47.5 0H8.5C3.8 0 0 3.8 0 8.5v39.1C0 52.2 3.8 56 8.5 56h39.1c4.7 0 8.5-3.8 8.5-8.5V8.5C56 3.8 52.2 0 47.5 0z M54 47.5  c0 3.6-2.9 6.5-6.5 6.5H8.5C4.9 54 2 51.1 2 47.5V30.4l6.2-6.2C9 24.7 10 25 11 25s2-0.3 2.8-0.8l10.1 10.1C23.3 35 23 36 23 37  c0 2.8 2.2 5 5 5s5-2.2 5-5c0-1-0.3-2-0.8-2.8L46 20.4V27c0 0.6 0.4 1 1 1s1-0.4 1-1V17H38c-0.6 0-1 0.4-1 1s0.4 1 1 1h6.6  L30.8 32.8C30 32.3 29 32 28 32s-2 0.3-2.8 0.8L15.2 22.8C15.7 22 16 21 16 20c0-2.8-2.2-5-5-5s-5 2.2-5 5c0 1 0.3 2 0.8 2.8L2 27.6  V8.5C2 4.9 4.9 2 8.5 2h39.1C51.1 2 54 4.9 54 8.5V47.5z M11 23c-1.7 0-3-1.3-3-3s1.3-3 3-3s3 1.3 3 3S12.7 23 11 23z M31 37  c0 1.7-1.3 3-3 3s-3-1.3-3-3s1.3-3 3-3S31 35.3 31 37z'/>";
            xaml += "                   <Canvas Name='g12'>";
            xaml += "                       <Ellipse Canvas.Left='6' Canvas.Top='15' Width='11' Height='11' Name='circle8' Fill='#FF6D3B'/>";
            xaml += "                       <Ellipse Canvas.Left='8' Canvas.Top='17' Width='7' Height='7' Name='circle10' Fill='#FFFFFF'/>";
            xaml += "                   </Canvas>";
            xaml += "                   <Canvas Name='g18'>";
            xaml += "                       <Ellipse Canvas.Left='45.8' Canvas.Top='9.5' Width='8.6' Height='8.6' Name='circle14' Fill='#FF6D3B'/>";
            xaml += "                       <Ellipse Canvas.Left='47.9' Canvas.Top='11.6' Width='4.4' Height='4.4' Name='circle16' Fill='#FFFFFF'/>";
            xaml += "                   </Canvas>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        

        public static string GetHighLowF( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='HighHighF' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='HighHighFCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Name='path4' Fill='#000000' Data='M47.5 0H8.5C3.8 0 0 3.8 0 8.5v39.1C0 52.2 3.8 56 8.5 56h39.1c4.7 0 8.5-3.8 8.5-8.5V8.5C56 3.8 52.2 0 47.5 0z M54 47.5  c0 3.6-2.9 6.5-6.5 6.5H8.5C4.9 54 2 51.1 2 47.5V30.4l6.2-6.2C9 24.7 10 25 11 25s2-0.3 2.8-0.8l10.1 10.1C23.3 35 23 36 23 37  c0 2.8 2.2 5 5 5s5-2.2 5-5c0-1-0.3-2-0.8-2.8L46 20.4V27c0 0.6 0.4 1 1 1s1-0.4 1-1V17H38c-0.6 0-1 0.4-1 1s0.4 1 1 1h6.6  L30.8 32.8C30 32.3 29 32 28 32s-2 0.3-2.8 0.8L15.2 22.8C15.7 22 16 21 16 20c0-2.8-2.2-5-5-5s-5 2.2-5 5c0 1 0.3 2 0.8 2.8L2 27.6  V8.5C2 4.9 4.9 2 8.5 2h39.1C51.1 2 54 4.9 54 8.5V47.5z M11 23c-1.7 0-3-1.3-3-3s1.3-3 3-3s3 1.3 3 3S12.7 23 11 23z M31 37  c0 1.7-1.3 3-3 3s-3-1.3-3-3s1.3-3 3-3S31 35.3 31 37z'/>";
            xaml += "                   <Canvas Name='g12'>";
            xaml += "                       <Ellipse Canvas.Left='5.8' Canvas.Top='14.7' Width='10.6' Height='10.6' Name='circle8' Fill='#FF6D3B'/>";
            xaml += "                       <Ellipse  Canvas.Left='8.0' Canvas.Top='17.3' Width='6.4' Height='6.4' Name='circle10' Fill='#FFFFFF'/>";
            xaml += "                   </Canvas>";
            xaml += "                   <Canvas Name='g18'>";
            xaml += "                       <Ellipse  Canvas.Left='22.7' Canvas.Top='31.7' Width='10.6' Height='10.6' Name='circle14' Fill='#FF6D3B'/>";
            xaml += "                       <Ellipse  Canvas.Left='25.3' Canvas.Top='33.8' Width='6.4' Height='6.4' Name='circle16' Fill='#FFFFFF'/>";
            xaml += "                   </Canvas>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetBarSum( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='BarSum' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='BarSumCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";        
            xaml += "                       <Path Name='path4' Fill='#8A8E93' Data='M493.297 159.693c-12.477-30.878-31.231-59.828-56.199-84.792c-24.964-24.967-53.914-43.722-84.792-56.199    C321.426 6.222 288.617 0 255.824 0c-32.748 0-65.497 6.249-96.315 18.744c-30.814 12.49-59.695 31.242-84.607 56.158    c-24.915 24.911-43.668 53.792-56.158 84.607C6.25 190.324 0.001 223.072 0.001 255.821c0 32.795 6.222 65.604 18.701 96.485    c12.477 30.878 31.231 59.829 56.199 84.793c24.964 24.967 53.914 43.722 84.792 56.199c30.882 12.48 63.69 18.701 96.484 18.703    c32.748 0 65.497-6.249 96.315-18.743c30.814-12.49 59.695-31.242 84.607-56.158c24.915-24.912 43.668-53.793 56.158-84.608    c12.494-30.817 18.743-63.565 18.744-96.315C511.999 223.383 505.778 190.575 493.297 159.693z M461.612 339.661    c-10.821 26.683-27.018 51.648-48.659 73.292c-21.643 21.64-46.608 37.837-73.291 48.659    c-26.679 10.818-55.078 16.241-83.484 16.241c-28.477 0-56.947-5.406-83.688-16.214c-26.744-10.813-51.76-27.008-73.441-48.685    c-21.679-21.682-37.874-46.697-48.685-73.442c-10.808-26.741-16.214-55.212-16.213-83.689    c-0.002-28.406 5.422-56.804 16.239-83.483c10.821-26.683 27.018-51.648 48.659-73.291c21.643-21.64 46.608-37.837 73.292-48.659    c26.679-10.818 55.078-16.241 83.484-16.241c28.477 0 56.947 5.405 83.688 16.214c26.744 10.813 51.76 27.007 73.441 48.685    c21.679 21.682 37.873 46.697 48.685 73.441c10.808 26.741 16.214 55.211 16.214 83.688    C477.853 284.583 472.43 312.981 461.612 339.661z'/>";                       
            xaml += "                       <Path Name='path10' Fill='#448CE2' Data='M356.24 183.605c9.227 0 16.707-7.48 16.707-16.707v-33.412c0-4.399-1.782-8.703-4.893-11.814s-7.414-4.893-11.814-4.893    H155.761c-6.445 0-12.337 3.728-15.097 9.552c-2.76 5.825-1.915 12.745 2.167 17.734L234.415 256l-91.583 111.937    c-4.082 4.987-4.927 11.909-2.167 17.734c2.76 5.825 8.652 9.552 15.097 9.552H356.24c4.399 0 8.702-1.782 11.814-4.893    s4.893-7.414 4.893-11.814v-33.413c0-9.227-7.48-16.707-16.707-16.707s-16.707 7.48-16.707 16.707v16.707H191.016l77.915-95.229    c5.047-6.168 5.047-14.991 0-21.158l-77.915-95.229h148.517v16.706C339.534 176.124 347.014 183.605 356.24 183.605z'/>";            
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string Get90Deg( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='Degree90' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='Degree90Canvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M13.5 0C6 0 0 6 0 13.5s6 13.5 13.5 13.5c7.4 0 13.5-6 13.5-13.5S20.9 0 13.5 0z M13.5 25.7c-6.8 0-12.2-5.5-12.2-12.2    c0-6.8 5.5-12.3 12.2-12.3s12.2 5.5 12.2 12.3S20.2 25.7 13.5 25.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M9.3 4.6l-0.5-1L8.3 3.9l0.5 1C8.9 4.8 9.1 4.7 9.3 4.6z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M3.6 13.7c0-0.2 0-0.4 0-0.6H2.3v1.2h1.4C3.6 14.1 3.6 13.9 3.6 13.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M14.3 3.6V2.2h-1.2v1.4c0.2 0 0.4 0 0.6 0C13.9 3.6 14.1 3.6 14.3 3.6z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M24.1 9.1l-0.3-0.5l-1.1 0.6c0.1 0.2 0.2 0.4 0.3 0.6L24.1 9.1z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M19.1 5.1c0.2 0.1 0.3 0.2 0.5 0.3l0.6-0.9l-0.5-0.3L19.1 5.1z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M4.1 7.7l1 0.7c0.1-0.2 0.2-0.3 0.3-0.5l-1-0.7L4.1 7.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M17.9 22.9l0.7 1.5l0.5-0.3l-0.7-1.5C18.2 22.7 18 22.8 17.9 22.9z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M21.7 19.8l1.3 0.9l0.3-0.5L22 19.4C21.9 19.5 21.8 19.7 21.7 19.8z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M23.8 13.1c0 0.2 0 0.4 0 0.6c0 0.2 0 0.4 0 0.6h1.4v-1.2H23.8z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M13.1 23.7v1.4h1.2v-1.4c-0.2 0-0.4 0-0.6 0C13.5 23.8 13.3 23.7 13.1 23.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M7.3 23.4l0.5 0.3l0.9-1.4c-0.2-0.1-0.4-0.2-0.5-0.3L7.3 23.4z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M3.4 18.9l0.3 0.5L5 18.7c-0.1-0.2-0.2-0.4-0.3-0.5L3.4 18.9z'/>";
            xaml += "                       <Path Fill='#000000' Data='M6.1 16.8l1.2-0.1c0.2 1.4 0.8 2 1.8 2c0.4 0 0.8-0.1 1.2-0.4c0.4-0.3 0.7-0.8 0.9-1.6c0.2-0.8 0.4-1.6 0.4-2.6v-0.3   c-0.3 0.5-0.6 0.9-1.1 1.2s-0.9 0.5-1.4 0.5c-0.9 0-1.6-0.4-2.2-1.1s-0.9-1.8-0.9-3.1c0-1.3 0.3-2.4 1-3.2s1.4-1.2 2.4-1.2   c0.7 0 1.3 0.2 1.8 0.7s1 1.1 1.3 1.9s0.5 2.1 0.5 3.6c0 1.8-0.2 3.1-0.5 4.1s-0.8 1.7-1.4 2.1C10.4 19.8 9.8 20 9.1 20   c-0.8 0-1.5-0.3-2-0.8S6.2 17.9 6.1 16.8z M11.4 11.2c0-0.9-0.2-1.6-0.6-2.2c-0.4-0.5-0.9-0.8-1.4-0.8C8.8 8.2 8.3 8.5 7.9 9   c-0.4 0.6-0.7 1.3-0.7 2.3c0 0.8 0.2 1.5 0.6 2c0.4 0.5 0.9 0.8 1.5 0.8c0.6 0 1.1-0.3 1.5-0.8C11.2 12.8 11.4 12.1 11.4 11.2z'/>";
            xaml += "                       <Path Fill='#000000' Data='M14.1 13.5c0-2.2 0.3-3.8 0.9-4.9s1.4-1.7 2.6-1.7c1 0 1.8 0.5 2.4 1.4c0.7 1.1 1.1 2.8 1.1 5.2c0 2.2-0.3 3.8-0.9 4.9   S18.7 20 17.6 20c-1 0-1.8-0.5-2.5-1.5C14.4 17.6 14.1 15.9 14.1 13.5z M15.5 13.5c0 2.1 0.2 3.5 0.6 4.2c0.4 0.7 0.9 1.1 1.5 1.1   c0.6 0 1.1-0.4 1.5-1.1s0.6-2.1 0.6-4.2c0-2.1-0.2-3.5-0.6-4.2s-0.9-1.1-1.6-1.1c-0.6 0-1.1 0.4-1.5 1.1   C15.7 10 15.5 11.4 15.5 13.5z'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string Get60Deg( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='Degree90' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='Degree90Canvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M13.5 0C6 0 0 6 0 13.5s6 13.5 13.5 13.5c7.4 0 13.5-6 13.5-13.5S20.9 0 13.5 0z M13.5 25.7c-6.8 0-12.2-5.5-12.2-12.2    c0-6.8 5.5-12.3 12.2-12.3s12.2 5.5 12.2 12.3S20.2 25.7 13.5 25.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M9.3 4.6l-0.5-1L8.3 3.9l0.5 1C8.9 4.8 9.1 4.7 9.3 4.6z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M3.6 13.7c0-0.2 0-0.4 0-0.6H2.3v1.2h1.4C3.6 14.1 3.6 13.9 3.6 13.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M14.3 3.6V2.2h-1.2v1.4c0.2 0 0.4 0 0.6 0C13.9 3.6 14.1 3.6 14.3 3.6z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M24.1 9.1l-0.3-0.5l-1.1 0.6c0.1 0.2 0.2 0.4 0.3 0.6L24.1 9.1z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M19.1 5.1c0.2 0.1 0.3 0.2 0.5 0.3l0.6-0.9l-0.5-0.3L19.1 5.1z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M4.1 7.7l1 0.7c0.1-0.2 0.2-0.3 0.3-0.5l-1-0.7L4.1 7.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M17.9 22.9l0.7 1.5l0.5-0.3l-0.7-1.5C18.2 22.7 18 22.8 17.9 22.9z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M21.7 19.8l1.3 0.9l0.3-0.5L22 19.4C21.9 19.5 21.8 19.7 21.7 19.8z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M23.8 13.1c0 0.2 0 0.4 0 0.6c0 0.2 0 0.4 0 0.6h1.4v-1.2H23.8z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M13.1 23.7v1.4h1.2v-1.4c-0.2 0-0.4 0-0.6 0C13.5 23.8 13.3 23.7 13.1 23.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M7.3 23.4l0.5 0.3l0.9-1.4c-0.2-0.1-0.4-0.2-0.5-0.3L7.3 23.4z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M3.4 18.9l0.3 0.5L5 18.7c-0.1-0.2-0.2-0.4-0.3-0.5L3.4 18.9z'/>";
            xaml += "                       <Path Fill='#000000' Data='M12.7 10.1l-1.3 0.1c-0.1-0.7-0.3-1.1-0.5-1.4c-0.3-0.4-0.8-0.6-1.2-0.6c-0.8 0-1.4 0.5-1.8 1.4c-0.4 0.8-0.6 2-0.6 3.6   c0.3-0.6 0.7-1 1.1-1.3s0.9-0.4 1.4-0.4c0.8 0 1.6 0.4 2.2 1.1c0.6 0.8 0.9 1.8 0.9 3c0 0.9-0.2 1.6-0.5 2.3   c-0.3 0.7-0.7 1.2-1.2 1.6s-1 0.5-1.6 0.5c-1.1 0-1.9-0.5-2.6-1.4s-1-2.6-1-4.8c0-2.5 0.4-4.3 1.1-5.3s1.6-1.6 2.7-1.6   c0.8 0 1.5 0.3 2 0.8C12.2 8.3 12.6 9 12.7 10.1z M7.4 15.7c0 0.9 0.2 1.7 0.7 2.2c0.4 0.6 0.9 0.8 1.5 0.8c0.5 0 1-0.3 1.4-0.8   s0.6-1.3 0.6-2.2c0-0.9-0.2-1.6-0.6-2.1s-0.9-0.8-1.5-0.8c-0.6 0-1.1 0.3-1.5 0.8C7.6 14.1 7.4 14.8 7.4 15.7z'/>";
            xaml += "                       <Path Fill='#000000' Data='M14.2 13.5c0-2.2 0.3-3.8 0.9-4.9s1.4-1.7 2.6-1.7c1 0 1.8 0.5 2.4 1.4c0.7 1.1 1.1 2.8 1.1 5.2c0 2.2-0.3 3.8-0.9 4.9   c-0.6 1.1-1.4 1.7-2.6 1.7c-1 0-1.8-0.5-2.5-1.5C14.5 17.6 14.2 15.9 14.2 13.5z M15.5 13.5c0 2.1 0.2 3.5 0.6 4.2   c0.4 0.7 0.9 1.1 1.5 1.1c0.6 0 1.1-0.4 1.5-1.1c0.4-0.7 0.6-2.1 0.6-4.2c0-2.1-0.2-3.5-0.6-4.2s-0.9-1.1-1.6-1.1   c-0.6 0-1.1 0.4-1.5 1.1C15.7 10 15.5 11.4 15.5 13.5z'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetSpecialDeg( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='Degree90' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='Degree90Canvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M13.5 0C6 0 0 6 0 13.5s6 13.5 13.5 13.5c7.4 0 13.5-6 13.5-13.5S20.9 0 13.5 0z M13.5 25.7c-6.8 0-12.2-5.5-12.2-12.2    c0-6.8 5.5-12.3 12.2-12.3s12.2 5.5 12.2 12.3S20.2 25.7 13.5 25.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M9.3 4.6l-0.5-1L8.3 3.9l0.5 1C8.9 4.8 9.1 4.7 9.3 4.6z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M3.6 13.7c0-0.2 0-0.4 0-0.6H2.3v1.2h1.4C3.6 14.1 3.6 13.9 3.6 13.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M14.3 3.6V2.2h-1.2v1.4c0.2 0 0.4 0 0.6 0C13.9 3.6 14.1 3.6 14.3 3.6z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M24.1 9.1l-0.3-0.5l-1.1 0.6c0.1 0.2 0.2 0.4 0.3 0.6L24.1 9.1z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M19.1 5.1c0.2 0.1 0.3 0.2 0.5 0.3l0.6-0.9l-0.5-0.3L19.1 5.1z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M4.1 7.7l1 0.7c0.1-0.2 0.2-0.3 0.3-0.5l-1-0.7L4.1 7.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M17.9 22.9l0.7 1.5l0.5-0.3l-0.7-1.5C18.2 22.7 18 22.8 17.9 22.9z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M21.7 19.8l1.3 0.9l0.3-0.5L22 19.4C21.9 19.5 21.8 19.7 21.7 19.8z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M23.8 13.1c0 0.2 0 0.4 0 0.6c0 0.2 0 0.4 0 0.6h1.4v-1.2H23.8z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M13.1 23.7v1.4h1.2v-1.4c-0.2 0-0.4 0-0.6 0C13.5 23.8 13.3 23.7 13.1 23.7z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M7.3 23.4l0.5 0.3l0.9-1.4c-0.2-0.1-0.4-0.2-0.5-0.3L7.3 23.4z'/>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M3.4 18.9l0.3 0.5L5 18.7c-0.1-0.2-0.2-0.4-0.3-0.5L3.4 18.9z'/>";
            xaml += "                       <Path Fill='#000000' Data='M9.3 15.8l1.3-0.1c0.1 0.7 0.2 1.3 0.5 1.7c0.2 0.4 0.6 0.7 1.1 1s1 0.4 1.6 0.4c0.8 0 1.5-0.2 1.9-0.6   c0.5-0.4 0.7-0.9 0.7-1.6c0-0.4-0.1-0.7-0.3-1c-0.2-0.3-0.4-0.5-0.8-0.7s-1.1-0.4-2.2-0.8c-1-0.3-1.7-0.6-2.1-0.9s-0.8-0.7-1-1.1   s-0.4-1-0.4-1.6c0-1 0.3-1.9 1-2.6s1.6-1 2.7-1c0.8 0 1.5 0.2 2.1 0.5c0.6 0.3 1 0.8 1.4 1.3c0.3 0.6 0.5 1.3 0.5 2L16 10.8   c-0.1-0.8-0.3-1.5-0.8-1.9c-0.4-0.4-1-0.6-1.8-0.6c-0.8 0-1.4 0.2-1.8 0.5S11 9.6 11 10.2c0 0.5 0.2 0.9 0.5 1.2s1 0.6 2.1 0.9   c1 0.3 1.8 0.5 2.2 0.8c0.6 0.3 1.1 0.8 1.5 1.3s0.5 1.2 0.5 1.9c0 0.7-0.2 1.4-0.5 2c-0.3 0.6-0.8 1.1-1.4 1.4   c-0.6 0.3-1.3 0.5-2.1 0.5c-1.3 0-2.3-0.4-3.1-1.2C9.7 18.2 9.3 17.1 9.3 15.8z'/>";            
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetPivotPoint( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='PivotPoint' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='PivotPointCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                       <Path Fill='" + colorStr + "' Data='M108 296.4H55.4c-14.3 0-26-11.7-26-25.9v-52.6c0-14.3 11.7-26 25.9-26h52.6    c14.3 0 26 11.7 26 25.9v52.6C134 284.7 122.3 296.4 108 296.4z'/>";
            xaml += "                       <Path Fill='#000000' Data='M81.7 305.5L81.7 305.5c-33.8 0-61.3-27.5-61.4-61.3c0-16.3 6.4-31.7 18-43.3s27-18 43.3-18c33.8 0 61.3 27.5 61.4 61.3     c0 16.3-6.4 31.7-18 43.3S98 305.5 81.7 305.5z M81.7 201c-11.5 0-22.3 4.5-30.5 12.7s-12.7 19-12.7 30.5     c0 23.8 19.4 43.2 43.2 43.2v9.1v-9.1c11.5 0 22.3-4.5 30.5-12.7s12.7-19 12.7-30.5C124.9 220.4 105.5 201 81.7 201z'/>";
            xaml += "                       <Path Fill='#000000' Data='M81.5 122.6C81.5 122.6 81.6 122.6 81.5 122.6c16.4 0 31.8-6.4 43.4-18s18-27 18-43.3C142.9 27.5 115.4 0 81.5 0    C65.2 0 49.8 6.4 38.2 18s-18 27-18 43.3C20.2 95.1 47.7 122.6 81.5 122.6z M51 30.8c8.2-8.2 19-12.7 30.5-12.7    c23.8 0 43.2 19.4 43.2 43.2c0 11.5-4.5 22.3-12.7 30.5s-19 12.7-30.5 12.7v9.1v-9.1c-23.8 0-43.2-19.4-43.2-43.2    C38.3 49.8 42.8 39 51 30.8z'/>";
            xaml += "                       <Path Fill='#000000' Data='M81.8 365.8c-16.3 0-31.7 6.4-43.3 18s-18 27-18 43.3c0 33.8 27.5 61.3 61.3 61.3l0 0c33.8 0 61.3-27.6 61.3-61.4    C143.1 393.3 115.6 365.8 81.8 365.8z M81.9 470.3v9.1V470.3c-23.8 0-43.2-19.4-43.2-43.2c0-11.5 4.5-22.3 12.7-30.5    s19-12.7 30.5-12.7c23.8 0 43.2 19.4 43.2 43.2C125 450.9 105.7 470.3 81.9 470.3z'/>";
            xaml += "                       <Path Fill='#000000' Data='M468.2 57.7c0-5-4.1-9.1-9.1-9.1l0 0l-248.8 0.2c-5 0-9.1 4.1-9.1 9.1s4.1 9.1 9.1 9.1l0 0l248.8-0.2    C464.1 66.7 468.2 62.7 468.2 57.7z'/>";
            xaml += "                       <Path Fill='#000000' Data='M198 244.1c0 2.4 1 4.7 2.6 6.4c1.7 1.7 4 2.7 6.4 2.7s4.7-1 6.4-2.7s2.7-4 2.7-6.4s-1-4.7-2.7-6.4s-4-2.7-6.4-2.7    s-4.7 1-6.4 2.7C199 239.4 198 241.7 198 244.1z'/>";
            xaml += "                       <Path Fill='#000000' Data='M247.2 244.1c0-5-4-9.1-9.1-9.1c-5 0-9.1 4.1-9 9.1c0 5 4 9.1 9.1 9.1C243.2 253.1 247.3 249.1 247.2 244.1z'/>";
            xaml += "                       <Path Fill='#000000' Data='M278.3 244c0-5-4-9-9.1-9c-5 0-9.1 4.1-9.1 9.1s4.1 9.1 9.1 9.1C274.3 253.1 278.4 249.1 278.3 244z'/>";
            xaml += "                       <Path Fill='#000000' Data='M433.9 243.9c0-5-4.1-9.1-9.1-9.1s-9 4.1-9 9.1s4.1 9.1 9.1 9.1C429.8 253 433.9 248.9 433.9 243.9z'/>";
            xaml += "                       <Path Fill='#000000' Data='M371.7 244c0-5-4.1-9.1-9.1-9.1s-9 4.1-9 9.1s4 9 9.1 9C367.6 253 371.7 249 371.7 244z'/>";
            xaml += "                       <Path Fill='#000000' Data='M340.6 244c0-5-4.1-9.1-9.1-9.1s-9 4.1-9 9.1s4 9 9.1 9C336.5 253.1 340.6 249 340.6 244z'/>";
            xaml += "                       <Path Fill='#000000' Data='M402.8 244c0-5-4.1-9.1-9.1-9.1s-9 4.1-9 9.1s4.1 9.1 9.1 9.1C398.7 253 402.8 249 402.8 244z'/>";
            xaml += "                       <Path Fill='#000000' Data='M309.5 244c0-5-4.1-9.1-9.1-9c-5 0-9.1 4-9 9.1c0 5 4 9.1 9.1 9C305.4 253.1 309.5 249 309.5 244z'/>";
            xaml += "                       <Path Fill='#000000' Data='M462.3 250.3c1.7-1.7 2.7-4 2.7-6.4s-1-4.7-2.7-6.4s-4-2.7-6.4-2.7s-4.7 1-6.4 2.7c-1.7 1.7-2.7 4-2.7 6.4s1 4.7 2.7 6.4    s4 2.7 6.4 2.7S460.6 252 462.3 250.3z'/>";
            xaml += "                       <Path Fill='#000000' Data='M198.2 427c0 5 4.1 9.1 9.1 9.1l0 0l248.8-0.2c5 0 9.1-4.1 9.1-9.1s-4.1-9.1-9.1-9.1l0 0L207.2 418    C202.2 418 198.2 422 198.2 427z'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }


        public static string GetImportantPositiveDivergence( int width, int colorInt, int colorInt2 )
        {
            string colorStr  = RgbToString( colorInt );
            string colorStr2 = RgbToString( colorInt2 );

            string xaml = "<ControlTemplate x:Key='ImportantPositiveDivergence' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='ImportantPositiveDivergenceCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Name='path2' Fill='" + colorStr + "' Data='M0 240.941c0 133.12 107.821 240.941 240.941 240.941V0C107.821 0 0 107.821 0 240.941z'/>";
            xaml += "                   <Path Name='path4' Fill='" + colorStr2 + "' Data='M240.941 0v481.882c133.12 0 240.941-107.821 240.941-240.941S374.061 0 240.941 0z'/>";
            xaml += "                   <Polygon Points='386.108,204.198 286.72,204.198 286.72,104.207 195.162,104.207 195.162,204.198   95.774,204.198 95.774,295.755 195.162,295.755 195.162,395.144 286.72,395.144 286.72,295.755 386.108,295.755 ' Name='polygon6' FillRule='NonZero' Fill='#FF31978C'/>";
            xaml += "                   <Polygon Points='386.108,186.127 286.72,186.127 286.72,86.739 195.162,86.739 195.162,186.127   95.774,186.127 95.774,277.685 195.162,277.685 195.162,377.675 286.72,377.675 286.72,277.685 386.108,277.685 ' Name='polygon8' FillRule='NonZero' Fill='#FFFFFFFF'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetImportantNegativeDivergence( int width, int colorInt, int colorInt2 )
        {
            string colorStr  = RgbToString( colorInt );
            string colorStr2 = RgbToString( colorInt2 );

            string xaml = "<ControlTemplate x:Key='ImportantNegativeDivergence' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='ImportantNegativeDivergenceCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Name='path2' Fill='" + colorStr + "' Data='M502.92 188.251C473.217 79.747 373.929 0 256 0C114.615 0 0 114.615 0 256  c0 117.929 79.747 217.217 188.251 246.92L502.92 188.251z'/>";
            xaml += "                   <Path Name='path4' Fill='" + colorStr2 + "' Data='M502.92 188.252l-89.587-89.587l-7.797 7.797C367.268 68.193 314.398 44.522 256 44.522  C139.204 44.522 44.522 139.203 44.522 256c0 58.398 23.67 111.268 61.941 149.539l-7.797 7.797l89.586 89.586  C209.832 508.828 232.545 512 256 512c141.384 0 256-114.616 256-256C512 232.545 508.828 209.832 502.92 188.252z'/>";
            xaml += "                   <Path Name='path6' Fill='#FFB2EBF2' Data='M256 33.391L200.348 256L256 478.609c122.943 0 222.609-99.665 222.609-222.609  S378.943 33.391 256 33.391z'/>";
            xaml += "                   <Path Name='path8' Fill='#FFFFFFFF' Data='M33.391 256c0 122.943 99.665 222.609 222.609 222.609V33.391  C133.057 33.391 33.391 133.057 33.391 256z'/>";
            xaml += "                   <Path Name='path10' Fill='#FFFF5722' Data='M256 55.652L205.913 256L256 456.348c110.649 0 200.348-89.699 200.348-200.348  S366.649 55.652 256 55.652z'/>";
            xaml += "                   <Path Name='path12' Fill='#FFFF7043' Data='M55.652 256c0 110.649 89.699 200.348 200.348 200.348V55.652  C145.351 55.652 55.652 145.351 55.652 256z'/>";
            xaml += "                   <Polygon Points='389.797,169.426 342.575,122.203 256,208.777 233.739,256 256,303.223 342.575,389.797   389.797,342.574 303.222,256 ' Name='polygon14' FillRule='NonZero' Fill='#FFE0F7FA'/>";
            xaml += "                   <Polygon Points='169.425,122.203 122.203,169.426 208.778,256 122.203,342.574 169.425,389.797 256,303.223   256,208.777 ' Name='polygon16' FillRule='NonZero' Fill='#FFFFFFFF'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetExitOverBought( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );            

            string xaml = "<ControlTemplate x:Key='ExitOverBought' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='ExitOverBoughtCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Fill='" + colorStr + "' Data='M253.254 420.02L58.883 225.617h127.578c2.569-19.24 6.3-74.921-28.166-117.694      C127.838 70.109 75.132 50.942 1.666 50.942L0 33.547c1.252-0.228 31.214-5.926 72.962-5.926      c88.106 0 236.631 25.955 252.083 197.996h122.595L253.254 420.02z'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetExitOverSold( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='ExitOverSold' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='ExitOverSoldCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Fill='" + colorStr + "' Data='M55.205 317.732L55.205 317.732c-31.591 0-54.244-4.305-55.205-4.497l1.261-13.139c55.581 0 95.449-14.52 118.49-43.126      c26.073-32.354 23.25-74.46 21.293-89.016H44.532L191.583 20.897l147.046 147.058h-92.738      C234.211 298.073 121.853 317.732 55.205 317.732z'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetImptHiddenNegativeDivergence( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='ImptHiddenNegativeDivergence' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='ImptHiddenNegativeDivergenceCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Name='path2' Fill='" + colorStr + "' Data='M353.4 353.4c-1.4 1.4-3.4 2.1-5.4 1.8l-198.3-25.9c-1.5-0.2-2.8-0.9-3.7-1.8c-1-1-1.6-2.2-1.8-3.7  c-0.4-2.9 1.2-5.7 3.8-6.8l79.6-34.5L95.4 150.3c-2.5-2.5-2.5-6.6 0-9.2l45.7-45.7c2.5-2.5 6.6-2.5 9.2 0l132.1 132.1l34.5-79.6  c1.2-2.7 4-4.2 6.9-3.8c2.9 0.4 5.1 2.7 5.5 5.6l25.9 198.3C355.5 349.9 354.8 351.9 353.4 353.4z M69 402.6C-23 310.7-23 161 69 69  c92-92 241.7-92 333.6 0s92 241.7 0 333.6C310.6 494.6 161 494.6 69 402.6z M384.3 384.3c81.9-81.9 81.9-215.2 0-297.1  c-81.9-81.9-215.2-81.9-297 0c-81.9 81.9-81.9 215.1 0 297.1C169.2 466.2 302.4 466.2 384.3 384.3z'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetCorrection( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='Correction' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='CorrectionCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Name='path2' Fill='" + colorStr + "' Data='M489.2 65.3c0-35.7-28.5-65.2-64.2-65.2s-64.2 29.6-64.2 65.2c0 28.1 17.7 52.3 42.6 61.4L375.2 359    c-6.6 0.5-12.9 2-18.7 4.4l-97.1-99.6c1.6-5.8 2.5-11.8 2.5-18.1c0-35.7-28.5-65.2-64.2-65.2s-64.2 29.6-64.2 65.2    c0 15.6 5.4 29.9 14.5 41.2l-62.3 75.7c-6.7-2.4-14-3.8-21.5-3.8C28.5 358.8 0 388.4 0 424c0 35.7 28.5 65.2 64.2 65.2    c36.7 0 64.2-29.6 64.2-65.2c0-13.3-4-25.8-10.9-36.2l64.8-78.8c4.9 1.2 10 1.9 15.3 1.9c14.5 0 27.8-4.9 38.5-13.1l89.4 91.8    c-6.2 10-9.7 21.8-9.7 34.4c0 35.7 28.5 65.2 64.2 65.2s64.2-29.6 64.2-65.2c0-23.1-12-43.6-30.1-55.2l29.5-241    C470.1 119.6 489.2 94.4 489.2 65.3z M425 40.8c12.2 0 23.4 11.2 23.4 24.5S437.2 89.8 425 89.8s-23.4-11.2-23.4-24.5    S411.7 40.8 425 40.8z M64.2 448.5c-12.2 0-23.4-11.2-23.4-24.5c0-13.2 10.2-24.5 23.4-24.5s23.4 11.2 23.4 24.5    C87.7 437.3 76.4 448.5 64.2 448.5z M197.7 270.1c-12.2 0-23.4-11.2-23.4-24.5c0-13.2 10.2-24.5 23.4-24.5s23.4 11.2 23.4 24.5    C221.2 259 209.9 270.1 197.7 270.1z M380.1 448.5c-12.2 0-23.4-11.2-23.4-24.5c0-13.2 10.2-24.5 23.4-24.5    c13.2 0 23.4 11.2 23.4 24.5C403.6 437.3 392.4 448.5 380.1 448.5z'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        

        public static string GetImptHiddenPositiveDivergence( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='ImptHiddenPositiveDivergence' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='ImptHiddenPositiveDivergenceCanvas' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Name='path2' Fill='" + colorStr + "' Data='M353.4 118.3c1.4 1.4 2.1 3.4 1.8 5.4L329.3 322c-0.2 1.5-0.9 2.8-1.8 3.7c-1 1-2.2 1.6-3.7 1.8c-2.9 0.4-5.7-1.2-6.8-3.8  l-34.5-79.6L150.3 376.2c-2.5 2.5-6.6 2.5-9.2 0l-45.7-45.7c-2.5-2.5-2.5-6.6 0-9.2l132.1-132.1l-79.6-34.5c-2.7-1.2-4.2-4-3.8-6.9  c0.4-2.9 2.7-5.1 5.6-5.5l198.3-25.9C349.9 116.2 351.9 116.9 353.4 118.3z M402.6 402.6c-92 92-241.7 92-333.6 0  C-23 310.7-23 161 69 69s241.7-92 333.6 0C494.6 161 494.6 310.6 402.6 402.6z M384.3 87.3c-81.9-81.9-215.2-81.9-297.1 0  c-81.9 81.9-81.9 215.2 0 297c81.9 81.9 215.1 81.9 297.1 0C466.2 302.4 466.2 169.2 384.3 87.3z'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetHiddenNegativeDivergence( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='HiddenNegativeDivergence' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='HiddenNegativeDivergence' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Name='path2' Fill='" + colorStr + "' Data='M353.4 353.4c-1.4 1.4-3.4 2.1-5.4 1.8l-198.3-25.9c-1.5-0.2-2.8-0.9-3.7-1.8c-1-1-1.6-2.2-1.8-3.7  c-0.4-2.9 1.2-5.7 3.8-6.8l79.6-34.5L95.4 150.3c-2.5-2.5-2.5-6.6 0-9.2l45.7-45.7c2.5-2.5 6.6-2.5 9.2 0l132.1 132.1l34.5-79.6  c1.2-2.7 4-4.2 6.9-3.8c2.9 0.4 5.1 2.7 5.5 5.6l25.9 198.3C355.5 349.9 354.8 351.9 353.4 353.4z M69 402.6C-23 310.7-23 161 69 69  c92-92 241.7-92 333.6 0s92 241.7 0 333.6C310.6 494.6 161 494.6 69 402.6z M384.3 384.3c81.9-81.9 81.9-215.2 0-297.1  c-81.9-81.9-215.2-81.9-297 0c-81.9 81.9-81.9 215.1 0 297.1C169.2 466.2 302.4 466.2 384.3 384.3z'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetHiddenPositiveDivergence( int width, int colorInt )
        {
            string colorStr = RgbToString( colorInt );

            string xaml = "<ControlTemplate x:Key='HiddenNegativeDivergence' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='HiddenNegativeDivergenceCanvase' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Name='path2' Fill='" + colorStr + "' Data='M353.4 118.3c1.4 1.4 2.1 3.4 1.8 5.4L329.3 322c-0.2 1.5-0.9 2.8-1.8 3.7c-1 1-2.2 1.6-3.7 1.8c-2.9 0.4-5.7-1.2-6.8-3.8  l-34.5-79.6L150.3 376.2c-2.5 2.5-6.6 2.5-9.2 0l-45.7-45.7c-2.5-2.5-2.5-6.6 0-9.2l132.1-132.1l-79.6-34.5c-2.7-1.2-4.2-4-3.8-6.9  c0.4-2.9 2.7-5.1 5.6-5.5l198.3-25.9C349.9 116.2 351.9 116.9 353.4 118.3z M402.6 402.6c-92 92-241.7 92-333.6 0  C-23 310.7-23 161 69 69s241.7-92 333.6 0C494.6 161 494.6 310.6 402.6 402.6z M384.3 87.3c-81.9-81.9-215.2-81.9-297.1 0  c-81.9 81.9-81.9 215.2 0 297c81.9 81.9 215.1 81.9 297.1 0C466.2 302.4 466.2 169.2 384.3 87.3z'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetBuy( int width )
        {            
            string xaml = "<ControlTemplate x:Key='BuySignal' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='BuyCanvase' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Data='m 4 14 4 0 0 -8 3 0 -5 -5 -5 5 3 0 z' Fill='#571CB61C' Stroke='#FF00B400' StrokeThickness='1' />";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }

        public static string GetSell( int width )
        {
            

            string xaml = "<ControlTemplate x:Key='BuySignal' >";
            xaml += "<Canvas Name='Layer_1' Width='" + width + "' Height='" + width + "' Canvas.Left='0' Canvas.Top='0'>";
            xaml += "     <Canvas.RenderTransform >";
            xaml += "         <TranslateTransform X = '0' Y = '0' />";
            xaml += "     </Canvas.RenderTransform >";
            xaml += "     <Canvas.Resources />";
            xaml += "     <Canvas Name='BuyCanvase' >";
            xaml += "           <Viewbox Height='" + width + "' Stretch='Fill' Width='" + width + "'>";
            xaml += "               <Grid>";
            xaml += "                   <Path Data='m 3.5 0.5 4 0 0 8 3 0 -5 5 -5 -5 3 0 z' Fill='#57B22020' Stroke='#FF990000' StrokeThickness='1'/>";
            xaml += "               </Grid>";
            xaml += "           </Viewbox>";
            xaml += "       </Canvas>";
            xaml += "</Canvas>";
            xaml += "</ControlTemplate>";

            return xaml;
        }
    }
}
