using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_dack
{
    public class Menu : GameControl
    {
        List<MenuItem> items = new List<MenuItem>();
        public Menu(Game game, Sprite2D model) : base(game, model)
        {
            var mnuItem = CreateItems(1, "new_game");
            mnuItem.Click += NewGame_Click;
            mnuItem.Hover += MnuItem_Hover;
            items.Add(mnuItem);
            mnuItem = CreateItems(2, "save");
            mnuItem.Click += Save_Click;
            mnuItem.Hover += MnuItem_Hover;
            items.Add(mnuItem);
            mnuItem = CreateItems(3, "load");
            mnuItem.Click += Load_Click;
            mnuItem.Hover += MnuItem_Hover;
            items.Add(mnuItem);
            mnuItem = CreateItems(4, "options");
            mnuItem.Click += Options_Click;
            mnuItem.Hover += MnuItem_Hover;
            items.Add(mnuItem);
            mnuItem = CreateItems(5, "exit");
            mnuItem.Click += Exit_Click;
            mnuItem.Hover += MnuItem_Hover;
            items.Add(mnuItem);
        }
        private void MnuItem_Hover(object sender, EventArgs e)
        {
            this._game.Window.Title = "HOVER HOVER HOVERINGGGG YEAHHH~~~";
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            this._game.Window.Title = "You clicked on New Game Button.";
        }

        private void Save_Click(object sender, EventArgs e)
        {
            this._game.Window.Title = "You clicked on Save Button.";
        }

        private void Load_Click(object sender, EventArgs e)
        {
            this._game.Window.Title = "You clicked on Load Button.";
        }
        private void Options_Click(object sender, EventArgs e)
        {
            this._game.Window.Title = "You clicked on Options Button.";
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            this._game.Window.Title = "You clicked on Exit Button.";
        }

        private MenuItem CreateItems(int index, String str)
        {
            float itemHeight = 40f;
            float x = this._model.Left + 20;
            float y = this._model.Top + index * itemHeight;
            var sprite = new Sprite2D(x, y);
            List<string> paths = new List<string>();
            paths.Add("menu/"+ str + "_1");
            sprite.LoadContent(this._game, paths, 1f);
            return new MenuItem(this._game, sprite);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Draw(gameTime, spriteBatch);
            }
        }
    }
}