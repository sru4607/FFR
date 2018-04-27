using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ActualGame
{
    class Button
    {
        private Texture2D texture;
        private string name;
        private Rectangle rectangle;

        public Button(Texture2D texture, string name, Rectangle rectangle)
        {
            this.texture = texture;
            this.name = name;
            this.rectangle = rectangle;
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }
        //returns if button contains point p
        public bool Contains(Point p)
        {
            return rectangle.Contains(p);
        }
    }
}
