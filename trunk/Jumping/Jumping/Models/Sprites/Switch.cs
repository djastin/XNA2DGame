using Jumping.Models.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Jumping.Models.Sprites
{
    [XmlRoot(ElementName = "Switch")]
    public class Switch : ISprite
    {
        private Animator _animator;
        private SpriteEffects _effects;

        [XmlElement("Position")]
        public Vector2 Position { get; set; }
        [XmlIgnore]
        public Texture2D Texture { get; set; }
        [XmlElement("TextureName")]
        public String TextureName { get; set; }
        [XmlIgnore]
        public Rectangle CollisionBox { get; set; }
        [XmlIgnore]
        [XmlElement("State")]
        public Animation2 Animation { get; set; }

        public void Initialize()
        {
            if (TextureName == "")
            {
                _effects = SpriteEffects.None;
                _animator = new Animator();
                Animation.Initialize();
            }
            else
                this.Texture = TextureLoader.GetInstance().GetTexture(TextureName);

            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (TextureName != "")
            {
                spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y,
                    Texture.Width, Texture.Height), Color.White);
            }
            else
                _animator.Draw(gameTime, spriteBatch, Position, _effects);
        }

        public void Update()
        {
            if (TextureName == "")
                _animator.PlayAnimation(Animation);
        }
    }
}
