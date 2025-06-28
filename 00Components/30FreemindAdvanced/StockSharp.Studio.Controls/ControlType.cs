// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ControlType
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Studio.Controls;

public class ControlType : NotifiableObject
{
    private static readonly CachedSynchronizedSet<ControlType> _components = new CachedSynchronizedSet<ControlType>();

    public Type Type { get; }

    public string Name
    {
        get => this.Type.GetDisplayName( null);
    }

    public string Description
    {
        get => this.Type.GetDescription( (string)null);
    }

    public ImageSource Icon { get; }

    public bool IsToolWindow { get; }

    public ControlType(Type type)
    {
        this.Type = type ?? throw new ArgumentNullException(nameof(type));
        this.IsToolWindow = AttributeHelper.GetAttribute<ToolWindowAttribute>((ICustomAttributeProvider)type, true) != null;
        Uri vectorIcon = StockSharp.Messages.Extensions.TryGetVectorIcon(type);
        if ((object)vectorIcon != null)
            this.Icon = vectorIcon.Url2Img();
        Uri iconUrl = Ecng.ComponentModel.Extensions.GetIconUrl(type);
        if (iconUrl != (Uri)null)
            this.Icon = (ImageSource)new BitmapImage(iconUrl);
        LocalizedStrings.ActiveLanguageChanged += new Action(this.OnActiveLanguageChanged);
    }

    private void OnActiveLanguageChanged()
    {
        this.NotifyPropertyChanged("Name");
        this.NotifyPropertyChanged("Description");
    }

    public static void AddComponent<T>() where T : IStudioControl
    {
        ((BaseCollection<ControlType, ISet<ControlType>>)ControlType._components).Add(new ControlType(typeof(T)));
    }

    public static IEnumerable<ControlType> GetComponents()
    {
        return (IEnumerable<ControlType>)ControlType._components.Cache;
    }
}
