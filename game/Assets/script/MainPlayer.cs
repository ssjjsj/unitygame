using UnityEngine;
using System;
using System.Text;

public class MainPlayer : Player
{
	public int playerId;
	private float deltaTimeSinceLastUpdate = 0.0f;
	private MoveCompent moveCompent;
	private bool needSync = false;
	private string curState = "";

	public class SyncPos
	{
		public string Module;
		public int PlayerId;
		public int PosX;
		public int PosY;
		public int TimeStamp;
		public float[] Rotation;
	}


	public class SyncState
	{
		public string Module;
		public int PlayerId;
		public string State;
		public int TimeStamp;
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
			needSync = true;
			this.x = this.x + deltax;
			this.y = this.y + deltay;

			if (curState != "run")
			{
				curState = "run";
				syncStateToServer("run");
				onStartMove();
			}


			moveCompent.MoveImmediately(rotation, new Vector3(this.x, 0.0f, this.y));
		}
		else
		{
			if (curState != "idel")
			{
				syncStateToServer("idel");
				onEndMove();
				curState = "idel";
			}
		}
	}


	private void syncStateToServer(string state)
	{
		SyncState data = new SyncState();
		data.Module = "scene";
		data.TimeStamp = GameTime.GetTimeStamp();
		data.PlayerId = playerId;
		data.State = state;

		string tdata = JsonUtility.ToJson(data);
		//Debug.Log("sync data:" + tdata);
		byte[] bytes = Encoding.ASCII.GetBytes(tdata);
		NetClient.Instance().Send(proto.C2S_SYNCSTATE, bytes);

		needSync = false;
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
		//Debug.Log("sync data:" + tdata);
		byte[] bytes = Encoding.ASCII.GetBytes(tdata);
		NetClient.Instance().Send(proto.C2S_SYNCPOS, bytes);
	}


	public override void Update()
	{
		if (needSync == false)
			return;

		deltaTimeSinceLastUpdate += Time.deltaTime*1000;

		if (deltaTimeSinceLastUpdate > 1000.0f/33.0f)
		{
			syncToServer();
			deltaTimeSinceLastUpdate = 0.0f;
			//Debug.Log("sync to server");
		} 
	}
}
