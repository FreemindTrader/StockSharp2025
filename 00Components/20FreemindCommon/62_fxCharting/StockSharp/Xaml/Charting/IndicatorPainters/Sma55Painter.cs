//using Ecng.Serialization;
//using fx.Indicators;
//using StockSharp.Algo.Indicators;
//using StockSharp.Localization;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Media;

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    public class Sma55Painter : BaseChartIndicatorPainter<FreemindSma55>
//    {
//        private readonly ChartLineElement _line;

//        public Sma55Painter()
//        {
//            _line = new ChartLineElement()
//            {
//                Color = Colors.Red
//            };

//            AddChildElement(Line);
//        }

//        public Sma55Painter(int fifoCapacity)
//        {
//            _line = new ChartLineElement()
//            {
//                FifoCapacity = fifoCapacity,
//                Color = Colors.Red
//            };

//            AddChildElement(Line);
//        }

//        [Display(Description = "Str1898", Name = "Str1898", ResourceType = typeof(LocalizedStrings))]
//        public ChartLineElement Line
//        {
//            get
//            {
//                return _line;
//            }
//        }

//        protected override bool OnDraw(SimpleMovingAverage indicator, IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
//        {
//            return ( DrawValues(data[(IIndicator)indicator.Length ], Line)
//        }

//        public override void Load(SettingsStorage storage)
//        {
//            base.Load(storage);
//            Line.Load(storage.GetValue("Line", (SettingsStorage)null));
//        }

//        public override void Save(SettingsStorage storage)
//        {
//            base.Save(storage);
//            storage.SetValue("Line", Line.Save());
//        }
//    }
//}
