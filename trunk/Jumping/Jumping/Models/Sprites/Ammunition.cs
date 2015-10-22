using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jumping.Models.Graphics;

namespace Jumping.Models
{
    public class Ammunition : ISprite
    {
        private float _speed;

        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle CollisionBox { get; set; }
        public String TextureName { get; set; }
        public Boolean isFired { get; set; }
        public Direction Direction { get; set; }

        public Ammunition()
        {
            _speed = 10f;
        }

        public void Initialize()
        {
            Texture = TextureLoader.GetInstance().GetTexture(TextureName);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);   
        }

        public bool CheckCollision(ISprite sprite)
        {
         
            bool isCollision = false;
            if (sprite is Enemy)
            {
                Enemy enemy = (Enemy)sprite;
                Rectangle enemyBox = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.Texture.Width, enemy.Texture.Height);
                isCollision = CollisionBox.Intersects(enemyBox);
            }

            return isCollision;
        }

        public void UpdateCollisionBox()
        {
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);   
        }

        public void Move(Direction direction)
        {
            if (direction == Direction.Right)
            {
                Position += new Vector2(_speed, 0);
                
            }
            else
                Position -= new Vector2(_speed, 0);
        
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
