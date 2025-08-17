// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.WebApiExtension
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5447883D-4177-4B33-881A-41D29A366FB7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.UI.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Ecng.Common;
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

#nullable enable
namespace StockSharp.Studio.Controls;

public static class WebApiExtension
{
    private static
#nullable disable
    StudioUserConfig Config => StudioUserConfig.Instance;

    public static bool ShowByeByeWindow(this Window owner, bool feedback = true) => true;

    public static void ShowOfflineWarning(this DependencyObject owner)
    {
        int num = owner != null ? (int)new MessageBoxBuilder().Warning().Text(LocalizedStrings.OfflineWarning).Owner(owner).Show() : throw new ArgumentNullException(nameof(owner));
    }

    public static async Task SendFeedback(this Window owner, CancellationToken cancellationToken)
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        IProductFeedbackService svc;
        if (WebApiServicesRegistry.Offline)
        {
            owner.ShowOfflineWarning();
            svc = (IProductFeedbackService)null;
        }
        else
        {
            svc = WebApiServicesRegistry.GetService<IProductFeedbackService>();
            if (await svc.GetByProductAndClientAsync(WebApiHelper.ProductId, new long?(), cancellationToken) != null)
            {
                svc = (IProductFeedbackService)null;
            }
            else
            {
                DateTime? nextTimeFeedback = WebApiExtension.Config.GetNextTimeFeedback();
                DateTime utcNow = DateTime.UtcNow;
                if ((nextTimeFeedback.HasValue ? (nextTimeFeedback.GetValueOrDefault() > utcNow ? 1 : 0) : 0) != 0)
                {
                    svc = (IProductFeedbackService)null;
                }
                else
                {
                    RatingWindow wnd = new RatingWindow();
                    if (!XamlHelper.ShowModal(wnd, owner))
                    {
                        WebApiExtension.Config.SetNextTimeFeedback(DateTime.UtcNow.AddDays(7.0));
                        svc = (IProductFeedbackService)null;
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
                        ProductFeedback productFeedback = await productFeedbackService.AddAsync(entity, cancellationToken1);
                        int num = (int)new MessageBoxBuilder().Text(LocalizedStrings.ThankYouForFeedback).Owner(owner).Show();
                        svc = (IProductFeedbackService)null;
                    }
                }
            }
        }
    }

    public static void OpenChat(this Window owner)
    {
        Paths.Chat.TryOpenLink((DependencyObject)owner);
    }

    public static void SendLogs(this Window owner)
    {
        LoggingHelper.ObserveErrorAndLog(owner.SendLogsAsync(new CancellationToken()));
    }

    private static async Task SendLogsAsync(this Window owner, CancellationToken cancellationToken)
    {
        ILogReceiver logs;
        if (WebApiServicesRegistry.Offline)
        {
            owner.ShowOfflineWarning();
            logs = (ILogReceiver)null;
        }
        else
        {
            LogsDurationWindow wnd1 = new LogsDurationWindow();
            if (!XamlHelper.ShowModal(wnd1, owner))
            {
                logs = (ILogReceiver)null;
            }
            else
            {
                logs = LogManager.Instance?.Application;
                try
                {
                    string zip = await wnd1.Duration.PrepareLogsFile((Action<Exception>)(ex => LoggingHelper.LogError(ex, "Prepare logs file error: {0}")), cancellationToken);
                    if (zip == null)
                    {
                        ILogReceiver ilogReceiver = logs;
                        if (ilogReceiver == null)
                        {
                            logs = (ILogReceiver)null;
                        }
                        else
                        {
                            LoggingHelper.AddErrorLog(ilogReceiver, "Cannot find log folder.", Array.Empty<object>());
                            logs = (ILogReceiver)null;
                        }
                    }
                    else
                    {
                        ILogReceiver ilogReceiver1 = logs;
                        if (ilogReceiver1 != null)
                            LoggingHelper.AddDebugLog(ilogReceiver1, "created logs zip: {0}", new object[1]
                            {
                (object) zip
                            });
                        try
                        {
                            QuestionWindow wnd2 = new QuestionWindow()
                            {
                                AttachPath = zip,
                                MessagePrompt = LocalizedStrings.DescribeTheBugInDetails,
                                Caption = $"[{Paths.AppName2}] {LocalizedStrings.BugReport}"
                            };
                            IFileService fileSvc = WebApiServicesRegistry.GetService<IFileService>();
                            List<StockSharp.Web.DomainModel.File> attachments = new List<StockSharp.Web.DomainModel.File>();
                            wnd2.FileProcessing += (Func<string, byte[], Action<long>, CancellationToken, Task<StockSharp.Web.DomainModel.File>>)(async (name, body, progress, t) =>
                            {
                                IFileService service = fileSvc;
                                StockSharp.Web.DomainModel.File file1 = new StockSharp.Web.DomainModel.File();
                                file1.Name = name;
                                Stream body1 = Converter.To<Stream>((object)body);
                                Action<long> progress1 = progress;
                                CancellationToken cancellationToken1 = t;
                                Compressions? compression = new Compressions?();
                                CancellationToken cancellationToken2 = cancellationToken1;
                                StockSharp.Web.DomainModel.File file2 = await service.UploadFullAsync(file1, body1, progress: progress1, compression: compression, cancellationToken: cancellationToken2);
                                attachments.Add(file2);
                                return file2;
                            });
                            if (!XamlHelper.ShowModal(wnd2, owner))
                            {
                                logs = (ILogReceiver)null;
                                return;
                            }
                            IProductBugReportService service1 = WebApiServicesRegistry.GetService<IProductBugReportService>();
                            ProductBugReport productBugReport1 = new ProductBugReport();
                            Product product = new Product();
                            product.Id = WebApiHelper.ProductId;
                            productBugReport1.Product = product;
                            productBugReport1.Version = Paths.InstalledVersion;
                            productBugReport1.SystemInfo = WebApiHelper.GetSystemInfo();
                            Message message = new Message();
                            Topic topic = new Topic();
                            topic.Name = wnd2.Caption;
                            Domain domain = new Domain();
                            domain.Id = WebApiHelper.CurrentDomain;
                            topic.Domain = domain;
                            message.Topic = topic;
                            message.Text = wnd2.Text;
                            message.Attachments = attachments.ToEntitySet<StockSharp.Web.DomainModel.File>();
                            productBugReport1.Message = message;
                            ProductBugReport entity = productBugReport1;
                            ILogReceiver ilogReceiver2 = logs;
                            if (ilogReceiver2 != null)
                                LoggingHelper.AddDebugLog(ilogReceiver2, "sending {0} '{1}', {2} attachments", new object[3]
                                {
                  (object) "bug report",
                  (object) wnd2.Caption,
                  (object) attachments.Count
                                });
                            ProductBugReport productBugReport2 = await service1.AddAsync(entity, cancellationToken);
                            ILogReceiver ilogReceiver3 = logs;
                            if (ilogReceiver3 != null)
                                LoggingHelper.AddDebugLog(ilogReceiver3, $"report: (report={productBugReport2.Id})", Array.Empty<object>());
                            int num = (int)new MessageBoxBuilder().Text(LocalizedStrings.ThankYouForQuestion).Owner(owner).Show();
                        }
                        finally
                        {
                            try
                            {
                                System.IO.File.Delete(zip);
                            }
                            catch (Exception ex)
                            {
                                LoggingHelper.LogError(ex, (string)null);
                            }
                        }
                        zip = (string)null;
                        logs = (ILogReceiver)null;
                    }
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogError(ex, "Prepare logs file error: {0}");
                    int num = (int)new MessageBoxBuilder().Owner(owner).Error().Text(LocalizedStrings.CanNotCreateLogsFile).Button(MessageBoxButton.OK).Show();
                    logs = (ILogReceiver)null;
                }
            }
        }
    }
}
