using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code {
  public struct Vector2 {
    public float x;
    public float y;

    public Vector2(float x, float y) {
      this.x = x;
      this.y = y;
    }

    public static Vector2 Add(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.x + v2.x, v1.y + v2.y);
    }
  }
}
