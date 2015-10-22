using Jumping.Models.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumping.Models.Features
{
    public class SurvivalStrategy : IStrategyBehavior
    {
        private Player _player;
        private Enemy _enemy;

        private bool playerPassed;

        public void SetEnemy(Enemy enemy)
        {
            this._enemy = enemy;
        }

        public void SetPlayer(Player player)
        {
            this._player = player;
        }

        public void Strategy()
        {
            float distance = CalculateDistance();
            Random random = new Random();

            if ((int)distance > 0 && (int)distance < 400)
            {
                if (_player.Direction == Direction.Left && !playerPassed)
                {
                    _enemy.Move(Direction.Right, random.Next(4));
                   
                }
                else if (_player.Direction == Direction.Right && !playerPassed)
                {
                    _enemy.Move(Direction.Left, random.Next(2));
                }
                else
                {
                    _enemy.Move(Direction.Right, random.Next(2));
                }
            }

            if (random.Next(500) == 2)
            {
                ((MovableObject)_enemy).Jump(true);
                ((MovableObject)_enemy).SetInitialVelocity(4.5f);
            }
        }

        private float CalculateDistance()
        {
            float distance = 0;
            playerPassed = false;

            if (_player.Position.X > _enemy.Position.X)
            {
                distance = _player.Position.X - _enemy.Position.X;
                playerPassed = true;
            }
            else if (_enemy.Position.X > _player.Position.X)
                distance = _enemy.Position.X - _player.Position.X;

            return distance;
        }
    }
}
