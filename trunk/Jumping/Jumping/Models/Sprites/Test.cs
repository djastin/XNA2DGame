using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Jumping.Models.Sprites
{
    public class Test
    {

        [XmlIgnore]
        public int hoi;

        public String Name;

        [XmlElement ("Path")]
        public String path { get; set; }

        [XmlElement ("Position")]
        public Vector2 Position { get; set; }

        [XmlElement ("Obstacles")]
        public List<Obstacle> obstacles;


    }
}
