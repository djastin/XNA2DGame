using Jumping.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using Jumping.Models.Core;

namespace Jumping.Models.Features
{
    [XmlRoot(ElementName="YellAttack")]
    public class YellAttack : IAttackBehavior
    {
        private Level _level;
        private Vector2 _position;
        private Texture2D _texture;
        private Rectangle _collisionBox;
        private int _damage;
        
        private string _textureName { get; set; }
        public bool isBeingUsed { get; set; }

        public void Initialize()
        {
            _textureName = "yell";
            _texture = TextureLoader.GetInstance().GetTexture(_textureName);
            _position = Vector2.Zero;
            isBeingUsed = false;
        }

        public void Use(MovableObject objectType)
        {
            CollisionDetector collisionDetector = CollisionDetector.GetInstance();
            isBeingUsed = true;

            _position = new Vector2(objectType.Position.X + 30, objectType.Position.Y + 5);
            _collisionBox = new Rectangle((int)_position.X, (int) _position.Y, _texture.Width, _texture.Height);

            for (int movableObjects_inc = 0; movableObjects_inc < _level.GetMovableObjects().Count(); movableObjects_inc++)
            {
                if (_level.GetMovableObjects()[movableObjects_inc] is Enemy)
                {
                    Enemy enemy = (Enemy)_level.GetMovableObjects()[movableObjects_inc];
                    if(collisionDetector.CheckCollision(_collisionBox, enemy.CollisionBox))
                    {
                        _level.GetMovableObjects().Remove(enemy);
                        _level.Layers[1].Sprites.Remove(enemy);
                    }
                }
            }
        }
        public void Update(GameTime gameTime)
        {
        }

        public void SetPlayer(Player player)
        {
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public void Use(GameTime gametime)
        {
        }
    }
}
