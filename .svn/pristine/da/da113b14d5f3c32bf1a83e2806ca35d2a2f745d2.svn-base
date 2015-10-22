using Jumping.Models.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Jumping.Models.Features
{
    [XmlRoot(ElementName = "Diamond")]
    public class Diamond : Item
    {
        public override void Initialize()
        {
            Texture = TextureLoader.GetInstance().GetTexture(TextureName);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public override int Use()
        {
            Level level = GetLevel();
            Player p = level.Player;

            return 0;
        }
    }
}
