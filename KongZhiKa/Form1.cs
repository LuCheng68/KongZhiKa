using KongZhiKa.ZmotionHelp;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            uiComboBox1.Items.Clear();
            uiComboBox1.Items.AddRange(zm.ToArray());

        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uiComboBox1.SelectedText.ToString()))
            {
                MessageBox.Show("请选择ip地址");
                return;
            }

            if (!zmotioncs.OpenZmtion(uiComboBox1.SelectedText.ToString()).IsSuccess)
            {
                MessageBox.Show("连接失败");
            }
            toolStripStatusLabel2.BackColor = Color.Green;
            MessageBox.Show("连接成功");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            zmotioncs.CloseZmtion();
        }
    }
}