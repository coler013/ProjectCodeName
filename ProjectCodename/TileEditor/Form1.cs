using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TileEngine;

namespace TileEditor
{
    using Image = System.Drawing.Image;

    public partial class Form1 : Form
    {
        //Supported image types
        string[] imageExtensions = new string[]
        {
            ".jpg", ".png",
        };
        
        //Variables
        SpriteBatch spriteBatch;
        Texture2D tileTexture;
        Camera camera = new Camera();
        TileLayer currentLayer;
        TileMap tileMap = new TileMap();
        int cellX, cellY;

        Dictionary<string, TileLayer> layerDict = new Dictionary<string, TileLayer>();
        Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
        Dictionary<string, Image> previewDict = new Dictionary<string, Image>();

        int maxWidth = 0, maxHeight = 0;


        //
        public Vector2 GetMousePosition()
        {
            System.Drawing.Point point = this.PointToClient(Control.MousePosition);
            return new Vector2(point.X, point.Y);
        }


        //
        public GraphicsDevice GraphicsDevice
        {
            get { return tileDisplay1.GraphicsDevice;  }
        }


        //
        public Form1()
        {
            InitializeComponent();

            tileDisplay1.OnInitialize += new EventHandler(tileDisplay1_OnInitialize);
            tileDisplay1.OnDraw += new EventHandler(tileDisplay1_OnDraw);

            //Invalidates the idle to allow the screen to update faster
            Application.Idle += delegate { tileDisplay1.Invalidate(); };

            //Restricts selectible file types to .layer
            openFileDialog1.Filter = "Layer File | *.layer";
            saveFileDialog1.Filter = "Layer File | *.layer";

            Mouse.WindowHandle = tileDisplay1.Handle;
        }


        //
        void tileDisplay1_OnInitialize(object sender, EventArgs e)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            FileStream fileStream = new FileStream("Content/tile.png", FileMode.Open);
            tileTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
        }


        //
        void tileDisplay1_OnDraw(object sender, EventArgs e)
        {
            Logic();
            Render();
        }


        //Update method
        private void Logic()
        {
            camera.position.X = hScrollBar1.Value * TileLayer.TileWidth;
            camera.position.Y = vScrollBar1.Value * TileLayer.TileHeight;

            //Get mouse position
            //MouseState ms = Mouse.GetState();
            int mx = Mouse.GetState().X;
            int my = Mouse.GetState().Y;
            //int mx = (int)GetMousePosition().X;
            //int my = (int)GetMousePosition().Y;
            //int mx = Control.MousePosition.X; ;
            //int my = Control.MousePosition.Y; ;

            System.Diagnostics.Debug.WriteLine("msX " + ms.X);
            System.Diagnostics.Debug.WriteLine("msY " + ms.Y);

            int mx = ms.X;
            int my = ms.Y;

            System.Diagnostics.Debug.WriteLine("mX " + mx);
            System.Diagnostics.Debug.WriteLine("mY " + my);

            if (currentLayer != null)
            {
                //Find what cell mouse is over
                if (mx >= 0 && mx < tileDisplay1.Width && my >= 0 && my < tileDisplay1.Height)
                {
                    cellX = mx / TileLayer.TileWidth;
                    cellY = my / TileLayer.TileHeight;

                    cellX = (int)MathHelper.Clamp(cellX, 0, currentLayer.Width);
                    cellY = (int)MathHelper.Clamp(cellY, 0, currentLayer.Height);

                    if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        if (drawRadioButton.Checked)
                        {
                            //Get name and index
                            Texture2D texture = textureDict[textureListBox.SelectedItem as string];
                            int index = currentLayer.IsUsingTexture(texture);

                            //Adds texture to layer if not already used
                            if (index == -1)
                            {
                                currentLayer.AddTexture(texture);
                                index = currentLayer.IsUsingTexture(texture);
                            }

                            //Draw index to cell
                            currentLayer.SetCellIndex(cellX, cellY, index);
                        }
                        else if (eraseRadioButton.Checked)
                        {
                            currentLayer.SetCellIndex(cellX, cellY, -1);
                        }
                    }
                }
                else
                {
                    cellX = cellY = -1;
                }
            }
        }

        //Draw method
        private void Render()
        {
            GraphicsDevice.Clear(Color.Black);

            foreach (TileLayer layer in tileMap.Layers)
            {
                layer.Draw(spriteBatch, camera);

                spriteBatch.Begin();

                for ( int y = 0; y < layer.Height; y++)
                {
                    for (int x = 0; x < layer.Width; x++)
                    {
                        if (layer.GetCellIndex(x,y) == -1)
                        {
                            spriteBatch.Draw(tileTexture, new Rectangle(x * TileLayer.TileWidth - (int)camera.position.X, y * TileLayer.TileHeight - (int)camera.position.Y, TileLayer.TileWidth, TileLayer.TileHeight), Color.White);
                        }
                    }
                }

                spriteBatch.End();
            }

            //Draw cell cursor
            if (currentLayer != null)
            {
                if (cellX != -1 && cellY != -1)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(tileTexture, new Rectangle(cellX * TileLayer.TileWidth - (int)camera.position.X, cellY * TileLayer.TileHeight - (int)camera.position.Y, TileLayer.TileWidth, TileLayer.TileHeight), Color.Red);
                    spriteBatch.End();
                }
            }

        }

        private void browseForContentButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                contentPathTextbox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void newTileMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check for OK click
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;

                string[] textureNames;

                //Get texture names from file
                TileLayer layer = TileLayer.FromFile(filename, out textureNames);

                if (layer.WidthInPixels > tileDisplay1.Width)
                {
                    maxWidth = (int)Math.Max(layer.Width, maxWidth);

                    hScrollBar1.Visible = true;
                    hScrollBar1.Minimum = 0;
                    hScrollBar1.Maximum = maxWidth;
                }

                if (layer.HeightInPixels > tileDisplay1.Height)
                {
                    maxHeight = (int)Math.Max(layer.Height, maxHeight);

                    vScrollBar1.Visible = true;
                    vScrollBar1.Minimum = 0;
                    vScrollBar1.Maximum = maxHeight;
                }

                //Add Layer to dictionary, Tile Map, and Layer List Box
                layerDict.Add(Path.GetFileName(filename), layer);
                tileMap.Layers.Add(layer);
                layerListBox.Items.Add(Path.GetFileName(filename));

                //Get Textures
                foreach (string textureName in textureNames)
                {
                    string fullPath = contentPathTextbox.Text + "/" + textureName;

                    //Find file extension
                    foreach (string ext in imageExtensions)
                    {
                        if(File.Exists(fullPath + ext))
                        {
                            fullPath += ext;
                            break;
                        }
                    }

                    //Load Textures and Images
                    FileStream filestream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                    Texture2D tex = Texture2D.FromStream(GraphicsDevice, filestream);
                    Image image = Image.FromStream(filestream);

                    //Add Textures and Images to dictionary
                    textureDict.Add(textureName, tex);
                    previewDict.Add(textureName, image);

                    //Add name of texture to Texture List Box
                    textureListBox.Items.Add(textureName);

                    //Add texture to layer
                    layer.AddTexture(tex);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (layerListBox.SelectedItem != null)
            {
                string filename = layerListBox.SelectedItem as string;

                TileLayer tileLayer = layerDict[filename];

                Dictionary<int, string> utilizedTextures = new Dictionary<int, string>();

                //Creates Dictionary of textures being used in this layer
                foreach (string textureName in textureListBox.Items)
                {
                    int index = tileLayer.IsUsingTexture(textureDict[textureName]);

                    if (index != -1)
                    {
                        utilizedTextures.Add(index, textureName);
                    }
                }

                List<string> utilizedTextureList = new List<string>();

                for (int i = 0; i < utilizedTextures.Count; i++)
                {
                    utilizedTextureList.Add(utilizedTextures[i]);
                }

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    tileLayer.Save(saveFileDialog1.FileName, utilizedTextureList.ToArray());
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void layerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (layerListBox.SelectedItem != null)
            {
                if (layerListBox.SelectedItem != null)
                    currentLayer = layerDict[layerListBox.SelectedItem as string];
            }
        }
        private void textureListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textureListBox.SelectedItem != null)
            {
                texturePreviewBox.Image = previewDict[textureListBox.SelectedItem as string];
            }
        }
    }
}
