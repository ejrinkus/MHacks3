#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;
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
        int stream;
        int fxFlanger;
        int fxReverb;
        int fxFilter;
        int fxChorus;
        int fxGargle;
        int fxOverdrive;
        int fxDistortion;

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


            // init BASS using the default output device 
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                // create a stream channel from a file
                stream = Bass.BASS_StreamCreateFile("Content/cinema.wav", 0, 0, BASSFlag.BASS_DEFAULT);
                if (stream != 0)
                {
                    // play the stream channel
                    Bass.BASS_ChannelPlay(stream, false);
                }
                else
                {
                    // error creating the stream 
                    Console.WriteLine("Stream error: {0}", Bass.BASS_ErrorGetCode());
                }

            }
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
            currKeyState = Keyboard.GetState();

            // Exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || currKeyState.IsKeyDown(Keys.Escape))
            {
                // free the stream 
                Bass.BASS_StreamFree(stream);
                // free BASS 
                Bass.BASS_Free();
                Exit();
            }

            // Flanger
            if (currKeyState.IsKeyDown(Keys.F) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.F)))
            {
                fxFlanger = Bass.BASS_ChannelSetFX(stream, BASSFXType.BASS_FX_DX8_FLANGER, 1);
            }
            else if (currKeyState.IsKeyUp(Keys.F) && prevKeyState.IsKeyDown(Keys.F))
            {
                Bass.BASS_ChannelRemoveFX(stream, fxFlanger);
            }

            // Reverb (TODO: make it less jarring on the bass hits...)
            if (currKeyState.IsKeyDown(Keys.R) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.R)))
            {
                fxReverb = Bass.BASS_ChannelSetFX(stream, BASSFXType.BASS_FX_DX8_REVERB, 1);
            }
            else if (currKeyState.IsKeyUp(Keys.R) && prevKeyState.IsKeyDown(Keys.R))
            {
                Bass.BASS_ChannelRemoveFX(stream, fxReverb);
            }

            // Filter

            // Chorus
            if (currKeyState.IsKeyDown(Keys.C) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.C)))
            {
                fxChorus = Bass.BASS_ChannelSetFX(stream, BASSFXType.BASS_FX_DX8_CHORUS, 1);
            }
            else if (currKeyState.IsKeyUp(Keys.C) && prevKeyState.IsKeyDown(Keys.C))
            {
                Bass.BASS_ChannelRemoveFX(stream, fxChorus);
            }

            // Gargle
            if (currKeyState.IsKeyDown(Keys.G) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.G)))
            {
                fxGargle = Bass.BASS_ChannelSetFX(stream, BASSFXType.BASS_FX_DX8_GARGLE, 1);
            }
            else if (currKeyState.IsKeyUp(Keys.G) && prevKeyState.IsKeyDown(Keys.G))
            {
                Bass.BASS_ChannelRemoveFX(stream, fxGargle);
            }

            // Pitch Control

            // Distortion (TODO: Need to play with this a lot, it doesn't play nice with other FX)
            if (currKeyState.IsKeyDown(Keys.D) &&
                (prevKeyState == null || prevKeyState.IsKeyUp(Keys.D)))
            {
                fxDistortion = Bass.BASS_ChannelSetFX(stream, BASSFXType.BASS_FX_DX8_DISTORTION, 1);
            }
            else if (currKeyState.IsKeyUp(Keys.D) && prevKeyState.IsKeyDown(Keys.D))
            {
                Bass.BASS_ChannelRemoveFX(stream, fxDistortion);
            }

            // Overdrive

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
