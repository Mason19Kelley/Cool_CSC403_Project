namespace Fall2020_CSC403_Project.Forms
{
    partial class BonusLevel
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.floor1 = new System.Windows.Forms.PictureBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.character = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.floor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.character)).BeginInit();
            this.SuspendLayout();
            // 
            // floor1
            // 
            this.floor1.BackColor = System.Drawing.SystemColors.InfoText;
            this.floor1.Location = new System.Drawing.Point(-2, 676);
            this.floor1.Margin = new System.Windows.Forms.Padding(2);
            this.floor1.Name = "floor1";
            this.floor1.Size = new System.Drawing.Size(1181, 51);
            this.floor1.TabIndex = 0;
            this.floor1.TabStop = false;
            // 
            // character
            // 
            this.character.BackColor = System.Drawing.Color.Transparent;
            this.character.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.donald;
            this.character.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.character.Location = new System.Drawing.Point(119, 253);
            this.character.Name = "character";
            this.character.Size = new System.Drawing.Size(98, 157);
            this.character.TabIndex = 1;
            this.character.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.BonusLevel_tick);
            // 
            // BonusLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1176, 726);
            this.Controls.Add(this.character);
            this.Controls.Add(this.floor1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BonusLevel";
            this.Text = "BonusLevel";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLevel_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmLevel_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.floor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.character)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox floor1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.PictureBox character;
        private System.Windows.Forms.Timer timer1;
    }
}