using System;
using System.Collections.Generic;
using System.Text;

namespace UserChat1
{
    public interface IClient
    {
        int Port { get; set; }

        void Connect();

        void CloseSocket();

    }
}
