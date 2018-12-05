using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_dack
{
    public class GameControl : GameVisibleEntity
    {
        protected Game _game;

        public GameControl(Game game, Sprite2D model)
        {
            this._game = game;
            this._model = model;
        }
    }
}