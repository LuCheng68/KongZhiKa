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

        /// <summary>
        /// 设置指定轴的限位和原点
        /// </summary>
        /// <param name="axis">轴的标识符</param>
        /// <returns>API执行结果</returns>
        public abstract ApiResult SetLimitAndHome(int axis);

        /// <summary>
        /// 获取指定输入的状态
        /// </summary>
        /// <param name="io">输入的标识符</param>
        /// <param name="status">输出的状态值</param>
        public abstract void GetInputStatus(int io, out uint status);

        /// <summary>
        /// 获取指定轴的位置
        /// </summary>
        /// <param name="axisList">轴的标识符数组</param>
        /// <param name="pos">输出的位置值数组</param>
        public abstract void GetPostion(int[] axisList, out float[] pos);

        /// <summary>
        /// 轴相对移动
        /// </summary>
        /// <param name="nAxis"></param>
        /// <param name="TextBox_units"></param>
        /// <param name="TextBox_lspeed"></param>
        /// <param name="TextBox_speed"></param>
        /// <param name="TextBox_accel"></param>
        /// <param name="TextBox_decel"></param>
        /// <param name="TextBox_sramp"></param>
        /// <returns></returns>
        public abstract ApiResult relativeMove(int nAxis, float TextBox_units, float TextBox_lspeed, float TextBox_speed, float TextBox_accel, float TextBox_decel, float TextBox_sramp, float fdistance);



        /// <summary>
        /// 轴绝对移动
        /// </summary>
        /// <param name="nAxis"></param>
        /// <param name="TextBox_units"></param>
        /// <param name="TextBox_lspeed"></param>
        /// <param name="TextBox_speed"></param>
        /// <param name="TextBox_accel"></param>
        /// <param name="TextBox_decel"></param>
        /// <param name="TextBox_sramp"></param>
        /// <returns></returns>
        public abstract ApiResult MoveAS(int nAxis, float TextBox_units, float TextBox_lspeed, float TextBox_speed, float TextBox_accel, float TextBox_decel, float TextBox_sramp, float fdistance);


        public abstract ApiResult MultipleSpindle(int[] nAxis, float TextBox_units, float TextBox_lspeed, float TextBox_speed, float TextBox_accel, float TextBox_decel, float TextBox_sramp, float[] fdistance);


        public abstract ApiResult IsMove(int axis);

        /// <summary>
        /// 等待轴停止
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public abstract ApiResult WaitStop(int axis);

    }
}
