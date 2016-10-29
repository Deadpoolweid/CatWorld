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
        private Texture2D texture;

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

            texture = Content.Load<Texture2D>("Cat");

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
            float scale = 0.04f;

            var CatSize = new Size((texture.Width*scale),(texture.Height * scale));

            

            spriteBatch.Begin();

            var vectors = new List<Vector2>();

            int catsCount = (int)(Window.ClientBounds.Width/CatSize.Width);
            float distanceX = (int) ((Window.ClientBounds.Width)/catsCount);

            for (int i = 0; i < catsCount; i++)
            {
                vectors.Add(new Vector2(i*(distanceX) ,Window.ClientBounds.Height - CatSize.Height));
            }




            //catsCount = (int) ((Window.ClientBounds.Width/2)/CatSize.Width);
            catsCount /= 2;
            //distanceX = (int) ((Window.ClientBounds.Width/2)/(catsCount));
            float distanceY = (int)((Window.ClientBounds.Height - CatSize.Height*2+CatSize.Height/2) / (catsCount));


            for (int i = 1; i < catsCount; i++)
            {
                for (int k = catsCount; k > 0; k--)
                {
                    if (catsCount - k == i)
                    {
                        vectors.Add(new Vector2(i*distanceX, k*distanceY));
                    }
                }
            }


            //for (int i = catsCount-2; i > 0; i--)
            //{
            //    vectors.Add(new Vector2((Window.ClientBounds.Width/2)+i*distanceX,(i)*distanceY));
            //}

            for (int i = catsCount-1; i > 0; i--)
            {
                for (int k = catsCount; k > 0; k--)
                {
                    if (k == i)
                    {
                        vectors.Add(new Vector2(i * distanceX+(Window.ClientBounds.Width/2)-(CatSize.Width/2), k * distanceY));
                    }
                }
            }


            vectors.Add(new Vector2(Window.ClientBounds.Width/2-CatSize.Width/2,0));
            //vectors.Add(new Vector2(0,Window.ClientBounds.Height - CatSize.Height));
            //vectors.Add(new Vector2(Window.ClientBounds.Width - CatSize.Width,Window.ClientBounds.Height - CatSize.Height));


            foreach (var vector2 in vectors)
            {
                spriteBatch.Draw(texture,vector2,null,Color.White,0,Vector2.Zero,scale, SpriteEffects.None, 0);

            }
            //spriteBatch.Draw(texture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 0.06f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static float k(float width, float height)
        {
            return height/width;
        }

        public static float f(float x, float k)
        {
            return k * x;
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
