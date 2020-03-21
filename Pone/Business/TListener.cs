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
using System.Data;

namespace Business
{
    public class TListener : IDisposable
    {
        //Dictionary<Socket, Thread> children_sockets = new Dictionary<Socket, Thread>();
        List<SocketUserInfo> children_sockets = new List<SocketUserInfo>();
        private byte[] buffer_result = new byte[1024];
        public static object li_Lock = new object();



        Socket Server_Socket;
        public string server_ip { get { return ipe.Address.ToString(); } }
        public int server_port { get { return ipe.Port; } }

        IPEndPoint ipe;
        /// <summary>
        /// 监听的线程
        /// </summary>
        Thread t_list;
        bool t_listExit = false;
        /// <summary>
        /// 检查在线的线程
        /// </summary>
        Thread t_check;
        bool t_checkExit = false;


        public Action<SocketUserInfo> AddList;
        public Action<SocketUserInfo> RemoveList;

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
            ipe = new IPEndPoint(IPAddress.Parse(Comm.GetLocalIP()), p);//本机预使用的IP和端


        }
        /// <summary>
        /// 打开监听
        /// </summary>
        public void BeginListen()
        {
            Comm.UpdateServerInfo(server_ip, server_port);

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
                Socket clientSocket = null;
                if (t_listExit)
                {
                    return;
                }

                try
                {
                    clientSocket = Server_Socket.Accept();
                }
                catch (Exception ex)
                {  //这里异常说明服务器可能关闭了
                    return;
                }
                lock (li_Lock)
                {
                    buffer_result = new byte[1024];
                    int len = clientSocket.Receive(buffer_result);
                    string zkzh = Encoding.UTF8.GetString(buffer_result, 0, len);
                    DataTable dt = Comm.GetUserInfoByZKZH(zkzh);
                    if (dt.Rows.Count == 0)
                    {
                        SendTOClient(new ConnectCheck() { RCode = ResponseCode.ServerColseConnected }, clientSocket);
                    }
                    else
                    {
                        DataRow row = dt.Rows[0];
                        AddClientToList(new SocketUserInfo() { socket = clientSocket, ZKZH = Convert.ToString(row["ZKZH"]), XH = Convert.ToString(row["XH"]), UName = Convert.ToString(row["UName"]), EID = Convert.ToInt32(row["EID"]), STID = Convert.ToInt32(row["STID"]) });
                        SendTOClient(new ConnectCheck() { RCode = ResponseCode.ClientConnected }, clientSocket);
                    }



                }

            }

        }

        public string GetHostName(string zkzh, int EID)
        {
           return (children_sockets.Where(c => c.ZKZH == zkzh && c.EID == EID).FirstOrDefault()?.socket.RemoteEndPoint as IPEndPoint)?.Address.ToString();
        }

        /// <summary>
        /// 考试结束
        /// </summary>
        /// <param name="EID"></param>
        public void EndExamByEID(IEnumerable<int> EID)
        {
            lock (li_Lock)
            {
                EID.ToList().ForEach(c =>
                {
                    SendTOClient(new ConnectCheck() { RCode = ResponseCode.EndExam }, children_sockets.Where(cc => cc.EID == c).FirstOrDefault()?.socket);
                });

            }
        }

        public void StartExamByEID(IEnumerable<int> EID)
        {
            lock (li_Lock)
            {
                EID.ToList().ForEach(c =>
                {
                    SendTOClient(new ConnectCheck() { RCode = ResponseCode.StartExam }, children_sockets.Where(cc => cc.EID == c).FirstOrDefault()?.socket);
                });
            }

        }


        private void CheckConnected()
        {
            while (true)
            {
                lock (li_Lock)
                {
                    int ii = children_sockets.Count - 1;
                    for (int i = ii; i > -1; i--)
                    {
                        if (t_checkExit)
                        {
                            return;
                        }

                        SendTOClient(new ConnectCheck() { RCode = ResponseCode.CheckOnLine }, children_sockets[ii].socket);
                    }

                    if (t_checkExit)
                    {
                        return;
                    }
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
            if (client == null)
            {
                return;
            }
            try
            {

                client.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg) + Comm.EndMark));
            }
            catch (Exception ex)
            {
                RemoveClientFromList(children_sockets.Where(c => c.socket == client).FirstOrDefault());
            }
        }

        public void SendTOClient(ConnectCheck msg, string ZKZH)
        {
            SendTOClient(msg, children_sockets.Where(c => c.ZKZH == ZKZH).FirstOrDefault()?.socket);

        }


        #region 添加移除和添加队列的方法.方便在添加或移除时做其它事
        void AddClientToList(SocketUserInfo client)
        {
            lock (li_Lock)
            {
                children_sockets.Add(client);
            }
            AddList?.Invoke(client);
        }

        void RemoveClientFromList(SocketUserInfo client)
        {
            if (client == null)
            {
                return;
            }

            RemoveList?.Invoke(client);

            lock (li_Lock)
            {
                children_sockets.Remove(client);
            }

            try
            {
                client.socket.Shutdown(SocketShutdown.Both);

            }
            catch
            {

            }
            finally
            {
                client.socket.Close();
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

            using (TeacherB tb = new TeacherB())
            {
                tb.EmptyServerInfo();
            }

            bool have = false;
            t_listExit = t_checkExit = true;

            if (have = children_sockets.Count > 0)
            {
                lock (li_Lock)
                {
                    //通知所有用户已经终止了
                    children_sockets.ForEach(c =>
                    {
                        SendTOClient(new ConnectCheck() { RCode = ResponseCode.ServerColseConnected }, c.socket);
                    });

                    int cc = children_sockets.Count - 1;
                    for (int i = cc; i > -1; i--)
                    {
                        RemoveClientFromList(children_sockets[i]);
                    }
                }

            }
            try
            {
                //这里有很大可能会出错.但如果 socket在执行中需要先关闭所以要先执行一下
                Server_Socket.Shutdown(SocketShutdown.Both);
            }
            catch
            {

            }
            finally
            {
                Server_Socket.Close();
            }
            t_list.Abort();
            t_check.Abort();
        }
    }





}
