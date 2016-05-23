using UnityEngine;
using System.Collections;
using System.IO;

public class CTileEditor : MonoBehaviour {
    StreamWriter fp = null;
    public GameObject tile;
    GameObject[] cube = new GameObject[11*15];
    bool[] isSafe = new bool[11 * 15];
    Vector3 mousePos;
    Vector2 startPos;
    float tileSize;
    int index = 0;
    int prevIdx = -1;
    // Use this for initialization
    void Start () {
        tileSize = tile.transform.localScale.z / 15;
        startPos.x = tile.transform.localPosition.x - tile.transform.localScale.x / 2;
        startPos.y = tile.transform.localPosition.z - tile.transform.localScale.z / 2;
        for( int i = 0 ; i < cube.Length ; ++i ) {
            string findStr = "Text (" + i.ToString() + ")";
            cube[i] = GameObject.Find(findStr);
            Vector3 pos = new Vector3(startPos.x + tileSize / 2 + tileSize * (i % 11), 0.01f
                                    , startPos.y + tileSize / 2 + tileSize * (int)(i / 11)+0.2f);
            cube[i].transform.position = pos;
            cube[i].GetComponent<TextMesh>().color = new Color(1f, 0.5f, 0.5f);
            isSafe[i] = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        mousePos.x = startPos.x + Input.mousePosition.x/Screen.height*30;
        mousePos.y = 0.1f;
        mousePos.z = startPos.y + Input.mousePosition.y/Screen.width*22;
        
        if( Input.GetMouseButton(0) ) {
            index = (int)(( mousePos.z + tileSize / 2 ) / tileSize + 7)*11 // index of y
            + (int)(( mousePos.x + tileSize / 2 ) / tileSize + 5); //index of x
            if( index != prevIdx ) {
                _SetTile(index);
                print(index);
                prevIdx = index;
            }
        } else if( Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown("s") ) {
            fp = File.CreateText("./road.txt");
            print("save txt file");
            if( fp != null ) {
                for( int i = 0 ; i < isSafe.Length ; ++i ) {
                    if( isSafe[i] )
                        fp.WriteLine(i);
                }
                fp.Close();
            }
        }
    }
    void _SetTile(int idx ) {
        isSafe[idx] = !isSafe[idx];
        cube[idx].GetComponent<TextMesh>().color = new Color(isSafe[idx] ? 0.5f : 1f, 0.5f, isSafe[idx] ? 1f : 0.5f ); 
    }
}
