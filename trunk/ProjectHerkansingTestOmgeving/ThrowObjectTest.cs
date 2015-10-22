using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jumping;
using Jumping.Models;
using Jumping.Models.Sprites;
using Microsoft.Xna.Framework;
using Jumping.Models.Features;

namespace ProjectHerkansingTestOmgeving
{
    [TestClass]
    public class ThrowObjectTest
    {
        Player player;
        ThrowObject thrownObject;
        Level level;
        Enemy throwingEnemy;
        ThrowAttack throwAttack;

        [TestInitialize]
        public void Setup()
        {
            player = new Player();
            player.width = 50;
            player.height = 50;

            
            thrownObject = new ThrowObject();
            thrownObject.width = 50;
            thrownObject.height = 50;

            level = new Level();
            level.Player = (Player)player;
            level.ThrownObjects = new List<ThrowObject>();
            level.ThrownObjects.Add((ThrowObject)thrownObject);

            throwAttack = new ThrowAttack();
            throwAttack.SetLevel(level);
            thrownObject.SetLevel(level);
            player.SetLevel(level);
            throwingEnemy = new LittleEnemy();
            throwingEnemy.SetAttack(throwAttack);

        }
        // TEST COLLISIONS MET PLAYER & THROWNOBJECT VAN ALLE VIER DE KANTEN 

        [TestMethod]
        public void TestPlayerCollidedWithThrownObjectOnright()
        {
            player.Position = new Vector2(50, 50);
            thrownObject.Position = new Vector2(99, 50);

            player.CollisionBox = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.width, player.height);
            thrownObject.CollisionBox = new Rectangle((int)thrownObject.Position.X, (int)thrownObject.Position.Y, thrownObject.width, thrownObject.height);

            thrownObject.SetThrower(throwingEnemy);

            ThrowObject throwObject = player.hasCollissionWithThrowObject();
            Assert.AreEqual(throwObject, thrownObject,"Player heeft geen collision met de throwObject!");
        }
        [TestMethod]
        public void TestPlayerCollidedWithThrownObjectOnLeft()
        {
            player.Position = new Vector2(50, 50);
            thrownObject.Position = new Vector2(1, 50);

            player.CollisionBox = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.width, player.height);
            thrownObject.CollisionBox = new Rectangle((int)thrownObject.Position.X, (int)thrownObject.Position.Y, thrownObject.width, thrownObject.height);

            thrownObject.SetThrower(throwingEnemy);

            ThrowObject throwObject = player.hasCollissionWithThrowObject();
            Assert.AreEqual(throwObject, thrownObject, "Player heeft geen collision met de throwObject!");
        }

        [TestMethod]
        public void TestPlayerCollidedWithThrownObjectOnTop()
        {
            player.Position = new Vector2(50, 50);
            thrownObject.Position = new Vector2(50, 1);

            player.CollisionBox = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.width, player.height);
            thrownObject.CollisionBox = new Rectangle((int)thrownObject.Position.X, (int)thrownObject.Position.Y, thrownObject.width, thrownObject.height);

            thrownObject.SetThrower(throwingEnemy);

            ThrowObject throwObject = player.hasCollissionWithThrowObject();
            Assert.AreEqual(throwObject, thrownObject, "Player heeft geen collision met de throwObject!");
        }
        [TestMethod]
        public void TestPlayerCollidedWithThrownObjectOnBottom()
        {
            player.Position = new Vector2(50, 50);
            thrownObject.Position = new Vector2(1, 99);

            player.CollisionBox = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.width, player.height);
            thrownObject.CollisionBox = new Rectangle((int)thrownObject.Position.X, (int)thrownObject.Position.Y, thrownObject.width, thrownObject.height);

            thrownObject.SetThrower(throwingEnemy);

            ThrowObject throwObject = player.hasCollissionWithThrowObject();
            Assert.AreEqual(throwObject, thrownObject, "Player heeft geen collision met de throwObject!");
        }
        // TEST OF ER EEN COLLISION IS ALS DE THROWNOBJECT OP DE PLAYER POSITION ZIT 
        [TestMethod]
        public void TestPlayerCollidedWithThrownObjectInside()
        {
            player.Position = new Vector2(50, 50);
            thrownObject.Position = new Vector2(50, 50);

            player.CollisionBox = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.width, player.height);
            thrownObject.CollisionBox = new Rectangle((int)thrownObject.Position.X, (int)thrownObject.Position.Y, thrownObject.width, thrownObject.height);

            thrownObject.SetThrower(throwingEnemy);

            ThrowObject throwObject = player.hasCollissionWithThrowObject();
            Assert.AreEqual(throwObject, thrownObject, "Player heeft geen collision met de throwObject!");
        }

        [TestMethod]
        public void TestPlayerIsHitByCollision()
        {
            player.Hp = 50;
            thrownObject.setDamage(40);
            thrownObject.SetThrower(throwingEnemy);

            ThrowAttack attack = (ThrowAttack)throwingEnemy.GetAttack();
            attack.ThrownObjectHasCollidedWithPlayer(player, throwObject: thrownObject);

            Assert.AreEqual(player.Hp, 10);
        }

        [TestMethod]
        public void TestThrownObjectDeletedAfterCollision()
        {
            ThrowAttack throwAttack = (ThrowAttack)throwingEnemy.GetAttack();
            throwAttack.DeleteThrowObject(thrownObject);

            Assert.IsFalse(level.ThrownObjects.Contains(thrownObject));
        }


        // TEST POSITIE UPDATE
        [TestMethod]
        public void TestThrownObjectPositionToTheRightUpdate()
        {
            Vector2 expectedEndPosition = new Vector2(55, 50);
            thrownObject.Position = new Vector2(50, 50);

            thrownObject.direction = Direction.Right;
            thrownObject.Speed = 5f;
            thrownObject.UpdateThrownObjectPosition();
            Assert.AreEqual(thrownObject.Position, expectedEndPosition, "Positie komt niet overeen met verwachte eind positie!");
        }
        [TestMethod]
        public void TestThrownObjectPositionToTheLeftUpdate()
        {
            Vector2 expectedEndPosition = new Vector2(45, 50);
            thrownObject.Position = new Vector2(50, 50);

            thrownObject.direction = Direction.Left;
            thrownObject.Speed = 5f;
            thrownObject.UpdateThrownObjectPosition();
            Assert.AreEqual(thrownObject.Position, expectedEndPosition, "Positie komt niet overeen met verwachte eind positie!");
        }


        // TEST COLLISIONBOX UPDATE
        [TestMethod]
        public void TestThrownObjectCollisionBoxToTheRightUpdate()
        {
            thrownObject.Position = new Vector2(50, 50);
            Rectangle expectedCollisionBox = new Rectangle((int)thrownObject.Position.X + 5, (int)thrownObject.Position.Y, thrownObject.width, thrownObject.height);

            thrownObject.direction = Direction.Right;
            thrownObject.Speed = 5f;
            thrownObject.UpdateThrownObjectPosition();
            thrownObject.UpdateCollisionBoxWithoutTextureSize();

            Assert.AreEqual(thrownObject.CollisionBox, expectedCollisionBox, "CollisionBox komt niet overeen met verwachte eind positie!");
        }
        [TestMethod]
        public void TestThrownObjectCollisionBoxToTheLeftUpdate()
        {
            thrownObject.Position = new Vector2(50, 50);
            Rectangle expectedCollisionBox = new Rectangle((int)thrownObject.Position.X -5, (int)thrownObject.Position.Y, thrownObject.width, thrownObject.height);

            thrownObject.direction = Direction.Left;
            thrownObject.Speed = 5f;
            thrownObject.UpdateThrownObjectPosition();
            thrownObject.UpdateCollisionBoxWithoutTextureSize();

            Assert.AreEqual(thrownObject.CollisionBox, expectedCollisionBox, "CollisionBox komt niet overeen met verwachte eind positie!");
        }
        [TestMethod]
        public void TestThrownObjectsAtLeftScreenEdgeDeletion()
        {
            thrownObject.Position = new Vector2(4, 50);
            thrownObject.ScreenBound = new Rectangle(0, 0, 1000, 1000);
            thrownObject.frameWidth = thrownObject.width;
            thrownObject.direction = Direction.Left;
            thrownObject.Speed = 5f;
            thrownObject.UpdateThrownObjectPosition();
            thrownObject.UpdateCollisionBoxWithoutTextureSize();
            thrownObject.SetThrower(throwingEnemy);
            level.RemoveEdgeScreenThrownObjects();

            Assert.IsFalse(level.ThrownObjects.Contains(thrownObject));
        }
        [TestMethod]
        public void TestThrownObjectsAtRightScreenEdgeDeletion()
        {
            thrownObject.Position = new Vector2(950, 50);
            thrownObject.ScreenBound = new Rectangle(0, 0, 1000, 1000);
            thrownObject.frameWidth = thrownObject.width;
            thrownObject.direction = Direction.Right;
            thrownObject.Speed = 5f;
            thrownObject.UpdateThrownObjectPosition();
            thrownObject.UpdateCollisionBoxWithoutTextureSize();
            thrownObject.SetThrower(throwingEnemy);
            level.RemoveEdgeScreenThrownObjects();

            Assert.IsFalse(level.ThrownObjects.Contains(thrownObject));
        }
        [TestMethod]
        public void TestThrownObjectsNotDeletingInScreenBound()
        {
            thrownObject.Position = new Vector2(6, 50);
            thrownObject.ScreenBound = new Rectangle(0, 0, 1000, 1000);
            thrownObject.frameWidth = thrownObject.width;
            thrownObject.direction = Direction.Left;
            thrownObject.Speed = 5f;
            thrownObject.UpdateThrownObjectPosition();
            thrownObject.UpdateCollisionBoxWithoutTextureSize();
            thrownObject.SetThrower(throwingEnemy);
            level.RemoveEdgeScreenThrownObjects();

            Assert.IsTrue(level.ThrownObjects.Contains(thrownObject));
        }
    }
}
