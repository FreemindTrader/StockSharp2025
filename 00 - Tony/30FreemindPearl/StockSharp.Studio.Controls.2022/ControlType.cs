
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Xaml;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StockSharp.Studio.Controls
{
    public class ControlType
    {
        public Type Type { get; }

        public string Name { get; }

        public string Description { get; }

        public ImageSource Icon { get; }

        public bool IsToolWindow { get; }

        public ControlType( Type type )
          : this( type, type.GetDisplayName( null ), type.GetDescription( null ), type.GetIconUrl() )
        {
            DockingWindowTypeAttribute attribute = type.GetAttribute<DockingWindowTypeAttribute>( true );
            IsToolWindow = attribute != null && attribute.IsToolWindow;
            string icon = type.GetAttribute<VectorIconAttribute>( true )?.Icon;
            if ( icon.IsEmpty() )
                return;
            Icon = ThemedIconsExtension.GetImage( icon );
        }

        public ControlType( Type type, string name, string description, Uri icon )
        {
            Type type1 = type;
            if ( ( object )type1 == null )
                throw new ArgumentNullException( nameof( type ) );
            Type = type1;
            string str = name;
            if ( str == null )
                throw new ArgumentNullException( nameof( name ) );
            Name = str;
            Description = description;
            if ( !( icon != null ) )
                return;
            Icon = new BitmapImage( icon );
        }
    }
}
