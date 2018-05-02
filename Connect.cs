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
 
        public Connect()
        {
            
            
            
        }

        public string ReadConnection(out int result)
        {

            try
            {
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect("127.0.0.1", 8000);
                // use the ipaddress as in the server program

                Console.WriteLine("Connected");
                //Console.Write("Enter the string to be transmitted : ");

                //String str = Console.ReadLine();
                Stream stm = tcpclnt.GetStream();

                byte[] bb = new byte[100];
                string output = "";
                int k = stm.Read(bb, 0, 100);
                for (int i = 0; i < k; i++)
                    output += Convert.ToChar(bb[i]).ToString();
                //settings.Output = settings.Output +  " " + result;

                result = 1;
                tcpclnt.Close();
                stm.Close();
                return output;
            }

            catch (Exception e)
            {
                result = 0;
                Console.WriteLine("Error..... " + e.StackTrace);
                return "error";
            }
        }
        public string WriteConnection(string path)
        {
            try
            {
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect("127.0.0.1", 8000);
                // use the ipaddress as in the server program

                Console.WriteLine("Connected");
                //Console.Write("Enter the string to be transmitted : ");

                //String str = Console.ReadLine();
                Stream stm = tcpclnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(path);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[100];
                string output = "";
                int k = stm.Read(bb, 0, 100);
                for (int i = 0; i < k; i++)
                    output += Convert.ToChar(bb[i]).ToString();
                //settings.Output = settings.Output +  " " + result;

                
                tcpclnt.Close();
                stm.Close();
                return output;
            

            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
               
                return "error";
            }

        }

    }

}


