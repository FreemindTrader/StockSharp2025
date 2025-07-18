// Decompiled with JetBrains decompiler
// Type: #=ziARJyOecclYiJO5UbZqQJ8tTDgXi4mHI9$dB3n962FPd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public abstract class LabelProviderBase :
  ProviderBase,
  ILabelProvider
{
    public virtual void OnBeginAxisDraw()
    {
    }

    public virtual ITickLabelViewModel CreateDataContext(
      IComparable _param1 )
    {
        return this.UpdateDataContext( ( ITickLabelViewModel ) new DefaultTickLabelViewModel(), _param1 );
    }

    public virtual ITickLabelViewModel UpdateDataContext(
      ITickLabelViewModel _param1,
      IComparable _param2 )
    {
        string str = this.FormatLabel(_param2);
        _param1.set_Text( str );
        return _param1;
    }

    public abstract string FormatLabel( IComparable _param1 );

    public abstract string FormatCursorLabel( IComparable _param1, bool _param2 );
}
