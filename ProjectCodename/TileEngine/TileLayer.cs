using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TileEngine
{
    public class TileLayer
    {
        //Texture tile size
        static int tileWidth = 64;
        static int tileHeight = 64;


        //Restricts Tile Sizes
        public static int TileWidth
        {
            get { return tileWidth; }
            set
            {
                tileWidth = (int)MathHelper.Clamp(value, 20f, 100f);
            }
        }

        public static int TileHeight
        {
            get { return tileHeight; }
            set
            {
                tileHeight = (int)MathHelper.Clamp(value, 20f, 100f);
            }
        }


        //Returns textures length in pixels
        List<Texture2D> tileTextures = new List<Texture2D>();
        int[,] map;


        //Gets the size of the map in pixels
        public int WidthInPixels
        {
            get
            {
                return Width * tileWidth;
            }
        }

        public int HeightInPixels
        {
            get
            {
                return Height * tileHeight;
            }
        }


        //Gets the size of the map
        public int Width
        {
            get { return map.GetLength(1); }
        }

        public int Height
        {
            get { return map.GetLength(0); }
        }


        //Creates new map
        public TileLayer (int width, int height)
        {
            map = new int[height, width];
        }


        //Clones map
        public TileLayer(int[,] existingMap)
        {
            map = (int[,])existingMap.Clone();
        }


        //Loads each texture
        public void LoadTileTextures(ContentManager content, params string[] textureNames)
        {
            //Variables
            Texture2D texture;

            //Stores texture files in Tiles folder
            foreach (string textureName in textureNames)
            {
                texture = content.Load<Texture2D>(textureName);
                tileTextures.Add(texture);
            }
        }


        //Add texture to available textures
        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }



        public void Draw (SpriteBatch batch, Camera camera)
        {
            batch.Begin();

            //Gets tile map dimensions
            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            //Draws tiles
            for (int x = 0; x < tileMapWidth; x++)
            {
                for (int y = 0; y < tileMapHeight; y++)
                {
                    int textureIndex = map[y, x];
                    Texture2D texture = tileTextures[textureIndex];

                    batch.Draw(texture, new Rectangle(x * tileWidth - (int)camera.position.X, y * tileHeight - (int)camera.position.Y, tileWidth, tileHeight), Color.White);
                }
            }

            batch.End();
        }
    }
}
