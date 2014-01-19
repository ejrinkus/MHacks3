#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace BASSProto1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState currKeyState;
        KeyboardState prevKeyState;
        Music music;
        float low;
        float high;

        public Game1()
            : base()
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
            base.Initialize();
            low = 0;
            high = 16000;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create the music object
            music = new Music("Content/cinema.mp3");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            music.unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            currKeyState = Keyboard.GetState();

            // Exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || currKeyState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Play/Pause
            if (currKeyState.IsKeyDown(Keys.Space) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.Space)))
            {
                music.playPause();
            }

            // Flanger
            if (currKeyState.IsKeyDown(Keys.F) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.F)))
            {
                music.toggleFlanger();   
            }

            // Reverb (TODO: make it less jarring on the bass hits...)
            if (currKeyState.IsKeyDown(Keys.R) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.R)))
            {
                music.toggleReverb();
            }

            // Filter
            if (currKeyState.IsKeyDown(Keys.B) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.B)))
            {
                music.toggleFilter();
            }

            // Chorus
            if (currKeyState.IsKeyDown(Keys.C) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.C)))
            {
                music.toggleChorus();   
            }

            // Gargle
            if (currKeyState.IsKeyDown(Keys.G) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.G)))
            {
                music.toggleGargle();   
            }

            // Distortion (TODO: Need to play with this a lot, it doesn't play nice with other FX)
            if (currKeyState.IsKeyDown(Keys.D) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.D)))
            {
                music.toggleDistortion();
            }

            // Speed Control
            if (currKeyState.IsKeyDown(Keys.Up) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.Up)))
            {
                music.speedUp(10);
            }
            if (currKeyState.IsKeyDown(Keys.Down) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.Down)))
            {
                music.slowDown(10);
            }

            // Pitch Control
            if (currKeyState.IsKeyDown(Keys.NumPad8) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.NumPad8)))
            {
                music.pitchUp(1);
            }
            if (currKeyState.IsKeyDown(Keys.NumPad2) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.NumPad2)))
            {
                music.pitchDown(1);
            }

            // Filter experiment
            if (currKeyState.IsKeyDown(Keys.Z) && prevKeyState.IsKeyDown(Keys.Z))
            {
                float templow = low + 1;
                float temphigh = high - 43.333f;
                if (templow < temphigh)
                {
                    low = templow;
                    high = temphigh;
                    music.transformFilter(low, high);
                }
            }
            else if (currKeyState.IsKeyUp(Keys.Z) && prevKeyState.IsKeyUp(Keys.Z))
            {
                if (low != 0 && high != 16000)
                {
                    float templow = low - 1;
                    float temphigh = high + 43.333f;
                    if (templow > 0 && temphigh < 16000)
                    {
                        low = templow;
                        high = temphigh;
                        music.transformFilter(low, high);
                    }
                    else
                    {
                        low = 0;
                        high = 16000;
                        music.toggleFilter();
                    }
                }

            }

            // Autowah
            if (currKeyState.IsKeyDown(Keys.W) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.W)))
            {
                music.toggleWah();
            }

            prevKeyState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
