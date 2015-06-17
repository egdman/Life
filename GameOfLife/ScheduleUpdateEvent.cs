using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsBase;

namespace GameOfLife
{
	public class ScheduleUpdateEvent : DEVS.ModelEvent
	{
		public static HashSet<Tuple<int, int>> AddedEvents = new HashSet<Tuple<int, int>>();
		GameField field;
		int x;
		int y;

		public ScheduleUpdateEvent(GameField f, int X, int Y)
		{
			field = f;
			x = X;
			y = Y;
		}

		public override void Execute()
		{
			for (int i = -1; i <= 1; ++i)
			{
				for (int j = -1; j <= 1; ++j)
				{
					if (!AddedEvents.Contains(new Tuple<int, int>(x + i, y + j)))
					{
						var ev = new UpdateCellEvent(field, x + i, y + j);
						ev.eTime = DEVS.GlobalTime;
						DEVS.ModelEvent.Enque(ev);
						AddedEvents.Add(new Tuple<int, int>(x + i, y + j));
					}
				}
			}
		}

	}
}
