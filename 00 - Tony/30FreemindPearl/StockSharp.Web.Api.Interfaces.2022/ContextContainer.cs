using Ecng.Backup;
using System;

namespace StockSharp.Web.Api.Interfaces
{
    public class ContextContainer
    {
        public ContextContainer( object dataContext, IClientContext clientContext, IBackupService backupService)
        {
            object obj = dataContext;
            if (obj == null)
                throw new ArgumentNullException(nameof(dataContext));
            DataContext = obj;
            IClientContext clientContext1 = clientContext;
            if (clientContext1 == null)
                throw new ArgumentNullException(nameof(clientContext));
            ClientContext = clientContext1;
            IBackupService backupService1 = backupService;
            if (backupService1 == null)
                throw new ArgumentNullException(nameof(backupService));
            BackupService = backupService1;
        }

        public object DataContext { get; }

        public IClientContext ClientContext { get; }

        public IBackupService BackupService { get; }
    }
}
