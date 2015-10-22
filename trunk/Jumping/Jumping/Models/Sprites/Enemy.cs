using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Jumping.Models;
using Jumping.Models.Interfaces;
using Jumping.Models.Graphics;
using System.Xml.Serialization;
using Jumping.Models.Features;
using Jumping.Models.Sprites;
using Jumping.Enumerations;

namespace Jumping.Models
{
    [XmlInclude(typeof(LittleEnemy))]
    [XmlInclude(typeof(EndBoss))]
    [XmlRoot(ElementName = "Enemy")]
    public abstract class Enemy : MovableObject
    {
       
        private IAttackBehavior _attack;
        private Player _player;
        private IStrategyBehavior _behavior;
        private EnemyState _es;

        [XmlElement("AttackBehavior")]
        public String AttackName { get; set; }
        [XmlElement("StrategyBehavior")]
        public String StrategyName { get; set; }

        public void SetAttackBehavior(IAttackBehavior attack)
        {
            this._attack = attack;
        }
        
        public void SetBehavior()
        {
            _player = _level.Player;
            _behavior = FeatureLoader.GetInstance().GetStrategy(StrategyName);
            _behavior.SetEnemy(this);
            _behavior.SetPlayer(_player);
        }
        
        public int GetHp()
        {
            return Hp;
        }

        public void SetHp(int HP)
        {
            this.Hp = HP;
        }

        public bool IsAlive()
        {
            bool state = true;

            if (Hp <= 0)
            {
                state = false;
                Hp = 0;
            }
            else if (Hp <= 25)
            {
                SetState(EnemyState.DYING);
            }
            return state;
        }



        public IStrategyBehavior GetBehavior()
        {
            return _behavior;
        }

        public void PerformStrategy()
        {
            _behavior.Strategy();
        }

        // TESTING METHODS
        public void SetAttack(ThrowAttack throwAttack)
        {
            _attack = throwAttack;
        }
        // END TESTING METHODS
        public void SetAttack()
        {
            _attack = FeatureLoader.GetInstance().GetAttack(AttackName);
        }
        
        public IAttackBehavior GetAttack()
        {
            return _attack;
        }

        public override void Initialize(bool animatedObject)
        {
            Hp = 1000;
            if (this is LittleEnemy)
            {
                
                SetHp(Hp);
                setDamage(20);
                SetState(EnemyState.NORMAL);
            }
            if (this is EndBoss)
            {
                SetHp(Hp);
                setDamage(20);
                SetState(EnemyState.NORMAL);
            }
            base.Initialize(true);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            IsAlive();
            SetBehavior();
            PerformStrategy();
            Fall(gameTime);
        }

        public void SetState(EnemyState state)
        {
            _es = state;
        }

        public EnemyState GetState()
        {
            return _es;
        }
    }
}

