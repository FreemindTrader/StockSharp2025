using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace fx.Charting
{
    [Serializable]
    public enum ChartAnnotationTypes
    {
        None,
        [Display( Name = "Str1898",        ResourceType = typeof( LocalizedStrings ) )] LineAnnotation,
        [Display( Name = "Str1899",        ResourceType = typeof( LocalizedStrings ) )] LineArrowAnnotation,
        [Display( Name = "Str217",         ResourceType = typeof( LocalizedStrings ) )] TextAnnotation,
        [Display( Name = "Area",           ResourceType = typeof( LocalizedStrings ) )] BoxAnnotation,
        [Display( Name = "Str1901",        ResourceType = typeof( LocalizedStrings ) )] HorizontalLineAnnotation,
        [Display( Name = "Str1902",        ResourceType = typeof( LocalizedStrings ) )] VerticalLineAnnotation,
        [Display( Name = "FibRetracement", ResourceType = typeof( LocalizedStrings ) )] fxFibonacciRetracementAnnotation,
        [Display( Name = "FibExpansion",   ResourceType = typeof( LocalizedStrings ) )] fxFibonacciExtensionAnnotation,
        [Display( Name = "ElliottWave",    ResourceType = typeof( LocalizedStrings ) )] fxElliotWaveAnnotation,
        [Display( Name = "Str1902",        ResourceType = typeof (LocalizedStrings ) )] RulerAnnotation,
    }
}
