using ClassLibrary;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataRepresentor
{
    public abstract class DataRepresentorController : INotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection
        {
            new LineSeries
            {
                AreaLimit = -10,
                Values = new ChartValues<ObservableValue>(),
            },
            new LineSeries
            {
                AreaLimit = -10,
                Values = new ChartValues<ObservableValue>(),
            },
            new LineSeries
            {
                AreaLimit = -10,
                Values = new ChartValues<ObservableValue>()
            },
            new CandleSeries()
            {
                Values = new ChartValues<OhlcPoint>(),
            },
        };
        public Candle LastCandle { get; protected set; }
        public int MaxPointsCount { get; set; }

        public DataRepresentorController(int maxPointsCount)
        {
            MaxPointsCount = maxPointsCount;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop ="")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public abstract void AddValueToLine(Candle candle, Func<Candle, decimal> funcOpen, Func<Candle, decimal> funcClose);
    }
}
