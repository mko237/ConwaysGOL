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
    class Grid
    {
        public Point Size { get; private set; }
        private Cell[,] cells;
        private bool[,] nextCellStates;
        private TimeSpan updateTimer;
        private Random rand = new Random();
        private bool randbool;
       


        public Grid()
        {
            nextCellStates = new bool[Game1.CellsX, Game1.CellsY];
            Size = new Point(Game1.CellsX, Game1.CellsY);
            cells = new Cell[Size.X, Size.Y];
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {  
                    int r = rand.Next(0, 256);
                    int g = rand.Next(0, 256);
                    int b = rand.Next(0, 256);
                    int a = 255;//rand.Next(0, 256);
                    Color color = Color.LightBlue;
                    cells[i, j] = new Cell(new Point(i, j),color);
                }
            }
            //Random start
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    cells[i, j].IsAlive = true;
                    randbool = (rand.NextDouble() > 0.5) ? true : false;
                    if (randbool)//((50 < i) && (i < 250)) || ((350 < i) && (i < 550)))
                    {
                        randbool = (rand.NextDouble() > 0.5) ? true : false;
                        if (randbool)
                        {
                            randbool = (rand.NextDouble() > 0.5) ? true : false;
                            cells[i, j].IsAlive = true;
                        }
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();
            //random spawn on r key
            if(keyState.IsKeyDown(Keys.R ))
            {
                for (int i = 0; i < Size.X; i++)
                {
                    for (int j = 0; j < Size.Y; j++)
                    {
                        randbool = (rand.NextDouble() > 0.5) ? true : false;
                        if (true)//((50 < i) && (i < 250)) || ((350 < i) && (i < 550)))
                        {
                            randbool = (rand.NextDouble() > 0.5) ? true : false;
                            if (true)
                            {
                                randbool = (rand.NextDouble() > 0.5) ? true : false;
                                cells[i, j].IsAlive = randbool;
                               
                                                               
                                                                
                            }
                        }
                    }
                }
            }
            foreach (Cell cell in cells)
                cell.Update(mouseState);

            if (Game1.Paused)
                return;

            updateTimer += gameTime.ElapsedGameTime;



            if (updateTimer.TotalMilliseconds > 1000f / Game1.UPS)
            {
                updateTimer = TimeSpan.Zero;
                // Loop through every cell on the grid.

                int ii = 0;


                for (int i = 0; i < Size.X; i++)
                {
                    for (int j = 0; j < Size.Y; j++)
                    {
                        bool living = cells[i, j].IsAlive;
                        int count = GetLivingNeighbors(i, j);
                        bool result = false;
                        
                        

                        // Apply the rules and set the next state.
                        if (living && count < 6)
                            result = false;
                        if (living && (count == 3 || count == 2))
                            result = true;
                       // if (living && count > 3)
                        //    result = false;
                        //if (!living && count == 3)
                          //  result = true;
                        if (!living && count == 5)
                            result = true;

                        nextCellStates[i, j] = result; //determines if the cell will be alive next state

                        //random chance cell comes alive 
                        
                        //int randi = rand.Next(0, Size.X-(Size.X/2));
                        //int randj = rand.Next(0, Size.Y);

                        //int randii = rand.Next(0, Size.X);
                        //int randjj = rand.Next(0, Size.Y);
                        //ii++;
                        //if (ii % 25 == 0)
                        //{
                        //    ii = 0;
                        //    cells[randi, randj].toggleLife(true);
                        //}
                        //if (ii % 75 == 0)
                        //{
                        //    ii = 0;
                        //    cells[randii, randjj].toggleLife(true);
                        //}
                            
                        
                        
                        
                        
                    }
                }

                SetNextState(); //applies the next state of each cell
            }

        }

        public int GetLivingNeighbors(int x, int y)
        {
            int count = 0;

            //Check cell on the right.
            if (x != Size.X - 1)
            {
                if (cells[x + 1, y].IsAlive)
                    count++;
            }
            if (x == Size.X - 1) //w
            {
                if (cells[0, y].IsAlive)
                    count++;
                if (y != 0 && y != Size.Y - 1)
                {
                    if (cells[0, y - 1].IsAlive)
                        count++;
                    if (cells[0, y + 1].IsAlive)
                        count++;
                }
                
            }
            //comment out all else if statements to stop updates from wrapping
            // Check cell on the bottom right.
            if (x != Size.X - 1 && y != Size.Y - 1)
            {
                if (!(x == Size.X - 1 && y == 0) || !(x == 0 && y == 0) || !(x == 0 && y == Size.Y - 1))
                {
                    if (cells[x + 1, y + 1].IsAlive)
                        count++;
                }
            }
            if (x == Size.X - 1 && y == Size.Y - 1) //w
            {
                //otehr corners
                if (cells[0, Size.Y - 1].IsAlive)
                    count++;
                if (cells[0, 0].IsAlive)
                    count++;
                if (cells[Size.X - 1, 0].IsAlive)
                    count++;
               //adjacents
                if (cells[x - 1, y - 1].IsAlive)
                    count++;
                if (cells[x - 1, y].IsAlive)
                    count++;
                if (cells[x, y - 1].IsAlive)
                    count++;
            }
            // Check cell on the bottom.
            if (y != Size.Y - 1)
            {
                if (cells[x, y + 1].IsAlive)
                    count++;
            }
            if (y == Size.Y - 1) //w
            {
                if (cells[x, 0].IsAlive)
                    count++;
                if (x != 0 && x != Size.X - 1)
                {
                    if (cells[x - 1, 0].IsAlive)
                        count++;
                    if (cells[x + 1, 0].IsAlive)
                        count++;
                }

            }
            // Check cell on the bottom left.
            if (x != 0 && y != Size.Y - 1)
            {
                if (!(x == Size.X - 1 && y == 0) || !(x == 0 && y == 0) || !(x == Size.X - 1 && y == Size.Y - 1))
                {
                    if (cells[x - 1, y + 1].IsAlive)
                        count++;
                }
            }
            if (x == 0 && y == Size.Y - 1) //w
            {
                //other corners
                if (cells[Size.X-1, 0].IsAlive)
                    count++;
                if (cells[0, 0].IsAlive)
                    count++;
                if (cells[Size.X - 1, Size.Y - 1].IsAlive)
                    count++;
                //adjacents
                if (cells[x + 1 , y - 1].IsAlive)
                    count++;
                if (cells[x + 1, y].IsAlive)
                    count++;
                if (cells[x, y - 1].IsAlive)
                    count++;
            }
            // Check cell on the left.
            if (x != 0)
            {
                if (cells[x - 1, y].IsAlive)
                    count++;
            }
            if (x == 0) //w
            {
                if (cells[Size.X-1, y].IsAlive)
                    count++;
                if (y != 0 && y != Size.Y - 1)
                {
                    if (cells[Size.X - 1, y - 1].IsAlive)
                        count++;
                    if (cells[Size.X - 1, y + 1].IsAlive)
                        count++;
                }

            }
            // Check cell on the top left.
            if (x != 0 && y != 0)
            {
                if (!(x == Size.X - 1 && y == 0) || !(x == 0 && y == Size.Y - 1) || !(x == Size.X - 1 && y == Size.Y - 1))
                {
                    if (cells[x - 1, y - 1].IsAlive)
                        count++;
                }
                
            }
            if (x == 0 && y == 0) //w corner
            {
                // other corners
                if (cells[0, Size.Y - 1].IsAlive)
                    count++;
                if (cells[Size.X - 1, 0].IsAlive)
                    count++;
                if (cells[Size.X - 1, Size.Y - 1].IsAlive)
                    count++;
                //adjacents
                if (cells[x + 1, y + 1].IsAlive)
                    count++;
                if (cells[x + 1, y].IsAlive)
                    count++;
                if (cells[x, y + 1].IsAlive)
                    count++;
                
            }
            // Check cell on the top.
            if (y != 0)
            {
                
            }
            if (y == 0) //w
            {
                if (cells[x, Size.Y-1].IsAlive)
                    count++;
                if (x != 0 && x != Size.X - 1)
                {
                    if (cells[x - 1, Size.Y-1].IsAlive)
                        count++;
                    if (cells[x + 1, Size.Y-1].IsAlive)
                        count++;
                }
            }
            // Check cell on the top right.
            if (x != Size.X - 1 && y != 0)
            {
                if (!(x == 0 && y == 0) || !(x == 0 && y == Size.Y - 1) || !(x == Size.X - 1 && y == Size.Y - 1))
                {
                    if (cells[x + 1, y - 1].IsAlive)
                        count++;
                }
            }
            if (x == Size.X - 1 && y == 0) //w
            {
                //other corners
                if (cells[0, Size.Y-1].IsAlive)
                    count++;
                if (cells[0, 0].IsAlive)
                    count++;
                if (cells[Size.X-1, Size.Y-1].IsAlive)
                    count++;
                
                //corner adjacents
                if (cells[x - 1, Size.Y - 1].IsAlive)
                    count++;
                if (cells[0, 1].IsAlive)
                    count++;

                //adjacents
                if (cells[x - 1, y + 1].IsAlive)
                    count++;
                if (cells[x - 1, y].IsAlive)
                    count++;
                if (cells[x, y + 1].IsAlive)
                    count++;
            }

            return count;
        }
        public void Clear()
        {
            for (int i = 0; i < Size.X; i++)
                for (int j = 0; j < Size.Y; j++)
                    nextCellStates[i, j] = false;

            SetNextState();
        }

        public void SetNextState()
        {
            for (int i = 0; i < Size.X; i++)
                for (int j = 0; j < Size.Y; j++)
                    cells[i, j].toggleLife(nextCellStates[i, j]);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Cell cell in cells)
                cell.Draw(spriteBatch);

            //draw vertical gridlines.
            for (int i = 0; i < Size.X; i++)
                spriteBatch.Draw(Game1.Pixel, new Rectangle(i * Game1.CellSize - 1, 0, 1, Size.Y * Game1.CellSize), Color.DarkGoldenrod);
            //draw horizontal gridlines.
            for (int j = 0; j < Size.Y; j++)
                spriteBatch.Draw(Game1.Pixel, new Rectangle(0, j * Game1.CellSize - 1, Size.X * Game1.CellSize,1), Color.DarkGoldenrod);
            

        }
    }
}
