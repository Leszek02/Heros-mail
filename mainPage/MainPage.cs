using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace ClientApplication
{
    public partial class MainPage : Form
    {

        private string userName;
        private float labelHeight = 30F;
        private Color backgroundColor = Color.FromArgb(36, 36, 36);
        emailClient.ServerHandler.EmailClient client = new emailClient.ServerHandler.EmailClient();

        public MainPage(string username, string ipAdress)
        {
            InitializeComponent(backgroundColor);
            int conn = client.ConnectToServer(ipAdress, username);
            if (conn == 0)
            {
                //TODO: bad connection handle
                MessageBox.Show("Nie udało się nawiązać połączenia ze wskazanym adresem.", "Brak połączenia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            userName = username;
            this.Text = "Poczta Heros: Witaj " + username;
            tableLayoutPanel1.CellPaint += tableLayoutPanel1_CellPaint;
            this.Resize += ResizeForm;
        }

        private void tableLayoutPanel1_CellPaint(Object sender, TableLayoutCellPaintEventArgs e)
        {
                using (SolidBrush brush = new SolidBrush(backgroundColor))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
        }

        private void NewMessageButton_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();

            Label recipientLabel = new Label();
            recipientLabel.Text = "Adresat:";
            recipientLabel.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            recipientLabel.Location = new Point(110, 15);
            recipientLabel.ForeColor = Color.White;
            this.MainPanel.Controls.Add(recipientLabel);

            Label titleLabel = new Label();
            titleLabel.Text = "Tytuł:";
            titleLabel.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            titleLabel.Location = new Point(110, (int)(15 + labelHeight));
            titleLabel.ForeColor = Color.White;
            this.MainPanel.Controls.Add(titleLabel);

            TextBox recipient = new TextBox();
            recipient.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            recipient.BorderStyle = BorderStyle.FixedSingle;
            recipient.Width = (int)(MainPanel.Width * 0.98F - 200);
            recipient.BackColor = backgroundColor;
            recipient.Location = new Point(210, 15);
            recipient.ForeColor = Color.White;
            recipient.Tag = "NewMessage";
            this.MainPanel.Controls.Add(recipient);

            TextBox title = new TextBox();
            title.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            title.BorderStyle = BorderStyle.FixedSingle;
            title.Width = (int)(MainPanel.Width * 0.98F - 200);
            title.Location = new Point(210, (int)(15 + labelHeight));
            title.ForeColor = Color.White;
            title.BackColor = backgroundColor;
            title.Tag = "NewMessage";
            this.MainPanel.Controls.Add(title);

            TextBox mailContent = new TextBox();
            mailContent.Multiline = true;
            mailContent.WordWrap = true;
            mailContent.ScrollBars = ScrollBars.Vertical;
            mailContent.Font = new System.Drawing.Font("Comic Sans MS", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            mailContent.BorderStyle = BorderStyle.FixedSingle;
            mailContent.Width = (int)(MainPanel.Width * 0.98F);
            mailContent.Height = (int)(MainPanel.Height * 0.98F - (15 + 2 * labelHeight));
            mailContent.Location = new Point(10, (int)(15 + 2 * labelHeight));
            mailContent.ForeColor = Color.White;
            mailContent.BackColor = backgroundColor;
            this.MainPanel.Controls.Add(mailContent);

            Button sendMail = new Button();
            sendMail.Location = new Point(10, 15);
            sendMail.Size = new Size(95, 55);
            sendMail.Font = new System.Drawing.Font("Comic Sans MS", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            sendMail.Text = @"Wyślij wiadomość";
            sendMail.ForeColor = Color.White;
            sendMail.BackColor = Color.FromArgb(31, 106, 164);
            sendMail.FlatStyle = FlatStyle.Flat;
            sendMail.FlatAppearance.BorderColor = Color.FromArgb(8, 11, 94);
            sendMail.Click += (s, EventArgs) => { sendMail_Click(s, EventArgs, title.Text, recipient.Text, mailContent.Text); };
            this.MainPanel.Controls.Add(sendMail);
        }

        private void SendButton_Click(Object sender, EventArgs e)
        {
            //Tutaj czwórka
                MainPanel.Controls.Clear();
                BackgroundWorker printMail1 = new BackgroundWorker();
                string result = string.Empty;
                printMail1.DoWork += (s, args) =>
                {
                    //Debug.WriteLine("Próbuje dostać");
                    result = client.ReceiveSendMail();
                };

                printMail1.RunWorkerCompleted += (s, args) =>
                {
                    List<string> titles = result.Split('\n').ToList();

                    int labelNumber = 0;

                    foreach (string mail in titles)
                    {
                        if (string.IsNullOrEmpty(mail))
                            break;
                        Label label = new Label();
                        label.Text = mail;
                        label.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                        label.BorderStyle = BorderStyle.FixedSingle;
                        label.Width = (int)(MainPanel.Width * 0.98F);
                        label.Location = new Point(10, (int)(15 + labelNumber * labelHeight));
                        label.ForeColor = Color.White;
                        label.Click += (Sender, E) => printMail(mail, 1);
                        label.MouseEnter += MouseEnterLabel;
                        label.Tag = "LabelList";
                        label.MouseLeave += MouseLeaveLabel;
                        this.MainPanel.Controls.Add(label);
                        labelNumber++;
                    }

                    if (labelNumber == 0)
                    {
                        Label label = new Label();
                        label.Text = "Nie wysłałeś jeszcze żadnych wiadomości";
                        label.Width = MainPanel.Width;
                        label.Location = new Point(10, (int)(10 + labelNumber * labelHeight));
                        label.ForeColor = Color.White;
                        this.MainPanel.Controls.Add(label);
                        labelNumber++;
                    }
                };

                printMail1.RunWorkerAsync();
        }

        private void ReceivedButton_Click(Object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            BackgroundWorker printMail2 = new BackgroundWorker();
            string result = string.Empty;

            printMail2.DoWork += (s, args) =>
            {
                //Debug.WriteLine("Próbuje dostać");
                result = client.ReceiveReceivedMail();
            };

            printMail2.RunWorkerCompleted += (s, args) =>
            {
                List<string> titles = result.Split('\n').ToList();

                int labelNumber = 0;

                foreach (string mail in titles)
                {
                    if (string.IsNullOrEmpty(mail))
                        break;
                    Label label = new Label();
                    label.Text = mail;
                    label.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                    label.BorderStyle = BorderStyle.FixedSingle;
                    label.Width = (int)(MainPanel.Width * 0.98F);
                    label.Location = new Point(10, (int)(15 + labelNumber * labelHeight));
                    label.ForeColor = Color.White;
                    label.Click += (Sender, E) => printMail(mail, 0);
                    label.MouseEnter += MouseEnterLabel;
                    label.MouseLeave += MouseLeaveLabel;
                    label.Tag = "LabelList";
                    this.MainPanel.Controls.Add(label);
                    labelNumber++;
                }

                if (labelNumber == 0)
                {
                    Label label = new Label();
                    label.Text = "Nie dostałeś jeszcze żadnych wiadomości";
                    label.Width = MainPanel.Width;
                    label.Location = new Point(10, (int)(10 + labelNumber * labelHeight));
                    label.ForeColor = Color.White;
                    this.MainPanel.Controls.Add(label);
                    labelNumber++;
                }
            };

            printMail2.RunWorkerAsync();
        }

        protected void printMail(string mail, int mode)
        {
            MainPanel.Controls.Clear();

            List<string> result = new List<string>();
            BackgroundWorker printMail = new BackgroundWorker();

            printMail.DoWork += (s, args) =>
            {
                if (mode == 1) 
                    result = client.GetSenderMail(mail);
                else
                    result = client.GetReceiverMail(mail);
            };

            printMail.RunWorkerCompleted += (s, args) =>
            {
                
                Label recipientLabel = new Label();
                recipientLabel.Text = "Adresat:";
                recipientLabel.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                recipientLabel.Location = new Point(10, 15);
                recipientLabel.ForeColor = Color.White;
                this.MainPanel.Controls.Add(recipientLabel);

                Label titleLabel = new Label();
                titleLabel.Text = "Tytuł:";
                titleLabel.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                titleLabel.Location = new Point(10, (int)(15 + labelHeight));
                titleLabel.ForeColor = Color.White;
                this.MainPanel.Controls.Add(titleLabel);

                Label recipient = new Label();
                if (mode == 1)
                    recipient.Text = result[1]; //TODO
                else
                    recipient.Text = result[0];
                recipient.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                recipient.BorderStyle = BorderStyle.FixedSingle;
                recipient.Width = (int)(MainPanel.Width * 0.98F - 100);
                recipient.Location = new Point(110, 15);
                recipient.ForeColor = Color.White;
                recipient.Tag = "ShowMessage";
                this.MainPanel.Controls.Add(recipient);

                Label title = new Label();
                title.Text = result[2];
                title.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                title.BorderStyle = BorderStyle.FixedSingle;
                title.Width = (int)(MainPanel.Width * 0.98F - 100);
                title.Location = new Point(110, (int)(15 + labelHeight));
                title.ForeColor = Color.White;
                title.Tag = "ShowMessage";
                this.MainPanel.Controls.Add(title);

                TextBox mailContent = new TextBox();
                mailContent.Multiline = true;
                mailContent.ReadOnly = true;
                mailContent.WordWrap = true;
                mailContent.ScrollBars = ScrollBars.Vertical;
                mailContent.Text = result[3];
                mailContent.Font = new System.Drawing.Font("Comic Sans MS", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                mailContent.BorderStyle = BorderStyle.FixedSingle;
                mailContent.Width = (int)(MainPanel.Width * 0.98F);
                mailContent.Height = (int)(MainPanel.Height * 0.98F - (15 + 2 * labelHeight));
                mailContent.Location = new Point(10, (int)(15 + 2 * labelHeight));
                mailContent.ForeColor = Color.White;
                mailContent.BackColor = backgroundColor;
                this.MainPanel.Controls.Add(mailContent);
            };

            printMail.RunWorkerAsync();
        }

        protected void ResizeForm(Object sender, EventArgs e)
        {
            foreach (Control control in MainPanel.Controls)
            {
                if ((string)control.Tag == "LabelList")
                {
                    control.Width = (int)(MainPanel.Width * 0.98F);
                }
                else
                {
                    if ((string)control.Tag == "ShowMessage")
                    {
                        control.Width = (int)(MainPanel.Width * 0.98F - 100);
                    }
                    else if ((string)control.Tag == "NewMessage")
                    {
                        control.Width = (int)(MainPanel.Width * 0.98F - 200);
                    }
                    else if (control is TextBox)
                    {
                        control.Width = (int)(MainPanel.Width * 0.98F);
                        control.Height = (int)(MainPanel.Height * 0.98F - (15 + 2 * labelHeight));
                    }
                }
            }
        }

        private void MouseEnterLabel(Object sender, EventArgs e)
        {
            if (sender is Label label)
                label.BackColor = Color.FromArgb(70, 70, 70);
        }

        private void MouseLeaveLabel(Object sender, EventArgs e)
        {
            if (sender is Label label)
                label.BackColor = backgroundColor;
        }

        private void sendMail_Click(Object sender, EventArgs e, string title, string recipient, string mailContent)
        {
            
            BackgroundWorker sendMail = new BackgroundWorker();
            sendMail.DoWork += (s, args) =>
            {
                client.SendMail(title, userName, mailContent, recipient);
            };
            sendMail.RunWorkerAsync();

        }
    }
}
