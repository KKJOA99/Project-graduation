using UnityEngine;
using System.Collections;

public class openthedoor : MonoBehaviour {
    public GameObject door; // object door (include pivot)
    public GameObject open; // collider testbox true = open, false = close
    public Animation mAni;   //animation (#0 open, #1 close)
    public GameObject CanAction;
    bool isopen = false;
    //Vector3 initial_pos;
    //Quaternion initial_rot;
    BoxCollider opening = null;
    GUIText text;
	// Use this for initialization
	void Start () {
        mAni = door.GetComponent<Animation>();
        //initial_pos = door.transform.position;
        //initial_rot = door.transform.rotation;
        //BoxCollider exp;  //BoxCollider 변수를 만들어서 사용할 수 있음
        //exp = this.gameObject.GetComponent<BoxCollider>();
        opening = open.gameObject.GetComponent<BoxCollider>();
        text = CanAction.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(mAni.isPlaying); //mAni의 플레이상태를 true or false로 Log해줌
	}


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
            text.enabled = true;
    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0) && isopen == false)
                {
                    mAni.Play("opendoor");
                    //////////애니메이션 사용하기전 코드/////////////
                    /////////스테이지에서만 사용가능 절대좌표라서////
                    //door.transform.position = new Vector3(-1.2f, 2.0f, -14.0f);
                    //door.transform.Rotate(0.0f, 0.0f, 90.0f);
                    isopen = true;
                    //text.enabled = false;
                }
            if (!mAni.isPlaying && isopen == true) {
                opening.isTrigger = true;
            }
            //this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            //Destroy(this.gameObject,1.0f);
        }
    }
    void OnTriggerExit(Collider coll)
    {
        //Debug.Log("EXIT");
        if(coll.gameObject.tag == "Player" && isopen)
        {
            isopen = false;
            opening.isTrigger = false;
            //mAni.Stop();

            mAni.Play("closedoor");
            //door.transform.position = initial_pos;
            //door.transform.rotation = initial_rot;
            
        }

        //text.enabled = false;
    }
}
