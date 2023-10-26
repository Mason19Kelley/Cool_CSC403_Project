using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  public partial class FrmBattle : Form {
    public static FrmBattle instance = null;
    private Enemy enemy;
    private Player player;

    private FrmBattle() {
      InitializeComponent();
      player = Game.player;
      lblHit.Text = "                             HIT!";
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
    }

    public void SetupForBossBattle() {
      picBossBattle.Location = Point.Empty;
      picBossBattle.Size = ClientSize;
      picBossBattle.Visible = true;
      btnHeavyAttack.SendToBack();

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
      HitDisplay();
      await Task.Delay(1500);
      player.OnAttack(-4);
      if (enemy.Health > 0) {
        enemy.OnAttack(-2);
      }
      EnemyDmgDisplay();
      await Task.Delay(1750);
      UpdateHealthBars();
      await Task.Delay(750);
      DmgGivenDisplay();
      btnHeavyAttack.Enabled = true;
      if (player.Health <= 0 || enemy.Health <= 0) {
        instance = null;
        Close();
      }
    }
   private async void btnHeavyAttack_Click(object sender, EventArgs e){
      lblDamage.Text = "  Dealt 16 damage!";
      HitDisplay();
      await Task.Delay(1500);
      player.OnHeavyAttack(-4);
      if (enemy.Health > 0){
        enemy.OnAttack(-2);
      }
      EnemyDmgDisplay();
      await Task.Delay(1750);     
      UpdateHealthBars();
      await Task.Delay(750);
      DmgGivenDisplay();
      btnHeavyAttack.Enabled = false;
      if (player.Health <= 0 || enemy.Health <= 0){
        instance = null;
        Close();
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

    private void tmrFinalBattle_Tick(object sender, EventArgs e) {
      picBossBattle.Visible = false;
      tmrFinalBattle.Enabled = false;
    }

        private void FrmBattle_Load(object sender, EventArgs e)
        {

        }

    }
}
