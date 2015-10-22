using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jumping.Models.Interfaces;
using Jumping.Models.Core;
using Jumping.Enumerations;

namespace Jumping.Models.Features
{
    public class CarefullStrategy:IStrategyBehavior
    {
        private Enemy _enemy;
        private Player _player;
        private bool _hasPassed;

        public void SetEnemy(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void SetPlayer(Player player)
        {
            _player = player;
        }

        public void Strategy()
        {
            if (_enemy.GetState() == EnemyState.NORMAL)
            {
                _enemy.Move(Direction.Right, _enemy.Speed);
            }
            if (CalculateDistance() > 80)
            {
                _enemy.SetState(EnemyState.CHASING);
                if (_enemy.GetState() == EnemyState.CHASING)
                {
                    Follow();
                }
            }
            if (_enemy.GetState() == EnemyState.DYING)
            {
                Flee();
            }
            
           
           // HandleObstacles();
        }

        private float CalculateDistance()
        {
            _hasPassed = false;
            float distance = 0;
            distance =_enemy.Position.X - _player.Position.X;
            if (_player.Position.X > _enemy.Position.X)
            {
                _hasPassed = true;
            }
            return distance;
        }

        private void HandleObstacles()
        {
            CollisionDetector collisionDetector = CollisionDetector.GetInstance();
            foreach (Obstacle o in _enemy.GetLevel().Obstacles)
            {
                if(collisionDetector.CollisionLeft || collisionDetector.CollisionRight)
                {
                    _enemy.StopMoving();
                }
            }
        }

        private void Follow()
        {
            if (_enemy.GetHp() >25 && !_hasPassed)
            {
                if (_player.direction == Direction.Left)
                {
                    _enemy.Move(Direction.Right,_enemy.Speed);
                }
                else if (_player.direction == Direction.Right)
                {
                    _enemy.Move(Direction.Left, _enemy.Speed);
                }
                else
                {
                    _enemy.Move(Direction.Right, _enemy.Speed);
                }
            }
        }

        private void Flee()
        {
            if (_enemy.GetHp() <= 25 && CalculateDistance() < 80)
            {
                if (_player.direction == Direction.Left)
                {
                    _enemy.Move(Direction.Left, _enemy.Speed);
                }
                else if (_player.direction == Direction.Right)
                {
                    _enemy.Move(Direction.Right, _enemy.Speed);
                }
            }
        }

       
    }
}
