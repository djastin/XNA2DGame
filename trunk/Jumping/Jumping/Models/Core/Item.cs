using Jumping.Models.Features;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Jumping.Models.Core
{
    [XmlInclude(typeof(Weapon))]
    [XmlInclude(typeof(MedKit))]
    [XmlRoot(ElementName = "Item")]
    public abstract class Item : ISprite
    {
        private Level _level;
        [XmlElement("Position")]
        public Vector2 Position { get; set; }
        [XmlElement("TextureName")]
        public String TextureName { get; set; }
        [XmlIgnore]
        public Texture2D Texture { get; set; }
        [XmlIgnore]
        public Rectangle CollisionBox { get; set; }
      
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract int Use();
        public abstract void Initialize();

        public void SetLevel(Level level)
        {
            this._level = level;
        }

        public Level GetLevel()
        {
            return _level;
        }
    }
}
