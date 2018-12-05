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
            SwordBattle(game, model);
            BowBattle(game, model);
        }

        public void SwordBattle(Game game, Sprite2D model)
        {
            List<string> paths = new List<string>();
            for (int i = 1; i <= 34; i++)
            {
                paths.Add(string.Format("unit/7Heroes/Lyn/attack/sword/{0}", i));
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
            pos.Add(new Vector2(220, 160));
            pos.Add(new Vector2(230, 140));
            pos.Add(new Vector2(240, 120));
            pos.Add(new Vector2(250, 100));
            pos.Add(new Vector2(260, 120));
            pos.Add(new Vector2(270, 150));
            pos.Add(new Vector2(280, 170));
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

        public void BowBattle(Game game, Sprite2D model)
        {
            List<string> paths = new List<string>();
            for (int i = 1; i <= 15; i++)
            {
                paths.Add(string.Format("unit/7Heroes/Lyn/attack/bow/{0}", i));
            }
            model.LoadContent(game, paths, 3f);
            List<Vector2> pos = new List<Vector2>();
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
            pos.Add(new Vector2(300, 180));
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
