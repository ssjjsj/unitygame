using UnityEngine;
using System;
using System.Text;

public class MainPlayer : Player
{
	public int playerId;
	public class SyncPos
	{
		public string Module;
		public int PlayerId;
		public int PosX;
		public int PosY;
		public string TimeStep;
	}


	public void Move(float deltax, float deltay)
	{
		this.x = this.x + deltax;
		this.y = this.y + deltay;

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
}
