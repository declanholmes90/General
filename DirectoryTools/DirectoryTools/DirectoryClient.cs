using DirectoryTools.DirectoryClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            bool exit = false;

            IPHostEntry localHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            IPAddress ipAddr = localHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddr, PORT);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sender.Connect(remoteEP);

                while(!exit)
                {
                    byte[] buffer = new byte[1024 * 4];
                    int readBytes = sender.Receive(buffer);
                    MemoryStream memoryStream = new MemoryStream();

                    while(readBytes > 0)
                    {
                        memoryStream.Write(buffer, 0, readBytes);

                        if (sender.Available > 0)
                        {
                            readBytes = sender.Receive(buffer);
                        }
                        else
                        {
                            break;
                        }
                    }

                    byte[] totalBytes = memoryStream.ToArray();
                    memoryStream.Close();

                    string readData = Encoding.Default.GetString(totalBytes);
                    List<FileSystemElement> response = JsonConvert.DeserializeObject<List<FileSystemElement>>(readData, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                    Console.WriteLine("JSON Data received from server");
                    Console.WriteLine("Press any key to exit...");

                    Console.Read();

                    exit = true;
                }

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
