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
    class Controls
    {
        #region Fields
        private Dictionary<string, Keys> listOfControls;
        #endregion

        #region Properties
        /// <summary>
        /// A property to get or set the key that will make the Player move left
        /// </summary>
        public Keys Left
        {
            get { return listOfControls["left"]; }
            set
            {
                if (CanChangeToKey(value, "left"))
                    listOfControls["left"] = value;
            }
        }

        /// <summary>
        /// A property to get or set the key that will make the Player move right
        /// </summary>
        public Keys Right
        {
            get { return listOfControls["right"]; }
            set
            {
                if (CanChangeToKey(value, "right"))
                    listOfControls["right"] = value;
            }
        }

        /// <summary>
        /// A property to get or set the key that will make the Player jump
        /// </summary>
        public Keys Jump
        {
            get { return listOfControls["jump"]; }
            set
            {
                if (CanChangeToKey(value, "jump"))
                    listOfControls["jump"] = value;
            }
        }

        /// <summary>
        /// A property to get or set the key that will make the Player attack
        /// </summary>
        public Keys Attack
        {
            get { return listOfControls["attack"]; }
            set
            {
                if (CanChangeToKey(value, "attack"))
                    listOfControls["attack"] = value;
            }
        }

        /// <summary>
        /// A property to get or set the key that will pause the game
        /// </summary>
        public Keys Pause
        {
            get { return listOfControls["pause"]; }
            set
            {
                if(CanChangeToKey(value, "pause"))
                    listOfControls["pause"] = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// A constructor that sets all of the controls for the game
        /// </summary>
        /// <param name="left">The key that makes the Player move left</param>
        /// <param name="right">The key that makes the Player move right</param>
        /// <param name="jump">The key that makes the Player jump</param>
        /// <param name="attack">The key that makes the Player attack</param>
        /// <param name="pause">The key that makes the game pause</param>
        public Controls(Keys left = Keys.Left, Keys right = Keys.Right, Keys jump = Keys.Up, Keys attack = Keys.Z, Keys pause = Keys.Escape)
        {
            listOfControls = new Dictionary<string, Keys>();
            listOfControls.Add("left", left);
            listOfControls.Add("right", right);
            listOfControls.Add("jump", jump);
            listOfControls.Add("attack", attack);
            listOfControls.Add("pause", pause);
        }
        #endregion

        #region Methods
        /// <summary>
        /// A method to see if there are any conflicting controls for a potential change
        /// </summary>
        /// <returns>True if the change is allowed; false otherwise</returns>
        public bool CanChangeToKey(Keys key, string controlName)
        {
            foreach(KeyValuePair<string, Keys> control in listOfControls)
            {
                if (controlName != control.Key && key != control.Value)
                    return false;
            }
            return true;
        }
        #endregion
    }
}
