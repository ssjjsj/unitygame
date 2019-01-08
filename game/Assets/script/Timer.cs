using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameTime
{
	private static int timeDelta = 0;
	public static int GetTimeStamp()
	{
		return System.DateTime.Now.Millisecond + timeDelta;
	}
}
