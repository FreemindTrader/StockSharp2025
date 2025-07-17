//// Decompiled with JetBrains decompiler
//// Type: StockSharp.Xaml.Charting.Visuals.Axes.LogarithmicNumericAxis
//// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
//// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
//// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

//using StockSharp.Xaml.Charting.Common.Extensions;
//using StockSharp.Xaml.Charting.Numerics;
//using StockSharp.Xaml.Charting.Numerics.TickCoordinateProviders;
//using StockSharp.Xaml.Charting.Visuals.Axes.LabelProviders;
//using StockSharp.Xaml.Charting.Visuals.Axes.LogarithmicAxis;
//using System;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using System.Windows;

//namespace StockSharp.Xaml.Charting.Visuals.Axes
//{
//    public class LogarithmicNumericAxis : NumericAxis, ILogarithmicAxis, IAxis, IAxisParams, IHitTestable, ISuspendable, IInvalidatableElement, IDrawable
//    {
//        public static readonly DependencyProperty LogarithmicBaseProperty = DependencyProperty.Register(nameof (LogarithmicBase), typeof (double), typeof (LogarithmicNumericAxis), new PropertyMetadata((object) 10.0, new PropertyChangedCallback(LogarithmicNumericAxis.OnLogarithmicBaseChanged)));

//        private static void OnLogarithmicBaseChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//        {
//            LogarithmicNumericAxis logarithmicNumericAxis = d as LogarithmicNumericAxis;
//            if ( logarithmicNumericAxis == null )
//            {
//                return;
//            }

//            if ( logarithmicNumericAxis.LogarithmicBase <= 0.0 )
//            {
//                throw new InvalidOperationException( string.Format( "The value {0} is not a valid base for the LogarithmicNumericAxis.", ( object ) logarithmicNumericAxis.LogarithmicBase ) );
//            }

//            AxisBase.InvalidateParent( d, e );
//        }

//        [TypeConverter( typeof( LogarithmicBaseConverter ) )]
//        public double LogarithmicBase
//        {
//            get
//            {
//                return ( double ) this.GetValue( LogarithmicNumericAxis.LogarithmicBaseProperty );
//            }
//            set
//            {
//                this.SetValue( LogarithmicNumericAxis.LogarithmicBaseProperty, ( object ) value );
//            }
//        }

//        public LogarithmicNumericAxis( )
//        {
//            this.LabelProvider = ( ILabelProvider ) new LogarithmicNumericLabelProvider();
//            this.SetCurrentValue( AxisBase.TickProviderProperty, ( object ) new LogarithmicNumericTickProvider() );
//        }

//        public override bool IsLogarithmicAxis
//        {
//            get
//            {
//                return true;
//            }
//        }

//        public override AxisParams GetAxisParams( )
//        {
//            AxisParams axisParams = base.GetAxisParams();
//            axisParams.IsLogarithmicAxis = true;
//            axisParams.LogarithmicBase = this.LogarithmicBase;
//            return axisParams;
//        }

//        public override bool IsValidRange( IRange range )
//        {
//            return base.IsValidRange( range ) && Math.Sign( range.Min.ToDouble() ) == Math.Sign( range.Max.ToDouble() );
//        }

//        protected override IDeltaCalculator GetDeltaCalculator( )
//        {
//            LogarithmicDeltaCalculator instance = (LogarithmicDeltaCalculator) LogarithmicDeltaCalculator.Instance;
//            instance.LogarithmicBase = this.LogarithmicBase;
//            return ( IDeltaCalculator ) instance;
//        }

//        protected override TickCoordinates CalculateTicks( )
//        {
//            LogarithmicNumericTickProvider tickProvider = this.TickProvider as LogarithmicNumericTickProvider;
//            if ( tickProvider != null )
//            {
//                tickProvider.LogarithmicBase = this.LogarithmicBase;
//            }

//            return base.CalculateTicks();
//        }

//        public override IRange GetDefaultNonZeroRange( )
//        {
//            return ( IRange ) new DoubleRange( Math.Pow( this.LogarithmicBase, -1.0 ), Math.Pow( this.LogarithmicBase, 2.0 ) );
//        }


//    }
//}

// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Axes.LogarithmicNumericAxis
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Numerics;
using StockSharp.Xaml.Charting.Numerics.TickCoordinateProviders;
using StockSharp.Xaml.Charting.Visuals.Axes.LabelProviders;
using StockSharp.Xaml.Charting.Visuals.Axes.LogarithmicAxis;

namespace StockSharp.Xaml.Charting.Visuals.Axes
{
    public class LogarithmicNumericAxis : NumericAxis, ILogarithmicAxis, IAxis, IAxisParams, IHitTestable, ISuspendable, IInvalidatableElement, IDrawable
    {
        public static readonly DependencyProperty LogarithmicBaseProperty = DependencyProperty.Register(nameof (LogarithmicBase), typeof (double), typeof (LogarithmicNumericAxis), new PropertyMetadata((object) 10.0, new PropertyChangedCallback(LogarithmicNumericAxis.OnLogarithmicBaseChanged)));

        private static void OnLogarithmicBaseChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            LogarithmicNumericAxis logarithmicNumericAxis = d as LogarithmicNumericAxis;
            if ( logarithmicNumericAxis == null )
            {
                return;
            }

            if ( logarithmicNumericAxis.LogarithmicBase <= 0.0 )
            {
                throw new InvalidOperationException( string.Format( "The value {0} is not a valid base for the LogarithmicNumericAxis.", ( object ) logarithmicNumericAxis.LogarithmicBase ) );
            }

            AxisBase.InvalidateParent( d, e );
        }

        [TypeConverter( typeof( LogarithmicBaseConverter ) )]
        public double LogarithmicBase
        {
            get
            {
                return ( double ) this.GetValue( LogarithmicNumericAxis.LogarithmicBaseProperty );
            }
            set
            {
                this.SetValue( LogarithmicNumericAxis.LogarithmicBaseProperty, ( object ) value );
            }
        }

        public LogarithmicNumericAxis()
        {
            this.LabelProvider = ( ILabelProvider ) new LogarithmicNumericLabelProvider();
            this.SetCurrentValue( AxisBase.TickProviderProperty, ( object ) new LogarithmicNumericTickProvider() );
        }

        public override bool IsLogarithmicAxis
        {
            get
            {
                return true;
            }
        }

        public override AxisParams GetAxisParams()
        {
            AxisParams axisParams = base.GetAxisParams();
            axisParams.IsLogarithmicAxis = true;
            axisParams.LogarithmicBase = this.LogarithmicBase;
            return axisParams;
        }

        public override bool IsValidRange( IRange range )
        {
            if ( base.IsValidRange( range ) )
            {
                return Math.Sign( range.Min.ToDouble() ) == Math.Sign( range.Max.ToDouble() );
            }

            return false;
        }

        protected override IDeltaCalculator GetDeltaCalculator()
        {
            LogarithmicDeltaCalculator instance = (LogarithmicDeltaCalculator) LogarithmicDeltaCalculator.Instance;
            instance.LogarithmicBase = this.LogarithmicBase;
            return ( IDeltaCalculator ) instance;
        }

        protected override TickCoordinates CalculateTicks()
        {
            LogarithmicNumericTickProvider tickProvider = this.TickProvider as LogarithmicNumericTickProvider;
            if ( tickProvider != null )
            {
                tickProvider.LogarithmicBase = this.LogarithmicBase;
            }

            return base.CalculateTicks();
        }

        public override IRange GetDefaultNonZeroRange()
        {
            return ( IRange ) new DoubleRange( Math.Pow( this.LogarithmicBase, -1.0 ), Math.Pow( this.LogarithmicBase, 2.0 ) );
        }

        [SpecialName]
        HorizontalAlignment IAxis.HorizontalAlignment
        {
            get
            {
                return this.HorizontalAlignment;
            }

            set
            {
                this.HorizontalAlignment = value;
            }
        }



        [SpecialName]
        VerticalAlignment IAxis.VerticalAlignment
        {
            get
            {
                return this.VerticalAlignment;
            }

            set
            {
                this.VerticalAlignment = value;
            }

        }


        [SpecialName]
        Visibility IAxis.Visibility
        {
            get
            {
                return this.Visibility;
            }

            set
            {
                this.Visibility = value;
            }

        }


        [SpecialName]
        double IHitTestable.ActualWidth
        {
            get
            {
                return this.ActualWidth;
            }
        }

        [SpecialName]
        double IHitTestable.ActualHeight
        {
            get
            {
                return this.ActualHeight;
            }
        }
    }
}

