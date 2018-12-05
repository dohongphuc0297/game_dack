using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_dack
{
    public class Cloud : Unit
    {
        public Cloud(Game game, Sprite2D model) : base(game, model)
        {
            List<string> paths = new List<string>();
            paths.Add("ground/BattleGround");
            model.LoadContent(game, paths, 3f);
            List<Vector2> pos = new List<Vector2>();
            pos.Add(new Vector2(0, 0));
            model.LoadPos(pos);
        }
    }
}