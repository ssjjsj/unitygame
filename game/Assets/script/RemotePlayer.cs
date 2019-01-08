using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class RemotePlayer : Player
{
	private RemoteMoveController remoteController;
	protected override void onCreate()
	{
		base.onCreate();
		MoveCompent compent = playerObj.AddComponent<MoveCompent>();
		remoteController = new RemoteMoveController(compent);
	}


	public void Sync(int posx, int posy, int timeStamp, Quaternion rotation)
	{
		SetPosition(posx, posy);
		remoteController.AddSnapshot(timeStamp, posx, posy, rotation);
	}
}
