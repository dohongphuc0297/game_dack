using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace game_dack
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<GameEntity> Units = new List<GameEntity>();
        Random random = new Random();
        Boolean IsClicked = false;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            this.IsMouseVisible = true;
        }

        private GameVisibleEntity CreateUnit(Vector2 position)
        {
            var sprite = new Sprite2D(position.X, position.Y);
            List<string> paths = new List<string>();
            for (int i = 1; i <= 34; i++)
            {
                paths.Add(string.Format("images/7Heroes/Lyn/Lyn_{0}", i));
            }
            sprite.LoadContent(this, paths, 3f);
            return new Lyn(this, sprite);
        }

        private GameVisibleEntity CreateMap(Vector2 position)
        {
            var sprite = new Sprite2D(position.X, position.Y);
            List<string> paths = new List<string>();
            paths.Add("ground/BattleGround");
            sprite.LoadContent(this, paths, 3f);
            return new Cloud(this, sprite);
        }

        private GameVisibleEntity CreateMenu(Vector2 position)
        {
            var sprite = new Sprite2D(position.X, position.Y);
            List<string> paths = new List<string>();
            paths.Add("menu/menu");
            sprite.LoadContent(this, paths, 1f);
            return new Menu(this, sprite);
        }

        private Vector2 GetRndPos()
        {
            if (Units.Count <= 1) return new Vector2(0, 0);
            Vector2 pos = new Vector2();
            pos.X = random.Next(0, graphics.GraphicsDevice.Viewport.Width - 184);
            pos.Y = random.Next(0, graphics.GraphicsDevice.Viewport.Height - 268);

            return pos;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Units.Add(CreateMap(new Vector2(0, 0)));
            Units.Add(CreateUnit(new Vector2(250, 180)));
            //Units.Add(CreateUnit(GetRndPos()));
            //Units.Add(CreateUnit(GetRndPos()));
            //Units.Add(CreateUnit(GetRndPos()));
            //Units.Add(CreateUnit(GetRndPos()));
            //Units.Add(CreateUnit(GetRndPos()));
            //Units.Add(CreateMenu(new Vector2(250, 100)));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            for (int i = 0; i < Units.Count; i++)
            {
                Units[i].UnloadContent();
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState ms = Mouse.GetState();
            // TODO: Add your update logic here
            for (int i = 0; i < Units.Count; i++)
            {
                Units[i].Update(gameTime);
            }

            if (!IsClicked && ms.LeftButton == ButtonState.Pressed)
            {
                IsClicked = true;
                for (int i = Units.Count - 1; i >= 0; i--)
                {
                    if (Units[i] is GameVisibleEntity)
                    {
                        GameVisibleEntity temp = Units[i] as GameVisibleEntity;
                        if (temp.IsSelected(ms))
                        {
                            temp.Select();
                            break;
                        }
                    }
                }
            }
            if (ms.LeftButton == ButtonState.Released) IsClicked = false;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront);
            for (int i = 0; i < Units.Count; i++)
            {
                if (Units[i] is GameVisibleEntity)
                {
                    ((GameVisibleEntity)Units[i]).Draw(gameTime, spriteBatch);
                }

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
