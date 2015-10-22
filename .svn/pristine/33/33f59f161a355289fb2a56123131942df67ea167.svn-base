using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jumping.Models;
using Jumping.Models.Core;

namespace Jumping
{
    public class Coin : Item
    {
        public Vector2 Position;
        private Rectangle CoinRectangle;

        public Coin(Vector2 position)
        {
            this.Texture = TextureLoader.GetInstance().GetTexture("coin");
            CoinRectangle = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);

            Position = position;

            setColor();
        }

        public override void Initialize()
        {

        }

        public Vector2 _Position { get { return Position; } set { Position = value; } }
        public Texture2D Texture { get; set; }

        public override bool CheckCollision(ISprite sprite)
        {
            Player player = (Player)sprite;
            Rectangle rectangle = new Rectangle((int)player._Position.X, (int)player._Position.Y, player.Texture.Width, player.Texture.Height);

            return CoinRectangle.Intersects(rectangle);    
        }

        public void setColor()
        {
            Color[] data = new Color[Texture.Width * Texture.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Blue;
            Texture.SetData(data);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public override int Use()
        {
            throw new NotImplementedException();
        }
    }
}
