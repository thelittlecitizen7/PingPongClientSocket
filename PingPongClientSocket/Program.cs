using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UserChat1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the ip");
            string adress = Console.ReadLine();

            Console.WriteLine("Please enter the port");
            int port = int.Parse(Console.ReadLine());

            IClient client = new TCPSocketClient(adress,port);
            
            client.Connect();

        }
    }
}
