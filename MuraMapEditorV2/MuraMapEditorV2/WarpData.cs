using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuraMapEditorV2
{
    public class WarpData
    {
        // Fields
        // Stored so that the program can change the warp destination if neccesary
        private WarpCreator creator;

        // Properties
        public string MapName
        {
            get { return creator.MapName; }
        }

        public int XOffset
        {
            get { return creator.XOffset; }
        }

        public int YOffset
        {
            get { return creator.YOffset; }
        }

        public WarpCreator WarpCreator
        {
            get { return creator; }
        }

        // Constructor
        public WarpData(WarpCreator creator)
        {
            this.creator = creator;
        }
    }
}
