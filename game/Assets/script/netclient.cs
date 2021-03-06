﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;


public class NetClient
{
    private TcpClient client;
    private byte[] recvBuf = new byte[2048];
    private List<Byte[]> sendList = new List<byte[]>();

	private static NetClient instance;

	public static NetClient Instance()
	{
		if (instance == null)
			instance = new NetClient();

		return instance;
	}

    public void Connect(String server, Int32 port)
    {
        try
        {
            this.client = new TcpClient();
            client.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(server), port));
            this.startRecv();
        }
        catch (ArgumentNullException e)
        {
            Debug.Log(string.Format("ArgumentNullException: {0}", e));
        }
    }


    public void Close()
    {
        sendList.Clear();
        client.Close();
    }

    public void Send(int id, byte[] data)
    {
        byte[] sendData = MessagePacker.encode(id, data);
        send(sendData);
    }


    private void send(byte[] sendData)
    {
        byte[] willSendData;
        lock(sendList)
        {
            sendList.Add(sendData);
            willSendData = sendList[0];
        }
        NetworkStream stream = client.GetStream();
        stream.BeginWrite(willSendData, 0, willSendData.Length, new AsyncCallback(sendCallback), willSendData);
    }


    private void sendCallback(IAsyncResult ar)
    {
        try
        {
            if (ar.IsCompleted)
            {
                lock(sendList)
                {
                    byte[] sendData = (byte[])ar.AsyncState;
                    sendList.Remove(sendData);
                    if (sendList.Count > 0)
                    {
                        send(sendList[0]);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    public void startRecv()
    {
        NetworkStream stream = client.GetStream();
        stream.BeginRead(recvBuf, 0, 4, new AsyncCallback(recvHeadCallBack), null);
    }


    private void recvHeadCallBack(IAsyncResult ar)
    {
        if (ar.IsCompleted)
        {
            NetworkStream stream = client.GetStream();
			byte[] lengthdata = new byte[4];
			Array.Copy(recvBuf, 0, lengthdata, 0, 4);
			if (System.BitConverter.IsLittleEndian == false)
				Array.Reverse(lengthdata);
            int length = System.BitConverter.ToInt32(lengthdata, 0);

            stream.BeginRead(recvBuf, 4, length, new AsyncCallback(recvBodyCallBack), length);
        }
        else
        {
            

        }
    }


    private void recvBodyCallBack(IAsyncResult ar)
    {
        if (ar.IsCompleted)
        {
            int bodyLength = (int)ar.AsyncState;
            MessageData msg = MessagePacker.decode(recvBuf, 4, bodyLength);
            MessageMgr.Instance().AddMsg(msg);
            NetworkStream stream = client.GetStream();
            stream.BeginRead(recvBuf, 0, 4, new AsyncCallback(recvHeadCallBack), null);
        }
    }
}