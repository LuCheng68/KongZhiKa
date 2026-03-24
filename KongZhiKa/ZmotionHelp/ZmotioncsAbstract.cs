using System;
using System.Collections.Generic;
using System.Dynamic;
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

        /// <summary>
        /// 持续移动
        /// </summary>
        /// <param name="nAxis"></param>
        /// <param name="TextBox_units"></param>
        /// <param name="TextBox_lspeed"></param>
        /// <param name="TextBox_speed"></param>
        /// <param name="TextBox_accel"></param>
        /// <param name="TextBox_decel"></param>
        /// <param name="TextBox_sramp"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public abstract ApiResult Vmove(int nAxis, float TextBox_units, float TextBox_lspeed, float TextBox_speed, float TextBox_accel, float TextBox_decel, float TextBox_sramp, int dir);


        /// <summary>
        /// 停止轴
        /// </summary>
        /// <param name="nAxis">停止轴</param>
        /// <returns></returns>
        public abstract ApiResult StopAxis(int nAxis);

        public abstract ApiResult SetLimitAndHome(int axis);

        public abstract void GetInputStatus(int io, out uint status);


    }
}
