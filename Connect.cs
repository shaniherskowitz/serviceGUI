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
        public string closeHandler;
        public bool connected = false;
        private object lockObj = new object();
        public event EventHandler<MessageEventArgs> MessageRecieved;

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
        public void  SubscribeToMessage(IReceiver receiver)
        {
            MessageRecieved += receiver.Subscribe;
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
            while (true)
            {
                try
                {
                    byte[] bb = new byte[1000];
                    string output = "";
                    
                    int k = stm.Read(bb, 0, 1000);
                    for (int i = 0; i < k; i++)
                         output += Convert.ToChar(bb[i]).ToString();
                    

                    if (output[0] == '1') MessageRecieved?.Invoke(this, new MessageEventArgs(output, "Log"));
                    else if (output[0] == '2') MessageRecieved?.Invoke(this, new MessageEventArgs(output, "Settings"));
                    else MessageRecieved?.Invoke(this, new MessageEventArgs(output, "Close"));
                    
                }

                catch (Exception e)
                {
                    //Console.WriteLine("Error..... " + e.StackTrace);
                    return "error";
                }
            }
        }

        public void Write(string path)
        {
            try
            {
                byte[] ba = asen.GetBytes(path);
                Console.WriteLine("Transmitting.....");
                lock (lockObj)
                {
                    stm.Write(ba, 0, ba.Length);
                }
            }
            catch(Exception e)
                {
                Console.WriteLine("Error..... " + e.StackTrace);

                return;
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

    





