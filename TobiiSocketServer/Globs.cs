using EyeXFramework;
using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobiiSocketServer
{
    public static class Globs
    {
        public static SocketServer server;
        public static EyeXHost tracker;

        public const int TRACKER_ACTIVATED = 80;
        public const int TRACKER_DEACTIVATED = 85;

        public const int TRACKER_FAILED_TO_ACTIVATE = 70;
        public const int TRACKER_NOT_ACTIVE = 75;

        public const int TRACKER_ENGINE_NOT_AVAILABLE = 65;
        public const int TRACKER_ENGINE_NOT_RUNNING = 66;
        public const int TRACKER_ENGINE_OUTDATED = 67;

        public const int NO_STATUS = 0;
    }
}
