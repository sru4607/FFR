using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuraMapEditorV2
{
    public enum EventType { Enemy, Warp }

    public partial class GameEvent : UserControl
    {
        // Fields
        private EventType eventType;
        private int xIndex;
        private int yIndex;
        private WarpData warpData;

        // Properties
        public Image Image
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }
        public WarpData WarpData
        {
            get { return warpData; }
            set
            {
                if (eventType == EventType.Warp)
                    warpData = value;
                else
                    warpData = null;
            }
        }

        public EventType EventType
        {
            get { return eventType; }
            set
            {
                eventType = value;
                if (eventType != EventType.Warp)
                    warpData = null;
            }
        }

        public int XIndex
        {
            get { return xIndex; }
            set { xIndex = value; }
        }

        public int YIndex
        {
            get { return yIndex; }
            set { yIndex = value; }
        }

        public GameEvent()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            base.OnMouseClick(e);
        }
    }
}
