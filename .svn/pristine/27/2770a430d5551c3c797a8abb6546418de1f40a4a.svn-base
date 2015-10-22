using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Jumping.Models.Core;

namespace Jumping.Views
{
    public class ScreenManager
    {
        public static ScreenManager instance;
        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }
        XmlManager<GameScreen> xmlGameScreenManager;

        GameScreen currentScreen, newScreen;
        public GraphicsDevice graphicsDevice;
        public SpriteBatch spriteBatch;

        public Image image;

        private ScreenManager()
        {
            Dimensions = new Vector2(640, 480);
            currentScreen = new MenuScreen();
            xmlGameScreenManager = new XmlManager<GameScreen>();
            xmlGameScreenManager.Type = currentScreen.Type;
            //currentScreen = xmlGameScreenManager.Load("Data/sprites_level1.xml");
        }

        public static ScreenManager GetInstance()
        {
            if (instance == null)
            {
                //XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
               // instance = xml.Load("Data/ScreenManager.xml");
                instance = new ScreenManager();
            }
            return instance;
        }

        public void ChangeScreens(string screenName)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("YoutubeRPG." +screenName));
        }

        void Transition()
        {

        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }
    }
}
