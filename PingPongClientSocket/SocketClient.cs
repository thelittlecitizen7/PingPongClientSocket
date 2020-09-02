using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UserChat1
{
    public class SocketClient : IClient
    {
        public Socket sender { get; set; }

        public IPEndPoint localEndPoint { get; set; }
        public int Port { get; set; }
        public SocketClient(int port)
        {
            Port = port;
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];

            localEndPoint = new IPEndPoint(ipAddr, Port);

            sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect()
        {
            try
            {


                try
                {
                    sender.Connect(localEndPoint);
                    Thread thread = new Thread(ListenAnswer);
                    thread.Start();

                    Console.WriteLine("Socket connected to -> {0} ", sender.RemoteEndPoint.ToString());

                    Console.WriteLine("Please enter input");
                    string input = Console.ReadLine();
                    while (true)
                    {
                        byte[] messageSent = Encoding.ASCII.GetBytes($"{input}");
                        int byteSent = sender.Send(messageSent);
                        Console.WriteLine("Please enter input");
                        input = Console.ReadLine();
                    }
                }


                catch (ArgumentNullException ane)
                {
                    CloseSocket();
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }

                catch (SocketException se)
                {
                    CloseSocket();
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }

                catch (Exception e)
                {
                    CloseSocket();
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }

            catch (Exception e)
            {
                CloseSocket();
                Console.WriteLine(e.ToString());
            }
        }

        public void ListenAnswer()
        {
            while (true)
            {
                try
                {
                    byte[] bytes = new Byte[1024];
                    string data = null;
                    int numByte = sender.Receive(bytes);

                    data = Encoding.ASCII.GetString(bytes, 0, numByte);
                    Console.WriteLine($"Recive from server : {data}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void CloseSocket()
        {
            sender.Close();
        }
    }
}
