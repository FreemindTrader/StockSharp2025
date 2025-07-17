// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Animation.DoubleAnimator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace fx.Xaml.Charting
{
    internal class DoubleAnimator
    {
        private TimeSpan duration;
        private UIElement target;
        private string targetProperty;
        private double from;
        private double to;
        private EventHandler handler;

        public DoubleAnimator WithTarget( UIElement target )
        {
            this.target = target;
            return this;
        }

        public DoubleAnimator WithFromTo( double from, double to )
        {
            this.from = from;
            this.to = to;
            return this;
        }

        public DoubleAnimator WithTargetProperty( string targetProperty )
        {
            this.targetProperty = targetProperty;
            return this;
        }

        public DoubleAnimator WithDuration( TimeSpan duration )
        {
            this.duration = duration;
            return this;
        }

        public DoubleAnimator WithCompletedHandler( EventHandler handler )
        {
            this.handler = handler;
            return this;
        }

        public void Go()
        {
            SplineDoubleKeyFrame splineDoubleKeyFrame1 = new SplineDoubleKeyFrame();
            splineDoubleKeyFrame1.KeyTime = KeyTime.FromTimeSpan( TimeSpan.Zero );
            splineDoubleKeyFrame1.Value = this.from;
            SplineDoubleKeyFrame splineDoubleKeyFrame2 = splineDoubleKeyFrame1;
            SplineDoubleKeyFrame splineDoubleKeyFrame3 = new SplineDoubleKeyFrame();
            splineDoubleKeyFrame3.KeyTime = KeyTime.FromTimeSpan( this.duration );
            splineDoubleKeyFrame3.KeySpline = new KeySpline()
            {
                ControlPoint1 = new Point( 0.73, 0.14 ),
                ControlPoint2 = new Point( 0.1, 1.0 )
            };
            splineDoubleKeyFrame3.Value = this.to;
            SplineDoubleKeyFrame splineDoubleKeyFrame4 = splineDoubleKeyFrame3;
            DoubleAnimationUsingKeyFrames animationUsingKeyFrames = new DoubleAnimationUsingKeyFrames() { KeyFrames = { (DoubleKeyFrame) splineDoubleKeyFrame2, (DoubleKeyFrame) splineDoubleKeyFrame4 } };
            Storyboard.SetTarget( ( DependencyObject ) animationUsingKeyFrames, ( DependencyObject ) this.target );
            Storyboard.SetTargetProperty( ( DependencyObject ) animationUsingKeyFrames, new PropertyPath( this.targetProperty, new object[ 0 ] ) );
            if ( this.handler != null )
                animationUsingKeyFrames.Completed += this.handler;
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add( ( Timeline ) animationUsingKeyFrames );
            storyboard.Begin();
        }
    }
}
