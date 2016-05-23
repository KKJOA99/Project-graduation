using UnityEngine;
using System.Collections;

public class CLaser_Body : MonoBehaviour {
    GameObject startPos;
	// Use this for initialization
	void Start () {
        startPos = GetComponentInParent<CLaser>().StartPos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        Vector3 pos = startPos.transform.position;
        pos.y = coll.transform.position.y;
        print("col");
        if (coll.tag == "Player")
        {
            coll.transform.position = pos;
        }
    }
}
