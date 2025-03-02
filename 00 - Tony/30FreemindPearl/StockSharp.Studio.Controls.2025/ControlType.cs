// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ControlType
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StockSharp.Studio.Controls
{
    public class ControlType : NotifiableObject
    {
        private static readonly CachedSynchronizedSet<ControlType> _components = new CachedSynchronizedSet<ControlType>();

        public Type Type { get; }

        public string Name
        {
            get
            {
                return Ecng.ComponentModel.Extensions.GetDisplayName( ( ICustomAttributeProvider ) this.Type, ( string ) null );
            }
        }

        public string Description
        {
            get
            {
                return Ecng.ComponentModel.Extensions.GetDescription( ( ICustomAttributeProvider ) this.Type, ( string ) null );
            }
        }

        public ImageSource Icon { get; }

        public bool IsToolWindow { get; }

        public ControlType( Type type )
        {
            
            Type type1 = type;
            if (  type1 == null )
                throw new ArgumentNullException( nameof( type ) );
            this.Type = type1;
            this.IsToolWindow = AttributeHelper.GetAttribute<ToolWindowAttribute>( ( ICustomAttributeProvider ) type, true ) != null;
            
            Uri vectorIcon = StockSharp.Messages.Extensions.TryGetVectorIcon( type );
            if (  vectorIcon != null )
                this.Icon = vectorIcon.Url2Img();
            Uri iconUrl = Ecng.ComponentModel.Extensions.GetIconUrl(type);
            if ( iconUrl != ( Uri ) null )
                this.Icon = ( ImageSource ) new BitmapImage( iconUrl );
            LocalizedStrings.ActiveLanguageChanged += new Action( this.OnActiveLanguageChanged );
        }

        private void OnActiveLanguageChanged()
        {
            this.NotifyPropertyChanged( "Name" );
            this.NotifyPropertyChanged( "Description" );
        }

        public static void AddComponent<T>() where T : IStudioControl
        {
            ( ( BaseCollection<ControlType, ISet<ControlType>> ) ControlType._components ).Add( new ControlType( typeof( T ) ) );
        }

        public static IEnumerable<ControlType> GetComponents()
        {
            return ( IEnumerable<ControlType> ) ControlType._components.Cache;
        }
    }
}
