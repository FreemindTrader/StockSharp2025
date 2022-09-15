// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Database.DatabaseConnectionWindow
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Data;
using LinqToDB;
using StockSharp.Localization;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace Ecng.Xaml.Database
{
  /// <summary>Window for create and edit database connection.</summary>
  /// <summary>DatabaseConnectionWindow</summary>
  public class DatabaseConnectionWindow : ThemedWindow, IComponentConnector
  {
    
    private DatabaseConnectionWindow.\u0023\u003DzFPloZ6M\u003D \u0023\u003DzJqavrOQ\u003D;
    
    internal DatabaseConnectionComboBox \u0023\u003Dz\u0024U83MOcrnEFH;
    
    internal PropertyGridEx \u0023\u003Dz81B3wZ3qyaBJ;
    
    internal SimpleButton \u0023\u003Dz3nm74ic\u003D;
    
    internal SimpleButton \u0023\u003DzPZiYgJWITTT_;
    
    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Xaml.Database.DatabaseConnectionWindow" />.
    /// </summary>
    public DatabaseConnectionWindow()
    {
      base.\u002Ector();
      this.InitializeComponent();
      this.Pair = new DatabaseConnectionPair();
    }

    /// <summary>
    /// Show <see cref="T:Ecng.Xaml.Database.DatabaseConnectionComboBox" />.
    /// </summary>
    public bool ShowComboBox
    {
      get
      {
        return ((UIElement) this.\u0023\u003Dz\u0024U83MOcrnEFH).IsVisible;
      }
      set
      {
        ((UIElement) this.\u0023\u003Dz\u0024U83MOcrnEFH).Visibility = value ? Visibility.Visible : Visibility.Collapsed;
      }
    }

    private DatabaseConnectionPair \u0023\u003DzLp9SojY\u003D()
    {
      if (this.\u0023\u003DzJqavrOQ\u003D == null)
        return (DatabaseConnectionPair) null;
      DatabaseConnectionPair databaseConnectionPair = new DatabaseConnectionPair();
      databaseConnectionPair.set_Provider(this.\u0023\u003DzJqavrOQ\u003D.Provider);
      databaseConnectionPair.ConnectionString = this.\u0023\u003DzJqavrOQ\u003D.ConnectionString;
      return databaseConnectionPair;
    }

    /// <summary>Connection info.</summary>
    public DatabaseConnectionPair Pair
    {
      get
      {
        DatabaseConnectionPair databaseConnectionPair = this.\u0023\u003DzLp9SojY\u003D();
        if (databaseConnectionPair == null)
          return (DatabaseConnectionPair) null;
        if (DatabaseHelper.Cache != null)
          databaseConnectionPair = DatabaseHelper.Cache.GetOrAddCache(databaseConnectionPair);
        return databaseConnectionPair;
      }
      set
      {
        if (value == null)
        {
          this.\u0023\u003DzJqavrOQ\u003D = (DatabaseConnectionWindow.\u0023\u003DzFPloZ6M\u003D) null;
        }
        else
        {
          this.\u0023\u003DzJqavrOQ\u003D = new DatabaseConnectionWindow.\u0023\u003DzFPloZ6M\u003D()
          {
            Provider = value.get_Provider()
          };
          if (!value.ConnectionString.IsEmpty())
            this.\u0023\u003DzJqavrOQ\u003D.ConnectionString = value.ConnectionString;
        }
        this.\u0023\u003Dz81B3wZ3qyaBJ.set_SelectedObject((object) this.\u0023\u003DzJqavrOQ\u003D);
        ((UIElement) this.\u0023\u003Dz3nm74ic\u003D).IsEnabled = value != null;
      }
    }

    private void dje_z935M9NP2DCUL7LY9MA6DV_ejd(object _param1, EditValueChangedEventArgs _param2)
    {
      this.Pair = (DatabaseConnectionPair) _param2.get_NewValue();
      ((UIElement) this.\u0023\u003Dz3nm74ic\u003D).IsEnabled = _param2.get_NewValue() != null;
    }

    private void \u0023\u003DzYgNVIjYSvytb(object _param1, RoutedEventArgs _param2)
    {
      ((UIElement) this.\u0023\u003DzPZiYgJWITTT_).IsEnabled = this.\u0023\u003DzLp9SojY\u003D().Verify((DependencyObject) this, true);
    }

    /// <summary>InitializeComponent</summary>
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        return;
      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      Application.LoadComponent((object) this, new Uri(nameof(2127280870), UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    internal Delegate \u0023\u003Dzk_6RQsm5oL9L(Type _param1, string _param2)
    {
      return Delegate.CreateDelegate(_param1, (object) this, _param2);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
      int _param1,
      object _param2)
    {
      switch (_param1)
      {
        case 1:
          this.\u0023\u003Dz\u0024U83MOcrnEFH = (DatabaseConnectionComboBox) _param2;
          break;
        case 2:
          this.\u0023\u003Dz81B3wZ3qyaBJ = (PropertyGridEx) _param2;
          break;
        case 3:
          this.\u0023\u003Dz3nm74ic\u003D = (SimpleButton) _param2;
          ((ButtonBase) this.\u0023\u003Dz3nm74ic\u003D).Click += new RoutedEventHandler(this.\u0023\u003DzYgNVIjYSvytb);
          break;
        case 4:
          this.\u0023\u003DzPZiYgJWITTT_ = (SimpleButton) _param2;
          break;
        default:
          this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
          break;
      }
    }

    [Display(Description = "StringDescription", Name = "Settings", ResourceType = typeof (LocalizedStrings))]
    private sealed class \u0023\u003DzFPloZ6M\u003D : NotifiableObject
    {
      
      private readonly DbConnectionStringBuilder \u0023\u003Dz6frtjSc\u003D = new DbConnectionStringBuilder();
      
      private string \u0023\u003DzBKQFYes\u003D;

      private T \u0023\u003DzvUSYIzE\u003D<T>(string _param1)
      {
        if (!this.\u0023\u003Dz6frtjSc\u003D.Keys.Cast<string>().Contains<string>(_param1))
          return default (T);
        try
        {
          return this.\u0023\u003Dz6frtjSc\u003D[_param1].To<T>();
        }
        catch (InvalidCastException ex)
        {
          return default (T);
        }
      }

      [ItemsSource(typeof (DatabaseConnectionWindow.\u0023\u003DzFPloZ6M\u003D.\u0023\u003DzHophxWFMr7Vb))]
      [Display(Description = "ProviderSettings", GroupName = "Common", Name = "Provider", Order = 0, ResourceType = typeof (LocalizedStrings))]
      public string Provider
      {
        get
        {
          return this.\u0023\u003DzBKQFYes\u003D;
        }
        set
        {
          this.\u0023\u003DzBKQFYes\u003D = value;
          this.NotifyChanged(nameof(2127281581));
        }
      }

      [Display(Description = "ServerDescription", GroupName = "Common", Name = "Str3416", Order = 1, ResourceType = typeof (LocalizedStrings))]
      public string Server
      {
        get
        {
          return this.\u0023\u003DzvUSYIzE\u003D<string>(nameof(2127281564));
        }
        set
        {
          this.\u0023\u003Dz6frtjSc\u003D[nameof(2127281564)] = (object) value;
          this.NotifyChanged(nameof(2127281550));
        }
      }

      [Display(Description = "DatabaseDescription", GroupName = "Common", Name = "Database", Order = 2, ResourceType = typeof (LocalizedStrings))]
      public string Database
      {
        get
        {
          return this.\u0023\u003DzvUSYIzE\u003D<string>(nameof(2127280757));
        }
        set
        {
          this.\u0023\u003Dz6frtjSc\u003D[nameof(2127280757)] = (object) value;
          this.NotifyChanged(nameof(2127281550));
        }
      }

      [Display(Description = "LoginDescription", GroupName = "Common", Name = "Login", Order = 3, ResourceType = typeof (LocalizedStrings))]
      public string UserName
      {
        get
        {
          return this.\u0023\u003DzvUSYIzE\u003D<string>(nameof(2127280731));
        }
        set
        {
          this.\u0023\u003Dz6frtjSc\u003D[nameof(2127280731)] = (object) value;
          this.NotifyChanged(nameof(2127281550));
        }
      }

      [Display(Description = "PasswordDescription", GroupName = "Common", Name = "Password", Order = 4, ResourceType = typeof (LocalizedStrings))]
      public SecureString Password
      {
        get
        {
          return this.\u0023\u003DzvUSYIzE\u003D<string>(nameof(2127280713)).Secure();
        }
        set
        {
          this.\u0023\u003Dz6frtjSc\u003D[nameof(2127280713)] = (object) value.UnSecure();
          this.NotifyChanged(nameof(2127281550));
        }
      }

      [Display(Description = "SecurityDescription", GroupName = "Common", Name = "IntegratedSecurity", Order = 5, ResourceType = typeof (LocalizedStrings))]
      public bool IntegratedSecurity
      {
        get
        {
          return this.\u0023\u003DzvUSYIzE\u003D<bool>(nameof(2127280696));
        }
        set
        {
          if (value)
            this.\u0023\u003Dz6frtjSc\u003D[nameof(2127280696)] = (object) true;
          else
            this.\u0023\u003Dz6frtjSc\u003D.Remove(nameof(2127280696));
          this.NotifyChanged(nameof(2127281550));
        }
      }

      [Display(Description = "ConnectionStringDescription", GroupName = "Common", Name = "NewConnectionString", Order = 6, ResourceType = typeof (LocalizedStrings))]
      public string ConnectionString
      {
        get
        {
          return this.\u0023\u003Dz6frtjSc\u003D.ConnectionString;
        }
        set
        {
          this.\u0023\u003Dz6frtjSc\u003D.ConnectionString = value;
          this.NotifyChanged(nameof(2127280674));
          this.NotifyChanged(nameof(2127280671));
          this.NotifyChanged(nameof(2127280654));
          this.NotifyChanged(nameof(2127280713));
          this.NotifyChanged(nameof(2127280893));
        }
      }

      private sealed class \u0023\u003DzHophxWFMr7Vb : ItemsSourceBase<string>
      {
        
        private static readonly string[] \u0023\u003DzLEvL2hc\u003D = ((IEnumerable<FieldInfo>) typeof (ProviderName).GetFields(BindingFlags.Static | BindingFlags.Public)).Select<FieldInfo, object>(new Func<FieldInfo, object>(DatabaseConnectionWindow.\u0023\u003DzFPloZ6M\u003D.\u0023\u003DzHophxWFMr7Vb.SomeShit.ShitMethod02.\u0023\u003DzhUrNTak4q0JOU1S7k2FTVBA\u003D)).OfType<string>().ToArray<string>();

        protected override IEnumerable<string> GetValues()
        {
          return (IEnumerable<string>) DatabaseConnectionWindow.\u0023\u003DzFPloZ6M\u003D.\u0023\u003DzHophxWFMr7Vb.\u0023\u003DzLEvL2hc\u003D;
        }

        [Serializable]
        private sealed class SomeShit
        {
          public static readonly DatabaseConnectionWindow.\u0023\u003DzFPloZ6M\u003D.\u0023\u003DzHophxWFMr7Vb.SomeShit ShitMethod02 = new DatabaseConnectionWindow.\u0023\u003DzFPloZ6M\u003D.\u0023\u003DzHophxWFMr7Vb.SomeShit();

          internal object \u0023\u003DzhUrNTak4q0JOU1S7k2FTVBA\u003D(FieldInfo _param1)
          {
            return _param1.GetValue((object) null);
          }
        }
      }
    }
  }
}
