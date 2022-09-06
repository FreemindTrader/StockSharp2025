using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class Poll : BaseEntity
    {
        public string Question { get; set; }

        public DateTime? Till { get; set; }

        public int Flags { get; set; }

        public Client Client { get; set; }

        public BaseEntitySet<PollChoice> Choices { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Question = storage.GetValue("Question", (string)null);
            Till     = storage.GetValue("Till", new DateTime?());
            Flags    = storage.GetValue("Flags", 0);
            Client   = storage.GetValue("Client", (Client)null);
            Choices  = storage.GetValue("Choices", (BaseEntitySet<PollChoice>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Question", Question).Set("Till", Till).Set("Flags", Flags).Set("Client", Client).Set("Choices", Choices);
        }
    }
}
