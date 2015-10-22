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
    [XmlRoot(ElementName = "Medkit")]
    public class MedKit : Item
    {
        private int _hp = 2000;

        public override void Initialize()
        {
            Texture = TextureLoader.GetInstance().GetTexture(TextureName);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public override void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public override int Use()
        {
            Level level = GetLevel();
            Player p = level.Player;
            if (p.Hp <= 10000)
            {
                p.Hp = p.Hp + _hp;
            }
            return p.Hp;
        }
    }
}
