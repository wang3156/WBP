using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    public class CListener : IDisposable
    {

        Socket Client_Socket;
        IPEndPoint ipe;
        /// <summary>
        /// 收到服务器消息后的处理方法
        /// </summary>
        public Action<ConnectCheck> ServerP;


        public CListener()
        {
            /*
             * Create Table E_ServerInfo(
                    ServerIP varchar(128),
                    ServerPort int
               )
            */

        }

        public void BeginConnction(string zkzh)
        {

            DataTable dt = Comm.GetServerSocketInfo();

            while (dt.Rows.Count == 0)
            {
                Thread.Sleep(6 * 1000);
                dt = Comm.GetServerSocketInfo();
            }

            ipe = new IPEndPoint(IPAddress.Parse(dt.Rows[0]["ServerIP"].ToString()), Convert.ToInt32(dt.Rows[0]["ServerPort"]));

            Client_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Client_Socket.Connect(ipe);
                Client_Socket.Send(Encoding.UTF8.GetBytes(zkzh));
            }
            catch (Exception)
            {

                throw;
            }
            //byte[] re = new byte[1024];
            //int recv = Client_Socket.Receive(re);
            //ConnectCheck cc = JsonConvert.DeserializeObject<ConnectCheck>(Encoding.UTF8.GetString(re, 0, recv));
            //if (cc.RCode != ResponseCode.ClientConnected)
            //{
            //    throw new Exception("未能连接到服务器!!");
            //}

            //while (true)
            //{
            //    re = new byte[1024];
            //    recv = Client_Socket.Receive(re);
            //    cc = JsonConvert.DeserializeObject<ConnectCheck>(Encoding.ASCII.GetString(re, 0, recv));
            //    DoByServerCode(cc);
            //}
            Thread t = new Thread(() =>
            {
                byte[] re = new byte[1024];
                int recv = 0;
                ConnectCheck cc;

                while (true)
                {
                    re = new byte[1024];
                    try
                    {
                        recv = Client_Socket.Receive(re);
                    }
                    catch (Exception ex)
                    {
                        DoByServerCode(new ConnectCheck { RCode = ResponseCode.ServerColseConnected });
                        return;
                    }
                    string ss = Encoding.UTF8.GetString(re, 0, recv);
                    if (!ss.EndsWith(Comm.EndMark))
                    {
                        //包不完整
                        continue;
                    }
                    ss.Split(new string[] { Comm.EndMark }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(c =>
                    {
                        cc = JsonConvert.DeserializeObject<ConnectCheck>(c);
                        DoByServerCode(cc);
                    });
                }

            });

            t.IsBackground = true;
            t.Start();


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
                case ResponseCode.ClientColseConnected:
                    break;
                default:
                    if (ServerP != null)
                    {
                        ServerP(cc);
                    }
                    break;
            }
        }

        public void Dispose()
        {
            try
            {
                Client_Socket.Shutdown(SocketShutdown.Both);
            }
            catch
            {

            }
            finally
            {
                Client_Socket.Close();
            }
        }
    }
}
