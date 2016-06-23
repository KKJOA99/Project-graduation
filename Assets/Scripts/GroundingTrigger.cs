using UnityEngine;
using System.Collections;

/// <summary>
/// 점프방식 변경과 사운드 추가에 따른 지면 충돌 검사 스크립트(160620)
/// </summary>

public class GroundingTrigger : MonoBehaviour {
    public static bool isGround = false;
    public AudioClip jump;
    public AudioClip land;
    private AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay()
    {
        isGround = true;
    }
    void OnTriggerEnter()
    {
        isGround = true;
        source.PlayOneShot(land);
        print("land");
    }

    void OnTriggerExit()
    {
        isGround = false;
        source.PlayOneShot(jump);
        print("jump");
    }
}
