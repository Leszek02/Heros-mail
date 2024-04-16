using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ClientApplication
{
    public partial class LoginPage : Form
    {

        public LoginPage()
        {
            InitializeComponent();
            tableLayoutPanel1.BackColor = Color.FromArgb(36, 36, 36);
            panel1.BackColor = Color.FromArgb(45, 45, 45);
            this.DoubleBuffered = true;
        }

        //background gradient
        private void initialize_background(Object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle background = new Rectangle(0, 0, Width, Height);

            Brush brush = new LinearGradientBrush(background, Color.FromArgb(27, 32, 36), Color.FromArgb(27, 32, 36), 45f);
            graphics.FillRectangle(brush, background);
        }

        private void loginButton1_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(textUsername.Text) && validateIpAddress(comboServerList.Text))
            {
                this.Hide(); 
                string username = textUsername.Text;
                MainPage main = new MainPage(username, comboServerList.Text);
                main.FormClosed += (s, args) => this.Close(); 
                main.Show();
            }
            else
            {
                MessageBox.Show("Wprowadź poprawny adres e-mail", "Niepoprawna nazwa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool validateIpAddress(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
                return false;
            string[] splitValues = input.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
    }
}
