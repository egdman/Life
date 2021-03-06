﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
	public class GameField
	{

		bool[,] field;
		bool[,] oldField;
		int width;
		int height;

		public GameField( int h, int w )
		{
			width		= w;
			height		= h;
			field		= new bool[h, w];
			oldField	= new bool[h, w];
		}


		public void CheckBorders( ref int h, ref int w )
		{
			h = wrap( h, height );
			w = wrap( w, width );
		}


		public bool GetCell( int h, int w )
		{
			CheckBorders( ref h, ref w );
			return oldField[h, w];
		}

		public bool GetRecent( int h, int w )
		{
			CheckBorders( ref h, ref w );
			return field[h, w];
		}


		public void SetCell( int h, int w, bool value )
		{
			CheckBorders( ref h, ref w );
			field[h, w] = value;
		}



		public bool isChanged( int h, int w )
		{
			CheckBorders( ref h, ref w );
			return ( oldField[h, w] != field[h, w] );
		}


		public int Height
		{
			get { return height; } 
		}

		public int Width
		{
			get { return width; } 
		}


		public void Swap()
		{
			var save = field;
			field = oldField;
			oldField = save;
		}


		int wrap( int value, int upperLimit )
		{
			while( value < 0 )
			{
				value += upperLimit;
			}
			while ( value >= upperLimit )
			{
				value -= upperLimit;
			}
			return value;
		}



		public void Clear()
		{
			for ( int x = 0; x < height; ++x )
			{
				for ( int y = 0; y < width; ++y )
				{
					field	[x, y]	= false;
					oldField[x, y]	= false;
				}
			}
		}

	}
}
