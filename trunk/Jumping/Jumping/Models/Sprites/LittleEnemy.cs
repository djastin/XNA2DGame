using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jumping.Models.Graphics;

namespace Jumping.Models
{
    [XmlRoot(ElementName = "LittleEnemy")]
    public class LittleEnemy : Enemy
    {
        public void Initialize()
        {
            
            base.Initialize(true);
        }
        
    }
}
