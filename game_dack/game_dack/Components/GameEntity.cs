using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_dack
{
    public abstract class GameEntity
    {
        protected Sprite2D _model;
        public virtual void Update(GameTime gameTime)
        {
            this._model.Update(gameTime);
        }

        public virtual void UnloadContent()
        {
            this._model.UnloadContent();
        }
    }
}