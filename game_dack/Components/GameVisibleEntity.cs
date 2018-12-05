using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_dack
{
    public abstract class GameVisibleEntity : GameEntity
    {
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this._model.Draw(gameTime, spriteBatch);
        }

        public virtual bool IsSelected(MouseState ms)
        {
            return false;
        }

        public void Select()
        {
        }
    }
}