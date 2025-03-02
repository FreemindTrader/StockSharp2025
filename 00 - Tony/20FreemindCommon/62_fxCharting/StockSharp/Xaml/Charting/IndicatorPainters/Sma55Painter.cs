using Ecng.Serialization;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace fx.Charting.IndicatorPainters
{
    public class Sma55Painter : BaseChartIndicatorPainter
    {
        private readonly LineUI _line;

        public Sma55Painter()
        {
            _line = new LineUI()
            {
                Color = Colors.Red
            };

            AddChildElement(Line);
        }

        public Sma55Painter(int fifoCapacity)
        {
            _line = new LineUI()
            {
                FifoCapacity = fifoCapacity,
                Color = Colors.Red
            };

            AddChildElement(Line);
        }

        [Display(Description = "Str1898", Name = "Str1898", ResourceType = typeof(LocalizedStrings))]
        public LineUI Line
        {
            get
            {
                return _line;
            }
        }

        protected override bool OnDraw()
        {
            return DrawValues(Indicator, Line);
        }

        public override void Load(SettingsStorage storage)
        {
            base.Load(storage);
            Line.Load(storage.GetValue("Line", (SettingsStorage)null));
        }

        public override void Save(SettingsStorage storage)
        {
            base.Save(storage);
            storage.SetValue("Line", Line.Save());
        }
    }
}
