// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.TimeSpanEditor
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using Ecng.ComponentModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace Ecng.Xaml
{
  /// <summary>
  /// <see cref="T:System.TimeSpan" /> editor.
  ///     </summary>
  /// <summary>TimeSpanEditor</summary>
  public class TimeSpanEditor : UserControl, IComponentConnector
  {
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Value" />.
    ///     </summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(2127277753), typeof (TimeSpan?), typeof (TimeSpanEditor), (PropertyMetadata) new UIPropertyMetadata((object) TimeSpan.FromMinutes(1.0), new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzT5MpcJ4\u003D))));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.HasValue" />.
    ///     </summary>
    public static readonly DependencyProperty IsNullableProperty = DependencyProperty.Register(nameof(2127277749), typeof (bool), typeof (TimeSpanEditor), (PropertyMetadata) new UIPropertyMetadata((object) false));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.HasValue" />.
    ///     </summary>
    public static readonly DependencyProperty HasValueProperty = DependencyProperty.Register(nameof(2127279465), typeof (bool), typeof (TimeSpanEditor), (PropertyMetadata) new UIPropertyMetadata((object) true, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzDGS0nUE\u003D))));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Microseconds" />.
    ///     </summary>
    public static readonly DependencyProperty MicrosecondsProperty = DependencyProperty.Register(nameof(2127279448), typeof (int), typeof (TimeSpanEditor), (PropertyMetadata) new UIPropertyMetadata((object) 0, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzDGS0nUE\u003D))));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Milliseconds" />.
    ///     </summary>
    public static readonly DependencyProperty MillisecondsProperty = DependencyProperty.Register(nameof(2127279435), typeof (int), typeof (TimeSpanEditor), (PropertyMetadata) new UIPropertyMetadata((object) 0, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzDGS0nUE\u003D))));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Seconds" />.
    ///     </summary>
    public static readonly DependencyProperty SecondsProperty = DependencyProperty.Register(nameof(2127279422), typeof (int), typeof (TimeSpanEditor), (PropertyMetadata) new UIPropertyMetadata((object) 0, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzDGS0nUE\u003D))));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Minutes" />.
    ///     </summary>
    public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register(nameof(2127279404), typeof (int), typeof (TimeSpanEditor), (PropertyMetadata) new UIPropertyMetadata((object) 1, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzDGS0nUE\u003D))));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Hours" />.
    ///     </summary>
    public static readonly DependencyProperty HoursProperty = DependencyProperty.Register(nameof(2127279386), typeof (int), typeof (TimeSpanEditor), (PropertyMetadata) new UIPropertyMetadata((object) 0, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzDGS0nUE\u003D))));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Days" />.
    ///     </summary>
    public static readonly DependencyProperty DaysProperty = DependencyProperty.Register(nameof(2127279382), typeof (int), typeof (TimeSpanEditor), (PropertyMetadata) new UIPropertyMetadata((object) 0, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzDGS0nUE\u003D))));
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Mask" />.
    ///     </summary>
    public static readonly DependencyProperty MaskProperty = DependencyProperty.Register(nameof(2127279361), typeof (TimeSpanEditorMask), typeof (TimeSpanEditor), (PropertyMetadata) new UIPropertyMetadata((object) (TimeSpanEditorMask.Hours | TimeSpanEditorMask.Minutes | TimeSpanEditorMask.Seconds)));
    
    private TextBox \u0023\u003DzVejVA5F4qMnd;
    
    private bool \u0023\u003DzrPQ8KQRm9H8n;
    
    private readonly List<Key> \u0023\u003DzuYjap5XXWazh;
    
    internal TimeSpanEditor \u0023\u003DzFRPJlsE\u003D;
    
    internal CheckBox \u0023\u003DzmIYUjKh5KP3_;
    
    internal TextBox \u0023\u003Dz4IdarG3ffalk;
    
    internal TextBox \u0023\u003Dz\u0024zNZXfhUB4CL;
    
    internal TextBox \u0023\u003DzGHBrGHWkpS1C;
    
    internal TextBox \u0023\u003DzOL\u0024vjSq8v0K6;
    
    internal TextBox \u0023\u003Dz9cciczw6XbW8;
    
    internal TextBox \u0023\u003Dz_M6C3I0qNvuNYFXfDoLZ3vI\u003D;
    
    internal RepeatButton \u0023\u003DzbJOWRUy_w9TJ;
    
    internal RepeatButton \u0023\u003Dz4KN6dB68Pb9H;
    
    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Xaml.TimeSpanEditor" />.
    /// </summary>
    public TimeSpanEditor()
    {
      List<Key> keyList = new List<Key>();
      keyList.Add((Key) 34);
      keyList.Add((Key) 35);
      keyList.Add((Key) 36);
      keyList.Add((Key) 37);
      keyList.Add((Key) 38);
      keyList.Add((Key) 39);
      keyList.Add((Key) 40);
      keyList.Add((Key) 41);
      keyList.Add((Key) 42);
      keyList.Add((Key) 43);
      keyList.Add((Key) 24);
      keyList.Add((Key) 26);
      keyList.Add((Key) 23);
      keyList.Add((Key) 25);
      keyList.Add((Key) 32);
      keyList.Add((Key) 2);
      this.\u0023\u003DzuYjap5XXWazh = keyList;
      // ISSUE: explicit constructor call
      base.\u002Ector();
      this.InitializeComponent();
      this.\u0023\u003DzVejVA5F4qMnd = this.\u0023\u003DzGHBrGHWkpS1C;
    }

    private static void \u0023\u003DzT5MpcJ4\u003D(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
      TimeSpanEditor timeSpanEditor = (TimeSpanEditor) _param0;
      TimeSpan? newValue = (TimeSpan?) ((DependencyPropertyChangedEventArgs) ref _param1).get_NewValue();
      timeSpanEditor.\u0023\u003DzrPQ8KQRm9H8n = true;
      try
      {
        if (newValue.HasValue)
        {
          timeSpanEditor.HasValue = true;
          timeSpanEditor.Seconds = newValue.Value.Seconds;
          timeSpanEditor.Minutes = newValue.Value.Minutes;
          timeSpanEditor.Hours = newValue.Value.Hours;
          timeSpanEditor.Days = newValue.Value.Days;
          timeSpanEditor.Milliseconds = newValue.Value.Milliseconds;
          timeSpanEditor.Microseconds = newValue.Value.GetMicroseconds();
        }
        else
        {
          timeSpanEditor.HasValue = false;
          timeSpanEditor.Seconds = 0;
          timeSpanEditor.Minutes = 0;
          timeSpanEditor.Hours = 0;
          timeSpanEditor.Days = 0;
          timeSpanEditor.Milliseconds = 0;
          timeSpanEditor.Microseconds = 0;
        }
      }
      finally
      {
        timeSpanEditor.\u0023\u003DzrPQ8KQRm9H8n = false;
      }
      Action<TimeSpan?> z6CcXvpw = timeSpanEditor.\u0023\u003Dz6CCXvpw\u003D;
      if (z6CcXvpw == null)
        return;
      z6CcXvpw(newValue);
    }

    /// <summary>
    /// The <see cref="T:System.TimeSpan" /> value.
    /// </summary>
    public TimeSpan? Value
    {
      get
      {
        return (TimeSpan?) this.GetValue(TimeSpanEditor.ValueProperty);
      }
      set
      {
        this.SetValue(TimeSpanEditor.ValueProperty, (object) value);
      }
    }

    /// <summary>Is nullable.</summary>
    public bool IsNullable
    {
      get
      {
        return (bool) this.GetValue(TimeSpanEditor.IsNullableProperty);
      }
      set
      {
        this.SetValue(TimeSpanEditor.IsNullableProperty, (object) value);
      }
    }

    /// <summary>Has value.</summary>
    public bool HasValue
    {
      get
      {
        return (bool) this.GetValue(TimeSpanEditor.HasValueProperty);
      }
      set
      {
        this.SetValue(TimeSpanEditor.HasValueProperty, (object) value);
      }
    }

    /// <summary>Microseconds.</summary>
    public int Microseconds
    {
      get
      {
        return (int) this.GetValue(TimeSpanEditor.MicrosecondsProperty);
      }
      set
      {
        if (value > 999)
          value = 0;
        if (value < 0)
          value = 999;
        this.SetValue(TimeSpanEditor.MicrosecondsProperty, (object) value);
      }
    }

    /// <summary>Milliseconds.</summary>
    public int Milliseconds
    {
      get
      {
        return (int) this.GetValue(TimeSpanEditor.MillisecondsProperty);
      }
      set
      {
        if (value > 999)
          value = 0;
        if (value < 0)
          value = 999;
        this.SetValue(TimeSpanEditor.MillisecondsProperty, (object) value);
      }
    }

    /// <summary>Seconds.</summary>
    public int Seconds
    {
      get
      {
        return (int) this.GetValue(TimeSpanEditor.SecondsProperty);
      }
      set
      {
        if (value > 59)
          value = 0;
        if (value < 0)
          value = 59;
        this.SetValue(TimeSpanEditor.SecondsProperty, (object) value);
      }
    }

    /// <summary>Minutes.</summary>
    public int Minutes
    {
      get
      {
        return (int) this.GetValue(TimeSpanEditor.MinutesProperty);
      }
      set
      {
        if (value > 59)
          value = 0;
        if (value < 0)
          value = 59;
        this.SetValue(TimeSpanEditor.MinutesProperty, (object) value);
      }
    }

    /// <summary>Hours.</summary>
    public int Hours
    {
      get
      {
        return (int) this.GetValue(TimeSpanEditor.HoursProperty);
      }
      set
      {
        if (value > 23)
          value = 0;
        if (value < 0)
          value = 23;
        this.SetValue(TimeSpanEditor.HoursProperty, (object) value);
      }
    }

    /// <summary>The days value.</summary>
    public int Days
    {
      get
      {
        return (int) this.GetValue(TimeSpanEditor.DaysProperty);
      }
      set
      {
        if (value > 364)
          value = 0;
        if (value < 0)
          value = 364;
        this.SetValue(TimeSpanEditor.DaysProperty, (object) value);
      }
    }

    private static void \u0023\u003DzDGS0nUE\u003D(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
      TimeSpanEditor timeSpanEditor = (TimeSpanEditor) _param0;
      if (timeSpanEditor.\u0023\u003DzrPQ8KQRm9H8n)
        return;
      timeSpanEditor.Value = timeSpanEditor.HasValue ? new TimeSpan?(new TimeSpan(timeSpanEditor.Days, timeSpanEditor.Hours, timeSpanEditor.Minutes, timeSpanEditor.Seconds, timeSpanEditor.Milliseconds).AddMicroseconds((long) timeSpanEditor.Microseconds)) : new TimeSpan?();
    }

    /// <summary>Show parts mask.</summary>
    public TimeSpanEditorMask Mask
    {
      get
      {
        return (TimeSpanEditorMask) this.GetValue(TimeSpanEditor.MaskProperty);
      }
      set
      {
        this.SetValue(TimeSpanEditor.MaskProperty, (object) value);
      }
    }

    /// <summary>
    /// The <see cref="P:Ecng.Xaml.TimeSpanEditor.Value" /> changed event.
    /// </summary>
    public event Action<TimeSpan?> ValueChanged;

    private void \u0023\u003Dzn8LY\u0024XRD5qQ4(object _param1, RoutedEventArgs _param2)
    {
      if (this.\u0023\u003DzVejVA5F4qMnd == null)
        return;
      RepeatButton repeatButton = (RepeatButton) _param1;
      string name = this.\u0023\u003DzVejVA5F4qMnd.Name;
      if (!(name == nameof(2127279612)))
      {
        if (!(name == nameof(2127279586)))
        {
          if (!(name == nameof(2127279560)))
          {
            if (!(name == nameof(2127279545)))
            {
              if (!(name == nameof(2127279530)))
              {
                if (!(name == nameof(2127279513)))
                  return;
                if (repeatButton.Name == nameof(2127279511))
                  ++this.Days;
                else
                  --this.Days;
              }
              else if (repeatButton.Name == nameof(2127279511))
                ++this.Hours;
              else
                --this.Hours;
            }
            else if (repeatButton.Name == nameof(2127279511))
              ++this.Minutes;
            else
              --this.Minutes;
          }
          else if (repeatButton.Name == nameof(2127279511))
            ++this.Seconds;
          else
            --this.Seconds;
        }
        else if (repeatButton.Name == nameof(2127279511))
          ++this.Milliseconds;
        else
          --this.Milliseconds;
      }
      else if (repeatButton.Name == nameof(2127279511))
        ++this.Microseconds;
      else
        --this.Microseconds;
    }

    private void \u0023\u003Dz5Ev\u0024ggg\u003D(object _param1, KeyEventArgs _param2)
    {
      if (!this.\u0023\u003DzuYjap5XXWazh.Contains(_param2.Key))
      {
        _param2.Handled = true;
      }
      else
      {
        string name = ((FrameworkElement) _param1).Name;
        if (!(name == nameof(2127279612)))
        {
          if (!(name == nameof(2127279586)))
          {
            if (!(name == nameof(2127279560)))
            {
              if (!(name == nameof(2127279545)))
              {
                if (!(name == nameof(2127279530)))
                {
                  if (!(name == nameof(2127279513)))
                    return;
                  if (_param2.Key == 24)
                    ++this.Days;
                  if (_param2.Key != 26)
                    return;
                  --this.Days;
                }
                else
                {
                  if (_param2.Key == 24)
                    ++this.Hours;
                  if (_param2.Key != 26)
                    return;
                  --this.Hours;
                }
              }
              else
              {
                if (_param2.Key == 24)
                  ++this.Minutes;
                if (_param2.Key != 26)
                  return;
                --this.Minutes;
              }
            }
            else
            {
              if (_param2.Key == 24)
                ++this.Seconds;
              if (_param2.Key != 26)
                return;
              --this.Seconds;
            }
          }
          else
          {
            if (_param2.Key == 24)
              ++this.Milliseconds;
            if (_param2.Key != 26)
              return;
            --this.Milliseconds;
          }
        }
        else
        {
          if (_param2.Key == 24)
            ++this.Microseconds;
          if (_param2.Key != 26)
            return;
          --this.Microseconds;
        }
      }
    }

    private void \u0023\u003DzREn2b\u0024c\u003D(object _param1, TextChangedEventArgs _param2)
    {
      TextBox textBox = (TextBox) _param1;
      string name = textBox.Name;
      if (!(name == nameof(2127279612)))
      {
        if (!(name == nameof(2127279586)))
        {
          if (!(name == nameof(2127279560)))
          {
            if (!(name == nameof(2127279545)))
            {
              if (!(name == nameof(2127279530)))
              {
                if (!(name == nameof(2127279513)))
                  return;
                int num = TimeSpanEditor.\u0023\u003DzvBUOpn3V87tm(this.Days, 364, textBox.Text);
                this.Days = num;
                textBox.Text = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
              }
              else
              {
                int num = TimeSpanEditor.\u0023\u003DzvBUOpn3V87tm(this.Hours, 23, textBox.Text);
                this.Hours = num;
                textBox.Text = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
              }
            }
            else
            {
              int num = TimeSpanEditor.\u0023\u003DzvBUOpn3V87tm(this.Minutes, 59, textBox.Text);
              this.Minutes = num;
              textBox.Text = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
            }
          }
          else
          {
            int num = TimeSpanEditor.\u0023\u003DzvBUOpn3V87tm(this.Seconds, 59, textBox.Text);
            this.Seconds = num;
            textBox.Text = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          }
        }
        else
        {
          int num = TimeSpanEditor.\u0023\u003DzvBUOpn3V87tm(this.Milliseconds, 999, textBox.Text);
          this.Milliseconds = num;
          textBox.Text = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        }
      }
      else
      {
        int num = TimeSpanEditor.\u0023\u003DzvBUOpn3V87tm(this.Microseconds, 999, textBox.Text);
        this.Microseconds = num;
        textBox.Text = num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    private static int \u0023\u003DzvBUOpn3V87tm(int _param0, int _param1, string _param2)
    {
      if (_param2.Length > _param1.ToString().Length)
        return _param0;
      int result;
      if (!int.TryParse(_param2, out result))
        return 0;
      if (result > _param1)
        result = 0;
      if (result < 0)
        result = _param1;
      return result;
    }

    private void \u0023\u003DzwDADti1l4g8_(object _param1, RoutedEventArgs _param2)
    {
      this.\u0023\u003DzVejVA5F4qMnd = (TextBox) _param1;
    }

    private void \u0023\u003Dz2jm2\u0024nOOajiN(object _param1, MouseWheelEventArgs _param2)
    {
      string name = ((FrameworkElement) _param1).Name;
      if (!(name == nameof(2127279612)))
      {
        if (!(name == nameof(2127279586)))
        {
          if (!(name == nameof(2127279560)))
          {
            if (!(name == nameof(2127279545)))
            {
              if (!(name == nameof(2127279530)))
              {
                if (!(name == nameof(2127279513)))
                  return;
                if (_param2.Delta > 0)
                  ++this.Days;
                else
                  --this.Days;
              }
              else if (_param2.Delta > 0)
                ++this.Hours;
              else
                --this.Hours;
            }
            else if (_param2.Delta > 0)
              ++this.Minutes;
            else
              --this.Minutes;
          }
          else if (_param2.Delta > 0)
            ++this.Seconds;
          else
            --this.Seconds;
        }
        else if (_param2.Delta > 0)
          ++this.Milliseconds;
        else
          --this.Milliseconds;
      }
      else if (_param2.Delta > 0)
        ++this.Microseconds;
      else
        --this.Microseconds;
    }

    /// <summary>InitializeComponent</summary>
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    public void InitializeComponent()
    {
      if (this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        return;
      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      Application.LoadComponent((object) this, new Uri(nameof(2127279491), UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    internal Delegate \u0023\u003Dzk_6RQsm5oL9L(Type _param1, string _param2)
    {
      return Delegate.CreateDelegate(_param1, (object) this, _param2);
    }

    [DebuggerNonUserCode]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
      int _param1,
      object _param2)
    {
      switch (_param1)
      {
        case 1:
          this.\u0023\u003DzFRPJlsE\u003D = (TimeSpanEditor) _param2;
          break;
        case 2:
          this.\u0023\u003DzmIYUjKh5KP3_ = (CheckBox) _param2;
          break;
        case 3:
          this.\u0023\u003Dz4IdarG3ffalk = (TextBox) _param2;
          this.\u0023\u003Dz4IdarG3ffalk.PreviewKeyDown += new KeyEventHandler(this.\u0023\u003Dz5Ev\u0024ggg\u003D);
          this.\u0023\u003Dz4IdarG3ffalk.GotFocus += new RoutedEventHandler(this.\u0023\u003DzwDADti1l4g8_);
          this.\u0023\u003Dz4IdarG3ffalk.TextChanged += new TextChangedEventHandler(this.\u0023\u003DzREn2b\u0024c\u003D);
          this.\u0023\u003Dz4IdarG3ffalk.MouseWheel += new MouseWheelEventHandler(this.\u0023\u003Dz2jm2\u0024nOOajiN);
          break;
        case 4:
          this.\u0023\u003Dz\u0024zNZXfhUB4CL = (TextBox) _param2;
          this.\u0023\u003Dz\u0024zNZXfhUB4CL.MouseWheel += new MouseWheelEventHandler(this.\u0023\u003Dz2jm2\u0024nOOajiN);
          this.\u0023\u003Dz\u0024zNZXfhUB4CL.PreviewKeyDown += new KeyEventHandler(this.\u0023\u003Dz5Ev\u0024ggg\u003D);
          this.\u0023\u003Dz\u0024zNZXfhUB4CL.GotFocus += new RoutedEventHandler(this.\u0023\u003DzwDADti1l4g8_);
          this.\u0023\u003Dz\u0024zNZXfhUB4CL.TextChanged += new TextChangedEventHandler(this.\u0023\u003DzREn2b\u0024c\u003D);
          break;
        case 5:
          this.\u0023\u003DzGHBrGHWkpS1C = (TextBox) _param2;
          this.\u0023\u003DzGHBrGHWkpS1C.MouseWheel += new MouseWheelEventHandler(this.\u0023\u003Dz2jm2\u0024nOOajiN);
          this.\u0023\u003DzGHBrGHWkpS1C.PreviewKeyDown += new KeyEventHandler(this.\u0023\u003Dz5Ev\u0024ggg\u003D);
          this.\u0023\u003DzGHBrGHWkpS1C.GotFocus += new RoutedEventHandler(this.\u0023\u003DzwDADti1l4g8_);
          this.\u0023\u003DzGHBrGHWkpS1C.TextChanged += new TextChangedEventHandler(this.\u0023\u003DzREn2b\u0024c\u003D);
          break;
        case 6:
          this.\u0023\u003DzOL\u0024vjSq8v0K6 = (TextBox) _param2;
          this.\u0023\u003DzOL\u0024vjSq8v0K6.MouseWheel += new MouseWheelEventHandler(this.\u0023\u003Dz2jm2\u0024nOOajiN);
          this.\u0023\u003DzOL\u0024vjSq8v0K6.PreviewKeyDown += new KeyEventHandler(this.\u0023\u003Dz5Ev\u0024ggg\u003D);
          this.\u0023\u003DzOL\u0024vjSq8v0K6.GotFocus += new RoutedEventHandler(this.\u0023\u003DzwDADti1l4g8_);
          this.\u0023\u003DzOL\u0024vjSq8v0K6.TextChanged += new TextChangedEventHandler(this.\u0023\u003DzREn2b\u0024c\u003D);
          break;
        case 7:
          this.\u0023\u003Dz9cciczw6XbW8 = (TextBox) _param2;
          this.\u0023\u003Dz9cciczw6XbW8.MouseWheel += new MouseWheelEventHandler(this.\u0023\u003Dz2jm2\u0024nOOajiN);
          this.\u0023\u003Dz9cciczw6XbW8.PreviewKeyDown += new KeyEventHandler(this.\u0023\u003Dz5Ev\u0024ggg\u003D);
          this.\u0023\u003Dz9cciczw6XbW8.GotFocus += new RoutedEventHandler(this.\u0023\u003DzwDADti1l4g8_);
          this.\u0023\u003Dz9cciczw6XbW8.TextChanged += new TextChangedEventHandler(this.\u0023\u003DzREn2b\u0024c\u003D);
          break;
        case 8:
          this.\u0023\u003Dz_M6C3I0qNvuNYFXfDoLZ3vI\u003D = (TextBox) _param2;
          this.\u0023\u003Dz_M6C3I0qNvuNYFXfDoLZ3vI\u003D.MouseWheel += new MouseWheelEventHandler(this.\u0023\u003Dz2jm2\u0024nOOajiN);
          this.\u0023\u003Dz_M6C3I0qNvuNYFXfDoLZ3vI\u003D.PreviewKeyDown += new KeyEventHandler(this.\u0023\u003Dz5Ev\u0024ggg\u003D);
          this.\u0023\u003Dz_M6C3I0qNvuNYFXfDoLZ3vI\u003D.GotFocus += new RoutedEventHandler(this.\u0023\u003DzwDADti1l4g8_);
          this.\u0023\u003Dz_M6C3I0qNvuNYFXfDoLZ3vI\u003D.TextChanged += new TextChangedEventHandler(this.\u0023\u003DzREn2b\u0024c\u003D);
          break;
        case 9:
          this.\u0023\u003DzbJOWRUy_w9TJ = (RepeatButton) _param2;
          this.\u0023\u003DzbJOWRUy_w9TJ.Click += new RoutedEventHandler(this.\u0023\u003Dzn8LY\u0024XRD5qQ4);
          break;
        case 10:
          this.\u0023\u003Dz4KN6dB68Pb9H = (RepeatButton) _param2;
          this.\u0023\u003Dz4KN6dB68Pb9H.Click += new RoutedEventHandler(this.\u0023\u003Dzn8LY\u0024XRD5qQ4);
          break;
        default:
          this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
          break;
      }
    }
  }
}
