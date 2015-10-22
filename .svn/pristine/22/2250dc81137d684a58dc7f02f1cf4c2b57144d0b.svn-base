using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jumping.Models.Interfaces;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jumping.Models.Features
{
    public class MeditationBoost: IBoostBehavior
    {
        private Player player;
        private Boolean meditationBoostUsing;
        private Timer meditationTimer = new Timer();
        private Texture2D texture;

        public MeditationBoost(Texture2D texture)
        {
            meditationTimer.Elapsed += new ElapsedEventHandler(StopBoost);
            meditationTimer.Interval = 8000; 
            this.texture = texture;
        }
        public void UseBoost()
        {
            if (!meditationBoostUsing)
            {
                player.StartMeditating();
                meditationTimer.Start();
                meditationBoostUsing = true;
            }
        }
        private void StopBoost(object source, ElapsedEventArgs e)
        {
            player.StopMeditating();
            meditationBoostUsing = false;
            meditationTimer.Stop();
        }
        public void SetPlayer(Player player)
        {
            this.player = player;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
    
}
