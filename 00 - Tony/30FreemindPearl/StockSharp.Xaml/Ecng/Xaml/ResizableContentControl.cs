// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.ResizableContentControl
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  [TemplatePart(Name = "PART_Gripper", Type = typeof (Thumb))]
  [TemplatePart(Name = "PART_Presenter", Type = typeof (ContentPresenter))]
  public class ResizableContentControl : ContentControl
  {
    /// <summary>
    /// </summary>
    public static readonly DependencyProperty CanAutoSizeProperty = DependencyProperty.Register(nameof(2127280029), typeof (bool), typeof (ResizableContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
    /// <summary>
    /// </summary>
    public static readonly DependencyProperty GripperBackgroundProperty = DependencyProperty.Register(nameof(2127280015), typeof (Brush), typeof (ResizableContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) Brushes.Transparent));
    /// <summary>
    /// </summary>
    public static readonly DependencyProperty GripperForegroundProperty = DependencyProperty.Register(nameof(2127279223), typeof (Brush), typeof (ResizableContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) new SolidColorBrush(Color.FromRgb((byte) 184, (byte) 180, (byte) 162))));
    /// <summary>
    /// </summary>
    public static readonly DependencyProperty ResizeModeProperty = DependencyProperty.Register(nameof(2127279199), typeof (ControlResizeMode), typeof (ResizableContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) ControlResizeMode.Both));
    
    private Thumb \u0023\u003Dz2GcW1\u0024Ac1uY6UoM8fw\u003D\u003D;
    
    private Size \u0023\u003DzwD7BuzssgqXl;
    
    private Point \u0023\u003DzGtsEDLcy8W1Y;

    static ResizableContentControl()
    {
      Control.IsTabStopProperty.OverrideMetadata(typeof (ResizableContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof (ResizableContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) typeof (ResizableContentControl)));
      FrameworkElement.MinHeightProperty.OverrideMetadata(typeof (ResizableContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) 4.0));
      FrameworkElement.MinWidthProperty.OverrideMetadata(typeof (ResizableContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) 4.0));
      UIElement.FocusableProperty.OverrideMetadata(typeof (ResizableContentControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
    }

    /// <summary>
    /// </summary>
    public ResizableContentControl()
    {
    }

    /// <summary>
    /// </summary>
    public ResizableContentControl(object content)
      : this()
    {
      this.Content = content;
    }

    /// <summary>
    /// </summary>
    public bool CanAutoSize
    {
      get
      {
        return (bool) this.GetValue(ResizableContentControl.CanAutoSizeProperty);
      }
      set
      {
        this.SetValue(ResizableContentControl.CanAutoSizeProperty, (object) value);
      }
    }

    /// <summary>
    /// </summary>
    public Brush GripperBackground
    {
      get
      {
        return (Brush) this.GetValue(ResizableContentControl.GripperBackgroundProperty);
      }
      set
      {
        this.SetValue(ResizableContentControl.GripperBackgroundProperty, (object) value);
      }
    }

    /// <summary>
    /// </summary>
    public Brush GripperForeground
    {
      get
      {
        return (Brush) this.GetValue(ResizableContentControl.GripperForegroundProperty);
      }
      set
      {
        this.SetValue(ResizableContentControl.GripperForegroundProperty, (object) value);
      }
    }

    /// <summary>
    /// </summary>
    public ControlResizeMode ResizeMode
    {
      get
      {
        return (ControlResizeMode) this.GetValue(ResizableContentControl.ResizeModeProperty);
      }
      set
      {
        this.SetValue(ResizableContentControl.ResizeModeProperty, (object) value);
      }
    }

    private Thumb \u0023\u003DzsXhmX7nVG8wb()
    {
      return this.GetTemplateChild(nameof(2127279168)) as Thumb;
    }

    private void \u0023\u003DzlDi1_r6VeOJA(object _param1, DragDeltaEventArgs _param2)
    {
      Size size1;
      ((Size) ref size1).\u002Ector(double.PositiveInfinity, double.PositiveInfinity);
      Size size2;
      ((Size) ref size2).\u002Ector(this.MinWidth, this.MinHeight);
      FrameworkElement content = this.Content as FrameworkElement;
      while (true)
      {
        ContentPresenter contentPresenter = content as ContentPresenter;
        if (contentPresenter != null)
          content = contentPresenter.Content as FrameworkElement;
        else
          break;
      }
      if (content != null)
      {
        Rect rect = content.TransformToAncestor((Visual) this).TransformBounds(new Rect(new Point(0.0, 0.0), content.RenderSize));
        Size renderSize1 = this.RenderSize;
        double num1 = Math.Max(0.0, ((Size) ref renderSize1).get_Width() - ((Rect) ref rect).get_Width());
        Size renderSize2 = this.RenderSize;
        double num2 = Math.Max(0.0, ((Size) ref renderSize2).get_Height() - ((Rect) ref rect).get_Height());
        ((Size) ref size2).set_Width(Math.Max(((Size) ref size2).get_Width(), content.MinWidth + num1));
        ((Size) ref size2).set_Height(Math.Max(((Size) ref size2).get_Height(), content.MinHeight + num2));
        if (!double.IsNaN(content.MaxWidth) && content.MaxWidth < 100000.0)
          ((Size) ref size1).set_Width(Math.Min(((Size) ref size1).get_Width(), content.MaxWidth + num1));
        if (!double.IsNaN(content.MaxHeight) && content.MaxHeight < 100000.0)
          ((Size) ref size1).set_Height(Math.Min(((Size) ref size1).get_Height(), content.MaxHeight + num2));
      }
      if (BrowserInteropHelper.IsBrowserHosted)
      {
        if (this.ResizeMode == ControlResizeMode.Both || this.ResizeMode == ControlResizeMode.Horizontal)
        {
          double width1 = ((Size) ref size1).get_Width();
          double width2 = ((Size) ref size2).get_Width();
          Size renderSize = this.RenderSize;
          double val2_1 = ((Size) ref renderSize).get_Width() + _param2.HorizontalChange;
          double val2_2 = Math.Max(width2, val2_1);
          this.Width = Math.Min(width1, val2_2);
        }
        if (this.ResizeMode != ControlResizeMode.Both && this.ResizeMode != ControlResizeMode.Vertical)
          return;
        double height1 = ((Size) ref size1).get_Height();
        double height2 = ((Size) ref size2).get_Height();
        Size renderSize1 = this.RenderSize;
        double val2_3 = ((Size) ref renderSize1).get_Height() + _param2.VerticalChange;
        double val2_4 = Math.Max(height2, val2_3);
        this.Height = Math.Min(height1, val2_4);
      }
      else
      {
        Point screen = this.PointToScreen(Mouse.GetPosition((IInputElement) this));
        if (this.ResizeMode == ControlResizeMode.Both || this.ResizeMode == ControlResizeMode.Horizontal)
          this.Width = Math.Min(((Size) ref size1).get_Width(), Math.Max(((Size) ref size2).get_Width(), ((Size) ref this.\u0023\u003DzwD7BuzssgqXl).get_Width() + (this.FlowDirection == FlowDirection.LeftToRight ? 1.0 : -1.0) * (((Point) ref screen).get_X() - ((Point) ref this.\u0023\u003DzGtsEDLcy8W1Y).get_X())));
        if (this.ResizeMode != ControlResizeMode.Both && this.ResizeMode != ControlResizeMode.Vertical)
          return;
        this.Height = Math.Min(((Size) ref size1).get_Height(), Math.Max(((Size) ref size2).get_Height(), ((Size) ref this.\u0023\u003DzwD7BuzssgqXl).get_Height() + (((Point) ref screen).get_Y() - ((Point) ref this.\u0023\u003DzGtsEDLcy8W1Y).get_Y())));
      }
    }

    private void \u0023\u003DzMKmeA3ViF\u0024ZR(object _param1, DragStartedEventArgs _param2)
    {
      if (BrowserInteropHelper.IsBrowserHosted)
        return;
      this.\u0023\u003DzGtsEDLcy8W1Y = this.PointToScreen(Mouse.GetPosition((IInputElement) this));
      this.\u0023\u003DzwD7BuzssgqXl = this.RenderSize;
    }

    private void \u0023\u003DzmMscCRWMwznF8lXQnA\u003D\u003D(
      object _param1,
      MouseButtonEventArgs _param2)
    {
      if (!this.CanAutoSize)
        return;
      this.AutoSize();
    }

    /// <summary>
    /// </summary>
    public void AutoSize()
    {
      this.Width = double.NaN;
      this.Height = double.NaN;
    }

    /// <inheritdoc />
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      if (this.\u0023\u003Dz2GcW1\u0024Ac1uY6UoM8fw\u003D\u003D != null)
      {
        this.\u0023\u003Dz2GcW1\u0024Ac1uY6UoM8fw\u003D\u003D.DragDelta -= new DragDeltaEventHandler(this.\u0023\u003DzlDi1_r6VeOJA);
        this.\u0023\u003Dz2GcW1\u0024Ac1uY6UoM8fw\u003D\u003D.DragStarted -= new DragStartedEventHandler(this.\u0023\u003DzMKmeA3ViF\u0024ZR);
        this.\u0023\u003Dz2GcW1\u0024Ac1uY6UoM8fw\u003D\u003D.MouseDoubleClick -= new MouseButtonEventHandler(this.\u0023\u003DzmMscCRWMwznF8lXQnA\u003D\u003D);
      }
      this.\u0023\u003Dz2GcW1\u0024Ac1uY6UoM8fw\u003D\u003D = this.\u0023\u003DzsXhmX7nVG8wb();
      if (this.\u0023\u003Dz2GcW1\u0024Ac1uY6UoM8fw\u003D\u003D == null)
        return;
      this.\u0023\u003Dz2GcW1\u0024Ac1uY6UoM8fw\u003D\u003D.DragDelta += new DragDeltaEventHandler(this.\u0023\u003DzlDi1_r6VeOJA);
      this.\u0023\u003Dz2GcW1\u0024Ac1uY6UoM8fw\u003D\u003D.DragStarted += new DragStartedEventHandler(this.\u0023\u003DzMKmeA3ViF\u0024ZR);
      this.\u0023\u003Dz2GcW1\u0024Ac1uY6UoM8fw\u003D\u003D.MouseDoubleClick += new MouseButtonEventHandler(this.\u0023\u003DzmMscCRWMwznF8lXQnA\u003D\u003D);
    }
  }
}
