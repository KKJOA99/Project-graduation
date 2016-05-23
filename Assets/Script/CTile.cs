using UnityEngine;
using System.Collections;
using System.IO;
public class CTile : MonoBehaviour {
    public GameObject player;
    public GameObject startPos;
    public int numPerRow;
    public int numPerCol;
    public StreamReader fp = null;
    bool[] pTile;
    Vector2 tileStart;
    float tileSize;
    int index;
    // Use this for initialization
    void Start () {
        pTile = new bool[numPerRow * numPerCol];
        print(pTile.Length);
        for (int i = 0; i < numPerRow * numPerCol; ++i)
            pTile[i] = false;
        fp = File.OpenText("./road.txt");
        if (fp != null) {
            while( fp.Peek() >= 0 )
            {
                string num = fp.ReadLine();
                pTile[int.Parse(num)] = true;
                //Debug.Log("idx =" + num);
            }
        }
        GameObject start = GameObject.Find("tileStart");
        GameObject next = GameObject.Find("nextTile");
        if ( start ) {
            tileSize = next.transform.position.x - start.transform.position.x;
            tileStart.x = start.transform.position.x - tileSize/2;
            tileStart.y = start.transform.position.z - tileSize/2;
        }
	}
	// Update is called once per frame
	void Update () {
        index = (int)((player.transform.position.z - tileStart.y)/tileSize) * numPerRow
            + (int)((player.transform.position.x - tileStart.x)/tileSize);
        if( index >= 0 && index <= pTile.Length-1 && !pTile[index] ) {
            player.transform.position = startPos.transform.position;
        }
	}
}