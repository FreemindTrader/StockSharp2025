// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.MessageBoxBuilder
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Diagnostics;
using System.Windows;

namespace Ecng.Xaml
{
  /// <summary>Message box builder.</summary>
  public class MessageBoxBuilder
  {
    
    private static IMessageBoxHandler \u0023\u003DzdjWpfk51_\u0024d3 = (IMessageBoxHandler) new MessageBoxBuilder.WpfMessageBoxHandler();
    
    private IMessageBoxHandler \u0023\u003DznbA8D9M\u003D = MessageBoxBuilder.DefaultHandler;
    
    private Window \u0023\u003DzLaippzM\u003D;
    
    private string \u0023\u003DzgaZbeog\u003D;
    
    private string \u0023\u003DzquiKLvw\u003D;
    
    private MessageBoxButton \u0023\u003Dzy_SDzBA\u003D;
    
    private MessageBoxImage \u0023\u003Dz2\u00248hlbk\u003D;
    
    private MessageBoxResult \u0023\u003Dz2GzD8EFr1vNR;
    
    private MessageBoxOptions \u0023\u003DzBrLD9pw\u003D;

    /// <summary>Default message box handler.</summary>
    public static IMessageBoxHandler DefaultHandler
    {
      get
      {
        return MessageBoxBuilder.\u0023\u003DzdjWpfk51_\u0024d3;
      }
      set
      {
        IMessageBoxHandler messageBoxHandler = value;
        if (messageBoxHandler == null)
          throw new ArgumentNullException(nameof(2127280101));
        MessageBoxBuilder.\u0023\u003DzdjWpfk51_\u0024d3 = messageBoxHandler;
      }
    }

    /// <summary>Set custom message box handler.</summary>
    /// <param name="handler">Handler.</param>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Handler(IMessageBoxHandler handler)
    {
      IMessageBoxHandler messageBoxHandler = handler;
      if (messageBoxHandler == null)
        throw new ArgumentNullException(nameof(2127280080));
      this.\u0023\u003DznbA8D9M\u003D = messageBoxHandler;
      return this;
    }

    /// <summary>Set owner object.</summary>
    /// <param name="owner">Owner.</param>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Owner(DependencyObject owner)
    {
      this.\u0023\u003DzLaippzM\u003D = owner.GetWindow();
      return this;
    }

    /// <summary>Set owner window.</summary>
    /// <param name="owner">Owner.</param>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Owner(Window owner)
    {
      this.\u0023\u003DzLaippzM\u003D = owner;
      return this;
    }

    /// <summary>Set message box text.</summary>
    /// <param name="text">Text.</param>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Text(string text)
    {
      this.\u0023\u003DzgaZbeog\u003D = text;
      return this;
    }

    /// <summary>Set message box caption.</summary>
    /// <param name="caption">Caption.</param>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Caption(string caption)
    {
      this.\u0023\u003DzquiKLvw\u003D = caption;
      return this;
    }

    /// <summary>
    /// Set message box type to error and set exception to show.
    /// </summary>
    /// <param name="error">Error.</param>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Error(Exception error)
    {
      if (error == null)
        throw new ArgumentNullException(nameof(2127280075));
      return this.Text(error.ToString()).Icon(MessageBoxImage.Hand);
    }

    /// <summary>Set message box type to error.</summary>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Error()
    {
      return this.Icon(MessageBoxImage.Hand);
    }

    /// <summary>Set message box type to warning.</summary>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Warning()
    {
      return this.Icon(MessageBoxImage.Exclamation);
    }

    /// <summary>Set message box type to info.</summary>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Info()
    {
      return this.Icon(MessageBoxImage.Asterisk);
    }

    /// <summary>Set message box type to question.</summary>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Question()
    {
      return this.Icon(MessageBoxImage.Question);
    }

    /// <summary>Set message box icon.</summary>
    /// <param name="icon">Icon.</param>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Icon(MessageBoxImage icon)
    {
      this.\u0023\u003Dz2\u00248hlbk\u003D = icon;
      return this;
    }

    /// <summary>Set message box type to yes/no.</summary>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder YesNo()
    {
      return this.Button(MessageBoxButton.YesNo);
    }

    /// <summary>Set message box type to ok/cancel.</summary>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder OkCancel()
    {
      return this.Button(MessageBoxButton.OKCancel);
    }

    /// <summary>Set message box buttons.</summary>
    /// <param name="button">Button.</param>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Button(MessageBoxButton button)
    {
      this.\u0023\u003Dzy_SDzBA\u003D = button;
      return this;
    }

    /// <summary>Set message box options.</summary>
    /// <param name="options">Options.</param>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder Options(MessageBoxOptions options)
    {
      this.\u0023\u003DzBrLD9pw\u003D = options;
      return this;
    }

    /// <summary>Set message box default result.</summary>
    /// <param name="defaultResult">Default result.</param>
    /// <returns>Builder.</returns>
    public MessageBoxBuilder DefaultResult(MessageBoxResult defaultResult)
    {
      this.\u0023\u003Dz2GzD8EFr1vNR = defaultResult;
      return this;
    }

    /// <summary>Show message box.</summary>
    /// <returns>Result.</returns>
    public MessageBoxResult Show()
    {
      string caption = this.\u0023\u003DzquiKLvw\u003D ?? TypeHelper.ApplicationName;
      if (this.\u0023\u003DzLaippzM\u003D != null)
        return this.\u0023\u003DznbA8D9M\u003D.Show(this.\u0023\u003DzLaippzM\u003D, this.\u0023\u003DzgaZbeog\u003D, caption, this.\u0023\u003Dzy_SDzBA\u003D, this.\u0023\u003Dz2\u00248hlbk\u003D, this.\u0023\u003Dz2GzD8EFr1vNR, this.\u0023\u003DzBrLD9pw\u003D);
      return this.\u0023\u003DznbA8D9M\u003D.Show(this.\u0023\u003DzgaZbeog\u003D, caption, this.\u0023\u003Dzy_SDzBA\u003D, this.\u0023\u003Dz2\u00248hlbk\u003D, this.\u0023\u003Dz2GzD8EFr1vNR, this.\u0023\u003DzBrLD9pw\u003D);
    }

    /// <summary>WPF message box builder.</summary>
    public class WpfMessageBoxHandler : IMessageBoxHandler
    {
      MessageBoxResult IMessageBoxHandler.Show(
        string _param1,
        string _param2,
        MessageBoxButton _param3,
        MessageBoxImage _param4,
        MessageBoxResult _param5,
        MessageBoxOptions _param6)
      {
        return MessageBox.Show(_param1, _param2, _param3, _param4, _param5, _param6);
      }

      MessageBoxResult IMessageBoxHandler.Show(
        Window _param1,
        string _param2,
        string _param3,
        MessageBoxButton _param4,
        MessageBoxImage _param5,
        MessageBoxResult _param6,
        MessageBoxOptions _param7)
      {
        return MessageBox.Show(_param1, _param2, _param3, _param4, _param5, _param6, _param7);
      }
    }
  }
}
