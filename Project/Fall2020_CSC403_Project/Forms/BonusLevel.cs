using Fall2020_CSC403_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project.Forms
{
    public partial class BonusLevel : Form
    {
        public BonusLevel()
        {
            InitializeComponent();
            Image originalImage = Properties.Resources.brick;

            // Resize the image to a new width and height
            Image resizedImage = ResizeImage(originalImage, new Size(50, 50)); // Replace with your desired width and height

            // Set the resized image to the PictureBox
            SetImage(floor1, resizedImage, 28);
        }

        public void SetImage(PictureBox pic, Image image, int repeatX)
        {
            Bitmap bm = new Bitmap(image.Width * repeatX, image.Height);
            Graphics gp = Graphics.FromImage(bm);
            for (int x = 0; x <= bm.Width - image.Width; x += image.Width)
            {
                gp.DrawImage(image, new Point(x, 0));
            }
            pic.Image = bm;
        }

        public Image ResizeImage(Image image, Size newSize)
        {
            if (image != null)
            {
                return new Bitmap(image, newSize);
            }
            return null;
        }

    }
}
