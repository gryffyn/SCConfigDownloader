using System;
using System.Windows.Forms;

namespace sccdownloader
{
    public partial class SteamLoginFrm : Form
    {
        public SteamLoginFrm()
        {
            InitializeComponent();
        }

        private void steamLogin_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        void updateInputs()
        {
            button2.Enabled = textBox1.Text == "" && textBox2.Text == "";
        }

        private void usernameInput_TextChanged(object sender, EventArgs e)
        {
            updateInputs();
        }

        private void passwordInput_TextChanged(object sender, EventArgs e)
        {
            updateInputs();
        }

        public string getPassword()
        {
            return textBox2.Text;
        }

        public string getUsername()
        {
            return textBox1.Text;
        }

        public bool getRememberLogin()
        {
            return checkBox1.Checked;
        }
    }
}