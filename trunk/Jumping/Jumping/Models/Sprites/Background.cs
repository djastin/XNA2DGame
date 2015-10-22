using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jumping.Models
{
    [XmlRoot(ElementName = "Background")]
    public class Background : ISprite
    {
        [XmlIgnore]
        public Rectangle CollisionBox { get; set; }
        [XmlElement("Position")]
        public Vector2 Position { get; set; }
        [XmlElement("TextureName")]
        public String textureName;
        public Texture2D Texture { get; set; }
       
        public void Initialize()
        {
            this.Texture = TextureLoader.GetInstance().GetTexture(textureName);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
        
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
