// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Rendering.Common.RenderOperationLayer
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;

namespace StockSharp.Xaml.Charting.Rendering.Common
{
    public class RenderOperationLayer
    {
        private readonly List<Action> operations = new List<Action>();

        public void Enqueue( Action operation )
        {
            this.operations.Add( operation );
        }

        public void Flush()
        {
            foreach ( Action operation in this.operations )
                operation();
            this.operations.Clear();
        }
    }
}
