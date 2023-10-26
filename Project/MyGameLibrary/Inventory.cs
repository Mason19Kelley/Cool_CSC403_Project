using System.Collections.Generic;

namespace Fall2020_CSC403_Project.code
{
    public class Inventory
    {
        private List<Item> items;
        public Inventory()
        {
            items = new List<Item>();
        }

        // Add an item to the inventory
        public void AddItem(Item item)
        {
            items.Add(item);
        }

        // Remove an item from the inventory
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        // Check if the inventory contains a specific item
        public bool ContainsItem(Item item)
        {
            return items.Contains(item);
        }

        // Get a list of all items in the inventory
        public List<Item> GetItems()
        {
            return items;
        }

        // Overload ToString to display the inventory
        public override string ToString()
        {
            string inventoryString = "Inventory:\n";
            foreach (Item item in items)
            {
                inventoryString += $"{item.Name}\n";
            }
            return inventoryString;
        }
    }
}