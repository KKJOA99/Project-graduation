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
    bool[] IsSafeTile;
    float tileSize;
    int index;
    string secureStr;
    // Use this for initialization
    void Start () {
        //Init bool array means isSafeTile.
        IsSafeTile = new bool[numPerRow * numPerCol];
        for (int i = 0; i < numPerRow * numPerCol; ++i)
            IsSafeTile[i] = false;

        
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
                    print(int.Parse(_idx));
                    IsSafeTile[int.Parse(_idx)] = true;
                    _idx = "";
                } else 
                    _idx += (char)ch;
            }

            br.Close();
        }

        //set tile's startPos & size.
        tileSize = tile.transform.lossyScale.z/numPerRow;
        print("tileSize = "+tileSize);
        tileStart.x = tile.transform.position.x - tile.transform.lossyScale.x/2;
        tileStart.y = tile.transform.position.z - tile.transform.lossyScale.z/2;
        print("tileStart = " + tileStart);
        startPos.x = tile.transform.position.x;
        startPos.z = tileStart.y;
        startPos.y = player.transform.position.y;
    }
	// Update is called once per frame
	void Update () {
        index = (int)( ( player.transform.position.z - tileStart.y ) / tileSize ) * numPerCol
            + (int)( ( player.transform.position.x - tileStart.x ) / tileSize );
        if( index >= 0 && index <= IsSafeTile.Length - 1 && !IsSafeTile[index] ) {
            player.transform.position = startPos;
            print("idx:"+index);
        }
    }
}