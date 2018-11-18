using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_dack
{
    public class Sprite2D
    {
        float _left;
        float _top;
        float _width;
        float _height;
        List<Texture2D> _images = new List<Texture2D>();
        List<Vector2> _pos = new List<Vector2>();
        int imageIndex = 0;
        int state;
        Vector2 _scale;
        private float timeElapsed;
        private float timeToUpdate = 0.1f;
        private Boolean _isSelected = false;
        private Color _color = Color.White;

        public float Width { get => _width; set => _width = value; }
        public float Height { get => _height; set => _height = value; }
        public float Top { get => _top; set => _top = value; }
        public float Left { get => _left; set => _left = value; }

        public Sprite2D(float left, float top)
        {
            this._left = left;
            this._top = top;
            this.state = 0;
        }

        public void LoadContent(Game game, List<string> content, float scale)
        {
            for (int i = 0; i < content.Count; i++) 
            {
                _images.Add(game.Content.Load<Texture2D>(content[i]));
            }
            if (content.Count > 0)
            {
                _scale = new Vector2(scale, scale);
                _width = scale * _images[0].Width;
                _height = scale * _images[0].Height;
            }
        }

        public void LoadPos(List<Vector2> content)
        {
            for (int i = 0; i < content.Count; i++)
            {
                _pos.Add(content[i]);
            }
            
        }

        public void UnloadContent()
        {
            this._images.Clear();
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (imageIndex < _images.Count - 1)
                    imageIndex++;

                else imageIndex = 0;
            }
        }

        public Boolean IsSelected(MouseState ms)
        {
            if(ms.Position.X>= _left && ms.Position.X<= _left + _width)
            {
                if(ms.Position.Y>=_top && ms.Position.Y<= _top + _height)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean IsPoint(Vector2 pos)
        {
            if (pos.X >= _left && pos.X <= _left + _width && pos.X + _width >= _left)
            {
                if (pos.Y <= _top + _height && pos.Y + _height >= _top)
                {
                    return true;
                }
            }
            return false;
        }

        public void Select()
        {
            _isSelected = !_isSelected;
            if (_isSelected) _color = Color.Yellow;
            else _color = Color.White;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_images.Count > 0 && _pos.Count > 0) 
            {
                spriteBatch.Draw(_images[imageIndex], position: _pos[imageIndex], scale: _scale, color: _color);
            }
        }
    }
}
