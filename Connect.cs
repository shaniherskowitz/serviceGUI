using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI
{

    class Connect
    {
        private static Connect instance;
        private Stream stm;
        private TcpClient tcpclnt;
        private ASCIIEncoding asen;
        private bool connected = false;
        private object lockObj = new object();

        public static Connect Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Connect();

                }
                return instance;
            }
        }

        private Connect()
        {

            try
            {
                tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect("127.0.0.1", 8000);
                // use the ipaddress as in the server program

                Console.WriteLine("Connected");
                //Console.Write("Enter the string to be transmitted : ");

                //String str = Console.ReadLine();
                stm = tcpclnt.GetStream();
                asen = new ASCIIEncoding();
                connected = true;
            }
            catch (Exception e)
            {

                Console.WriteLine("Error..... " + e.StackTrace);

            }
        }


        public string ReadConnection()
        {
            
            try
            {
                byte[] bb = new byte[1000];
                string output = "";
                int k = stm.Read(bb, 0, 1000);
                for (int i = 0; i < k; i++)
                    output += Convert.ToChar(bb[i]).ToString();
      
                return output;
            }

            catch (Exception e)
            {
                
                //Console.WriteLine("Error..... " + e.StackTrace);
                return "error";
            }
        }
        public string WriteConnection(string path)
        {
            lock (lockObj)
            {
                try
                {

                    byte[] ba = asen.GetBytes(path);
                    Console.WriteLine("Transmitting.....");

                    stm.Write(ba, 0, ba.Length);

                    byte[] bb = new byte[1000];
                    string output = "";
                    int k = stm.Read(bb, 0, 1000);
                    for (int i = 0; i < k; i++)
                        output += Convert.ToChar(bb[i]).ToString();
                    return output;


                }

                catch (Exception e)
                {
                    Console.WriteLine("Error..... " + e.StackTrace);

                    return "error";
                }
            }

        }
        public void CloseConnction()
        {
            if(tcpclnt != null) tcpclnt.Close();
            if(stm != null)stm.Close();
        }

    }
}

    





