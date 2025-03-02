﻿using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.PropertyGrid;
using fx.Charting;
using fx.Charting.Ultrachart;

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

        IfxChartElement element = GetChartElement( editorColumn.Owner );
        if ( element == null )
        {
            return base.CreateEditor( assignEditorSettings, defaultViewInfo, optimizationMode );
        }

        SetItemSource( element );
        return base.CreateEditor( assignEditorSettings, defaultViewInfo, optimizationMode );
    }

    protected abstract void SetItemSource( IfxChartElement element );

    private IfxChartElement GetChartElement( RowData data )
    {
        IndicatorUISettingsObject elementSettingsObject;
        for ( ; ( elementSettingsObject = data.Value as IndicatorUISettingsObject ) == null; data = data.Parent )
        {
            IfxChartElement chartElement;
            if ( ( chartElement = data.Value as IfxChartElement ) != null )
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
