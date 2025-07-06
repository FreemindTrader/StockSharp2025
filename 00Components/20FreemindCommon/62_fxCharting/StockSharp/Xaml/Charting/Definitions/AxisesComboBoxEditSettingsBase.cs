using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.PropertyGrid;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.Ultrachart;

internal abstract class AxisesComboBoxEditSettingsBase : ComboBoxEditSettings
{
    public override IBaseEdit CreateEditor( bool assignEditorSettings, IDefaultEditorViewInfo defaultViewInfo, EditorOptimizationMode optimizationMode )
    {
        if ( ItemsSource != null )
        {
            return base.CreateEditor( assignEditorSettings, defaultViewInfo, optimizationMode );
        }

        EditorColumn editorColumn;
        if ( ( editorColumn = defaultViewInfo as EditorColumn ) == null )
        {
            return base.CreateEditor( assignEditorSettings, defaultViewInfo, optimizationMode );
        }

        IChartElement element = GetChartElement( editorColumn.Owner );
        if ( element == null )
        {
            return base.CreateEditor( assignEditorSettings, defaultViewInfo, optimizationMode );
        }

        SetItemSource( element );
        return base.CreateEditor( assignEditorSettings, defaultViewInfo, optimizationMode );
    }

    protected abstract void SetItemSource( IChartElement element );

    private IChartElement GetChartElement( RowData data )
    {
        ChartIndicatorElementSettingsObject elementSettingsObject;
        for ( ; ( elementSettingsObject = data.Value as ChartIndicatorElementSettingsObject ) == null; data = data.Parent )
        {
            IChartElement chartElement;
            if ( ( chartElement = data.Value as IChartElement ) != null )
            {
                return chartElement;
            }

            if ( data.Parent == null )
            {
                return null;
            }
        }
        return elementSettingsObject.Orig;
    }
}
