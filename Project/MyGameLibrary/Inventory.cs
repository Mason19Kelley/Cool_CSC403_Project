using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

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

        // Check if the inventory contains an item with a specfic attribute
        public bool ContainsAttribute(Inventory inventory, string attribute)
        {
            bool contains = false;
            foreach (Item item in inventory.GetItems())
            {
                if(item.Attribute == attribute)
                {
                    contains = true;
                    break;
                }
            }

            return contains;
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

        public bool itemHasDurability(Inventory inventory, string attribute)
        {
            bool hasDurability = false;
            foreach (Item item in inventory.GetItems())
            {
                if(item.Attribute == attribute)
                {
                    if(item.Durability > 0) { 
                    hasDurability = true;
                    break;
                    }
                }
            }

            return hasDurability;
        }
    }
}