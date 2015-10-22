using Jumping.Models.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumping.Models.Features
{
    public class CuriousStrategy : IStrategyBehavior
    {
        private Player _player;
        private Enemy _enemy;

        private bool _playerPassed;

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
            float heightdistance = CalculateHeightDistance();
            Random random = new Random();

            if ((int)distance > 0 && (int)distance < 400 &&
                   (int)heightdistance > 0 && (int)heightdistance < 100)
            {
                if (_player.direction == Direction.Left && !_playerPassed)
                {
                    _enemy.Move(Direction.Right, random.Next(40));

                }
                else if (_player.direction == Direction.Right && !_playerPassed)
                {
                    _enemy.Move(Direction.Left, random.Next(40));

                }
                else
                {
                    _enemy.Move(Direction.Right, random.Next(4));
                }
            }
        }

        private float CalculateHeightDistance()
        {
            float distance = 0;

            if (_player.Position.Y > _enemy.Position.Y)
            {
                distance = _player.Position.Y - _enemy.Position.Y;
            }
            else if (_enemy.Position.Y > _player.Position.Y)
                distance = _enemy.Position.Y - _player.Position.Y;

            return distance;
        }

        private float CalculateDistance()
        {
            float distance = 0;
            _playerPassed = false;

            if (_player.Position.X > _enemy.Position.X)
            {
                distance = _player.Position.X - _enemy.Position.X;
                _playerPassed = true;
            }
            else if (_enemy.Position.X > _player.Position.X)
                distance = _enemy.Position.X - _player.Position.X;

            return distance;
        }
    }
}
