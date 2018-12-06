using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;

namespace game_dack
{
    class Assassin : Unit
    {

        public Assassin(Game game, Sprite2D model) : base(game, model)
        {
            SwordBattle(game, model);
        }

        public void SwordBattle(Game game, Sprite2D model)
        {
            List<string> paths = new List<string>();
            for (int i = 1; i <= 35; i++)
            {
                paths.Add(string.Format("unit/PlayableUnit/Assassin/attack/sword/{0}", i));
            }
            model.LoadContent(game, paths, 3f);
            List<Vector2> pos = new List<Vector2>();
            int y = 200;
            int x = 400;
            int x1 = 250;
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x1, y));
            pos.Add(new Vector2(x-20, y));
            pos.Add(new Vector2(x-20, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            model.LoadPos(pos);
        }
    }
}
