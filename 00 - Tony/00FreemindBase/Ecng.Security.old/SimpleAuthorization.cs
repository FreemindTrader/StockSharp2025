// Decompiled with JetBrains decompiler
// Type: Ecng.Security.SimpleAuthorization
// Assembly: Ecng.Security, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E35A157-8791-4E76-9265-8307D4E0C0A6
// Assembly location: T:\00-FreemindTrader\packages\ecng.security\1.0.119\lib\net6.0\Ecng.Security.dll

using Ecng.Common;
using System;
using System.Net;
using System.Security;

namespace Ecng.Security
{
  public class SimpleAuthorization : AnonymousAuthorization
  {
    public string Login { get; set; }

    public SecureString Password { get; set; }

    public override string ValidateCredentials(
      string login,
      SecureString password,
      IPAddress clientAddress)
    {
      if (this.Login.IsEmpty())
        return base.ValidateCredentials(login, password, clientAddress);
      if (login.EqualsIgnoreCase(this.Login) && password != null && (this.Password != null && password.IsEqualTo(this.Password)))
        return Guid.NewGuid().To<string>();
      throw new UnauthorizedAccessException();
    }
  }
}
