using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jumping.Models.Core
{
    [XmlRoot(ElementName ="GravityDown")]
    public class GravityDown: Item
    {
        private float _gravityChange = -1.0f;
        public override void Initialize()
        {
            Texture = TextureLoader.GetInstance().GetTexture(TextureName);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public override int Use()
        {
            Player p = GetLevel().Player;
            p.SetGravity(_gravityChange);
            return 0;   
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
        }
    }
}
