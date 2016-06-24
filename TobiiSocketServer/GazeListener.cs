using EyeXFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Tobii.EyeX.Framework;

namespace TobiiSocketServer
{
    public class GazeListener
    {
        public void onGazeChanged(object s, GazePointEventArgs e)
        {
            if (Globs.server != null)
            {
                if (Globs.server.allSockets.Count > 0)
                {
                    var eyeNavData = new EyeNavData(Globs.tracker.EyeTrackingDeviceStatus.Value, (int)e.Timestamp, e.X, e.Y);
                    var json = new JavaScriptSerializer().Serialize(eyeNavData);
                    System.Console.WriteLine(json);
                    Globs.server.sendToAll(json);
                }
            }
        }
    }
}
