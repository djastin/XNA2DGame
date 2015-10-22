using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jumping.Models.Core
{
    public class ThrowObject : MovableObject
    {
        private Boolean isEnemyObject;
        private MovableObject thrower;
        
        public ThrowObject()
        {
            speed = 3f;
            damage = 120;
        }
        public int getDamage()
        {
            return damage;
        }
        public void setThrower(MovableObject thrower)
        {
            if (thrower is Enemy) { isEnemyObject = true; }
            else { isEnemyObject = false; }
            this.thrower = thrower;
        }
        public override void Initialize()
        {
            screenBoundWidth = 1500;
            frameHeight = 30;
            frameWidth = 30;
            base.Initialize();
        }
        public bool CheckCollision(ISprite sprite)
        {
            collisionBox = new Rectangle((int)_Position.X, (int)_Position.Y, Texture.Width, Texture.Height);
            bool isCollision = false;

            if (sprite is Enemy && !isEnemyObject)
            {
                Enemy player = (Enemy)sprite;
                Rectangle enemyBox = new Rectangle((int)player._Position.X, (int)player._Position.Y, player.Texture.Width, player.Texture.Height);
                isCollision = collisionBox.Intersects(enemyBox);
            }
            if (sprite is Player && isEnemyObject)
            {
                Player enemy = (Player)sprite;
                Rectangle enemyBox = new Rectangle((int)enemy._Position.X, (int)enemy._Position.Y, enemy.Texture.Width, enemy.Texture.Height);
                isCollision = collisionBox.Intersects(enemyBox);
            }
            return isCollision;
        }
    }
}
