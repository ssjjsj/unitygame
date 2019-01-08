using System;
using UnityEngine;
using System.Collections.Generic;

public class Snapshot
{
	public int timestamp;
	public int posX;
	public int posY;
	public Quaternion rotation;
}

public class RemoteMoveController
{
	private Queue<Snapshot> snapshots = new Queue<Snapshot>();
	private MoveCompent moveCompent;

	public RemoteMoveController(MoveCompent compent)
	{
		moveCompent = compent;
	}


	public void AddSnapshot(int timestamp, int posx, int posy, Quaternion rotation)
	{
		Snapshot s = new Snapshot();
		s.timestamp = timestamp;
		s.posX = posx;
		s.posY = posy;
		s.rotation = rotation;

		snapshots.Enqueue(s);
	}

	public void Update(int timestampNow)
	{
		if (snapshots.Count > 0)
		{
			if (snapshots.Peek().timestamp < timestampNow)
			{
				Snapshot s = snapshots.Dequeue();
				moveCompent.Move(s.rotation, new Vector3(s.posX, 0.0f, s.posY));
			}
		}
	}
}