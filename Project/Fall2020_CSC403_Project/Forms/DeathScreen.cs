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
        public static DeathScreen Instance { get; private set; }

        private FrmLevel frmLevel = FrmLevel.Instance;
        public DeathScreen()
        {
            InitializeComponent();
            Instance = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DeathScreen_Load(object sender, EventArgs e)
        {
            frmLevel.Visible = false;
        }
    }
}
