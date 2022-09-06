
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class FileDownload : BaseEntity
    {
        public Client Client { get; set; }

        public File File { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            Client = storage.GetValue("Client", (Client)null);
            File   = storage.GetValue("File", (File)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Client", Client).Set("File", File);
        }
    }
}
