using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sccdownloader
{
    public partial class SteamGuard : Form
    {
        public SteamGuard()
        {
            InitializeComponent();
        }

        private void steamGuard_Load(object sender, EventArgs e)
        {

        }

        public string getCodeInput()
        {
            return codeInput.Text;
        }
    }
}
