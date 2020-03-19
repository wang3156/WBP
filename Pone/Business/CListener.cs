using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CListener
    {

        Socket Client_Socket;
        IPEndPoint ipe;
        public CListener()
        {
            /*
             * Create Table E_ServerInfo(
                    ServerIP varchar(128),
                    ServerPort int
               )
            */
            DataTable dt = Comm.GetServerSocketInfo();
            if (dt.Rows.Count == 0)
            {
                throw new Exception("服务端未开启!");
            }

            ipe = new IPEndPoint(new IPAddress(Comm.IpToInt(dt.Rows[0]["ServerIP"].ToString())), Convert.ToInt32(dt.Rows[0]["ServerPort"]));
        }

        public void BeginConnction()
        {
            Client_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Client_Socket.Connect(ipe);
            }
            catch (Exception)
            {

                throw;
            }
            byte[] re = new byte[1024];
            int recv = Client_Socket.Receive(re);
            ConnectCheck cc = JsonConvert.DeserializeObject<ConnectCheck>(Encoding.UTF8.GetString(re, 0, recv));
            if (cc.RCode != ResponseCode.ClientConnected)
            {
                throw new Exception("未能连接到服务器!!");
            }

            while (true)
            {
                re = new byte[1024];
                recv = Client_Socket.Receive(re);
                cc = JsonConvert.DeserializeObject<ConnectCheck>(Encoding.ASCII.GetString(re, 0, recv));
                DoByServerCode(cc);
            }

        }

        private void DoByServerCode(ConnectCheck cc)
        {
            //根据服务器传的指定做事情
            switch (cc.RCode)
            {
                case ResponseCode.CheckOnLine:
                    break;
                case ResponseCode.ClientConnected:
                    break;
                case ResponseCode.ServerColseConnected:
                    Client_Socket.Shutdown(SocketShutdown.Both);
                    Client_Socket.Close();
                    break;
                case ResponseCode.ClientColseConnected:
                    break;
                default:
                    break;
            }
        }
    }
}
