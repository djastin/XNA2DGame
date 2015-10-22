using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Jumping.Models;
using Jumping.Models.Interfaces;
using Jumping.Models.Graphics;
using Jumping.Models.Core;
using Jumping.Models.Features;
using Jumping.Models.Sprites;
using System.Timers;

namespace Jumping
{
    [XmlRoot(ElementName = "Player")]
    public class Player : MovableObject
    {
        private IBoostBehavior _boost;
        private Boolean _meditating;
        private Boolean _doesDamage;
        private IAttackBehavior _attack;
        private int _weaponIndex;
        protected List<Weapon> _weapons;
        protected Weapon _currentWeapon;
        private Timer _shootTimer;

        
        [XmlElement("CurrentWeapon")]
        public String SelectedWeapon { get; set; }
        [XmlElement("AttackBehavior")]
        public String AttackName { get; set; }
        [XmlElement("BoostBehavior")]
        public String BoostName { get; set; }
        [XmlIgnore]
        public List<Item> CollectedItems;


        public void Initialize()
        {
            CollectedItems = new List<Item>();
            _weapons = new List<Weapon>();

            _weapons.Add(FeatureLoader.GetInstance().GetWeapon(SelectedWeapon));
            _attack = FeatureLoader.GetInstance().GetAttack(AttackName);
            _boost = FeatureLoader.GetInstance().GetBoost(BoostName);
            _weaponIndex = 0;
            _doesDamage = false;
            Hp = 1000;
            _attack.SetPlayer(this);
            _boost.SetPlayer(this);

            SetCurrentWeapon(0);
            SetHp(Hp);
            SetShootTimer();
            
            this.effects = SpriteEffects.None;
            base.Initialize(true);
        }

        private void SetShootTimer()
        {
           
            _shootTimer = new Timer();
            _shootTimer.Elapsed += new ElapsedEventHandler(StopAnimateShoot);
            _shootTimer.Interval = 500;

        }

        

        public bool IsCompletedItems()
        {
            if (_level.GetTotalCollectableItems() == CollectedItems.Count)
                return true;
            else
                return false;
        }

        public IAttackBehavior GetAttack()
        {
            return _attack;
        }
      
        public void SetDoesDamage(Boolean lean)
        {
            _doesDamage = lean;
        }

        public Boolean IsMeditating()
        {
            return _meditating;
        }
        public void StartMeditating()
        {
            this._meditating = true;
        }
        public void StopMeditating()
        {
            this._meditating = false;
        }
        public IBoostBehavior GetBoost()
        {
            return _boost;
        }

        public void SetCurrentWeapon(int which)
        {
            _currentWeapon = _weapons.ElementAt(which);
            _currentWeapon.Position = Position;
        }

        public Weapon GetWeapon()
        {
            return _currentWeapon;
        }

        public void AddWeapon(Weapon weapon)
        {
            _weapons.Add(weapon);

            if (_currentWeapon == null)
                SetCurrentWeapon(0);
        }

        public void SwitchWeapon()
        {
            if (_weaponIndex < _weapons.Count())
            {
                SetCurrentWeapon(_weaponIndex);
                _weaponIndex++;
            }
            else
                _weaponIndex = 0;
        }

        public void animateShoot()
        {
            if (States.Count > 3)
            {
                _shootTimer.Start();
                this.SetIsAnimatedObject(false);
                this.GetAnimator().PlayAnimation(States[4]);
            }
            else
                return;
        }

        private void StopAnimateShoot(object source, ElapsedEventArgs e)
        {
            this.SetIsAnimatedObject(true);
            _shootTimer.Stop();
        }

        public void UpdateAmmoPosition(Ammunition ammo)
        {
            if (!ammo.isFired)
            {
                ammo.Direction = this.Direction;
                ammo.Move(ammo.Direction);
                ammo.isFired = true;
            }
            if
             (ammo.isFired)
            {
                ammo.Move(ammo.Direction);
            }
            ammo.UpdateCollisionBox();
        }

        public void UpdateWeaponPosition(GameTime gameTime)
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.Position = Position + new Vector2(15, 30);
                if (_currentWeapon is ShootableWeapon)
                {
                    ShootableWeapon shootableWeapon = (ShootableWeapon)_currentWeapon;

                    if (shootableWeapon.GetAmmo().Count() > 0)
                    {
                        foreach (Ammunition filteredAmmo in shootableWeapon.GetAmmo())
                            UpdateAmmoPosition(filteredAmmo);
                    }
                }
            }
        }

        public void Fire()
        {
            if (_currentWeapon is ShootableWeapon)
            {
                ShootableWeapon weapon = (ShootableWeapon)_currentWeapon;
                Ammunition ammo = loadAmmo();
                ammo.Initialize();

                if (weapon.IsCooledDown)
                {
                    animateShoot();
                    weapon.AddAmmo(ammo);    
                }
            }
            else
                _currentWeapon.Use();
        }

        public Ammunition loadAmmo()
        {
            Ammunition ammo = new Ammunition();
            ammo.Position = _currentWeapon.Position;

            ammo.TextureName = "gun1_ammo";
            ammo.Initialize();

            return ammo;
        }

        public void Input(KeyboardState keyState, GameTime gameTime)
        {

            if (keyState.IsKeyDown(Keys.Space) && (IsJumping == false || Position.Y == Ground))
            {
                Jump(true);
                SetInitialVelocity(2.5f);
            }
            if (keyState.IsKeyDown(Keys.Left) && !keyState.IsKeyDown(Keys.Right))
            {
                if (keyState.IsKeyDown(Keys.A))
                    Move(Direction.Left, 2f);
                else
                {
                    Move(Direction.Left, 1f);
                }
            }
            else if (!keyState.IsKeyDown(Keys.Left) && keyState.IsKeyDown(Keys.Right))
            {
                if (keyState.IsKeyDown(Keys.A))
                    Move(Direction.Right, 2f);
                else
                    Move(Direction.Right, 1f);

                this.effects = SpriteEffects.None;
            }

            StopMoving();


            _prevKB = keyState;
        }

        public bool IsAlive()
        {
            bool state = true;

            if (Hp <= 0)
                state = false;
           
            return state;
        }

        public void Hit(int damage)
        {
            if (IsAlive())
                Hp -= damage;

            //_healthBar.DecreaseHealth(Hp);
        }

        public int GetHp()
        {
            return Hp;
        }

        public void SetHp(int hp)
        {
            this.Hp = hp;
        }


        private void getsHit(MovableObject hitObj, MovableObject attackObj)
        {
            hitObj.Hit(attackObj.getDamage());
            if (hitObj.CollisionBox.Right > attackObj.CollisionBox.Left)
            {
                hitObj.Move(Direction.Right, 1f);
            }
            if (hitObj.CollisionBox.Left < attackObj.CollisionBox.Right)
            {
                hitObj.Move(Direction.Left, 1f);
            }
        }


          public void HandleEnemyCollision()
          {
              MovableObject obj = _level.GetMovableObjectOfCollision();
              if (obj is Enemy && obj != null)
              {
                  Enemy enemy = (Enemy)obj;
                  if (!_doesDamage)
                  {
                      getsHit(this, enemy);
                  }
                  else 
                  {
                      getsHit(enemy, this);
                  }

              }
          }

        public void HandleDoorCollision()
        {
            Door door = _level.GetDoorOfCollision();

            if (door != null)
                _level.LoadRoom(door);
        }

        private void HandleItemCollision()
        {
            Item item = _level.GetItemOfCollision();

            if (item != null)
            {

                item.Use();
                if (item is BadGrade || item is Diamond)
                {
                    CollectedItems.Add(item);
                }
                _level.Items.Remove(item);
                _level.Layers[1].Sprites.Remove(item);
            }

        }

        public override void Update(GameTime gameTime)
        {
            if (IsAlive())
            {
                _attack.Update(gameTime);
                HandleEnemyCollision();
                HandleItemCollision();
                HandleDoorCollision();
                UpdateWeaponPosition(gameTime);
            }

            base.Update(gameTime);
        }
    }
}
