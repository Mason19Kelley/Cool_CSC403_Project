using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Forms;
using MyGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class FrmLevel : Form
    {
        private Player player;

        public Enemy enemyPoisonPacket;
        public Enemy bossKoolaid;
        public Enemy enemyCheeto;
        private Character[] walls;

        private Item gun;
        private Item potion;
        private Inventory inventory;

        private DateTime timeBegin;
        private FrmBattle frmBattle;

        private FrmInventory frmInventory;

        private Character leftBarrier;
        private Character rightBarrier;
        private Character pipeCollider;

        private ConfirmQuit confirmQuit = null;

        private BonusLevel bonusLevel;

        private DeathScreen deathScreen = DeathScreen.Instance;

        public static FrmLevel Instance { get; private set; }


        public FrmLevel()
        {
            InitializeComponent();
            this.KeyPreview = true;
            Instance = this;
        }

        private void FrmLevel_Load(object sender, EventArgs e)
        {
            const int PADDING = 7;
            const int NUM_WALLS = 13;

            gun = new Item(CreatePosition(picGun), CreateCollider(picGun, PADDING), "Gun", "Heavy");
            gun.Img = picGun.BackgroundImage;
            potion = new Item(CreatePosition(picPotion), CreateCollider(picPotion, PADDING), "Potion", "Healing");
            potion.Img = picPotion.BackgroundImage;

            player = new Player(CreatePosition(mainCharacter), CreateCollider(mainCharacter, 0));
            bossKoolaid = new Enemy(CreatePosition(picBossKoolAid), CreateCollider(picBossKoolAid, PADDING));
            enemyPoisonPacket = new Enemy(CreatePosition(picEnemyPoisonPacket), CreateCollider(picEnemyPoisonPacket, PADDING));
            enemyCheeto = new Enemy(CreatePosition(picEnemyCheeto), CreateCollider(picEnemyCheeto, PADDING));

            bossKoolaid.Img = picBossKoolAid.BackgroundImage;
            enemyPoisonPacket.Img = picEnemyPoisonPacket.BackgroundImage;
            enemyCheeto.Img = picEnemyCheeto.BackgroundImage;

            bossKoolaid.Color = Color.Red;
            enemyPoisonPacket.Color = Color.Green;
            enemyCheeto.Color = Color.FromArgb(255, 245, 161);

            // set values for enemmies
            bossKoolaid.BossKoolAidBC();
            enemyPoisonPacket.PoisonBC();
            enemyCheeto.CheetoBC();
            this.createPipeBarrier();
            pipeCollider = new Character(CreatePosition(pipe), CreatePipeCollider(pipe, 100));

            inventory = new Inventory();

            walls = new Character[NUM_WALLS];
            for (int w = 0; w < NUM_WALLS; w++)
            {
                PictureBox pic = Controls.Find("picWall" + w.ToString(), true)[0] as PictureBox;
                walls[w] = new Character(CreatePosition(pic), CreateWallCollider(pic, 0, mainCharacter.Size.Height));
            }

            /*this.Transparent_images_Click();*/
            MusicPlayer.PlayLevelMusic();
            Game.player = player;
            timeBegin = DateTime.Now;
        }

        private Vector2 CreatePosition(PictureBox pic)
        {
            return new Vector2(pic.Location.X, pic.Location.Y);
        }

        private Collider CreateCollider(PictureBox pic, int padding)
        {
            Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
            return new Collider(rect);
        }

        private Collider CreatePipeCollider(PictureBox pic, int padding)
        {
            int x = pic.Left + pic.Width / 2 - (pic.Size.Width - padding) / 2;
            int y = pic.Top + pic.Height / 2 - (pic.Size.Height - padding) / 2;
            Rectangle rect = new Rectangle(x, y, pic.Size.Width - padding, pic.Size.Height - padding);
            return new Collider(rect);
        }


        // needed to create different size hitbox for walls
        private Collider CreateWallCollider(PictureBox pic, int padding, int characterHeight)
        {
            Rectangle rect;
            if (pic.Size.Width > pic.Size.Height)
            {
                rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, Convert.ToInt32(Math.Max(pic.Size.Height - characterHeight, pic.Size.Height * .1))));
            }
            else
            {
                rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
            }


            return new Collider(rect);
        }

        private void tmrUpdateInGameTime_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - timeBegin;
            string time = span.ToString(@"hh\:mm\:ss");
            lblInGameTime.Text = "Time: " + time.ToString();
        }


        private void tmrPlayerMove_Tick(object sender, EventArgs e)
        {
            if (deathScreen == null)
            {
                // move player
                player.Move();

                // check collision with walls
                if (HitAWall(player))
                {
                    player.MoveBack();
                }

                // check collision with enemies
                if (HitAChar(player, enemyPoisonPacket))
                {
                    Fight(enemyPoisonPacket);
                }
                else if (HitAChar(player, enemyCheeto))
                {
                    Fight(enemyCheeto);
                }
                if (HitAChar(player, bossKoolaid))
                {
                    Fight(bossKoolaid);
                }

                if (HitAChar(player, leftBarrier) || HitAChar(player, rightBarrier))
                {
                    // Handle collision, e.g., prevent the character from moving
                    player.MoveBack();
                }

                if (HitAChar(player, pipeCollider))
                {
                    openBonusLevel();
                }

                // update player's picture box
                mainCharacter.Location = new Point((int)player.Position.x, (int)player.Position.y);
            }
        }

        private bool HitAWall(Character c)
        {
            bool hitAWall = false;
            for (int w = 0; w < walls.Length; w++)
            {
                if (c.Collider.Intersects(walls[w].Collider))
                {
                    hitAWall = true;
                    break;
                }
            }
            return hitAWall;
        }

        private bool HitAChar(Character you, Character other)
        {
            if (other != null)
            {
                return you.Collider.Intersects(other.Collider);
            }
            else return false;
            
        }

        private bool HitAItem(Character you, Item item) {
            if(item != null)
            {
                return you.Collider.Intersects(item.Collider);
            } else
            {

                return false;
            }
        }

        private void Fight(Enemy enemy)
        {
            MusicPlayer.StopLevelMusic();
            player.ResetMoveSpeed();
            player.MoveBack();
            frmBattle = FrmBattle.GetInstance(enemy, inventory);
            frmBattle.Show();

            if (enemy == bossKoolaid)
            {
                frmBattle.SetupForBossBattle();
            }



        }

        private void PickUpItem(Character you)
        {
            if (HitAItem(you, gun))
            {
                inventory.AddItem(gun);
                picGun.Dispose();
                gun = null;
            } else if (HitAItem(you, potion))
            {
                inventory.AddItem(potion);
                picPotion.Dispose();
                picPotion = null;
            }
        }

        private void ShowInven() {
            frmInventory = new FrmInventory(inventory);
            frmInventory.Show();
        }

        private void FrmLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (panel1.Visible == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        player.KeysPressed["left"] = new Vector2(-Player.GO_INC, 0);
                        break;

                    case Keys.Right:
                        player.KeysPressed["right"] = new Vector2(Player.GO_INC, 0);
                        break;

                    case Keys.Up:
                        player.KeysPressed["up"] = new Vector2(0, -Player.GO_INC);
                        break;

                    case Keys.Down:
                        player.KeysPressed["down"] = new Vector2(0, Player.GO_INC);
                        break;

                    case Keys.Escape:
                        if (panel1.Visible == false)
                        {
                            this.ShowOverlay();
                        }
                        else
                        {
                            this.CloseOverlay();
                        }
                        break;

                    case Keys.E:
                        PickUpItem(player);
                    break;
                    
                    case Keys.I:
                        ShowInven();
                        break;

                    default:
                        player.ResetMoveSpeed();
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        if (panel1.Visible == false)
                        {
                            this.ShowOverlay();
                        }
                        else
                        {
                            this.CloseOverlay();
                            Console.WriteLine("here");
                        }
                        break;


                    default:
                        player.ResetMoveSpeed();
                        break;

                }
            }
        }

        private void ShowOverlay()
        {
            button1.Location = new Point(520, 137);
            button3.Location = new Point(520, 243);
            
            panel1.Visible = true;
            panel1.BringToFront();
            richTextBox1.Visible = false;
        }

        private void CloseOverlay()
        {
            panel1.Visible = false;
 
        }
        private void backToGame(object sender, EventArgs e)
        {
            CloseOverlay();
            this.Focus();

        }

        private void exit_Click(object sender, EventArgs e)
        {

            if (confirmQuit == null || confirmQuit.IsDisposed)
            {
                confirmQuit = new ConfirmQuit();
                confirmQuit.Show();

            }
            else
            {
                confirmQuit.BringToFront();
            }
        }

        private void fullScreen(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.Sizable)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        private void showControls(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                richTextBox1.SelectionFont = new Font("Arial", 24, FontStyle.Bold);
                richTextBox1.AppendText("Controls\n\n");
                richTextBox1.SelectionFont = new Font("Times New Roman", 14, FontStyle.Italic);
                richTextBox1.AppendText(" E\t\t\tPick up item\n\n I\t\t\tShow Inventory\n\n \u2191\t\t\tMove up\n\n \u2193\t\t\tMove down\n\n \u2190\t\t\tMove left\n\n \u2192\t\t\tMove right\n\n Esc\t\t\tOpen Menu");
            }
            if (button1.Location.X > 200)
            {
                button1.Left -= 400;
                button3.Left -= 400;
                richTextBox1.Visible = true;
            }
            else
            {
                button1.Left += 400;
                button3.Left += 400;
                richTextBox1.Visible = false;
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
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

                case Keys.Up:
                    player.KeysPressed.Remove("up");
                    break;

                case Keys.Down:
                    player.KeysPressed.Remove("down");
                    break;
            }

        }



        private void lblInGameTime_Click(object sender, EventArgs e)
        {

        }

        private void createPipeBarrier()
        {
            PictureBox pic = Controls.Find("barrier", true)[0] as PictureBox;
            pic.SendToBack();
            leftBarrier = new Character(CreatePosition(pic), CreateWallCollider(pic, 0, mainCharacter.Size.Height));

            PictureBox pic2 = Controls.Find("barrier2", true)[0] as PictureBox;
            pic2.SendToBack();
            rightBarrier = new Character(CreatePosition(pic2), CreateWallCollider(pic2, 0, mainCharacter.Size.Height));
        }

        private void openBonusLevel()
        {
            MusicPlayer.StopLevelMusic();
            player.ResetMoveSpeed();
            player.MoveBack();
            if (bonusLevel == null || bonusLevel.IsDisposed)
            {
                bonusLevel = new BonusLevel();
                bonusLevel.Show();

            }
            else
            {
                bonusLevel.BringToFront();
            }
            
            bonusLevel.StartPosition = FormStartPosition.Manual;
            bonusLevel.Location = this.Location;
            bonusLevel.Size = this.Size;
        }

        
    }
}
