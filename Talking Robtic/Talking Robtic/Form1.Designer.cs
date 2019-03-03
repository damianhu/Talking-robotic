namespace Talking_Robtic
{
    partial class Robtic
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Record_BT = new System.Windows.Forms.Button();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.Record_Box = new System.Windows.Forms.TextBox();
            this.Robot_Box = new System.Windows.Forms.TextBox();
            this.Talk_Box = new System.Windows.Forms.TextBox();
            this.Voice_BT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Record_BT
            // 
            this.Record_BT.Location = new System.Drawing.Point(179, 247);
            this.Record_BT.Name = "Record_BT";
            this.Record_BT.Size = new System.Drawing.Size(103, 39);
            this.Record_BT.TabIndex = 0;
            this.Record_BT.Text = "录音";
            this.Record_BT.UseVisualStyleBackColor = true;
            this.Record_BT.Click += new System.EventHandler(this.Record_BT_Click);
            // 
            // Record_Box
            // 
            this.Record_Box.Location = new System.Drawing.Point(61, 28);
            this.Record_Box.Multiline = true;
            this.Record_Box.Name = "Record_Box";
            this.Record_Box.Size = new System.Drawing.Size(617, 213);
            this.Record_Box.TabIndex = 1;
            // 
            // Robot_Box
            // 
            this.Robot_Box.Location = new System.Drawing.Point(61, 350);
            this.Robot_Box.Multiline = true;
            this.Robot_Box.Name = "Robot_Box";
            this.Robot_Box.Size = new System.Drawing.Size(617, 216);
            this.Robot_Box.TabIndex = 3;
            // 
            // Talk_Box
            // 
            this.Talk_Box.Location = new System.Drawing.Point(61, 292);
            this.Talk_Box.Name = "Talk_Box";
            this.Talk_Box.Size = new System.Drawing.Size(617, 25);
            this.Talk_Box.TabIndex = 4;
            this.Talk_Box.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Talk_Box_KeyDown);
            // 
            // Voice_BT
            // 
            this.Voice_BT.Location = new System.Drawing.Point(457, 247);
            this.Voice_BT.Name = "Voice_BT";
            this.Voice_BT.Size = new System.Drawing.Size(103, 39);
            this.Voice_BT.TabIndex = 5;
            this.Voice_BT.Text = "识别";
            this.Voice_BT.UseVisualStyleBackColor = true;
            this.Voice_BT.Click += new System.EventHandler(this.Voice_BT_Click);
            // 
            // Robtic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 600);
            this.Controls.Add(this.Voice_BT);
            this.Controls.Add(this.Talk_Box);
            this.Controls.Add(this.Robot_Box);
            this.Controls.Add(this.Record_Box);
            this.Controls.Add(this.Record_BT);
            this.Name = "Robtic";
            this.Text = "聊天机器人";
            this.Click += new System.EventHandler(this.Voice_BT_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Record_BT;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.TextBox Record_Box;
        private System.Windows.Forms.TextBox Robot_Box;
        private System.Windows.Forms.TextBox Talk_Box;
        private System.Windows.Forms.Button Voice_BT;
    }
}

