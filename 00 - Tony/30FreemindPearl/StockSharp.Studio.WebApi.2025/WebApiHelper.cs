// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.WebApi.WebApiHelper
// Assembly: StockSharp.Studio.WebApi, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 54E25E17-EECA-4F64-ACCA-A2705D24CD36
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.WebApi.dll

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
using StockSharp.Web.DomainModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using static StockSharp.Web.DomainModel.Ids;
using Product = StockSharp.Web.DomainModel.Product;
using ProductGroup = StockSharp.Web.DomainModel.ProductGroup;

namespace StockSharp.Studio.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiHelper
    {
        private static readonly AsyncLock _sendingBugReportLock = new AsyncLock();
        private static readonly AsyncLock _logsPreparationLock = new AsyncLock();
        private static readonly Dictionary<string, ProductContentTypes2> _extensionMap = new Dictionary<string, ProductContentTypes2>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase)
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
        private static int _uniqueBugReports;
        private const int _maxUniqueBugReports = 10;
        private static StockSharp.Web.DomainModel.Client _profile;
        private static bool _sendingBugReport;
        private static Task<string> _currentLogsPreparationTask;
        private const long _socialId = 4;

        /// <summary>Current domain id.</summary>
        public static long CurrentDomain
        {
            get
            {
                return !( Paths.Domain == "com" ) ? 1L : 2L;
            }
        }

        /// <summary>Current product id.</summary>
        public static long ProductId { get; private set; }

        /// <summary>Get product domain info.</summary>
        /// <param name="product">Product.</param>
        /// <returns>Product domain info.</returns>
        public static ProductDomain GetCurrentDomain( this Product product )
        {            
            return StockSharp.Web.Common.ProductExtensions.TryGetDomain( product, WebApiHelper.CurrentDomain, ( d => true ) );
        }

        /// <summary>Get product group domain info.</summary>
        /// <param name="group">Product group.</param>
        /// <returns>Product group domain info.</returns>
        public static ProductGroupDomain GetCurrentDomain( this ProductGroup group )
        {
            return  StockSharp.Web.Common.ProductExtensions.TryGetDomain( group, WebApiHelper.CurrentDomain, ( d => true ) );
            
        }

        /// <summary>Get product's name.</summary>
        /// <param name="product">Product.</param>
        /// <returns>Name.</returns>
        public static string GetName( this Product product )
        {
            string str = product.GetCurrentDomain()?.Name;
            if ( StringHelper.IsEmpty( str ) )
                str = StringHelper.IsEmpty( product.PackageId, ( string ) Converter.To<string>( ( object ) product.Id ) );
            return str;
        }

        /// <summary>Get group's name.</summary>
        /// <param name="group">Product group.</param>
        /// <returns>Name.</returns>
        public static string GetName( this ProductGroup group )
        {
            string name = group.GetCurrentDomain()?.Name;
            if ( StringHelper.IsEmpty( name ) )
                name = ( string ) Converter.To<string>( ( object ) group.Id );
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="onlineMode"></param>
        public static void RegisterWebApiServices( this Type owner, bool? onlineMode = null )
        {
            throw new NotImplementedException();

            //if ( ( object ) owner == null )
            //    throw new ArgumentNullException( nameof( owner ) );
            //onlineMode.GetValueOrDefault();
            //if ( !onlineMode.HasValue )
            //{
            //    AppStartSettings appStartSettings = AppStartSettings.TryLoad();
            //    onlineMode = new bool?( appStartSettings == null || appStartSettings.Online );
            //}
            //long? productId = StockSharp.Configuration.Extensions.TryGetProductId();
            //if ( !productId.HasValue )
            //    throw new InvalidOperationException( "ProductIdAttribute is missing." );
            //WebApiHelper.ProductId = productId.GetValueOrDefault();
            //ConfigManager.RegisterService<IApiServiceProvider>( ( IApiServiceProvider ) new ApiServiceProvider() );
            //ConfigManager.RegisterService<ICredentialsProvider>( ( ICredentialsProvider ) new DefaultCredentialsProvider() );
            //bool? nullable = onlineMode;
            //bool flag = false;
            //WebApiServicesRegistry.Offline = nullable.GetValueOrDefault() == flag & nullable.HasValue && StringHelper.IsEmpty( "Offline".ValidateLicense( ( string ) null, owner ) );
            //if ( WebApiServicesRegistry.Offline )
            //{
            //    WebApiHelper.Profile = new StockSharp.Web.DomainModel.Client()
            //    {
            //        DisplayName = "Offline"
            //    };
            //}
            //else
            //{
            //    ConfigManager.RegisterService<IBackupService>( ( IBackupService ) new StockSharpBackupService() );
            //    ObservableCollection<ClientSocial> observableCollection = new ObservableCollection<ClientSocial>();
            //    ConfigManager.RegisterService<IList<ClientSocial>>( ( IList<ClientSocial> ) observableCollection );
            //    ConfigManager.RegisterService<IEnumerable<ITelegramChannel>>( ( IEnumerable<ITelegramChannel> ) observableCollection );
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        public static Task InitServices( Func<IWebSocketService, CancellationToken, ValueTask> socketConnected, CancellationToken token )
        {
            throw new NotImplementedException();

            //ConfigManager.RegisterService<IInstrumentInfoService>( WebApiServicesRegistry.GetService<IInstrumentInfoService>() );
            //return Task.Run( ( Func<Task> ) ( async () =>
            //{
            //    // ISSUE: explicit reference operation
            //    // ISSUE: reference to a compiler-generated field
            //    int num1 = (^this).\u003C\u003E1__state;
            //    ISessionService sessionSvc;
            //    Session session;
            //    try
            //    {
            //        sessionSvc = WebApiServicesRegistry.GetService<ISessionService>();
            //        try
            //        {
            //            IWebSocketService socket = WebApiServicesRegistry.GetService<IWebSocketService>();
            //            ConfigManager.RegisterService<IWebSocketService>( socket );
            //            ( ( ICollection<ILogSource> ) LogManager.get_Instance()?.get_Sources() ).Add( ( ILogSource ) socket );
            //            ValueTaskAwaiter awaiter1 = socket.ConnectAsync(token).GetAwaiter();
            //            ValueTaskAwaiter valueTaskAwaiter;
            //            if ( !awaiter1.IsCompleted )
            //            {
            //                // ISSUE: explicit reference operation
            //                // ISSUE: reference to a compiler-generated field
            //                ( ^this ).\u003C\u003E1__state = num1 = 0;
            //                valueTaskAwaiter = awaiter1;
            //                // ISSUE: explicit reference operation
            //                // ISSUE: reference to a compiler-generated field
            //                ( ^this ).\u003C\u003Et__builder.AwaitUnsafeOnCompleted < ValueTaskAwaiter, WebApiHelper.\u003C\u003Ec__DisplayClass11_0.\u003C\u003CInitServices\u003Eb__0\u003Ed > (ref awaiter1, this);
            //                return;
            //            }
            //            awaiter1.GetResult();
            //            if ( socketConnected != null )
            //            {
            //                ValueTaskAwaiter awaiter2 = socketConnected(socket, token).GetAwaiter();
            //                if ( !awaiter2.IsCompleted )
            //                {
            //                    // ISSUE: explicit reference operation
            //                    // ISSUE: reference to a compiler-generated field
            //                    ( ^this ).\u003C\u003E1__state = num1 = 1;
            //                    valueTaskAwaiter = awaiter2;
            //                    // ISSUE: explicit reference operation
            //                    // ISSUE: reference to a compiler-generated field
            //                    ( ^this ).\u003C\u003Et__builder.AwaitUnsafeOnCompleted < ValueTaskAwaiter, WebApiHelper.\u003C\u003Ec__DisplayClass11_0.\u003C\u003CInitServices\u003Eb__0\u003Ed > (ref awaiter2, this);
            //                    return;
            //                }
            //                awaiter2.GetResult();
            //            }
            //            socket = ( IWebSocketService ) null;
            //        }
            //        catch ( Exception ex )
            //        {
            //            LoggingHelper.LogError( ex, ( string ) null );
            //        }
            //        ISessionService sessionService = sessionSvc;
            //        Session entity = new Session();
            //        entity.Product = new Product()
            //        {
            //            Id = WebApiHelper.ProductId
            //        };
            //        entity.Version = Paths.InstalledVersion;
            //        CancellationToken cancellationToken = token;
            //        TaskAwaiter<Session> awaiter3 = sessionService.AddAsync(entity, cancellationToken).GetAwaiter();
            //        TaskAwaiter<Session> taskAwaiter1;
            //        int num2;
            //        if ( !awaiter3.IsCompleted )
            //        {
            //            // ISSUE: explicit reference operation
            //            // ISSUE: reference to a compiler-generated field
            //            ( ^this ).\u003C\u003E1__state = num2 = 2;
            //            taskAwaiter1 = awaiter3;
            //            // ISSUE: explicit reference operation
            //            // ISSUE: reference to a compiler-generated field
            //            ( ^this ).\u003C\u003Et__builder.AwaitUnsafeOnCompleted < TaskAwaiter<Session>, WebApiHelper.\u003C\u003Ec__DisplayClass11_0.\u003C\u003CInitServices\u003Eb__0\u003Ed > (ref awaiter3, this);
            //            return;
            //        }
            //        session = awaiter3.GetResult();
            //        TimeSpan interval = TimeSpan.FromMinutes(5.0);
            //        int unauth = 0;
            //        while ( true )
            //        {
            //            try
            //            {
            //                TaskAwaiter awaiter1 = AsyncHelper.Delay(interval, token).GetAwaiter();
            //                if ( !awaiter1.IsCompleted )
            //                {
            //                    // ISSUE: explicit reference operation
            //                    // ISSUE: reference to a compiler-generated field
            //                    ( ^this ).\u003C\u003E1__state = num1 = 3;
            //                    TaskAwaiter taskAwaiter2 = awaiter1;
            //                    // ISSUE: explicit reference operation
            //                    // ISSUE: reference to a compiler-generated field
            //                    ( ^this ).\u003C\u003Et__builder.AwaitUnsafeOnCompleted < TaskAwaiter, WebApiHelper.\u003C\u003Ec__DisplayClass11_0.\u003C\u003CInitServices\u003Eb__0\u003Ed > (ref awaiter1, this);
            //                    return;
            //                }
            //                awaiter1.GetResult();
            //                TaskAwaiter<Session> awaiter2 = sessionSvc.UpdateAsync(session, token).GetAwaiter();
            //                if ( !awaiter2.IsCompleted )
            //                {
            //                    // ISSUE: explicit reference operation
            //                    // ISSUE: reference to a compiler-generated field
            //                    ( ^this ).\u003C\u003E1__state = num1 = 4;
            //                    taskAwaiter1 = awaiter2;
            //                    // ISSUE: explicit reference operation
            //                    // ISSUE: reference to a compiler-generated field
            //                    ( ^this ).\u003C\u003Et__builder.AwaitUnsafeOnCompleted < TaskAwaiter<Session>, WebApiHelper.\u003C\u003Ec__DisplayClass11_0.\u003C\u003CInitServices\u003Eb__0\u003Ed > (ref awaiter2, this);
            //                    return;
            //                }
            //                awaiter2.GetResult();
            //                unauth = 0;
            //            }
            //            catch ( Exception ex1 )
            //            {
            //                if ( !token.IsCancellationRequested )
            //                {
            //                    LoggingHelper.LogError( ex1, ( string ) null );
            //                    HttpRequestException ex2 = ex1 as HttpRequestException;
            //                    if ( ex2 != null )
            //                    {
            //                        HttpStatusCode? statusCode = ex2.TryGetStatusCode();
            //                        if ( statusCode.HasValue )
            //                        {
            //                            switch ( statusCode.GetValueOrDefault() )
            //                            {
            //                                case HttpStatusCode.Unauthorized:
            //                                case HttpStatusCode.Forbidden:
            //                                if ( ++unauth <= 5 )
            //                                    continue;
            //                                goto label_32;
            //                                default:
            //                                continue;
            //                            }
            //                        }
            //                    }
            //                }
            //                else
            //                    break;
            //            }
            //        }
            //        session.Logout = true;
            //        try
            //        {
            //            TaskAwaiter<Session> awaiter1 = sessionSvc.UpdateAsync(session, new CancellationToken()).GetAwaiter();
            //            if ( !awaiter1.IsCompleted )
            //            {
            //                // ISSUE: explicit reference operation
            //                // ISSUE: reference to a compiler-generated field
            //                ( ^this ).\u003C\u003E1__state = num2 = 5;
            //                taskAwaiter1 = awaiter1;
            //                // ISSUE: explicit reference operation
            //                // ISSUE: reference to a compiler-generated field
            //                ( ^this ).\u003C\u003Et__builder.AwaitUnsafeOnCompleted < TaskAwaiter<Session>, WebApiHelper.\u003C\u003Ec__DisplayClass11_0.\u003C\u003CInitServices\u003Eb__0\u003Ed > (ref awaiter1, this);
            //                return;
            //            }
            //            awaiter1.GetResult();
            //        }
            //        catch ( Exception ex )
            //        {
            //            LoggingHelper.LogError( ex, ( string ) null );
            //        }
            //    }
            //    catch ( Exception ex )
            //    {
            //        // ISSUE: explicit reference operation
            //        // ISSUE: reference to a compiler-generated field
            //        ( ^this ).\u003C\u003E1__state = -2;
            //        sessionSvc = ( ISessionService ) null;
            //        session = ( Session ) null;
            //        // ISSUE: explicit reference operation
            //        // ISSUE: reference to a compiler-generated field
            //        ( ^this ).\u003C\u003Et__builder.SetException( ex );
            //        return;
            //    }
            //label_32:
            //    // ISSUE: explicit reference operation
            //    // ISSUE: reference to a compiler-generated field
            //    ( ^this ).\u003C\u003E1__state = -2;
            //    sessionSvc = ( ISessionService ) null;
            //    session = ( Session ) null;
            //    // ISSUE: explicit reference operation
            //    // ISSUE: reference to a compiler-generated field
            //    ( ^this ).\u003C\u003Et__builder.SetResult();
            //} ), token );
        }

        private static IMessageAdapterProvider AdapterProvider
        {
            get
            {
                return ConfigManager.GetService<IMessageAdapterProvider>();
            }
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
          SecureString password )
        {
            if ( adapter == null )
                throw new ArgumentNullException( nameof( adapter ) );
            IAddressAdapter<EndPoint> addressAdapter = adapter as IAddressAdapter<EndPoint>;
            if ( addressAdapter != null )
                addressAdapter.Address = ( EndPoint ) Converter.To<EndPoint>( ( object ) address );
            ILoginPasswordAdapter loginPasswordAdapter = adapter as ILoginPasswordAdapter;
            if ( loginPasswordAdapter != null )
            {
                loginPasswordAdapter.Login = login;
                loginPasswordAdapter.Password = password;
            }
            ISenderTargetAdapter senderTargetAdapter = adapter as ISenderTargetAdapter;
            if ( senderTargetAdapter != null )
            {
                senderTargetAdapter.SenderCompId = login;
                senderTargetAdapter.TargetCompId = name;
            }
            return adapter;
        }

        /// <summary>Create adapters for StockSharp server connections.</summary>
        /// <param name="transactionIdGenerator">Transaction id generator.</param>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <returns>Adapters for StockSharp server connections.</returns>
        public static IEnumerable<IMessageAdapter> CreateStockSharpAdapters( this IdGenerator transactionIdGenerator, string login, SecureString password )
        {
            throw new NotImplementedException();

        //    if ( transactionIdGenerator == null )
        //        throw new ArgumentNullException( nameof( transactionIdGenerator ) );
        //    IMessageAdapter transportAdapter1 = WebApiHelper.AdapterProvider.CreateTransportAdapter(transactionIdGenerator);
        //    IMessageAdapter transportAdapter2 = WebApiHelper.AdapterProvider.CreateTransportAdapter(transactionIdGenerator);
        //    IMessageAdapter adapter1 = transportAdapter1;
        //    DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
        //    interpolatedStringHandler.AppendLiteral( "stocksharp.com:" );
        //    interpolatedStringHandler.AppendFormatted<int>( 24020 );
        //    string address1 = ConfigManager.TryGet<string>("transAddr", interpolatedStringHandler.ToStringAndClear());
        //    string name1 = ConfigManager.TryGet<string>("transName", "StockSharpTS");
        //    string login1 = login;
        //    SecureString password1 = password;
        //    adapter1.InitAdapter( address1, name1, login1, password1 );
        //    IMessageAdapter adapter2 = transportAdapter2;
        //    interpolatedStringHandler = new DefaultInterpolatedStringHandler( 15, 1 );
        //    interpolatedStringHandler.AppendLiteral( "stocksharp.com:" );
        //    interpolatedStringHandler.AppendFormatted<int>( 24021 );
        //    string address2 = ConfigManager.TryGet<string>("dataAddr", interpolatedStringHandler.ToStringAndClear());
        //    string name2 = ConfigManager.TryGet<string>("dataName", "StockSharpMD");
        //    string login2 = login;
        //    SecureString password2 = password;
        //    adapter2.InitAdapter( address2, name2, login2, password2 );
        //    transportAdapter1.ChangeSupported( false, true );
        //    transportAdapter2.ChangeSupported( false, false );
        //    // ISSUE: object of a compiler-generated type is created
        //    return ( IEnumerable<IMessageAdapter> ) new \u003C\u003Ez__ReadOnlyArray<IMessageAdapter>( new IMessageAdapter [2]
        //    {
        //transportAdapter1,
        //transportAdapter2
        //    } );
        }

        /// <summary>Profile.</summary>
        public static StockSharp.Web.DomainModel.Client Profile
        {
            get
            {
                return WebApiHelper._profile;
            }
            set
            {
                WebApiHelper._profile = value;
                Action profileChanged = WebApiHelper.ProfileChanged;
                if ( profileChanged == null )
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
        public static async Task<ProductBugReport> TrySendBugReport( this LogMessage log, CancellationToken cancellationToken )
        {
            throw new NotImplementedException();

            //if ( log.get_Level() == 5 && WebApiHelper._uniqueBugReports < 10 )
            //{
            //    StockSharp.Web.DomainModel.Client profile = WebApiHelper.Profile;
            //    if ( ( profile != null ? ( !profile.IsAllowStatistics.GetValueOrDefault() ? 1 : 0 ) : 1 ) == 0 && !WebApiHelper._sendingBugReport )
            //    {
            //        using ( await WebApiHelper._sendingBugReportLock.LockAsync( cancellationToken ) )
            //        {
            //            if ( WebApiHelper._sendingBugReport )
            //                return ( ProductBugReport ) null;
            //            WebApiHelper._sendingBugReport = true;
            //        }
            //        object obj =  null;
            //        int num = 0;
            //        ProductBugReport productBugReport;
            //        try
            //        {
            //            IProductBugReportService service1 = WebApiServicesRegistry.GetService<IProductBugReportService>();
            //            ProductBugReport entity = new ProductBugReport();
            //            Product product = new Product();
            //            product.Id = WebApiHelper.ProductId;
            //            entity.Product = product;
            //            entity.Version = Paths.InstalledVersion;
            //            entity.SystemInfo = WebApiHelper.GetSystemInfo();
            //            entity.Framework = RuntimeInformation.FrameworkDescription;
            //            entity.Lang = LocalizedStrings.ActiveLanguage;
            //            entity.Message = new StockSharp.Web.DomainModel.Message()
            //            {
            //                Text = log.get_Message()
            //            };
            //            CancellationToken cancellationToken1 = cancellationToken;
            //            ProductBugReport bugReport = await service1.TryProposeAsync(entity, cancellationToken1);
            //            if ( bugReport == null )
            //            {
            //                productBugReport = ( ProductBugReport ) null;
            //            }
            //            else
            //            {
            //                ++WebApiHelper._uniqueBugReports;
            //                string zip = await TimeSpan.FromDays(1.0).PrepareLogsFile((Action<Exception>) (ex => {}), cancellationToken);
            //                if ( zip != null )
            //                {
            //                    try
            //                    {
            //                        using ( FileStream body = System.IO.File.OpenRead( zip ) )
            //                        {
            //                            IFileService service2 = WebApiServicesRegistry.GetService<IFileService>();
            //                            StockSharp.Web.DomainModel.File file1 = new StockSharp.Web.DomainModel.File();
            //                            file1.Message = bugReport.Message;
            //                            file1.Name = Path.GetFileName( zip );
            //                            FileStream fileStream = body;
            //                            CancellationToken cancellationToken2 = cancellationToken;
            //                            Compressions? compression = new Compressions?();
            //                            CancellationToken cancellationToken3 = cancellationToken2;
            //                            StockSharp.Web.DomainModel.File file2 = await service2.UploadFullAsync(file1, (Stream) fileStream, 102400, (Action<long>) null, compression, cancellationToken3);
            //                        }
            //                    }
            //                    finally
            //                    {
            //                        try
            //                        {
            //                            System.IO.File.Delete( zip );
            //                        }
            //                        catch
            //                        {
            //                        }
            //                    }
            //                }
            //                productBugReport = bugReport;
            //            }
            //            num = 1;
            //        }
            //        catch ( object ex )
            //        {
            //            obj = ex;
            //        }
            //        using ( await WebApiHelper._sendingBugReportLock.LockAsync( cancellationToken ) )
            //            WebApiHelper._sendingBugReport = false;
            //        object obj1 = obj;
            //        if ( obj1 != null )
            //        {
            //            Exception source = obj1 as Exception;
            //            if ( source == null )
            //                throw obj1;
            //            ExceptionDispatchInfo.Capture( source ).Throw();
            //        }
            //        if ( num == 1 )
            //            return productBugReport;
            //        obj = ( object ) null;
            //        productBugReport = ( ProductBugReport ) null;
            //        ProductBugReport productBugReport1;
            //        return productBugReport1;
            //    }
            //}
            //return ( ProductBugReport ) null;
        }

        /// <summary>
        /// </summary>
        public static string GetSystemInfo()
        {
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 2);
            interpolatedStringHandler.AppendLiteral( "64bit: " );
            interpolatedStringHandler.AppendFormatted<bool>( Environment.Is64BitProcess );
            interpolatedStringHandler.AppendLiteral( "\r\nOS: " );
            interpolatedStringHandler.AppendFormatted<OperatingSystem>( Environment.OSVersion );
            return interpolatedStringHandler.ToStringAndClear();
        }

        /// <summary>
        /// </summary>
        public static async Task<string> PrepareLogsFile( this TimeSpan duration, Action<Exception> errorHandler, CancellationToken cancellationToken )
        {
            throw new NotImplementedException();

        //    // ISSUE: explicit reference operation
        //    // ISSUE: reference to a compiler-generated field
        //    int num1 = (^this).\u003C\u003E1__state;
        //    string result1;
        //    try
        //    {
        //        if ( errorHandler == null )
        //            throw new ArgumentNullException( nameof( errorHandler ) );
        //        TaskAwaiter<IDisposable> awaiter1 = WebApiHelper._logsPreparationLock.LockAsync(cancellationToken).GetAwaiter();
        //        TaskAwaiter<IDisposable> taskAwaiter1;
        //        int num2;
        //        if ( !awaiter1.IsCompleted )
        //        {
        //            // ISSUE: explicit reference operation
        //            // ISSUE: reference to a compiler-generated field
        //            ( ^this ).\u003C\u003E1__state = num2 = 0;
        //            taskAwaiter1 = awaiter1;
        //            // ISSUE: explicit reference operation
        //            // ISSUE: reference to a compiler-generated field
        //            ( ^this ).\u003C\u003Et__builder.AwaitUnsafeOnCompleted < TaskAwaiter<IDisposable>, WebApiHelper.\u003CPrepareLogsFile\u003Ed__31 > (ref awaiter1, this);
        //            return;
        //        }
        //        IDisposable disposable = awaiter1.GetResult();
        //        TaskAwaiter<string> taskAwaiter2;
        //        try
        //        {
        //            if ( WebApiHelper._currentLogsPreparationTask != null )
        //            {
        //                TaskAwaiter<string> awaiter2 = WebApiHelper._currentLogsPreparationTask.GetAwaiter();
        //                if ( !awaiter2.IsCompleted )
        //                {
        //                    // ISSUE: explicit reference operation
        //                    // ISSUE: reference to a compiler-generated field
        //                    ( ^this ).\u003C\u003E1__state = num1 = 1;
        //                    taskAwaiter2 = awaiter2;
        //                    // ISSUE: explicit reference operation
        //                    // ISSUE: reference to a compiler-generated field
        //                    ( ^this ).\u003C\u003Et__builder.AwaitUnsafeOnCompleted < TaskAwaiter<string>, WebApiHelper.\u003CPrepareLogsFile\u003Ed__31 > (ref awaiter2, this);
        //                    return;
        //                }
        //                result1 = awaiter2.GetResult();
        //                goto label_34;
        //            }
        //            else
        //                WebApiHelper._currentLogsPreparationTask = Task.Run<string>( ( Func<string> ) ( () =>
        //                {
        //                    DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
        //                    interpolatedStringHandler.AppendLiteral( "logs_" );
        //                    interpolatedStringHandler.AppendFormatted<DateTime>( DateTime.Today, "yyyyMMdd" );
        //                    string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        //                    LogManager instance = LogManager.get_Instance();
        //                    string path1 = instance != null ? ((IEnumerable) instance.get_Listeners()).OfType<FileLogListener>().FirstOrDefault<FileLogListener>()?.get_LogDirectory() : (string) null;
        //                    if ( StringHelper.IsEmpty( path1 ) )
        //                        return ( string ) null;
        //                    string str1 = Path.Combine(Paths.ReportLogsPath, stringAndClear);
        //                    string str2 = str1 + ".zip";
        //                    IOHelper.SafeDeleteDir( str1 );
        //                    try
        //                    {
        //                        Directory.CreateDirectory( str1 );
        //                        DateTime dateTime = duration == TimeSpan.MaxValue ? DateTime.MinValue : DateTime.Today - duration;
        //                        foreach ( string directory in Directory.GetDirectories( path1 ) )
        //                        {
        //                            try
        //                            {
        //                                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
        //                                if ( !( directoryInfo.LastWriteTime < dateTime ) )
        //                                {
        //                                    string path2 = Path.Combine(str1, directoryInfo.Name);
        //                                    Directory.CreateDirectory( path2 );
        //                                    IOHelper.CopyDirectory( directoryInfo.FullName, path2 );
        //                                }
        //                            }
        //                            catch ( Exception ex )
        //                            {
        //                                errorHandler( ex );
        //                            }
        //                        }
        //                        if ( System.IO.File.Exists( str2 ) )
        //                            System.IO.File.Delete( str2 );
        //                        ZipFile.CreateFromDirectory( str1, str2 );
        //                    }
        //                    finally
        //                    {
        //                        try
        //                        {
        //                            IOHelper.SafeDeleteDir( str1 );
        //                        }
        //                        catch
        //                        {
        //                        }
        //                    }
        //                    return str2;
        //                } ) );
        //        }
        //        finally
        //        {
        //            if ( num1 < 0 && disposable != null )
        //                disposable.Dispose();
        //        }
        //        disposable = ( IDisposable ) null;
        //        object obj =  null;
        //        int num = 0;
        //        string str;
        //        try
        //        {
        //            TaskAwaiter<string> awaiter2 = WebApiHelper._currentLogsPreparationTask.GetAwaiter();
        //            if ( !awaiter2.IsCompleted )
        //            {
        //                // ISSUE: explicit reference operation
        //                // ISSUE: reference to a compiler-generated field
        //                ( ^this ).\u003C\u003E1__state = num1 = 2;
        //                taskAwaiter2 = awaiter2;
        //                // ISSUE: explicit reference operation
        //                // ISSUE: reference to a compiler-generated field
        //                ( ^this ).\u003C\u003Et__builder.AwaitUnsafeOnCompleted < TaskAwaiter<string>, WebApiHelper.\u003CPrepareLogsFile\u003Ed__31 > (ref awaiter2, this);
        //                return;
        //            }
        //            str = awaiter2.GetResult();
        //            num = 1;
        //        }
        //        catch ( object ex )
        //        {
        //            obj = ex;
        //        }
        //        TaskAwaiter<IDisposable> awaiter3 = WebApiHelper._logsPreparationLock.LockAsync(cancellationToken).GetAwaiter();
        //        if ( !awaiter3.IsCompleted )
        //        {
        //            // ISSUE: explicit reference operation
        //            // ISSUE: reference to a compiler-generated field
        //            ( ^this ).\u003C\u003E1__state = num2 = 3;
        //            taskAwaiter1 = awaiter3;
        //            // ISSUE: explicit reference operation
        //            // ISSUE: reference to a compiler-generated field
        //            ( ^this ).\u003C\u003Et__builder.AwaitUnsafeOnCompleted < TaskAwaiter<IDisposable>, WebApiHelper.\u003CPrepareLogsFile\u003Ed__31 > (ref awaiter3, this);
        //            return;
        //        }
        //        IDisposable result2 = awaiter3.GetResult();
        //        try
        //        {
        //            WebApiHelper._currentLogsPreparationTask = ( Task<string> ) null;
        //        }
        //        finally
        //        {
        //            if ( num1 < 0 && result2 != null )
        //                result2.Dispose();
        //        }
        //        object obj1 = obj;
        //        if ( obj1 != null )
        //        {
        //            Exception source = obj1 as Exception;
        //            if ( source == null )
        //                throw obj1;
        //            ExceptionDispatchInfo.Capture( source ).Throw();
        //        }
        //        if ( num == 1 )
        //        {
        //            result1 = str;
        //        }
        //        else
        //        {
        //            obj = ( object ) null;
        //            str = ( string ) null;
        //        }
        //    }
        //    catch ( Exception ex )
        //    {
        //        // ISSUE: explicit reference operation
        //        // ISSUE: reference to a compiler-generated field
        //        ( ^this ).\u003C\u003E1__state = -2;
        //        // ISSUE: explicit reference operation
        //        // ISSUE: reference to a compiler-generated field
        //        ( ^this ).\u003C\u003Et__builder.SetException( ex );
        //        return;
        //    }
        //label_34:
        //    // ISSUE: explicit reference operation
        //    // ISSUE: reference to a compiler-generated field
        //    ( ^this ).\u003C\u003E1__state = -2;
        //    // ISSUE: explicit reference operation
        //    // ISSUE: reference to a compiler-generated field
        //    ( ^this ).\u003C\u003Et__builder.SetResult( result1 );
        }

        /// <summary>
        /// <see cref="T:StockSharp.Web.DomainModel.ClientSocial" /> items source.
        /// </summary>
        public static IList<ClientSocial> ClientSocialsSource
        {
            get
            {
                return ConfigManager.GetService<IList<ClientSocial>>();
            }
        }

        /// <summary>
        /// <see cref="T:StockSharp.Web.DomainModel.ClientSocial" /> items source.
        /// </summary>
        public static IList<ClientSocial> TryClientSocialsSource
        {
            get
            {
                return ConfigManager.TryGetService<IList<ClientSocial>>();
            }
        }

        /// <summary>
        /// </summary>
        public static ClientSocial DefaultClientSocial { get; set; }

        /// <summary>
        /// </summary>
        public static async Task RefreshAsync( this IClientSocialService svc, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            throw new NotImplementedException();

            //if ( svc == null )
            //    throw new ArgumentNullException( nameof( svc ) );
            //IClientSocialService clientSocialService = svc;
            //long? nullable = new long?(4L);
            //CancellationToken cancellationToken1 = cancellationToken;
            //long? count = new long?();
            //bool? deleted = new bool?();
            //bool? orderByDesc = new bool?();
            //bool? totalCount = new bool?();
            //DateTime? creationStart = new DateTime?();
            //DateTime? creationEnd = new DateTime?();
            //long? clientId = new long?();
            //long? socialId = nullable;
            //long? domainId = new long?();
            //ComparisonOperator? likeCompare = new ComparisonOperator?();
            //CancellationToken cancellationToken2 = cancellationToken1;
            //BaseEntitySet<ClientSocial> async = await clientSocialService.FindAsync(0L, count, deleted, (string) null, orderByDesc, totalCount, creationStart, creationEnd, clientId, socialId, domainId, (string) null, likeCompare, cancellationToken2);
            //IList<ClientSocial> clientSocialsSource = WebApiHelper.ClientSocialsSource;
            //clientSocialsSource.Clear();
            //CollectionHelper.AddRange<ClientSocial>(  clientSocialsSource,  async.Items );
            //WebApiHelper.DefaultClientSocial = clientSocialsSource.FirstOrDefault<ClientSocial>();
        }

        /// <summary>
        /// Convert file extension to <see cref="T:StockSharp.Web.DomainModel.ProductContentTypes2" />.
        /// </summary>
        /// <param name="extension">File extension.</param>
        /// <returns><see cref="T:StockSharp.Web.DomainModel.ProductContentTypes2" /></returns>
        public static ProductContentTypes2 ToContentType( this string extension )
        {
            return WebApiHelper._extensionMap [extension];
        }

        /// <summary>
        /// </summary>
        public static Func<App> CreateApp( long pictureId )
        {
            throw new NotImplementedException();

            //return ( Func<App> ) ( () =>
            //{
            //    return new App()
            //    {
            //        Name = Paths.AppName2,
            //        Picture = new StockSharp.Web.DomainModel.File() { Id = pictureId },
            //        LocalPath = Environment.GetCommandLineArgs() [0],
            //        Version = Paths.InstalledVersion,
            //        UserId = Paths.AppName2 + HardwareInfo.GetId()
            //    };
            //} );
        }

        /// <summary>
        /// </summary>
        public static CurrencyTypes GetCurrency( this ProductDomain domain )
        {
            // ISSUE: explicit non-virtual call
            return ( domain != null ? ( CurrencyTypes? ) ( domain.Domain )?.Currency : new CurrencyTypes?() ).GetValueOrDefault( ( CurrencyTypes ) 149 );
        }

        /// <summary>
        /// </summary>
        public static bool IsAvailable( this Product product )
        {
            return !StringHelper.IsEmpty( ( ( Product ) TypeHelper.CheckOnNull<Product>(  product, nameof( product ) ) ).AvailableVersion );
        }

        /// <summary>
        /// </summary>
        public static string GetUrlRelative( this Product product )
        {
            throw new NotImplementedException();
            //return product.GetUrlPart( WebApiHelper.CurrentDomain );
        }

        /// <summary>
        /// </summary>
        public static string GetUrlAbsolute( this Product product )
        {
            return Paths.GetPageUrl( 164L, ( object ) product.GetUrlRelative() );
        }
    }
}
