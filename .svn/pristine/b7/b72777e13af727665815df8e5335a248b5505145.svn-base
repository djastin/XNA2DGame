using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumping.Models
{
    public class TextureLoader
    {
        private Dictionary<String, Texture2D> _textures;
        private static TextureLoader _instance;

        private TextureLoader()
        {
            _textures = new Dictionary<string, Texture2D>();
        }

        public static TextureLoader GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TextureLoader();
            }
            return _instance;
        }

        public void AddTexture(String Key, Texture2D Texture)
        {
            _textures.Add(Key, Texture);
        }

        public Dictionary<String, Texture2D> GetTextures()
        {
            return _textures;
        }

        public Texture2D GetTexture(String key)
        {
            key += ".xnb";
            if (_textures.ContainsKey(key))
                return _textures[key];
            return null;
        }
    }
}
