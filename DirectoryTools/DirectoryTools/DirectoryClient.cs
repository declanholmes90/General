using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryTools
{
    public class DirectoryClient
    {
        private const int PORT = 11000;

        public static void StartClient()
        {
            IPHostEntry localHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            IPAddress ipAddr = localHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddr, PORT);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sender.Connect(remoteEP);

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch
            {

            }
        }
    }
}
