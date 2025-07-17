// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.LabelProviderBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
namespace Ecng.Xaml.Charting
{
    public abstract class LabelProviderBase : ProviderBase, ILabelProvider
    {
        public virtual void OnBeginAxisDraw()
        {
        }

        public virtual ITickLabelViewModel CreateDataContext( IComparable dataValue )
        {
            return this.UpdateDataContext( ( ITickLabelViewModel ) new DefaultTickLabelViewModel(), dataValue );
        }

        public virtual ITickLabelViewModel UpdateDataContext( ITickLabelViewModel labelDataContext, IComparable dataValue )
        {
            string str = this.FormatLabel(dataValue);
            labelDataContext.Text = str;
            return labelDataContext;
        }

        public abstract string FormatLabel( IComparable dataValue );

        public abstract string FormatCursorLabel( IComparable dataValue );
    }
}
