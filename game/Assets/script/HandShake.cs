//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;

//class HandShake
//{
//	class SyncData
//	{
//		public uint ServerTime;
//	}

//	class SendData
//	{
//		bool Send;
//	}

//	private static HandShake instance;
//	public static HandShake Instance()
//	{
//		return instance;
//	}

//	private float deltaSniceLastUpdate = 0.0f;
//	private uint serverTime;


//	public static void AddHandShake()
//	{
//		MessageMgr.Instance().AddAction(proto.S2C_SHAKE, new System.Action<MessageData>(x => {

//		}));
//	}


//	private void onSyncServerTime(MessageData jsonData)
//	{
//		SyncData syncData = (SyncData)UnityEngine.JsonUtility.FromJson(jsonData.data, typeof(SyncData));
//		uint receiveTime = syncData.ServerTime;
//	}


//	public void Update(float deltaTime)
//	{
//		deltaSniceLastUpdate += deltaTime;
//		if (deltaSniceLastUpdate > 1.0f)
//		{
//			SendData data = new SendData();
//			string tdata = JsonUtility.ToJson(data);
//			byte[] bytes = Encoding.ASCII.GetBytes(tdata);
//			NetClient.Instance().Send(proto.C2S_SHAKE, bytes);
//		}
//	}
//}
