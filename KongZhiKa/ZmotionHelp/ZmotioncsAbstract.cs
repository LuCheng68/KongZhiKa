using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KongZhiKa.ZmotionHelp
{

    internal abstract class ZmotioncsAbstract
    {
        public IntPtr g_handle;
        public bool isOpen = false;
        /// <summary>
        /// 获取ip
        /// </summary>
        /// <returns></returns>
        public abstract List<string> GetIpAddress();

        /// <summary>
        /// 打开轴卡
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        public abstract ApiResult OpenZmtion(string ip);

        /// <summary>
        /// 关闭轴卡
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        public abstract ApiResult CloseZmtion();
    }
}
