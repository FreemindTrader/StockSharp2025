// Decompiled with JetBrains decompiler
// Type: #=zkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using StockSharp.Xaml.Charting.Visuals.Axes;

#nullable disable
public interface ILabelProvider
{
    void Init( IAxis _param1 );

    void OnBeginAxisDraw();

    ITickLabelViewModel CreateDataContext( IComparable _param1 );

    ITickLabelViewModel UpdateDataContext( ITickLabelViewModel _param1, IComparable _param2 );

    string FormatLabel( IComparable _param1 );

    string FormatCursorLabel( IComparable _param1, bool _param2 );
}
