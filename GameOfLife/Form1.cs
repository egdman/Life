﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using EventsBase;

namespace GameOfLife
{
	public partial class Form1 : Form
	{

		GameField field;
		CustomBitmap graphics;
		HashSet<Point> newCells;


		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			field = new GameField( 60, 36 );
			newCells = new HashSet<Point>();
			InitializeGraphics();
		}
		
		public void InitializeGraphics()
        {
            graphics = new CustomBitmap(GraphicsBox);
            graphics.DrawField(field);           
        }
  
        public int rgbaToColor(byte r, byte g, byte b, byte a)
        {
            return r + (g << 8) + (b << 16) + (a << 24);
        }


		private void GraphicsBox_Click(object sender, EventArgs e)
		{
			
		}

		private void GraphicsBox_MouseHover(object sender, MouseEventArgs e)
		{
			Point loc = e.Location;
			int x = loc.X;
			int y = loc.Y;
			x /= 10;
			y /= 10;
			graphics.Clear();
			graphics.DrawSquare(x, y);
			graphics.DrawField(field);
			graphics.DrawNewCells( newCells );
			graphics.Refresh();
		}


		private void GraphicsBox_MouseDown(object sender, MouseEventArgs e)
		{
			Point loc = e.Location;
			int x = loc.X;
			int y = loc.Y;
			x /= 10;
			y /= 10;
			loc.X = x;
			loc.Y = y;
			if ( !newCells.Remove( loc ) )
			{
				newCells.Add( loc );
			}

			graphics.DrawNewCells( newCells );
			graphics.Refresh();
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			if ( newCells.Count > 0 )
			{
				foreach( var cell in newCells )
				{
					field.SetCell( cell.X, cell.Y, true );
				}
				newCells.Clear();
			}

			DEVS.AddStartEvent( new CheckAllEvent( field ) );
			DEVS.ProcessAllEvents();
			graphics.Clear();
			graphics.DrawField(field);
			graphics.Refresh();
		}


		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Space )
			{
				NextButton_Click(sender, e);
			}
		}
    }

  
    public class CustomBitmap
    {
        private PictureBox holder;
        private Bitmap frontBitmap;

        public CustomBitmap(PictureBox holder)
        {
            this.holder = holder;
            frontBitmap = new System.Drawing.Bitmap(holder.Width, holder.Height, PixelFormat.Format32bppArgb);
        }

        public int GetDataSize()
        {
            return holder.Width * holder.Height;
        }


		public void Refresh()
		{
			holder.Image = frontBitmap;
		}

        public void DrawField( GameField field )
        {           
			using( Graphics g = Graphics.FromImage( frontBitmap ) )
			{
				drawField( field, g );
			}
        }


		public void DrawSquare( int x, int y )
		{
			using( Graphics g = Graphics.FromImage( frontBitmap ) )
 			{
				g.DrawRectangle( Pens.Red, x * 10, y * 10, 10, 10 );
			}
		}


		public void DrawNewCells( HashSet<Point> cells )
		{
			if ( cells.Count <= 0 )
			{
				return;
			}
			using( Graphics g = Graphics.FromImage( frontBitmap ) )
 			{
				foreach( var cell in cells )
				{
					g.DrawRectangle( Pens.Red, cell.X*10, cell.Y*10, 10, 10 );
				}
			}
		}

		public void Clear()
		{
			using( Graphics g = Graphics.FromImage( frontBitmap ) )
 			{
				g.Clear( Color.White );
			}
		}

		void drawField( GameField field, Graphics gr )
		{
			gr.DrawRectangle( Pens.Green, 0, 0, 10 * field.Height, 10 * field.Width );

			for ( int x = 0; x < field.Height; ++x )
			{
				for ( int y = 0; y < field.Width; ++y )
				{
					if ( field.GetCell( x, y ) )
					{
						gr.DrawRectangle( Pens.Black, x * 10, y * 10, 10, 10 );
					}
				}
			}
		}



	}
}
