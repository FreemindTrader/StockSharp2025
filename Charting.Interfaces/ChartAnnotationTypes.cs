namespace StockSharp.Charting;

/// <summary>
/// The annotations types.
/// </summary>
[Serializable]
public enum ChartAnnotationTypes
{
	/// <summary>
	/// None.
	/// </summary>
	[Display(ResourceType = typeof(LocalizedStrings), Name = LocalizedStrings.NoneKey)]
	None,

	/// <summary>
	/// Line.
	/// </summary>
	[Display(ResourceType = typeof(LocalizedStrings), Name = LocalizedStrings.Line2Key)]
	LineAnnotation,

	/// <summary>
	/// Pointer.
	/// </summary>
	[Display(ResourceType = typeof(LocalizedStrings), Name = LocalizedStrings.PointerKey)]
	LineArrowAnnotation,

	/// <summary>
	/// Text.
	/// </summary>
	[Display(ResourceType = typeof(LocalizedStrings), Name = LocalizedStrings.TextKey)]
	TextAnnotation,

	/// <summary>
	/// Area.
	/// </summary>
	[Display(ResourceType = typeof(LocalizedStrings), Name = LocalizedStrings.AreaKey)]
	BoxAnnotation,

	/// <summary>
	/// Horizontal line.
	/// </summary>
	[Display(ResourceType = typeof(LocalizedStrings), Name = LocalizedStrings.HorLineKey)]
	HorizontalLineAnnotation,

	/// <summary>
	/// Vertical line.
	/// </summary>
	[Display(ResourceType = typeof(LocalizedStrings), Name = LocalizedStrings.VerLineKey)]
	VerticalLineAnnotation,

	/// <summary>
	/// Ruler.
	/// </summary>
	[Display(ResourceType = typeof(LocalizedStrings), Name = LocalizedStrings.RulerKey)]
	RulerAnnotation,

    #region Tony New Defines
    /// <summary>
    /// Harmonic Elliott Wave Annotation
    /// </summary>
    fxElliotWaveAnnotation,

    /// <summary>
    /// Harmonic Elliott Wave Retracement
    /// </summary>
    fxFibonacciRetracementAnnotation,

    /// <summary>
    /// Harmonic Elliott Wave Extension
    /// </summary>
    fxFibonacciExtensionAnnotation

    #endregion
}