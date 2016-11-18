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
        private SpriteFont spriteFont;
        private Texture2D tCat;
        private Texture2D tTrain;
        private Texture2D tSpace;

        private float speed = 2f;

        private Vector2 FontPosition;

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
            tSpace = Content.Load<Texture2D>("Space");


            spriteFont = Content.Load<SpriteFont>("Courier New");

            CatSize = new Size((tCat.Width * CatScale), (tCat.Height * CatScale));


            FontPosition = new Vector2(100,10);

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

            position.X += speed;

            if (position.X > Window.ClientBounds.Width - CatSize.Width || position.X < 0)
                speed *= -1;

            base.Update(gameTime);
        }

        Vector2 position = Vector2.Zero;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            var TrainSize = new Size(tTrain.Width * TrainScale, tTrain.Height * TrainScale);

            int halfY = Window.ClientBounds.Height / 2;
            int halfX = Window.ClientBounds.Width / 2;

            spriteBatch.Begin();

            var cats = new List<Vector2>();
            var trains = new List<Vector2>();


            rotationAngle += 0.01f;

            if (Forward)
            {
                scalePicture += 0.002f;
                if (scalePicture > 0.5f)
                {
                    scalePicture = CatScale;
                }
            }
            else
            {
                scalePicture -= 0.001f;
            }

            Forward = !Forward;
            
            spriteBatch.Draw(tSpace, new Vector2(0,0), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);

            cats.Add(position);

            foreach (var cat in cats)
            {
                spriteBatch.Draw(tCat, cat, null, Color.White, rotationAngle, Vector2.Zero, scalePicture, SpriteEffects.None, 0);
            }

            // Строка Hello World

            string output = rotationAngle.ToString();

            // Отыскать центр строки

            Vector2 FontOrigin = spriteFont.MeasureString(output) / 2;

            // Прорисовка строки

            spriteBatch.DrawString(spriteFont, output, FontPosition, Color.Purple, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);


            spriteBatch.End();

            base.Draw(gameTime);
        }

        private float rotationAngle = 0;

        private bool Forward = true;

        private static float scalePicture = 0.02f;

        static float CatScale = 0.02f;
        static float TrainScale = 0.3f;

        private Size CatSize;
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
