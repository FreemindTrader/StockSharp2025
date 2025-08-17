// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.WebApi.ClientSocialItemsSource
// Assembly: StockSharp.Studio.WebApi, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B97A7121-FFB7-49F4-8E30-FC5C471868BC
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.xml

using System.Collections.Generic;
using System.Linq;
using Ecng.ComponentModel;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Studio.WebApi;

/// <summary>
/// <see cref="T:Ecng.ComponentModel.IItemsSource" /> implementation for <see cref="T:StockSharp.Web.DomainModel.ClientSocial" />.
/// </summary>
public class ClientSocialItemsSource : ItemsSourceBase<ClientSocial>
{
    /// <inheritdoc />
    protected override string GetName(ClientSocial value) => value.Name;

    /// <inheritdoc />
    protected override IEnumerable<ClientSocial> GetValues()
    {
        return (IEnumerable<ClientSocial>)WebApiHelper.TryClientSocialsSource ?? Enumerable.Empty<ClientSocial>();
    }
}
