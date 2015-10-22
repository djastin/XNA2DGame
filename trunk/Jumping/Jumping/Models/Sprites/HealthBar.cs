using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumping.Models.Sprites
{
    public class HealthBar
    {
        private float _scale;
        private SpriteEffects _effects;
        private Vector2 _nonUniformScale;
        private int _hp;

        public Texture2D GreenBar { get; set; }
        public Texture2D RedBar { get; set; }
        public Vector2 Position { get; set; }

        public HealthBar()
        {
            GreenBar = TextureLoader.GetInstance().GetTexture("greenBar");
            RedBar = TextureLoader.GetInstance().GetTexture("redBar");

            _effects = SpriteEffects.None;
            _scale = -0.5f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle redBarRectangle = new Rectangle((int)Position.X, (int)Position.Y, RedBar.Width, RedBar.Height);
            Rectangle greenBarRectangle = new Rectangle((int)Position.X, (int)Position.Y, GreenBar.Width, GreenBar.Height);

            spriteBatch.Draw(GreenBar, Position, null, Color.White, 0f, Vector2.Zero, _scale, _effects, 0f);
            spriteBatch.Draw(RedBar, Position, null, Color.Red, 0f, Vector2.Zero, _nonUniformScale, _effects, 0f);
        }

        public void DecreaseHealth(int hp)
        {
            this._hp = hp;
            _nonUniformScale.Y = _scale;
            float decreaseValue = 100f / _hp;

            if (decreaseValue <= 1f)
                _nonUniformScale.X = _scale * decreaseValue;
        }

        public void UpdateHealthBar(int movableObjectHP)
        {
            this._hp = movableObjectHP;
        }
    }
}
