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
        KeyboardPlayerSprite keyboard;
        PlatformSprite[] platform;
        List<AbstractSprite> entities;
        Music music;
        BulletSprite bullets;
        List<EnemySprite> enemies;
        AbstractPlayerSprite player;
        GamePadState oldpadstate;
        GamePadState gamepadstate;
        KeyboardState oldkeystate;
        KeyboardState keystate;
        MouseState oldmousestate;
        MouseState mousestate;
        bool songChosen;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            entities = new List<AbstractSprite>();
            bullets = new BulletSprite(this);
            player_one = new ControllerPlayerSprite(PlayerIndex.One, this, ref entities, ref bullets);
            keyboard = new KeyboardPlayerSprite(this, ref entities, ref bullets);
            platform = new PlatformSprite[4];
            // init platform locs
            platform[0] = new PlatformSprite(this, 0.25f, 200, 400);
            platform[1] = new PlatformSprite(this, 0.25f, 400, 380);
            platform[2] = new PlatformSprite(this, 0.25f, 175, 200);
            platform[3] = new PlatformSprite(this, 0.25f, 550, 150);
            entities.Add(platform[0]);
            entities.Add(platform[1]);
            entities.Add(platform[2]);
            entities.Add(platform[3]);

            this.IsMouseVisible = true;
            
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
            keyboard.Set_Sprite_Batch(spriteBatch);
            keyboard.Content_Load("swordguy");
            bullets.Set_Sprite_Batch(spriteBatch);
            bullets.Content_Load("bullet");
            player_one.Content_Load("swordguy"); //set the texture for player one
            for (int i = 0; i < platform.Length; i++)
            {
                platform[i].Set_Sprite_Batch(spriteBatch);
                platform[i].Content_Load("wood-platform");
                platform[i].Update();//just for an initital load. dont need to call repeatedly
            }
            foreground = Content.Load<Texture2D>("earth");
            basichud = Content.Load<SpriteFont>("myFont");
            
            music = new Music("Content/dropkick.mp3");
            music.playPause();

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
            //Choose a song
            if (!songChosen)
            {
                
            }
            oldpadstate = gamepadstate;
            gamepadstate = GamePad.GetState(PlayerIndex.One);
            oldkeystate = keystate;
            keystate = Keyboard.GetState();
            oldmousestate = mousestate;
            mousestate = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player_one.Update();
            keyboard.Update();
            bullets.Update();
            if (gamepadstate.IsConnected)
            {
                player = player_one;
            } else{
                player = keyboard;
            }
            int newspawns = 0;
            if (enemies == null)
            {
                newspawns = 1;
                enemies = new List<EnemySprite>();
            }
            foreach (EnemySprite enemy in enemies)
            {
                if (enemy != null && enemy.health <= 0)
                {
                    enemy.Respawn();
                    newspawns++;
                }
                
                enemy.Update();
            }
            for (int i = 0; i < newspawns; i++)
            {
                EnemySprite enemy = new EnemySprite(this, entities, player, ref bullets, music);
                enemy.Set_Sprite_Batch(spriteBatch);
                enemy.Content_Load("blog_alien_concepts");
                enemies.Add(enemy);
                enemy.Update();
            }
            

            // Filter logic (narrower bandpass filter when the character is higher up)
            float ratio = player.location.Y / (this.GraphicsDevice.Viewport.Height - player.cur_height);
            float low = 360f - (360f * ratio);
            float high = 16000f - (low * 43.333f);
            music.transformFilter(low, high);

            // Pan logic (match pan to lateral location of character)
            ratio = player.location.X / ((this.GraphicsDevice.Viewport.Width + player.cur_width) / 2);
            music.pan(ratio - 1);

            // Flanger logic (overlay effect while shooting)
            if ((oldpadstate.ThumbSticks.Right.X != 0 || oldpadstate.ThumbSticks.Right.Y != 0) &&
                (gamepadstate.ThumbSticks.Right.X == 0 && gamepadstate.ThumbSticks.Right.Y == 0))
            {
                music.toggleFlanger();
            }
            if ((oldpadstate.ThumbSticks.Right.X == 0 && oldpadstate.ThumbSticks.Right.Y == 0) &&
                (gamepadstate.ThumbSticks.Right.X != 0 || gamepadstate.ThumbSticks.Right.Y != 0))
            {
                music.toggleFlanger();
            }
            if (oldkeystate.IsKeyDown(Keys.W) && !keystate.IsKeyDown(Keys.W))
            {
                music.toggleFlanger();
            }
            if (!oldkeystate.IsKeyDown(Keys.W) && keystate.IsKeyDown(Keys.W))
            {
                music.toggleFlanger();
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(foreground, new Vector2(450, 420), Color.White);
            if (gamePadState.IsConnected)
            {
                player_one.Draw();
            }
            else
            {
                keyboard.Draw();
            }
            for (int i = 0; i < platform.Length; i++)
            {
                platform[i].Draw();
            }
            foreach (EnemySprite enemy in enemies) enemy.Draw();
            
            spriteBatch.DrawString(basichud, "ALL YOUR BASE ARE BELONG TO US", new Vector2(this.GraphicsDevice.Viewport.Width/2, 20), Color.White);
            spriteBatch.DrawString(basichud, "Health " + player.health + "%", new Vector2(10, 10), Color.White);
            bullets.Draw();


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    
}
