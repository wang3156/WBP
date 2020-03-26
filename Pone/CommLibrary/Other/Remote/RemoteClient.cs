using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace SmartKernel.Net
{
    /// <summary>
    /// 远程客户端
    /// </summary>
    public class RemoteClient
    {
        /// <summary>
        /// 开始一个远程客户端
        /// </summary>
        public void BeginRemote()
        {
            TcpServerChannel channel = new TcpServerChannel(24563);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Monitor), "MonitorServerUrl", WellKnownObjectMode.SingleCall);
        }
    }
}
