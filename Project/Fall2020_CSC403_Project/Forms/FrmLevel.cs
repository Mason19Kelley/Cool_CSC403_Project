using Fall2020_CSC403_Project.code;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  public partial class FrmLevel : Form {
    private Player player;

    private Enemy enemyPoisonPacket;
    private Enemy bossKoolaid;
    private Enemy enemyCheeto;
    private Character[] walls;

    private DateTime timeBegin;
    private FrmBattle frmBattle;

    public FrmLevel() {
      InitializeComponent();
    }

    private void FrmLevel_Load(object sender, EventArgs e) {
      const int PADDING = 7;
      const int NUM_WALLS = 13;

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

      walls = new Character[NUM_WALLS];
      for (int w = 0; w < NUM_WALLS; w++) {
        PictureBox pic = Controls.Find("picWall" + w.ToString(), true)[0] as PictureBox;
        walls[w] = new Character(CreatePosition(pic), CreateWallCollider(pic, 0, mainCharacter.Size.Height));
      }

       /*this.Transparent_images_Click();*/
      Game.player = player;
      timeBegin = DateTime.Now;
    }

    private Vector2 CreatePosition(PictureBox pic) {
      return new Vector2(pic.Location.X, pic.Location.Y);
    }

    private Collider CreateCollider(PictureBox pic, int padding) {
      Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
      return new Collider(rect);
    }
        // needed to create different size hitbox for walls
        private Collider CreateWallCollider(PictureBox pic, int padding, int characterHeight)
    {
        Rectangle rect;
        if (pic.Size.Width > pic.Size.Height)
        {
            rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, Convert.ToInt32(Math.Max(pic.Size.Height-characterHeight, pic.Size.Height*.1))));
        } else
        {
            rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
        }

        
        return new Collider(rect);
    }


    private void tmrUpdateInGameTime_Tick(object sender, EventArgs e) {
      TimeSpan span = DateTime.Now - timeBegin;
      string time = span.ToString(@"hh\:mm\:ss");
      lblInGameTime.Text = "Time: " + time.ToString();
    }

    private void tmrPlayerMove_Tick(object sender, EventArgs e) {
      // move player
      player.Move();

      // check collision with walls
      if (HitAWall(player)) {
        player.MoveBack();
      }

      // check collision with enemies
      if (HitAChar(player, enemyPoisonPacket)) {
        Fight(enemyPoisonPacket);
      }
      else if (HitAChar(player, enemyCheeto)) {
        Fight(enemyCheeto);
      }
      if (HitAChar(player, bossKoolaid)) {
        Fight(bossKoolaid);
      }

      // update player's picture box
      mainCharacter.Location = new Point((int)player.Position.x, (int)player.Position.y);
    }

        private bool HitAWall(Character c) {
          bool hitAWall = false;
          for (int w = 0; w < walls.Length; w++) {
            if (c.Collider.Intersects(walls[w].Collider)) {
              hitAWall = true;
              break;
            }
          }
          return hitAWall;
        }

        private bool HitAChar(Character you, Character other) {
          return you.Collider.Intersects(other.Collider);
        }

    private void Fight(Enemy enemy) {
      player.ResetMoveSpeed();
      player.MoveBack();
      frmBattle = FrmBattle.GetInstance(enemy);
      frmBattle.Show();

      if (enemy == bossKoolaid) {
        frmBattle.SetupForBossBattle();
      }
    }

    private void FrmLevel_KeyDown(object sender, KeyEventArgs e) {
      switch (e.KeyCode) {
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

                case Keys.Up:
                    player.KeysPressed.Remove("up");
                    break;

                case Keys.Down:
                    player.KeysPressed.Remove("down");
                    break;
            }

        }



        private void lblInGameTime_Click(object sender, EventArgs e) {

      }
  }
}
