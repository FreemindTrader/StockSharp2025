
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class FileShareVisit : BaseEntity
    {
        public FileShare FileShare { get; set; }

        public Client Client { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            FileShare = storage.GetValue("FileShare", (FileShare)null);
            Client    = storage.GetValue("Client", (Client)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("FileShare", FileShare).Set("Client", Client);
        }
    }
}
