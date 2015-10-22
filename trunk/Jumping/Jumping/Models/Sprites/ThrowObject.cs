using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jumping.Models.Sprites
{
    public class ThrowObject : MovableObject
    {
        private Boolean _isEnemyObject;
        private MovableObject _thrower;
        public Texture2D _throwTexture;
        public ThrowObject()
        {
            Speed = 4f;
            _damage = 120;
        }
        public int GetDamage()
        {
            return _damage;
        }
        public void SetThrower(MovableObject thrower)
        {
            if (thrower is Enemy) { _isEnemyObject = true; }
            else { _isEnemyObject = false; }
            this._thrower = thrower;
        }

        public MovableObject GetThrower()
        {
            return _thrower;
        }
        public void Initialize()
        {
            this.Position = _thrower.Position;
            this.direction = _thrower.Direction;
            String throwTextureName = GetThrowObjectTextureName();
            _throwTexture = TextureLoader.GetInstance().GetTexture(GetThrowObjectTextureName());
            CollisionBox = new Rectangle((int) this.Position.X, (int) this.Position.Y, _throwTexture.Width,_throwTexture.Height);
            ScreenBoundWidth = _thrower.ScreenBoundWidth;
            frameHeight = TextureLoader.GetInstance().GetTexture(throwTextureName).Height;
            frameWidth = TextureLoader.GetInstance().GetTexture(throwTextureName).Width;
            base.Initialize(false);
        }

        public void UpdateCollisionBox()
        {
            CollisionBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, _throwTexture.Width, _throwTexture.Height);
        }
        public void UpdateCollisionBoxWithoutTextureSize()
        {
            CollisionBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);
        }

        private String GetThrowObjectTextureName()
        {
            String throwTextureName = "";

            switch (_thrower.GetLevel().Name)
            {
                case "Starbucksalandorium":
                    if (_thrower.GetType() == typeof(LittleEnemy))
                    {
                        throwTextureName = "ThrowAttackLittleHipster";

                    }
                    else if (_thrower.GetType() == typeof(EndBoss))
                    {
                        throwTextureName = "lp";
                    }
                    break;
                case "Cijfernarium":
                    if (_thrower.GetType() == typeof(LittleEnemy))
                    {
                        throwTextureName = "laptop";
                    }
                    if (_thrower.GetType() == typeof(EndBoss))
                    {
                        throwTextureName = "FGrade";
                    }
                    if (_thrower.GetType() == typeof(Player))
                    {
                        throwTextureName = "papercut";
                    }
                    break;
                case "Crackenarium":
                    if (_thrower.GetType() == typeof(LittleEnemy))
                    {
                        throwTextureName = "heroineNaald";

                    }
                    if (_thrower.GetType() == typeof(EndBoss))
                    {
                        throwTextureName = "heroineNaald";
                    }  
                    break;
                default:
                    throwTextureName = "tazer";
                    break;
            }
            return throwTextureName;
        }
        public override void Update(GameTime gameTime)
        {
            UpdateThrownObjectPosition();
            UpdateCollisionBox();
        }

        public void UpdateThrownObjectPosition()
        {
            if (direction == Direction.Right)
            {
                Position += new Vector2(Speed, 0);
            }
            else
                Position -= new Vector2(Speed, 0);
        }
        public bool CheckCollision(ISprite sprite)
        {
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, _throwTexture.Width, _throwTexture.Height);
            bool isCollision = false;

            if (sprite is Enemy && !_isEnemyObject)
            {
                Enemy player = (Enemy)sprite;
                Rectangle enemyBox = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Texture.Width, player.Texture.Height);
                isCollision = CollisionBox.Intersects(enemyBox);
            }
            if (sprite is Player && _isEnemyObject)
            {
                Player enemy = (Player)sprite;
                Rectangle enemyBox = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.Texture.Width, enemy.Texture.Height);
                isCollision = CollisionBox.Intersects(enemyBox);
            }
            return isCollision;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_throwTexture, Position + new Vector2(50, 30), Color.White);
        }
    }
}

