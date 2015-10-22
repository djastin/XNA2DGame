using Jumping.Models.Features;
using Jumping.Models.Interfaces;
using Jumping.Models.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Jumping.Models.Core
{
    [XmlRoot(ElementName = "Room")]
    public class Room 
    {
       
        private Level _level;
        private Player _player;

        [XmlElement("Name")]
        public String Name { get; set; }
        [XmlElement("Obstacle")]
        public List<Obstacle> Obstacles { get; set; }
        [XmlElement("Decoration")]
        public List<Decoration> Decorations { get; set; }
        [XmlElement(typeof(ShootableWeapon))]
        [XmlElement(typeof(Weapon))]
        [XmlElement(typeof(MedKit))]
        [XmlElement(typeof(Diamond))]
        [XmlElement(typeof(BadGrade))]
        [XmlElement("Item")]
        public List<Item> Items { get; set; }
        [XmlElement("Present")]
        public bool IsPresent { get; set; }
        [XmlElement("Door")]
        public Door Door { get; set; }

        public void Initialize(Level level)
        {
            this._level = level;
            _player = level.Player;
            Vector2 doorPosition = Door.Position;

            foreach (Obstacle obstacle in Obstacles)
                obstacle.Initialize();
            
            foreach (Decoration decoration in Decorations)
                decoration.Initialize();
     
            foreach (Item item in level.Items)
                item.Initialize();
            
            Door.Initialize();
        }

        private Item GetItemOfCollision()
        {
            for (int item_i = 0; item_i < Items.Count(); item_i++)
            {
                Item item = Items[item_i];
                if (CollisionDetector.GetInstance().CheckCollision(_player.CollisionBox, new Rectangle((int)item.Position.X, (int)item.Position.Y,
                    item.Texture.Width, item.Texture.Height)))
                {
                    return item;
                }
            }
            return null;
        }

        public void ObstacleOfCollision()
        {
            Obstacle obstacle = null;

            for (int obstacle_inc = 0; obstacle_inc <= Obstacles.Count - 1; obstacle_inc++)
            {
                obstacle = Obstacles[obstacle_inc];

                if (CollisionDetector.GetInstance().CheckObstacleCollision(_player, obstacle))
                {
                    _player.HandleCollidedObstacle(obstacle);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            _player.Update(gameTime);

            ObstacleOfCollision();
            HandleDoorCollision();
            _level.GetCamera().LookAt(_player.Position);
        }

        public Door GetDoorOfCollision()
        {
            if (CollisionDetector.GetInstance().CheckCollision(_player.CollisionBox, new Rectangle((int)Door.Position.X,
                (int)Door.Position.Y, Door.Texture.Width, Door.Texture.Height)))
            {
                return Door;
            }

            return null;
        }

        public void HandleDoorCollision()
        {
            Door door = GetDoorOfCollision();
            if (door != null)
            {
                IsPresent = false;
            }
        }
    }
}
