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
    BinaryReader br = null;
    bool[] pTile;
    float tileSize;
    int index;
    string secureStr;
    // Use this for initialization
    void Start () {
        //Init bool array means isSafeTile.
        pTile = new bool[numPerRow * numPerCol];
        for (int i = 0; i < numPerRow * numPerCol; ++i)
            pTile[i] = false;

        
        //set bool array with indexes read in "road.dat".    
            //first, open the binaryFile;
        br = new BinaryReader(new FileStream("road.dat", FileMode.Open));
        if (br != null) {
            //second, read the binary to string
            secureStr = br.ReadString();
            print("bin:"+secureStr);
            //third, Decrypt the string
            string str = CSecureity.Decrypt(secureStr);
            print("full str:" + str);
            //fourth, 숫자를 하나씩 떼어낸당
            StringReader sw = new StringReader(str);
            int ch;
            string _idx = "";
            while( (ch = sw.Read()) != -1  ) {
                if( (char)ch == ' ' ) {
                    print(_idx);
                    _idx = "";
                } else
                    _idx += (char)ch;
            }

            br.Close();
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