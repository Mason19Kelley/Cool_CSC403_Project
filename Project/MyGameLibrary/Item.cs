using System.Drawing;

namespace Fall2020_CSC403_Project.code
{
    /// <summary>
    /// This is the class for an item
    /// </summary>
    public class Item
    {
        public Vector2 Position { get; private set; }
        public Collider Collider { get; private set; }
        public Image Img { get; set; }
        public string Name { get; set; }

        public string Attribute { get; set; }

        public int Durability { get; set; }

        /// <summary>
        /// The Creation of the item.
        /// </summary>
        /// <param name="initPos">this is the inital position of the item</param>
        /// <param name="collider">this is the collider for the enemy</param>
        public Item(Vector2 initPos, Collider collider, string name, string attri) {
            Position = initPos;
            Collider = collider;
            Name = name;
            Attribute = attri;
        }
   
    }

}

