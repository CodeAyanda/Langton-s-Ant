using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Langton_sAnt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Grid grid = new Grid();
            Ant ant = new Ant(Grid.numCells/2, Grid.numCells / 2, 4);
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Grid.Show(g);
            Ant.Show(g);


            for (int i = 0; i < 20; i++)
            {

                Ant.Move(Ant.x, Ant.y);

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }

    class Ant
    {
        public static int x;
        public static int y;
        static int dir;
        static int size = 5;

        public Ant(int x_, int y_, int dir_)
        {
            x = x_;
            y = y_;
            dir = dir_;
        }

        public static void Show(Graphics g)
        {
            SolidBrush fill = new SolidBrush(Color.Red);
            Rectangle rect = new Rectangle(x*size, y*size, size, size);
            g.FillRectangle(fill, rect);
        }

        public static void Move(int x, int y)
        {
            x = mod(x, Grid.numCells);
            y = mod(y, Grid.numCells);

            if (Grid.grid[x, y] == 0)
            {
                Grid.grid[x, y] = 1;
                Ant.dir++;
                if (Ant.dir > 4)
                {
                    Ant.dir = 1;
                }
                MoveAnt();

            }
            else if (Grid.grid[x, y] == 1)
            {
                Grid.grid[x, y] = 0;
                Ant.dir--;
                if (Ant.dir < 1)
                {
                    Ant.dir = 4;
                }
                MoveAnt();

            }
        }

        private static void MoveAnt()
        {
            if (dir == 1)
            {
                Ant.y -= 1;
            }
            else if (dir == 2)
            {
                Ant.x += 1;
            }
            else if (dir == 3)
            {
                Ant.y += 1;
            }
            else if (dir == 4)
            {
                Ant.x -= 1;
            }
        }

        public static int mod(int index, int maxSize)
        {
            if (index >= 0)
                return index % maxSize;
            else
                return (maxSize + index % maxSize) % maxSize;
        }

    }

    class Grid
    {
        public static int numCells = 160;
        static int size = 5;
        public static int[,] grid = new int[numCells,numCells];

        public Grid()
        {
            for (int i = 0; i < numCells; i++)
            {
                for (int j = 0; j < numCells; j++)
                {
                    grid[i, j] = 0;
                }
            }
        }

        public static void Show(Graphics g)
        {
            Pen black = new Pen(Color.Black);
            SolidBrush white = new SolidBrush(Color.White);
            SolidBrush blackFill = new SolidBrush(Color.Black);

            for (int i = 0; i < numCells; i++)
            {
                for (int j = 0; j < numCells; j++)
                {
                    Rectangle rect = new Rectangle(i * size, j * size, size+1, size+1);
                    g.DrawRectangle(black, rect);

                    if (grid[i, j] == 0)
                    {
                        g.FillRectangle(white, (i * size)+1, (j * size)+1, size - 2, size - 2);
                    }
                    else
                    {
                        g.FillRectangle(blackFill, (i * size)+1, (j * size)+1, size - 2, size - 2);

                    }

                }
            }
        }

    }
}
