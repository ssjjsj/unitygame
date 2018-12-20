using UnityEngine;

public class Player
{
    protected float x;
	protected float y;
    private GameObject playerObj;

    public void Create(int playerId, bool isMainPlayer)
    {
        GameObject prefab = (GameObject)Resources.Load("prefab/player");
        playerObj = GameObject.Instantiate(prefab);
		playerObj.name = "player_" + playerId.ToString();
		if (isMainPlayer)
			playerObj.name = "mainPlayer";


		playerObj.GetComponent<Renderer>().material = (Material)Resources.Load("material/playerMaterial");
		if (isMainPlayer)
		{
			playerObj.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
		}
		else
		{
			playerObj.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
		}
    }

    public void SetPosition(float x, float y)
    {
		this.x = x;
		this.y = y;
        playerObj.transform.position = new Vector3(x, 1, y);
	}
}