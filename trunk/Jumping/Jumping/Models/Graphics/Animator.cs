using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jumping.Models.Graphics
{
    public class Animator
    {
        private Vector2 _position;
        private Animation2 _animation;
        private int _frameIndex;
        private float _time;

        public Vector2 Origin
        {
            get { return new Vector2(_animation.FrameWidth / 2.0f, _animation.FrameHeight / 2.0f); }
        }

        public void PlayAnimation(Animation2 animation)
        {
            if (_animation == animation)
                return;

            this._animation = animation;
            this._frameIndex = 0;
            this._time = 0.0f;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 pos, SpriteEffects spriteEffects)
        {
            this._position = pos;
            if (_animation == null)
                throw new NotSupportedException("No animation is currently playing.");

            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (_time > _animation.FrameTime)
            {
                _time -= _animation.FrameTime;

                if (_animation.IsLooping)
                {
                    _frameIndex = (_frameIndex + 1) % _animation.FrameCount;
                }
                else
                {
                    _frameIndex = Math.Min(_frameIndex + 1, _animation.FrameCount - 1);
                }
            }

            Rectangle source = new Rectangle(_frameIndex * _animation.Texture.Height, 0, _animation.Texture.Height, _animation.Texture.Height);

            spriteBatch.Draw(_animation.Texture, _position, source, Color.White, 0.0f, Vector2.Zero, 1.0f, spriteEffects, 0.0f);
        }
    }
}
