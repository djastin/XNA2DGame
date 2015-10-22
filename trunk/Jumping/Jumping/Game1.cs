using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Jumping.Models;
using Jumping.Controllers;
using Jumping.Models.Core;
using Jumping.Models.Graphics;
using Jumping.Models.Sprites;
using Jumping.Models.Features;
using Jumping.Models.Interfaces;
using System.IO;
using System.Diagnostics;
using Jumping.Enumerations;

namespace Jumping
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState _prevKB;
        private XmlManager<Level> _xmlManager;
        private Level _selectedLevel;
        private SpriteFont _textFont, _deadFont;
        private GameController _controller;
        private String _xmlPath;
        private TextureLoader _textureLoader;
        private FeatureLoader _featureLoader;
        private StartMenu _startMenu;
        private MouseState _mouseStatePrevious;
        private readonly TimeSpan _warningTime = TimeSpan.FromSeconds(30);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _featureLoader = FeatureLoader.GetInstance();
            _textureLoader = TextureLoader.GetInstance();
            _startMenu = new StartMenu();
            _textFont = Content.Load<SpriteFont>("Fonts/Font");
            _deadFont = Content.Load<SpriteFont>("Fonts/DeadFont");

            SpriteFont levelsFont = Content.Load<SpriteFont>("Fonts/levelsFont"); 
            SpriteFont gameOverFont = Content.Load<SpriteFont>("Fonts/gameOverFont"); 
            _startMenu.SetFonts(levelsFont, gameOverFont: gameOverFont); 

            LoadListContent("Starbucksalandorium");
            LoadListContent("Kloonwereld");
            LoadListContent("Crackenarium");
            LoadListContent("Gideon");
            LoadListContent("Startmenu");
            
            _startMenu.Initialize();

            base.Initialize();
        }

        public void LoadListContent(string contentFolder)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Content.RootDirectory + "/" + contentFolder);

            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException();

            FileInfo[] files = directoryInfo.GetFiles("*.*");

            foreach (FileInfo file in files)
            {
                if (!_textureLoader.GetTextures().ContainsKey(file.Name))
                {
                    string textureKey = Path.GetFileNameWithoutExtension(file.Name);

                    Texture2D texture = Content.Load<Texture2D>(contentFolder + "/" + textureKey);
                    _textureLoader.AddTexture(file.Name, texture);
                }
            }
        }

        private void LoadFeatures()
        {
            Weapon gun1, sword1, tazer;
            gun1 = new ShootableWeapon();
            gun1.TextureName = "gun";
            gun1.Initialize();

            sword1 = new Weapon();
            sword1.TextureName = "sword";
            sword1.Initialize();

            tazer = new Weapon();
            tazer.TextureName = "tazer";
            tazer.Initialize();

            IStrategyBehavior curiousStrategy, survivalStrategy, carefullStrategy;
            curiousStrategy = new CuriousStrategy();
            survivalStrategy = new SurvivalStrategy();
            carefullStrategy = new CarefullStrategy();

            IAttackBehavior Throwattack;
            Throwattack = new ThrowAttack();

            ((ThrowAttack)Throwattack).SetLevel(_selectedLevel);
            _featureLoader.AddAttack("ThrowAttack", Throwattack);

            IAttackBehavior headRollAttack = new HeadRollAttack();
            _featureLoader.AddAttack("headroll", headRollAttack);

            IAttackBehavior instaAttack = new InstaAttack();
            _featureLoader.AddAttack("InstaAttack", instaAttack);
            ((InstaAttack)instaAttack).Initialize();

            IAttackBehavior minonsAttack = new MinionAttack();
            _featureLoader.AddAttack("MinionsAttack", minonsAttack);

            IBoostBehavior meditationBoost;
            meditationBoost = new MeditationBoost(_textureLoader.GetTexture("meditationWings"));
            _featureLoader.AddBoost("MeditationBoost", meditationBoost);


            IBoostBehavior drinkBoost;
            drinkBoost = new DrinkBoost();
            _featureLoader.AddBoost("DrinkBoost", drinkBoost);


            IBoostBehavior calculatorBoost;
            calculatorBoost = new CalculatorBoost(_textureLoader.GetTexture("Boost"));
            _featureLoader.AddBoost("CalcBoost", calculatorBoost);

            _featureLoader.addStrategy("curious", curiousStrategy);
            _featureLoader.addStrategy("survival", survivalStrategy);
            _featureLoader.addStrategy("carefull", carefullStrategy);
            _featureLoader.AddWeapon("gun", gun1);
            _featureLoader.AddWeapon("sword", sword1);
            _featureLoader.AddWeapon("tazer", tazer);
        }

        private void LoadFeatureLevels()
        {
            foreach (IAttackBehavior attackBehavior in FeatureLoader.GetInstance().GetAttacks().Values)
            {
                if (attackBehavior is ThrowAttack)
                {
                    ((ThrowAttack)attackBehavior).SetLevel(this._selectedLevel);
                }
            }
        }


        private void CheckPlayerClicked() 
        { 
            MouseState _mouseStateCurrent = Mouse.GetState(); 
            var mousePosition = new Point (_mouseStateCurrent.X, _mouseStateCurrent.Y);

            foreach(KeyValuePair<String, Rectangle> buttonArea in _startMenu.Buttonareas) 
            { 
                if(buttonArea.Value.Contains(mousePosition) && _startMenu.ShowMenu) 
                { 
                    _startMenu.SetLevelHighlighted(buttonArea.Key); 

                    if(_mouseStateCurrent.LeftButton == ButtonState.Released && _mouseStatePrevious.LeftButton == ButtonState.Pressed) 
                    { 
                        if(buttonArea.Key == "gameOverButton") 
                            _startMenu.SetGameOver(false); 
                        else
                        { 
                            if(!_startMenu.IsGameOver()) 
                            { 
                                _selectedLevel = new Level(); 
                                _xmlManager = new XmlManager<Level>(); 
                                _xmlManager.Type = _selectedLevel.GetType(); 
                                
                                switch(buttonArea.Key) 
                                { 
                                    case"danielButton": 
                                        _xmlPath = "Data/Crackenarium.xml"; 
                                        LoadContent();
                                        LoadFeatureLevels();
                                        break; 
                                    case"djastinButton": 
                                        _xmlPath = "Data/Kloonwereld.xml"; 
                                        LoadContent();
                                        LoadFeatureLevels();
                                        break; 
                                    case"gideonButton": 
                                        _xmlPath = "Data/Cijfernarium.xml"; 
                                        LoadContent();
                                        LoadFeatureLevels();
                                        break; 
                                    case"rutgerButton": 
                                        _xmlPath = "Data/Starbucksalandorium.xml"; 
                                        LoadContent(); 
                                        LoadFeatureLevels();
                                        break; 
                                } 
                            } 
                        } 
                    } 
                } 
            } 

            _mouseStatePrevious = _mouseStateCurrent;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            if (_selectedLevel == null)
                _startMenu.Show(_graphics);
            else
            {
                _startMenu.Hide(_graphics); 
                _selectedLevel = _xmlManager.Load(_xmlPath);


                LoadFeatures();
                _selectedLevel.Load();

                _controller = new GameController(_selectedLevel);
            }
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            CheckPlayerClicked();

            if (_selectedLevel != null)
            {
                _controller.HandleInput(_graphics, _prevKB, Keyboard.GetState(), gameTime);

                if (_selectedLevel.EnteredRoom != null)
                {
                    if (_selectedLevel.EnteredRoom.IsPresent)
                        _selectedLevel.EnteredRoom.Update(gameTime);
                    else
                        _selectedLevel.Update(gameTime);
                }
                else
                    _selectedLevel.Update(gameTime);


                if(!_selectedLevel.Player.IsAlive())
                {
                    _startMenu.SetGameOver(true);
                    _startMenu.Show(_graphics);
                    _selectedLevel = null;
                } 

                _prevKB = Keyboard.GetState();
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);

            if (_startMenu.ShowMenu)
            {
                SpriteBatch batch = new SpriteBatch(_graphics.GraphicsDevice);
                batch.Begin();
                _startMenu.Draw(batch);
                batch.End();
            }
            if (_selectedLevel != null && !_startMenu.ShowMenu)
            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                _selectedLevel.GetCamera().GetViewMatrix(new Vector2(1.0f)));

                _selectedLevel.Draw(_spriteBatch, gameTime);

                UpdateScreenText(_spriteBatch);

                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        private void UpdateScreenText(SpriteBatch spriteBatch)
        {
            Player player = _selectedLevel.Player;
            String weaponName = player.GetWeapon().TextureName;

            spriteBatch.DrawString(_textFont, "Selected Weapon: " + weaponName.Substring(0, 1).ToUpper() + weaponName.Substring(1)
                , _selectedLevel.GetCamera().Position + new Vector2(10, 10), Color.Green);

            spriteBatch.DrawString(_textFont, "HP: " + player.GetHp(), _selectedLevel.GetCamera().Position + new Vector2(10, 35)
                , Color.Green);

            spriteBatch.DrawString(_textFont, "Total items: " + player.CollectedItems.Count + " of " + _selectedLevel.GetTotalCollectableItems(), 
                _selectedLevel.GetCamera().Position + new Vector2(10, 60), Color.Green);

            if (!player.IsAlive())
                spriteBatch.DrawString(_deadFont, "Game Over!", _selectedLevel.GetCamera().Position + new Vector2(260, 240), Color.Red);

            if (_selectedLevel.GetMovableObjects().Count == 1)
                spriteBatch.DrawString(_textFont, "You've won!", _selectedLevel.GetCamera().Position + new Vector2(180, 240), Color.Red);
            if (_selectedLevel.Name == "Cijfernarium")
            {
                DrawRemainingTime();
            }
        }

        private void DrawRemainingTime()
        {
            string timeString = "Time: " + _selectedLevel.GetRemainingTime().Minutes.ToString("00") + ":" + _selectedLevel.GetRemainingTime().Seconds.ToString("00");
            Color timeColor;
            if (_selectedLevel.GetRemainingTime() > _warningTime || (int)_selectedLevel.GetRemainingTime().TotalSeconds % 2 == 0)
            {
                timeColor = Color.Yellow;
            }
            else
            {
                timeColor = Color.Red;
            }
            _spriteBatch.DrawString(_textFont, timeString, _selectedLevel.GetCamera().Position + new Vector2(10, 75), timeColor);

        }
    }
}
