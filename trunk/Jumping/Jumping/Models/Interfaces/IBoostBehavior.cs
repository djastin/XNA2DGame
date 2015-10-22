using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumping.Models.Interfaces
{
    public interface IBoostBehavior
    {
        void UseBoost();
        void SetPlayer(Player player);
    }
}
