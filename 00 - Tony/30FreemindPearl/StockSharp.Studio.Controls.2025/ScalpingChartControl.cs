// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ScalpingChartControl
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.GroupRowLayout;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Messages;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    public class ScalpingChartControl : UserControl
    {
        private readonly SyncObject _lock = new SyncObject();
        
        private readonly SortedDictionary<long, (double? bid, double? ask, (Sides side, double price, double volume)? tick)> _data = new SortedDictionary<long, ValueTuple<double?, double?, ValueTuple<Sides, double, double>?>>();
        private readonly Dictionary<ValueTuple<Color, Color, int>, Color> _colorBlendCache = new Dictionary<ValueTuple<Color, Color, int>, Color>();
        private Color _backgroundColor = Color.FromRgb((byte) 30, (byte) 30, (byte) 30);
        private readonly Color _askLineColor = Color.FromRgb((byte) 224, (byte) 123, (byte) 139);
        private readonly Color _bidLineColor = Color.FromRgb((byte) 72, (byte) 190, (byte) 149);
        private readonly Color _buyVolumeColor = Color.FromRgb((byte) 72, (byte) 190, (byte) 149);
        private readonly Color _sellVolumeColor = Color.FromRgb((byte) 224, (byte) 123, (byte) 139);
        private readonly Color _buyVolumeBorderColor = Color.FromRgb((byte) 50, (byte) 133, (byte) 104);
        private readonly Color _sellVolumeBorderColor = Color.FromRgb((byte) 157, (byte) 86, (byte) 97);
        private readonly Color _crosshairColor = Colors.Gray;
        private readonly Color _highlightColor = Colors.Yellow;
        private readonly int _lineThickness = 4;
        private readonly int _circleBorderThickness = 2;
        private readonly int _crosshairThickness = 1;
        private long _minX = long.MaxValue;
        private long _maxX = long.MinValue;
        private double _minY = double.MaxValue;
        private double _maxY = double.MinValue;
        private WriteableBitmap _writeableBitmap;
        private readonly DispatcherTimer _updateTimer;
        private System.Windows.Point _mousePosition;
        private readonly TextBlock _crosshairTextBlock;        
        private (int x , int y, int radius)? _highlightedCircle;
        private double _maxVolume;
        private const int _maxCount = 100;

        public ScalpingChartControl()
        {
            Image image = new Image();
            this._crosshairTextBlock = new TextBlock
            {
                Foreground = Brushes.White,
                Background = new SolidColorBrush( Color.FromArgb( 128, 0, 0, 0 ) ),
                Padding = new Thickness( 5.0 ),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Visibility = Visibility.Collapsed
            };
            base.Content = new Grid
            {
                Children =
                {
                    image,
                    this._crosshairTextBlock
                }
            };
            this._updateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds( 500.0 )
            };
            this._updateTimer.Tick += delegate ( object _, EventArgs _ )
            {
                this.DrawChart();
            };
            this._updateTimer.Start();
            base.SizeChanged += delegate ( object _, SizeChangedEventArgs _ )
            {
                this.InitializeBitmap( image );
            };
            base.MouseMove += this.OnMouseMove;
            base.MouseLeave += delegate ( object _, MouseEventArgs _ )
            {
                this._crosshairTextBlock.Visibility = Visibility.Collapsed;
            };
            
            this._backgroundColor = ( ThemeExtensions.IsCurrDark() ? Color.FromRgb( 51, 51, 51 ) : Color.FromRgb( 249, 249, 249 ) );
            
            ThemeManager.ApplicationThemeChanged += delegate ( DependencyObject s, ThemeChangedRoutedEventArgs e )
            {
                this._backgroundColor = ( ThemeExtensions.IsCurrDark() ? Color.FromRgb( 51, 51, 51 ) : Color.FromRgb( 249, 249, 249 ) );
                this.DrawChart();
            };             
        }

        private void InitializeBitmap( Image image )
        {
            this._writeableBitmap = new WriteableBitmap( ( int ) this.ActualWidth, ( int ) this.ActualHeight, 96.0, 96.0, PixelFormats.Bgr32, ( BitmapPalette ) null );
            image.Source = ( ImageSource ) this._writeableBitmap;
            using ( ScalpingChartControl.DrawingContext drawingContext = new ScalpingChartControl.DrawingContext( this._writeableBitmap, this._backgroundColor ) )
                drawingContext.Clear();
            this.DrawChart();
        }

        private void OnMouseMove( object sender, MouseEventArgs e )
        {
            this._mousePosition = e.GetPosition( ( IInputElement ) this );
            double actualWidth = this.ActualWidth;
            double actualHeight = this.ActualHeight;
            string stringAndClear;
            lock ( this._lock )
            {
                if ( this._data.Count < 2 )
                    return;
                ValueTuple<double, double> yrange = this.CalculateYRange();
                double num1 = yrange.Item1;
                double num2 = yrange.Item2;
                double num3 = actualWidth / (double) (this._data.Count - 1);
                double num4 = actualHeight / (num2 - num1);
                int index1 = Math.Max(0, Math.Min((int) Math.Round( _mousePosition.X / num3), this._data.Count - 1));
                KeyValuePair<long, ValueTuple<double?, double?, ValueTuple<Sides, double, double>?>>[] array = this._data.ToArray<KeyValuePair<long, ValueTuple<double?, double?, ValueTuple<Sides, double, double>?>>>();
                long key;
                ValueTuple<double?, double?, ValueTuple<Sides, double, double>?> valueTuple1;
                array [index1].Deconstruct( out key, out valueTuple1 );
                ValueTuple<double?, double?, ValueTuple<Sides, double, double>?> valueTuple2 = valueTuple1;
                long utc = key;
                double? nullable1 = valueTuple2.Item1;
                double? nullable2 = valueTuple2.Item2;
                ValueTuple<int, ValueTuple<Sides, double, double>, int, double>? nullable3 = new ValueTuple<int, ValueTuple<Sides, double, double>, int, double>?();
                int mouseX = (int)_mousePosition.X;
                int mouseY = (int) _mousePosition.Y;
                for ( int index2 = 0; index2 < array.Length; ++index2 )
                {
                    array [index2].Deconstruct( out key, out valueTuple1 );
                    ValueTuple<double?, double?, ValueTuple<Sides, double, double>?> valueTuple3 = valueTuple1;
                    if ( valueTuple3.Item3.HasValue )
                    {
                        ValueTuple<Sides, double, double> valueTuple4 = valueTuple3.Item3.Value;
                        Sides sides = valueTuple4.Item1;
                        double num5 = valueTuple4.Item2;
                        double num6 = valueTuple4.Item3;
                        int x = (int) ((double) index2 * num3);
                        int y = (int) (actualHeight - (num5 - num1) * num4);
                        int radius = Math.Max(5, (int) (Math.Sqrt(num6 / this._maxVolume) * 30.0));
                        if ( isInCircle() )
                        {
                            double center = distanceToCenter();
                            if ( !nullable3.HasValue || center < nullable3.Value.Item4 )
                                nullable3 = new ValueTuple<int, ValueTuple<Sides, double, double>, int, double>?( new ValueTuple<int, ValueTuple<Sides, double, double>, int, double>( index2, new ValueTuple<Sides, double, double>( sides, num5, num6 ), radius, center ) );
                        }

                        bool isInCircle()
                        {
                            int num1 = mouseX - x;
                            int num2 = mouseY - y;
                            return num1 * num1 + num2 * num2 <= radius * radius;
                        }

                        double distanceToCenter()
                        {
                            int num1 = mouseX - x;
                            int num2 = mouseY - y;
                            return Math.Sqrt( ( double ) ( num1 * num1 + num2 * num2 ) );
                        }
                    }
                }
                if ( nullable3.HasValue )
                {
                    ValueTuple<int, ValueTuple<Sides, double, double>, int, double> valueTuple3 = nullable3.Value;
                    ValueTuple<Sides, double, double> valueTuple4 = valueTuple3.Item2;
                    int index2 = valueTuple3.Item1;
                    Sides sides = valueTuple4.Item1;
                    double num5 = valueTuple4.Item2;
                    double num6 = valueTuple4.Item3;
                    int num7 = valueTuple3.Item3;
                    this._highlightedCircle = new ValueTuple<int, int, int>?( new ValueTuple<int, int, int>( ( int ) ( ( double ) index2 * num3 ), ( int ) ( actualHeight - ( num5 - num1 ) * num4 ), num7 ) );
                    DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 6);
                    interpolatedStringHandler.AppendFormatted( getDateStr( array [index2].Key ) );
                    interpolatedStringHandler.AppendLiteral( "\n" );
                    interpolatedStringHandler.AppendLiteral( "B: " );
                    ref DefaultInterpolatedStringHandler local1 = ref interpolatedStringHandler;
                    double? nullable4 = nullable1;
                    Decimal? nullable5 = nullable4.HasValue ? new Decimal?((Decimal) nullable4.GetValueOrDefault()) : new Decimal?();
                    local1.AppendFormatted<Decimal?>( nullable5 );
                    interpolatedStringHandler.AppendLiteral( "\n" );
                    interpolatedStringHandler.AppendLiteral( "A: " );
                    ref DefaultInterpolatedStringHandler local2 = ref interpolatedStringHandler;
                    nullable4 = nullable2;
                    Decimal? nullable6 = nullable4.HasValue ? new Decimal?((Decimal) nullable4.GetValueOrDefault()) : new Decimal?();
                    local2.AppendFormatted<Decimal?>( nullable6 );
                    interpolatedStringHandler.AppendLiteral( "\n" );
                    interpolatedStringHandler.AppendLiteral( "P: " );
                    interpolatedStringHandler.AppendFormatted<Decimal>( ( Decimal ) num5 );
                    interpolatedStringHandler.AppendLiteral( "\n" );
                    interpolatedStringHandler.AppendLiteral( "V: " );
                    interpolatedStringHandler.AppendFormatted<Decimal>( ( Decimal ) num6 );
                    interpolatedStringHandler.AppendLiteral( "\n" );
                    interpolatedStringHandler.AppendLiteral( "S: " );
                    interpolatedStringHandler.AppendFormatted<Sides>( sides );
                    stringAndClear = interpolatedStringHandler.ToStringAndClear();
                }
                else
                {
                    this._highlightedCircle = new ValueTuple<int, int, int>?();
                    DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
                    interpolatedStringHandler.AppendFormatted( getDateStr( utc ) );
                    interpolatedStringHandler.AppendLiteral( "\n" );
                    interpolatedStringHandler.AppendLiteral( "B: " );
                    ref DefaultInterpolatedStringHandler local1 = ref interpolatedStringHandler;
                    double? nullable4 = nullable1;
                    Decimal? nullable5 = nullable4.HasValue ? new Decimal?((Decimal) nullable4.GetValueOrDefault()) : new Decimal?();
                    local1.AppendFormatted<Decimal?>( nullable5 );
                    interpolatedStringHandler.AppendLiteral( "\n" );
                    interpolatedStringHandler.AppendLiteral( "A: " );
                    ref DefaultInterpolatedStringHandler local2 = ref interpolatedStringHandler;
                    nullable4 = nullable2;
                    Decimal? nullable6 = nullable4.HasValue ? new Decimal?((Decimal) nullable4.GetValueOrDefault()) : new Decimal?();
                    local2.AppendFormatted<Decimal?>( nullable6 );
                    stringAndClear = interpolatedStringHandler.ToStringAndClear();
                }
            }
            this._crosshairTextBlock.Text = stringAndClear;
            this._crosshairTextBlock.Visibility = Visibility.Visible;
            Canvas.SetLeft( ( UIElement ) this._crosshairTextBlock, Math.Min( _mousePosition.X + 10.0, actualWidth - this._crosshairTextBlock.ActualWidth - 10.0 ) );
            Canvas.SetTop( ( UIElement ) this._crosshairTextBlock, Math.Min( _mousePosition.Y + 10.0, actualHeight - this._crosshairTextBlock.ActualHeight - 10.0 ) );
            this.DrawChart();

            string getDateStr( long utc )
            {
                DateTime dateTime = TimeHelper.UtcKind(new DateTime(utc));
                dateTime = dateTime.ToLocalTime();
                return dateTime.ToString( "HH:mm:ss" );
            }
        }

        public void UpdateChart( DateTime time, double? bidPrice, double? askPrice, (Sides side, double price, double volume)? tick )
        {
            time = TimeHelper.Truncate( time, TimeSpan.FromSeconds( 1.0 ) );
            long ticks = time.Ticks;
            SyncObject @lock = this._lock;
            lock ( @lock )
            {
                ValueTuple<double?, double?, ValueTuple<Sides, double, double>?> valueTuple;
                double? num;
                if ( this._data.TryGetValue( ticks, out valueTuple ) )
                {
                    ValueTuple<Sides, double, double>? item = (tick != null) ? ((valueTuple.Item3 != null && valueTuple.Item3.Value.Item3 > tick.Value.Item3) ? valueTuple.Item3 : tick) : valueTuple.Item3;
                    SortedDictionary<long, ValueTuple<double?, double?, ValueTuple<Sides, double, double>?>> data = this._data;
                    long key = ticks;
                    num = bidPrice;
                    double? item2 = (num != null) ? num : valueTuple.Item1;
                    num = askPrice;
                    data [key] = new ValueTuple<double?, double?, ValueTuple<Sides, double, double>?>( item2, ( num != null ) ? num : valueTuple.Item2, item );
                }
                else
                {
                    this._data [ticks] = new ValueTuple<double?, double?, ValueTuple<Sides, double, double>?>( bidPrice, askPrice, tick );
                }
                if ( bidPrice != null )
                {
                    this._minY = Math.Min( this._minY, bidPrice.Value );
                    this._maxY = Math.Max( this._maxY, bidPrice.Value );
                }
                if ( askPrice != null )
                {
                    this._minY = Math.Min( this._minY, askPrice.Value );
                    this._maxY = Math.Max( this._maxY, askPrice.Value );
                }
                num = ( ( tick != null ) ? new double?( tick.GetValueOrDefault().Item2 ) : null );
                if ( num != null )
                {
                    double valueOrDefault = num.GetValueOrDefault();
                    this._minY = Math.Min( this._minY, valueOrDefault );
                    this._maxY = Math.Max( this._maxY, valueOrDefault );
                }
                num = ( ( tick != null ) ? new double?( tick.GetValueOrDefault().Item3 ) : null );
                if ( num != null )
                {
                    double valueOrDefault2 = num.GetValueOrDefault();
                    this._maxVolume = Math.Max( this._maxVolume, valueOrDefault2 );
                }
                this._minX = Math.Min( this._minX, ticks );
                this._maxX = Math.Max( this._maxX, ticks );
                if ( this._data.Count > 100 )
                {
                    bool flag2 = false;
                    bool flag3 = false;
                    while ( this._data.Count > 100 )
                    {
                        ValueTuple<double?, double?, ValueTuple<Sides, double, double>?> andRemove = CollectionHelper.GetAndRemove<long, ValueTuple<double?, double?, ValueTuple<Sides, double, double>?>>(this._data, this._data.Keys.First<long>());
                        num = ( ( andRemove.Item3 != null ) ? new double?( andRemove.Item3.GetValueOrDefault().Item3 ) : null );
                        double num2 = this._maxVolume;
                        if ( num.GetValueOrDefault() == num2 & num != null )
                        {
                            flag2 = true;
                        }
                        num = andRemove.Item1;
                        num2 = this._minY;
                        if ( !( num.GetValueOrDefault() == num2 & num != null ) )
                        {
                            num = andRemove.Item2;
                            num2 = this._minY;
                            if ( !( num.GetValueOrDefault() == num2 & num != null ) )
                            {
                                num = ( ( andRemove.Item3 != null ) ? new double?( andRemove.Item3.GetValueOrDefault().Item2 ) : null );
                                num2 = this._minY;
                                if ( !( num.GetValueOrDefault() == num2 & num != null ) )
                                {
                                    num = andRemove.Item1;
                                    num2 = this._maxY;
                                    if ( !( num.GetValueOrDefault() == num2 & num != null ) )
                                    {
                                        num = andRemove.Item2;
                                        num2 = this._maxY;
                                        if ( !( num.GetValueOrDefault() == num2 & num != null ) )
                                        {
                                            num = ( ( andRemove.Item3 != null ) ? new double?( andRemove.Item3.GetValueOrDefault().Item2 ) : null );
                                            num2 = this._maxY;
                                            if ( !( num.GetValueOrDefault() == num2 & num != null ) )
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        flag3 = true;
                    }
                    this._minX = this._data.Keys.First<long>();
                    if ( flag2 )
                    {
                        this._maxVolume = this._data.Values.Max( delegate ( (double? bid, double? ask, (Sides side, double price, double volume)? tick) v )
                        {
                            if ( v.Item3 == null )
                            {
                                return 0.0;
                            }
                            return v.Item3.GetValueOrDefault().Item3;
                        } );
                    }
                    if ( flag3 )
                    {
                        double[] source = CollectionHelper.OrderBy<double>(from v in this._data.Values.SelectMany(( (double? bid, double? ask, (Sides side, double price, double volume)? tick) v) => new double?[]
                                                                            {
                                                                                v.Item1,
                                                                                v.Item2,
                                                                                (v.Item3 != null) ? new double?(v.Item3.GetValueOrDefault().Item2) : null
                                                                            }).Where(delegate(double? v)
                                                                            {
                                                                                double? num3 = v;
                                                                                double num4 = 0.0;
                                                                                return num3.GetValueOrDefault() > num4 & num3 != null;
                                                                            })
                                                                           select v.Value).ToArray<double>();
                        this._minY = source.First<double>();
                        this._maxY = source.Last<double>();
                    }
                }
            }
        }

        
        private (double minY, double maxY) CalculateYRange()
        {
            if ( this._data.Count == 0 )
                return new ValueTuple<double, double>( 0.0, 1.0 );
            if ( this._minY == this._maxY )
                return new ValueTuple<double, double>( this._minY - 0.5, this._maxY + 0.5 );
            double num = (this._maxY - this._minY) * 0.1;
            return new ValueTuple<double, double>( this._minY - num, this._maxY + num );
        }

        public void Clear()
        {
            lock ( this._lock )
                this._data.Clear();
            if ( this._writeableBitmap == null )
                return;
            using ( ScalpingChartControl.DrawingContext drawingContext = new ScalpingChartControl.DrawingContext( this._writeableBitmap, this._backgroundColor ) )
                drawingContext.Clear();
        }

        private void DrawChart()
        {
            if ( this._writeableBitmap == null )
                return;
            int pixelWidth = this._writeableBitmap.PixelWidth;
            int height = this._writeableBitmap.PixelHeight;
            List<ValueTuple<int, int, int, Color, Color>> valueTupleList1;
            System.Windows.Point[] array1;
            System.Windows.Point[] array2;
            lock ( this._lock )
            {
                if ( this._data.Count < 2 )
                    return;
                valueTupleList1 = new List<ValueTuple<int, int, int, Color, Color>>();
                ValueTuple<double, double> yrange = this.CalculateYRange();
                double minY = yrange.Item1;
                double num1 = yrange.Item2;
                double scaleX = (double) pixelWidth / (double) (this._data.Count - 1);
                double scaleY = (double) height / (num1 - minY);
                array1 = ScalpingChartControl.SmoothPoints( this._data.Select( ( kvp, index ) => new
                {
                    Index = index,
                    Value = kvp.Value
                } ).Where( kvp =>
                {
                    if ( !kvp.Value.Item2.HasValue )
                        return false;
                    double? nullable = kvp.Value.Item2;
                    double num = 0.0;
                    return !( nullable.GetValueOrDefault() == num & nullable.HasValue );
                } ).Select( item => new System.Windows.Point( calculateX( item.Index ), calculateY( item.Value.Item2.Value ) ) ), 5 ).ToArray<System.Windows.Point>();
                array2 = ScalpingChartControl.SmoothPoints( this._data.Select( ( kvp, index ) => new
                {
                    Index = index,
                    Value = kvp.Value
                } ).Where( kvp =>
                {
                    if ( !kvp.Value.Item1.HasValue )
                        return false;
                    double? nullable = kvp.Value.Item1;
                    double num = 0.0;
                    return !( nullable.GetValueOrDefault() == num & nullable.HasValue );
                } ).Select( item => new System.Windows.Point( calculateX( item.Index ), calculateY( item.Value.Item1.Value ) ) ), 5 ).ToArray<System.Windows.Point>();
                int index1 = -1;
                foreach ( KeyValuePair<long, ValueTuple<double?, double?, ValueTuple<Sides, double, double>?>> keyValuePair in this._data )
                {
                    long key;
                    ValueTuple<double?, double?, ValueTuple<Sides, double, double>?> valueTuple1;
                    keyValuePair.Deconstruct( out key, out valueTuple1 );
                    ValueTuple<double?, double?, ValueTuple<Sides, double, double>?> valueTuple2 = valueTuple1;
                    ++index1;
                    if ( valueTuple2.Item3.HasValue )
                    {
                        ValueTuple<Sides, double, double> valueTuple3 = valueTuple2.Item3.Value;
                        Sides sides = valueTuple3.Item1;
                        double num2 = valueTuple3.Item2;
                        double num3 = valueTuple3.Item3;
                        double num4 = num2;
                        int x = (int) calculateX(index1);
                        int y = (int) calculateY(num4);
                        int num5 = Math.Max(5, (int) (Math.Sqrt(num3 / this._maxVolume) * 30.0));
                        Color color1;
                        Color color2;
                        if ( sides != Sides.Buy )
                        {
                            Color sellVolumeColor = this._sellVolumeColor;
                            color1 = this._sellVolumeBorderColor;
                            color2 = sellVolumeColor;
                        }
                        else
                        {
                            Color buyVolumeColor = this._buyVolumeColor;
                            color1 = this._buyVolumeBorderColor;
                            color2 = buyVolumeColor;
                        }
                        if ( this._highlightedCircle.HasValue )
                        {
                            ValueTuple<int, int, int> valueTuple4 = this._highlightedCircle.Value;
                            int num6 = x;
                            int num7 = y;
                            int num8 = num5;
                            if ( valueTuple4.Item1 == num6 && valueTuple4.Item2 == num7 && valueTuple4.Item3 == num8 )
                                color1 = this._highlightColor;
                        }
                        valueTupleList1.Add( new ValueTuple<int, int, int, Color, Color>( x, y, num5, color2, color1 ) );
                    }
                }

                double calculateX( int index )
                {
                    return ( double ) index * scaleX;
                }

                double calculateY( double value )
                {
                    return ( double ) height - ( value - minY ) * scaleY;
                }
            }
            valueTupleList1.Sort( ( Comparison<ValueTuple<int, int, int, Color, Color>> ) ( ( a, b ) => b.Item3.CompareTo( a.Item3 ) ) );
            using ( ScalpingChartControl.DrawingContext context = new ScalpingChartControl.DrawingContext( this._writeableBitmap, this._backgroundColor ) )
            {
                context.Clear();
                ScalpingChartControl.DrawLineWithOutline( context, array1, this._askLineColor, this._askLineColor, this._lineThickness );
                ScalpingChartControl.DrawLineWithOutline( context, array2, this._bidLineColor, this._bidLineColor, this._lineThickness );
                List<ValueTuple<int, int, int, Color, Color>> valueTupleList2 = new List<ValueTuple<int, int, int, Color, Color>>();
                foreach ( ValueTuple<int, int, int, Color, Color> valueTuple1 in valueTupleList1 )
                {
                    bool flag = false;
                    foreach ( ValueTuple<int, int, int, Color, Color> valueTuple2 in valueTupleList2 )
                    {
                        int x2 = valueTuple2.Item1;
                        int y2 = valueTuple2.Item2;
                        int r2 = valueTuple2.Item3;
                        if ( isInCircle( valueTuple1.Item1, valueTuple1.Item2, valueTuple1.Item3, x2, y2, r2 ) )
                        {
                            flag = true;
                            break;
                        }
                    }
                    if ( !flag )
                        valueTupleList2.Add( valueTuple1 );
                }
                foreach ( ValueTuple<int, int, int, Color, Color> valueTuple in valueTupleList2 )
                {
                    int x0 = valueTuple.Item1;
                    int y0 = valueTuple.Item2;
                    int radius = valueTuple.Item3;
                    Color fillColor = valueTuple.Item4;
                    Color borderColor = valueTuple.Item5;
                    if ( x0 >= 0 && x0 < pixelWidth )
                        this.DrawFilledCircle( context, x0, y0, radius, fillColor, borderColor );
                }
                if ( this._crosshairTextBlock.Visibility != Visibility.Visible )
                    return;
                ScalpingChartControl.DrawLine( context, new System.Windows.Point [2]
                {
          new System.Windows.Point(0.0, _mousePosition.Y),
          new System.Windows.Point((double) context.PixelWidth, _mousePosition.Y)
                }, this._crosshairColor, this._crosshairThickness );
                ScalpingChartControl.DrawLine( context, new System.Windows.Point [2]
                {
          new System.Windows.Point(_mousePosition.X, 0.0),
          new System.Windows.Point(_mousePosition.X, (double) context.PixelHeight)
                }, this._crosshairColor, this._crosshairThickness );
            }

            bool isInCircle( int x1, int y1, int r1, int x2, int y2, int r2 )
            {
                return ( x2 - x1 ) * ( x2 - x1 ) + ( y2 - y1 ) * ( y2 - y1 ) <= ( r2 - r1 ) * ( r2 - r1 );
            }
        }

        private static void DrawLine(
          ScalpingChartControl.DrawingContext context,
          System.Windows.Point [ ] points,
          Color color,
          int thickness )
        {
            for ( int index = 1; index < points.Length; ++index )
                ScalpingChartControl.DrawAntialiasedLine( context, points [index - 1], points [index], color, thickness );
        }

        private static IEnumerable<System.Windows.Point> SmoothPoints( IEnumerable<System.Windows.Point> points, int windowSize = 5 )
        {
            Queue<Point> buffer = new Queue<Point>(windowSize);
            foreach ( Point item in points )
            {
                buffer.Enqueue( item );
                if ( buffer.Count == windowSize )
                {
                    yield return new Point( buffer.Average( ( Point p ) => p.X ), buffer.Average( ( Point p ) => p.Y ) );
                    buffer.Dequeue();
                }
            }
            IEnumerator<Point> enumerator = null;
            yield break;
            yield break;
        }

        private static void DrawLineWithOutline( ScalpingChartControl.DrawingContext context, System.Windows.Point [ ] points, Color fillColor, Color outlineColor, int thickness )
        {
            if ( points.Length < 2 )
                return;
            for ( int index = 1; index < points.Length; ++index )
                ScalpingChartControl.DrawAntialiasedLine( context, points [index - 1], points [index], fillColor, thickness );
        }

        private static void DrawAntialiasedLine( ScalpingChartControl.DrawingContext context, System.Windows.Point start, System.Windows.Point end, Color color, int thickness )
        {
            int startX = (int)start.X;
            int startY = (int)start.Y;
            int endX = (int)end.X;
            int endY = (int)end.Y;
            bool flag = Math.Abs(endY - startY) > Math.Abs(endX - startX);
            if ( flag )
            {
                ScalpingChartControl.Swap<int>( ref startX, ref startY );
                ScalpingChartControl.Swap<int>( ref endX, ref endY );
            }
            if ( startX > endX )
            {
                ScalpingChartControl.Swap<int>( ref startX, ref endX );
                ScalpingChartControl.Swap<int>( ref startY, ref endY );
            }
            int xDist = endX - startX;
            int yDist = Math.Abs(endY - startY);
            float halfX = (float)xDist / 2f;
            int sign = (startY < endY) ? 1 : -1;
            int num9 = startY;
            for ( int i = startX; i <= endX; i++ )
            {
                float alpha = 1f - halfX / (float)xDist;
                if ( flag )
                {
                    for ( int j = -thickness / 2; j <= thickness / 2; j++ )
                    {
                        context.SetPixel( num9 + j, i, color, alpha );
                    }
                }
                else
                {
                    for ( int k = -thickness / 2; k <= thickness / 2; k++ )
                    {
                        context.SetPixel( i, num9 + k, color, alpha );
                    }
                }
                halfX -= ( float ) yDist;
                if ( halfX < 0f )
                {
                    num9 += sign;
                    halfX += ( float ) xDist;
                }
            }
        }

        private void DrawFilledCircle(
          ScalpingChartControl.DrawingContext context,
          int x0,
          int y0,
          int radius,
          Color fillColor,
          Color borderColor )
        {
            for ( int index1 = -radius; index1 <= radius; ++index1 )
            {
                for ( int index2 = -radius; index2 <= radius; ++index2 )
                {
                    int num1 = index1 * index1 + index2 * index2;
                    if ( num1 <= radius * radius )
                    {
                        double num2 = Math.Sqrt((double) num1);
                        if ( ( double ) radius - num2 < ( double ) this._circleBorderThickness )
                        {
                            double alpha = 1.0 - ((double) radius - num2) / (double) this._circleBorderThickness;
                            Color color = this.BlendColors(fillColor, borderColor, alpha);
                            context.SetPixel( x0 + index1, y0 + index2, color, 1f );
                        }
                        else
                            context.SetPixel( x0 + index1, y0 + index2, fillColor, 1f );
                    }
                }
            }
        }

        private Color BlendColors( Color c1, Color c2, double alpha )
        {
            return ( Color ) CollectionHelper.SafeAdd<ValueTuple<Color, Color, int>, Color>( this._colorBlendCache,  new ValueTuple<Color, Color, int>( c1, c2, ( int ) ( alpha * 100.0 ) ),  ( key => Color.FromArgb( byte.MaxValue, ( byte ) ( ( double ) c1.R * ( 1.0 - alpha ) + ( double ) c2.R * alpha ), ( byte ) ( ( double ) c1.G * ( 1.0 - alpha ) + ( double ) c2.G * alpha ), ( byte ) ( ( double ) c1.B * ( 1.0 - alpha ) + ( double ) c2.B * alpha ) ) ) );
        }

        private static void Swap<T>( ref T a, ref T b )
        {
            T obj1 = a;
            T obj2 = b;
            b = obj1;
            a = obj2;
        }

        private class DrawingContext : Disposable
        {
            private readonly WriteableBitmap _bitmap;
            private readonly Color _backgroundColor;

            public DrawingContext( WriteableBitmap bitmap, Color backgroundColor )
            {                
                WriteableBitmap writeableBitmap = bitmap;
                if ( writeableBitmap == null )
                    throw new ArgumentNullException( nameof( bitmap ) );
                this._bitmap = writeableBitmap;
                this.PixelWidth = bitmap.PixelWidth;
                this.PixelHeight = bitmap.PixelHeight;
                this.BackBuffer = bitmap.BackBuffer;
                this.Stride = bitmap.BackBufferStride;
                this._backgroundColor = backgroundColor;
                bitmap.Lock();
            }

            public int PixelWidth { get; }

            public int PixelHeight { get; }

            public IntPtr BackBuffer { get; }

            public int Stride { get; }

            protected override void DisposeManaged()
            {
                this._bitmap.AddDirtyRect( new Int32Rect( 0, 0, this.PixelWidth, this.PixelHeight ) );
                this._bitmap.Unlock();
                base.DisposeManaged();
            }

            public unsafe void SetPixel( int x, int y, Color color, float alpha )
            {
                if ( x < 0 || x >= this.PixelWidth || ( y < 0 || y >= this.PixelHeight ) )
                    return;
                IntPtr num1 = (IntPtr) (void*) this.BackBuffer + y * this.Stride + x * 4;
                Color backgroundColor = this._backgroundColor;
                *( sbyte* ) num1 = ( sbyte ) ( byte ) ( ( double ) backgroundColor.B * ( 1.0 - ( double ) alpha ) + ( double ) color.B * ( double ) alpha );
                IntPtr num2 = num1 + 1;
                backgroundColor = this._backgroundColor;
                int num3 = (int) (byte) ((double) backgroundColor.G * (1.0 - (double) alpha) + (double) color.G * (double) alpha);
                *( sbyte* ) num2 = ( sbyte ) num3;
                IntPtr num4 = num1 + 2;
                backgroundColor = this._backgroundColor;
                int num5 = (int) (byte) ((double) backgroundColor.R * (1.0 - (double) alpha) + (double) color.R * (double) alpha);
                *( sbyte* ) num4 = ( sbyte ) num5;
                *( sbyte* ) ( num1 + 3 ) = ( sbyte ) ( byte ) ( ( double ) byte.MaxValue * ( double ) alpha );
            }

            public void Clear()
            {
                int[] numArray = new int[this.PixelWidth * this.PixelHeight];
                Color backgroundColor = this._backgroundColor;
                int num1 = (int) backgroundColor.A << 24;
                backgroundColor = this._backgroundColor;
                int num2 = (int) backgroundColor.R << 16;
                int num3 = num1 | num2;
                backgroundColor = this._backgroundColor;
                int num4 = (int) backgroundColor.G << 8;
                int num5 = num3 | num4;
                backgroundColor = this._backgroundColor;
                int b = (int) backgroundColor.B;
                int num6 = num5 | b;
                for ( int index = 0; index < numArray.Length; ++index )
                    numArray [index] = num6;
                this._bitmap.WritePixels( new Int32Rect( 0, 0, this.PixelWidth, this.PixelHeight ), ( Array ) numArray, this.PixelWidth * 4, 0 );
            }
        }
    }
}
