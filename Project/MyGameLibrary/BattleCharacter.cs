using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 1591 // use this to disable comment warnings

namespace Fall2020_CSC403_Project.code {
  public class BattleCharacter : Character {
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    private float strength;
    private float heavystrength;
    public int special;

    public event Action<int> AttackEvent;

    public BattleCharacter(Vector2 initPos, Collider collider) : base(initPos, collider) {
      MaxHealth = 20;
      strength = 2;
      heavystrength = 4;
      Health = MaxHealth;
    }

    public void OnAttack(int amount) {
      AttackEvent((int)(amount * strength));
    }
    public void OnHeavyAttack(int amount){
      AttackEvent((int)(amount * heavystrength));
    }

    //special attacks for the enemies
    public void CheetoSpecial(int amount) {
      AttackEvent((int)(amount * 1));
      AttackEvent((int)(amount * 1));      
    }

    public void PoisonSpecial(int amount) {
      AttackEvent((int)(amount * 1.5));
      AttackEvent((int)(amount * 1.5));
      AttackEvent((int)(amount * 1.5));
    }

    public  void KoolAidSpecial(int amount) {
      AttackEvent((int)(amount * 0.5));
      AttackEvent((int)(amount * 0.5));
      AttackEvent((int)(amount * 0.5));
      AttackEvent((int)(amount * 0.5));
      AttackEvent((int)(amount * 0.5));
    }

    public void AlterHealth(int amount) {
      Health += amount;
    }

    public void CheetoBC() {
      MaxHealth = 15;
      strength = 2;
      heavystrength = 3;
      Health = MaxHealth;
      special = 1;
    }

    public void PoisonBC() {
      MaxHealth = 25;
      strength = 1;
      heavystrength = 2;
      Health = MaxHealth;
      special = 2;  
    }

    public void BossKoolAidBC() {
      MaxHealth = 40;
      strength = 3;
      heavystrength = 4;
      Health = MaxHealth;
      special = 3;
    }


    }
}
