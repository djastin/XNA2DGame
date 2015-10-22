using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Jumping.Models;
using System.Timers;
using Jumping.Controllers;

namespace Jumping
{
    public class LevelLoader
    {
        private static LevelLoader instance;
        private Player player;
        private int tileWidth, tileHeight;
        private SpriteFont font, deadFont;

        private Level selected_level;
        private List<Level> Levels;

        private static ContentManager Content;
        private static GraphicsDeviceManager graphics;
        
        private Color BackgroundColor;
        KeyboardState prevKB;
        private Camera Camera;

        private GameController Controller;

        private LevelLoader(ContentManager _Content, GraphicsDeviceManager _graphics)
        {
            Content = _Content;
            graphics = _graphics;
 
            Levels = new List<Level>();
            BackgroundColor = Color.White;
        }

        public static LevelLoader getInstance(ContentManager Content, GraphicsDeviceManager graphics)
        {
            if (instance == null)
            {
                instance = new LevelLoader(Content, graphics);
            }
            return instance;
        }

        public void InitializeLevels()
        {
            Level l1, l2;

            char[,] tiles1 = {{'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','#','#','#','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','#','#','#','#','#', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','#','#','#','#','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','#','#','#','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','#','#','#','#','.','.','.','.','.', '.','.','.','.','.','#','#','#','#','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','#','#','#','#','#','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','#','#','#','#','.','#','#','#','#','.','.'},
                              {'.','.','.','.','.','.','.','.','#','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','#','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','#','#','#','#','.','.','.', '.','#','#','#','.','.','.','.','.','#','#','#','#','.','.','.', '#','#','#','#','#','#','#','#','#','#','#','#','#','#','.','.', '#','#','#','#','#','#','#','#','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'P','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#', '#','#','#','#','#','#','#','#','#','#','#','#','#','#','.','.', '#','#','#','#','#','#','#','#','.','.','.','.','.','.','.','.', '#','#','#','#','#','#','#','#','#','#','#','.','.','.','.','.', '.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#', '#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#', '#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#', '#','#','#','#','#','#','#','#','.','#','#','#','#','#','#','#', '#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#', '#','.','.','#','#','#','#','#','#','#','#','#','#','#','#','#'}};
             
            char[,] tiles2 = {{'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','C','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','C','.','.','#','.','.','.'},
                              {'.','.','.','.','.','.','.','-','-','-','.','.','#','.','C','.'},
                              {'.','.','.','.','.','C','.','.','.','.','.','.','#','.','C','.'},
                              {'.','.','.','-','-','-','.','.','.','.','.','.','#','.','C','.'},
                              {'C','.','.','.','.','.','.','.','.','.','.','.','#','.','C','.'},
                              {'#','#','.','.','.','.','.','.','.','.','.','.','#','.','C','.'},
                              {'#','#','.','.','.','.','.','.','.','P','.','.','#','.','C','.'}};

            l1 = new Level(tiles1);
            l2 = new Level(tiles2);

            Levels.Add(l1);
            Levels.Add(l2);

            selected_level = l1;

            Camera = new Camera(graphics.GraphicsDevice.Viewport);
            Camera.Limits = new Rectangle(0, 0, 1700, 1200);

            selected_level.SetCamera(Camera);
        }
        public void loadContent()
        {
            TextureLoader textureLoader = TextureLoader.GetInstance();

            Texture2D blockSpriteA = Content.Load<Texture2D>("blockA");
            Texture2D blockSpriteB = Content.Load<Texture2D>("blockB");
            Texture2D coin = Content.Load<Texture2D>("coin");
            Texture2D background = Content.Load<Texture2D>("b");
            Texture2D weapon_texture = Content.Load<Texture2D>("weapon");
            Texture2D ammo_texture = Content.Load<Texture2D>("ammo");

            textureLoader.addTexture("blockA", blockSpriteA);
            textureLoader.addTexture("blockB", blockSpriteB);
            textureLoader.addTexture("coin", coin);
            textureLoader.addTexture("b", background);
            textureLoader.addTexture("weapon", weapon_texture);
            textureLoader.addTexture("ammo", ammo_texture);

            font = Content.Load<SpriteFont>("Font");
            deadFont = Content.Load<SpriteFont>("DeadFont");

            Texture2D ballSprite = Content.Load<Texture2D>("ball");
            player = new Player(Vector2.Zero, 6.0f, new Rectangle(0, 0, 3900,
                1200));

            selected_level.SetPlayer(player);
            selected_level.Layers[0].addSprite(new Background(Vector2.Zero));

            ShootableWeapon w1;
            w1 = new ShootableWeapon("ak47", weapon_texture, player._Position, 0);
            player.addWeapon(w1);
            player.SetCurrentWeapon(0);

            selected_level.addEnemy(50, 5);


  
            selected_level.Load();
            Controller = new GameController(selected_level);
        }

        public Texture2D LoadTexture(String Name)
        {
            return Content.Load<Texture2D>(Name);
        }

        public void Update(GameTime gameTime)
        {
            Controller.HandleInput(graphics, prevKB, Keyboard.GetState(), gameTime);
            Camera.LookAt(player._Position);
            player.Update(gameTime);
            player.UpdateWeaponPosition();
            
            selected_level.CheckBlockCollisions();
            selected_level.CheckCoinCollisions();
            selected_level.CheckEnemyCollisions();
            selected_level.checkAmmoCollision();
            selected_level.UpdateAmmoInteraction();
            selected_level.updateWeaponPositions();

            if (player.IsBoostActive())
            {
                 selected_level.freezeEnemies(gameTime);
            }
            else
                 selected_level.walkEnemies(gameTime);



                prevKB = Keyboard.GetState();
          }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.getViewMatrix(new Vector2(1.0f)));

            for (int layer_inc = 0; layer_inc <= selected_level.GetLayers().Count() - 1; layer_inc++)
            {
                selected_level.GetLayers()[1].Draw(spriteBatch);
                Layer layer = selected_level.GetLayers()[1];

                ShootableWeapon weapon = ((ShootableWeapon)player.GetWeapon());

                if (weapon.getAmmo().Count() > 0)
                {
                    weapon.getAmmo()[0].Draw(spriteBatch);
                }
                weapon.Draw(spriteBatch);
            }

            updateHp(spriteBatch);
            spriteBatch.End();
        }

        public void updateHp(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "HP: " + selected_level.GetPlayer().GetHp(), new Vector2(10, 10), Color.Red);
        }

        public Color getBackgroundColor()
        {
            return BackgroundColor;
        }
    }
}
