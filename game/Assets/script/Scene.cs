using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;


public class Scene
{
    [UnityEngine.SerializeField]
    class sysncData
    {
		public int PosX;
		public int PosY;
		public int PlayerId;
		public int TimeStep;
		public float[] Rotation;
	}

	[UnityEngine.SerializeField]
	class syncNewPlayer
	{
		public int PlayerId;
	}


    Dictionary<int, Player> players = new Dictionary<int, Player>();
	MainPlayer mainPlayer;
	int mainPlayerId;

    public void Init()
    {
        MessageMgr.Instance().AddAction(proto.S2C_SYNCPOS, new System.Action<MessageData>(x => {
            onSync(x);
        }));


		MessageMgr.Instance().AddAction(proto.S2C_ADDMAINPLAYER, new System.Action<MessageData>(x => {
			AddMainPlayer(x);
		}));
	}

    public void AddNewPalyer(int id, float x, float y)
    {
        RemotePlayer p = new RemotePlayer();
		p.Create(id, false);
        players[id] = p;
    }


    public void RemovePlayer(int id)
    {
        players.Remove(id);
    }


    public void onSync(MessageData jsonData)
    {
        sysncData data = (sysncData)UnityEngine.JsonUtility.FromJson(jsonData.data, typeof(sysncData));
		Debug.Log(jsonData.data);
        if (players.ContainsKey(data.PlayerId) == false)
        {
            AddNewPalyer(data.PlayerId, data.PosX, data.PosY);
        }
        else
        {
			if (data.PlayerId != mainPlayerId)
			{
				Player p = players[data.PlayerId];
				RemotePlayer remotePlayer = (RemotePlayer)p;
				Quaternion q = new Quaternion(data.Rotation[0], data.Rotation[1], data.Rotation[2], data.Rotation[3]);
				remotePlayer.Sync(data.PosX, data.PosY, data.TimeStep, q);
			}
        }

		Debug.Log("sync player:" + data.PlayerId + " MainPlayer id is:" + mainPlayerId);
    }


	public void AddMainPlayer(MessageData jsonData)
	{
		syncNewPlayer data = (syncNewPlayer)UnityEngine.JsonUtility.FromJson(jsonData.data, typeof(syncNewPlayer));
		int playerId = data.PlayerId;
		mainPlayerId = playerId;
		mainPlayer = new MainPlayer();
		mainPlayer.playerId = mainPlayerId;
		mainPlayer.Create(mainPlayerId, true);
		mainPlayer.SetPosition(0f, 0f);
		players[playerId] = mainPlayer;
	}


	public MainPlayer GetMainPlayer()
	{
		return mainPlayer;
	}
}
