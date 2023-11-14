using MyGameLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class TitleScreen : Form
    {
        private FontFamily titleFont;
        private Font titleFontStyle;
        public static FrmLevel FrmLevelInstance {get; private set;}

        public TitleScreen()
        {
            InitializeComponent();
            InitializeFonts();
            InitializeUI();
            MusicPlayer.PlayTitleSound();
            this.SizeChanged += TitleScreen_SizeChanged;
        }

        private void InitializeFonts()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("./Fonts/pixelated.ttf");
            titleFont = pfc.Families[0];
        }

        private void InitializeUI()
        {
            SetTitleFont();
            SetTitleLocation();
            SetButtonStyle();
            SetButtonSize();
            SetButtonLocation();
            
        }

        private void SetTitleFont()
        {
            float fontSize = this.ClientSize.Width / 12;
            FontStyle fontStyle = FontStyle.Regular;

            titleFontStyle = new Font(titleFont, fontSize, fontStyle);
            label1.Font = titleFontStyle;
        }

        private void SetTitleLocation()
        {
            int x = (this.ClientSize.Width - label1.Width) / 2;
            int y = (this.ClientSize.Height - label1.Height) / 5;
            label1.Location = new Point(x, y);
        }

        private void SetButtonStyle()
        {
            float buttonFontSize = this.ClientSize.Width / 24;
            FontStyle buttonFontStyle = FontStyle.Regular; 
            Font buttonFont = new Font(titleFont, buttonFontSize, buttonFontStyle);
            button1.Font = buttonFont;
        }

        private void SetButtonLocation()
        {
            int buttonX = (this.ClientSize.Width - button1.Width) / 2;
            int buttonY = (2 * this.ClientSize.Height) / 3 - button1.Height; 
            button1.Location = new Point(buttonX, buttonY);
        }

        private void TitleScreen_SizeChanged(object sender, EventArgs e)
        {
            SetTitleFont();
            SetTitleLocation();
            SetButtonStyle();
            SetButtonSize();
            SetButtonLocation();
        }

        private void SetButtonSize()
        {
            int buttonWidth = this.ClientSize.Width / 4;
            int buttonHeight = this.ClientSize.Height / 15;

            button1.Size = new Size(buttonWidth, buttonHeight);
        }

        // Leave empty, needed to render the button
        private void button1_Click(object sender, EventArgs e)
        {
            MusicPlayer.StopTitleSound();
            this.Hide();
            FrmLevelInstance = new FrmLevel();
            FrmLevelInstance.Closed += (s, args) => this.Close();
            FrmLevelInstance.Show();
        }

        // Leave empty, needed to render the label
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}