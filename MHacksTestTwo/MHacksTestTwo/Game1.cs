﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace MHacksTestOne
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        Texture2D foreground;
        SpriteFont basichud;
        ControllerPlayerSprite player_one;
        PlatformSprite platform;
        List<AbstractSprite> entities;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            entities = new List<AbstractSprite>();
            player_one = new ControllerPlayerSprite(PlayerIndex.One, this, ref entities);
            platform = new PlatformSprite(this);
            
            entities.Add(platform);
            
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
            
            spriteBatch = new SpriteBatch(GraphicsDevice); //create the sprite batch
            background = Content.Load<Texture2D>("stars");
            player_one.Set_Sprite_Batch(spriteBatch); //pass the spritebatch that is used in drawing
            player_one.Content_Load("swordguy"); //set the texture for player one
            platform.Set_Sprite_Batch(spriteBatch);
            platform.Content_Load("cube");
            foreground = Content.Load<Texture2D>("earth");
            basichud = Content.Load<SpriteFont>("myFont");

            // TODO: use this.Content to load your game content here
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
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            platform.Update();
            player_one.Update();
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

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            player_one.Draw();
            platform.Draw();
            spriteBatch.Draw(foreground, new Vector2(450, 420), Color.White);
            spriteBatch.DrawString(basichud, "ALL YOUR BASE ARE BELONG TO US", new Vector2(this.GraphicsDevice.Viewport.Width/2, 20), Color.White);



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    
}
