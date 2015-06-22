using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
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



        public DeleteEvents(GameField f)
		{
			field = f;
		}

		public override void Execute()
		{
			List<LocalEvent> eventsToDelete = new List<LocalEvent>();

			foreach ( var ev in DEVS.EventQueue )
			{
				bool deleteThis = true;
				if (ev is LocalEvent)
				{
					var locEv = (LocalEvent)ev;
					int x = locEv.X;
					int y = locEv.Y;

					for (int i = -1; i <= 1; ++i)
					{
						for (int j = -1; j <= 1; ++j)
						{
							if (field.isChanged(x + i, y + j))
							{
								deleteThis = false;
							}
						}
					}
					if (deleteThis)
					{
						eventsToDelete.Add( locEv );
	
					}
				}
			}

			foreach (var locEv in eventsToDelete)
			{
				DEVS.ModelEvent.Delete(locEv.X, locEv.Y);
				locEv.Remove();
			}
		}


    }
}
