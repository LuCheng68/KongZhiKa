using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KongZhiKa
{
    //实体类与数据库表结构一致， 表名一致，如果不一致 则使用[SugarTable("表名")]
    [SugarTable("hx_user")]
    public class UserEntity
    {
        public int uid { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string accountNumber { get; set; }
        public string password { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }



    }
}
