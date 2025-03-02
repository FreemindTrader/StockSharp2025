using Ecng.Common;
using Ecng.Configuration;
using StockSharp.Algo;
using StockSharp.Configuration;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security;

namespace StockSharp.Studio.Community
{
    /// <summary>Community helper.</summary>
    public static class CommunityHelper
    {
        /// <summary>Create community adapter.</summary>
        /// <param name="transactionIdGenerator">Transaction id generator.</param>
        /// <param name="address">Address.</param>
        /// <param name="name">Server name.</param>
        /// <param name="clientVersion">Client app version.</param>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <returns>Adapter.</returns>
        public static IMessageAdapter CreateAdapter( this IdGenerator transactionIdGenerator, string address, string name, string clientVersion, string login, SecureString password)
        {
            return ServicesRegistry.AdapterProvider.CreateTransportAdapter(transactionIdGenerator).InitAdapter(address, name, clientVersion, login, password);
        }

        /// <summary>Create community adapter.</summary>
        /// <param name="adapter">Adapter.</param>
        /// <param name="address">Address.</param>
        /// <param name="name">Server name.</param>
        /// <param name="clientVersion">Client app version.</param>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <returns>Adapter.</returns>
        public static IMessageAdapter InitAdapter( this IMessageAdapter adapter, string address, string name, string clientVersion, string login, SecureString password)
        {
            if (adapter == null)
                throw new ArgumentNullException(nameof(adapter));
            IAddressAdapter<EndPoint> addressAdapter = adapter as IAddressAdapter<EndPoint>;
            if (addressAdapter != null)
                addressAdapter.Address = address.To<EndPoint>();
            ILoginPasswordAdapter loginPasswordAdapter = adapter as ILoginPasswordAdapter;
            if (loginPasswordAdapter != null)
            {
                loginPasswordAdapter.Login = login;
                loginPasswordAdapter.Password = password;
            }
            ISenderTargetAdapter senderTargetAdapter = adapter as ISenderTargetAdapter;
            if (senderTargetAdapter != null)
            {
                senderTargetAdapter.SenderCompId = login;
                senderTargetAdapter.TargetCompId = name;
            }
            IClientVersionAdapter clientVersionAdapter = adapter as IClientVersionAdapter;
            if (clientVersionAdapter != null)
                clientVersionAdapter.ClientVersion = clientVersion;
            return adapter;
        }

        /// <summary>Create adapters for StockSharp server connections.</summary>
        /// <param name="transactionIdGenerator">Transaction id generator.</param>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <returns>Adapters for StockSharp server connections.</returns>
        public static IEnumerable<IMessageAdapter> CreateStockSharpAdapters( this IdGenerator transactionIdGenerator, string login, SecureString password)
        {
            if (transactionIdGenerator == null)
                throw new ArgumentNullException(nameof(transactionIdGenerator));
            return ServicesRegistry.AdapterProvider.CreateTransportAdapter(transactionIdGenerator).InitStockSharpAdapters(ServicesRegistry.AdapterProvider.CreateTransportAdapter(transactionIdGenerator), login, password);
        }

        /// <summary>Create adapters for StockSharp server connections.</summary>
        /// <param name="transAdapter">Transactinal adapter.</param>
        /// <param name="dataAdapter">Market-data adapter.</param>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <returns>Adapters for StockSharp server connections.</returns>
        public static IEnumerable<IMessageAdapter> InitStockSharpAdapters( this IMessageAdapter transAdapter, IMessageAdapter dataAdapter, string login, SecureString password)
        {
            transAdapter.InitAdapter(ConfigManager.TryGet( "transAddr", string.Format("stocksharp.com:{0}", 24020 ) ), ConfigManager.TryGet( "transName", "StockSharpTS"), Paths.InstalledVersion, login, password);
            dataAdapter.InitAdapter(ConfigManager.TryGet( "dataAddr", string.Format("stocksharp.com:{0}", 24021 ) ), ConfigManager.TryGet( "dataName", "StockSharpMD"), Paths.InstalledVersion, login, password);
            transAdapter.ChangeSupported(false, true);
            dataAdapter.ChangeSupported(false, false);
            return new IMessageAdapter[2] { transAdapter, dataAdapter };
        }
    }
}
