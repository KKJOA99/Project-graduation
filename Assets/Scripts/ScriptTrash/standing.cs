using UnityEngine;
using System.Collections;

/// <summary>
/// 의자 세우는 스크립트 였는데 그랩 및 내려놓기로 기획을 변경하게 되면서
/// 사용하지 않는 스크립트가 될 듯(160314)
/// </summary>

public class standing : MonoBehaviour {
    public GameObject chair;
    Transform chair_pos;
	// Use this for initialization
	void Start () {
        chair_pos = chair.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider coll)
    {
        
        if (coll.gameObject.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0))
            {
                chair_pos.rotation = Quaternion.Euler(0, 0, 0);
                //chair_pos.Translate(0.0f, 0.0f, 0.0f);
            }
        }
    }
}
