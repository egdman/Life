using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EventsBase;

namespace GameOfLife
{
	public class UpdateCellEvent : LocalEvent
	{

		public static HashSet<Point> AddedEvents = new HashSet<Point>();
		GameField field;


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


		public override bool Remove()
		{
			return UpdateCellEvent.AddedEvents.Remove(new Point(X, Y));
		}


		public UpdateCellEvent( GameField f, int X, int Y ) : base( X, Y )
		{
			field = f;
		}


		public override void Execute()
		{
			AddedEvents.Remove( new Point(X, Y) );
			bool alive = field.GetCell(X, Y);
			if ( alive )
			{
				field.SetCell(X, Y, updateAlive() );
			}
			else
			{
				field.SetCell(X, Y, updateDead() );
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
			ScheduleUpdateEvent.Add( field, X, Y, DEVS.GlobalTime + 1 );
        }


		int countAliveNeighbours()
		{
			int alive = 0;
			for ( int i = -1; i <= 1; ++i )
			{
				for ( int j = -1; j <= 1; ++j )
				{
					if ( field.GetCell( X+i, Y+j ) )
					{
						++alive;
					}
				}
			}
			return alive;
		}

	}
}
