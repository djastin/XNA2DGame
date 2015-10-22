using Jumping.Models.Core;
using Jumping.Models.Features;
using Jumping.Models.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;
using Jumping.Enumerations;

namespace Jumping.Models
{
    [XmlRoot(ElementName="Level")]
    public class Level
    {
        private Camera _camera;
        private CollisionDetector _collisionDetector;
        private int _totalCollectableItems;
        private TimeSpan _timeRemaining;

        public String Name { get; set; }
        [XmlElement("Player")]
        public Player Player { get; set; }
        [XmlElement("Background")]
        public List<Background> Backgrounds { get; set; }
        [XmlElement(typeof(ShootableWeapon))]
        [XmlElement(typeof(Weapon))]
        [XmlElement(typeof(MedKit))]
        [XmlElement(typeof(Diamond))]
        [XmlElement(typeof(BadGrade))]
        [XmlElement("Item")]
        public List<Item> Items { get; set; }
        [XmlElement("Obstacle")]
        public List<Obstacle> Obstacles { get; set; }
        [XmlElement(typeof(LittleEnemy))]
        [XmlElement(typeof(EndBoss))]
        [XmlElement("MovableObject")]
        public List<MovableObject> MovableObjects { get; set; }
        public List<ThrowObject> ThrownObjects { get; set; }
        [XmlElement("Decoration")]
        public List<Decoration> Decorations { get; set; }
        [XmlIgnore]
        public List<Layer> Layers { get; set; }
        [XmlIgnore]
        public WorldViewer Viewer { get; set; }
        [XmlElement(typeof(AccessibleDoor))]
        [XmlElement("Door")]
        public List<Door> Doors { get; set; }
        [XmlElement("Room")]
        public List<Room> Rooms { get; set; }
        [XmlIgnore]
        public Room EnteredRoom { get; set; }

        public Level()
        {
            Viewer = new WorldViewer();
            _collisionDetector = CollisionDetector.GetInstance();
            _totalCollectableItems = 0;
            _timeRemaining = TimeSpan.FromMinutes(2.0);

            CreateCamera();
            InitializeLayers();
        }

        private void CreateCamera()
        {
            _camera = new Camera(new Viewport(0, 0, 640, 480));
            _camera.Limits = new Rectangle(0, 0, 1700, 1200);
        }

        private void InitializeLayers()
        {
            Layers = new List<Layer>
            {
                new Layer() { Parallax = new Vector2(0.0f, 1.0f)},
                new Layer() { Parallax = new Vector2(0.1f, 1.0f)},
                new Layer() { Parallax = new Vector2(0.1f, 1.0f)},
                new Layer() { Parallax = new Vector2(0.1f, 1.0f)}
            };
        }

        public void LoadRoom(Door door)
        {
            EnteredRoom = FilterRoom(door.Source);
            AddRoomToSprites(EnteredRoom);
        }

        public void AddRoomToSprites(Room room)
        {
            Layers[(int)LayerIndex.RoomGameObjects].Sprites.AddRange(room.Decorations);
            Layers[(int)LayerIndex.RoomGameObjects].Sprites.AddRange(room.Items);
            Layers[(int)LayerIndex.RoomGameObjects].Sprites.AddRange(room.Obstacles);
            Layers[(int)LayerIndex.RoomGameObjects].addSprite(room.Door);
        }

        private Room FilterRoom(String source)
        {
            Room filteredRoom = null;

            foreach (Room room in Rooms)
            {
                if (room.Name == source)
                {
                    filteredRoom = room;
                    filteredRoom.Initialize(this);
                }
            }
            return filteredRoom;
        }

        private void AddGameObjectsToSprites()
        {
            MovableObjects.Add(Player);
            Layers[(int)LayerIndex.LevelDecorations].Sprites.AddRange(Backgrounds);
            Layers[(int)LayerIndex.LevelDecorations].Sprites.AddRange(Decorations);

            Layers[(int)LayerIndex.LevelGameObjects].Sprites.AddRange(Obstacles);
            Layers[(int)LayerIndex.LevelGameObjects].Sprites.AddRange(Items);
            Layers[(int)LayerIndex.LevelGameObjects].Sprites.AddRange(MovableObjects);
            Layers[(int)LayerIndex.LevelGameObjects].Sprites.AddRange(Doors);

            Layers[(int)LayerIndex.Player].Sprites.Add(Player);
        }

        public void AddSpriteToLayer1(ISprite sprite)
        {
            Layers[(int) LayerIndex.LevelGameObjects].Sprites.Add(sprite);
        }

        public Camera GetCamera()
        {
            return _camera;
        }

        public List<MovableObject> GetMovableObjects()
        {
            return MovableObjects;
        }

        public TimeSpan GetRemainingTime()
        {
            return _timeRemaining;
        }

        public void InitializeGameObjects()
        {
            Player.Initialize();

            foreach (Background background in Backgrounds)
                background.Initialize();

            foreach (Obstacle obstacle in Obstacles)
                obstacle.Initialize();

            foreach (MovableObject movableObject in MovableObjects)
            {
                if (movableObject is Enemy)
                    InitializeEnemy((Enemy)movableObject);
            }

            foreach (Decoration decoration in Decorations)
                decoration.Initialize();

            foreach (Item item in Items)
            {
                item.Initialize();
                item.SetLevel(this);
            }

            foreach (Door door in Doors)
                door.Initialize();

            CountTotalCollectableItems();
        }

        private void InitializeEnemy(Enemy enemy)
        {
            if (enemy is EndBoss)
            {
                ((EndBoss)enemy).Initialize();
            }
            else
            {
                enemy.Initialize(true);
            }
            enemy.SetLevel(this);
            enemy.SetAttack();
        }

        public void Load()
        {
            Player.SetLevel(this);
            InitializeGameObjects();
            AddGameObjectsToSprites();
            
        }

        private void CountTotalCollectableItems()
        {
            foreach (Item item in Items)
            {
                if (item is Diamond || item is BadGrade)
                    _totalCollectableItems++;
            }
        }

        public int GetTotalCollectableItems()
        {
            return _totalCollectableItems;
        }

        public Door GetDoorOfCollision()
        {
            foreach (Door filteredDoor in Doors)
            {
                Rectangle filteredDoorRectangle = new Rectangle((int)filteredDoor.Position.X,
                    (int)filteredDoor.Position.Y, filteredDoor.Texture.Width, filteredDoor.Texture.Height);

                if (_collisionDetector.CheckCollision(Player.CollisionBox, filteredDoorRectangle))
                {
                    return filteredDoor;
                }
            }
            return null;
        }
    

        public MovableObject GetMovableObjectOfCollision()
        {
            foreach (MovableObject movableObject in MovableObjects)
            {
                if (movableObject is Enemy)
                {
                    Enemy filteredEnemy = (Enemy)movableObject;

                    if (_collisionDetector.CheckCollision(Player.CollisionBox, filteredEnemy.CollisionBox))
                    {
                        return filteredEnemy;
                    }
                }
            }
            return null;
        }

        public Item GetItemOfCollision()
        {
            foreach (Item filteredItem in Items)
            {
                if (_collisionDetector.CheckCollision(Player.CollisionBox, filteredItem.CollisionBox))
                {
                    return filteredItem;
                }
            }
            return null;
        }
        
        public void ObstacleOfCollision()
        {
            foreach (Obstacle filteredObstacle in Obstacles)
            {
                foreach (MovableObject filteredMovableObject in MovableObjects)
                {
                    if (_collisionDetector.CheckObstacleCollision(filteredMovableObject, filteredObstacle))
                        filteredMovableObject.HandleCollidedObstacle(filteredObstacle);
                }
            }
        }

        /*oude maar deze werkte wel 
        public void CheckAmmoCollision()
        {
            for (int enemy_inc = 0; enemy_inc <= MovableObjects.Count - 1; enemy_inc++)
            {
                if (MovableObjects[enemy_inc] is Enemy)
                {
                    Enemy enemy = (Enemy)MovableObjects[enemy_inc];
                    if (Player.GetWeapon() is ShootableWeapon)
                    {
                        for (int ammunition_inc = 0; ammunition_inc <= ((ShootableWeapon)Player.GetWeapon()).GetAmmo().Count() - 1;
                            ammunition_inc++)
                        {
                            Ammunition ammunition = ((ShootableWeapon)Player.GetWeapon()).GetAmmo()[ammunition_inc];
                            if (ammunition.CheckCollision(enemy))
                            {
                                enemy.Hit(100);
                                ((ShootableWeapon)Player.GetWeapon()).GetAmmo().Remove(ammunition);
                            }
                        }
                    }
                }
            }
        }*/
      
        public void CheckAmmoCollision()
        {
            for (int object_inc = 0; object_inc < MovableObjects.Count(); object_inc++)
            {
                Ammunition ammunition = MovableObjects[object_inc].hasCollissionWithAmmunition();
               if (ammunition != null)
               {
                    Enemy enemy = (Enemy)MovableObjects[object_inc];
                
                    enemy.Hit(100);
                    ((ShootableWeapon)Player.GetWeapon()).GetAmmo().Remove(ammunition);
                }
            }
        }
      
        public void CheckThrowObjectCollission(MovableObject movableObject)
        {
            ThrowObject throwObject = movableObject.hasCollissionWithThrowObject();
            if (throwObject != null)
            {
                MovableObject thrower = throwObject.GetThrower();
                if (movableObject is Enemy && Player.GetAttack() is ThrowAttack)
                {
                    Enemy enemy = (Enemy)movableObject;
                    ThrowAttack attack = ((ThrowAttack)Player.GetAttack());
                    attack.ThrownObjectHasCollidedWithEnemy(enemy, throwObject);
                }
                else if (movableObject is Player && thrower is Enemy && ((Enemy)thrower).GetAttack() is ThrowAttack)
                {
                    ThrowAttack attack = ((ThrowAttack)((Enemy)thrower).GetAttack());
                    attack.ThrownObjectHasCollidedWithPlayer(Player, throwObject);
                }
            }
        }

       public void UpdateAnimatedDecorations()
       {
           foreach (Decoration decoration in Decorations)
               decoration.Update();
       }

       private void DrawLayers(GameTime gameTime, SpriteBatch spriteBatch)
       {
           Layers[(int)LayerIndex.LevelDecorations].Draw(gameTime, spriteBatch);
           Layers[(int)LayerIndex.LevelGameObjects].Draw(gameTime, spriteBatch);
       }

       public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
       {
           if (EnteredRoom != null)
           {
               if (EnteredRoom.IsPresent)
               {
                   Layers[(int)LayerIndex.RoomGameObjects].Draw(gameTime, spriteBatch);
                   Layers[(int)LayerIndex.Player].Draw(gameTime, spriteBatch);
               }
               else
                   DrawLayers(gameTime, spriteBatch);
           }
           else
               DrawLayers(gameTime, spriteBatch);

           if (Player.IsAlive())
           {
               Weapon weapon = Player.GetWeapon();
               weapon.Draw(gameTime, spriteBatch);

               if (Player.IsMeditating())
               {
                   Player player = Player;
                   CalculatorBoost calculatorBoost = ((CalculatorBoost)player.GetBoost());
                   calculatorBoost.Draw(spriteBatch);
                   
                   //MeditationBoost meditationBoost = ((MeditationBoost)player.GetBoost());
                   //meditationBoost.Draw(spriteBatch, player.Position);
               }
           }

           foreach (ThrowObject throwObject in ThrownObjects)
           {
               throwObject.Draw(spriteBatch);
           }

           foreach (MovableObject movableObject in MovableObjects)
           {
               if (movableObject is EndBoss)
               {
                   EndBoss endBoss = (EndBoss)movableObject;

                   if (endBoss.GetSpecialAttackEnabled() && endBoss.GetAttack() is InstaAttack)
                   {
                       ((InstaAttack)endBoss.GetAttack()).Draw(spriteBatch);
                   }
               }

               /*if (movableObject is Enemy)
               {
                   Enemy enemy = (Enemy)movableObject;
                   ThrowAttack attack = ((ThrowAttack)enemy.GetAttack());

                   if (attack != null && attack.GetThrowObject(enemy) != null)
                   {
                       attack.GetThrowObject(enemy).Move(Direction.Right, attack.GetThrowObject(enemy).Speed);
                       attack.GetThrowObject(enemy).Update(gameTime);
                       attack.GetThrowObject(enemy).Draw(spriteBatch);
                   }
               }*/

              
           }
       }

       public void RemoveEdgeScreenThrownObjects()
       {
           for (int object_inc = 0; object_inc < ThrownObjects.Count(); object_inc++)
           {
               if (ThrownObjects[object_inc].Position.X < 0 || ThrownObjects[object_inc].Position.X + ThrownObjects[object_inc].frameWidth >= ThrownObjects[object_inc].ScreenBound.Width)
               {
                   
                   ThrowAttack attack = null;
                   if (ThrownObjects[object_inc].GetThrower() is LittleEnemy)
                   {
                       attack = (ThrowAttack)((LittleEnemy)ThrownObjects[object_inc].GetThrower()).GetAttack();
                   }
                   else if (ThrownObjects[object_inc].GetThrower() is EndBoss)
                   {
                       attack = (ThrowAttack)((EndBoss)ThrownObjects[object_inc].GetThrower()).GetAttack();
                   }
                   else if (ThrownObjects[object_inc].GetThrower() is Player)
                   {
                       attack = (ThrowAttack)((Player)ThrownObjects[object_inc].GetThrower()).GetAttack();
                   }
                   attack.DeleteThrowObject(ThrownObjects[object_inc]);
               }
           }
       }

        public void RemoveDeadObjects() 
       { 
            for (int obj_inc = 0; obj_inc <= MovableObjects.Count - 1; obj_inc++) 
           { 
                if (!MovableObjects[obj_inc].IsAlive()) 
               { 
                   Layers[1].Sprites.Remove(MovableObjects[obj_inc]); 
                   MovableObjects.Remove(MovableObjects[obj_inc]);            
               } 
           } 
       } 


       private void UpdateThrownObjects(GameTime gametime)
       {
           foreach (ThrowObject throwObject in ThrownObjects)
           {
               throwObject.Update(gametime);
           }
       }

        public void Update(GameTime gameTime)
        {
            _camera.LookAt(Player.Position);
            CheckAmmoCollision();
            Player.Update(gameTime);
            
            

            for (int object_inc = 0;object_inc < MovableObjects.Count(); object_inc++)
            {
                if (MovableObjects[object_inc] is Enemy)
                {
                    Enemy enemy = (Enemy)MovableObjects[object_inc];
                    enemy.Update(gameTime);
                }

                if (MovableObjects[object_inc].GetType() == typeof(LittleEnemy))
                {
                    if (((LittleEnemy)MovableObjects[object_inc]).AttackName == "ThrowAttack")
                    {
                        LittleEnemy enemy = (LittleEnemy)MovableObjects[object_inc];

                        if (enemy.GetAttack() != null)
                        {
                            ThrowAttack attack = ((ThrowAttack)enemy.GetAttack());
                            Random random = new Random();

                            if (random.Next(0, 1000) > 990)
                            {
                                enemy.SetState(EnemyState.ATTACK);
                                if (enemy.GetState() == EnemyState.ATTACK)
                                    enemy.GetAttack().Use(enemy);
                            }
                        }
                    }
                }

                CheckThrowObjectCollission(MovableObjects[object_inc]);
            }
           
            UpdateThrownObjects(gameTime);
            ObstacleOfCollision();
            UpdateAnimatedDecorations();
            RemoveEdgeScreenThrownObjects();
            RemoveDeadObjects();
            _timeRemaining -= gameTime.ElapsedGameTime;
        }
    }
 }

    

