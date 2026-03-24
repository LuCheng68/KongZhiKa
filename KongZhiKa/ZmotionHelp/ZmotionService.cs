using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zmotion08;

namespace KongZhiKa.ZmotionHelp
{
    internal class ZmotionService : ZmotioncsAbstract
    {
        public override ApiResult CloseZmtion()
        {
            if (!isOpen)
            {
                return ApiResult.CreateFail();
            }
            int result = zmcaux.ZAux_Close(g_handle);
            return result == 0 ? ApiResult.CreateSuccess() : ApiResult.CreateFail();
        }


        public override List<string> GetIpAddress()
        {
            StringBuilder sb = new StringBuilder();
            int result = zmcaux.ZAux_SearchEthlist(sb, 1000, 1000);
            if (result != 0)
            {
                return new List<string>();
            }

            string ip = sb.ToString();
            if (ip.Contains(' '))
            {
                return ip.Trim().Split(' ').ToList();
            }
            return new List<string>();

        }

        public override ApiResult OpenZmtion(string ip)
        {
            int result = zmcaux.ZAux_OpenEth(ip, out g_handle);
            if (result != 0)
            {
                return ApiResult.CreateFail();
            }
            //打开轴卡
            isOpen = true;
            return ApiResult.CreateSuccess();
        }

        public override ApiResult StopAxis(int nAxis)
        {
            if (!base.isOpen)
            {
                return ApiResult.CreateFail();
            }
            int result = zmcaux.ZAux_Direct_Single_Cancel(g_handle, nAxis, 2);
            return result == 0 ? ApiResult.CreateSuccess() : ApiResult.CreateFail();
        }

        public override ApiResult Vmove(int nAxis, float TextBox_units, float TextBox_lspeed, float TextBox_speed, float TextBox_accel, float TextBox_decel, float TextBox_sramp, int dir)
        {
            if (!isOpen)
            {
                return ApiResult.CreateFail();
            }
            List<int> list = new List<int>();
            try
            {
                //设置轴类型
                list.Add(zmcaux.ZAux_Direct_SetAtype(g_handle, nAxis, 1));
                //设置脉冲当量
                list.Add(zmcaux.ZAux_Direct_SetUnits(g_handle, nAxis, TextBox_units));
                //设置轴起始速度
                list.Add(zmcaux.ZAux_Direct_SetLspeed(g_handle, nAxis, TextBox_lspeed));
                //设置轴速度
                list.Add(zmcaux.ZAux_Direct_SetSpeed(g_handle, nAxis, TextBox_speed));
                //设置轴加速度
                list.Add(zmcaux.ZAux_Direct_SetAccel(g_handle, nAxis, TextBox_accel));
                //设置轴减速度
                list.Add(zmcaux.ZAux_Direct_SetDecel(g_handle, nAxis, TextBox_decel));
                //设置轴S段加速度
                list.Add(zmcaux.ZAux_Direct_SetSramp(g_handle, nAxis, TextBox_sramp));
                //连续运动
                list.Add(zmcaux.ZAux_Direct_Single_Vmove(g_handle, nAxis, dir));


                if (list.Sum() > 0)
                {
                    return ApiResult.CreateFail($"执行连续运动失败，错误代码：{list.Sum()}");
                }
                return ApiResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return ApiResult.CreateFail($"执行连续运动失败，异常信息：{ex.Message}");
            }
        }


        /// <summary>
        /// 获取轴限位
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public int[] GetLimit(int axis)
        {
            switch (axis)
            {
                case 0:
                    return new int[] { 11, 13, 12 };
                case 1:
                    return new int[] { 8, 9, 10 };
                case 2:
                    return new int[] { 0, 1, 2 };
                default:
                    break;
            } 
            return new int[] { };

        }

        public override ApiResult SetLimitAndHome(int axis)
        {
            if (!base.isOpen)
            {
                return ApiResult.CreateFail();
            }
            int[] array = GetLimit(axis);
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, array[1], 1);
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, array[2], 1);
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, array[0], 1);


            zmcaux.ZAux_Direct_SetFwdIn(g_handle, axis, array[1]);
            zmcaux.ZAux_Direct_SetRevIn(g_handle, axis, array[2]);
            zmcaux.ZAux_Direct_SetDatumIn(g_handle, axis, array[0]);
             
            return ApiResult.CreateSuccess();

        }

        public override void GetInputStatus(int io, out uint status)
        {
            status = 0;
            if (!isOpen)
            {
                return;
            }
            zmcaux.ZAux_Direct_GetIn(g_handle, io, ref status);
        }
    }
}
