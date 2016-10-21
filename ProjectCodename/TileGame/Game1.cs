using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TileEngine;

namespace TileGame
{
    public class Game1 : Game
    {
        //Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TileMap tileMap = new TileEngine.TileMap();

        Camera camera = new Camera();
        TileLayer tileLayer;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1281; // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 721; // set this value to the desired height of your window
            graphics.ApplyChanges();
        }



        protected override void Initialize()
        {
            base.Initialize();

            tileMap.Layers.Add(tileLayer);
        }



        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tileLayer = TileLayer.FromFile(Content, "Content/Layers/Layer1.layer");
        }



        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        protected override void Update(GameTime gameTime)
        {
            //Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            camera.Update();

            //Clamp camera on Top and Left of the window
            if (camera.position.X < 1)
                camera.position.X = 1;
            if (camera.position.Y < 1)
                camera.position.Y = 1;

            //Get window Width and Height
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;

            //Clamp camera on Bottom and Right of the window
            if (camera.position.X > tileLayer.WidthInPixels - screenWidth)
                camera.position.X = tileLayer.WidthInPixels - screenWidth;
            if (camera.position.Y > tileLayer.HeightInPixels - screenHeight)
                camera.position.Y = tileLayer.HeightInPixels - screenHeight;

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            tileMap.Draw(spriteBatch, camera);

            base.Draw(gameTime);
        }
    }
}