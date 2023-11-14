using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fall2020_CSC403_Project.code;

namespace Fall2020_CSC403_Project
{
    public partial class FrmInventory : Form
    {
        public static FrmInventory instance = null;
        public static PictureBox[] pictureBoxArray;
        public static Label[] labelArrayBack;
        public static Label[] labelArrayFront;


        public void DisplayInventory(Inventory inventory)
        {

            //Set all durability bars to invisable
            foreach(Label label in labelArrayBack)
            {
                label.Visible= false;
            }
            foreach(Label label in labelArrayFront)
            {
                label.Visible= false;
            }

            List<Item> items = inventory.GetItems(); // Replace this with your actual method to get items            
            
            const int MAX_DURABILITY_WIDTH = 102;


            for (int i = 0; i < items.Count(); i++)
            {
                PictureBox slot = pictureBoxArray[11-i];
                Label backSlot = labelArrayBack[11-i];
                Label frontSlot = labelArrayFront[11-i];
                Item item = items[i];
                float itemDurability = item.Durability / (float)item.MaxDurability;

                backSlot.Visible = true;
                frontSlot.Visible = true;
                frontSlot.Width = (int)(MAX_DURABILITY_WIDTH * itemDurability);
                slot.BackgroundImage = item.Img; 
                slot.Tag = item.Name.ToString();
  
            }
        }

        public FrmInventory(Inventory inventory)
        {
            InitializeComponent();
            this.Visible= true;
            this.KeyDown += FrmInventory_KeyDown;
            this.KeyPreview = true;

            // Add all PictureBox controls on the form to the pictureBoxSlots list
            pictureBoxArray = Controls.OfType<PictureBox>()
                .Where(pictureBox => pictureBox.Name.StartsWith("invSlot"))
                .ToArray();

            // Add all label controls on the form to the labelSlots list
            labelArrayBack = Controls.OfType<Label>()
                .Where(label => label.Name.StartsWith("durb"))
                .ToArray();

            labelArrayFront = Controls.OfType<Label>()
                .Where(label => label.Name.StartsWith("durf"))
                .ToArray();

            DisplayInventory(inventory);
        }

        private void FrmInventory_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.I:
                    Hide();
                    break;
                case Keys.Escape:
                    Close();
                    break;
                default:
                    break;
            }
        }

        // Dont touch anything below this! Its the click event for inventory slots, there has
        // to be one for every slot.
        private void invSlot1_Click(object sender, EventArgs e)
        {
            if(invSlot1.Tag != null)
            {
                nameBox.Text = invSlot1.Tag.ToString();
            }
        }

        private void invSlot2_Click(object sender, EventArgs e)
        {
            if (invSlot2.Tag != null)
            {
                nameBox.Text = invSlot2.Tag.ToString();
            }
        }

        private void invSlot3_Click(object sender, EventArgs e)
        {
            if (invSlot3.Tag != null)
            {
                nameBox.Text = invSlot3.Tag.ToString();
            }
        }

        private void invSlot4_Click(object sender, EventArgs e)
        {
            if (invSlot4.Tag != null)
            {
                nameBox.Text = invSlot4.Tag.ToString();
            }
        }

        private void invSlot5_Click(object sender, EventArgs e)
        {
            if (invSlot5.Tag != null)
            {
                nameBox.Text = invSlot5.Tag.ToString();
            }
        }

        private void invSlot6_Click(object sender, EventArgs e)
        {
            if(invSlot6.Tag != null)
            {
                nameBox.Text = invSlot6.Tag.ToString();
            }
        }

        private void invSlot7_Click(object sender, EventArgs e)
        {
            if(invSlot7.Tag != null)
            {
                nameBox.Text = invSlot7.Tag.ToString();
            }
        }

        private void invSlot8_Click(object sender, EventArgs e)
        {
            if (invSlot8.Tag != null)
            {
                nameBox.Text = invSlot8.Tag.ToString();
            }
        }

        private void invSlot9_Click(object sender, EventArgs e)
        {
            if(invSlot9.Tag != null)
            {
                nameBox.Text = invSlot9.Tag.ToString();
            }
        }

        private void invSlot10_Click(object sender, EventArgs e)
        {
            if(invSlot10.Tag != null)
            {
                nameBox.Text = invSlot10.Tag.ToString();
            }
        }

        private void invSlot11_Click(object sender, EventArgs e)
        {
            if(invSlot11.Tag != null)
            {
                nameBox.Text = invSlot11.Tag.ToString();
            }
        }

        private void invSlot12_Click(object sender, EventArgs e)
        {
            if(invSlot12.Tag != null)
            {
                nameBox.Tag = invSlot12.Tag.ToString();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
