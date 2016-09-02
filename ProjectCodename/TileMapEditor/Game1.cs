using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TileMapEditor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Texture2D> tileTextures = new List<Texture2D>();

        int[,] tileMap = new int[,]
        {
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },
            { 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 0, },

        };

        //Texture tile size
        int tileWidth = 64;
        int tileHeight = 64;

        //Camera
        Vector2 cameraPosition = Vector2.Zero;
        float cameraSpeed = 5;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280; // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 720; // set this value to the desired height of your window
            graphics.ApplyChanges();
        }



        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }



        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D texture;

            texture = Content.Load<Texture2D>("Tiles/se_free_dirt_texture");
            tileTextures.Add(texture);

            texture = Content.Load<Texture2D>("Tiles/se_free_grass_texture");
            tileTextures.Add(texture);

            texture = Content.Load<Texture2D>("Tiles/se_free_ground_texture");
            tileTextures.Add(texture);

            texture = Content.Load<Texture2D>("Tiles/se_free_mud_texture");
            tileTextures.Add(texture);

            texture = Content.Load<Texture2D>("Tiles/se_free_road_texture");
            tileTextures.Add(texture);

            texture = Content.Load<Texture2D>("Tiles/se_free_rock_texture");
            tileTextures.Add(texture);

            texture = Content.Load<Texture2D>("Tiles/se_free_wood_texture");
            tileTextures.Add(texture);

            texture = Content.Load<Texture2D>("Tiles/wood_axis_lightwood");
            tileTextures.Add(texture);

        }



        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Variables
            KeyboardState keyState = Keyboard.GetState();
            //GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            Vector2 motion = Vector2.Zero;

            //Maps and inverts joystick input
            //motion = new Vector2(gamePadState.ThumbSticks.Left.X, -gamePadState.ThumbSticks.Left.Y);

            //Maps keyboard movement
            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
                motion.Y--;
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
                motion.Y++;
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
                motion.X--;
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
                motion.X++;

            //Normalize and add speed
            if (motion != Vector2.Zero)
            {
                motion.Normalize(); //Comment out if using gamepad for non-analog movement
                cameraPosition += motion * cameraSpeed;
            }

            //Clamp camera on Top and Left of the window
            if (cameraPosition.X < 0)
                cameraPosition.X = 0;
            if (cameraPosition.Y < 0)
                cameraPosition.Y = 0;

            //Get window Width and Height
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;

            //Get tileMap Width and Height
            int tileMapWidth = tileMap.GetLength(1) * tileWidth;
            int tileMapHeight = tileMap.GetLength(0) * tileHeight;

            //Clamp camera on Bottom and Right of the window
            if (cameraPosition.X > tileMapWidth - screenWidth)
                cameraPosition.X = tileMapWidth - screenWidth;
            if (cameraPosition.Y > tileMapHeight - screenHeight)
                cameraPosition.Y = tileMapHeight - screenHeight;

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //Gets tile map dimensions
            int tileMapWidth = tileMap.GetLength(1);
            int tileMapHeight = tileMap.GetLength(0);

            //Draws tiles
            for (int x = 0; x < tileMapWidth; x++)
            {
                for (int y = 0; y < tileMapHeight; y++)
                {
                    int textureIndex = tileMap[y, x];
                    Texture2D texture = tileTextures[textureIndex];

                    spriteBatch.Draw(texture, new Rectangle(x * tileWidth - (int)cameraPosition.X, y * tileHeight - (int)cameraPosition.Y, tileWidth, tileHeight), Color.White);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}