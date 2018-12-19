using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using LiveCharts.Defaults;

namespace DataRepresentor
{
    public class DataRocController : DataRepresentorController
    {
        public DataRocController(int maxCount) : base(maxCount) { }
        public override void AddValueToLine(Candle candle, Func<Candle, decimal> funcOpen, Func<Candle, decimal> funcClose)
        {
            var curValue = funcOpen.Invoke(candle);
            var lineValues = SeriesCollection[0].Values;
            var lineValues2 = SeriesCollection[1].Values;
            var lineValues3 = SeriesCollection[2].Values;
            var candleValues = SeriesCollection[3].Values;
            double value = 0, //знач средняя линия
                value2 = 0, //знач верхняя линия
                value3 = 0; //знач нижняя линия
            if (LastCandle != null)
            {
                if (funcOpen.Invoke(LastCandle) > funcClose.Invoke(LastCandle))
                {
                    value = LastCandle == null ? 0 : (double)((funcOpen.Invoke(LastCandle) + funcClose.Invoke(LastCandle)) / 2);
                    value2 = LastCandle == null ? 0 : (double)(funcOpen.Invoke(LastCandle)) + 0.01;
                    value3 = LastCandle == null ? 0 : (double)(funcClose.Invoke(LastCandle)) - 0.01;
                }
                if (funcOpen.Invoke(LastCandle) < funcClose.Invoke(LastCandle))
                {
                    value = LastCandle == null ? 0 : (double)((funcOpen.Invoke(LastCandle) + funcClose.Invoke(LastCandle)) / 2);
                    value2 = LastCandle == null ? 0 : (double)(funcClose.Invoke(LastCandle)) + 0.01;
                    value3 = LastCandle == null ? 0 : (double)(funcOpen.Invoke(LastCandle)) - 0.01;
                }
            }
            //double value = LastCandle == null ? 0 : (double)((funcOpen.Invoke(LastCandle) + funcClose.Invoke(LastCandle)) / 2);
            //double value2 = LastCandle == null ? 0 : (double)(funcOpen.Invoke(LastCandle));
            //double value3 = LastCandle == null ? 0 : (double)(funcClose.Invoke(LastCandle));
            lineValues.Add(new ObservableValue(value));
            lineValues2.Add(new ObservableValue(value2));
            lineValues3.Add(new ObservableValue(value3));
            candleValues.Add(new OhlcPoint((double)candle.Open, (double)candle.High, (double)candle.Low, (double)candle.Close));
            while (lineValues.Count > MaxPointsCount)
            {
                lineValues.RemoveAt(0);
            }
            while (lineValues2.Count > MaxPointsCount)
            {
                lineValues2.RemoveAt(0);
            }
            while (lineValues3.Count > MaxPointsCount)
            {
                lineValues3.RemoveAt(0);
            }
            while (candleValues.Count > MaxPointsCount) 
            {
                candleValues.RemoveAt(0);
            }
            LastCandle = candle;
            OnPropertyChanged("SeriesCollection");
        }
    }
}
