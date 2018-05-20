using UnityEngine;

public class Player
{
    private float x;
    private float y;
    private GameObject playerObj;

    public void Create()
    {
        GameObject prefab = (GameObject)Resources.Load("prefab/player");
        playerObj = GameObject.Instantiate(prefab);
    }

    public void SetPosition(float x, float y)
    {
        playerObj.transform.position = new Vector3(x, 1, y);
    }
}