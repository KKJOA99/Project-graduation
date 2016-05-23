using UnityEngine;
using System.Collections;
using System.IO;
public class CTile : MonoBehaviour {
    public GameObject player;
    public GameObject tile;
    Vector3 startPos;
    Vector2 tileStart;
    public int numPerRow;
    public int numPerCol;
    public StreamReader fp = null;
    bool[] pTile;
    float tileSize;
    int index;
    // Use this for initialization
    void Start () {
        //Init bool array means isSafeTile.
        pTile = new bool[numPerRow * numPerCol];
        for (int i = 0; i < numPerRow * numPerCol; ++i)
            pTile[i] = false;

        //set bool array with indexes read in "road.txt". 
        fp = File.OpenText("./road.txt");
        if (fp != null) {
            while( fp.Peek() >= 0 )
            {
                string num = fp.ReadLine();
                pTile[int.Parse(num)] = true;
                //Debug.Log("idx =" + num);
            }
        }

        //set tile's startPos & size.
        tileSize = tile.transform.localScale.z/numPerRow;
        tileStart.x = tile.transform.localPosition.x - tile.transform.localScale.x/2 + tileSize/2;
        tileStart.y = tile.transform.localPosition.z - tile.transform.localScale.z/2 + tileSize/2;
        startPos.x = tile.transform.localPosition.x;
        startPos.z = tileStart.y;
        startPos.y = player.transform.localPosition.y;
	}
	// Update is called once per frame
	void Update () {
        index = (int)( ( player.transform.localPosition.x - tileStart.x ) / tileSize ) * numPerRow
            + (int)( ( player.transform.localPosition.z - tileStart.y ) / tileSize );
        if( index >= 0 && index <= pTile.Length - 1 && !pTile[index] ) {
            player.transform.localPosition = startPos;
            print("out");
        }
    }
}