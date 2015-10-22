using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;

namespace Jumping.Models.Graphics
{
    [XmlRoot(ElementName = "Animation2")]
    public class Animation2
    {
        public String TextureName { get; set; }
        [XmlIgnore]
        public Texture2D Texture { get; set; }
        public float FrameTime { get; set; }
        public bool IsLooping { get; set; }

        [XmlIgnore]
        public int FrameCount
        {
            get { return FrameWidth / FrameHeight; }
        }
        [XmlIgnore]
        public int FrameWidth
        {
            get { return Texture.Width; }
        }
        [XmlIgnore]
        public int FrameHeight
        {
            get { return Texture.Height; }
        }

        public void Initialize()
        {
            Texture = TextureLoader.GetInstance().GetTexture(TextureName);
        }
    }

}
