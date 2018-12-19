using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepresentation
{
    public class RepresentDataController : INotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection { get; set; } = new SeriesCollection
        {
            new LineSeries
            {
                AreaLimit = -10;
                Values = new ChartValues<ObservableValue>(),
            }
        };
    }
}
