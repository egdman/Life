using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsBase;

namespace GameOfLife
{
    public class DeleteEvents : DEVS.ModelEvent
    {
        GameField field;

        public static bool Add(GameField f, double time)
        {
			var ev = new DeleteEvents(f);
			ev.eTime = time;
			DEVS.ModelEvent.Enque(ev);
			return true;
        }



        public DeleteEvents(GameField f) : base (-1, -1)
		{
			field = f;
		}

		public override void Execute()
		{
			foreach (var updEv in UpdateCellEvent.AddedEvents)
			{
				bool deleteThis = true;
				int x = updEv.X;
				int y = updEv.Y;
				for (int i = -1; i <= 1; ++i)
				{
					for (int j = -1; j <= 1; ++j)
					{
						if (field.isChanged(x+i, y+j))
						{
							deleteThis = false;
						}
					}
				}
				if (deleteThis) { DEVS.ModelEvent.Delete(updEv.X, updEv.Y); }
			}
		}


    }
}
