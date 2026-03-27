namespace KongZhiKa
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Noto Sans SC", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Text = "大林上位机视觉实训";
            // 
            // uiPanel1
            // 
            this.uiPanel1.BackColor = System.Drawing.Color.Transparent;
            this.uiPanel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiPanel1.FillColor = System.Drawing.Color.Lavender;
            this.uiPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.uiPanel1.Location = new System.Drawing.Point(743, 174);
            this.uiPanel1.Opacity = ((byte)(80));
            this.uiPanel1.RadiusSides = ((Sunny.UI.UICornerRadiusSides)((((Sunny.UI.UICornerRadiusSides.LeftTop | Sunny.UI.UICornerRadiusSides.RightTop) 
            | Sunny.UI.UICornerRadiusSides.RightBottom) 
            | Sunny.UI.UICornerRadiusSides.LeftBottom)));
            this.uiPanel1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(190)))), ((int)(((byte)(172)))));
            // 
            // lblSubText
            // 
            this.lblSubText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSubText.ForeColor = System.Drawing.Color.Black;
            this.lblSubText.Location = new System.Drawing.Point(0, 572);
            this.lblSubText.Size = new System.Drawing.Size(263, 29);
            this.lblSubText.Text = "正运动四轴控制台1.0.1";
            // 
            // Form2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(978, 601);
            this.LoginImage = Sunny.UI.UILoginForm.UILoginImage.Custom;
            this.MaximumSize = new System.Drawing.Size(0, 0);
            this.Name = "Form2";
            this.SubText = "正运动四轴控制台1.0.1";
            this.Text = "";
            this.Title = "大林上位机视觉实训";
            this.ButtonCancelClick += new System.EventHandler(this.Form2_ButtonCancelClick);
            this.OnLogin += new Sunny.UI.UILoginForm.OnLoginHandle(this.Form2_OnLogin);
            this.ResumeLayout(false);

        }

        #endregion
    }
}