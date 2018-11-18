using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_dack
{
    public class Unit : GameVisibleEntity
    {
        protected Game _game;

        public Unit(Game game, Sprite2D model)
        {
            this._game = game;
            this._model = model;
        }
    }
}