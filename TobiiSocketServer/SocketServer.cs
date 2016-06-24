using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;

namespace TobiiSocketServer
{
    public class SocketServer
    {
        public List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
        private WebSocketServer server = null;
        private int port;
        private string address;

        public SocketServer(int port, string address)
        {
            this.port = port;
            this.address = address;
        }

        public void start()
        {
            this.server = new WebSocketServer("ws://" + this.address + ":" + this.port.ToString());
            this.server.Start(socket =>
            {

                socket.OnOpen = () =>
                {
                    Console.WriteLine(socket.ConnectionInfo.Host.ToString() + "has connected");
                    allSockets.Add(socket);
                };

                socket.OnClose = () =>
                {
                    Console.WriteLine(socket.ConnectionInfo.Host.ToString() + "has disconnected");
                    allSockets.Remove(socket);
                };

                socket.OnError = (err) =>
                {
                    Console.WriteLine(socket.ConnectionInfo.Host.ToString() + "had error: " + err.ToString());
                };

                socket.OnMessage = message =>
                {
                    handleEyeNavMessage(socket, message);
                    Console.WriteLine(socket.ConnectionInfo.Host.ToString() + "sent message: " + message);
                };
            });
        }

        private void handleEyeNavMessage(IWebSocketConnection socket, String message)
        {
            int status = Globs.NO_STATUS;
            switch (message)
            {
                case "startTracker":
                    status = ClientMessageHandler.startTracker();
                    break;
                case "stopTracker":
                    status = ClientMessageHandler.stopTracker();
                    break;
            }
        }

        public void sendToAll(string message)
        {
            allSockets.ToList().ForEach(s => s.Send(message));
        }
    }
}
