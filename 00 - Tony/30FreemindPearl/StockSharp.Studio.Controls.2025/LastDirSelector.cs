// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LastDirSelector
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Studio.Core.Services;
using System;

namespace StockSharp.Studio.Controls
{
    internal class LastDirSelector : ILastDirSelector
    {
        private readonly IPersistableService _service;

        public LastDirSelector( IPersistableService service )
        {
            IPersistableService persistableService = service;
            if ( persistableService == null )
                throw new ArgumentNullException( nameof( service ) );
            this._service = persistableService;
        }

        void ILastDirSelector.SetValue( string ctrlName, string value )
        {
            this._service.SetLastDir( ctrlName, value );
        }

        bool ILastDirSelector.TryGetValue( string ctrlName, out string value )
        {
            value = this._service.GetLastDir( ctrlName );
            return !StringHelper.IsEmpty( value );
        }
    }
}
