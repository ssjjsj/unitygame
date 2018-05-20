using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;


public class Scene
{
    [UnityEngine.SerializeField]
    class sysncData
    {
        public int playerId;
        public float x;
        public float y;
    }
    Dictionary<int, Player> players = new Dictionary<int, Player>();

    public void Init()
    {
        MessageMgr.Instance().AddAction(1, new System.Action<MessageData>(x => {
            onSync(x);
        }));
    }

    public void AddNewPalyer(int id, float x, float y)
    {
        Player p = new Player();
        players[id] = p;
    }


    public void RemovePlayer(int id)
    {
        players.Remove(id);
    }


    public void onSync(object jsonData)
    {
        sysncData data = (sysncData)UnityEngine.JsonUtility.FromJson((string)jsonData, typeof(sysncData));
        if (players.ContainsKey(data.playerId) == false)
        {
            AddNewPalyer(data.playerId, data.x, data.y);
        }
        else
        {
            Player p = players[data.playerId];
            p.SetPosition(data.x, data.y);
        }
    }

}
