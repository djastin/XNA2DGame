using Jumping.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Jumping.Models.Sprites;
using Jumping.Models.Interfaces;
using Jumping.Models.Features;

namespace Jumping.Controllers
{
    public class GameController
    {
        private Level level;
        private Player player;
        

        public GameController(Level level)
        {
            this.level = level;
        }

        public void Input(KeyboardState keyState, GameTime gameTime, KeyboardState _prevKB)
        {

            if (keyState.IsKeyDown(Keys.Space) && (player.IsJumping == false || player.Position.Y == player.Ground))
            {
                player.Jump(true);
                player.SetInitialVelocity(2.5f);
            }
            if (keyState.IsKeyDown(Keys.Left) && !keyState.IsKeyDown(Keys.Right))
            {
                if (keyState.IsKeyDown(Keys.A))
                    player.Move(Direction.Left, 2f);
                else
                {
                    player.Move(Direction.Left, 1f);
                }
            }
            else if (!keyState.IsKeyDown(Keys.Left) && keyState.IsKeyDown(Keys.Right))
            {
                if (keyState.IsKeyDown(Keys.A))
                    player.Move(Direction.Right, 2f);
                else
                    player.Move(Direction.Right, 1f);

                
            }

            player.StopMoving();

            _prevKB = keyState;
        }

        public void HandleInput(GraphicsDeviceManager graphics, KeyboardState prevKB, KeyboardState keyState, 
            GameTime gameTime)
        {
            player = level.Player;
            player.Input(keyState, gameTime);
       

            if (prevKB.IsKeyUp(Keys.F) && keyState.IsKeyDown(Keys.F))
            {
                graphics.ToggleFullScreen();
                graphics.ApplyChanges();
            }

            if (prevKB.IsKeyUp(Keys.L) && keyState.IsKeyDown(Keys.L))
            {
                //currentLevel = (currentLevel + 1) % Levels.Count;
                // LoadLevel(currentLevel);
            }

            if (prevKB.IsKeyDown(Keys.C) && keyState.IsKeyDown(Keys.C))
            {
                player.Fire();
            }
            if (prevKB.IsKeyDown(Keys.E) && keyState.IsKeyDown(Keys.E))
            {
                player.SwitchWeapon();
            }
            if(prevKB.IsKeyDown(Keys.Y) && keyState.IsKeyDown(Keys.Y))
            {
                player.GetWeapon().SetDrawWeapon();
              
            }
            if(prevKB.IsKeyDown(Keys.B) && keyState.IsKeyDown(Keys.B))
            {
                player.GetBoost().UseBoost();
            }
            if(prevKB.IsKeyDown(Keys.L) && keyState.IsKeyDown(Keys.L))
            {
                IAttackBehavior Attack = player.GetAttack();
                if (Attack is ThrowAttack)
                {
                    Attack.Use(player);
                }
                if (Attack is HeadRollAttack)
                {
                    Attack.Use(gameTime);
                }
            }
            if (prevKB.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.S))
            {
                level.Viewer.Down();
            }
            if (prevKB.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.W))
            {
                level.Viewer.Up();
            }
            if (prevKB.IsKeyDown(Keys.A) && keyState.IsKeyDown(Keys.A))
            {
                level.Viewer.Left();
            }
            if (prevKB.IsKeyDown(Keys.D) && keyState.IsKeyDown(Keys.D))
            {
                level.Viewer.Right();
            }
        }

       
    }
}
