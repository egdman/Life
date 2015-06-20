using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EventsBase;

namespace GameOfLife
{
	public class UpdateCellEvent : DEVS.ModelEvent
	{

		public static HashSet<Point> AddedEvents = new HashSet<Point>();
		GameField field;
		int x;
		int y;


		public static bool Add(GameField f, int X, int Y, double time)
		{
			f.CheckBorders( ref X, ref Y );
			if (!AddedEvents.Contains(new Point(X, Y)))
			{
				var ev = new UpdateCellEvent(f, X, Y);
				ev.eTime = time;
				DEVS.ModelEvent.Enque(ev);
				AddedEvents.Add(new Point(X, Y));
				return true;
			}
			return false;
		}

		public UpdateCellEvent( GameField f, int X, int Y )
		{
			field = f;
			x = X;
			y = Y;
		}


		public override void Execute()
		{
			AddedEvents.Remove( new Point(x, y) );
			bool alive = field.GetCell(x, y);
			if ( alive )
			{
				field.SetCell(x, y, updateAlive() );
			}
			else
			{
				field.SetCell(x, y, updateDead() );
			}
		}



		bool updateAlive()
		{
			int alive = countAliveNeighbours();
			alive -= 1; // minus self
			if ( alive > 3 || alive < 2 )
			{
                schedule();
				return false;
			}
			return true;
		}

		bool updateDead()
		{
			int alive = countAliveNeighbours();
			if ( alive == 3 )
			{
                schedule();
				return true;
			}
			return false;
		}


        void schedule()
        {
			ScheduleUpdateEvent.Add( field, x, y, DEVS.GlobalTime + 1 );
        }


		int countAliveNeighbours()
		{
			int alive = 0;
			for ( int i = -1; i <= 1; ++i )
			{
				for ( int j = -1; j <= 1; ++j )
				{
					if ( field.GetCell( x+i, y+j ) )
					{
						++alive;
					}
				}
			}
			return alive;
		}

	}
}
