// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Windows.TaskInfo
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using Ecng.ComponentModel;

using StockSharp.Localization;
using StockSharp.Messages;
using System;

namespace StockSharpStringDecrypt
{
    public class TaskInfo : NotifiableObject
    {
        private bool _isSelected;
        private bool _isVisible;

        public TaskInfo( Type task )
        {
            Type type = task;
            if ( ( object )type == null )
                throw new ArgumentNullException( nameof( task ) );
            Task = type;
            Name = "Test";
            Description = "Test";
            Icon = new Uri( "https://www.ebay.com" );

            Name = Name + " (ru)";
        }

        public string Name { get; }

        public string Description { get; }

        public Uri Icon { get; }

        public Type Task { get; }

        public event Action Selected;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                NotifyChanged( nameof( IsSelected ) );
                Action selected = Selected;
                if ( selected == null )
                    return;
                selected();
            }
        }

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                NotifyChanged( nameof( IsVisible ) );
            }
        }
    }
}
