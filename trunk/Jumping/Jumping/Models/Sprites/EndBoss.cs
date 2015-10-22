using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Jumping.Models.Features;
using System.Timers;
using Microsoft.Xna.Framework;

namespace Jumping.Models.Sprites
{
    [XmlRoot(ElementName = "EndBoss")]
    public class EndBoss : Enemy
    {
        private Boolean _IsSpecialAttackEnabled;
        private Random _SpecialAttackRandomizer = new Random();
        private FeatureLoader _featureLoader;
        private Timer _attackTimer = new Timer();
        private Timer _attackCoolDownTimer = new Timer();
        private Boolean _isAttackCooledDown;

        public void Initialize()
        {
            SetHp(1000);
            _featureLoader = FeatureLoader.GetInstance();
            _attackTimer.Elapsed += new ElapsedEventHandler(UseAttack);
            _attackTimer.Interval = 2000;
            _attackTimer.Enabled = true;

            _attackCoolDownTimer.Elapsed += new ElapsedEventHandler(ResetCoolDown);
            _attackCoolDownTimer.Interval = 5000;
            _attackCoolDownTimer.Enabled = true;
            base.Initialize(true);
            _attackTimer.Start();
        }
        private void ResetCoolDown(object source, ElapsedEventArgs e)
        {
            _isAttackCooledDown = false;
        }
        public Boolean GetSpecialAttackEnabled()
        {
            return _IsSpecialAttackEnabled;
        }
        private void EnableSpecialAttack()
        {
            _IsSpecialAttackEnabled = true;
        }
        private void DisableSpecialAttack()
        {
            _IsSpecialAttackEnabled = false;
        }
        private void UseAttack(object source, ElapsedEventArgs e)
        {
            if (_IsSpecialAttackEnabled && !_isAttackCooledDown)
            {
                GetAttack().Use(this);
                _isAttackCooledDown = true;
                _attackCoolDownTimer.Start();
            }
            _attackTimer.Stop();
            _attackTimer.Start();
        }

        private Boolean HasExistingThrownObjects()
        {
            foreach (ThrowObject throwObject in _level.ThrownObjects)
            {
                if (throwObject.GetThrower() == this)
                {
                    return true;
                }
            }
            return false;
        }
        private Boolean IsInstaAttackDone()
        {
            if (AttackName == "InstaAttack")
            {
                return ((InstaAttack)GetAttack()).IsInstaAttackDone;
            }
            else
            {
                return true;
            }
        }
        public void ChangeSpecialAttack()
        {
            if (_IsSpecialAttackEnabled && !_isAttackCooledDown)
            {
                int randomNumber = _SpecialAttackRandomizer.Next(0, 3333);
                if (randomNumber <= 1111)
                {
                    AttackName = "ThrowAttack";
                }
                if (randomNumber > 1111 && randomNumber <= 2222)
                {
                    AttackName = "MinionsAttack";
                }
                else if (randomNumber > 2222)
                {
                    AttackName = "InstaAttack";
                }
                SetAttackBehavior(_featureLoader.GetAttack(AttackName));
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsEndBossNearPlayer())
            {
                EnableSpecialAttack();
                if (!HasExistingThrownObjects() && IsInstaAttackDone())
                {
                    ChangeSpecialAttack();
                }
            }
            else
            {
                DisableSpecialAttack();
            }
        }
        private Boolean IsEndBossNearPlayer()
        {
            float distance = CalculateDistanceBetweenPlayer();
            float heightdistance = CalculateHeightDistance();
            if ((int)distance > 0 && (int)distance < 900 &&
                    (int)heightdistance > 0 && (int)heightdistance < 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private float CalculateHeightDistance()
        {
            float distance = 0;

            if ((_level.Player.Position.Y > Position.Y))
            {
                distance = _level.Player.Position.Y - Position.Y;
            }
            else if (Position.Y > (_level.Player.Position.Y))
                distance = Position.Y - _level.Player.Position.Y;

            return distance;
        }
        private float CalculateDistanceBetweenPlayer()
        {
            float distance = 0;

            if (_level.Player.Position.X > Position.X)
            {
                distance = _level.Player.Position.X - Position.X;
            }
            else if (Position.X > _level.Player.Position.X)
                distance = Position.X - _level.Player.Position.X;

            return distance;
        }
    }
}
