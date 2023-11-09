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
            this.floor1 = new System.Windows.Forms.PictureBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            ((System.ComponentModel.ISupportInitialize)(this.floor1)).BeginInit();
            this.SuspendLayout();
            // 
            // floor1
            // 
            this.floor1.BackColor = System.Drawing.SystemColors.InfoText;
            this.floor1.Location = new System.Drawing.Point(-2, 832);
            this.floor1.Name = "floor1";
            this.floor1.Size = new System.Drawing.Size(1575, 63);
            this.floor1.TabIndex = 0;
            this.floor1.TabStop = false;
            // 
            // BonusLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1568, 894);
            this.Controls.Add(this.floor1);
            this.Name = "BonusLevel";
            this.Text = "BonusLevel";
            ((System.ComponentModel.ISupportInitialize)(this.floor1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox floor1;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}