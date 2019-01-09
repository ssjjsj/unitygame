using UnityEngine;

public class Player
{
    protected float x;
	protected float y;
    protected GameObject playerObj;
	protected Animator animatiorController;

	public void Create(int playerId, bool isMainPlayer)
    {
		string resPath;
		if (isMainPlayer)
			resPath = "prefab/player01/player";
		else
			resPath = "prefab/player02/player";
		GameObject prefab = (GameObject)Resources.Load(resPath);
        playerObj = GameObject.Instantiate(prefab);
		playerObj.name = "player_" + playerId.ToString();
		if (isMainPlayer)
			playerObj.name = "mainPlayer";

		onCreate();
    }


	protected virtual void onCreate()
	{

	}


	public virtual void Update()
	{

	}


	public void SetPosition(float x, float y)
    {
		this.x = x;
		this.y = y;
	}


	protected void onStartMove()
	{
		animatiorController.SetBool("idel", false);
		animatiorController.SetBool("run", true);
	}


	protected void onEndMove()
	{
		animatiorController.SetBool("idel", true);
		animatiorController.SetBool("run", false);
	}
}