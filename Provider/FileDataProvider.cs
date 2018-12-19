using ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Provider
{
    public abstract class FileDataProvider : DataProvider
    {
        protected Queue<Candle> Candles { get; set; } = new Queue<Candle>();
        public FileDataProvider(OpenFileDialog openFileDialog)
        {
            ReadDataFromFile(openFileDialog);
        }
        public override Candle GetData()
        {
            if (Candles.Count > 0)
            {
                return Candles.Dequeue();
            }
            return null;
        }
        protected abstract void ReadDataFromFile(OpenFileDialog openFileDialog);
    }
}
