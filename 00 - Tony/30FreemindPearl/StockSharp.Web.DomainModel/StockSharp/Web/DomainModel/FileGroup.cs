
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class FileGroup : BaseEntity
    {
        public string Name { get; set; }

        public Client Owner { get; set; }

        public string Description { get; set; }

        public BaseEntitySet<FileGroup> Child { get; set; }

        public BaseEntitySet<File> Files { get; set; }

        public BaseEntitySet<FileGroup> Parent { get; set; }

        public BaseEntitySet<Client> Roles { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);

            Name        = storage.GetValue("Name", (string)null);
            Owner       = storage.GetValue("Owner", (Client)null);
            Description = storage.GetValue("Description", (string)null);
            Child       = storage.GetValue("Child", (BaseEntitySet<FileGroup>)null);
            Files       = storage.GetValue("Files", (BaseEntitySet<File>)null);
            Parent      = storage.GetValue("Parent", (BaseEntitySet<FileGroup>)null);
            Roles       = storage.GetValue("Roles", (BaseEntitySet<Client>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Name", Name).Set("Owner", Owner).Set("Description", Description).Set("Child", Child).Set("Files", Files).Set("Parent", Parent).Set("Roles", Roles);
        }
    }
}
