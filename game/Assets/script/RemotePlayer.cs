using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Snapshot
{
	public int timestamp;
	public int posX;
	public int posY;
	public Quaternion rotation;
	public bool isStateSnapshot;
	public string state;
}

class RemotePlayer : Player
{
	private RemoteMoveController remoteController;
	private Queue<Snapshot> snapshots = new Queue<Snapshot>();
	protected override void onCreate()
	{
		base.onCreate();
		MoveCompent compent = playerObj.AddComponent<MoveCompent>();
		remoteController = new RemoteMoveController(compent, playerObj.GetComponent<Animator>());
		animatiorController = playerObj.GetComponent<Animator>();
	}


	public void Sync(int posx, int posy, int timeStamp, Quaternion rotation)
	{
		//Debug.Log("sync rotation:" + rotation.ToString());
		SetPosition(posx, posy);
		Snapshot s = new Snapshot();
		s.posX = posx;
		s.posY = posy;
		s.timestamp = timeStamp;
		s.rotation = rotation;
		s.isStateSnapshot = false;

		snapshots.Enqueue(s);
	}


	public override void Update()
	{
		int timestampNow = GameTime.GetTimeStamp();
		if (snapshots.Count > 0)
		{
			if (snapshots.Peek().timestamp < timestampNow)
			{
				Snapshot s = snapshots.Dequeue();
				if (s.isStateSnapshot)
				{
					if (s.state == "run")
					{
						onStartMove();
					}
					else if (s.state == "idel")
					{
						onEndMove();
					}
				}
				else
				{
					remoteController.Move(s.rotation, new Vector3(s.posX, 0.0f, s.posY));
				}
			}
		}
	}


	public void SyncState(string state, int timeStamp)
	{
		Snapshot s = new Snapshot();
		s.state = state;
		s.timestamp = timeStamp;
		s.isStateSnapshot = true;
		snapshots.Enqueue(s);
	}
}
