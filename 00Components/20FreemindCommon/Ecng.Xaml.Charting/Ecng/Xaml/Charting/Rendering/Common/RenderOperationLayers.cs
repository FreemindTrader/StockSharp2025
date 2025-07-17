// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Rendering.Common.RenderOperationLayers
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Collections;
using System.Collections.Generic;

namespace StockSharp.Xaml.Charting.Rendering.Common
{
    public class RenderOperationLayers : IEnumerable<RenderOperationLayer>, IEnumerable
    {
        private readonly IDictionary<RenderLayer, RenderOperationLayer> _layers = (IDictionary<RenderLayer, RenderOperationLayer>) new Dictionary<RenderLayer, RenderOperationLayer>() { { RenderLayer.AxisBands, new RenderOperationLayer() }, { RenderLayer.AxisMinorGridlines, new RenderOperationLayer() }, { RenderLayer.AxisMajorGridlines, new RenderOperationLayer() }, { RenderLayer.RenderableSeries, new RenderOperationLayer() } };

        public RenderOperationLayer this[ RenderLayer layer ]
        {
            get
            {
                return this._layers[ layer ];
            }
        }

        public IEnumerator<RenderOperationLayer> GetEnumerator()
        {
            return this._layers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ( IEnumerator ) this.GetEnumerator();
        }

        public void Flush()
        {
            foreach ( RenderOperationLayer renderOperationLayer in this )
                renderOperationLayer.Flush();
        }
    }
}
