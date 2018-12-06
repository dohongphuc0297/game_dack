using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_dack
{
    class Archer : Unit
    {
        public Archer(Game game, Sprite2D model) : base(game, model)
        {
            //BowBattle(game, model);
            BallistaBattle(game, model);
        }

        public void BallistaBattle(Game game, Sprite2D model)
        {
            List<string> paths = new List<string>();
            for (int i = 1; i <= 5; i++)
            {
                paths.Add(string.Format("unit/PlayableUnit/Archer/attack/ballista/{0}", i));
            }
            model.LoadContent(game, paths, 3f);
            List<Vector2> pos = new List<Vector2>();
            int x = 240;
            int y = 150;
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y-100));
            pos.Add(new Vector2(x, y-100));
            model.LoadPos(pos);
        }

        public void BowBattle(Game game, Sprite2D model)
        {
            List<string> paths = new List<string>();
            for (int i = 1; i <= 17; i++)
            {
                paths.Add(string.Format("unit/PlayableUnit/Archer/attack/bow/{0}", i));
            }
            model.LoadContent(game, paths, 3f);
            List<Vector2> pos = new List<Vector2>();
            int y = 200;
            int x = 480;
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
            pos.Add(new Vector2(x, y));
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
