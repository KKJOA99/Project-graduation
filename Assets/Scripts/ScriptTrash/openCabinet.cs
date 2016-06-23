using UnityEngine;
using System.Collections;

public class openCabinet : MonoBehaviour {
    public GameObject right; // object right door (include pivot)
    public GameObject left; // object left door (include pivot)
    bool isopen = false;
    Animation mAni1, mAni2 = null;
    
	// Use this for initialization
	void Start () {
        mAni1 = right.GetComponent<Animation>();
        mAni2 = left.GetComponent<Animation>();
           //right door animation (open)
        //Animation mAni2 = door2.GetComponent<Animation>();   //right door animation (close)
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0) && !isopen)
            {
                mAni1.Play("cabinetOpen01");
                mAni2.Play("cabinetOpen02");
                isopen = true;
                return;
            }
            if (Input.GetMouseButtonDown(0) && isopen)
            {
                mAni1.Play("cabinetClose01");
                mAni2.Play("cabinetClose02");

                isopen = false;
                return;
            }     
        }
    }
}
