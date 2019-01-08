using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MoveCompent : MonoBehaviour
{
	private float speed = 0.1f;
	private float targetPosX;
	private float targetPosY; 

	private bool canMove = false;
	private void step(Quaternion rotation)
	{
		//rotation.Normalize();
		transform.rotation = rotation;
	}

	public void Move(Quaternion rotation, Vector3 targetPos)
	{
		canMove = true;
		targetPosX = targetPos.x;
		targetPosY = targetPos.z;
		step(rotation);
	}

	public void MoveImmediately(Quaternion rotation, Vector3 targetPos)
	{
		transform.rotation = rotation;
		transform.position = targetPos;
	}

	public void Stop()
	{
		canMove = false;
	}


	private void Update()
	{
		if (canMove)
		{
			float delta = Time.deltaTime;
			float posX = transform.position.x + transform.forward.x * speed;
			float posZ = transform.position.z + transform.forward.z * speed;
			transform.position += new Vector3(posX, 0, posZ);

			if (Mathf.Abs(posX - targetPosX) < 0.001f && Mathf.Abs(posZ-targetPosY) < 0.001f)
			{
				canMove = false;
			}
		}
	}
}
