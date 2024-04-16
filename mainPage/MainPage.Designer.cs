using System.Drawing;

namespace ClientApplication
{
    partial class MainPage
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
        private void InitializeComponent(Color backgroundColor)
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.NewMessageButton = new ClientApplication.CustomControls.LoginButton();
            this.ReceivedButton = new ClientApplication.CustomControls.LoginButton();
            this.SendButton = new ClientApplication.CustomControls.LoginButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.75F));
            this.tableLayoutPanel1.Controls.Add(this.MainPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ButtonPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = backgroundColor;
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(136, 3);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(661, 444);
            this.MainPanel.TabIndex = 4;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ButtonPanel.Controls.Add(this.tableLayoutPanel2);
            this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonPanel.Location = new System.Drawing.Point(3, 3);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(127, 444);
            this.ButtonPanel.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.NewMessageButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ReceivedButton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.SendButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 154F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(127, 444);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // NewMessageButton
            // 
            this.NewMessageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(106)))), ((int)(((byte)(164)))));
            this.NewMessageButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.NewMessageButton.FlatAppearance.BorderSize = 0;
            this.NewMessageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewMessageButton.ForeColor = System.Drawing.Color.White;
            this.NewMessageButton.Location = new System.Drawing.Point(3, 3);
            this.NewMessageButton.Name = "NewMessageButton";
            this.NewMessageButton.Size = new System.Drawing.Size(121, 46);
            this.NewMessageButton.TabIndex = 1;
            this.NewMessageButton.Text = "Nowa wiadomość";
            this.NewMessageButton.UseVisualStyleBackColor = false;
            this.NewMessageButton.Click += new System.EventHandler(this.NewMessageButton_Click);
            // 
            // ReceivedButton
            // 
            this.ReceivedButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(106)))), ((int)(((byte)(164)))));
            this.ReceivedButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.ReceivedButton.FlatAppearance.BorderSize = 0;
            this.ReceivedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReceivedButton.ForeColor = System.Drawing.Color.White;
            this.ReceivedButton.Location = new System.Drawing.Point(3, 106);
            this.ReceivedButton.Name = "ReceivedButton";
            this.ReceivedButton.Size = new System.Drawing.Size(121, 23);
            this.ReceivedButton.TabIndex = 2;
            this.ReceivedButton.Text = "Odebrane";
            this.ReceivedButton.UseVisualStyleBackColor = false;
            this.ReceivedButton.Click += new System.EventHandler(this.ReceivedButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(106)))), ((int)(((byte)(164)))));
            this.SendButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.SendButton.FlatAppearance.BorderSize = 0;
            this.SendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendButton.ForeColor = System.Drawing.Color.White;
            this.SendButton.Location = new System.Drawing.Point(3, 66);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(121, 23);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "Wysłane";
            this.SendButton.UseVisualStyleBackColor = false;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 927);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(908, 675);
            this.Icon = Properties.Resources.mailLogo;
            this.Name = "MainPage";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ButtonPanel.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomControls.LoginButton NewMessageButton;
        private CustomControls.LoginButton ReceivedButton;
        private CustomControls.LoginButton SendButton;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel ButtonPanel;
        private System.Windows.Forms.Label label1;
    }
}