using UnityEngine;
using System;
using System.Text;

public class MainPlayer : Player
{
	public int playerId;
	private Animator animatiorController;
	private MoveCompent moveCompent;

	public class SyncPos
	{
		public string Module;
		public int PlayerId;
		public int PosX;
		public int PosY;
		public int TimeStamp;
		public float[] Rotation;
	}

	protected override void onCreate()
	{
		CameraController controller = GameObject.Find("Main Camera").GetComponent<CameraController>();
		controller.SetTarget(playerObj);

		animatiorController = playerObj.GetComponent<Animator>();
		moveCompent = playerObj.AddComponent<MoveCompent>();
	}


	public void Move(float deltax, float deltay, Quaternion rotation, bool needMove)
	{
		if (needMove)
		{
			this.x = this.x + deltax;
			this.y = this.y + deltay;

			onStartMove();
			syncToServer();

			moveCompent.MoveImmediately(rotation, new Vector3(this.x, 0.0f, this.y));
		}
		else
		{
			onEndMove();
		}
	}


	private void syncToServer()
	{
		SyncPos data = new SyncPos();
		data.PosX = (int)x;
		data.PosY = (int)y;
		data.Module = "scene";
		data.TimeStamp = GameTime.GetTimeStamp();
		data.PlayerId = playerId;
		Quaternion rotation = playerObj.transform.rotation;
		data.Rotation = new float[4];
		data.Rotation[0] = rotation.x;
		data.Rotation[1] = rotation.y;
		data.Rotation[2] = rotation.z;
		data.Rotation[3] = rotation.w;

		string tdata = JsonUtility.ToJson(data);
		Debug.Log("sync data:" + tdata);
		byte[] bytes = Encoding.ASCII.GetBytes(tdata);
		NetClient.Instance().Send(proto.C2S_SYNCPOS, bytes);
	}


	void onStartMove()
	{
		animatiorController.SetBool("idel", false);
		animatiorController.SetBool("run", true);
	}


	void onEndMove()
	{
		animatiorController.SetBool("idel", true);
		animatiorController.SetBool("run", false);
	}
}
