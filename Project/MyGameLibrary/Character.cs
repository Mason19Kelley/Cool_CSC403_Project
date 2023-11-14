using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code {
  public class Character {
    public const int GO_INC = 3;
    public float jumpSpeed = 20.0f;
    public float gravity = 10.0f;

        public Vector2 MoveSpeed { get; private set; }
    public Vector2 LastPosition { get; private set; }
    public Vector2 Position { get; set; }
    public Collider Collider { get; private set; }
    public Dictionary<string, Vector2> KeysPressed = new Dictionary<string, Vector2>();
        public bool IsGrounded { get; set; }

        public Character(Vector2 initPos, Collider collider) {
      Position = initPos;
      Collider = collider;
    }

        public void Move(bool bonus = false)
        {
            LastPosition = Position;
            SetMoveSpeed();
            Vector2 newPosition = new Vector2(Position.x + MoveSpeed.x, Position.y + MoveSpeed.y);
            
            if (newPosition.y < 0)
            {
                newPosition.y = 0;
            }
            // Apply gravity only if bonus is true
            if (bonus && !IsGrounded)
            {
                newPosition.y += gravity;
            }
            Position = newPosition;
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
