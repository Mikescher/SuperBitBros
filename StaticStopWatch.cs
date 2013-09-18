using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros
{
    public class StaticStopWatch
    {
        private static Stopwatch sw = new Stopwatch();

        public static void Start() {
            sw.Restart();
        }

        public static long Stop() {
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
