
using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class PollChoice : BaseEntity
    {
        public string Text { get; set; }

        public Poll Poll { get; set; }

        public BaseEntitySet<PollVote> Votes { get; set; }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Text  = storage.GetValue("Text", (string)null);
            Poll  = storage.GetValue("Poll", (Poll)null);
            Votes = storage.GetValue("Votes", (BaseEntitySet<PollVote>)null);
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.Set("Text", Text).Set("Poll", Poll).Set("Votes", Votes);
        }
    }
}
