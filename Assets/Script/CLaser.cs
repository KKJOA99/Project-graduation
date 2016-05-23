using UnityEngine;
using System.Collections;

public class CLaser : MonoBehaviour {
    public float passTime;
    public GameObject StartPos;
    GameObject left;
    GameObject right;
    GameObject body;
    CLaser_SideBar leftCompo;
    CLaser_SideBar rightCompo;
    
    // Use this for initialization
    void Start () {
        left = transform.GetChild(0).gameObject;
        right = transform.GetChild(1).gameObject;
        body = transform.GetChild(2).gameObject;
        leftCompo = left.GetComponent<CLaser_SideBar>();
        rightCompo = right.GetComponent<CLaser_SideBar>();
        leftCompo.SetSpeed(passTime);
        rightCompo.SetSpeed(passTime);
        leftCompo.SetOpposite(right);
        rightCompo.SetOpposite(left);
    }
	
	// Update is called once per frame
	void Update() {
        if( leftCompo.GetMove() == false && rightCompo.GetMove() == false ) {
            leftCompo.SetMove();
            rightCompo.SetMove();
        }
        leftCompo.Move();
        rightCompo.Move();
        Vector3 pos = (left.transform.position + right.transform.position) / 2;
        pos.y = body.transform.position.y;
        body.transform.position = pos;
        Vector3 scale = body.transform.localScale;
        scale.y = Mathf.Sqrt(Mathf.Pow((left.transform.localPosition.x - right.transform.localPosition.x)/2,2)
            +Mathf.Pow((right.transform.localPosition.z - left.transform.localPosition.z )/2,2));
        body.transform.localScale = scale;
        SetRotation();
	}

    void SetRotation() {
        Vector3 angle = body.transform.eulerAngles;
        angle.y = right.transform.eulerAngles.y-90;
        body.transform.eulerAngles = angle;
    }   
}
