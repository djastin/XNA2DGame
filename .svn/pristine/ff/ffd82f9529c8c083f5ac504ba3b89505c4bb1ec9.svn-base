using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace Jumping.Models.Core
{
    [XmlRoot(ElementName = "BadGrade")]
    public class BadGrade: Item
    {
        private int _numberOfBadGrades;

        public override void Initialize()
        {
            Texture = TextureLoader.GetInstance().GetTexture(TextureName);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public override int Use()
        {
            _numberOfBadGrades++;
            return _numberOfBadGrades;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
