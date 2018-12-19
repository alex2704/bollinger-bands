﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Candle
    {
        public long TimeStamp { get; }
        public decimal High { get; }
        public decimal Low { get; }
        public decimal Open { get; }
        public decimal Close { get; }
        public DateTime Time { get; }
        public Candle(decimal high, decimal low, decimal open, decimal close, DateTime time)
        {
            High = high;
            Low = low;
            Open = open;
            Close = close;
            Time = time;
        }
        public Candle(long timeStamp, decimal high, decimal low, decimal open, decimal close)
        {
            TimeStamp = timeStamp;
            High = high;
            Low = low;
            Open = open;
            Close = close;
            Time = new DateTime(1970, 1, 1).AddSeconds(timeStamp);
        }
    }
}
