﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jumping.Models;
using Jumping.Models.Core;
using Jumping.Models.Features;

namespace TestEnvironment
{
    [TestClass]
    public class AddToLayersTest
    {
        Layer layer;


        [TestInitialize]
        public void Setup()
        {
            layer = new Layer(null);
        }

        [TestMethod]
        public void TestSpriteRemoving()
        {
            Item medKit1, medKit2, weapon1;
            medKit1 = new MedKit();
            medKit2 = new MedKit();
            weapon1 = new ShootableWeapon();

            layer.addSprite(medKit1);
            layer.addSprite(medKit2);
            layer.addSprite(weapon1);

            layer.Sprites.Remove(medKit2);

            Assert.AreSame((int)layer.Sprites.Count, 2);
        }
    }
}
