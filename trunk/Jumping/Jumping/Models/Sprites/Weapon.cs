using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jumping.Models.Core;

namespace Jumping.Models
{
    [XmlInclude(typeof(ShootableWeapon))]
    public class Weapon : Item
    {
        private Boolean _drawWeapon;
       


        public override void Initialize()
        {
            Texture = TextureLoader.GetInstance().GetTexture(TextureName);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            _drawWeapon = false;
        }
        public void SetDrawWeapon()
        {
            _drawWeapon = !_drawWeapon;
        }

        public override void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            if (!_drawWeapon)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
            if (this is ShootableWeapon)
            {
                ShootableWeapon weapon = (ShootableWeapon)this;

                for (int ammo_inc = 0; ammo_inc <= weapon.GetAmmo().Count() - 1; ammo_inc++)
                {
                    weapon.GetAmmo()[ammo_inc].Draw(gameTime,spriteBatch);
                }
            }
        }

       

        public override int Use()
        {
            return 0;
        }
    }
}

