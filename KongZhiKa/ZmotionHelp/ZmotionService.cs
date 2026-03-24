using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zmotion08;

namespace KongZhiKa.ZmotionHelp
{
    // 实现 ZmotioncsAbstract 抽象类的具体服务类
    internal class ZmotionService : ZmotioncsAbstract
    {
        /// <summary>
        /// 关闭Zmtion操作
        /// </summary>
        /// <returns>如果关闭成功返回成功结果，否则返回失败结果</returns>
        public override ApiResult CloseZmtion()
        {
            // 检查当前设备是否已经打开
            if (!isOpen)
            {
                return ApiResult.CreateFail();
            }
            // 关闭轴卡
            int result = zmcaux.ZAux_Close(g_handle);
            return result == 0 ? ApiResult.CreateSuccess() : ApiResult.CreateFail();
        }


        // 实现获取 IP 列表的逻辑
        public override List<string> GetIpAddress()
        {
            // 使用 StringBuilder 接收设备搜索结果
            StringBuilder sb = new StringBuilder();
            // 调用底层 API 搜索以太网设备列表
            int result = zmcaux.ZAux_SearchEthlist(sb, 1000, 1000);
            // 如果搜索失败，返回空列表
            if (result != 0)
            {
                return new List<string>();
            }
            // 提取字符串内容
            string ip = sb.ToString();
            // 按空格分割成多个 IP 地址
            if (ip.Contains(' '))
            {
                return ip.Trim().Split(' ').ToList();
            }
            return new List<string>();

        }
        // 实现打开设备的逻辑
        public override ApiResult OpenZmtion(string ip)
        {
            // 调用底层 API 打开以太网设备
            int result = zmcaux.ZAux_OpenEth(ip, out g_handle);
            // 如果打开失败，返回失败结果
            if (result != 0)
            {
                return ApiResult.CreateFail();
            }
            //打开轴卡
            isOpen = true;
            return ApiResult.CreateSuccess();
        }

        // 实现停止某轴运动的逻辑
        public override ApiResult StopAxis(int nAxis)
        {
            // 检查设备是否已打开
            if (!base.isOpen)
            {
                return ApiResult.CreateFail();
            }
            // 调用底层 API 停止轴
            int result = zmcaux.ZAux_Direct_Single_Cancel(g_handle, nAxis, 2);
            return result == 0 ? ApiResult.CreateSuccess() : ApiResult.CreateFail();
        }

        // 实现控制轴连续运动的逻辑
        public override ApiResult Vmove(int nAxis, float TextBox_units, float TextBox_lspeed, float TextBox_speed, float TextBox_accel, float TextBox_decel, float TextBox_sramp, int dir)
        {
            // 检查设备是否已打开
            if (!isOpen)
            {
                return ApiResult.CreateFail();
            }
            // 新建操作记录列表
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

                // 判断上述操作是否有失败的
                if (list.Sum() > 0)
                {
                    // 如果存在错误码，合并所有错误码并返回失败结果
                    return ApiResult.CreateFail($"执行连续运动失败，错误代码：{list.Sum()}");
                }
                // 成功执行完所有操作后返回成功结果
                return ApiResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                // 如果执行过程中抛异常，则返回失败结果和异常信息
                return ApiResult.CreateFail($"执行连续运动失败，异常信息：{ex.Message}");
            }
        }


        /// <summary>
        /// 获取轴限位
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns></returns>
        public int[] GetLimit(int axis)
        {
            switch (axis)
            {
                // 不同轴号对应不同的限位信号输入端口编号
                case 0:
                    return new int[] { 11, 13, 12 };
                case 1:
                    return new int[] { 8, 9, 10 };
                case 2:
                    return new int[] { 0, 1, 2 };
                default:
                    break;
            }
            // 默认返回空数组
            return new int[] { };

        }

        // 实现设置轴限位和原点信号的功能
        public override ApiResult SetLimitAndHome(int axis)
        {
            // 判断设备是否打开
            if (!base.isOpen)
            {
                return ApiResult.CreateFail();
            }
            // 获取到对应轴的限位信号编号数组
            int[] array = GetLimit(axis);
            // 配置反转信号
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, array[1], 1);
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, array[2], 1);
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, array[0], 1);

            // 配置前进限位输入端口
            zmcaux.ZAux_Direct_SetFwdIn(g_handle, axis, array[1]);
            // 配置后退限位输入端口
            zmcaux.ZAux_Direct_SetRevIn(g_handle, axis, array[2]);
            // 配置原点输入端口
            zmcaux.ZAux_Direct_SetDatumIn(g_handle, axis, array[0]);
            // 返回设置成功的结果
            return ApiResult.CreateSuccess();
        }

        // 实现读取特定 IO 状态的逻辑
        public override void GetInputStatus(int io, out uint status)
        {
            // 初始化返回状态变量为 0
            status = 0;
            if (!isOpen)
            {
                return;
            }
            // 调用底层 API 获取指定 IO 口的状态
            zmcaux.ZAux_Direct_GetIn(g_handle, io, ref status);
        }

        // 实现读取轴位置信息的逻辑
        public override void GetPostion(int[] axisList, out float[] pos)
        {
            // 动态初始化返回数组大小与输入轴数量一致
            pos = new float[axisList.Length];
            if (!isOpen)
            {
                return;
            }
            // 遍历所有要查询位置的轴
            for (int i = 0; i < axisList.Length; i++)
            {
                //获取当前轴坐标
                int nAxis = axisList[i];
                // 调用底层 API 获取轴位置并存入 pos 数组
                zmcaux.ZAux_Direct_GetDpos(g_handle, nAxis, ref pos[i]);
            }
        }
    }
}
