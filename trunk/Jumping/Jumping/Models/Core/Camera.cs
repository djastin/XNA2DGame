using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jumping.Models
{
    public class Camera
    {
        private Vector2 _position;
        private Vector2 _origin;
        private float _rotation, _zoom;
        private Viewport _view;
        private Rectangle? _limits;
        private const float _cameraPositionHeight = 4.35f, _cameraPositionWidth = 2f;
         
        public Camera(Viewport viewport)
        {
            _view = viewport;
            _origin = new Vector2(viewport.Width, viewport.Height);
            _zoom = 1.0f;
            _rotation = 0.0f;
        }

        public Rectangle? Limits
        {
            get { return _limits; }
            set
            {
                if (value != null)
                {
                    _limits = new Rectangle
                    {
                        X = value.Value.X,
                        Y = value.Value.Y,

                        Width = System.Math.Max(_view.Width, 4500),
                        Height = System.Math.Max(_view.Height, 6000)
                    };
                   
                }
                else
                    _limits = null;
            }
        }

        public Vector2 Position
        {
            get { return _position; }
            set 
            { 
                _position = value;

                if (Limits != null && _zoom == 1.0f && _rotation == 0.0f)
                {
                    _position.X = MathHelper.Clamp(_position.X, Limits.Value.X, Limits.Value.X + Limits.Value.Width - _view.Width);
                    _position.Y = MathHelper.Clamp(_position.Y, Limits.Value.Y, Limits.Value.Y + Limits.Value.Height - _view.Height);
                }
            }
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-_origin, 0.0f)) *
                Matrix.CreateRotationZ(_rotation) *
                Matrix.CreateScale(_zoom, _zoom, 1) *
                Matrix.CreateTranslation(new Vector3(_origin, 0.0f));
        }

        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(_view.Width / _cameraPositionWidth, _view.Height - (_view.Height) / _cameraPositionHeight);
        }
    }
}
