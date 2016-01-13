using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace KerbalTimer
{
    public class Timer
    {
        public Stopwatch maintimer = new Stopwatch();
        public Stopwatch lapTimer = new Stopwatch();
        public List<String> laptimes = new List<String>();
        public bool hasStopped = true;
        public String finaltime = "";
    }
}
