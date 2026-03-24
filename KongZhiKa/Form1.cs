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
            List<string> zm = zmotioncs.GetIpAddress();
            ui_ip.Items.Clear();
            ui_ip.Items.AddRange(zm.ToArray());
            ui_ip.Items.Add ("192.168.0.11");

           Debug.WriteLine(zm.ToArray());
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ui_ip.Text.ToString()))
            {
                MessageBox.Show("请选择ip地址");
                Debug.WriteLine("ip地址：" + ui_ip.Text.ToString());
                return;
            }
            if (!zmotioncs.OpenZmtion(ui_ip.Text.ToString()).IsSuccess)
            {
                MessageBox.Show("连接失败");
                return;
            }
             
            toolStripStatusLabel2.BackColor = Color.Green;

            zmotioncs.SetLimitAndHome(0);
            zmotioncs.SetLimitAndHome(1);
            zmotioncs.SetLimitAndHome(3);

            timer1.Enabled = true;

            MessageBox.Show("连接成功");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            zmotioncs.CloseZmtion();
        }

        private void pictureBox11_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            string[] str = button.Tag.ToString().Split(',').ToArray();
            zmotioncs.Vmove(int.Parse(str[0]), Convert.ToSingle(uiTextBox1.Text),
                Convert.ToSingle(uiTextBox2.Text), Convert.ToSingle(uiTextBox3.Text),
                Convert.ToSingle(uiTextBox4.Text), Convert.ToSingle(uiTextBox5.Text),
                Convert.ToSingle(uiTextBox6.Text), 1);
        }

        private void pictureBox11_MouseUp(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            string[] str = button.Tag.ToString().Split(',').ToArray();
            zmotioncs.StopAxis(int.Parse(str[0]));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!zmotioncs.isOpen)
            {
                return;
            }
            foreach (Control item in uiGroupBox3.Controls)
            {
                if (!(item is PictureBox))
                {
                    continue;
                }
                PictureBox pic = item as PictureBox;
                string[] str = pic.Tag.ToString().Split("_");
                zmotioncs.GetInputStaus(int.Parse(str[0]), out uint status);

                if (status == 1)
                {
                    pic.BackColor = Color.Red;
                }
                else
                {
                    pic.BackColor = Color.Green;
                }
            }
        }
    }
}
