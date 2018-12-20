using UnityEngine;
using System;
using System.Text;

public class MainPlayer : Player
{
	public int playerId;
	public bool needMove = false;
	public float minDelta = 0.01f;
	public float speed = 1.0f;
	private Animator animatiorController;
	float deltaX;
	float deltaY;
	Vector3 moveDelta;


	public class SyncPos
	{
		public string Module;
		public int PlayerId;
		public int PosX;
		public int PosY;
		public string TimeStep;
	}

	protected override void onCreate()
	{
		CameraController controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
		controller.SetTarget(playerObj);

		animatiorController = playerObj.GetComponent<Animator>();
	}


	public void Move(float deltax, float deltay)
	{
		this.deltaX = deltax;
		this.deltaY = deltay;
		this.x = this.x + deltax;
		this.y = this.y + deltay;

		onStartMove();
		syncToServer();
	}


	private void syncToServer()
	{
		SyncPos data = new SyncPos();
		data.PosX = (int)x;
		data.PosY = (int)y;
		data.Module = "scene";
		data.TimeStep = "0";
		data.PlayerId = playerId;

		string tdata = JsonUtility.ToJson(data);
		byte[] bytes = Encoding.ASCII.GetBytes(tdata);
		NetClient.Instance().Send(proto.C2S_SYNCPOS, bytes);
	}


	void onStartMove()
	{
		needMove = true;
		animatiorController.SetBool("idel", false);
		animatiorController.SetBool("run", true);
	}


	void onEndMove()
	{
		Debug.Log("end run");
		animatiorController.SetBool("idel", true);
		animatiorController.SetBool("run", false);
	}


	public void Update(float deltaTime)
	{
		if (needMove)
		{
			needMove = false;
			float delta = deltaTime * speed;
			moveDelta.y = 0.0f;
			if (deltaX < 0 && deltaX > -minDelta)
			{
				deltaX += delta;
				moveDelta.x = -delta;
				needMove = true;
			}
			else if (deltaX> 0 && deltaX > minDelta)
			{
				deltaX -= delta;
				moveDelta.x = delta;
				needMove = true;
			}


			if (deltaY < 0 && deltaY > -minDelta)
			{
				deltaY += delta;
				moveDelta.z = -delta;
				needMove = true;
			}
			else if (deltaY > 0 && deltaY > minDelta)
			{
				deltaY -= delta;
				moveDelta.z = delta;
				needMove = true;
			}

			if (needMove)
			{
				playerObj.transform.position += moveDelta;
			}
			else
			{
				onEndMove();
			}
		}
	}
}
