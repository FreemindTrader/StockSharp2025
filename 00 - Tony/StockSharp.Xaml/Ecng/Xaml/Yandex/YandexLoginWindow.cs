//// Decompiled with JetBrains decompiler
//// Type: Ecng.Xaml.Yandex.YandexLoginWindow
//// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
//// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

//using DevExpress.Xpf.Core;
//using Disk.SDK;
//using Disk.SDK.Provider;
//using Ecng.ComponentModel;
//using StockSharp.Localization;
//using System;
//using System.CodeDom.Compiler;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Markup;
//using System.Windows.Navigation;

//namespace Ecng.Xaml.Yandex
//{
//  /// <summary>Yandex.Disk login (user-interaction) window.</summary>
//  /// <summary>YandexLoginWindow</summary>
//  public class YandexLoginWindow : ThemedWindow, IComponentConnector
//  {
    
//    private bool \u0023\u003DzD9kHvqx\u0024c5Ky;
    
//    private readonly YandexLoginWindow.\u0023\u003DzQ6bvcT8\u003D \u0023\u003Dzw\u0024ajoegwEW49;
    
//    internal LoadingDecorator \u0023\u003DzqJcJcnz30TNY;
    
//    internal WebBrowser \u0023\u003DzxK3Irdc\u003D;
    
//    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="T:Ecng.Xaml.Yandex.YandexLoginWindow" />.
//    /// </summary>
//    public YandexLoginWindow()
//    {
//      base.\u002Ector();
//      this.InitializeComponent();
//      this.\u0023\u003DzxK3Irdc\u003D.Visibility = Visibility.Hidden;
//      this.\u0023\u003DzqJcJcnz30TNY.SplashScreenDataContext = ((object) this.\u0023\u003Dzw\u0024ajoegwEW49);
//      this.\u0023\u003Dzw\u0024ajoegwEW49.Title = LocalizedStrings.InProgress;
//      this.\u0023\u003DzqJcJcnz30TNY.IsSplashScreenShown = (new bool?(true));
//      this.\u0023\u003DzxK3Irdc\u003D.Navigated += new NavigatedEventHandler(this.\u0023\u003DztAJiab4qQVyK);
//    }

//    /// <summary>Authentication completed.</summary>
//    public event EventHandler<GenericSdkEventArgs<string>> AuthCompleted;

//    private void \u0023\u003DztAJiab4qQVyK(object _param1, NavigationEventArgs _param2)
//    {
//      if (this.\u0023\u003DzD9kHvqx\u0024c5Ky)
//        return;
//      this.\u0023\u003DzxK3Irdc\u003D.Visibility = Visibility.Visible;
//      this.\u0023\u003DzqJcJcnz30TNY.IsSplashScreenShown = (new bool?(false));
//    }

//    private void \u0023\u003DzlRo5TiGexHib\u0024OxZY1n9UZw\u003D(
//      object _param1,
//      RoutedEventArgs _param2)
//    {
//      DiskSdkClientExtensions.AuthorizeAsync((IDiskSdkClient) new DiskSdkClient(string.Empty), (IBrowser) new WebBrowserWrapper(this.\u0023\u003DzxK3Irdc\u003D), nameof(2127278133), nameof(2127278092), new EventHandler<GenericSdkEventArgs<string>>(this.\u0023\u003Dz69taBK1QREsh));
//    }

//    private void \u0023\u003Dz69taBK1QREsh(object _param1, GenericSdkEventArgs<string> _param2)
//    {
//      YandexLoginWindow.SomeShitLambda00342234 _someShitIUnderstand = new YandexLoginWindow.SomeShitLambda00342234();
//      _someShitIUnderstand._ed01 = this;
//      _someShitIUnderstand.\u0023\u003DzmrVh_q4\u003D = _param2;
//      this.\u0023\u003DzD9kHvqx\u0024c5Ky = true;
//      this.\u0023\u003DzxK3Irdc\u003D.Visibility = Visibility.Hidden;
//      this.\u0023\u003Dzw\u0024ajoegwEW49.Title = LocalizedStrings.Str1574;
//      this.\u0023\u003DzqJcJcnz30TNY.IsSplashScreenShown = (new bool?(true));
//      Task.Factory.StartNew(new Action(_someShitIUnderstand.\u0023\u003DzhKDpsXenHzKMD7JirA\u003D\u003D)).ContinueWith(new Action<Task>(_someShitIUnderstand.\u0023\u003Dz50_A11zLETPixoJPiA\u003D\u003D), TaskScheduler.FromCurrentSynchronizationContext());
//    }

//    /// <summary>InitializeComponent</summary>
//    [DebuggerNonUserCode]
//    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
//    public void InitializeComponent()
//    {
//      if (this.\u0023\u003DzwPHzQV2Vg5J\u0024)
//        return;
//      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
//      Application.LoadComponent((object) this, new Uri(nameof(2127278300), UriKind.Relative));
//    }

//    [DebuggerNonUserCode]
//    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
//    [EditorBrowsable(EditorBrowsableState.Never)]
//    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
//      int _param1,
//      object _param2)
//    {
//      switch (_param1)
//      {
//        case 1:
//          ((FrameworkElement) _param2).Loaded += new RoutedEventHandler(this.\u0023\u003DzlRo5TiGexHib\u0024OxZY1n9UZw\u003D);
//          break;
//        case 2:
//          this.\u0023\u003DzqJcJcnz30TNY = (LoadingDecorator) _param2;
//          break;
//        case 3:
//          this.\u0023\u003DzxK3Irdc\u003D = (WebBrowser) _param2;
//          break;
//        default:
//          this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
//          break;
//      }
//    }

//    private sealed class \u0023\u003DzQ6bvcT8\u003D : NotifiableObject
//    {
      
//      private string \u0023\u003DzqXxyzpc\u003D;

//      public \u0023\u003DzQ6bvcT8\u003D()
//      {
//        base.\u002Ector();
//      }

//      public string Title
//      {
//        get
//        {
//          return this.\u0023\u003DzqXxyzpc\u003D;
//        }
//        set
//        {
//          this.\u0023\u003DzqXxyzpc\u003D = value;
//          this.NotifyChanged(nameof(2127278137));
//        }
//      }
//    }

//    private sealed class SomeShitLambda00342234
//    {
//      public YandexLoginWindow _ed01;
//      public GenericSdkEventArgs<string> \u0023\u003DzmrVh_q4\u003D;

//      internal void \u0023\u003DzhKDpsXenHzKMD7JirA\u003D\u003D()
//      {
//        EventHandler<GenericSdkEventArgs<string>> zlQkM76thbNmc = this._ed01.\u0023\u003DzlQKM76thbNmc;
//        if (zlQkM76thbNmc == null)
//          return;
//        zlQkM76thbNmc((object) this._ed01, new GenericSdkEventArgs<string>(this.\u0023\u003DzmrVh_q4\u003D.Result));
//      }

//      [NullableContext(1)]
//      internal void \u0023\u003Dz50_A11zLETPixoJPiA\u003D\u003D(Task _param1)
//      {
//        this._ed01.\u0023\u003DzqJcJcnz30TNY.IsSplashScreenShown = (new bool?(false));
//        ((Window) this._ed01).DialogResult = new bool?(true);
//      }
//    }
//  }
//}
