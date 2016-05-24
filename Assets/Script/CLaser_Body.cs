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

    void OnTriggerEnter(Collider coll) {
        Vector3 pos = startPos.transform.position;
        pos.y = coll.transform.parent.position.y;
        print("col");
        if (coll.transform.parent.tag == "Player") {
            coll.transform.parent.position = pos;
        }
    }
}
