// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.WebApi.WebApiHelper
// Assembly: StockSharp.Studio.WebApi, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B97A7121-FFB7-49F4-8E30-FC5C471868BC
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Backup;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Logging;
using Ecng.Net;
using Nito.AsyncEx;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Web.Api.Client;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.Common;
using StockSharp.Web.DomainModel;

#nullable enable
namespace StockSharp.Studio.WebApi;

/// <summary>
/// 
/// </summary>
public static class WebApiHelper
{
    private static int _uniqueBugReports;
    private const int _maxUniqueBugReports = 10;
    private static
#nullable disable
    StockSharp.Web.DomainModel.Client _profile;
    private static readonly AsyncLock _sendingBugReportLock = new AsyncLock();
    private static bool _sendingBugReport;
    private static readonly AsyncLock _logsPreparationLock = new AsyncLock();
    private static Task<string> _currentLogsPreparationTask;
    private const long _socialId = 4;
    private static readonly Dictionary<string, ProductContentTypes2> _extensionMap = new Dictionary<string, ProductContentTypes2>((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase)
  {
    {
      ".json",
      ProductContentTypes2.Schema
    },
    {
      ".cs",
      ProductContentTypes2.SourceCode
    },
    {
      ".vb",
      ProductContentTypes2.SourceCode
    },
    {
      ".fs",
      ProductContentTypes2.SourceCode
    },
    {
      ".py",
      ProductContentTypes2.SourceCode
    },
    {
      ".dll",
      ProductContentTypes2.CompiledAssembly
    }
  };

    /// <summary>Current domain id.</summary>
    public static long CurrentDomain => !(Paths.Domain == "com") ? 1L : 2L;

    /// <summary>Current product id.</summary>
    public static long ProductId { get; private set; }

    /// <summary>Get product domain info.</summary>
    /// <param name="product">Product.</param>
    /// <returns>Product domain info.</returns>
    public static ProductDomain GetCurrentDomain(this Product product)
    {
        return product.TryGetDomain(WebApiHelper.CurrentDomain, (Func<ProductDomain, bool>)(d => true));
    }

    /// <summary>Get product group domain info.</summary>
    /// <param name="group">Product group.</param>
    /// <returns>Product group domain info.</returns>
    public static ProductGroupDomain GetCurrentDomain(this ProductGroup group)
    {
        return group.TryGetDomain(WebApiHelper.CurrentDomain, (Func<ProductGroupDomain, bool>)(d => true));
    }

    /// <summary>Get product's name.</summary>
    /// <param name="product">Product.</param>
    /// <returns>Name.</returns>
    public static string GetName(this Product product)
    {
        string name = product.GetCurrentDomain()?.Name;
        if (StringHelper.IsEmpty(name))
            name = StringHelper.IsEmpty(product.PackageId, Converter.To<string>((object)product.Id));
        return name;
    }

    /// <summary>Get group's name.</summary>
    /// <param name="group">Product group.</param>
    /// <returns>Name.</returns>
    public static string GetName(this ProductGroup group)
    {
        string name = group.GetCurrentDomain()?.Name;
        if (StringHelper.IsEmpty(name))
            name = Converter.To<string>((object)group.Id);
        return name;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="onlineMode"></param>
    public static void RegisterWebApiServices(this Type owner, bool? onlineMode = null)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }
            
        onlineMode.GetValueOrDefault();

        if (!onlineMode.HasValue)
        {
            AppStartSettings appStartSettings = AppStartSettings.TryLoad();
            onlineMode = new bool?(appStartSettings == null || appStartSettings.Online);
        }

        WebApiHelper.ProductId = StockSharp.Configuration.Extensions.TryGetProductId() ?? throw new InvalidOperationException("ProductIdAttribute is missing.");
        ConfigManager.RegisterService<IApiServiceProvider>((IApiServiceProvider)new ApiServiceProvider());
        ConfigManager.RegisterService<ICredentialsProvider>((ICredentialsProvider)new DefaultCredentialsProvider());
                        
        WebApiServicesRegistry.Offline = onlineMode.GetValueOrDefault() == false & onlineMode.HasValue; //&& StringHelper.IsEmpty("Offline".ValidateLicense(component: owner));

        if (WebApiServicesRegistry.Offline)
        {
            WebApiHelper.Profile = new StockSharp.Web.DomainModel.Client()
            {
                DisplayName = "Offline"
            };
        }
        else
        {
            ConfigManager.RegisterService<IBackupService>((IBackupService)new StockSharpBackupService());
            ObservableCollection<ClientSocial> observableCollection = new ObservableCollection<ClientSocial>();
            ConfigManager.RegisterService<IList<ClientSocial>>((IList<ClientSocial>)observableCollection);
            ConfigManager.RegisterService<IEnumerable<ITelegramChannel>>((IEnumerable<ITelegramChannel>)observableCollection);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static Task InitServices(
      Func<IWebSocketService, CancellationToken, ValueTask> socketConnected,
      CancellationToken token)
    {
        ConfigManager.RegisterService<IInstrumentInfoService>(WebApiServicesRegistry.GetService<IInstrumentInfoService>());
        return Task.Run((Func<Task>)(async () =>
        {
            ISessionService sessionSvc = WebApiServicesRegistry.GetService<ISessionService>();
            try
            {
                IWebSocketService socket = WebApiServicesRegistry.GetService<IWebSocketService>();
                ConfigManager.RegisterService<IWebSocketService>(socket);
                LogManager.Instance?.Sources.Add((ILogSource)socket);
                await socket.ConnectAsync(token);
                if (socketConnected != null)
                    await socketConnected(socket, token);
                socket = (IWebSocketService)null;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(ex, (string)null);
            }
            ISessionService sessionService = sessionSvc;
            Session entity = new Session();
            entity.Product = new Product()
            {
                Id = WebApiHelper.ProductId
            };
            entity.Version = Paths.InstalledVersion;
            CancellationToken cancellationToken = token;
            Session session = await sessionService.AddAsync(entity, cancellationToken);
            TimeSpan interval = TimeSpan.FromMinutes(5.0);
            int unauth = 0;
            while (true)
            {
                try
                {
                    await AsyncHelper.Delay(interval, token);
                    Session session1 = await sessionSvc.UpdateAsync(session, token);
                    unauth = 0;
                }
                catch (Exception ex)
                {
                    if (!token.IsCancellationRequested)
                    {
                        LoggingHelper.LogError(ex, (string)null);
                        if (ex is HttpRequestException requestException2)
                        {
                            HttpStatusCode? statusCode = NetworkHelper.TryGetStatusCode(requestException2);
                            if (statusCode.HasValue)
                            {
                                switch (statusCode.GetValueOrDefault())
                                {
                                    case HttpStatusCode.Unauthorized:
                                    case HttpStatusCode.Forbidden:
                                        if (++unauth > 5)
                                        {
                                            sessionSvc = (ISessionService)null;
                                            session = (Session)null;
                                            return;
                                        }
                                        continue;
                                    default:
                                        continue;
                                }
                            }
                        }
                    }
                    else
                        break;
                }
            }
            session.Logout = true;
            try
            {
                Session session2 = await sessionSvc.UpdateAsync(session);
                sessionSvc = (ISessionService)null;
                session = (Session)null;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(ex, (string)null);
                sessionSvc = (ISessionService)null;
                session = (Session)null;
            }
        }), token);
    }

    private static IMessageAdapterProvider AdapterProvider
    {
        get => ConfigManager.GetService<IMessageAdapterProvider>();
    }

    /// <summary>Create community adapter.</summary>
    /// <param name="adapter">Adapter.</param>
    /// <param name="address">Address.</param>
    /// <param name="name">Server name.</param>
    /// <param name="login">Login.</param>
    /// <param name="password">Password.</param>
    /// <returns>Adapter.</returns>
    public static IMessageAdapter InitAdapter(
      this IMessageAdapter adapter,
      string address,
      string name,
      string login,
      SecureString password)
    {
        if (adapter == null)
            throw new ArgumentNullException(nameof(adapter));
        if (adapter is IAddressAdapter<EndPoint> iaddressAdapter)
            iaddressAdapter.Address = Converter.To<EndPoint>((object)address);
        if (adapter is ILoginPasswordAdapter iloginPasswordAdapter)
        {
            iloginPasswordAdapter.Login = login;
            iloginPasswordAdapter.Password = password;
        }
        if (adapter is ISenderTargetAdapter isenderTargetAdapter)
        {
            isenderTargetAdapter.SenderCompId = login;
            isenderTargetAdapter.TargetCompId = name;
        }
        return adapter;
    }

    /// <summary>Create adapters for StockSharp server connections.</summary>
    /// <param name="transactionIdGenerator">Transaction id generator.</param>
    /// <param name="login">Login.</param>
    /// <param name="password">Password.</param>
    /// <returns>Adapters for StockSharp server connections.</returns>
    public static IEnumerable<IMessageAdapter> CreateStockSharpAdapters(this IdGenerator transactionIdGenerator, string login, SecureString password)
    {
        var adapter = transactionIdGenerator != null ? WebApiHelper.AdapterProvider.CreateTransportAdapter(transactionIdGenerator) : throw new ArgumentNullException(nameof(transactionIdGenerator));
        var transportAdapter = WebApiHelper.AdapterProvider.CreateTransportAdapter(transactionIdGenerator);
        adapter.InitAdapter(ConfigManager.TryGet<string>("transAddr", $"stocksharp.com:{24020}"), ConfigManager.TryGet<string>("transName", "StockSharpTS"), login, password);
        transportAdapter.InitAdapter(ConfigManager.TryGet<string>("dataAddr", $"stocksharp.com:{24021}"), ConfigManager.TryGet<string>("dataName", "StockSharpMD"), login, password);
        StockSharp.Messages.Extensions.ChangeSupported(adapter, false, true);
        StockSharp.Messages.Extensions.ChangeSupported(transportAdapter, false, false);
        
        return new ReadOnlyCollection<IMessageAdapter>(new IMessageAdapter[2] {adapter, transportAdapter });
    }

    /// <summary>Profile.</summary>
    public static StockSharp.Web.DomainModel.Client Profile
    {
        get => WebApiHelper._profile;
        set
        {
            WebApiHelper._profile = value;
            Action profileChanged = WebApiHelper.ProfileChanged;
            if (profileChanged == null)
                return;
            profileChanged();
        }
    }

    /// <summary>
    /// <see cref="P:StockSharp.Studio.WebApi.WebApiHelper.Profile" /> changed event.
    /// </summary>
    public static event Action ProfileChanged;

    /// <summary>
    /// </summary>
    public static async Task<ProductBugReport> TrySendBugReport(this LogMessage log, CancellationToken cancellationToken)
    {
        if (log.Level == LogLevels.Error && WebApiHelper._uniqueBugReports < 10)
        {
            StockSharp.Web.DomainModel.Client profile = WebApiHelper.Profile;
            if ((profile != null ? (!profile.IsAllowStatistics.GetValueOrDefault() ? 1 : 0) : 1) == 0 && !WebApiHelper._sendingBugReport)
            {
                using (await WebApiHelper._sendingBugReportLock.LockAsync(cancellationToken))
                {
                    if (WebApiHelper._sendingBugReport)
                        return null;
                    WebApiHelper._sendingBugReport = true;
                }
                Exception myException = null;
                int errorCount = 0;
                ProductBugReport productBugReport = null;

                try
                {
                    IProductBugReportService service1 = WebApiServicesRegistry.GetService<IProductBugReportService>();
                    ProductBugReport entity = new ProductBugReport();
                    Product product = new Product();
                    product.Id = WebApiHelper.ProductId;
                    entity.Product = product;
                    entity.Version = Paths.InstalledVersion;
                    entity.SystemInfo = WebApiHelper.GetSystemInfo();
                    entity.Framework = RuntimeInformation.FrameworkDescription;
                    entity.Lang = LocalizedStrings.ActiveLanguage;
                    entity.Message = new StockSharp.Web.DomainModel.Message()
                    {
                        Text = log.Message
                    };
                    CancellationToken cancellationToken1 = cancellationToken;
                    ProductBugReport bugReport = await service1.TryProposeAsync(entity, cancellationToken1);
                    if (bugReport == null)
                    {
                        productBugReport = (ProductBugReport)null;
                    }
                    else
                    {
                        ++WebApiHelper._uniqueBugReports;
                        string zip = await TimeSpan.FromDays(1.0).PrepareLogsFile((Action<Exception>)(ex => { }), cancellationToken);
                        if (zip != null)
                        {
                            try
                            {
                                using (FileStream body1 = System.IO.File.OpenRead(zip))
                                {
                                    IFileService service2 = WebApiServicesRegistry.GetService<IFileService>();
                                    StockSharp.Web.DomainModel.File file1 = new StockSharp.Web.DomainModel.File();
                                    file1.Message = bugReport.Message;
                                    file1.Name = Path.GetFileName(zip);
                                    FileStream body2 = body1;
                                    CancellationToken cancellationToken2 = cancellationToken;
                                    Compressions? compression = new Compressions?();
                                    CancellationToken cancellationToken3 = cancellationToken2;
                                    StockSharp.Web.DomainModel.File file2 = await service2.UploadFullAsync(file1, (Stream)body2, compression: compression, cancellationToken: cancellationToken3);
                                }
                            }
                            finally
                            {
                                try
                                {
                                    System.IO.File.Delete(zip);
                                }
                                catch
                                {
                                }
                            }
                        }
                        productBugReport = bugReport;
                    }
                    errorCount = 1;
                }
                catch (Exception ex)
                {
                    myException = ex;
                }
                using (await WebApiHelper._sendingBugReportLock.LockAsync(cancellationToken))
                    WebApiHelper._sendingBugReport = false;
                
                if (myException != null)
                {
                    if (!(myException is Exception source))
                        throw myException;
                    ExceptionDispatchInfo.Capture(source).Throw();
                }
                if (errorCount == 1)
                    return productBugReport;
                myException = null;
                
                return null;
            }
        }
        return null;
    }

    /// <summary>
    /// </summary>
    public static string GetSystemInfo()
    {
        return $"64bit: {Environment.Is64BitProcess}\r\nOS: {Environment.OSVersion}";
    }

    /// <summary>
    /// </summary>
    public static async Task<string> PrepareLogsFile(this TimeSpan duration, Action<Exception> errorHandler, CancellationToken cancellationToken)
    {
        if (errorHandler == null)
            throw new ArgumentNullException(nameof(errorHandler));

        using (await WebApiHelper._logsPreparationLock.LockAsync(cancellationToken))
        {
            if (WebApiHelper._currentLogsPreparationTask != null)
                return await WebApiHelper._currentLogsPreparationTask;
            WebApiHelper._currentLogsPreparationTask = Task.Run<string>((Func<string>)(() =>
            {
                string path2 = $"logs_{DateTime.Today:yyyyMMdd}";
                LogManager instance = LogManager.Instance;
                string logDirectory = instance != null ? instance.Listeners.OfType<FileLogListener>().FirstOrDefault<FileLogListener>()?.LogDirectory : (string)null;
                if (StringHelper.IsEmpty(logDirectory))
                    return null;

                string str1 = Path.Combine(Paths.ReportLogsPath, path2);
                string str2 = str1 + ".zip";
                IOHelper.SafeDeleteDir(str1);
                try
                {
                    Directory.CreateDirectory(str1);
                    DateTime dateTime = duration == TimeSpan.MaxValue ? DateTime.MinValue : DateTime.Today - duration;
                    foreach (string directory in Directory.GetDirectories(logDirectory))
                    {
                        try
                        {
                            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                            if (!(directoryInfo.LastWriteTime < dateTime))
                            {
                                string path = Path.Combine(str1, directoryInfo.Name);
                                Directory.CreateDirectory(path);
                                IOHelper.CopyDirectory(directoryInfo.FullName, path);
                            }
                        }
                        catch (Exception ex)
                        {
                            errorHandler(ex);
                        }
                    }
                    if (System.IO.File.Exists(str2))
                        System.IO.File.Delete(str2);
                    ZipFile.CreateFromDirectory(str1, str2);
                }
                finally
                {
                    try
                    {
                        IOHelper.SafeDeleteDir(str1);
                    }
                    catch
                    {
                    }
                }
                return str2;
            }));
        }
        Exception myException = null;
        int num = 0;
        string str = "";
        try
        {
            str = await WebApiHelper._currentLogsPreparationTask;
            num = 1;
        }
        catch (Exception ex)
        {
            myException = ex;
        }

        using (await WebApiHelper._logsPreparationLock.LockAsync(cancellationToken))
        {
            WebApiHelper._currentLogsPreparationTask = null;
        }
            
        
        if (myException != null)
        {
            if (!(myException is Exception source))
                throw myException;
            ExceptionDispatchInfo.Capture(source).Throw();
        }
        if (num == 1)
            return str;
        
        myException = null;
        str = null;
        
        return null;
    }

    /// <summary>
    /// <see cref="T:StockSharp.Web.DomainModel.ClientSocial" /> items source.
    /// </summary>
    public static IList<ClientSocial> ClientSocialsSource
    {
        get => ConfigManager.GetService<IList<ClientSocial>>();
    }

    /// <summary>
    /// <see cref="T:StockSharp.Web.DomainModel.ClientSocial" /> items source.
    /// </summary>
    public static IList<ClientSocial> TryClientSocialsSource
    {
        get => ConfigManager.TryGetService<IList<ClientSocial>>();
    }

    /// <summary>
    /// </summary>
    public static ClientSocial DefaultClientSocial { get; set; }

    /// <summary>
    /// </summary>
    public static async Task RefreshAsync(
      this IClientSocialService svc,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        IClientSocialService clientSocialService = svc != null ? svc : throw new ArgumentNullException(nameof(svc));
        long? nullable = new long?(4L);
        CancellationToken cancellationToken1 = cancellationToken;
        long? count = new long?();
        bool? deleted = new bool?();
        bool? orderByDesc = new bool?();
        bool? totalCount = new bool?();
        DateTime? creationStart = new DateTime?();
        DateTime? creationEnd = new DateTime?();
        long? clientId = new long?();
        long? socialId = nullable;
        long? domainId = new long?();
        ComparisonOperator? likeCompare = new ComparisonOperator?();
        CancellationToken cancellationToken2 = cancellationToken1;
        BaseEntitySet<ClientSocial> async = await clientSocialService.FindAsync(count: count, deleted: deleted, orderByDesc: orderByDesc, totalCount: totalCount, creationStart: creationStart, creationEnd: creationEnd, clientId: clientId, socialId: socialId, domainId: domainId, likeCompare: likeCompare, cancellationToken: cancellationToken2);
        IList<ClientSocial> clientSocialsSource = WebApiHelper.ClientSocialsSource;
        clientSocialsSource.Clear();
        CollectionHelper.AddRange<ClientSocial>((ICollection<ClientSocial>)clientSocialsSource, (IEnumerable<ClientSocial>)async.Items);
        WebApiHelper.DefaultClientSocial = clientSocialsSource.FirstOrDefault<ClientSocial>();
    }

    /// <summary>
    /// Convert file extension to <see cref="T:StockSharp.Web.DomainModel.ProductContentTypes2" />.
    /// </summary>
    /// <param name="extension">File extension.</param>
    /// <returns><see cref="T:StockSharp.Web.DomainModel.ProductContentTypes2" /></returns>
    public static ProductContentTypes2 ToContentType(this string extension)
    {
        return WebApiHelper._extensionMap[extension];
    }

    /// <summary>
    /// </summary>
    public static Func<App> CreateApp(long pictureId)
    {
        return (Func<App>)(() =>
        {
            return new App()
            {
                Name = Paths.AppName2,
                Picture = new StockSharp.Web.DomainModel.File() { Id = pictureId },
                LocalPath = Environment.GetCommandLineArgs()[0],
                Version = Paths.InstalledVersion,
                UserId = Paths.AppName2 + Ecng.Interop.HardwareInfo.GetId()
            };
        });
    }

    /// <summary>
    /// </summary>
    public static CurrencyTypes GetCurrency(this ProductDomain domain)
    {
        return domain?.Domain?.Currency ?? (CurrencyTypes)149;
    }

    /// <summary>
    /// </summary>
    public static bool IsAvailable(this Product product)
    {
        return !StringHelper.IsEmpty(TypeHelper.CheckOnNull<Product>(product, nameof(product)).AvailableVersion);
    }

    /// <summary>
    /// </summary>
    public static string GetUrlRelative(this Product product)
    {
        return product.GetUrlPart(WebApiHelper.CurrentDomain);
    }

    /// <summary>
    /// </summary>
    public static string GetUrlAbsolute(this Product product)
    {
        return Paths.GetPageUrl(164L, (object)product.GetUrlRelative());
    }
}
