// Decompiled with JetBrains decompiler
// Type: #=zS5mFHV$eXnkCjzbt0Dx26vgE1y_16$Ql8QLBV6E=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

#nullable enable
public sealed class MouseManager : 
  IMouseManager
{
  public static readonly 
  #nullable disable
  DependencyProperty \u0023\u003Dzsbmsx7t0O6Lr = DependencyProperty.RegisterAttached("MouseEventGroup", typeof (string), typeof (MouseManager), new PropertyMetadata((object) null, new PropertyChangedCallback(MouseManager.\u0023\u003DzlYzeJT9HprfL)));
  private static readonly IDictionary<string, IList<IReceiveMouseEvents >> \u0023\u003DzcSUkDEet8fvl = (IDictionary<string, IList<IReceiveMouseEvents >>) new Dictionary<string, IList<IReceiveMouseEvents >>();
  private readonly IDictionary<IReceiveMouseEvents , \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTc1AR\u0024_Dkj9GLXUtdlw\u003D> \u0023\u003DzgeoglfeTtj59KhwCiNbQIv4\u003D = (IDictionary<IReceiveMouseEvents , \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTc1AR\u0024_Dkj9GLXUtdlw\u003D>) new Dictionary<IReceiveMouseEvents , \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTc1AR\u0024_Dkj9GLXUtdlw\u003D>();
  private readonly IDictionary<IReceiveMouseEvents , IPublishMouseEvents> \u0023\u003DzPmsN62CuaNQ5kbur20HKcgQ\u003D = (IDictionary<IReceiveMouseEvents , IPublishMouseEvents>) new Dictionary<IReceiveMouseEvents , IPublishMouseEvents>();
  private readonly IDictionary<object, Point> \u0023\u003DzPyqsKFbJsmS\u0024 = (IDictionary<object, Point>) new Dictionary<object, Point>();
  private DateTime \u0023\u003Dzm3u5L\u0024oPO_BN;
  private Point \u0023\u003DzFKi\u0024U\u0024DDFLIq;
  private \u0023\u003DzmAi_JN5raoSBYo9w2IEI_6oXJj6XJW84s7KCJRXjmNCv \u0023\u003DzRVmhoRM9XqGEVYcGQ0iUEic\u003D;
  private \u0023\u003DzJSYuf46gmaJ4ENA0KiaGx2h\u0024Ya9PCybhAP7KOmKvCUmh \u0023\u003DzxtFxjlXMliXLsTTuZoMzF38\u003D;

  public MouseManager()
  {
    this.\u0023\u003DzjAp2mS0rML6_((\u0023\u003DzmAi_JN5raoSBYo9w2IEI_6oXJj6XJW84s7KCJRXjmNCv) new \u0023\u003DzK74oGPE3yyB7zop8uDdzn9Sw3N5UqqpJcA8Ymt7sjac\u0024());
    this.\u0023\u003DzqG472mq10NeC((\u0023\u003DzJSYuf46gmaJ4ENA0KiaGx2h\u0024Ya9PCybhAP7KOmKvCUmh) new \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024tfRr1aHqDMq8Hw4E7fv0Hml());
  }

  [DllImport("user32.dll", EntryPoint = "GetDoubleClickTime")]
  private static extern uint dje_z23CSE6TFTRDAFJZ_ejd();

  public static void SetMouseEventGroup(DependencyObject _param0, string _param1)
  {
    _param0.SetValue(MouseManager.\u0023\u003Dzsbmsx7t0O6Lr, (object) _param1);
  }

  public static string GetMouseEventGroup(DependencyObject _param0)
  {
    return (string) _param0.GetValue(MouseManager.\u0023\u003Dzsbmsx7t0O6Lr);
  }

  public void AddPropertyEvents(
    IPublishMouseEvents _param1,
    IReceiveMouseEvents  _param2)
  {
    MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D dop2SzA2WchXh2wc = new MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D();
    dop2SzA2WchXh2wc._variableSome3535 = this;
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D = _param1;
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D, "element");
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param2, "receiveMouseEvents");
    this.RemovePropertyEvents(_param2);
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_ = new \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTc1AR\u0024_Dkj9GLXUtdlw\u003D();
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003Dz9koqASE\u003D(_param2);
    this.\u0023\u003DzXdSN34o\u003D(_param2);
    dop2SzA2WchXh2wc.\u0023\u003DzsBPNicmZmzb8 = new Action<object, TouchManipulationEventArgs, Action<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf, IReceiveMouseEvents , bool>>(dop2SzA2WchXh2wc.\u0023\u003DzBtRRdr52eoJt6EWpBA\u003D\u003D);
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003Dz4OcwbE_U1AbJvhWqNg\u003D\u003D(new EventHandler<TouchManipulationEventArgs>(dop2SzA2WchXh2wc.\u0023\u003DzKOy9SUIIrqQcIPL95A\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzleK_91fIDhGr(new EventHandler<TouchManipulationEventArgs>(dop2SzA2WchXh2wc.\u0023\u003DzcqqPjA6Dqci6x1tXwQ\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003Dz8YKyYO2kS4_3fMiuIQ\u003D\u003D(new EventHandler<TouchManipulationEventArgs>(dop2SzA2WchXh2wc.\u0023\u003DzD2qDzNj7gd61W9WmDA\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzyNsxNb\u0024Yoa0guHoeTA\u003D\u003D(new MouseButtonEventHandler(dop2SzA2WchXh2wc.\u0023\u003Dzz5fp6K8uMqseHnw\u0024UQ\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzzsBUzNJFjk1zjMASlw\u003D\u003D(new MouseButtonEventHandler(dop2SzA2WchXh2wc.\u0023\u003DzM8sFm9kPnycJtnC_Xg\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzTE9vIAKILVUt(new MouseEventHandler(dop2SzA2WchXh2wc.\u0023\u003DzzqIDni8VJCH37noJWg\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzFA9hHjxwlCNgnK2SGg\u003D\u003D(new MouseButtonEventHandler(dop2SzA2WchXh2wc.\u0023\u003DzZnnbh0m5An\u0024F1Uadug\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzZsZJ4xCgsAvD2JozIA\u003D\u003D(new MouseButtonEventHandler(dop2SzA2WchXh2wc.\u0023\u003Dz5fPIvWoN_1Tou0ljEQ\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003Dzq_lY_c\u0024vUyJTJkX80g\u003D\u003D(new MouseWheelEventHandler(dop2SzA2WchXh2wc.\u0023\u003DzdvuVqJx4Gky8\u0024mb6Zg\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzR5\u0024i5qyM\u0024aBKl_\u0024HKA\u003D\u003D(new MouseEventHandler(dop2SzA2WchXh2wc.\u0023\u003Dz3KMXnZMECV2OkUL8vw\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003Dz7eLmi8miZQ\u0024qwq776g\u003D\u003D(new MouseButtonEventHandler(dop2SzA2WchXh2wc.\u0023\u003DzTKjwLjWI6l9WjUYx9g\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzJo3xvx55uPozz862HA\u003D\u003D(new MouseButtonEventHandler(dop2SzA2WchXh2wc.\u0023\u003DzOKxMOPGH8TAVrjNYXw\u003D\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D.\u0023\u003DzD\u0024VBwMxDvQPr(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzbUyNA2FMHdRJQ1YZgg\u003D\u003D());
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D.\u0023\u003DzMGZAEi71Lpw4(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003Dzv00OH5Rhvucf());
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D.\u0023\u003DzHc1yVlkYVP_y(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzF\u0024gb6hCHsepLUXsfOA\u003D\u003D());
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D.add_MouseLeftButtonDown(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzpNQQUaDN\u0024tAX1ByfQA\u003D\u003D());
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D.add_MouseLeftButtonUp(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzuIqRk2OL1WmOPdXoUw\u003D\u003D());
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003Dz4e25HYK1p2WX(new \u0023\u003Dz3uoqT9PJZU9sx1O75XaUu\u0024W0gdZzFkccR0pTuu84OEtC(dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D));
    dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzRmjYaUJz_hkD().\u0023\u003Dz5v7amar0n_e8(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzBezB0J6YCKba());
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D.add_MouseRightButtonDown(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzHeNmN_zIc9mg6dRRAw\u003D\u003D());
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D.add_MouseRightButtonUp(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzlMGv\u0024SGoMjVaw4GVjg\u003D\u003D());
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D.add_MouseWheel(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003Dz1itw_W1a_AF7HX0UHw\u003D\u003D());
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D.add_MouseLeave(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003Dzg9HW45XXbGBbRNumZw\u003D\u003D());
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D.add_MouseMiddleButtonDown(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003Dzdn3_myXo13c6x3a1OQ\u003D\u003D());
    dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D.add_MouseMiddleButtonUp(dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzsLqMCtBM6Gbi\u0024EKqAw\u003D\u003D());
    this.\u0023\u003DzneYoHAvBJ0sq().Add(_param2, dop2SzA2WchXh2wc.\u0023\u003DzqvJkfbGyJf\u0024_);
    this.\u0023\u003DzPmsN62CuaNQ5kbur20HKcgQ\u003D.Add(_param2, dop2SzA2WchXh2wc.\u0023\u003DzL2OrHlw\u003D);
  }

  private Point \u0023\u003DzBp9oXZI\u003D(
    IPublishMouseEvents _param1,
    MouseEventArgs _param2)
  {
    return this.\u0023\u003DzUpBQCesOHau3().\u0023\u003DzBp9oXZI\u003D(_param1, _param2);
  }

  private TouchPointCollection \u0023\u003DzBp9oXZI\u003D(
    IPublishMouseEvents _param1,
    TouchFrameEventArgs _param2)
  {
    return this.\u0023\u003DzrUC99bjY9Cf_().\u0023\u003DzBp9oXZI\u003D(_param1, _param2);
  }

  private void \u0023\u003DzXdSN34o\u003D(
    IReceiveMouseEvents  _param1)
  {
    if (!(_param1 is DependencyObject dependencyObject))
      return;
    string str = (string) dependencyObject.GetValue(MouseManager.\u0023\u003Dzsbmsx7t0O6Lr);
    dependencyObject.SetCurrentValue(MouseManager.\u0023\u003Dzsbmsx7t0O6Lr, (object) string.Empty);
    dependencyObject.SetCurrentValue(MouseManager.\u0023\u003Dzsbmsx7t0O6Lr, (object) str);
  }

  public void RemovePropertyEvents(
    IPublishMouseEvents _param1)
  {
    if (_param1 == null)
      return;
    List<IReceiveMouseEvents > ag4ZlfwSgT7i2ApwList = new List<IReceiveMouseEvents >();
    foreach (KeyValuePair<IReceiveMouseEvents , \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTc1AR\u0024_Dkj9GLXUtdlw\u003D> keyValuePair in (IEnumerable<KeyValuePair<IReceiveMouseEvents , \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTc1AR\u0024_Dkj9GLXUtdlw\u003D>>) this.\u0023\u003DzneYoHAvBJ0sq())
    {
      if (this.\u0023\u003DzPmsN62CuaNQ5kbur20HKcgQ\u003D[keyValuePair.Key] == _param1)
      {
        \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTc1AR\u0024_Dkj9GLXUtdlw\u003D tc1ArDkj9GlxUtdlw = keyValuePair.Value;
        _param1.remove_MouseLeftButtonDown(tc1ArDkj9GlxUtdlw.\u0023\u003DzpNQQUaDN\u0024tAX1ByfQA\u003D\u003D());
        _param1.remove_MouseLeftButtonUp(tc1ArDkj9GlxUtdlw.\u0023\u003DzuIqRk2OL1WmOPdXoUw\u003D\u003D());
        _param1.remove_MouseMove(tc1ArDkj9GlxUtdlw.\u0023\u003DzBezB0J6YCKba());
        _param1.remove_MouseRightButtonDown(tc1ArDkj9GlxUtdlw.\u0023\u003DzHeNmN_zIc9mg6dRRAw\u003D\u003D());
        _param1.remove_MouseRightButtonUp(tc1ArDkj9GlxUtdlw.\u0023\u003DzlMGv\u0024SGoMjVaw4GVjg\u003D\u003D());
        _param1.remove_MouseWheel(tc1ArDkj9GlxUtdlw.\u0023\u003Dz1itw_W1a_AF7HX0UHw\u003D\u003D());
        _param1.remove_MouseLeave(tc1ArDkj9GlxUtdlw.\u0023\u003Dzg9HW45XXbGBbRNumZw\u003D\u003D());
        _param1.remove_MouseMiddleButtonDown(tc1ArDkj9GlxUtdlw.\u0023\u003Dzdn3_myXo13c6x3a1OQ\u003D\u003D());
        _param1.remove_MouseMiddleButtonUp(tc1ArDkj9GlxUtdlw.\u0023\u003DzsLqMCtBM6Gbi\u0024EKqAw\u003D\u003D());
        foreach (string key in (IEnumerable<string>) MouseManager.\u0023\u003DzcSUkDEet8fvl.Keys)
        {
          if (MouseManager.\u0023\u003DzcSUkDEet8fvl[key].Contains(tc1ArDkj9GlxUtdlw.\u0023\u003DzY2vUSRo\u003D()))
            MouseManager.\u0023\u003DzcSUkDEet8fvl[key].Remove(tc1ArDkj9GlxUtdlw.\u0023\u003DzY2vUSRo\u003D());
        }
        tc1ArDkj9GlxUtdlw.\u0023\u003Dz9koqASE\u003D((IReceiveMouseEvents ) null);
        tc1ArDkj9GlxUtdlw.\u0023\u003DzRmjYaUJz_hkD().Dispose();
        tc1ArDkj9GlxUtdlw.\u0023\u003Dz4e25HYK1p2WX((\u0023\u003Dz3uoqT9PJZU9sx1O75XaUu\u0024W0gdZzFkccR0pTuu84OEtC) null);
        ag4ZlfwSgT7i2ApwList.Add(keyValuePair.Key);
      }
    }
    foreach (IReceiveMouseEvents  key in ag4ZlfwSgT7i2ApwList)
    {
      this.\u0023\u003DzneYoHAvBJ0sq().Remove(key);
      this.\u0023\u003DzPmsN62CuaNQ5kbur20HKcgQ\u003D.Remove(key);
    }
  }

  public void RemovePropertyEvents(
    IReceiveMouseEvents  _param1)
  {
    if (_param1 == null || !this.\u0023\u003DzPmsN62CuaNQ5kbur20HKcgQ\u003D.ContainsKey(_param1))
      return;
    \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTc1AR\u0024_Dkj9GLXUtdlw\u003D tc1ArDkj9GlxUtdlw = this.\u0023\u003DzneYoHAvBJ0sq()[_param1];
    IPublishMouseEvents xwlnLqBsgQeCuZnV = this.\u0023\u003DzPmsN62CuaNQ5kbur20HKcgQ\u003D[_param1];
    xwlnLqBsgQeCuZnV.remove_MouseLeftButtonDown(tc1ArDkj9GlxUtdlw.\u0023\u003DzpNQQUaDN\u0024tAX1ByfQA\u003D\u003D());
    xwlnLqBsgQeCuZnV.remove_MouseLeftButtonUp(tc1ArDkj9GlxUtdlw.\u0023\u003DzuIqRk2OL1WmOPdXoUw\u003D\u003D());
    xwlnLqBsgQeCuZnV.remove_MouseMove(tc1ArDkj9GlxUtdlw.\u0023\u003DzBezB0J6YCKba());
    xwlnLqBsgQeCuZnV.remove_MouseRightButtonDown(tc1ArDkj9GlxUtdlw.\u0023\u003DzHeNmN_zIc9mg6dRRAw\u003D\u003D());
    xwlnLqBsgQeCuZnV.remove_MouseRightButtonUp(tc1ArDkj9GlxUtdlw.\u0023\u003DzlMGv\u0024SGoMjVaw4GVjg\u003D\u003D());
    xwlnLqBsgQeCuZnV.remove_MouseWheel(tc1ArDkj9GlxUtdlw.\u0023\u003Dz1itw_W1a_AF7HX0UHw\u003D\u003D());
    xwlnLqBsgQeCuZnV.remove_MouseLeave(tc1ArDkj9GlxUtdlw.\u0023\u003Dzg9HW45XXbGBbRNumZw\u003D\u003D());
    xwlnLqBsgQeCuZnV.remove_MouseMiddleButtonDown(tc1ArDkj9GlxUtdlw.\u0023\u003Dzdn3_myXo13c6x3a1OQ\u003D\u003D());
    xwlnLqBsgQeCuZnV.remove_MouseMiddleButtonUp(tc1ArDkj9GlxUtdlw.\u0023\u003DzsLqMCtBM6Gbi\u0024EKqAw\u003D\u003D());
    foreach (string key in (IEnumerable<string>) MouseManager.\u0023\u003DzcSUkDEet8fvl.Keys)
    {
      if (MouseManager.\u0023\u003DzcSUkDEet8fvl[key].Contains(tc1ArDkj9GlxUtdlw.\u0023\u003DzY2vUSRo\u003D()))
        MouseManager.\u0023\u003DzcSUkDEet8fvl[key].Remove(tc1ArDkj9GlxUtdlw.\u0023\u003DzY2vUSRo\u003D());
    }
    tc1ArDkj9GlxUtdlw.\u0023\u003Dz9koqASE\u003D((IReceiveMouseEvents ) null);
    tc1ArDkj9GlxUtdlw.\u0023\u003DzRmjYaUJz_hkD().Dispose();
    tc1ArDkj9GlxUtdlw.\u0023\u003Dz4e25HYK1p2WX((\u0023\u003Dz3uoqT9PJZU9sx1O75XaUu\u0024W0gdZzFkccR0pTuu84OEtC) null);
    this.\u0023\u003DzneYoHAvBJ0sq().Remove(_param1);
    this.\u0023\u003DzPmsN62CuaNQ5kbur20HKcgQ\u003D.Remove(_param1);
  }

  private static void \u0023\u003DzlYzeJT9HprfL(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    IReceiveMouseEvents  ag4ZlfwSgT7i2Apw = (IReceiveMouseEvents ) _param0;
    if (_param1.OldValue is string oldValue && MouseManager.\u0023\u003DzcSUkDEet8fvl.ContainsKey(oldValue))
      MouseManager.\u0023\u003DzcSUkDEet8fvl[oldValue].Remove(ag4ZlfwSgT7i2Apw);
    string newValue = _param1.NewValue as string;
    if (string.IsNullOrEmpty(newValue))
    {
      ag4ZlfwSgT7i2Apw.set_MouseEventGroup((string) null);
    }
    else
    {
      if (!MouseManager.\u0023\u003DzcSUkDEet8fvl.ContainsKey(newValue))
        MouseManager.\u0023\u003DzcSUkDEet8fvl[newValue] = (IList<IReceiveMouseEvents >) new List<IReceiveMouseEvents >();
      MouseManager.\u0023\u003DzcSUkDEet8fvl[newValue].Add(ag4ZlfwSgT7i2Apw);
      ag4ZlfwSgT7i2Apw.set_MouseEventGroup(newValue);
    }
  }

  private static void \u0023\u003DzW4ZNNlNDLXpa(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param0,
    IReceiveMouseEvents  _param1,
    bool _param2)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Raising {0}.OnModifierManipulationStarted", new object[1]
    {
      (object) _param1.GetType().Name
    });
    _param0.\u0023\u003Dz3P2dgUJ\u0024csBH(_param2);
    _param1.\u0023\u003Dz0yya794Z8OaI(_param0);
  }

  private static void \u0023\u003DzgBSk0UBueez\u0024(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param0,
    IReceiveMouseEvents  _param1,
    bool _param2)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Raising {0}.OnModifierManipulationDelta", new object[1]
    {
      (object) _param1.GetType().Name
    });
    _param0.\u0023\u003Dz3P2dgUJ\u0024csBH(_param2);
    _param1.\u0023\u003DzpmQpuKvOtHIk(_param0);
  }

  private static void \u0023\u003Dzg_TIY4pICjHa(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param0,
    IReceiveMouseEvents  _param1,
    bool _param2)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Raising {0}.OnModifierManipulationCompleted", new object[1]
    {
      (object) _param1.GetType().Name
    });
    _param0.\u0023\u003Dz3P2dgUJ\u0024csBH(_param2);
    _param1.\u0023\u003DzsSwjrBzrsGPJ(_param0);
  }

  private static void \u0023\u003Dzvn1sWKwdu6R4(
    ModifierMouseArgs _param0,
    IReceiveMouseEvents  _param1,
    bool _param2)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Raising {0}.OnModifierMouseDown", new object[1]
    {
      (object) _param1.GetType().Name
    });
    _param0.\u0023\u003Dz3P2dgUJ\u0024csBH(_param2);
    _param1.OnModifierMouseDown(_param0);
  }

  private void \u0023\u003DzzGr9tanIYzso(
    ModifierMouseArgs _param1,
    IReceiveMouseEvents  _param2,
    bool _param3)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Raising {0}.OnModifierMouseUp", new object[1]
    {
      (object) _param2.GetType().Name
    });
    _param1.\u0023\u003Dz3P2dgUJ\u0024csBH(_param3);
    _param2.OnModifierMouseUp(_param1);
  }

  private void \u0023\u003Dzzk9Obv44lrydFcInoQ\u003D\u003D(
    ModifierMouseArgs _param1,
    IReceiveMouseEvents  _param2,
    bool _param3)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Raising {0}.OnModifierDoubleClick", new object[1]
    {
      (object) _param2.GetType().Name
    });
    _param1.\u0023\u003Dz3P2dgUJ\u0024csBH(_param3);
    _param2.\u0023\u003Dz5y8F1YNwkhnW(_param1);
    this.\u0023\u003Dzm3u5L\u0024oPO_BN = DateTime.MinValue;
    this.\u0023\u003DzFKi\u0024U\u0024DDFLIq = new Point(-1.0, -1.0);
  }

  private void \u0023\u003DzuGCrm3cpH5KI(
    ModifierMouseArgs _param1,
    IReceiveMouseEvents  _param2,
    bool _param3)
  {
    _param1.\u0023\u003Dz3P2dgUJ\u0024csBH(_param3);
    _param2.OnModifierMouseMove(_param1);
  }

  private void \u0023\u003Dz6ICnkaskXJ9U(
    ModifierMouseArgs _param1,
    IReceiveMouseEvents  _param2,
    bool _param3)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Raising {0}.OnModifierMouseLeave", new object[1]
    {
      (object) _param2.GetType().Name
    });
    _param1.\u0023\u003Dz3P2dgUJ\u0024csBH(_param3);
    _param2.OnMasterMouseLeave(_param1);
  }

  private void \u0023\u003Dzs9W6JdpZ_79\u0024(
    ModifierMouseArgs _param1,
    IReceiveMouseEvents  _param2,
    bool _param3)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Raising {0}.OnMasterMouseLeave", new object[1]
    {
      (object) _param2.GetType().Name
    });
    _param1.\u0023\u003Dz3P2dgUJ\u0024csBH(_param3);
    _param2.\u0023\u003DzQTINWhMByBmJ(_param1);
  }

  public IEnumerable<IReceiveMouseEvents > \u0023\u003Dzwp8c1z8\u003D(
    IReceiveMouseEvents  _param1)
  {
    IEnumerable<IReceiveMouseEvents > ag4ZlfwSgT7i2Apws = Enumerable.Empty<IReceiveMouseEvents >();
    if (_param1 != null)
    {
      if (_param1.get_MouseEventGroup() == null)
      {
        if (_param1.\u0023\u003Dzo7mdr1Y1DFNe())
          ag4ZlfwSgT7i2Apws = (IEnumerable<IReceiveMouseEvents >) new IReceiveMouseEvents [1]
          {
            _param1
          };
      }
      else
        ag4ZlfwSgT7i2Apws = MouseManager.\u0023\u003DzcSUkDEet8fvl[_param1.get_MouseEventGroup()].Where<IReceiveMouseEvents >(MouseManager.SomeClass34343383.\u0023\u003DzgXbxBZLvg_0J\u0024EPUjg\u003D\u003D ?? (MouseManager.SomeClass34343383.\u0023\u003DzgXbxBZLvg_0J\u0024EPUjg\u003D\u003D = new Func<IReceiveMouseEvents , bool>(MouseManager.SomeClass34343383.SomeMethond0343.\u0023\u003DzRB7XSdoQ2eSQJafyp0e69aQ\u003D)));
    }
    return ag4ZlfwSgT7i2Apws;
  }

  public \u0023\u003DzmAi_JN5raoSBYo9w2IEI_6oXJj6XJW84s7KCJRXjmNCv \u0023\u003DzUpBQCesOHau3()
  {
    return this.\u0023\u003DzRVmhoRM9XqGEVYcGQ0iUEic\u003D;
  }

  public void \u0023\u003DzjAp2mS0rML6_(
    \u0023\u003DzmAi_JN5raoSBYo9w2IEI_6oXJj6XJW84s7KCJRXjmNCv _param1)
  {
    this.\u0023\u003DzRVmhoRM9XqGEVYcGQ0iUEic\u003D = _param1;
  }

  public \u0023\u003DzJSYuf46gmaJ4ENA0KiaGx2h\u0024Ya9PCybhAP7KOmKvCUmh \u0023\u003DzrUC99bjY9Cf_()
  {
    return this.\u0023\u003DzxtFxjlXMliXLsTTuZoMzF38\u003D;
  }

  public void \u0023\u003DzqG472mq10NeC(
    \u0023\u003DzJSYuf46gmaJ4ENA0KiaGx2h\u0024Ya9PCybhAP7KOmKvCUmh _param1)
  {
    this.\u0023\u003DzxtFxjlXMliXLsTTuZoMzF38\u003D = _param1;
  }

  public IDictionary<IReceiveMouseEvents , \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTc1AR\u0024_Dkj9GLXUtdlw\u003D> \u0023\u003DzneYoHAvBJ0sq()
  {
    return this.\u0023\u003DzgeoglfeTtj59KhwCiNbQIv4\u003D;
  }

  public IDictionary<object, Point> \u0023\u003Dzs1I8bOaKHdCgrtnv7Q\u003D\u003D()
  {
    return this.\u0023\u003DzPyqsKFbJsmS\u0024;
  }

  public IDictionary<string, IList<IReceiveMouseEvents >> \u0023\u003Dz8gAksnYQptna()
  {
    return MouseManager.\u0023\u003DzcSUkDEet8fvl;
  }

  public IDictionary<IReceiveMouseEvents , IPublishMouseEvents> \u0023\u003Dzk7_2WsyQK3mJ()
  {
    return this.\u0023\u003DzPmsN62CuaNQ5kbur20HKcgQ\u003D;
  }

  private sealed class \u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D
  {
    public MouseManager _variableSome3535;
    public \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTc1AR\u0024_Dkj9GLXUtdlw\u003D \u0023\u003DzqvJkfbGyJf\u0024_;
    public Action<object, TouchManipulationEventArgs, Action<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf, IReceiveMouseEvents , bool>> \u0023\u003DzsBPNicmZmzb8;
    public IPublishMouseEvents \u0023\u003DzL2OrHlw\u003D;

    public void \u0023\u003DzBtRRdr52eoJt6EWpBA\u003D\u003D(
      object _param1,
      TouchManipulationEventArgs _param2,
      Action<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf, IReceiveMouseEvents , bool> _param3)
    {
      MouseManager.\u0023\u003Dzx7SG8UTBOy2shIbrNVKi5DE\u003D utbOy2shIbrNvKi5De = new MouseManager.\u0023\u003Dzx7SG8UTBOy2shIbrNVKi5DE\u003D();
      utbOy2shIbrNvKi5De.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D = this;
      utbOy2shIbrNvKi5De.\u0023\u003DzaY_8iBE\u003D = _param3;
      IEnumerable<IReceiveMouseEvents > ag4ZlfwSgT7i2Apws = this._variableSome3535.\u0023\u003Dzwp8c1z8\u003D(this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      utbOy2shIbrNvKi5De.\u0023\u003DzTi2kmf4\u003D = new \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf(_param2.\u0023\u003DzJXMryAEkbm8q(), true, this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      Action<IReceiveMouseEvents > action = new Action<IReceiveMouseEvents >(utbOy2shIbrNvKi5De.\u0023\u003Dz9O7cRibjOuApTwUuGA\u003D\u003D);
      ag4ZlfwSgT7i2Apws.\u0023\u003Dz30RSSSygABj_<IReceiveMouseEvents >(action);
      _param2.Handled(utbOy2shIbrNvKi5De.\u0023\u003DzTi2kmf4\u003D.\u0023\u003Dz882B0y3Ue8fl());
    }

    public void \u0023\u003DzKOy9SUIIrqQcIPL95A\u003D\u003D(
      #nullable enable
      object? _param1,
      #nullable disable
      TouchManipulationEventArgs _param2)
    {
      this.\u0023\u003DzsBPNicmZmzb8(_param1, _param2, MouseManager.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003Dzc8pv9GVxdmtKAl\u00241BQ\u003D\u003D ?? (MouseManager.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003Dzc8pv9GVxdmtKAl\u00241BQ\u003D\u003D = new Action<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf, IReceiveMouseEvents , bool>(MouseManager.\u0023\u003DzW4ZNNlNDLXpa)));
    }

    public void \u0023\u003DzcqqPjA6Dqci6x1tXwQ\u003D\u003D(
      #nullable enable
      object? _param1,
      #nullable disable
      TouchManipulationEventArgs _param2)
    {
      this.\u0023\u003DzsBPNicmZmzb8(_param1, _param2, MouseManager.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzfA7Srqxog6G14gu2Jw\u003D\u003D ?? (MouseManager.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzfA7Srqxog6G14gu2Jw\u003D\u003D = new Action<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf, IReceiveMouseEvents , bool>(MouseManager.\u0023\u003DzgBSk0UBueez\u0024)));
    }

    public void \u0023\u003DzD2qDzNj7gd61W9WmDA\u003D\u003D(
      #nullable enable
      object? _param1,
      #nullable disable
      TouchManipulationEventArgs _param2)
    {
      this.\u0023\u003DzsBPNicmZmzb8(_param1, _param2, MouseManager.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUtAWjYDhFPs8QavWmQ\u003D\u003D ?? (MouseManager.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUtAWjYDhFPs8QavWmQ\u003D\u003D = new Action<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf, IReceiveMouseEvents , bool>(MouseManager.\u0023\u003Dzg_TIY4pICjHa)));
    }

    public void \u0023\u003Dzz5fp6K8uMqseHnw\u0024UQ\u003D\u003D(
      object _param1,
      MouseButtonEventArgs _param2)
    {
      MouseManager.\u0023\u003DzE4CEJXjA93amsG7Qqp_sMKQ\u003D xjA93amsG7QqpSMkq = new MouseManager.\u0023\u003DzE4CEJXjA93amsG7Qqp_sMKQ\u003D();
      xjA93amsG7QqpSMkq.\u0023\u003Dq9oPAsd_XX7A0iOT0n\u0024XoJRH5BDywfM7Sl5FCgaUG6WI\u003D = this;
      Point point = this._variableSome3535.\u0023\u003DzBp9oXZI\u003D(this.\u0023\u003DzL2OrHlw\u003D, (MouseEventArgs) _param2);
      IEnumerable<IReceiveMouseEvents > ag4ZlfwSgT7i2Apws = this._variableSome3535.\u0023\u003Dzwp8c1z8\u003D(this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      xjA93amsG7QqpSMkq.\u0023\u003DzTi2kmf4\u003D = new ModifierMouseArgs(point, (MouseButtons) 1, \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk(), true, this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      TimeSpan timeSpan = DateTime.UtcNow - this._variableSome3535.\u0023\u003Dzm3u5L\u0024oPO_BN;
      if (timeSpan < TimeSpan.FromMilliseconds((double) MouseManager.dje_z23CSE6TFTRDAFJZ_ejd()) && timeSpan > TimeSpan.FromMilliseconds(1.0) && \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzwAiTZQA\u003D(point, this._variableSome3535.\u0023\u003DzFKi\u0024U\u0024DDFLIq) < 5.0)
      {
        ag4ZlfwSgT7i2Apws.\u0023\u003Dz30RSSSygABj_<IReceiveMouseEvents >(new Action<IReceiveMouseEvents >(xjA93amsG7QqpSMkq.\u0023\u003DzWAuFYQDtZFB9v3VPYQ\u003D\u003D));
      }
      else
      {
        ag4ZlfwSgT7i2Apws.\u0023\u003Dz30RSSSygABj_<IReceiveMouseEvents >(new Action<IReceiveMouseEvents >(xjA93amsG7QqpSMkq.\u0023\u003DzUxQ2tKFJyEjKZs3bnA\u003D\u003D));
        _param2.Handled = xjA93amsG7QqpSMkq.\u0023\u003DzTi2kmf4\u003D.\u0023\u003Dz882B0y3Ue8fl();
        this._variableSome3535.\u0023\u003Dzm3u5L\u0024oPO_BN = DateTime.UtcNow;
        this._variableSome3535.\u0023\u003DzFKi\u0024U\u0024DDFLIq = point;
      }
    }

    public void \u0023\u003DzM8sFm9kPnycJtnC_Xg\u003D\u003D(
      object _param1,
      MouseButtonEventArgs _param2)
    {
      MouseManager.\u0023\u003DzOVbvGyAUlOlSG92isdmlY08\u003D aulOlSg92isdmlY08 = new MouseManager.\u0023\u003DzOVbvGyAUlOlSG92isdmlY08\u003D();
      aulOlSg92isdmlY08.\u0023\u003DqDGS82oTBZ\u0024CkKsNfUVM9XxR6WPEu4uyPJgSIUBBKyWs\u003D = this;
      Point point = this._variableSome3535.\u0023\u003DzBp9oXZI\u003D(this.\u0023\u003DzL2OrHlw\u003D, (MouseEventArgs) _param2);
      IEnumerable<IReceiveMouseEvents > ag4ZlfwSgT7i2Apws = this._variableSome3535.\u0023\u003Dzwp8c1z8\u003D(this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      aulOlSg92isdmlY08.\u0023\u003DzTi2kmf4\u003D = new ModifierMouseArgs(point, (MouseButtons) 1, \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk(), true, this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      Action<IReceiveMouseEvents > action = new Action<IReceiveMouseEvents >(aulOlSg92isdmlY08.\u0023\u003Dzi3lac9zHQwZ94EQd\u0024g\u003D\u003D);
      ag4ZlfwSgT7i2Apws.\u0023\u003Dz30RSSSygABj_<IReceiveMouseEvents >(action);
      _param2.Handled = aulOlSg92isdmlY08.\u0023\u003DzTi2kmf4\u003D.\u0023\u003Dz882B0y3Ue8fl();
    }

    public void \u0023\u003DzzqIDni8VJCH37noJWg\u003D\u003D(
      object _param1,
      MouseEventArgs _param2)
    {
      MouseManager.\u0023\u003DzyQni\u0024go7q3hZ_gnypm8UBRs\u003D go7q3hZGnypm8UbRs = new MouseManager.\u0023\u003DzyQni\u0024go7q3hZ_gnypm8UBRs\u003D();
      go7q3hZGnypm8UbRs.\u0023\u003DqtAzlqurTc5We1ZAU3ovVrx43Z68HZcrCMXu4\u0024g\u002481qA\u003D = this;
      Point point = this._variableSome3535.\u0023\u003DzBp9oXZI\u003D(this.\u0023\u003DzL2OrHlw\u003D, _param2);
      IEnumerable<IReceiveMouseEvents > ag4ZlfwSgT7i2Apws = this._variableSome3535.\u0023\u003Dzwp8c1z8\u003D(this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      go7q3hZGnypm8UbRs.\u0023\u003DzTi2kmf4\u003D = new ModifierMouseArgs(point, (MouseButtons) 0, \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk(), true, this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      Action<IReceiveMouseEvents > action = new Action<IReceiveMouseEvents >(go7q3hZGnypm8UbRs.\u0023\u003DzwgN4FHGLnJhwzv26BQ\u003D\u003D);
      ag4ZlfwSgT7i2Apws.\u0023\u003Dz30RSSSygABj_<IReceiveMouseEvents >(action);
      _param2.Handled = go7q3hZGnypm8UbRs.\u0023\u003DzTi2kmf4\u003D.\u0023\u003Dz882B0y3Ue8fl();
    }

    public void \u0023\u003DzZnnbh0m5An\u0024F1Uadug\u003D\u003D(
      object _param1,
      MouseButtonEventArgs _param2)
    {
      MouseManager.\u0023\u003Dzcq2XeUHBVZN9jqX8KauJiyM\u003D uhbvzN9jqX8KauJiyM = new MouseManager.\u0023\u003Dzcq2XeUHBVZN9jqX8KauJiyM\u003D();
      uhbvzN9jqX8KauJiyM.\u0023\u003DqVLPFUC1wnpGY7Zl3JTxkXnU2TIEt8CuV\u0024PKiDG\u0024bMmk\u003D = this;
      Point point = this._variableSome3535.\u0023\u003DzBp9oXZI\u003D(this.\u0023\u003DzL2OrHlw\u003D, (MouseEventArgs) _param2);
      IEnumerable<IReceiveMouseEvents > ag4ZlfwSgT7i2Apws = this._variableSome3535.\u0023\u003Dzwp8c1z8\u003D(this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      uhbvzN9jqX8KauJiyM.\u0023\u003DzTi2kmf4\u003D = new ModifierMouseArgs(point, (MouseButtons) 4, \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk(), true, this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      Action<IReceiveMouseEvents > action = new Action<IReceiveMouseEvents >(uhbvzN9jqX8KauJiyM.\u0023\u003Dz6WIAV5vso4Y6vPf5VA\u003D\u003D);
      ag4ZlfwSgT7i2Apws.\u0023\u003Dz30RSSSygABj_<IReceiveMouseEvents >(action);
      _param2.Handled = uhbvzN9jqX8KauJiyM.\u0023\u003DzTi2kmf4\u003D.\u0023\u003Dz882B0y3Ue8fl();
    }

    public void \u0023\u003Dz5fPIvWoN_1Tou0ljEQ\u003D\u003D(
      object _param1,
      MouseButtonEventArgs _param2)
    {
      MouseManager.\u0023\u003DzhRF\u0024rbCfI1ikWlftxh2eQ7U\u003D cfI1ikWlftxh2eQ7U = new MouseManager.\u0023\u003DzhRF\u0024rbCfI1ikWlftxh2eQ7U\u003D();
      cfI1ikWlftxh2eQ7U.\u0023\u003DqhIKxx2Fgk_utZ5064cd0qNYlbflZp5kXmNVaqrMla8Q\u003D = this;
      Point point = this._variableSome3535.\u0023\u003DzBp9oXZI\u003D(this.\u0023\u003DzL2OrHlw\u003D, (MouseEventArgs) _param2);
      IEnumerable<IReceiveMouseEvents > ag4ZlfwSgT7i2Apws = this._variableSome3535.\u0023\u003Dzwp8c1z8\u003D(this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      cfI1ikWlftxh2eQ7U.\u0023\u003DzTi2kmf4\u003D = new ModifierMouseArgs(point, (MouseButtons) 4, \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk(), true, this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      Action<IReceiveMouseEvents > action = new Action<IReceiveMouseEvents >(cfI1ikWlftxh2eQ7U.\u0023\u003DzolnDW1Cyh39ARqLnFg\u003D\u003D);
      ag4ZlfwSgT7i2Apws.\u0023\u003Dz30RSSSygABj_<IReceiveMouseEvents >(action);
      _param2.Handled = cfI1ikWlftxh2eQ7U.\u0023\u003DzTi2kmf4\u003D.\u0023\u003Dz882B0y3Ue8fl();
    }

    public void \u0023\u003DzdvuVqJx4Gky8\u0024mb6Zg\u003D\u003D(
      object _param1,
      MouseWheelEventArgs _param2)
    {
      MouseManager.\u0023\u003DzOQsMlZgjkVZiIBBBvq7BWpc\u003D zgjkVziIbbBvq7Bwpc = new MouseManager.\u0023\u003DzOQsMlZgjkVZiIBBBvq7BWpc\u003D();
      zgjkVziIbbBvq7Bwpc.\u0023\u003DqviizWS0uYlNRyOw9oXk1JmvnJzBswMLEUzlTrRzThKA\u003D = this;
      Point point = this._variableSome3535.\u0023\u003DzBp9oXZI\u003D(this.\u0023\u003DzL2OrHlw\u003D, (MouseEventArgs) _param2);
      IEnumerable<IReceiveMouseEvents > ag4ZlfwSgT7i2Apws = this._variableSome3535.\u0023\u003Dzwp8c1z8\u003D(this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      zgjkVziIbbBvq7Bwpc.\u0023\u003DzTi2kmf4\u003D = new ModifierMouseArgs(point, (MouseButtons) 0, \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk(), _param2.Delta, true, this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      Action<IReceiveMouseEvents > action = new Action<IReceiveMouseEvents >(zgjkVziIbbBvq7Bwpc.\u0023\u003Dzzbp47HAC77wwCj4mqw\u003D\u003D);
      ag4ZlfwSgT7i2Apws.\u0023\u003Dz30RSSSygABj_<IReceiveMouseEvents >(action);
      _param2.Handled = zgjkVziIbbBvq7Bwpc.\u0023\u003DzTi2kmf4\u003D.\u0023\u003Dz882B0y3Ue8fl();
    }

    public void \u0023\u003Dz3KMXnZMECV2OkUL8vw\u003D\u003D(
      object _param1,
      MouseEventArgs _param2)
    {
      MouseManager.\u0023\u003DzHiYHAiumYY10dpF2ltto0w8\u003D aiumYy10dpF2ltto0w8 = new MouseManager.\u0023\u003DzHiYHAiumYY10dpF2ltto0w8\u003D();
      aiumYy10dpF2ltto0w8.\u0023\u003Dqyaob7ZmPRWRFRSYWBmEDoAiypRnMSzJDCIc0XzNtSqo\u003D = this;
      Point point = this._variableSome3535.\u0023\u003DzBp9oXZI\u003D(this.\u0023\u003DzL2OrHlw\u003D, _param2);
      IEnumerable<IReceiveMouseEvents > ag4ZlfwSgT7i2Apws = this._variableSome3535.\u0023\u003Dzwp8c1z8\u003D(this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      aiumYy10dpF2ltto0w8.\u0023\u003DzTi2kmf4\u003D = new ModifierMouseArgs(point, (MouseButtons) 0, \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk(), true, this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      Action<IReceiveMouseEvents > action = new Action<IReceiveMouseEvents >(aiumYy10dpF2ltto0w8.\u0023\u003DzGFMMAqOoeHDnRrvhow\u003D\u003D);
      ag4ZlfwSgT7i2Apws.\u0023\u003Dz30RSSSygABj_<IReceiveMouseEvents >(action);
      _param2.Handled = aiumYy10dpF2ltto0w8.\u0023\u003DzTi2kmf4\u003D.\u0023\u003Dz882B0y3Ue8fl();
    }

    public void \u0023\u003DzTKjwLjWI6l9WjUYx9g\u003D\u003D(
      object _param1,
      MouseButtonEventArgs _param2)
    {
      MouseManager.\u0023\u003DzY7CfQOlCGtzQjF07XvwBPz4\u003D cgtzQjF07XvwBpz4 = new MouseManager.\u0023\u003DzY7CfQOlCGtzQjF07XvwBPz4\u003D();
      cgtzQjF07XvwBpz4.\u0023\u003DqFkNKRpbJGcPo6KdRUdjYgV32QrpnUdNLLzRVdhdnQvA\u003D = this;
      Point point = this._variableSome3535.\u0023\u003DzBp9oXZI\u003D(this.\u0023\u003DzL2OrHlw\u003D, (MouseEventArgs) _param2);
      IEnumerable<IReceiveMouseEvents > ag4ZlfwSgT7i2Apws = this._variableSome3535.\u0023\u003Dzwp8c1z8\u003D(this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      cgtzQjF07XvwBpz4.\u0023\u003DzTi2kmf4\u003D = new ModifierMouseArgs(point, (MouseButtons) 2, \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk(), true, this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      Action<IReceiveMouseEvents > action = new Action<IReceiveMouseEvents >(cgtzQjF07XvwBpz4.\u0023\u003DzdbgJTyl0_Z5NId2bow\u003D\u003D);
      ag4ZlfwSgT7i2Apws.\u0023\u003Dz30RSSSygABj_<IReceiveMouseEvents >(action);
      _param2.Handled = cgtzQjF07XvwBpz4.\u0023\u003DzTi2kmf4\u003D.\u0023\u003Dz882B0y3Ue8fl();
    }

    public void \u0023\u003DzOKxMOPGH8TAVrjNYXw\u003D\u003D(
      object _param1,
      MouseButtonEventArgs _param2)
    {
      MouseManager.\u0023\u003DzG\u0024gA6j6jfrxRQolA2x_tg40\u003D a6j6jfrxRqolA2xTg40 = new MouseManager.\u0023\u003DzG\u0024gA6j6jfrxRQolA2x_tg40\u003D();
      a6j6jfrxRqolA2xTg40.\u0023\u003Dq\u0024uKyDGtJtqwdqW0OlZQOaisIsBBEZn9KuuA4WX6kbxA\u003D = this;
      Point point = this._variableSome3535.\u0023\u003DzBp9oXZI\u003D(this.\u0023\u003DzL2OrHlw\u003D, (MouseEventArgs) _param2);
      IEnumerable<IReceiveMouseEvents > ag4ZlfwSgT7i2Apws = this._variableSome3535.\u0023\u003Dzwp8c1z8\u003D(this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      a6j6jfrxRqolA2xTg40.\u0023\u003DzTi2kmf4\u003D = new ModifierMouseArgs(point, (MouseButtons) 2, \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D.\u0023\u003DzNFIr3TSkl0uk(), true, this.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D());
      Action<IReceiveMouseEvents > action = new Action<IReceiveMouseEvents >(a6j6jfrxRqolA2xTg40.\u0023\u003DzTO8kgirXuwiDLDodow\u003D\u003D);
      ag4ZlfwSgT7i2Apws.\u0023\u003Dz30RSSSygABj_<IReceiveMouseEvents >(action);
      _param2.Handled = a6j6jfrxRqolA2xTg40.\u0023\u003DzTi2kmf4\u003D.\u0023\u003Dz882B0y3Ue8fl();
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly MouseManager.SomeClass34343383 SomeMethond0343 = new MouseManager.SomeClass34343383();
    public static Func<IReceiveMouseEvents , bool> \u0023\u003DzgXbxBZLvg_0J\u0024EPUjg\u003D\u003D;

    public bool \u0023\u003DzRB7XSdoQ2eSQJafyp0e69aQ\u003D(
      IReceiveMouseEvents  _param1)
    {
      return _param1.\u0023\u003Dzo7mdr1Y1DFNe();
    }
  }

  private sealed class \u0023\u003DzE4CEJXjA93amsG7Qqp_sMKQ\u003D
  {
    public ModifierMouseArgs \u0023\u003DzTi2kmf4\u003D;
    public MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D \u0023\u003Dq9oPAsd_XX7A0iOT0n\u0024XoJRH5BDywfM7Sl5FCgaUG6WI\u003D;

    public void \u0023\u003DzWAuFYQDtZFB9v3VPYQ\u003D\u003D(
      IReceiveMouseEvents  _param1)
    {
      this.\u0023\u003Dq9oPAsd_XX7A0iOT0n\u0024XoJRH5BDywfM7Sl5FCgaUG6WI\u003D._variableSome3535.\u0023\u003Dzzk9Obv44lrydFcInoQ\u003D\u003D(this.\u0023\u003DzTi2kmf4\u003D, _param1, _param1.Equals((object) this.\u0023\u003Dq9oPAsd_XX7A0iOT0n\u0024XoJRH5BDywfM7Sl5FCgaUG6WI\u003D.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D()));
    }

    public void \u0023\u003DzUxQ2tKFJyEjKZs3bnA\u003D\u003D(
      IReceiveMouseEvents  _param1)
    {
      MouseManager.\u0023\u003Dzvn1sWKwdu6R4(this.\u0023\u003DzTi2kmf4\u003D, _param1, _param1.Equals((object) this.\u0023\u003Dq9oPAsd_XX7A0iOT0n\u0024XoJRH5BDywfM7Sl5FCgaUG6WI\u003D.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D()));
    }
  }

  private sealed class \u0023\u003DzG\u0024gA6j6jfrxRQolA2x_tg40\u003D
  {
    public ModifierMouseArgs \u0023\u003DzTi2kmf4\u003D;
    public MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D \u0023\u003Dq\u0024uKyDGtJtqwdqW0OlZQOaisIsBBEZn9KuuA4WX6kbxA\u003D;

    public void \u0023\u003DzTO8kgirXuwiDLDodow\u003D\u003D(
      IReceiveMouseEvents  _param1)
    {
      this.\u0023\u003Dq\u0024uKyDGtJtqwdqW0OlZQOaisIsBBEZn9KuuA4WX6kbxA\u003D._variableSome3535.\u0023\u003DzzGr9tanIYzso(this.\u0023\u003DzTi2kmf4\u003D, _param1, _param1.Equals((object) this.\u0023\u003Dq\u0024uKyDGtJtqwdqW0OlZQOaisIsBBEZn9KuuA4WX6kbxA\u003D.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D()));
    }
  }

  private sealed class \u0023\u003DzHiYHAiumYY10dpF2ltto0w8\u003D
  {
    public ModifierMouseArgs \u0023\u003DzTi2kmf4\u003D;
    public MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D \u0023\u003Dqyaob7ZmPRWRFRSYWBmEDoAiypRnMSzJDCIc0XzNtSqo\u003D;

    public void \u0023\u003DzGFMMAqOoeHDnRrvhow\u003D\u003D(
      IReceiveMouseEvents  _param1)
    {
      this.\u0023\u003Dqyaob7ZmPRWRFRSYWBmEDoAiypRnMSzJDCIc0XzNtSqo\u003D._variableSome3535.\u0023\u003Dz6ICnkaskXJ9U(this.\u0023\u003DzTi2kmf4\u003D, _param1, _param1.Equals((object) this.\u0023\u003Dqyaob7ZmPRWRFRSYWBmEDoAiypRnMSzJDCIc0XzNtSqo\u003D.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D()));
    }
  }

  private sealed class \u0023\u003DzOQsMlZgjkVZiIBBBvq7BWpc\u003D
  {
    public ModifierMouseArgs \u0023\u003DzTi2kmf4\u003D;
    public MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D \u0023\u003DqviizWS0uYlNRyOw9oXk1JmvnJzBswMLEUzlTrRzThKA\u003D;

    public void \u0023\u003Dzzbp47HAC77wwCj4mqw\u003D\u003D(
      IReceiveMouseEvents  _param1)
    {
      this.\u0023\u003DqviizWS0uYlNRyOw9oXk1JmvnJzBswMLEUzlTrRzThKA\u003D._variableSome3535.\u0023\u003Dzs9W6JdpZ_79\u0024(this.\u0023\u003DzTi2kmf4\u003D, _param1, _param1.Equals((object) this.\u0023\u003DqviizWS0uYlNRyOw9oXk1JmvnJzBswMLEUzlTrRzThKA\u003D.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D()));
    }
  }

  private sealed class \u0023\u003DzOVbvGyAUlOlSG92isdmlY08\u003D
  {
    public ModifierMouseArgs \u0023\u003DzTi2kmf4\u003D;
    public MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D \u0023\u003DqDGS82oTBZ\u0024CkKsNfUVM9XxR6WPEu4uyPJgSIUBBKyWs\u003D;

    public void \u0023\u003Dzi3lac9zHQwZ94EQd\u0024g\u003D\u003D(
      IReceiveMouseEvents  _param1)
    {
      this.\u0023\u003DqDGS82oTBZ\u0024CkKsNfUVM9XxR6WPEu4uyPJgSIUBBKyWs\u003D._variableSome3535.\u0023\u003DzzGr9tanIYzso(this.\u0023\u003DzTi2kmf4\u003D, _param1, _param1.Equals((object) this.\u0023\u003DqDGS82oTBZ\u0024CkKsNfUVM9XxR6WPEu4uyPJgSIUBBKyWs\u003D.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D()));
    }
  }

  private sealed class \u0023\u003DzY7CfQOlCGtzQjF07XvwBPz4\u003D
  {
    public ModifierMouseArgs \u0023\u003DzTi2kmf4\u003D;
    public MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D \u0023\u003DqFkNKRpbJGcPo6KdRUdjYgV32QrpnUdNLLzRVdhdnQvA\u003D;

    public void \u0023\u003DzdbgJTyl0_Z5NId2bow\u003D\u003D(
      IReceiveMouseEvents  _param1)
    {
      MouseManager.\u0023\u003Dzvn1sWKwdu6R4(this.\u0023\u003DzTi2kmf4\u003D, _param1, _param1.Equals((object) this.\u0023\u003DqFkNKRpbJGcPo6KdRUdjYgV32QrpnUdNLLzRVdhdnQvA\u003D.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D()));
    }
  }

  private sealed class \u0023\u003Dzcq2XeUHBVZN9jqX8KauJiyM\u003D
  {
    public ModifierMouseArgs \u0023\u003DzTi2kmf4\u003D;
    public MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D \u0023\u003DqVLPFUC1wnpGY7Zl3JTxkXnU2TIEt8CuV\u0024PKiDG\u0024bMmk\u003D;

    public void \u0023\u003Dz6WIAV5vso4Y6vPf5VA\u003D\u003D(
      IReceiveMouseEvents  _param1)
    {
      MouseManager.\u0023\u003Dzvn1sWKwdu6R4(this.\u0023\u003DzTi2kmf4\u003D, _param1, _param1.Equals((object) this.\u0023\u003DqVLPFUC1wnpGY7Zl3JTxkXnU2TIEt8CuV\u0024PKiDG\u0024bMmk\u003D.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D()));
    }
  }

  private sealed class \u0023\u003DzhRF\u0024rbCfI1ikWlftxh2eQ7U\u003D
  {
    public ModifierMouseArgs \u0023\u003DzTi2kmf4\u003D;
    public MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D \u0023\u003DqhIKxx2Fgk_utZ5064cd0qNYlbflZp5kXmNVaqrMla8Q\u003D;

    public void \u0023\u003DzolnDW1Cyh39ARqLnFg\u003D\u003D(
      IReceiveMouseEvents  _param1)
    {
      this.\u0023\u003DqhIKxx2Fgk_utZ5064cd0qNYlbflZp5kXmNVaqrMla8Q\u003D._variableSome3535.\u0023\u003DzzGr9tanIYzso(this.\u0023\u003DzTi2kmf4\u003D, _param1, _param1.Equals((object) this.\u0023\u003DqhIKxx2Fgk_utZ5064cd0qNYlbflZp5kXmNVaqrMla8Q\u003D.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D()));
    }
  }

  private static class \u0023\u003Dzj2IY6aE\u003D
  {
    public static Action<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf, IReceiveMouseEvents , bool> \u0023\u003Dzc8pv9GVxdmtKAl\u00241BQ\u003D\u003D;
    public static Action<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf, IReceiveMouseEvents , bool> \u0023\u003DzfA7Srqxog6G14gu2Jw\u003D\u003D;
    public static Action<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf, IReceiveMouseEvents , bool> \u0023\u003DzUtAWjYDhFPs8QavWmQ\u003D\u003D;
  }

  private sealed class \u0023\u003Dzx7SG8UTBOy2shIbrNVKi5DE\u003D
  {
    public Action<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf, IReceiveMouseEvents , bool> \u0023\u003DzaY_8iBE\u003D;
    public \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf \u0023\u003DzTi2kmf4\u003D;
    public MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D \u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D;

    public void \u0023\u003Dz9O7cRibjOuApTwUuGA\u003D\u003D(
      IReceiveMouseEvents  _param1)
    {
      this.\u0023\u003DzaY_8iBE\u003D(this.\u0023\u003DzTi2kmf4\u003D, _param1, _param1.Equals((object) this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D()));
    }
  }

  private sealed class \u0023\u003DzyQni\u0024go7q3hZ_gnypm8UBRs\u003D
  {
    public ModifierMouseArgs \u0023\u003DzTi2kmf4\u003D;
    public MouseManager.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D \u0023\u003DqtAzlqurTc5We1ZAU3ovVrx43Z68HZcrCMXu4\u0024g\u002481qA\u003D;

    public void \u0023\u003DzwgN4FHGLnJhwzv26BQ\u003D\u003D(
      IReceiveMouseEvents  _param1)
    {
      this.\u0023\u003DqtAzlqurTc5We1ZAU3ovVrx43Z68HZcrCMXu4\u0024g\u002481qA\u003D._variableSome3535.\u0023\u003DzuGCrm3cpH5KI(this.\u0023\u003DzTi2kmf4\u003D, _param1, _param1.Equals((object) this.\u0023\u003DqtAzlqurTc5We1ZAU3ovVrx43Z68HZcrCMXu4\u0024g\u002481qA\u003D.\u0023\u003DzqvJkfbGyJf\u0024_.\u0023\u003DzY2vUSRo\u003D()));
    }
  }
}
