using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jumping.Models.Interfaces;
using Jumping.Models.Graphics;
using Microsoft.Xna.Framework;

namespace Jumping.Models.Features
{
    public class MinionAttack: IAttackBehavior
    {
        private MovableObject _minionOwner;
        private Boolean _doneSpawningMinions = true;

        public void Use(MovableObject movableObject)
        {
            if (_doneSpawningMinions)
            {
                _minionOwner = movableObject;
                SpawnMinions();
                _doneSpawningMinions = false;
            }
        }
        public void Use(GameTime gametime) { }
        private void SpawnMinions()
        {
            int i = 0;
            while (i < 3)
            {
                CreateMinion(i);
                i++;
            }
            _doneSpawningMinions = true;
        }

        private void CreateMinion(int i)
        {
            LittleEnemy minion = new LittleEnemy();
            SetMinionDefaultInformation(i, minion);
            SetMinionAnimations(minion);
            SetMinionStrategies(minion);
            minion.Initialize();
            minion.Texture = TextureLoader.GetInstance().GetTexture("LittleHipster");
            _minionOwner.GetLevel().MovableObjects.Add(minion);
            _minionOwner.GetLevel().AddSpriteToLayer1(minion);
        }

        private static void SetMinionStrategies(LittleEnemy minion)
        {
            minion.StrategyName = "curious";
            minion.AttackName = "ThrowAttack";
            minion.SetAttackBehavior(FeatureLoader.GetInstance().GetAttack(minion.AttackName));
        }

        private void SetMinionDefaultInformation(int i, LittleEnemy minion)
        {
            minion.Position = _minionOwner.Position + new Vector2(i * 100, 0);
            minion.Speed = 1.5f;
            minion.frameHeight = 37;
            minion.frameWidth = 60;
            minion.SetLevel(_minionOwner.GetLevel());
            minion.ScreenBoundWidth = _minionOwner.ScreenBoundWidth;
            minion.ScreenBoundHeight = _minionOwner.ScreenBoundHeight;
        }

        private static void SetMinionAnimations(LittleEnemy minion)
        {
            minion.DefaultState = "littleHipsterIdle";
            Animation2 animation1 = new Animation2();
            animation1.TextureName = "littleHipsterIdle";
            animation1.IsLooping = true;
            animation1.FrameTime = 0.5f;
            minion.States.Add(animation1);
            
            Animation2 animation2 = new Animation2();
            animation2.TextureName = "littleHipsterWalking";
            animation2.IsLooping = true;
            animation2.FrameTime = 0.5f;
            minion.States.Add(animation2);
        }

        public void Update(GameTime gameTime) { }
        public void SetPlayer(Player player) { }
    }
 }

