// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.UltrachartRenderedMessage
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
namespace Ecng.Xaml.Charting
{
    public class UltrachartRenderedMessage : LoggedMessageBase
    {
        private readonly IRenderContext2D _renderContext;

        public UltrachartRenderedMessage( object sender, IRenderContext2D renderContext )
          : base( sender )
        {
            this._renderContext = renderContext;
        }

        [Obsolete( "BitmapContext is no longer exposed. Instead, use RenderContext for 2D drawing operations" )]
        public BitmapContext BitmapContext
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRenderContext2D RenderContext
        {
            get
            {
                return this._renderContext;
            }
        }
    }
}
