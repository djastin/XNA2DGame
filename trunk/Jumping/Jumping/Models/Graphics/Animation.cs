using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jumping.Models.Graphics
{
    public class Animation
    {
        public int currentFrame;
        public int frameHeight;
        public int frameWidth;
        Rectangle SpriteRectangle;

        public Texture2D texture;

        public Vector2 position;
        Vector2 origin;
 

        float timer;
        float interval = 75;

        public Animation(Texture2D newTexture, Vector2 newPosition, int newFrameHeight, int newFrameWidth)
        {
            texture = newTexture;
            position = newPosition;

            frameHeight = newFrameHeight;
            frameWidth = newFrameWidth;
        }

        public void Update(GameTime gameTime, Vector2 currentPosition)
        {
            SpriteRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            origin = new Vector2(SpriteRectangle.Width / 2, SpriteRectangle.Height / 2);
            position = currentPosition;
        }

        public void AnimateRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            if (timer > interval)
            {

                currentFrame++;
                timer = 0;
                if (currentFrame > 3)
                    currentFrame = 0;
            }
        }

        public void AnimateLeft(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            if (timer > interval)
            {
                currentFrame--;
                timer = 0;
                if (currentFrame < 5 || currentFrame > 8)
                    currentFrame = 8;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, SpriteRectangle, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
        }



    }
}

