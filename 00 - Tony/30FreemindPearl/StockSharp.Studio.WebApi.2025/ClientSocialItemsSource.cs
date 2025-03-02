// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.WebApi.ClientSocialItemsSource
// Assembly: StockSharp.Studio.WebApi, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 54E25E17-EECA-4F64-ACCA-A2705D24CD36
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.WebApi.dll

using Ecng.ComponentModel;
using StockSharp.Web.DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Studio.WebApi
{
    /// <summary>
    /// <see cref="T:Ecng.ComponentModel.IItemsSource" /> implementation for <see cref="T:StockSharp.Web.DomainModel.ClientSocial" />.
    /// </summary>
    public class ClientSocialItemsSource : ItemsSourceBase<ClientSocial>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Studio.WebApi.ClientSocialItemsSource" />.
        /// </summary>
        public ClientSocialItemsSource()
        {
            
        }

        /// <inheritdoc />
        protected override string GetName( ClientSocial value )
        {
            return value.Name;
        }

        /// <inheritdoc />
        protected override IEnumerable<ClientSocial> GetValues()
        {
            return ( IEnumerable<ClientSocial> ) WebApiHelper.TryClientSocialsSource ?? Enumerable.Empty<ClientSocial>();
        }
    }
}
