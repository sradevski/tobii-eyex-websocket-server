using System;
using EyeXFramework;
using Tobii.EyeX.Framework;
using Tobii.EyeX.Client;
using Fleck;
namespace TobiiSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 8887;
            string address = "127.0.0.1";
            if (args.Length == 1)
            {
                try
                {
                    port = Int32.Parse(args[0]);
                }
                catch (Exception e)
                {
                }
            }
            try
            {
                Globs.server = new SocketServer(port, address);
                Globs.server.start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to start server on port " + port.ToString());
            }

            Console.ReadLine();
        }
    }
}
