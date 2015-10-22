﻿using Jumping.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers
;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Jumping.Models.Features
{
    public class HeadRollAttack : IAttackBehavior
    {

        private Player player;
        private Boolean headrollCooledDown;
        private Boolean animateRoll;
        private int oldDamage;
        private Timer rollTimer = new Timer();
        private Timer coolDownTimer = new Timer();
        public int damage;

        public void Use(MovableObject objectType) { }

        public HeadRollAttack()
        {
            headrollCooledDown = true;
            animateRoll = false;
            damage = 20;



            rollTimer.Elapsed += new ElapsedEventHandler(StopRolling);
            rollTimer.Interval = 800;
            coolDownTimer.Elapsed += new ElapsedEventHandler(CoolDown);
            coolDownTimer.Interval = 2000;
        }

        public void Update(GameTime gametime)
        {
            if (animateRoll)
            {
                player.SetIsAnimatedObject(false);

                if (player.Direction == Direction.Right)
                {
                    player.GetAnimator().PlayAnimation(player.States[2]);
                    player.Move(Direction.Right, 0.1f);
                }
                else
                {
                    player.GetAnimator().PlayAnimation(player.States[2]);
                    player.Move(Direction.Left, 0.1f);
                }
            }
        }

        public void Use(GameTime gametime)
        {
            if (headrollCooledDown)
            {
                oldDamage = player.getDamage();
                player.setDamage(damage);
                rollTimer.Start();
                coolDownTimer.Start();
                animateRoll = true;
                player.SetDoesDamage(true);
                headrollCooledDown = false;

                //player.animateWalk = false;
            }
        }


        private void StopRolling(object source, ElapsedEventArgs e)
        {
            animateRoll = false;
            player.SetIsAnimatedObject(true);
            player.SetDoesDamage(false);
            player.setDamage(oldDamage);
            //player.animateWalk = true;
            rollTimer.Stop();
        }

        private void CoolDown(object source, ElapsedEventArgs e)
        {
            headrollCooledDown = true;
            coolDownTimer.Stop();
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }
    }
}