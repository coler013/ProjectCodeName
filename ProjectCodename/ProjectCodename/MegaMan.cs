using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCodename
{
    class MegaMan:Sprite
    {
        const string MEGAMAN_ASSETNAME = "images/Mega-Man-Sprite";
        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        const int MEGAMAN_SPEED = 150;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        


        enum State
        {
            Walking
        }
        State currentState = State.Walking;
        Vector2 direction = Vector2.Zero;
        Vector2 speed = Vector2.Zero;

        KeyboardState previousKeyboardState;

        public void LoadContent (ContentManager contentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(contentManager, MEGAMAN_ASSETNAME);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            UpdateMovement(currentKeyboardState);

            previousKeyboardState = currentKeyboardState;
            base.Update(gameTime, speed, direction);
            
        }

        public void UpdateMovement(KeyboardState theCurrentKeyboardState)
        {
            if (currentState == State.Walking)
            {
                speed = Vector2.Zero;
                direction = Vector2.Zero;

                if (theCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
                {
                    speed.X = MEGAMAN_SPEED;
                    direction.X = MOVE_LEFT;
                }

                else if (theCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
                {
                    speed.X = MEGAMAN_SPEED;
                    direction.X = MOVE_RIGHT;
                }
                else if (theCurrentKeyboardState.IsKeyDown(Keys.Up) == true)
                {
                    speed.Y = MEGAMAN_SPEED;
                    direction.Y = MOVE_UP;
                }
                else if (theCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
                {
                    speed.Y = MEGAMAN_SPEED;
                    direction.Y = MOVE_DOWN;
                }
            }

        }


    }
}
