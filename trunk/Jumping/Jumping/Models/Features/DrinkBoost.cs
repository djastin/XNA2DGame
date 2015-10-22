using Jumping.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jumping.Models.Features
{
    public class DrinkBoost : IBoostBehavior
    {

        private Player player;

        private Boolean isDrinking;
        private Timer animationTimer = new Timer();
        private Timer boostTimer = new Timer();



        public DrinkBoost()
        {
            isDrinking = true;
            animationTimer.Elapsed += new ElapsedEventHandler(StopAnimation);
            animationTimer.Interval = 1500;
            boostTimer.Elapsed += new ElapsedEventHandler(StopBoost);
            boostTimer.Interval = 10000;

        }

        public void UseBoost()
        {
            if (isDrinking)
            {

                player.SetIsAnimatedObject(false);
                player.GetAnimator().PlayAnimation(player.States[3]);
                animationTimer.Start();
                boostTimer.Start();
                player.setDamage(player.getDamage() * 2);
                isDrinking = false;
            }
        }


        private void StopAnimation(object source, ElapsedEventArgs e)
        {

            player.SetIsAnimatedObject(true);
            animationTimer.Stop();
        }

        private void StopBoost(object source, ElapsedEventArgs e)
        {
            isDrinking = true;
            player.setDamage(player.getDamage() / 2);
            boostTimer.Stop();

        }

        public void SetPlayer(Player player) { this.player = player; }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            player.animator.Draw(gameTime, spriteBatch, position, SpriteEffects.None);

        }
    }
}
