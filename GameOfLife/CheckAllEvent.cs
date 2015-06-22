using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsBase;

namespace GameOfLife
{
	class CheckAllEvent : DEVS.ModelEvent
	{
		GameField field;
 
		public CheckAllEvent( GameField f )
		{
			field = f;
		}

		public override void Execute()
		{
			for ( int x = 0; x < field.Height; ++x )
			{
				for ( int y = 0; y < field.Width; ++y )
				{
					if ( field.isChanged(x, y) )
					{
						for ( int i = -1; i <= 1; ++i )
						{
							for ( int j = -1; j <= 1; ++j )
							{
								UpdateCellEvent EV = new UpdateCellEvent( field, x+i, y+j );
								EV.eTime = eTime + 1;
								DEVS.ModelEvent.Enque( EV );
							}
						}
					}
				}
			}
			field.Swap();
		}
	}
}
