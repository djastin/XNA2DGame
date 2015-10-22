using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Jumping.Models;
using Jumping.Models.Graphics;
using Jumping.Models.Core;
using Jumping.Models.Sprites;
using Jumping.Enumerations;

namespace Jumping.Models
{
    [XmlInclude(typeof(Player))]
    [XmlInclude(typeof(Enemy))]
    [XmlInclude(typeof(EndBoss))]
    public abstract class MovableObject : ISprite
    {
        private Animator _animator;
        private float _gravity;
        private bool _isAnimatedObject;
        private SpriteEffects _effects;
        private Vector2 _velocity;
       
        private float _initialVelocity;
        private float _time;
        protected float _jumpPower;
        protected int _damage;
        protected KeyboardState _prevKB;
        protected Level _level;
        protected HealthBar _healthBar;
        [XmlIgnore]
        public int Hp { get; set; }
        [XmlElement("DefaultState")]
        public String DefaultState { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        [XmlElement("ScreenBoundWidth")]
        public int ScreenBoundWidth { get; set; }
        [XmlElement("ScreenBoundHeight")]
        public int ScreenBoundHeight { get; set; }
        [XmlIgnore]
        public Rectangle ScreenBound { get; set; }
        [XmlElement(typeof(Animation2))]
        [XmlElement("State")]
        public List<Animation2> States = new List<Animation2>();
        [XmlIgnore]
        public Animator animator;
        [XmlIgnore]
        public Rectangle CollisionBox { get; set; }
        [XmlIgnore]
        public SpriteEffects effects { get; set; }
        [XmlIgnore]
        public Direction Direction { get; set; }
        
        [XmlIgnore]
        public Texture2D Texture { get; set; }
        [XmlIgnore]
        public Animation2 SelectedAnimation { get; set; }
        [XmlIgnore]
        public bool IsJumping { get; set; }
        [XmlIgnore]
        public float Ground { get; set; }
        public Direction direction;
        [XmlElement ("FrameWidth")]
        public int frameWidth; 
        [XmlElement ("FrameHeight")]
        public int frameHeight;
        
        // TESTING VARIABLES
        public int width;
        public int height;

       
        public virtual void Initialize(bool animatedObject)
        {
            ScreenBound = new Rectangle(0, 0, ScreenBoundWidth, ScreenBoundHeight);
            _isAnimatedObject = animatedObject;
            Ground = Position.Y;
            _velocity = Vector2.Zero;
            IsJumping = false;
            _jumpPower = 4.5f;
            _gravity = -6.8f;
            _time = 0;

            if (animatedObject)
            {
                _animator = new Animator();
                _effects = SpriteEffects.None;

                foreach (Animation2 animation2 in States)
                    animation2.Initialize();

                GetAnimation(DefaultState);
                CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, SelectedAnimation.FrameWidth, SelectedAnimation.FrameHeight);
                Texture = SelectedAnimation.Texture;
            }

            _healthBar = new HealthBar();
        }

        public Animator GetAnimator()
        {
            return _animator;
        }

        public void GetAnimation(String Name)
        {
            foreach (Animation2 animation2 in States)
            {
                if (animation2.TextureName == Name)
                    SelectedAnimation = animation2;
            }
        }

         public int getDamage()
        {
            return _damage;
        }

         public void SetIsAnimatedObject(Boolean state)
         {
             _isAnimatedObject = state;
         }

         public Boolean GetIsAnimatedObject()
         {
             return _isAnimatedObject;
         }

        public void HandleCollidedObstacle(Obstacle obstacle)
        {
            CollisionDetector collisionDetector = CollisionDetector.GetInstance();

            if (obstacle != null)
            {
                if (collisionDetector.CollisionTop && Position.Y + SelectedAnimation.FrameHeight > obstacle.Position.Y && Position.Y
                    + SelectedAnimation.FrameHeight < obstacle.Position.Y + obstacle.Texture.Height / 2)
                {
                    Position = new Vector2(Position.X, obstacle.Position.Y - SelectedAnimation.FrameHeight);
                    Ground = Position.Y;
                    _velocity.Y = 0;
                    IsJumping = false;
                    _time = 0;
                }
                else if (collisionDetector.CollisionBottom && Position.Y < obstacle.Position.Y + obstacle.Texture.Height)
                {
                    Position = new Vector2(Position.X, obstacle.Position.Y + obstacle.Texture.Height);
                    _time = 0;
                    SetInitialVelocity(0);
                }
                else if (collisionDetector.CollisionLeft && Position.X + SelectedAnimation.FrameWidth > obstacle.Position.X)
                {
                    Position = new Vector2(obstacle.Position.X - SelectedAnimation.FrameWidth, Position.Y);
                    _velocity.X = 0;
                }
                else if (collisionDetector.CollisionRight && Position.X < obstacle.Position.X + obstacle.Texture.Width)
                {
                    Position = new Vector2(obstacle.Position.X + obstacle.Texture.Width, Position.Y);
                    _velocity.X = 0;
                }
                else if (IsJumping == false && Ground == obstacle.Position.Y - SelectedAnimation.FrameHeight)
                {
                    IsJumping = true;
                    SetInitialVelocity(0);
                }

                if (obstacle.Position.Y > 1700)
                {
                    _level.Layers[(int)LayerIndex.Player].Sprites.Remove(this);
                    _level.MovableObjects.Remove(this);
                }
            }
        }

        public void setDamage(int damage)
        {
            this._damage = damage;
        }

        public void setTime(float t)
        {
            this._time = t;
        }

        private void PerformJumping(GameTime gameTime)
        {
            if (IsJumping == true)
            {
                _velocity.Y = (float)(_initialVelocity + (_gravity * _time));
                _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Jump(bool state)
        {
            IsJumping = state;
        }

        public Ammunition hasCollissionWithAmmunition()
        {
            CollisionDetector collisionDetector = CollisionDetector.GetInstance();
            Player player = _level.Player;
            if (player.GetWeapon() is ShootableWeapon)
            {
                List<Ammunition> ammo = ((ShootableWeapon)player.GetWeapon()).GetAmmo();
                foreach (Ammunition ammunition in ammo)
                {
                    if (this is Enemy)
                    {
                        if (collisionDetector.CheckCollision(ammunition.CollisionBox, this.CollisionBox))
                        {
                            return ammunition;
                        }
                    }

                }
            }
            return null;
        }
        public ThrowObject hasCollissionWithThrowObject()
        {
            CollisionDetector collisionDetector = CollisionDetector.GetInstance();
            foreach (ThrowObject thrownObject in _level.ThrownObjects)
            {
                if (this is Player && !(thrownObject.GetThrower() is Player) || this is Enemy && !(thrownObject.GetThrower() is Enemy))
                {
                    if (collisionDetector.CheckCollision(this.CollisionBox, thrownObject.CollisionBox))
                    {
                        return thrownObject;
                    }
                }
            }
            return null;
        }
        

        protected void Fall(GameTime gameTime)
        {
            _velocity.Y = (float)(_initialVelocity + (_gravity * _time));
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public bool IsAlive()
        {
            bool state = true;

            if (Hp <= 0)
            {
                state = false;
                Hp = 0;
            }
            return state;
        }

        public void Hit(int damage)
        {
            if (IsAlive())
                Hp -= damage;

            _healthBar.DecreaseHealth(Hp);
        }


        public virtual void Update(GameTime gameTime)
        {
            if (_isAnimatedObject)
            {
                if (Math.Abs(_velocity.X) > 0.02f)
                    _animator.PlayAnimation(States[1]);
                else
                    _animator.PlayAnimation(States[0]);
            }
  
            UpdatePosition();
            PerformJumping(gameTime);
            CheckScreenBoundaries();
            UpdateHealthBarPosition();
            _healthBar.UpdateHealthBar(Hp);
        }

        private void UpdateHealthBarPosition()
        {
            _healthBar.Position = Position + new Vector2(55, -10);
        }

        public void UpdatePosition()
        {
            Position += new Vector2((_velocity.X * Speed), 0);
            Position -= new Vector2(0, (_velocity.Y * Speed));
        }

        public virtual void CheckScreenBoundaries()
        {
            if (Position.X < 0)
            {
                Position = new Vector2(0, Position.Y);
                _velocity.X = 0;
            }
            else if (Position.X + frameWidth > ScreenBound.Width)
            {
                Position = new Vector2(ScreenBound.Width - frameWidth, Position.Y);
                _velocity.X = 0;
            }
            if (Position.Y < 0)
                SetInitialVelocity(0);
        }

        public void Move(Direction direction, float speed)
        {
            if (direction == Direction.Left)
            {
                if (_velocity.X > -speed)
                    _velocity.X -= (1.0f / 10);
                else
                    _velocity.X = -1.0f;
            }
            else if (direction == Direction.Right)
            {
                if (_velocity.X < speed)
                    _velocity.X += (1.0f / 10);
                else
                    _velocity.X = 1.0f;
            }

            this.Direction = direction;
        }

        public void StopMoving()
        {
            if (_velocity.X > 0.05 || _velocity.X < -0.05)
                _velocity.X *= 0.90f;
            else
                _velocity.X = 0;
        }

        public void SetInitialVelocity(float initialVelocity)
        {
            this._initialVelocity = initialVelocity;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_velocity.X > 0)
            {
                _effects = SpriteEffects.None;
            }
            else if (_velocity.X < 0)
            {
                _effects = SpriteEffects.FlipHorizontally;
            }
            
            _animator.Draw(gameTime, spriteBatch, Position, _effects);
            _healthBar.Draw(spriteBatch);
        }

       public void SetLevel(Level level)
        {
            this._level = level;
        }

        public Level GetLevel()
        {
            return this._level;
        }

        public void SetGravity(float grav)
        {
            _gravity = grav;
        }

        public float GetGravity()
        {
            return _gravity;
        }
    }
}
