using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MoveCompent : MonoBehaviour
{
	private float speed = 0.03f;
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

		//Debug.Log(string.Format("targetpos:{0}, {1}", targetPosX, targetPosY));
		//Debug.Log(targetPosY);
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


	public bool IsMove()
	{
		return canMove;
	}


	private void Update()
	{
		if (canMove)
		{
			float delta = Time.deltaTime;
			Vector3 forward = transform.forward;
			forward.Normalize();
			float originX = transform.position.x;
			float originY = transform.position.z;
			float posX = originX + forward.x * speed;
			float posY = originY + forward.z * speed;
			//Debug.Log(string.Format("set postion: {0}, {1}, {2}, {3}", posX, posY, forward.x*speed, forward.z*speed));
			

			if ((posX-targetPosX)*(targetPosX-originX) <= 0 && (posY - targetPosY) * (targetPosY - originY) <= 0)
			{
				transform.position = new Vector3(targetPosX, 0, targetPosY);
				canMove = false;
			}
			else
			{
				transform.position = new Vector3(posX, 0, posY);
			}
		}
	}
}
