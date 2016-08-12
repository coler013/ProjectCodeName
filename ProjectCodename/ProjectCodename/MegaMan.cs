using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

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
            Walking,
            Jumping
        }
        State currentState = State.Walking;
        Vector2 direction = Vector2.Zero;
        Vector2 speed = Vector2.Zero;
        Vector2 startingPosition = Vector2.Zero;

        KeyboardState previousKeyboardState;

        public void LoadContent (ContentManager contentManager)
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(contentManager, MEGAMAN_ASSETNAME);
        }
        
        #region Updates

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            UpdateMovement(currentKeyboardState);
            UpdateJump(currentKeyboardState);

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

        private void UpdateJump(KeyboardState aCurrentKeyboardState)
        {
            if (currentState == State.Walking)
            {
                if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && previousKeyboardState.IsKeyDown(Keys.Space) == false)
                {
                    Jump();
                }
            }

            if (currentState == State.Jumping)
            {
                if (startingPosition.Y - Position.Y > 150)
                {
                    direction.Y = MOVE_DOWN;
                }

                if (Position.Y > startingPosition.Y)
                {
                    Position.Y = startingPosition.Y;
                    currentState = State.Walking;
                    direction = Vector2.Zero;
                }
            }
        }

        #endregion

        /// <summary>
        /// Megaman SOARS into the air in a giant leap and then moves the world to his feet
        /// </summary>
        private void Jump()
        {
            if (currentState != State.Jumping)
            {
                currentState = State.Jumping;
                startingPosition = Position;
                direction.Y = MOVE_UP;
                speed = new Vector2(MEGAMAN_SPEED, MEGAMAN_SPEED);
            }
        }
    }
}
