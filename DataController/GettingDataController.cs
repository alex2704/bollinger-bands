using ClassLibrary;
using Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace DataController
{
    public class GettingDataController
    {
        public delegate void DataEventHandler(Candle data);
        public event DataEventHandler DataReceived;
        private readonly IDataProvider _dataProvider;
        private System.Timers.Timer t;
        public GettingDataController(OpenFileDialog openFileDialog)
        {
            string r = Path.GetExtension(openFileDialog.FileName);
            if (r == ".json")
                _dataProvider = new ProviderJson(openFileDialog);
            if (r == ".xlsx")
                _dataProvider = new ProviderExcel(openFileDialog);
        }
        public void StartReceiveData(int timeout)
        {
            t = new System.Timers.Timer(timeout);
            t.Elapsed += t_Elapsed;
            t.Start();
        }
        private void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            var data = _dataProvider.GetData();
            OnDataReceiver(data);
            
        }
        protected virtual void OnDataReceiver(Candle data)
        {
            DataReceived?.Invoke(data);
        }
    }
}
