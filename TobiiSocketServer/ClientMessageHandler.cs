using EyeXFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tobii.EyeX.Client;
using Tobii.EyeX.Framework;

namespace TobiiSocketServer
{
    public static class ClientMessageHandler
    {
        private static GazePointDataStream gazeStream;

        public static int startTracker()
        {
            var status = Globs.NO_STATUS;
            if (Globs.tracker == null || !Globs.tracker.IsStarted)
            {
                if (Globs.tracker != null) Globs.tracker.Dispose();

                Globs.tracker = new EyeXHost();
                status = startHost(Globs.tracker);
            }

            if(status != Globs.TRACKER_ACTIVATED && status != Globs.TRACKER_ENGINE_OUTDATED)
                return status;

            if (gazeStream != null)
                gazeStream.Dispose();

            gazeStream = Globs.tracker.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            var gazeListener = new GazeListener();
            gazeStream.Next += (s, e) => gazeListener.onGazeChanged(s, e);

            return Globs.TRACKER_ACTIVATED;
        }

        public static int stopTracker()
        {
            if (Globs.tracker != null)
            {
                Globs.tracker.Dispose();
                Globs.tracker = null;
                Console.WriteLine("Tracker Dectivated Sucessfully");
                return Globs.TRACKER_DEACTIVATED;
            }

            if (gazeStream != null)
            {
                gazeStream.Dispose();
                gazeStream = null;
            }

            Console.WriteLine("Tried to deactivate tracker, but tracker isn't active");
            return Globs.TRACKER_NOT_ACTIVE;
        }

        private static int startHost(EyeXHost host)
        {
            switch (EyeXHost.EyeXAvailability)
            {
                case EyeXAvailability.NotAvailable:
                    Console.WriteLine("This sample requires the EyeX Engine, but it isn't available.");
                    Console.WriteLine("Please install the EyeX Engine and try again.");
                    return Globs.TRACKER_ENGINE_NOT_AVAILABLE;

                case EyeXAvailability.NotRunning:
                    Console.WriteLine("This sample requires the EyeX Engine, but it isn't rnning.");
                    Console.WriteLine("Please make sure that the EyeX Engine is started.");
                    return Globs.TRACKER_ENGINE_NOT_RUNNING;
            }

            Version engineVersion;
            host.Start();
            if (!host.WaitUntilConnected(TimeSpan.FromSeconds(5)))
            {
                Console.WriteLine("Could not connect to EyeX Engine.");
                return Globs.TRACKER_FAILED_TO_ACTIVATE;
            }
            else
            {
                engineVersion = host.GetEngineVersion().Result;
                if (engineVersion.Major != 1 || engineVersion.Major == 1 && engineVersion.Minor < 4)
                {
                    Console.WriteLine("This sample requires EyeX Engine 1.4.");
                    return Globs.TRACKER_ENGINE_OUTDATED;
                }
            }

            Console.WriteLine("Tracker Activated Sucessfully");
            return Globs.TRACKER_ACTIVATED;
        }


        //public void startCalibration()
        //{

        //    ConsoleKey key;
        //    do
        //    {
        //        Console.WriteLine("EYEX CONFIGURATION TOOLS");
        //        Console.WriteLine("========================");
        //        Console.WriteLine();
        //        Console.WriteLine("T) Test calibration");
        //        Console.WriteLine("G) Guest calibration");
        //        Console.WriteLine("R) Recalibrate");
        //        Console.WriteLine("D) Display Setup");
        //        Console.WriteLine("P) Create Profile");

        //        key = Console.ReadKey(true).Key;
        //        switch (key)
        //        {
        //            case ConsoleKey.T:
        //                eyeXHost.LaunchCalibrationTesting();
        //                break;
        //            case ConsoleKey.G:
        //                eyeXHost.LaunchGuestCalibration();
        //                break;
        //            case ConsoleKey.R:
        //                eyeXHost.LaunchRecalibration();
        //                break;
        //            case ConsoleKey.D:
        //                eyeXHost.LaunchDisplaySetup();
        //                break;
        //            case ConsoleKey.P:
        //                eyeXHost.LaunchProfileCreation();
        //                break;
        //        }
        //    } while (key != ConsoleKey.S);
        //}

    }
}


