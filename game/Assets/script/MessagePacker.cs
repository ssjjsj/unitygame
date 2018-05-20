using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class MessagePacker
{
    public static byte[] encode(int id, byte[] data)
    {
        byte[] sendData = new byte[data.Length + 8];
        byte[] lengthBytes = System.BitConverter.GetBytes(data.Length + 4);
        byte[] idBytes = System.BitConverter.GetBytes(id);

        Array.Copy(lengthBytes, 0, sendData, 0, 4);
        Array.Copy(idBytes, 0, sendData, 4, 4);
        Array.Copy(data, 0, sendData, 8, data.Length);

        return sendData;
    }


    public static MessageData decode(byte[] data, int startIndex, int length)
    {
        int id = System.BitConverter.ToInt32(data, startIndex);
        string messageData = System.Text.Encoding.UTF8.GetString(data, startIndex+4, length - 4);
        MessageData msg = new MessageData();
        msg.id = id;
        msg.data = messageData;


        return msg;
    }
}