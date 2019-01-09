using System;
using UnityEngine;
using System.Collections.Generic;

public class RemoteMoveController
{
	private MoveCompent moveCompent;

	public RemoteMoveController(MoveCompent compent, Animator controller)
	{
		moveCompent = compent;
	}


	public void Move(Quaternion rotation, Vector3 targetPos)
	{
		moveCompent.Move(rotation, targetPos);
	}
}