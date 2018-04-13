using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ActualGame
{
    class QuadTreeNode
    {
        #region Constants
        //The maximum amount of objects before the node divides
        private const int MAX_OBJECTS_BEFORE_DIVIDE = 3;
        #endregion

        #region Fields
        //The game objects in this level of the tree
        private List<GameObject> objects;

        //The rectangle of this node
        private Rectangle rectangle;

        //The divisions of this node
        private QuadTreeNode[] divisions;

        //The parent of this node
        private QuadTreeNode parent;
        #endregion

        #region Properties
        /// <summary>
        /// A property returning the divisions of the node
        /// </summary>
        public QuadTreeNode[] Divisions { get { return divisions; } }

        /// <summary>
        /// A property returning the rectangle of the node
        /// </summary>
        public Rectangle Rectangle { get { return rectangle; } }

        /// <summary>
        /// A property returning the GameObjects in the node
        /// </summary>
        public List<GameObject> Objects { get { return objects; } }

        /// <summary>
        /// A property to get or set the parent QuadTreeNode (null if none exists)
        /// </summary>
        public QuadTreeNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor that creates a new QuadTreeNode
        /// </summary>
        /// <param name="x">The node's x position</param>
        /// <param name="y">The node's y position</param>
        /// <param name="width">The width of the node</param>
        /// <param name="height">The height of the node</param>
        public QuadTreeNode(int x, int y, int width, int height)
        {
            rectangle = new Rectangle(x, y, width, height);
            objects = new List<GameObject>();
            divisions = null;
            parent = null;
        }
        #endregion

        #region Methods
        /// <summary>
        /// A method to add a collidable GameObject to the node and divide the node if necessary
        /// </summary>
        /// <param name="gameObject">The GameObject to add to the node</param>
        public void AddObject(GameObject gameObject)
        {
            //Only adds the object if it could collide
            if(!gameObject.NoClip)
            {
                //Divides if the new GameObject is over the limit
                if (objects.Count >= MAX_OBJECTS_BEFORE_DIVIDE)
                    Divide();

                //Checks which division (if any) the object fits in completely and adds the object to the appropriate list
                if (rectangle.Contains(gameObject.Rect))
                {
                    int index = -1;
                    if (divisions != null)
                    {
                        for (int c = 0; c < divisions.Length; c++)
                        {
                            if (divisions[c].Rectangle.Contains(gameObject.Rect))
                                index = c;
                        }
                    }
                    if (index == -1)
                        objects.Add(gameObject);
                    else
                        divisions[index].AddObject(gameObject);
                }
            }
        }

        /// <summary>
        /// A method that divides the node into four sub-nodes and moves the objects into the proper divisions
        /// </summary>
        public void Divide()
        {
            //Divides only if there are no divisions already
            if(divisions == null)
            {
                //Creates the new division nodes and sets the parent to this node
                divisions = new QuadTreeNode[4];
                int x = rectangle.X;
                int y = rectangle.Y;
                int width = rectangle.Width / 2;
                int height = rectangle.Height / 2;
                divisions[0] = new QuadTreeNode(x, y, width, height);
                divisions[1] = new QuadTreeNode(x + width, y, width, height);
                divisions[2] = new QuadTreeNode(x, y + height, width, height);
                divisions[3] = new QuadTreeNode(x + width, y + height, width, height);
                divisions[0].Parent = this;
                divisions[1].Parent = this;
                divisions[2].Parent = this;
                divisions[3].Parent = this;

                //Checks which objects fit into the new divisions, and moves them into the new division object lists
                List<GameObject> objectsToRemove = new List<GameObject>();
                for(int c=0; c<objects.Count; c++)
                {
                    for(int c2=0; c2<divisions.Length; c2++)
                    {
                        if(divisions[c2].Rectangle.Contains(objects[c].Rect))
                        {
                            divisions[c2].Objects.Add(objects[c]);
                            objectsToRemove.Add(objects[c]);
                        }
                    }
                    for(int d=0; d<objectsToRemove.Count; d++)
                    {
                        objects.Remove(objectsToRemove[d]);
                    }
                }
            }
        }

        /// <summary>
        /// A method that returns all the GameObjects that are in the QuadTreeNode
        /// </summary>
        /// <returns>A list of all the GameObjects in the node</returns>
        public List<GameObject> GetAllGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            //Recursively adds the GameObjects and any sub-node GameObjects
            gameObjects.AddRange(objects);
            if(divisions != null)
            {
                for(int c=0; c<divisions.Length; c++)
                {
                    gameObjects.AddRange(divisions[c].GetAllGameObjects());
                }
            }

            return gameObjects;
        }

        /// <summary>
        /// A method that returns the smallest node that a GameObject fits in
        /// </summary>
        /// <param name="rectangle">A GameObject to check with the QuadTreeNode</param>
        /// <returns>The smallest QuadTreeNode that the GameObject fits in</returns>
        public QuadTreeNode GetContainingQuad(GameObject gameObject)
        {
            //Recursively checks the divisions until it finds the smallest node the rectangle fits in
            if (rectangle.Contains(gameObject.Rect))
            {
                if(divisions != null)
                {
                    for(int c=0; c<divisions.Length; c++)
                    {
                        if(divisions[c].Rectangle.Contains(gameObject.Rect))
                        {
                            if (divisions[c].Divisions == null)
                                return divisions[c];
                            else
                                return divisions[c].GetContainingQuad(gameObject);
                        }
                    }
                    return this;
                }
            }

            //Returns null if this quad doesn't completely contain the parameter rectangle
            return null;
        }

        /// <summary>
        /// A method to return a list of all the parents of this QuadTreeNode
        /// </summary>
        /// <returns>A list of all the parent nodes</returns>
        public List<QuadTreeNode> GetParents()
        {
            List<QuadTreeNode> parents = new List<QuadTreeNode>();
            QuadTreeNode current = this;
            while(current.Parent != null)
            {
                parents.Add(current.Parent);
                current = current.Parent;
            }
            return parents;
        }
        #endregion
    }
}
