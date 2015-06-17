using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsBase;

namespace GameOfLife
{
	public class UpdateCellEvent : DEVS.ModelEvent
	{

		GameField field;
		int x;
		int y;

		public UpdateCellEvent( GameField f, int X, int Y )
		{
			field = f;
			x = X;
			y = Y;
		}


		public override void Execute()
		{
			ScheduleUpdateEvent.AddedEvents.Remove( new Tuple<int,int>( x, y ) );
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
            ScheduleUpdateEvent ev = new ScheduleUpdateEvent(field, x, y);
			ev.eTime = DEVS.GlobalTime + 1;
			DEVS.ModelEvent.Enque(ev);
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
