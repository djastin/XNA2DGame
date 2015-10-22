using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jumping.Models;

namespace Jumping
{
    [XmlRoot (ElementName = "Obstacle")]
    public class Obstacle : ISprite
    {
        [XmlElement("Position")]
        public Vector2 Position { get; set; }
        [XmlIgnore]
        public Texture2D Texture { get; set; }
        [XmlElement("TextureName")]
        public String TextureName { get; set; }
        [XmlIgnore]
        public Rectangle CollisionBox { get; set; }
        [XmlIgnore]
        public SpriteEffects effects { get; set; }

        public void Initialize()
        {
            this.Texture = TextureLoader.GetInstance().GetTexture(TextureName);
        }

        public void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, 
                Texture.Width, Texture.Height), Color.White);
        }
    }
}
