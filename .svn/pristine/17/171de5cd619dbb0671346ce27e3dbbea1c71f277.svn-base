using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jumping.Models.Core;

namespace Jumping.Models
{
    public class Layer
    {
        public Vector2 Parallax { get; set; }
        public List<ISprite> Sprites { get; private set; }

        public Layer()
        {
            Parallax = Vector2.One;
            Sprites = new List<ISprite>();
        }

        public void addSprite(ISprite sprite)
        {
            Sprites.Add(sprite);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int sprite_i = 0; sprite_i <= Sprites.Count() - 1; sprite_i++ )
            {
                Sprites[sprite_i].Draw(gameTime,spriteBatch);
            }
        }
    }
}
