using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumping.Models.Core
{
    public class WorldViewer
    {
        public Vector2 position;

        public WorldViewer()
        {
            position = new Vector2(10, 10);
        }

        public void Left()
        {
            position += new Vector2(-10, 0);
        }

        public void Right()
        {
            position += new Vector2(10, 0);
        }

        public void Up()
        {
            position += new Vector2(0, -10);
        }

        public void Down()
        {
            position += new Vector2(0, 10);
        }

    }
}
