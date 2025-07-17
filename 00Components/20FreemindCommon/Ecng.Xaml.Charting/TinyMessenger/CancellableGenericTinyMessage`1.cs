// Decompiled with JetBrains decompiler
// Type: TinyMessenger.CancellableGenericTinyMessage`1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace TinyMessenger
{
    public class CancellableGenericTinyMessage<TContent> : TinyMessageBase
    {
        public Action Cancel
        {
            get; protected set;
        }

        public TContent Content
        {
            get; protected set;
        }

        public CancellableGenericTinyMessage( object sender, TContent content, Action cancelAction )
          : base( sender )
        {
            if ( cancelAction == null )
                throw new ArgumentNullException( nameof( cancelAction ) );
            this.Content = content;
            this.Cancel = cancelAction;
        }
    }
}
