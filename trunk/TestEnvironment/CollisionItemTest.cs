using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jumping;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jumping.Models;
using Jumping.Models.Core;
using Jumping.Models.Features;

namespace TestEnvironment
{
    [TestClass]
    public class CollisionItemTest
    {
        Player player;

        [TestInitialize]
        public void Setup()
        {
            Rectangle screenBound = new Rectangle();
            player = new Player();
            player._Position = Vector2.Zero;
            player.Speed = 6f;
            player.ScreenBound = screenBound;
            player._velocity = new Vector2(0.2f, 0);
        }

        [TestMethod]
        public void TestCollision()
        {
            Item medKit = new MedKit();
            medKit.Position = new Vector2(4.2f, 0);

            player.Move(Direction.Right, 2f);
            player.UpdatePosition();
            player.Move(Direction.Right, 2f);
            player.UpdatePosition();

            player.CollisionBox = new Rectangle((int)player._Position.X, (int)player._Position.Y, 1, 1);
            medKit.CollisionBox = new Rectangle((int)medKit.Position.X, (int)medKit.Position.Y, 1, 1);

            Assert.IsTrue(medKit.CollisionBox.Intersects(player.CollisionBox));
        }
        
    }
}
