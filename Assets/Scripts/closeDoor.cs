using UnityEngine;
using System.Collections;

public class closeDoor : MonoBehaviour {
    public GameObject Door;
    Animation doorAni;
    BoxCollider isTrigger;
	// Use this for initialization
	void Start () {
        doorAni = Door.GetComponent<Animation>();
        isTrigger = gameObject.GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            doorAni.Play("closedoor");
            isTrigger.isTrigger = false;
        }

    }
}
