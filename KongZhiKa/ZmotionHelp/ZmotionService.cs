using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
