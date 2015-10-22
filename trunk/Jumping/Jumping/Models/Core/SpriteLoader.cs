using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Jumping.Models.Core
{
    public class SpriteLoader
    {
        [XmlElement ("Type")]
        public String type;
        [XmlElement ("Position")]
        public Vector2 position;
        [XmlElement ("TextureName")]
        public String textureName;

        [XmlElement ("Obstacles")]
        public List<Obstacle> obstacles;

        public SpriteLoader()
        {

        }
    }
}
