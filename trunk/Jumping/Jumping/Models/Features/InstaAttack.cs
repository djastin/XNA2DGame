using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Timers;
using Jumping.Models.Interfaces;

namespace Jumping.Models.Features
{
    public class InstaAttack:IAttackBehavior
    {
        private Boolean _needToStopFlashing;
        private Timer _flashTimer = new Timer();
        private Texture2D _flashTexture;
        private Rectangle _screenSize;

        public Boolean IsInstaAttackDone
        {
            get { return _needToStopFlashing;}
        }

        public void Use(MovableObject movableObject)
        {
                movableObject.GetLevel().Player.Hit(300);
                _needToStopFlashing = false;
                _flashTimer.Elapsed += new ElapsedEventHandler(StopFlashing);
                _flashTimer.Interval = 1000;
                _flashTimer.Start();
                _screenSize = movableObject.ScreenBound;
        }
        public void Initialize()
        {
            _flashTexture = TextureLoader.GetInstance().GetTexture("flashTexture");
        }
        private void StopFlashing(object source, ElapsedEventArgs e)
        {
            _needToStopFlashing = true;
        }
        public void Use(GameTime gametime)
        {
        }

        public void Update(GameTime gameTime) { }
        public void SetPlayer(Player player) { }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_needToStopFlashing)
            {
                spriteBatch.Draw(_flashTexture, _screenSize, Color.WhiteSmoke);
            }
        }
    }
 }

