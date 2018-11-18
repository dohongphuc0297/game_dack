using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_dack
{
    class Lyn : Unit
    {
        public Lyn(Game game, Sprite2D model) : base(game, model)
        {
            List<string> paths = new List<string>();
            for (int i = 1; i <= 34; i++)
            {
                paths.Add(string.Format("images/7Heroes/Lyn/Lyn_{0}", i));
            }
            model.LoadContent(game, paths, 3f);
            List<Vector2> pos = new List<Vector2>();
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(200, 180));
            pos.Add(new Vector2(220, 175));
            pos.Add(new Vector2(230, 170));
            pos.Add(new Vector2(240, 165));
            pos.Add(new Vector2(250, 160));
            pos.Add(new Vector2(260, 165));
            pos.Add(new Vector2(270, 170));
            pos.Add(new Vector2(280, 175));
            pos.Add(new Vector2(290, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            model.LoadPos(pos);
        }
    }
}
