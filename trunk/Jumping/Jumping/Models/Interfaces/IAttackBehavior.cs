using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jumping.Models.Interfaces
{
    public interface IAttackBehavior
    {
        void Use(MovableObject objectType);
        void Update(GameTime gametime);
        void SetPlayer(Player player);
        void Use(GameTime gametime);
    }
}
