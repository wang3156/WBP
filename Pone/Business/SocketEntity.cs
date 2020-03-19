using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ConnectCheck
    {
        /// <summary>
        /// 状态码  10001 在线检测   10002 已接收到客户端连接(响应该Code给客户端)
        /// </summary>
        public ResponseCode RCode;

        public string Data = "";

    }

    public enum ResponseCode
    {
        /// <summary>
        /// 检查是否在线
        /// </summary>
        CheckOnLine = 10001,
        /// <summary>
        /// 第一次收到客户端连接时告诉客户端连成功了
        /// </summary>
        ClientConnected,
        /// <summary>
        /// 服务端断开连接
        /// </summary>
        ServerColseConnected,
        /// <summary>
        /// 客户端断开连接
        /// </summary>
        ClientColseConnected,
        /// <summary>
        /// 结束考试
        /// </summary>
        EndExam,
        /// <summary>
        /// 禁止考试
        /// </summary>
        DisabledExam
    }
}
