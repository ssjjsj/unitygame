using UnityEngine;
using System.Collections.Generic;
using System;

public class MessageData
{
    public int id;
    public string data;
}


public class MessageMgr
{
    private List<MessageData> msgList = new List<MessageData>();
    private Dictionary<int, System.Action<MessageData>> actionList = new Dictionary<int, Action<MessageData>>();

    private static MessageMgr instance;

    public static MessageMgr Instance()
    {
        if (instance == null)
            instance = new MessageMgr();

        return instance;
    }

    public void AddMsg(MessageData msg)
    {
        lock(msgList)
        {
            msgList.Add(msg);
        }
    }


    public void AddAction(int id, Action<MessageData> action)
    {
        actionList[id] = action;
    }



    public void DispatchMessage()
    {
        lock(msgList)
        {
            if (msgList.Count > 0)
            {
                List<MessageData> needRemove = new List<MessageData>();
                for (int i=0; i<msgList.Count; i++)
                {
                    MessageData msg = msgList[i];
                    if (actionList.ContainsKey(msg.id))
                    {
                        actionList[msg.id](msg);
                    }
                    needRemove.Add(msg);
                }

                for (int i=0; i<needRemove.Count; i++)
                {
                    msgList.Remove(needRemove[i]);
                }
            }
        }
    }



}