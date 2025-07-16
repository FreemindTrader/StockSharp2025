// Decompiled with JetBrains decompiler
// Type: -.AxisTitleTemplateSelector
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Charting;

public sealed class AxisTitleTemplateSelector : DataTemplateSelector
{

    private DataTemplate _stringTitleTemplate;


    public DataTemplate StringTitleTemplate
    {
        get => this._stringTitleTemplate;
        set
        {
            this._stringTitleTemplate = value;
            this.UpdateControlTemplate();
        }
    }

    public override DataTemplate SelectTemplate( object item, DependencyObject container )
    {
        if ( item is string )
        {
            return StringTitleTemplate;
        }
        return base.SelectTemplate( item, container );
    }
}
