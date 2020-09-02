using Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UserChat1;

namespace PingPongClientSocket
{
    public class ConverterObject
    {


        public byte[] ObjectToByteArray(Person obj)
        {
            if (obj == null)
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, obj);
            return ms.ToArray();


        }

        public Person FromByteArray(byte[] data)
        {
            if (data == null)
                return default(Person);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (Person)obj;
            }
        }




    }
}
