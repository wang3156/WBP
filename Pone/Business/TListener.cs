using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Business;
using System.Windows.Forms;

namespace Business
{
    public class TListener : IDisposable
    {
        //Dictionary<Socket, Thread> children_sockets = new Dictionary<Socket, Thread>();
        List<Socket> children_sockets = new List<Socket>();
        private byte[] buffer_result = new byte[1024];
        static object li_Lock = new object();



        Socket Server_Socket;
        public string server_ip { get { return ipe.Address.ToString(); } }
        public int server_port { get { return ipe.Port; } }

        IPEndPoint ipe;
        /// <summary>
        /// 监听的线程
        /// </summary>
        Thread t_list;
        /// <summary>
        /// 检查在线的线程
        /// </summary>
        Thread t_check;

        public TListener()
        {
            // string ip = ConfigurationManager.AppSettings["socket_Ip"];
            string port = ConfigurationManager.AppSettings["socket_port"];
            int p = 0;
            if (!int.TryParse(port, out p) || p < 5000)
            {
                throw new Exception
                 ("端口号应该是一个大于5000的数字!");

            }
            ipe = new IPEndPoint(IPAddress.Any, p);//本机预使用的IP和端


        }
        /// <summary>
        /// 打开监听
        /// </summary>
        public void BeginListen()
        {

            Server_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Server_Socket.Bind(ipe);

            //设置监听队列, 最大10个
            Server_Socket.Listen(10);
            t_list = new Thread(ListenClientConnect);
            t_list.IsBackground = true;
            t_list.Start();

            t_check = new Thread(CheckConnected);
            t_check.IsBackground = true;
            t_check.Start();
        }



        /// <summary>
        /// 监听客户端连接
        /// </summary>
        private void ListenClientConnect()
        {
            while (true)
            {

                Socket clientSocket = Server_Socket.Accept();
                lock (li_Lock)
                {
                    if (!children_sockets.Contains(clientSocket))
                    {
                        //Thread receiveThread = new Thread(ReceiveMessage);
                        //receiveThread.Start(clientSocket);
                        SendTOClient(new ConnectCheck() { RCode = ResponseCode.ClientConnected }, clientSocket);
                        AddClientToList(clientSocket);
                    }
                }

            }

        }


        private void CheckConnected()
        {
            while (true)
            {
                lock (li_Lock)
                {
                    children_sockets.ForEach(c =>
                    {
                        SendTOClient(new ConnectCheck() { RCode = ResponseCode.CheckOnLine }, c);
                    });
                }
                Application.DoEvents();
                Thread.Sleep(5 * 1000);
            }
        }

        /// <summary>
        /// 统一编码发送数据给客户端
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="client"></param>
        void SendTOClient(ConnectCheck msg, Socket client)
        {
            try
            {
                client.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg)));
            }
            catch (Exception ex)
            {
                RemoveClientToList(client);
            }
        }

        #region 添加移除和添加队列的方法.方便在添加或移除时做其它事
        void AddClientToList(Socket client)
        {
            lock (li_Lock)
            {
                children_sockets.Add(client);
            }
        }

        void RemoveClientFromList(Socket client)
        {

            lock (li_Lock)
            {
                children_sockets.Remove(client);
            }

            try
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception)
            {


            }

        }
        #endregion

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //通过clientSocket接收数据
                    int receiveNumber = myClientSocket.Receive(buffer_result);
                    Console.WriteLine("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(buffer_result, 0, receiveNumber));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }

        public void Dispose()
        {
            lock (li_Lock)
            {
                //通知所有用户已经终止了
                children_sockets.ForEach(c =>
                {
                    SendTOClient(new ConnectCheck() { RCode = ResponseCode.ServerColseConnected }, c);
                });

                int cc = children_sockets.Count - 1;
                for (int i = cc; i > -1; i--)
                {
                    RemoveClientToList(children_sockets[i]);
                }
            }
            t_list.Abort();           
            t_check.Abort();

            Server_Socket.Shutdown(SocketShutdown.Both);
            Server_Socket.Close();
        }
    }



}
