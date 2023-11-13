using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Fall2020_CSC403_Project.Forms
{
    public partial class BonusLevel : Form
    {

        private Player player;
        private Character floor;
        private Character leftBarrierObj;
        private Vector2 cameraPosition;
        private Vector2 prevCameraPosition;

        private Character pb1;
        private Character pb2;
        private Character pb3;
        private Character pb4;
        private Character pb5;
        private Character pb6;
        public BonusLevel()
        {
            InitializeComponent();
            cameraPosition = new Vector2(0, 0);
            prevCameraPosition = cameraPosition;
            Image originalImage = Properties.Resources.brick;

            // Resize the image to a new width and height
            Image resizedImage = ResizeImage(originalImage, new Size(50, 50)); // Replace with your desired width and height

            // Set the resized image to the PictureBox
            SetImage(floor1, resizedImage, 90);

            player = new Player(FrmLevel.CreatePosition(character), FrmLevel.CreateCollider(character, -20));
            /*player.KeysPressed.Add("gravity", new Vector2(0, 3));*/
            floor = new Character(FrmLevel.CreatePosition(floor1), FrmLevel.CreateCollider(floor1, -50));
            leftBarrierObj = new Character(FrmLevel.CreatePosition(leftBarrier), FrmLevel.CreateCollider(leftBarrier, 0));


            floor1.Location = new Point(floor1.Location.X - (int)cameraPosition.x, floor1.Location.Y - (int)cameraPosition.y);
            character.Location = new Point(character.Location.X - (int)cameraPosition.x, character.Location.Y - (int)cameraPosition.y);
            floor = new Character(FrmLevel.CreatePosition(floor1), FrmLevel.CreateCollider(floor1, -50));

            pb1 = new Character(FrmLevel.CreatePosition(pictureBox1), FrmLevel.CreateCollider(pictureBox1, 0));
            pb2 = new Character(FrmLevel.CreatePosition(pictureBox2), FrmLevel.CreateCollider(pictureBox2, 0));
            pb3 = new Character(FrmLevel.CreatePosition(pictureBox3), FrmLevel.CreateCollider(pictureBox3, 0));
            pb4 = new Character(FrmLevel.CreatePosition(pictureBox4), FrmLevel.CreateCollider(pictureBox4, 0));
            pb5 = new Character(FrmLevel.CreatePosition(pictureBox5), FrmLevel.CreateCollider(pictureBox5, 0));
            pb6 = new Character(FrmLevel.CreatePosition(pictureBox6), FrmLevel.CreateCollider(pictureBox6, 0));
        }

        private void BonusLevel_tick(object sender, EventArgs e)
        {
            if(FrmLevel.HitAChar(player, leftBarrierObj)){
                player.MoveBack();
            }
            if (player.MoveSpeed.y > 0 && (FrmLevel.HitAChar(player, pb1) || FrmLevel.HitAChar(player, pb2) || FrmLevel.HitAChar(player, pb3) || FrmLevel.HitAChar(player, pb4) || FrmLevel.HitAChar(player, pb5) || FrmLevel.HitAChar(player, pb6)))
            {
                // Prevent the character from moving through the picture box
                player.MoveBack();
            }
            if (player.MoveSpeed.y < 0 && (FrmLevel.HitAChar(player, pb1) || FrmLevel.HitAChar(player, pb2) || FrmLevel.HitAChar(player, pb3) || FrmLevel.HitAChar(player, pb4) || FrmLevel.HitAChar(player, pb5) || FrmLevel.HitAChar(player, pb6)))
            {
                // Prevent the character from moving through the picture box
                player.MoveBack();
            }

            bool isOnFloor = HitAny(player);
            player.Move(true);
            player.IsGrounded = isOnFloor;
            cameraPosition.x = (int)(player.Position.x - this.ClientSize.Width / 2.9);
            cameraPosition.y = (int)(player.Position.y - this.ClientSize.Height / 2.9);

            // Keep the character at the center of the screen horizontally
            character.Location = new Point(this.ClientSize.Width / 2 - character.Width, character.Location.Y + (int)(cameraPosition.y - prevCameraPosition.y));

            // Adjust the position of the floor based on the camera's position
            floor1.Location = new Point(floor1.Location.X - (int)(cameraPosition.x - prevCameraPosition.x), floor1.Location.Y);
            leftBarrier.Location = new Point(floor1.Location.X, leftBarrier.Location.Y);
            // Adjust the position of other game objects based on the camera's position
            // ...
            pictureBox1.Location = new Point(pictureBox1.Location.X - (int)(cameraPosition.x - prevCameraPosition.x), pictureBox1.Location.Y);
            UpdateColliderPosition(pb1, pictureBox1);
            pictureBox2.Location = new Point(pictureBox2.Location.X - (int)(cameraPosition.x - prevCameraPosition.x), pictureBox2.Location.Y);
            pictureBox3.Location = new Point(pictureBox3.Location.X - (int)(cameraPosition.x - prevCameraPosition.x), pictureBox3.Location.Y);
            pictureBox4.Location = new Point(pictureBox4.Location.X - (int)(cameraPosition.x - prevCameraPosition.x), pictureBox4.Location.Y);
            pictureBox5.Location = new Point(pictureBox5.Location.X - (int)(cameraPosition.x - prevCameraPosition.x), pictureBox5.Location.Y);
            pictureBox6.Location = new Point(pictureBox6.Location.X - (int)(cameraPosition.x - prevCameraPosition.x), pictureBox6.Location.Y);

            prevCameraPosition = cameraPosition;
        }

        private void FrmLevel_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    player.KeysPressed["left"] = new Vector2(-Player.GO_INC * 2, 0);
                    break;

                case Keys.Right:
                    player.KeysPressed["right"] = new Vector2(Player.GO_INC * 2, 0);
                    break;

                case Keys.Space:
                    if (player.IsGrounded)
                    {
                        player.KeysPressed["jump"] = new Vector2(0, -player.jumpSpeed);
                    }
                    break;

                default:
                    break;
            }
        }

        private void FrmLevel_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    player.KeysPressed.Remove("left");
                    break;

                case Keys.Right:
                    player.KeysPressed.Remove("right");
                    break;

                case Keys.Space:
                    player.KeysPressed.Remove("jump");
                    break;

                default:
                    break;
            }
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

        private bool HitAny(Character you)
        {
            if(FrmLevel.HitAChar(player, floor)){
                return true;
            }
            if(FrmLevel.HitAChar(you, pb1))
            {
                return true;
            }
            if (FrmLevel.HitAChar(you, pb2))
            {
                return true;
            }
            if (FrmLevel.HitAChar(you, pb3))
            {
                return true;
            }
            if (FrmLevel.HitAChar(you, pb4))
            {
                return true;
            }
            if (FrmLevel.HitAChar(you, pb5))
            {
                return true;
            }
            if (FrmLevel.HitAChar(you, pb6))
            {
                return true;
            }

            return false;
        }

        private void UpdateColliderPosition(Character character, PictureBox pictureBox)
        {
            character.Collider.MovePosition(pictureBox.Location.X, pictureBox.Location.Y);
        }

    }
}
