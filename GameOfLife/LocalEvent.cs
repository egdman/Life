using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsBase;

namespace GameOfLife
{
	public abstract class LocalEvent : DEVS.ModelEvent
	{
		public int X
		{
			get;
			set;
		}

		public int Y
		{
			get;
			set;
		}
		
		public abstract bool Remove();

		public LocalEvent(int x, int y)
		{
			X = x;
			Y = y;
		}

	}
}
