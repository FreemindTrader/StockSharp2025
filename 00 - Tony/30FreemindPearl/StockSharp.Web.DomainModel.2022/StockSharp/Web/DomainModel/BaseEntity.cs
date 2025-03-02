using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public abstract class BaseEntity : IPersistable
    {
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public bool Deleted { get; set; }

        public Client CreatedBy { get; set; }

        public DateTime CreationDateLocal
        {
            get
            {
                return CreationDate.ToLocalTime();
            }
        }

        public DateTime ModificationDateLocal
        {
            get
            {
                return ModificationDate.ToLocalTime();
            }
        }

        public string IP { get; set; }

        public virtual void Load(SettingsStorage storage)
        {
            Id               = storage.GetValue("Id", 0L);
            CreationDate     = storage.GetValue("CreationDate", new DateTime());
            ModificationDate = storage.GetValue("ModificationDate", new DateTime());
            Deleted          = storage.GetValue("Deleted", false);
            CreatedBy        = storage.GetValue("CreatedBy", (Client)null);
        }

        public virtual void Save(SettingsStorage storage)
        {
            storage.Set("Id", Id).Set("CreationDate", CreationDate).Set("ModificationDate", ModificationDate).Set("Deleted", Deleted).Set("CreatedBy", CreatedBy);
        }

        public override string ToString()
        {
            return string.Format("{{x:Type={0}, Id={1}}}", GetType(), Id);
        }
    }
}
