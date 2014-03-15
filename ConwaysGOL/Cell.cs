using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ConwaysGOL
{
  class Cell
  {
        
        public Point Position { get; private set; }
        public Rectangle Bounds { get; private set; }
        public Color Color {get; set;} 
        private TimeSpan updateTimer;
        private bool lastLife;
        private int age; 

        public bool IsAlive {get; set; }
        public Cell (Point position)
        {
                
                
                Position = position;
                Bounds = new Rectangle(Position.X * Game1.CellSize, Position.Y * Game1.CellSize, Game1.CellSize,Game1.CellSize);
                IsAlive = false;
                Color = Color.LightSalmon;
        }

        public Cell(Point position, Color color)
        {
            Position = position;
            Bounds = new Rectangle(Position.X * Game1.CellSize, Position.Y * Game1.CellSize, Game1.CellSize, Game1.CellSize);
            IsAlive = false;
            Color = color;
            lastLife = false;
            age = 0;
        }
        public void toggleLife(bool Value)
        {
            IsAlive = Value;
            if (lastLife == false && Value == true)
                Color = Color.White;
            if ((age > 5) && Value == true)
                Color = Color.LightBlue;
            if (Value == true)
                age++;
            if (age > 6)
                Color = new Color((Color.LightBlue.R - age/14), (Color.LightBlue.G - age/14), (Color.LightBlue.B-age/14));
            if (lastLife == true && Value == false)
                age = 0;

            lastLife = Value;
        }

        public void Update2(GameTime gametime)
        {
            
            updateTimer += gametime.ElapsedGameTime;
            if (updateTimer.TotalMilliseconds < 4000f / Game1.UPS)
            {
                
                Color = Color.Yellow;
            }
            else
            {
                Color = Color.LightBlue;
                
            }
        }
        public void Update(MouseState mouseState)
        {
            if( Bounds.Contains(new Point(mouseState.X, mouseState.Y)))
            {
                // Make cells come alive with left-click, or kill them with right-click.
                if (mouseState.LeftButton == ButtonState.Pressed)
                    IsAlive = true;
                else if (mouseState.RightButton == ButtonState.Pressed)
                    IsAlive = false;
            }
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            

            
            
            
            if(IsAlive)
                spriteBatch.Draw(Game1.Pixel, Bounds, Color);
            // Don't draw anything if it's dead, since the default background color is white.
        }
    }

}
