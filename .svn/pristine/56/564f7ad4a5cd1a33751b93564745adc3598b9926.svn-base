using Jumping.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumping.Models.Features
{
    public class CuriousBehavior : IStrategyBehavior
    {
        Player player;
        List<Enemy> enemies;

        public CuriousBehavior(Player player, List<Enemy> enemies)
        {
            this.player = player;
            this.enemies = enemies;
        }

        public void Strategy()
        {
            for (int enemy_inc = 0; enemy_inc <= enemies.Count() - 1; enemy_inc++)
            {
                Enemy enemy = enemies[enemy_inc];

                if (player.direction == Direction.Left)
                {
                    enemy.Walk(Direction.Left, 1f);
                }
                else
                    enemy.Walk(Direction.Right, 1f);
            }
        }
    }
}
