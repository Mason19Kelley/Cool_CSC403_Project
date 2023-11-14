using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Forms;
using Fall2020_CSC403_Project.Properties;
using MyGameLibrary;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  public partial class FrmBattle : Form {
    public static FrmBattle instance = null;
    private Enemy enemy;
    private Player player;
    Random atkchoice = new Random();
    private DeathScreen deathScreen;

    private FrmBattle() {
      InitializeComponent();
      player = Game.player;
      lblHit.Text = "                             HIT!";
      this.FormClosing += FrmBattle_FormClosing;
        }

    public void Setup() {
      // update for this enemy
      picEnemy.BackgroundImage = enemy.Img;
      picEnemy.Refresh();
      BackColor = enemy.Color;
      picBossBattle.Visible = false;
      lblDamage.Visible = false;
      lblHit.Visible = false;   

      // Observer pattern
      enemy.AttackEvent += PlayerDamage;
      player.AttackEvent += EnemyDamage;

      // show health
      UpdateHealthBars();
      MusicPlayer.PlayBattleSound();
    }

    public void SetupForBossBattle() {
      picBossBattle.Location = Point.Empty;
      picBossBattle.Size = ClientSize;
      picBossBattle.Visible = true;
      btnHeavyAttack.SendToBack();
      btnHeal.SendToBack();

      SoundPlayer simpleSound = new SoundPlayer(Resources.final_battle);
      simpleSound.Play();

      tmrFinalBattle.Enabled = true;
    }

    public static FrmBattle GetInstance(Enemy enemy) {
      if (instance == null) {
        instance = new FrmBattle();
        instance.enemy = enemy;
        instance.Setup();
      }
      return instance;
    }
            


    private void UpdateHealthBars() {
      float playerHealthPer = player.Health / (float)player.MaxHealth;
      float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;

      const int MAX_HEALTHBAR_WIDTH = 226;
      lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
      lblEnemyHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * enemyHealthPer);

      lblPlayerHealthFull.Text = player.Health.ToString();
      lblEnemyHealthFull.Text = enemy.Health.ToString();
    }

    private async void btnAttack_Click(object sender, EventArgs e) {
      lblDamage.Text = "   Dealt 8 damage!";
      btnAttack.Enabled = false;
      HitDisplay();
      await Task.Delay(1500);
      player.OnAttack(-4);
      if (enemy.Health > 0) {
        int enemyatk = atkchoice.Next(1, 4);
        if(enemyatk == 1){
          enemy.OnAttack(-2);
        }
        else if (enemyatk == 2) {
          enemy.OnHeavyAttack(-2);
        }
        else if (enemyatk == 3) {
          if (enemy.special == 1) {
            enemy.CheetoSpecial(-2);
          }
          else if (enemy.special == 2) {
            enemy.PoisonSpecial(-2);
          }
          else if (enemy.special == 3) {
            enemy.KoolAidSpecial(-2);
          }
        }

      }
      EnemyDmgDisplay();
      await Task.Delay(1750);
      UpdateHealthBars();
      await Task.Delay(750);
      DmgGivenDisplay();
      btnAttack.Enabled = true;
      btnHeavyAttack.Enabled = true;
      if (player.Health <= 0) {
        MusicPlayer.StopBattleSound();
        openDeathScreen();

                if (player.Health <= 0 && enemy.Health > 0)
        {
          MusicPlayer.StopLevelMusic();
          MusicPlayer.PlayGameOverSound();
        } else {
          MusicPlayer.PlayLevelMusic();
                    
        }
         instance = null;
         Close();
      }

      if (enemy.Health <= 0)
      {
                MusicPlayer.StopBattleSound();
                MusicPlayer.PlayLevelMusic();
                RemoveEnemy(enemy);
        instance = null;
        Close();
      }
    }
   private async void btnHeavyAttack_Click(object sender, EventArgs e){
      if(FrmLevel.Instance.inventory.ContainsAttribute(FrmLevel.Instance.inventory, "Heavy") & 
                FrmLevel.Instance.inventory.itemHasDurability(FrmLevel.Instance.inventory, "Heavy")){

        
        lblDamage.Text = "  Dealt 16 damage!";
        btnHeavyAttack.Enabled = false;
        HitDisplay();
        await Task.Delay(1500);
        player.OnHeavyAttack(-4);
        if (enemy.Health > 0){
          int enemyatk = atkchoice.Next(1, 4);
          if (enemyatk == 1) {
            enemy.OnAttack(-2);
          }
          else if (enemyatk == 2) {
            enemy.OnHeavyAttack(-2);
          }
          else if (enemyatk == 3) {
            if(enemy.special == 1) {
              enemy.CheetoSpecial(-2);   
            }
            else if(enemy.special == 2) {
              enemy.PoisonSpecial(-2);
            }
            else if(enemy.special == 3) {
              enemy.KoolAidSpecial(-2);
            }
          }
        }
        EnemyDmgDisplay();
        await Task.Delay(1750);     
        UpdateHealthBars();
        await Task.Delay(750);
        DmgGivenDisplay();
        FrmLevel.Instance.gun.Durability -= 1;
      }
      if (player.Health <= 0)
      {
        MusicPlayer.StopBattleSound();
        openDeathScreen();

        if (player.Health <= 0 && enemy.Health > 0)
        {
            MusicPlayer.StopLevelMusic();
            MusicPlayer.PlayGameOverSound();
        }
        else
        {
            MusicPlayer.PlayLevelMusic();

        }
      instance = null;
      Close();
      }

      if (enemy.Health <= 0)
      {
                MusicPlayer.StopBattleSound();
                MusicPlayer.PlayLevelMusic();
                RemoveEnemy(enemy);
            instance = null;
            Close();
      }

   }
    private void btnHeal_Click(object sender, EventArgs e)
    {
       if(FrmLevel.Instance.inventory.ContainsAttribute(FrmLevel.Instance.inventory, "Healing") &
                FrmLevel.Instance.inventory.itemHasDurability(FrmLevel.Instance.inventory, "Healing"))
       {
          player.AlterHealth(4);
          UpdateHealthBars();
          btnHeal.Enabled = false;
          FrmLevel.Instance.potion.Durability -= 1;
       }
    }
    private void EnemyDamage(int amount) {
      enemy.AlterHealth(amount);
    }

    private void PlayerDamage(int amount) {
      player.AlterHealth(amount);
    }

    
    private async void HitDisplay() {
      for (int i = 0; i < 5; i++) {
          lblHit.Visible = true;
          await Task.Delay(100);
          lblHit.Visible = false;
          await Task.Delay(100);
      }
    }

    private async void EnemyDmgDisplay() {
      for (int i = 0; i < 7; i++) {
          picEnemy.Visible = false;
          await Task.Delay(100);
          picEnemy.Visible = true;
          await Task.Delay(100);
      }
    }

    private async void DmgGivenDisplay() {
      lblDamage.Visible = true;
      await Task.Delay(1500);
      lblDamage.Visible = false;
    }

        // This method will directly access the controls on FrmLevel to delete them since it is just a 
        // copy here.
    private void RemoveEnemy(Enemy enemy)
        {
            // Poison Packet
            if(enemy.Color == Color.Green)
            {
                if(TitleScreen.FrmLevelInstance.picEnemyPoisonPacket.Parent != null)
                {
                    TitleScreen.FrmLevelInstance.picEnemyPoisonPacket.Parent.Controls.Remove(
                        TitleScreen.FrmLevelInstance.picEnemyPoisonPacket);
                    TitleScreen.FrmLevelInstance.enemyPoisonPacket= null;
                }
            // Cheeto
            } else if (enemy.Color == Color.FromArgb(255, 245, 161)) { 
                if(TitleScreen.FrmLevelInstance.picEnemyCheeto.Parent!= null)
                {
                    TitleScreen.FrmLevelInstance.picEnemyCheeto.Parent.Controls.Remove(
                        TitleScreen.FrmLevelInstance.picEnemyCheeto);
                    TitleScreen.FrmLevelInstance.enemyCheeto= null;
                }
            // Boss koolaid
            } else if (enemy.Color == Color.Red)
            {
                if(TitleScreen.FrmLevelInstance.picBossKoolAid.Parent!= null)
                {
                    TitleScreen.FrmLevelInstance.picBossKoolAid.Parent.Controls.Remove(
                        TitleScreen.FrmLevelInstance.picBossKoolAid);
                    TitleScreen.FrmLevelInstance.bossKoolaid= null;
                }
            }
        }
    private void tmrFinalBattle_Tick(object sender, EventArgs e) {
      picBossBattle.Visible = false;
      tmrFinalBattle.Enabled = false;
    }

        private void FrmBattle_Load(object sender, EventArgs e)
        {

        }


        private void FrmBattle_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Unsubscribe from event subscriptions to avoid memory leaks
            enemy.AttackEvent -= PlayerDamage;
            player.AttackEvent -= EnemyDamage;

            // Dispose of any other resources or perform cleanup as needed
            // ...

            // Set the instance to null to allow a new instance to be created if needed
            instance = null;
        }

        private void openDeathScreen()
        {
            deathScreen = new DeathScreen();
            deathScreen.Show();
        }

    }
}
