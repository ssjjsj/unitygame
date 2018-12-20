using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Camera mainCamera;
	private GameObject target;
	private Vector3 offset;

	public void SetTarget(GameObject target)
	{
		offset = new Vector3(0.0f, 20.0f, -20.0f);
		this.target = target;
		transform.Rotate(45.0f, 0.0f, 0.0f);
	}


	private void LateUpdate()
	{
		if (target != null)
		{
			Vector3 targetPos = target.transform.position + offset;
			//transform.position = targetPos + offset;
			transform.position = Vector3.Lerp(transform.position, targetPos, 3 * Time.deltaTime);
		}
	}
}
