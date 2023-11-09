using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class DeathScreen : Form
    {
        private FrmLevel frmLevel;
        public DeathScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DeathScreen_Load(object sender, EventArgs e)
        {
            FrmLevel frmLevel = new FrmLevel();
            frmLevel.Visible = false;
        }
    }
}
