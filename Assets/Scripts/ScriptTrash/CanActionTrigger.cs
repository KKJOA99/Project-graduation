using UnityEngine;
using System.Collections;

public class CanActionTrigger : MonoBehaviour {
    public GameObject Text;
    GUIText text = null;
	// Use this for initialization
	void Start () {
        text = Text.GetComponent<GUIText>();
        text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            text.enabled = true;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            text.enabled = false;
        }
    }
}
