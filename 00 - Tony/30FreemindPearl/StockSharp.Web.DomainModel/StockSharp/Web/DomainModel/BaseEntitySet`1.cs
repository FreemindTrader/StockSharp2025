using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class BaseEntitySet<TEntity> : IPersistable, IBaseEntitySet
    {
        public int Count { get; set; }

        public TEntity[] Items { get; set; }

        Array IBaseEntitySet.Items
        {
            get
            {
                return Items;
            }
        }

        public void Load(SettingsStorage storage)
        {
            Count = storage.GetValue("Count", 0);
            Items = storage.GetValue("Items", (TEntity[])null);
        }

        public void Save(SettingsStorage storage)
        {
            storage.Set("Count", Count).Set("Items", Items);
        }
    }
}
