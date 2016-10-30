using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;

namespace CatWorld
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D tCat;
        private Texture2D tTrain;

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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tCat = Content.Load<Texture2D>("Cat");
            tTrain = Content.Load<Texture2D>("train");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            // TODO: Add your update logic here

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
            float CatScale = 0.02f;
            float TrainScale = 0.3f;

            var CatSize = new Size((tCat.Width*CatScale),(tCat.Height * CatScale));
            var TrainSize = new Size(tTrain.Width*TrainScale,tTrain.Height * TrainScale);

            int halfY = Window.ClientBounds.Height/2;
            

            spriteBatch.Begin();

            var cats = new List<Vector2>();
            var trains = new List<Vector2>();


            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    cats.Add(new Vector2((85 * TrainScale) + ((TrainSize.Width / 4.75f) * i) + j*(TrainSize.Width), halfY - (TrainScale)));
                }

                trains.Add(new Vector2(j*TrainSize.Width, halfY - (35 * TrainScale) + CatSize.Height));
            }


            Random r = new Random();



            foreach (var train in trains)
            {
                Color color = new Color(r.Next(255), r.Next(255), r.Next(255));
                spriteBatch.Draw(tTrain, train, null, color, 0, Vector2.Zero, TrainScale, SpriteEffects.None, 0);
            }

            

            foreach (var cat in cats)
            {
                spriteBatch.Draw(tCat,cat,null,Color.White,0,Vector2.Zero,CatScale, SpriteEffects.None, 0);
            }



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public class Size
    {
        public Size(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public float Width { get; set; }

        public float Height { get; set; }


    }
}
