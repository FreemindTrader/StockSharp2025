// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.ServerCredentials
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Serialization;
using System.Security;

namespace Ecng.ComponentModel
{
  public class ServerCredentials : NotifiableObject, IPersistable
  {
    private bool _autoLogon = true;
    private string _email;
    private SecureString _password;
    private SecureString _token;

    public string Email
    {
      get
      {
        return this._email;
      }
      set
      {
        this._email = value;
        this.NotifyChanged(nameof (Email));
      }
    }

    public SecureString Password
    {
      get
      {
        return this._password;
      }
      set
      {
        this._password = value;
        this.NotifyChanged(nameof (Password));
      }
    }

    public SecureString Token
    {
      get
      {
        return this._token;
      }
      set
      {
        this._token = value;
        this.NotifyChanged(nameof (Token));
      }
    }

    public bool AutoLogon
    {
      get
      {
        return this._autoLogon;
      }
      set
      {
        this._autoLogon = value;
        this.NotifyChanged(nameof (AutoLogon));
      }
    }

    public virtual void Load(SettingsStorage storage)
    {
      this.Email = storage.GetValue<string>("Email", (string) null);
      this.Password = storage.GetValue<SecureString>("Password", (SecureString) null);
      this.AutoLogon = storage.GetValue<bool>("AutoLogon", false);
      this.Token = storage.GetValue<SecureString>("Token", (SecureString) null);
    }

    public virtual void Save(SettingsStorage storage)
    {
      storage.Set<string>("Email", this.Email).Set<SecureString>("Password", this.Password).Set<bool>("AutoLogon", this.AutoLogon).Set<SecureString>("Token", this.Token);
    }
  }
}
