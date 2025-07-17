// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.BaseMountainRenderableSeries
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
namespace fx.Xaml.Charting
{
    public abstract class BaseMountainRenderableSeries : BaseRenderableSeries
    {
        public static readonly DependencyProperty IsDigitalLineProperty = DependencyProperty.Register(nameof (IsDigitalLine), typeof (bool), typeof (BaseMountainRenderableSeries), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));
        public static readonly DependencyProperty AreaBrushProperty = DependencyProperty.Register(nameof (AreaBrush), typeof (Brush), typeof (BaseMountainRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.OnInvalidateParentSurface)));

        protected BaseMountainRenderableSeries()
        {
            this.SetCurrentValue( BaseRenderableSeries.ResamplingModeProperty, ( object ) ResamplingMode.Max );
        }

        public Brush AreaBrush
        {
            get
            {
                return ( Brush ) this.GetValue( BaseMountainRenderableSeries.AreaBrushProperty );
            }
            set
            {
                this.SetValue( BaseMountainRenderableSeries.AreaBrushProperty, ( object ) value );
            }
        }

        [Obsolete( "AreaColor is obsolete. Please use the AreaBrush property instead", true )]
        public Color AreaColor
        {
            get; set;
        }

        public bool IsDigitalLine
        {
            get
            {
                return ( bool ) this.GetValue( BaseMountainRenderableSeries.IsDigitalLineProperty );
            }
            set
            {
                this.SetValue( BaseMountainRenderableSeries.IsDigitalLineProperty, ( object ) value );
            }
        }
    }
}
