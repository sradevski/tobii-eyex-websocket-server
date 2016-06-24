using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tobii.EyeX.Framework;

namespace TobiiSocketServer
{
    public class EyeNavData
    {
        public int state = 0;
        public long timestamp = 0;
        public double x = 0;
        public double y = 0;

        public EyeNavData(EyeTrackingDeviceStatus state, int timestamp, double x, double y)
        {
            this.state = getMappedState(state);
            this.timestamp = timestamp;
            this.x = x;
            this.y = y;
        }

        private int getMappedState(EyeTrackingDeviceStatus tobiiState)
        {
            switch (tobiiState)
            {
                case EyeTrackingDeviceStatus.Tracking:
                    return 1;
                case EyeTrackingDeviceStatus.TrackingPaused:
                case EyeTrackingDeviceStatus.TrackingUnavailable:
                    return 2;
                default:
                    return 10;
            }
        }
    }
}
