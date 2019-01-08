using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GameMain : MonoBehaviour {
	private Scene mainScene;
	// Use this for initialization
	void Start () {
        NetClient.Instance().Connect("127.0.0.1", 3014);
        mainScene = new Scene();
		mainScene.Init();
	}

	// Update is called once per frame
	void Update()
	{
		MessageMgr.Instance().DispatchMessage();

		bool forward = false;
		bool back = false;
		bool right = false;
		bool left = false;
		float speed = 0.01f;
		float deltaX = 0.0f;
		float deltaY = 0.0f;

		bool needMove = true;

		if (Input.GetKey(KeyCode.W))
		{
			forward = true;
		}

		if (Input.GetKey(KeyCode.S))
		{
			back = true;
		}

		if (Input.GetKey(KeyCode.A))
		{
			left = true;
		}


		if (Input.GetKey(KeyCode.D))
		{
			right = true;
		}

		float angle;
		if (forward)
		{
			if (right)
			{
				deltaX = 1.0f;
				deltaY = 1.0f;
				angle = 45;
			}
			else if(left)
			{
				deltaY = 1.0f;
				deltaX = -1.0f;
				angle = -45;
			}
			else
			{
				angle = 0.0f;
				deltaY = 1.0f;
				deltaX = 0.0f;
			}
		}
		else if(back)
		{
			if (right)
			{
				deltaX = 1.0f;
				deltaY = -1.0f;
				angle = 135;
			}
			else if (left)
			{
				deltaY = -1.0f;
				deltaX = -1.0f;
				angle = -135;
			}
			else
			{
				angle = -180.0f;
				deltaY = -1.0f;
				deltaX = 0.0f;
			}
		}
		else
		{
			if (right)
			{
				deltaX = 1.0f;
				deltaY = 0.0f;
				angle = 90;
			}
			else if (left)
			{
				deltaY = 0.0f;
				deltaX = -1.0f;
				angle = -90;
			}
			else
			{
				needMove = false;
				angle = 0.0f;
				deltaY = 0.0f;
				deltaX = 0.0f;
			}
		}

		Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0.0f, 1.0f, 0.0f));

		if (mainScene != null && mainScene.GetMainPlayer() != null)
			mainScene.GetMainPlayer().Move(deltaX*speed, deltaY*speed, rotation, needMove);
	}


    private void OnDestroy()
    {
		NetClient.Instance().Close();
    }
}
