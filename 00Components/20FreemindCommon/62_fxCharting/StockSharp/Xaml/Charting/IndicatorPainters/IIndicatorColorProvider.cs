using System.Windows.Media;

#nullable disable
namespace fx.Charting.IndicatorPainters;

/// <summary>Provides colors for indicator painters.</summary>
public interface IIndicatorColorProvider
{
    /// <summary>
    /// Gets next color in sequence according to the current theme.
    /// </summary>
    Color GetNextColor();
}
