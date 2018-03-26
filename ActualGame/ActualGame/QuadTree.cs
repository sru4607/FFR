using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ActualGame
{
    class QuadTree
    { 
        #region Constants
        // The maximum number of objects in a quad
        // before a subdivision occurs
        private const int MAX_OBJECTS_BEFORE_SUBDIVIDE = 3;
        #endregion

        #region Variables
        // The game objects held at this level of the tree
        private List<GameObject> _objects;

        // This quad's rectangle area
        private Rectangle _rect;

        // This quad's divisions
        private QuadTree[] _divisions;
        #endregion

        #region Properties
        /// <summary>
        /// The divisions of this quad
        /// </summary>
        public QuadTree[] Divisions { get { return _divisions; } }

        /// <summary>
        /// This quad's rectangle
        /// </summary>
        public Rectangle Rectangle { get { return _rect; } }

        /// <summary>
        /// The game objects inside this quad
        /// </summary>
        public List<GameObject> GameObjects { get { return _objects; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new Quad Tree
        /// </summary>
        /// <param name="x">This quad's x position</param>
        /// <param name="y">This quad's y position</param>
        /// <param name="width">This quad's width</param>
        /// <param name="height">This quad's height</param>
        public QuadTree(int x, int y, int width, int height)
        {
            // Save the rectangle
            _rect = new Rectangle(x, y, width, height);

            // Create the object list
            _objects = new List<GameObject>();

            // No divisions yet
            _divisions = null;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a game object to the quad.  If the quad has too many
        /// objects in it, and hasn't been divided already, it should
        /// be divided
        /// </summary>
        /// <param name="gameObj">The object to add</param>
        public void AddObject(GameObject gameObj)
        {
            // ACTIVITY: Complete this method
            if (Rectangle.Contains(gameObj.Rect))
            {
                //if divisions exist
                if (Divisions != null)
                {
                    //attempt to add to divisions
                    bool successful = false;
                    foreach (QuadTree quad in Divisions)
                    {
                        if (quad.Rectangle.Contains(gameObj.Rect))
                        {
                            quad.AddObject(gameObj);
                            successful = true;
                        }

                    }
                    //if it didnt fit in divisions add to self
                    if (successful == false)
                    {
                        GameObjects.Add(gameObj);
                    }
                }
                else
                {
                    //add to self and if self has more than max objects divide
                    GameObjects.Add(gameObj);
                    if (GameObjects.Count > MAX_OBJECTS_BEFORE_SUBDIVIDE)
                    {
                        Divide();
                    }
                }

            }

        }

        /// <summary>
        /// Divides this quad into 4 smaller quads.  Moves any game objects
        /// that are completely contained within the new smaller quads into
        /// those quads and removes them from this one.
        /// </summary>
        public void Divide()
        {
            //Create 4 new divisions
            _divisions = new QuadTree[4];
            Divisions[0] = new QuadTree(Rectangle.X, Rectangle.Y, Rectangle.Width / 2, Rectangle.Height / 2);
            Divisions[1] = new QuadTree(Rectangle.X + Rectangle.Width / 2, Rectangle.Y, Rectangle.Width / 2, Rectangle.Height / 2);
            Divisions[2] = new QuadTree(Rectangle.X, Rectangle.Y + Rectangle.Height / 2, Rectangle.Width / 2, Rectangle.Height / 2);
            Divisions[3] = new QuadTree(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2, Rectangle.Width / 2, Rectangle.Height / 2);

            //Store all game objects in a seperate list
            List<GameObject> holder = new List<GameObject>();
            foreach (GameObject temp in GameObjects)
            {
                holder.Add(temp);
            }
            //remove all objects
            GameObjects.Clear();
            //readd all objects
            foreach (GameObject temp in holder)
            {
                AddObject(temp);
            }

        }

        /// <summary>
        /// Recursively populates a list with all of the rectangles in this
        /// quad and any subdivision quads.  Use the "AddRange" method of
        /// the list class to add the elements from one list to another.
        /// </summary>
        /// <returns>A list of rectangles</returns>
        public List<Rectangle> GetAllRectangles()
        {
            List<Rectangle> rects = new List<Rectangle>();
            if (Divisions != null)
            {
                //add all rectangles from divisions
                foreach (QuadTree div in Divisions)
                {
                    rects.AddRange(div.GetAllRectangles());
                }

            }
            else
            {
                //if no divisions return self
                List<Rectangle> temp = new List<Rectangle>();
                temp.Add(Rectangle);
                return temp;
            }




            return rects;
        }

        /// <summary>
        /// A possibly recursive method that returns the
        /// smallest quad that contains the specified rectangle
        /// </summary>
        /// <param name="rect">The rectangle to check</param>
        /// <returns>The smallest quad that contains the rectangle</returns>
        public QuadTree GetContainingQuad(Rectangle rect)
        {
            // ACTIVITY: Complete this method
            if (Rectangle.Contains(rect))
            {
                if (Divisions != null)
                {
                    bool successful = false;
                    foreach (QuadTree div in Divisions)
                    {
                        if (div.Rectangle.Contains(rect))
                        {
                            successful = true;
                            return div.GetContainingQuad(rect);
                        }

                    }
                    if (!successful)
                    {
                        return this;
                    }
                }
                else
                {
                    return this;
                }

            }
            // Return null if this quad doesn't completely contain
            // the rectangle that was passed in
            return null;
        }
        #endregion
    }
}


