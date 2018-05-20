using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GameMain : MonoBehaviour {

    private NetClient netClient;
    private class Hello
    {
        public string meg = "Hello";
    }
	// Use this for initialization
	void Start () {
        netClient = new NetClient();
        netClient.Connect("127.0.0.1", 3014);

        Scene s = new Scene();
        s.Init();
	}


    void sendHello()
    {
        string data = JsonUtility.ToJson(new Hello());
        byte[] bytes = Encoding.ASCII.GetBytes(data);
        netClient.Send(0, bytes);
    }
	
	// Update is called once per frame
	void Update () {
        MessageMgr.Instance().DispatchMessage();
    }


    private void OnDestroy()
    {
        netClient.Close();
    }
}
