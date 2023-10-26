using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code {
  public class Character {
    public const int GO_INC = 3;

    public Vector2 MoveSpeed { get; private set; }
    public Vector2 LastPosition { get; private set; }
    public Vector2 Position { get; private set; }
    public Collider Collider { get; private set; }
    public Dictionary<string, Vector2> KeysPressed = new Dictionary<string, Vector2>();

        public Character(Vector2 initPos, Collider collider) {
      Position = initPos;
      Collider = collider;
    }

    public void Move() {
      LastPosition = Position;
      SetMoveSpeed();
      Position = new Vector2(Position.x + MoveSpeed.x, Position.y + MoveSpeed.y);
      Collider.MovePosition((int)Position.x, (int)Position.y);
    }

    public void MoveBack() {
      Position = LastPosition;
    }

    public void SetMoveSpeed()
    {
        Vector2 sum = new Vector2();
        foreach (var vector in KeysPressed.Values)
        {
            sum = Vector2.Add(sum, vector);
        }
        MoveSpeed = sum;

    }

    public void ResetMoveSpeed() {
            this.KeysPressed.Clear();
      MoveSpeed = new Vector2(0, 0);
    }
  }
}
