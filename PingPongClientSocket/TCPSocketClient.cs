using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UserChat1
{
    public class TCPSocketClient : IClient
    {
        public int Port { get; set; }
        public string Adress { get; set; }

        public TCPSocketClient(string adress , int port)
        {
            Adress = IPAddress.Parse(adress).ToString();
            Port = port;
        }

        public void CloseSocket()
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            string hostname = Adress;
            var client = new TcpClient();
            client.Connect(hostname, Port);
            Console.WriteLine("Socket connected to");
            NetworkStream nts = client.GetStream();
            
            while (true)
            {

                Thread thread = new Thread(() => { ListenAnswerTCP(nts); });
                thread.Start();

                while (true)
                {
                    Console.WriteLine("Please enter Name");
                    string name = Console.ReadLine();

                    Console.WriteLine("Please enter age");
                    int age = int.Parse(Console.ReadLine());
                    Person person = new Person(name, age);
                    string input = person.ToString();

                    nts.Write(Encoding.ASCII.GetBytes(input), 0, input.Length);
                }
            }
        }

        private void ListenAnswerTCP(NetworkStream nts)
        {
            while (true)
            {
                byte[] tmpBuff = new byte[1024];
                int readOut = nts.Read(tmpBuff, 0, 1024);
                if (readOut > 0)
                {
                    string recMsg = Encoding.ASCII.GetString(tmpBuff, 0, readOut);
                    Console.WriteLine($"from server : {recMsg}");
                }
            }
        }

    }
}
