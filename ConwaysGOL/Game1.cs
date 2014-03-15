using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ConwaysGOL
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        public const int UPS = 15; //updates per second
        public const int FPS = 60;

        public const int CellSize =4;
        public const int CellsX = 250;
        public const int CellsY = 250;

        public static bool Paused = true;

        public static SpriteFont Font;
        public static Texture2D Pixel;

        public static Vector2 ScreenSize;

        private Grid grid;

        private KeyboardState keyboardState, lastKeyboardState;


        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / FPS);

            ScreenSize = new Vector2(CellsX, CellsY) * CellSize;

            graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            grid = new Grid();
            keyboardState = lastKeyboardState = Keyboard.GetState();
            
        }

        protected override void LoadContent()
        {
 	        spriteBatch = new SpriteBatch(GraphicsDevice);

            Font = Content.Load<SpriteFont>("Font");

            Pixel = new Texture2D(spriteBatch.GraphicsDevice,1, 1);
            Pixel.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {   
            keyboardState = Keyboard.GetState();

            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // Toggle pause when spacebar is pressed.
            if(keyboardState.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space))
                Paused = !Paused;
            //Clear screen if backspace is pressed.
            if (keyboardState.IsKeyDown(Keys.Back) && lastKeyboardState.IsKeyUp(Keys.Back))
                grid.Clear();
 	        
            base.Update(gameTime);
            grid.Update(gameTime);
            lastKeyboardState = keyboardState;
        }
        protected override void Draw(GameTime gameTime)
        {
            if (Paused)
                GraphicsDevice.Clear(Color.Gray);
            else
                GraphicsDevice.Clear(Color.Goldenrod);

            spriteBatch.Begin();
            if (Paused)
            {
                string paused = "Paused";
                spriteBatch.DrawString(Font, paused, ScreenSize / 2, Color.DarkGray, 0f, Font.MeasureString(paused) / 2, 1f, SpriteEffects.None, 0f);
            }
            grid.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);

        
        }

      
    }
}
