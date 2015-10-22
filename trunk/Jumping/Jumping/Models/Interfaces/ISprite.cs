using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jumping.Models
{
    public interface ISprite
    {
        Vector2 Position { get; set; }
        Rectangle CollisionBox { get; set; }
        Texture2D Texture { get; set; }
       
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        

    }
}
