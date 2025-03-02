// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.IPC.IpcHelper
// Assembly: StockSharp.Studio.IPC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65FF0FD2-B114-4B6B-959A-42B33214A877
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.IPC.dll

using Ecng.Common;
using System;

namespace StockSharp.Studio.IPC
{
    /// <summary>IPC helper.</summary>
    public static class IpcHelper
    {
        /// <summary>Special channel id for Installer.Console</summary>
        public static readonly long ProductIdInstallerConsole = 2147483648;

        /// <summary>
        /// </summary>
        public static void VerifyIsOk( this StudioMessage response, string format = "unexpected response: {0}" )
        {
            if ( response == null )
                throw new ArgumentNullException( nameof( response ) );
            if ( response is MsgOk )
                return;
            MsgError msgError = response as MsgError;
            if ( msgError != null )
                throw new InvalidOperationException( msgError.ErrorMessage );
            throw new InvalidOperationException( StringHelper.Put( format, new object [1]
            {
         response
            } ) );
        }
    }
}
