using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EventsBase;

namespace GameOfLife
{
	public class ScheduleUpdateEvent : DEVS.ModelEvent
	{
		public static HashSet<Point> AddedEvents = new HashSet<Point>();
		GameField field;


		public static bool Add(GameField f, int X, int Y, double time)
		{
			f.CheckBorders(ref X, ref Y);
			if (!AddedEvents.Contains(new Point(X, Y)))
			{
				var ev = new ScheduleUpdateEvent(f, X, Y);
				ev.eTime = time;
				DEVS.ModelEvent.Enque(ev);
				AddedEvents.Add(new Point(X, Y));
				return true;
			}
			return false;
		}


		public ScheduleUpdateEvent(GameField f, int X, int Y) : base( X, Y )
		{
			field = f;
		}

		public override void Execute()
		{
			AddedEvents.Remove(new Point(X, Y));
			for (int i = -1; i <= 1; ++i)
			{
				for (int j = -1; j <= 1; ++j)
				{
					UpdateCellEvent.Add( field, X + i, Y + j, DEVS.GlobalTime );
				}
			}
		}

	}
}
