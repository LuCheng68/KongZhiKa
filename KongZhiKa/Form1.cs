using KongZhiKa.ZmotionHelp;
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
using Zmotion08;

namespace KongZhiKa
{
    public partial class Form1 : UIForm
    {
        private ZmotioncsAbstract zmotioncs;

        public Form1()
        {
            InitializeComponent();

            zmotioncs = new ZmotionService();
            // 初始化Zmotion服务实例
            // 获取IP地址列表
            List<string> zm = zmotioncs.GetIpAddress();
            // 清空UI中的IP地址项
            ui_ip.Items.Clear();
            // 将获取到的IP地址列表添加到UI中
            ui_ip.Items.AddRange(zm.ToArray());
            // 添加一个固定的IP地址到UI中
            ui_ip.Items.Add("192.168.0.11");


            Debug.WriteLine(zm.ToArray());
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            // 处理按钮点击事件，检查IP地址是否为空或未选择
            if (string.IsNullOrEmpty(ui_ip.Text.ToString()))
            {
                MessageBox.Show("请选择ip地址");
                Debug.WriteLine("ip地址：" + ui_ip.Text.ToString());
                return;
            }
            // 尝试连接指定的IP地址，如果连接失败则显示消息并退出函数
            if (!zmotioncs.OpenZmtion(ui_ip.Text.ToString()).IsSuccess)
            {
                MessageBox.Show("连接失败");
                return;
            }

            // 设置状态栏颜色为绿色，表示连接成功
            toolStripStatusLabel2.BackColor = Color.Green;

            // 设置三个轴的限位和回原点
            zmotioncs.SetLimitAndHome(0);
            zmotioncs.SetLimitAndHome(1);
            zmotioncs.SetLimitAndHome(3);

            // 启动计时器，开始定时操作
            timer1.Enabled = true;

            // 显示连接成功的消息
            MessageBox.Show("连接成功");
        }


        // 当窗体关闭时触发此方法
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            zmotioncs.CloseZmtion();
        }


        private void pictureBox11_MouseDown(object sender, MouseEventArgs e)
        {
            // 将发送者转换为按钮对象
            Button button = sender as Button;
            // 将按钮的Tag属性拆分为字符串数组
            string[] str = button.Tag.ToString().Split(',').ToArray();
            // 调用zmotioncs类的Vmove方法，传入解析后的参数和一些文本框中的浮点数值
            zmotioncs.Vmove(int.Parse(str[0]), Convert.ToSingle(uiTextBox1.Text),
                Convert.ToSingle(uiTextBox2.Text), Convert.ToSingle(uiTextBox3.Text),
                Convert.ToSingle(uiTextBox4.Text), Convert.ToSingle(uiTextBox5.Text),
                Convert.ToSingle(uiTextBox6.Text), 1);
        }


        /// <summary>
        /// 当鼠标在pictureBox11上释放时触发的方法。
        /// 该方法假设sender是一个按钮，并且按钮的Tag属性包含一个轴标识符。
        /// 通过解析Tag属性中的轴标识符，停止相应的运动轴。
        /// </summary>
        /// <param name="sender">触发事件的对象，预期为Button类型。
        /// <param name="e">鼠标事件参数。
        private void pictureBox11_MouseUp(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            string[] str = button.Tag.ToString().Split(',').ToArray();
            zmotioncs.StopAxis(int.Parse(str[0]));
        }


        int[] axis = new int[] { 0, 1, 3 };
        private void timer1_Tick(object sender, EventArgs e)
        {
            // 检查是否连接，如果未打开则直接返回
            if (!zmotioncs.isOpen)
            {
                return;
            }


            zmotioncs.GetPostion(axis, out float[] pos);
            // 更新工具栏状态标签的文本内容，显示位置信息
            toolStripStatusLabel4.Text = pos[1].ToString();
            toolStripStatusLabel8.Text = pos[2].ToString();
            toolStripStatusLabel6.Text = pos[0].ToString();



            // 遍历uiGroupBox3控件集合中的每个控件
            foreach (Control item in uiGroupBox3.Controls)

            {
                // 如果当前控件不是 PictureBox，则跳过本次循环
                if (!(item is PictureBox))
                {
                    continue;
                }
                // 将当前控件转换为 PictureBox 类型
                PictureBox pic = item as PictureBox;
                // 将 PictureBox 的 Tag 属性拆分为字符串数组，使用 "_" 作为分隔符
                string[] str = pic.Tag.ToString().Split("_");
                // 调用 zmotioncs 的 GetInputStatus 方法获取输入状态，status 存储结果
                zmotioncs.GetInputStatus(int.Parse(str[0]), out uint status);

                // 如果状态为 1，则将 PictureBox 的背景颜色设置为红色
                if (status == 1)
                {
                    pic.BackColor = Color.Red;
                }
                // 否则，将 PictureBox 的背景颜色设置为绿色
                else
                {
                    pic.BackColor = Color.Green;
                }
            }

        }
    }
}
