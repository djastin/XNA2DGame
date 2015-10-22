using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jumping;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jumping.Models;

namespace TestEnvironment
{
    [TestClass]
    public class MovingPlayerTest
    {
        Player Player;

        [TestInitialize]
        public void Setup()
        {
            Rectangle screenBound = new Rectangle();
            Player = new Player();
            Player._Position = Vector2.Zero;
            Player.Speed = 6f;
            Player.ScreenBound = screenBound;
            Player._velocity = new Vector2(0.2f, 0);
        }

        [TestMethod]
        public void TestPlayerPosition()
        {
            Player.Move(Direction.Right, 2f);
            Player.UpdatePosition();

            Assert.AreEqual((decimal)Player._Position.X, (decimal)1.8);

            /**
             * Calculation:
             * Position Player = 0; Velocity.X = 0.2f
             * Speed = 6f; Position.X = 0
             * 
             * Position.X += (0.3f * 6) = 1.8
             * 
             * */

            Player.Move(Direction.Right, 2f);
            Player.UpdatePosition();

            Assert.AreEqual((decimal)Player._Position.X, (decimal)4.2);

            /*
             * Calculation:
             * Position Player = 1.8; Velocity.X = 0.3f
             * Speed = 6f; Position.X = 1.8
             * 
             * Position.X += (0.4f * 6) = 4.2
             * 
             * */
        }

        [TestMethod]
        public void TestPlayerJumping()
        {

        }
    }
}
