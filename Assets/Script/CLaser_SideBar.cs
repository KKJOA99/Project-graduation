using UnityEngine;
using System.Collections;

public class CLaser_SideBar : MonoBehaviour {
    public GameObject[] target;
    GameObject oppositeSide;
    float passTime;
    int length;
    Vector3[] endPos;
    Vector3 beginPos;
    Vector3 currentPos;
    float distLimit;
    int idx;
    Vector3 spd;
    Transform thisTrans;
    bool bMove;

    // Use this for initialization
    void Start() {
        bMove = false;
        thisTrans = this.transform;
        idx = 0;
        length = target.Length;
        beginPos = thisTrans.localPosition;
        currentPos = beginPos;
        endPos = new Vector3[length];
        for( int i = 0 ; i < length ; ++i )
            endPos[i] = target[i].transform.localPosition;
        distLimit = Vector3.Distance(currentPos, endPos[0]);
        spd = (endPos[idx] - beginPos)/passTime;
    }

    // Update is called once per frame
    void Update() {
    }

    public void Move() {
        if( bMove ) {
            thisTrans.localPosition += spd*Time.deltaTime;
            if( CheckDistance() ) {
                //transform.localPosition = endPos[idx++];
                idx++;
                if( idx >= target.Length ) {
                    idx = 0;
                    currentPos = beginPos;
                } else {
                    currentPos = endPos[idx-1];
                }
                distLimit = Vector3.Distance(currentPos, endPos[idx]);
                bMove = false;
            }
            SetRotation();
        }
    }

    bool CheckDistance() {
        return Vector3.Distance(currentPos,transform.localPosition) > distLimit;
    }

    public void SetMove() {
        transform.localPosition = currentPos;
        spd = ( endPos[idx] - currentPos ) / passTime * length;
        bMove = true;
    }

    public bool GetMove() {
        return bMove;
    }

    public void SetSpeed( float fPassTime ){
        passTime = fPassTime;
    }
    
    public void SetOpposite( GameObject obj ) {
        oppositeSide = obj;
    }

    void SetRotation( ) {
        transform.LookAt(oppositeSide.transform);
    }
}
