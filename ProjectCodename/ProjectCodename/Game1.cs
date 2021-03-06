﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectCodename 
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MegaMan megaManSprite;
        bool LastMenuMain = true;

        enum GameState
        {
            MainMenu,
            Options,
            Paused,
            Playing,
        }

        GameState CurrentGameState = GameState.MainMenu;

        //Screen Adjust setting
        int screenWidth = 800, screenHeight = 600;

        cButton btnPlay;
        cButton btnExit;

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
            megaManSprite = new MegaMan();            

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

            // TODO: use this.Content to load your game content here
            megaManSprite.LoadContent(Content);

            //Screen Setting Up
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            //graphics.IsFullScreen = true;

            graphics.ApplyChanges();
            IsMouseVisible = true;

            //Play Button
            btnPlay = new cButton(Content.Load<Texture2D>("images/PlayBtn"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(350, 300));

            //Options Button
            btnExit = new cButton(Content.Load<Texture2D>("images/ExitBtn"), graphics.GraphicsDevice);
            btnExit.setPosition(new Vector2(350, 350));
            
            

            
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
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();

            // TODO: Add your update logic here
            megaManSprite.Update(gameTime);


            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Playing;
                    btnPlay.Update(mouse);
                    break;
                case GameState.Playing:
                    LastMenuMain = false;
                    if (keyboard.IsKeyDown(Keys.Escape)) CurrentGameState = GameState.Options;
                    break;
                case GameState.Options:
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Playing;
                    btnPlay.Update(mouse);
                    if (btnExit.isClicked == true) Exit();
                    btnExit.Update(mouse);
                    break;
                case GameState.Paused:
                    break;
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

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            megaManSprite.Draw(spriteBatch);

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("images/MainMenu"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnPlay.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    LastMenuMain = false;
                    break;
                case GameState.Options:
                    spriteBatch.Draw(Content.Load<Texture2D>("images/OptionsMenu"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnExit.Draw(spriteBatch);
                    btnPlay.Draw(spriteBatch);
                    LastMenuMain = false;
                    break;
                case GameState.Paused:
                    break;
            }

            spriteBatch.End();

            

                    base.Draw(gameTime);
        }
      

        
    }
}
