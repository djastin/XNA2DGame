using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace Jumping.Models
{
    [XmlRoot(ElementName = "ShootableWeapon")]
    public class ShootableWeapon : Weapon
    {
        private List<Ammunition> _ammo;
        private Timer _shootTimer;
        public Boolean IsCooledDown { get; set; }
        
        public override void Initialize() 
        { 
            base.Initialize(); 
            IsCooledDown = true;

            _shootTimer = new Timer();
            _shootTimer.Elapsed += new ElapsedEventHandler(CooledDown); 
            _shootTimer.Interval = 500; 
            _ammo = new List<Ammunition>(); 
        }
        
        public void AddAmmo(Ammunition ammoArg)
        {
            _shootTimer.Start();
            IsCooledDown = false;
            _ammo.Add(ammoArg);
        }

        public void CooledDown(object source,ElapsedEventArgs e)
        {
            IsCooledDown = true;
            _shootTimer.Stop();
        }

        public override int Use()
        {
            Level level = GetLevel();
            Player p = level.Player;
            p.AddWeapon(this);
            return
            p.GetHp();
        }
        
        public List<Ammunition> GetAmmo()
        {
            return _ammo;
        }
    } 

}
