using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Jumping.Views;

namespace Jumping.Models.Core
{
    public class Image
    {
        [XmlElement ("Alpha")]
        public float alpha;
        public String text, fontName;
        [XmlElement ("Path")]
        public String path;
        [XmlElement ("IsActive")]
        public bool isActive;

        public Texture2D texture;
        Vector2 origin;
        [XmlElement ("Position")]
        public Vector2 position;
        [XmlElement ("Scale")]
        public Vector2 scale;

        ContentManager content;
        public Rectangle sourceRect;
        RenderTarget2D renderTarget;
        SpriteFont font;
        Dictionary<String, ImageEffect> effectList;
        [XmlElement ("Effects")]
        public string effects;

        [XmlElement ("FadeEffect")]
        public FadeEffect fadeEffect;

        void SetEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).isActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }
            effectList.Add(effect.GetType().ToString().Replace("YoutubeRPG", ""), 
                (effect as ImageEffect));
        }

        public void ActivateEffect(String effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].isActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeActivateEffect(String effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].isActive = false;
                effectList[effect].UnloadContent();
            }
 
        }

        public Image()
        {
            path = text = effects = String.Empty;
            fontName = "Fonts/Orbitron";
            position = Vector2.Zero;
            scale = Vector2.One;
            alpha = 1.0f;
            sourceRect = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
        }

        public void LoadContent()
        {
            content = new ContentManager(
                ScreenManager.GetInstance().Content.ServiceProvider, "Content");

            if (path != String.Empty)
                texture = content.Load<Texture2D>(path);

            font = content.Load<SpriteFont>(fontName);

            Vector2 dimensions = Vector2.Zero;

            if(texture != null)
                dimensions.X += texture.Width;
            dimensions.X += font.MeasureString(text).X;

            if(texture != null)
                dimensions.Y = Math.Max(texture.Height, font.MeasureString(text).Y);
            else
                dimensions.Y = font.MeasureString(text).Y;

            if(sourceRect == Rectangle.Empty)
                sourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

             renderTarget = new RenderTarget2D(ScreenManager.GetInstance().graphicsDevice, 
                 (int)dimensions.X, (int)dimensions.Y);
            ScreenManager.GetInstance().graphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.GetInstance().graphicsDevice.Clear(Color.Transparent);
            ScreenManager.GetInstance().spriteBatch.Begin();
            ScreenManager.GetInstance().spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            ScreenManager.GetInstance().spriteBatch.DrawString(font, text, Vector2.Zero, Color.White);
            ScreenManager.GetInstance().spriteBatch.End();

            texture = renderTarget;
            ScreenManager.GetInstance().graphicsDevice.SetRenderTarget(null);

            SetEffect<FadeEffect>(ref fadeEffect);

            if (effects != String.Empty)
            {
                string[] split = effects.Split(':');
                foreach (string item in split)
                    ActivateEffect(item);
                
            }
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList)
            {
                DeActivateEffect(effect.Key);
            }
            
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                if(effect.Value.isActive)
                    effect.Value.Update(gameTime);
            }
                
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
            spriteBatch.Draw(texture, position + origin, sourceRect, Color.White * alpha,
                0.0f, origin, scale, SpriteEffects.None, 0.0f);

            /* 
             *  FadeEffect op String werkt nog niet doordat alpha niet geset kan worden met Color!
             *  Uitzoeken hoe het in XNA 4 wel kan!!
             */
        }
    }
}
