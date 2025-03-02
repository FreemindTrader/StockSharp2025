﻿// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.AccessToken
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class AccessToken : BaseEntity, IClientEntity, IExpiryEntity
    {
        public Client Client { get; set; }

        public DateTime ExpiryDate { get; set; } = DateTime.MaxValue;

        public AccessTokenScopes Scope { get; set; }

        public string Text { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.ExpiryDate = ( DateTime ) storage.GetValue<DateTime>( "ExpiryDate", new DateTime() );
            this.Scope = ( AccessTokenScopes ) storage.GetValue<AccessTokenScopes>( "Scope", 0L );
            this.Text = ( string ) storage.GetValue<string>( "Text", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<DateTime>( "ExpiryDate", this.ExpiryDate ).Set<AccessTokenScopes>( "Scope", this.Scope ).Set<string>( "Text", this.Text );
        }
    }
}
