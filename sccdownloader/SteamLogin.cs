using System;

using System.Windows.Forms;

namespace sccdownloader
{
    public partial class SteamLogin : Form
    {
        public SteamLogin()
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
            button2.Enabled = usernameInput.Text == "" && passwordInput.Text == "";
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
            return passwordInput.Text;
        }

        public string getUsername()
        {
            return usernameInput.Text;
        }

        public bool getRememberLogin()
        {
            return rememberLogin.Checked;
        }
    }
}
