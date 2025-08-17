// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.IPC.IpcHelper
// Assembly: StockSharp.Studio.IPC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F4CDD47D-561A-463F-994A-61FC038C2B5F
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.IPC.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.IPC.xml

using System;
using Ecng.Common;

#nullable disable
namespace StockSharp.Studio.IPC;

/// <summary>IPC helper.</summary>
public static class IpcHelper
{
    /// <summary>Special channel id for Installer.Console</summary>
    public static readonly long ProductIdInstallerConsole = 2147483648 /*0x80000000*/;

    /// <summary>
    /// </summary>
    public static void VerifyIsOk(this StudioMessage response, string format = "unexpected response: {0}")
    {
        switch (response)
        {
            case null:
                throw new ArgumentNullException(nameof(response));
            case MsgOk _:
                break;
            case MsgError msgError:
                throw new InvalidOperationException(msgError.ErrorMessage);
            default:
                throw new InvalidOperationException(StringHelper.Put(format, new object[1]
                {
          (object) response
                }));
        }
    }
}
