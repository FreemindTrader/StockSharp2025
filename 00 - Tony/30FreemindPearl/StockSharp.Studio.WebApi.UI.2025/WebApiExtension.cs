// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.WebApiExtension
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A13266F-BFDB-4F15-BB1A-891982F4135B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.WebApi.UI.dll

using Ecng.Logging;
using Ecng.Xaml;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using StockSharp.Studio.WebApi;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using StockSharp.Xaml;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Studio.Controls
{
    public static class WebApiExtension
    {
        private static StudioUserConfig Config
        {
            get
            {
                return StudioUserConfig.Instance;
            }
        }

        public static bool ShowByeByeWindow( this Window owner, bool feedback = true )
        {
            return true;
        }

        public static void ShowOfflineWarning( this DependencyObject owner )
        {
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            int num = (int) new MessageBoxBuilder().Warning().Text(LocalizedStrings.OfflineWarning).Owner(owner).Show();
        }

        public static async Task SendFeedback(
          this Window owner,
          CancellationToken cancellationToken )
        {
            IProductFeedbackService svc;
            try
            {
                if ( owner == null )
                    throw new ArgumentNullException( nameof( owner ) );
                if ( WebApiServicesRegistry.Offline )
                {
                    owner.ShowOfflineWarning();
                }
                else
                {
                    svc = WebApiServicesRegistry.GetService<IProductFeedbackService>();
                    var result = await svc.GetByProductAndClientAsync(WebApiHelper.ProductId, new long?(), cancellationToken);

                    if ( result == null )
                    {
                        DateTime? nextTimeFeedback = WebApiExtension.Config.GetNextTimeFeedback();
                        DateTime now = DateTime.Now;
                        if ( ( nextTimeFeedback.HasValue ? ( nextTimeFeedback.GetValueOrDefault() > now ? 1 : 0 ) : 0 ) == 0 )
                        {
                            RatingWindow wnd = new RatingWindow();
                            if ( !XamlHelper.ShowModal( wnd, owner ) )
                            {
                                WebApiExtension.Config.SetNextTimeFeedback( DateTime.Now.AddDays( 7.0 ) );
                            }
                            else
                            {
                                int ratingValue = wnd.RatingValue;
                                string comment = wnd.Comment;
                                IProductFeedbackService productFeedbackService = svc;
                                ProductFeedback entity = new ProductFeedback();
                                Product product = new Product();
                                product.Id = WebApiHelper.ProductId;
                                entity.Product = product;
                                entity.Rate = ratingValue;
                                entity.Message = new Message()
                                {
                                    Text = comment
                                };
                                CancellationToken cancellationToken1 = cancellationToken;
                                var awaitResult = await productFeedbackService.AddAsync(entity, cancellationToken1);

                                new MessageBoxBuilder().Text( LocalizedStrings.ThankYouForFeedback ).Owner( owner ).Show();
                            }
                        }
                    }
                }
            }
            catch ( Exception ex )
            {

            }
        }

        public static void OpenChat( this Window owner )
        {
            Paths.Chat.TryOpenLink( ( DependencyObject ) owner );
        }

        public static void SendLogs( this Window owner )
        {
            LoggingHelper.ObserveErrorAndLog( owner.SendLogsAsync( new CancellationToken() ) );
        }

        private static async Task SendLogsAsync( this Window owner, CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
            //ILogReceiver logs;

            //int awaitCount = -2;

            //try
            //{
            //    if ( WebApiServicesRegistry.Offline )
            //    {
            //        owner.ShowOfflineWarning();
            //    }
            //    else
            //    {
            //        LogsDurationWindow wnd1 = new LogsDurationWindow();
            //        if ( XamlHelper.ShowModal( wnd1, owner ) )
            //        {
            //            logs = LogManager.Instance?.Application;
            //            try
            //            {
            //                var zip = await wnd1.Duration.PrepareLogsFile((Action<Exception>) (ex => LoggingHelper.LogError(ex, "Prepare logs file error: {0}")), cancellationToken);

            //                if ( zip == null )
            //                {                                
            //                    if ( logs != null ) logs.AddErrorLog( "Cannot find log folder." );
            //                }
            //                else
            //                {                                
            //                    if ( logs != null )
            //                        logs.AddDebugLog( "created logs zip: {0}", zip );

            //                    awaitCount = 1;


            //                    try
            //                    {
            //                        QuestionWindow wnd2 = new QuestionWindow()
            //                        {
            //                            AttachPath = zip,
            //                            MessagePrompt = LocalizedStrings.DescribeTheBugInDetails,
            //                            Caption = "[" + Paths.AppName2 + "] " + LocalizedStrings.BugReport
            //                        };
            //                        IFileService fileSvc = WebApiServicesRegistry.GetService<IFileService>();
            //                        List<StockSharp.Web.DomainModel.File> attachments = new List<StockSharp.Web.DomainModel.File>();

            //                        wnd2.FileProcessing += ( Func<string, byte [ ], Action<long>, CancellationToken, Task<StockSharp.Web.DomainModel.File>> ) ( async ( name, body, progress, t ) =>
            //                        {
            //                            IFileService service = fileSvc;
            //                            StockSharp.Web.DomainModel.File file1 = new StockSharp.Web.DomainModel.File();
            //                            file1.Name = name;
            //                            var m0 = Converter.To<Stream>( body);
            //                            Action<long> progress1 = progress;
            //                            CancellationToken cancellationToken1 = t;
            //                            Compressions? compression = new Compressions?();
            //                            CancellationToken cancellationToken2 = cancellationToken1;
            //                            StockSharp.Web.DomainModel.File file2 = await service.UploadFullAsync(file1, (Stream) m0, 102400, progress1, compression, cancellationToken2);
            //                            awaitCount = 2;
            //                            attachments.Add( file2 );
            //                            return file2;
            //                        } );

            //                        if ( wnd2.ShowModal( owner ) )
            //                        {
            //                            IProductBugReportService service = WebApiServicesRegistry.GetService<IProductBugReportService>();
            //                            ProductBugReport productBugReport = new ProductBugReport();
            //                            Product product = new Product();
            //                            product.Id = WebApiHelper.ProductId;
            //                            productBugReport.Product = product;
            //                            productBugReport.Version = Paths.InstalledVersion;
            //                            productBugReport.SystemInfo = WebApiHelper.GetSystemInfo();
            //                            Message message = new Message();
            //                            Topic topic = new Topic();
            //                            topic.Name = wnd2.Caption;
            //                            Domain domain = new Domain();
            //                            domain.Id = WebApiHelper.CurrentDomain;
            //                            topic.Domain = domain;
            //                            message.Topic = topic;
            //                            message.Text = wnd2.Text;
            //                            message.Attachments = attachments.ToEntitySet<StockSharp.Web.DomainModel.File>( 0L );
            //                            productBugReport.Message = message;
            //                            ProductBugReport entity = productBugReport;
            //                            ILogReceiver ilogReceiver2 = logs;
            //                            if ( ilogReceiver2 != null )
            //                                LoggingHelper.AddDebugLog( ilogReceiver2, "sending {0} '{1}', {2} attachments", new object [3]
            //                                {
            //             "bug report",
            //             wnd2.Caption,
            //             attachments.Count
            //                                } );


            //                            ProductBugReport result = await service.AddAsync(entity, cancellationToken);

            //                            if ( logs != null )
            //                            {
            //                                DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
            //                                interpolatedStringHandler.AppendLiteral( "report: (report=" );
            //                                interpolatedStringHandler.AppendFormatted<long>( result.Id );
            //                                interpolatedStringHandler.AppendLiteral( ")" );
            //                                logs.AddDebugLog( interpolatedStringHandler.ToStringAndClear(), Array.Empty<object>() );
            //                            }

            //                            awaitCount = 3;

            //                            new MessageBoxBuilder().Text(LocalizedStrings.ThankYouForQuestion).Owner(owner).Show();
            //                        }                                    
            //                    }
            //                    finally
            //                    {
            //                        if ( awaitCount < 0 )
            //                        {
            //                            try
            //                            {
            //                                System.IO.File.Delete( zip );
            //                            }
            //                            catch ( Exception ex )
            //                            {
            //                                LoggingHelper.LogError( ex, ( string ) null );
            //                            }
            //                        }
            //                    }
            //                    zip = ( string ) null;
            //                }
            //            }
            //            catch ( Exception ex )
            //            {
            //                LoggingHelper.LogError( ex, "Prepare logs file error: {0}" );
            //                int num2 = (int) new MessageBoxBuilder().Owner(owner).Error().Text(LocalizedStrings.CanNotCreateLogsFile).Button(MessageBoxButton.OK).Show();
            //            }
            //        }
            //    }
            //}
            //catch ( Exception ex )
            //{

            //}

            //logs = null;

        }
    }
}
