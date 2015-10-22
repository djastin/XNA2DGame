using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jumping.Models.Core
{
    public class StartMenu
    {
        private TextureLoader _textureLoader;
        private Texture2D _cijfernariumTexture;
        private Texture2D _crackenariumTexture;
        private Texture2D _starbucksalandoriumTexture;
        private Texture2D _kloonwereldTexture;
        private Texture2D _cijfernariumTextureHighlighted;
        private Texture2D _crackenariumTextureHighlighted;
        private Texture2D StarbucksalandoriumTextureHighlighted;
        private Texture2D _kloonwereldTextureHighlighted;
        private Texture2D _gameOverButtonHighlighted;
        private Texture2D _gameOverButton;
        private Texture2D _front;
        private String _hoveredOver;
        private SpriteFont _levelsFont;
        private SpriteFont _gameOverFont;
        private Boolean _showGameOver;
        private Boolean _hoveredOverGameOverButton;

        public Dictionary<String, Rectangle> Buttonareas = new Dictionary<string, Rectangle>();
        public Boolean ShowMenu { get; set; }

        public void SetGameOver(Boolean gameOver)
        {
            this._showGameOver = gameOver;
        }
        public Boolean IsGameOver()
        {
            return _showGameOver;
        }
        public void Show(GraphicsDeviceManager graphics)
        {
            this.ShowMenu = true;
            graphics.PreferredBackBufferWidth = (int)800;
            graphics.PreferredBackBufferHeight = (int)600;
            graphics.ApplyChanges();
        }
        public void Hide(GraphicsDeviceManager graphics)
        {
            this.ShowMenu = false;
            graphics.PreferredBackBufferWidth = (int)640;
            graphics.PreferredBackBufferHeight = (int)480;
            graphics.ApplyChanges();
        }
        public void SetFonts(SpriteFont font, SpriteFont gameOverFont)
        {
            this._levelsFont = font;
            this._gameOverFont = gameOverFont;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _hoveredOverGameOverButton = false;
            if (ShowMenu)
            {
                if (_hoveredOver == "gameOverButton")
                {
                    _hoveredOverGameOverButton = true;
                }
                    switch (_hoveredOver)
                    {
                        case "danielButton":
                            spriteBatch.Draw(_starbucksalandoriumTexture, new Vector2(0, 300), Color.White);
                            spriteBatch.Draw(_cijfernariumTexture, new Vector2(400, 0), Color.White);
                            spriteBatch.Draw(_kloonwereldTexture, new Vector2(0, 0), Color.White);
                            spriteBatch.Draw(_crackenariumTexture, new Vector2(400, 300), Color.White);
                            if (!_showGameOver)
                            {
                                spriteBatch.Draw(_crackenariumTextureHighlighted, new Vector2(400, 300), Color.White);
                                spriteBatch.DrawString(_levelsFont, "Crackenarium", new Vector2(600, 450), Color.Turquoise);
                            }
                            break;
                        case "djastinButton":
                            spriteBatch.Draw(_crackenariumTexture, new Vector2(400, 300), Color.White);
                            spriteBatch.Draw(_starbucksalandoriumTexture, new Vector2(0, 300), Color.White);
                            spriteBatch.Draw(_cijfernariumTexture, new Vector2(400, 0), Color.White);
                            spriteBatch.Draw(_kloonwereldTexture, new Vector2(0, 0), Color.White);
                            if (!_showGameOver)
                            {
                                spriteBatch.Draw(_kloonwereldTextureHighlighted, new Vector2(0, 0), Color.White);
                                spriteBatch.DrawString(_levelsFont, "Kloonwereld", new Vector2(200, 150), Color.Wheat);
                            }
                            break;
                        case "gideonButton":
                            spriteBatch.Draw(_crackenariumTexture, new Vector2(400, 300), Color.White);
                            spriteBatch.Draw(_kloonwereldTexture, new Vector2(0, 0), Color.White);
                            spriteBatch.Draw(_starbucksalandoriumTexture, new Vector2(0, 300), Color.White);
                            spriteBatch.Draw(_cijfernariumTexture, new Vector2(400, 0), Color.White);
                            if (!_showGameOver)
                            {
                                spriteBatch.Draw(_cijfernariumTextureHighlighted, new Vector2(400, 0), Color.White);
                                spriteBatch.DrawString(_levelsFont, "Cijfernarium", new Vector2(433, 103), Color.Black);
                                spriteBatch.DrawString(_levelsFont, "Cijfernarium", new Vector2(430, 100), Color.Orange);
                            }
                            break;
                        case "rutgerButton":
                            spriteBatch.Draw(_crackenariumTexture, new Vector2(400, 300), Color.White);
                            spriteBatch.Draw(_kloonwereldTexture, new Vector2(0, 0), Color.White);
                            spriteBatch.Draw(_starbucksalandoriumTexture, new Vector2(0, 300), Color.White);
                            spriteBatch.Draw(_cijfernariumTexture, new Vector2(400, 0), Color.White);
                            if (!_showGameOver)
                            {
                                spriteBatch.Draw(StarbucksalandoriumTextureHighlighted, new Vector2(0, 300), Color.White);
                                spriteBatch.DrawString(_levelsFont, "Starbucksalandorium", new Vector2(3, 453), Color.Black);
                                spriteBatch.DrawString(_levelsFont, "Starbucksalandorium", new Vector2(0, 450), Color.Lime);
                            }
                            break;
                        default:
                            spriteBatch.Draw(_crackenariumTexture, new Vector2(400, 300), Color.White);
                            spriteBatch.Draw(_kloonwereldTexture, new Vector2(0, 0), Color.White);
                            spriteBatch.Draw(_starbucksalandoriumTexture, new Vector2(0, 300), Color.White);
                            spriteBatch.Draw(_cijfernariumTexture, new Vector2(400, 0), Color.White);
                            spriteBatch.DrawString(_levelsFont, "Kies een level", new Vector2(400, 500), Color.Red);
                            break;
                    }
                    spriteBatch.Draw(_front, new Vector2(0, 0), Color.White);
                if (_showGameOver)
                {
                    if (_hoveredOverGameOverButton)
                    {
                        spriteBatch.Draw(_gameOverButtonHighlighted, new Vector2(300, 300), Color.White);
                    }
                    else { spriteBatch.Draw(_gameOverButton, new Vector2(300, 300), Color.White); }
                    spriteBatch.DrawString(_gameOverFont, "Game over", new Vector2(300, 200), Color.Black);
                    spriteBatch.DrawString(_gameOverFont, "Game over", new Vector2(300, 200), Color.Red);
                }
            }
        }
        public void SetLevelHighlighted(string levelName)
        {
            _hoveredOver = levelName;
        }
        public void Initialize()
        {
            if (Buttonareas.Count() == 0)
            {
                Rectangle djastinButtonArea = new Rectangle(0, 0, 400, 300);
                Rectangle rutgerButtonArea = new Rectangle(0, 300, 400, 300);
                Rectangle gideonButtonArea = new Rectangle(400, 0, 400, 300);
                Rectangle danielButtonArea = new Rectangle(400, 300, 400, 300);
                Rectangle gameOverButtonArea = new Rectangle(300, 300, 200, 100);
                Buttonareas.Add("djastinButton", djastinButtonArea);
                Buttonareas.Add("rutgerButton", rutgerButtonArea);
                Buttonareas.Add("gideonButton", gideonButtonArea);
                Buttonareas.Add("danielButton", danielButtonArea);
                Buttonareas.Add("gameOverButton", gameOverButtonArea);
                _textureLoader = TextureLoader.GetInstance();
                _cijfernariumTexture = _textureLoader.GetTexture("startMenuCijfernarium");
                _starbucksalandoriumTexture = _textureLoader.GetTexture("startMenuStarbucks");
                _crackenariumTexture = _textureLoader.GetTexture("startMenuCrackenarium");
                _kloonwereldTexture = _textureLoader.GetTexture("startMenuKloonwereld");
                _cijfernariumTextureHighlighted = _textureLoader.GetTexture("startMenuCijfernariumHighlighted");
                StarbucksalandoriumTextureHighlighted = _textureLoader.GetTexture("startMenuStarbucksHighlighted");
                _crackenariumTextureHighlighted = _textureLoader.GetTexture("startMenuCrackenariumHighlighted");
                _kloonwereldTextureHighlighted = _textureLoader.GetTexture("startMenuKloonwereldHighlighted");
                _gameOverButtonHighlighted = _textureLoader.GetTexture("gameOverButtonHighlighted");
                _gameOverButton = _textureLoader.GetTexture("gameOverButton");
                _front = _textureLoader.GetTexture("startMenuBackgroundEmpty");
                _showGameOver = false;
            }
        }

    }
}
