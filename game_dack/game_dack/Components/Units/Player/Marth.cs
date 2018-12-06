using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_dack.Components.Units.Player
{
    class Marth : Unit
    {
        public Marth(Game game, Sprite2D model) : base(game, model)
        {
            SwordBattle(game, model);
        }

        public void SwordBattle(Game game, Sprite2D model)
        {
            List<string> paths = new List<string>();
            for (int i = 1; i <= 71; i++)
            {
                paths.Add(string.Format("unit/7Heroes/Marth/attack/sword/{0}", i));
            }
            model.LoadContent(game, paths, 3f);
            List<Vector2> pos = new List<Vector2>();
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(600, 270));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(500, 300));
            pos.Add(new Vector2(520, 300));
            pos.Add(new Vector2(540, 300));
            pos.Add(new Vector2(560, 300));
            pos.Add(new Vector2(580, 300));
            pos.Add(new Vector2(600, 300));
            pos.Add(new Vector2(620, 300));
            pos.Add(new Vector2(640, 300));
            pos.Add(new Vector2(660, 300));
            pos.Add(new Vector2(680, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            pos.Add(new Vector2(700, 300));
            model.LoadPos(pos);
        }
    }
}
