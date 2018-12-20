using UnityEngine;

public class Player
{
    protected float x;
	protected float y;
    protected GameObject playerObj;

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


	public void SetPosition(float x, float y)
    {
		this.x = x;
		this.y = y;
        playerObj.transform.position = new Vector3(x, 1, y);
	}
}