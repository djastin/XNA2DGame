    using Jumping.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jumping.Models.Core;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Jumping.Models.Sprites;

namespace Jumping.Models.Features
{
  
    [XmlRoot(ElementName = "ThrowAttack")]
    public class ThrowAttack : IAttackBehavior
    {
        private Level _level;

        public void Use(MovableObject objectType)
        {
            ThrowObject throwObject = createThrowObject(objectType);
            _level.ThrownObjects.Add(throwObject);
        }

        public void SetLevel(Level level)
        {
            this._level = level;
        }

        public void DeleteThrowObject(ThrowObject throwObject)
        {
            _level.ThrownObjects.Remove(throwObject);
        }

        public ThrowObject GetThrowObject(MovableObject thrower)
        {
            foreach (ThrowObject FoundThrownObject in _level.ThrownObjects)
            {
                if (FoundThrownObject.GetThrower() == thrower)
                {
                    return FoundThrownObject;
                }
            }
            return null;
        }

        private ThrowObject createThrowObject(MovableObject objectType)
        {
            ThrowObject throwObject = new ThrowObject();
            throwObject.SetThrower(objectType);
            throwObject.Initialize();

            return throwObject;
        }
        public void ThrownObjectHasCollidedWithEnemy(Enemy enemy, ThrowObject throwObject)
        {
            enemy.Hit(throwObject.getDamage());
            DeleteThrowObject(throwObject);
        }
        public void ThrownObjectHasCollidedWithPlayer(Player player, ThrowObject throwObject)
        {
            player.Hit(throwObject.getDamage());
            DeleteThrowObject(throwObject);
        }
        public void Use(GameTime gameTime) { }
        public void SetPlayer(Player player) { }
        public void Update(GameTime gameTime) { }
    }
}
