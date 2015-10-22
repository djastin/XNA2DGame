using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Jumping.Models.Core
{
    public class CollisionDetector
    {
        private static CollisionDetector _collisionDetector;

        public bool CollisionLeft { get; set; }
        public bool CollisionRight { get; set; }
        public bool CollisionTop { get; set; }
        public bool CollisionBottom { get; set; }

        private CollisionDetector() { }

        public static CollisionDetector GetInstance()
        {
            if (_collisionDetector == null)
            {
                _collisionDetector = new CollisionDetector();
            }
            return _collisionDetector;
        }

        public bool CheckCollision(Rectangle A, Rectangle B)
        {
            if (A.Intersects(B))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckLeftObstacleCollision(MovableObject obj, Obstacle platform)
        {
            Rectangle collisionBox = obj.CollisionBox = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, obj.Texture.Width, obj.Texture.Height);

            Rectangle left = new Rectangle((int)platform.Position.X - 10, (int)platform.Position.Y + 5, 10, platform.Texture.Height - 10);
            return CollisionLeft = (obj.CollisionBox.Intersects(left));
        }

        public bool CheckRightObstacleCollision(MovableObject obj, Obstacle platform)
        {
            Rectangle collisionBox = obj.CollisionBox = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, obj.Texture.Width, obj.Texture.Height);

            Rectangle right = new Rectangle((int)platform.Position.X + platform.Texture.Width, (int)platform.Position.Y + 5, 10, platform.Texture.Height - 10);
            return CollisionRight = (obj.CollisionBox.Intersects(right));
        }

        public bool CheckTopObstacleCollision(MovableObject obj, Obstacle platform)
        {
            Rectangle collisionBox = obj.CollisionBox = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, obj.Texture.Width, obj.Texture.Height);

            Rectangle top = new Rectangle((int)platform.Position.X + 5, (int)platform.Position.Y - 10, platform.Texture.Width - 10, 10);
            return CollisionTop = (obj.CollisionBox.Intersects(top));
        }

        public bool CheckBottomObstacleCollision(MovableObject obj, Obstacle platform)
        {
            obj.CollisionBox = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, obj.SelectedAnimation.FrameWidth, obj.SelectedAnimation.FrameHeight);

            Rectangle bottom = new Rectangle((int)platform.Position.X + 5, (int)platform.Position.Y + platform.Texture.Height, platform.Texture.Width - 10, 10);
            return CollisionBottom = (obj.CollisionBox.Intersects(bottom));
        }

        public bool CheckObstacleCollision(MovableObject obj, Obstacle platform)
        {
            if (CheckBottomObstacleCollision(obj, platform) || CheckLeftObstacleCollision(obj, platform) || CheckRightObstacleCollision(obj, platform) 
                || CheckTopObstacleCollision(obj, platform) == true)
                return  true;
            else
                return false;


        }
    }
}
