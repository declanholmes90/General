using DirectoryTools.DirectoryClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryTools
{
    public class DirectoryServer
    {
        private const int PORT = 11000;

        public static void BeginListening()
        {
            DirectoryManager directoryManager = new DirectoryManager();

            IPHostEntry localHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = localHostInfo.AddressList[0];
            IPEndPoint ipLocalPoint = new IPEndPoint(ipAddr, PORT);

            Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(ipLocalPoint);
                listener.Listen(10);

                while(true)
                {
                    Console.WriteLine("Awaiting connection from client...");
                    Socket connectionHandler = listener.Accept();
                    Console.WriteLine("Client connected");

                    string jsonData = JsonConvert.SerializeObject(directoryManager.Directories, Formatting.Indented, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                    byte[] dataAsBytes = Encoding.Default.GetBytes(jsonData);

                    connectionHandler.Send(dataAsBytes);

                    Console.WriteLine("JSON Data sent to client.");

                    connectionHandler.Shutdown(SocketShutdown.Both);
                    connectionHandler.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception has occurred...");
                Console.WriteLine(ex);

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }

        public static void Main(string[] args)
        {
            DirectoryServer.BeginListening();
        }
    }
}
