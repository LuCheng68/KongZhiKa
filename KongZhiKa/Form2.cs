using SqlSugar;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbType = SqlSugar.DbType;

namespace KongZhiKa
{
    public partial class Form2 : UILoginForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private bool Form2_OnLogin(string userName, string password)
        {
            //数据库
            SqlSugarClient DB = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=localhost;Initial Catalog=DL_02;user ID=sa:password=wlc123wlc;Integrated Security=True;",
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            },
            db =>
            {
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                };
            });

            List<UserEntity> list = DB.Queryable<UserEntity>().ToList();
            foreach (var item in list)
            {
                Debug.WriteLine(item.name);
            }

            if (list.Count == 0)
            {
                this.ShowErrorTip("用户不存在！");
                return false;
            }

            if (userName == list.First().accountNumber && password == list.First().password)
            {
                this.ShowSuccessTip("登录成功！");
                return true;
            }
            this.ShowErrorTip("登录失败！");
            return false;
        }

        private void Form2_ButtonCancelClick(object sender, EventArgs e)
        {
            if (UIMessageBox.Show("确定要退出吗？", "提示", UIStyle.Red, UIMessageBoxButtons.OKCancel))
            {
                Application.Exit();
            }
        }
    }
}
