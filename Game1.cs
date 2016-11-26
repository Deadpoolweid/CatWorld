using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private Texture2D tOpenCat;

        private float speed = 2f;

        private Vector2 FontPosition;

        List<Song> Songs = new List<Song>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 150);
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

            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

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
            tOpenCat = Content.Load<Texture2D>("OpenCat");
            tTrain = Content.Load<Texture2D>("train");
            tSpace = Content.Load<Texture2D>("Space");

            Songs.Add(Content.Load<Song>("cats"));
            

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

        private bool isPlaying = false;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //position.X += speed;

            //if (position.X > Window.ClientBounds.Width - CatSize.Width || position.X < 0)
            //    speed *= -1;

            if (keyboardState.IsKeyDown(Keys.Left))
                position.X -= speed;
            if (keyboardState.IsKeyDown(Keys.Right))
                position.X += speed;
            if (keyboardState.IsKeyDown(Keys.Up))
                position.Y -= speed;
            if (keyboardState.IsKeyDown(Keys.Down))
                position.Y += speed;

            Random r = new Random(DateTime.Now.Millisecond);

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (isPlaying)
                {
                    MediaPlayer.Stop();
                }
                else
                {
                    MediaPlayer.Play(Songs[r.Next(0,Songs.Count)]);
                }
                isPlaying = !isPlaying;
            }

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

            int w = Window.ClientBounds.Width;
            int h = Window.ClientBounds.Height;

            int halfY = Window.ClientBounds.Height / 2;
            int halfX = Window.ClientBounds.Width / 2;

            spriteBatch.Begin();

            var cats = new List<Vector2>();
            var trains = new List<Vector2>();

            int numberOfCats = (int)(w/CatSize.Width);
            int j = 0;
            while (numberOfCats>0)
            {
                for (int i = 0; i < numberOfCats; i++)
                {
                    cats.Add(new Vector2(i * CatSize.Width + j*(CatSize.Width/2), h - CatSize.Height - (CatSize.Height/2)*j));
                }
                numberOfCats--;
                j++;
            }



            //rotationAngle += 0.01f;
            
            spriteBatch.Draw(tSpace, new Vector2(0,0), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);

            

            //cats.Add(position);

            cats.Reverse();




            foreach (var cat in cats)
            {
                bool IsCatSinging = r.Next(1,100) > 50;
                spriteBatch.Draw(IsCatSinging?tOpenCat:tCat, cat, null, Color.White, rotationAngle, Vector2.Zero, scalePicture, SpriteEffects.None, 0);
            }

            // Строка Hello World

            string output = k++.ToString();

            // Отыскать центр строки

            Vector2 FontOrigin = spriteFont.MeasureString(output) / 2;

            // Прорисовка строки

            spriteBatch.DrawString(spriteFont, output, FontPosition, Color.Purple, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);


            spriteBatch.End();

            base.Draw(gameTime);
        }

        Random r = new Random(DateTime.Now.Millisecond);


        private int k = 0;

        private float rotationAngle = 0;

        private bool Forward = true;

        private static float scalePicture = 0.1f;

        static float CatScale = 0.1f;
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
