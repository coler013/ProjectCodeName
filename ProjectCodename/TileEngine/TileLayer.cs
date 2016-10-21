using System;
using System.Collections.Generic;
using System.IO;
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

        //Creates Alpha Channel
        float alpha = 1f;

        public float Alpha
        {
            get { return alpha; }

            set
            {
                alpha = MathHelper.Clamp(value, 0f, 1f);
            }
        }

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


        //
        public static TileLayer FromFile(string filename, out string[] textureNameArray)
        {
            TileLayer tileLayer;
            bool readingTextures = false;
            bool readingLayout = false;
            List<string> textureNames = new List<string>();
            List<List<int>> tempLayout = new List<List<int>>();

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();

                    //Trims space and line breaks
                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.Contains("[Textures]"))
                    {
                        readingTextures = true;
                        readingLayout = false;
                    }
                    else if (line.Contains("[Layout]"))
                    {
                        readingTextures = false;
                        readingLayout = true;
                    }
                    else if (readingTextures)
                    {
                        textureNames.Add(line);
                    }
                    else if (readingLayout)
                    {
                        List<int> row = new List<int>();

                        string[] cells = line.Split(' ');

                        foreach (string c in cells)
                        {
                            if (!string.IsNullOrEmpty(c))
                                row.Add(int.Parse(c));
                        }

                        tempLayout.Add(row);
                    }
                }
            }

            int width = tempLayout[0].Count;
            int height = tempLayout.Count;

            tileLayer = new TileLayer(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tileLayer.SetCellIndex(x, y, tempLayout[y][x]);
                }
            }

            textureNameArray = textureNames.ToArray();

            return tileLayer;
        }


        //Checks to see if texture is in use
        public int IsUsingTexture(Texture2D texture)
        {
            if (tileTextures.Contains(texture))
                return tileTextures.IndexOf(texture);

            return -1;
        }


        //Save Textures and Layouts to file
        public void Save(string filename, string[] textureNames)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("[Textures]");
                foreach (string t in textureNames)
                    writer.WriteLine(t);

                writer.WriteLine();

                writer.WriteLine("[Layout]");
                for (int y = 0; y < Height; y++)
                {
                    string line = string.Empty;

                    for (int x = 0; x < Width; x++)
                    {
                        line += map[y, x].ToString() + " ";
                    }

                    writer.WriteLine(line);
                }
            }
        }


        //Load Textures and Layers from file
        public static TileLayer FromFile(ContentManager content, string filename)
        {
            TileLayer tileLayer;
            bool readingTextures = false;
            bool readingLayout = false;
            List<string> textureNames = new List<string>();
            List<List<int>> tempLayout = new List<List<int>>();

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();

                    //Trims space and line breaks
                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.Contains("[Textures]"))
                    {
                        readingTextures = true;
                        readingLayout = false;
                    }
                    else if (line.Contains("[Layout]"))
                    {
                        readingTextures = false;
                        readingLayout = true;
                    }
                    else if (readingTextures)
                    {
                        textureNames.Add(line);
                    }
                    else if (readingLayout)
                    {
                        List<int> row = new List<int>();

                        string[] cells = line.Split(' ');

                        foreach (string c in cells)
                        {
                            if (!string.IsNullOrEmpty(c))
                                row.Add(int.Parse(c));
                        }

                        tempLayout.Add(row);
                    }
                }
            }

            int width = tempLayout[0].Count;
            int height = tempLayout.Count;

            tileLayer = new TileLayer(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tileLayer.SetCellIndex(x, y, tempLayout[y][x]);
                }
            }

            tileLayer.LoadTileTextures(content, textureNames.ToArray());

            return tileLayer;
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


        public void SetCellIndex(int x, int y, int cellIndex)
        {
            map[y, x] = cellIndex;
        }


        public int GetCellIndex(int x, int y)
        {
            return map[y, x];
        }

        public void Draw (SpriteBatch batch, Camera camera)
        {
            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            //Gets tile map dimensions
            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            //Draws tiles
            for (int x = 0; x < tileMapWidth; x++)
            {
                for (int y = 0; y < tileMapHeight; y++)
                {
                    int textureIndex = map[y, x];

                    if (textureIndex == -1)
                        continue;

                    Texture2D texture = tileTextures[textureIndex];

                    batch.Draw(texture, new Rectangle(x * tileWidth - (int)camera.position.X, y * tileHeight - (int)camera.position.Y, tileWidth, tileHeight), new Color(new Vector4(1f, 1f, 1f, Alpha)));
                }
            }

            batch.End();
        }
    }
}
