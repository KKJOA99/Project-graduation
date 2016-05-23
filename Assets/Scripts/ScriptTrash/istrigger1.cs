using UnityEngine;
using System.Collections;

public class istrigger1 : MonoBehaviour {
    public Animation mAni = null;
    public GameObject door = null;
    
	// Use this for initialization
	void Start () {
        mAni = door.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("hi");
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("hi");
            mAni.Play();
        }
    }
}
