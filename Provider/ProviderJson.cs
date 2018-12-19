using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Provider
{
    public class ProviderJson : FileDataProvider
    {
        public ProviderJson(OpenFileDialog openFileDialog) : base(openFileDialog)
        {
        }
        protected override void ReadDataFromFile(OpenFileDialog openFileDialog)
        {
            using (var stream = new StreamReader(new FileStream(openFileDialog.FileName, FileMode.Open)))
            {
                var content = stream.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                var t = ((JArray)data["t"]).ToObject<List<long>>();
                var o = ((JArray)data["o"]).ToObject<List<decimal>>();
                var c = ((JArray)data["o"]).ToObject<List<decimal>>();
                var h = ((JArray)data["o"]).ToObject<List<decimal>>();
                var l = ((JArray)data["o"]).ToObject<List<decimal>>();

                for(int i = 0; i < t.Count; i++)
                {
                    var tValue = t[i];
                    var oValue = o[i];
                    var cValue = o[i];
                    var hValue = o[i];
                    var lValue = o[i];

                    Candles.Enqueue(new Candle(tValue, hValue, lValue, oValue, cValue));
                }
            }
        }
    }
}
