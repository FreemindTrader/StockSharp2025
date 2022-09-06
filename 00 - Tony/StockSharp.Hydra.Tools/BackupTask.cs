using Amazon;
using Ecng.Backup;
using Ecng.Backup.Amazon;
using Ecng.Backup.Yandex;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml.Yandex;
using Nito.AsyncEx;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Hydra.Tools
{
    [DisplayNameLoc( "Backup" )]
    [DescriptionLoc( "BackupDescription", false )]
    [Doc( "topics/HydraBackup.html" )]
    [XamlIcon( "Cloud.svg" )]
    [MessageAdapterCategory( MessageAdapterCategories.Tool )]
    internal class BackupTask : BaseHydraTask
    {
        private const string _sourceName = "Backup";

        public BackupTask()
        {
            Offset      = 1;
            StartFrom   = new DateTime( 2000, 1, 1 );
            Interval    = TimeSpan.FromDays( 1.0 );
            Service     = BackupServices.AwsS3;
            Address     = RegionEndpoint.USEast1.SystemName;
            ServiceRepo = "stocksharp";
            Login       = string.Empty;
            Password    = new SecureString();
        }

        [Display( Description = "Str3427Dot", GroupName = "Backup", Name = "Str3427", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        public BackupServices Service { get; set; }

        [Display( Description = "ServerAddressDot", GroupName = "Backup", Name = "Address", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public string Address { get; set; }

        [Display( Description = "Str1405Dot", GroupName = "Backup", Name = "Str1405", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        public string ServiceRepo { get; set; }

        [Display( Description = "LoginDot", GroupName = "Backup", Name = "Login", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
        public string Login { get; set; }

        [Display( Description = "PasswordDot", GroupName = "Backup", Name = "Password", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
        public SecureString Password { get; set; }

        [Display( Description = "Str3779", GroupName = "Backup", Name = "Str2282", Order = 5, ResourceType = typeof( LocalizedStrings ) )]
        public DateTime StartFrom { get; set; }

        [Display( Description = "Str3778", GroupName = "Backup", Name = "Str2284", Order = 6, ResourceType = typeof( LocalizedStrings ) )]
        public int Offset { get; set; }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<BackupServices>( "Service", Service );
            storage.SetValue<string>( "Address", Address );
            storage.SetValue<string>( "ServiceRepo", ServiceRepo );
            storage.SetValue<string>( "Login", Login );
            storage.SetValue<SecureString>( "Password", Password );
            storage.SetValue<DateTime>( "StartFrom", StartFrom );
            storage.SetValue<int>( "Offset", Offset );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Service = storage.GetValue<BackupServices>( "Service", Service );
            Address = storage.GetValue<string>( "Address", Address );
            ServiceRepo = storage.GetValue<string>( "ServiceRepo", ServiceRepo );
            Login = storage.GetValue<string>( "Login", Login );
            Password = storage.GetValue<SecureString>( "Password", Password );
            StartFrom = storage.GetValue<DateTime>( "StartFrom", StartFrom );
            Offset = storage.GetValue<int>( "Offset", Offset );
        }

        public override IEnumerable<StockSharp.Messages.DataType> SupportedDataTypes
        {
            get
            {
                return Enumerable.Empty<StockSharp.Messages.DataType>();
            }
        }

        protected override TimeSpan OnProcess()
        {
            using ( IBackupService service = CreateService() )
            {
                bool flag = false;
                this.AddInfoLog( LocalizedStrings.Str2306Params.Put( StartFrom ) );
                DateTime[ ] array1 = StartFrom.Range( DateTime.Today - TimeSpan.FromDays( Offset ), TimeSpan.FromDays( 1.0 ) ).ToArray<DateTime>();
                BackupEntry entry1 = ToEntry( new DirectoryInfo( Drive.Path ) );
                HydraTaskSecurity[ ] array2 = GetWorkingSecurities().ToArray<HydraTaskSecurity>();
                foreach ( DateTime date in array1 )
                {
                    foreach ( HydraTaskSecurity hydraTaskSecurity in array2 )
                    {
                        flag = true;
                        if ( CanProcess() )
                        {
                            BackupEntry backupEntry = new BackupEntry() { Name = LocalMarketDataDrive.GetDirName( date ), Parent = new BackupEntry() { Parent = new BackupEntry() { Name = hydraTaskSecurity.Security.Id.Substring( 0, 1 ), Parent = entry1 }, Name = hydraTaskSecurity.Security.Id } };
                            foreach ( StockSharp.Messages.DataType availableDataType in Drive.GetAvailableDataTypes( hydraTaskSecurity.Security.ToSecurityId( null, true, false ), StorageFormat ) )
                            {
                                IMarketDataStorage storage = StorageRegistry.GetStorage( hydraTaskSecurity.Security, availableDataType, Drive, StorageFormat );
                                Stream stream = storage.Drive.LoadStream( date );
                                if ( stream != Stream.Null )
                                {
                                    BackupEntry entry = new BackupEntry() { Name = LocalMarketDataDrive.GetFileName( availableDataType, new StorageFormats?( StorageFormat ), true ), Parent = backupEntry };
                                    AsyncContext.Run( () => service.UploadAsync( entry, stream, p => this.AddDebugLog( string.Format( "upload {0}% complete", p ) ), new CancellationToken() ) );
                                    this.AddInfoLog( LocalizedStrings.Str1580Params, GetPath( entry ) );
                                    IMarketDataMetaInfo metaInfo = storage.GetMetaInfo( date );
                                    if ( metaInfo == null )
                                        this.AddWarningLog( LocalizedStrings.Str1702Params.Put( date ) );
                                    else
                                        RaiseDataLoaded( hydraTaskSecurity.Security, availableDataType, new DateTimeOffset?( ( DateTimeOffset )metaInfo.LastTime ), metaInfo.Count );
                                }
                            }
                        }
                        else
                            break;
                    }
                    if ( CanProcess() )
                    {
                        StartFrom += TimeSpan.FromDays( 1.0 );
                        this.SaveSettings();
                    }
                }
                if ( !flag )
                {
                    this.AddWarningLog( LocalizedStrings.Str2292 );
                    return TimeSpan.MaxValue;
                }
                if ( CanProcess() )
                    this.AddInfoLog( LocalizedStrings.Str2300 );
                return base.OnProcess();
            }
        }

        private static string GetPath( BackupEntry entry )
        {
            if ( entry == null )
                return null;
            return GetPath( entry.Parent ) + "/" + entry.Name;
        }

        private static BackupEntry ToEntry( DirectoryInfo di )
        {
            if ( di.Parent == null )
                return null;
            return new BackupEntry() { Name = di.Name, Parent = di.Parent != null ? ToEntry( di.Parent ) : null };
        }

        public override bool CanTestConnect
        {
            get
            {
                return true;
            }
        }

        public override void TestConnect( Action<Exception> connectionChanged )
        {
            if ( connectionChanged == null )
                throw new ArgumentNullException( nameof( connectionChanged ) );
            Task.Run( async () =>
            {
                try
                {
                    using ( IBackupService service = CreateService() )
                    {
                        IEnumerable<BackupEntry> async = await service.FindAsync( null, string.Empty, new CancellationToken() );
                    }
                    connectionChanged( null );
                }
                catch ( Exception ex )
                {
                    connectionChanged( ex );
                }
            } );
        }

        private IBackupService CreateService()
        {
            switch ( Service )
            {
                case BackupServices.AwsS3:
                    return ( IBackupService )new AmazonS3Service( Address, ServiceRepo, Login, Password.UnSecure() );
                case BackupServices.AwsGlacier:
                    return ( IBackupService )new AmazonGlacierService( Address, ServiceRepo, Login, Password.UnSecure() );
                //case BackupServices.Yandex:
                //    return new YandexDiskService( YandexLoginWindow.Authorize( () => Application.Current.MainWindow ) );
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
