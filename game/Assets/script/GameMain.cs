using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GameMain : MonoBehaviour {
	private float lastUpdateTime = -1.0f;
	private Scene mainScene;
	// Use this for initialization
	void Start () {
        NetClient.Instance().Connect("127.0.0.1", 3014);
        mainScene = new Scene();
		mainScene.Init();
	}
	
	// Update is called once per frame
	void Update () {
        MessageMgr.Instance().DispatchMessage();

		updateMainPlayerPos();
		mainScene.Update(Time.deltaTime);
	}


	void updateMainPlayerPos()
	{
		float deltaX = 0;
		float deltaY = 0;

		if (Input.GetKey(KeyCode.W))
		{
			deltaY = 1.0f;
			//Debug.Log("key down W");
		}

		if (Input.GetKey(KeyCode.S))
		{
			deltaY = -1.0f;
			//Debug.Log("key down S");
		}

		if (Input.GetKey(KeyCode.A))
		{
			deltaX = -1.0f;
			//Debug.Log("key down A");
		}


		if (Input.GetKey(KeyCode.D))
		{
			deltaX = 1.0f;
			//Debug.Log("key down D");
		}

		if ((deltaX != 0.0f || deltaY != 0.0f) && mainScene.GetMainPlayer() != null)
			mainScene.GetMainPlayer().Move(deltaX, deltaY);
	}


    private void OnDestroy()
    {
		NetClient.Instance().Close();
    }
}
