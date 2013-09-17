using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBitBros.OpenGL
{
    public class AvgDurationWatch
    {
        private const int UPDATE_TIME = 500;

        public double Duration { get; private set; }

        private long lastUpdate;

        private long lastTick;
        private long timeSum;
        private int count;

        public AvgDurationWatch()
        {
            lastUpdate = 0;
            Duration = 1;
            timeSum = 0;
            count = 0;
            lastTick = 0;
        }

        public void Start()
        {
            lastTick = Environment.TickCount;
        }

        public void Stop()
        {
            timeSum += Environment.TickCount - lastTick;
            count++;

            if ((Environment.TickCount - lastUpdate) > UPDATE_TIME)
            {
                Duration = timeSum / (count*1d);

                timeSum = 0;
                count = 0;
                lastTick = 0;

                lastUpdate = Environment.TickCount;
            }
        }
    }
}
