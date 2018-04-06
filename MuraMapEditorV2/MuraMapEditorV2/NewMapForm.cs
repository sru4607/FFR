using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuraMapEditorV2
{
    public partial class NewMapForm : Form
    {
        // Fields
        MapEditor editor;

        public NewMapForm(MapEditor m)
        {
            InitializeComponent();
            editor = m;
        }

        private void Create_Click(object sender, EventArgs e)
        {
            int width;
            int height;

            // Make sure the two textboxes use valid ints, then create the map
            if (int.TryParse(NewWidth.Text, out width)
                && int.TryParse(NewHeight.Text, out height))
            {
                if (width >0 && height > 0)
                {
                    editor.Map.CreateMap(editor.Tileset, width, height);
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error: at least one of your dimensions was not positive.");
                }
            }
            else
            {
                MessageBox.Show("Error: at least one of your dimensions was not an integer.");
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
