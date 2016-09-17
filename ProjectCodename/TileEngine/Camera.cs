using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TileEngine
{
    public class Camera
    {
        //Variables
        float speed = 5;
        public Vector2 position = Vector2.Zero;


        //Restricts Camera Speed
        public float Speed
        {
            get { return speed; }
            set
            {
                //Keeps speed at 1 or greater
                speed = (float)Math.Max(value, 1f);
            }
        }



        public void Update()
        {
            //Variables
            KeyboardState keyState = Keyboard.GetState();
            Vector2 motion = Vector2.Zero;

            //GamePad variable and inverts joystick input
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            motion = new Vector2(gamePadState.ThumbSticks.Left.X, -gamePadState.ThumbSticks.Left.Y);

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
                position += motion * Speed;
            }
        }

    }
}
