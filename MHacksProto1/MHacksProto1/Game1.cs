#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
#endregion

namespace MHacksProto1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundEffect music;
        SoundEffectInstance musicInstance;
        KeyboardState prevKeyState;

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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            music = Content.Load<SoundEffect>("cinema");
            musicInstance = music.CreateInstance();
            musicInstance.Play();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            //Exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            //Play/Pause
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && 
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.Space)))
            {
                if (musicInstance.State == SoundState.Playing) musicInstance.Pause();
                else musicInstance.Resume();
            }

            //Volume Up
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && 
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.Up)))
            {
                Console.WriteLine("up");
                musicInstance.Volume += 0.1F;
                if (musicInstance.Volume > 1.0F) musicInstance.Volume = 1.0F;
            }

            //Volume Down
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && 
                (prevKeyState == null ||  prevKeyState.IsKeyUp(Keys.Down)))
            {
                Console.WriteLine("down");
                musicInstance.Volume -= 0.1F;
                if (musicInstance.Volume < 0.0F) musicInstance.Volume = 0.0F;
            }

            //Pan Right
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && 
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.Right)))
            {
                Console.WriteLine("right");
                musicInstance.Pan += 0.1F;
                if (musicInstance.Pan > 1.0F) musicInstance.Pan = 1.0F;
            }

            //Pan Left
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && 
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.Left)))
            {
                Console.WriteLine("left");
                musicInstance.Pan -= 0.1F;
                if (musicInstance.Pan < -1.0F) musicInstance.Pan = -1.0F;
            }

            //Pitch Up
            if (Keyboard.GetState().IsKeyDown(Keys.S) && 
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.S)))
            {
                Console.WriteLine("S");
                musicInstance.Pitch += (1.0F / 12.0F);
                if (musicInstance.Pitch > 1.0F) musicInstance.Pitch = 1.0F;
            }

            //Pitch Down
            if (Keyboard.GetState().IsKeyDown(Keys.A) && 
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.A)))
            {
                Console.WriteLine("A");
                musicInstance.Pitch -= (1.0F / 12.0F);
                if (musicInstance.Pitch < -1.0F) musicInstance.Pitch = -1.0F;
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
