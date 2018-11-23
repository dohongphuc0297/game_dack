using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_dack
{
    public class Ground : Unit
    {
        public Ground(Game game, Sprite2D model) : base(game, model)
        {
            List<string> paths = new List<string>();
            paths.Add("ground/BattleGround");
            model.LoadContent(game, paths, 4.8f);
            List<Vector2> pos = new List<Vector2>();
            pos.Add(new Vector2(100, 0));
            model.LoadPos(pos);
        }
    }
}