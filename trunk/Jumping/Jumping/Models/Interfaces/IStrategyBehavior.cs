using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumping.Models.Interfaces
{
    public interface IStrategyBehavior
    {
        void Strategy();
        void SetEnemy(Enemy enemy);
        void SetPlayer(Player player);
    }
}
