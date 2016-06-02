using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CPrologue : MonoBehaviour {
    enum ECanvasState { ein, eplay, eout }
    public GameObject headLight;
    public GameObject text;
    public GameObject door;
    public GameObject destination;
    public GameObject map;
    public GameObject canvas;
    bool isStart = false;
    bool isCanvasOn = false;
    public float MoveSpeed;
    public float LightDecreaseValue;
    public float LightAngleDecrease;
    public Sprite[] prologueImage = new Sprite[9];
    int index = 0;
    Animation animeFadeInOut;
    float fTime = 0.0f;
    public float fShowImageTime;
    ECanvasState eCanvasState = ECanvasState.ein;
	// Use this for initialization
	void Start () {
        animeFadeInOut = canvas.transform.GetChild(1).GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        if( isStart == false && Input.GetKeyDown(KeyCode.Space) ) {
            text.SetActive(false);
            isStart = true;
        }
        else if( isCanvasOn == false && isStart ) {
            EnterTheRoom();
        } else if( isCanvasOn ) {
            CanvasLoop();
        }
	}
    void EnterTheRoom() {
        door.transform.Translate(new Vector3(0, 0, MoveSpeed));
        this.gameObject.transform.Translate(new Vector3(0, 0, MoveSpeed));
        headLight.GetComponent<Light>().intensity -= LightDecreaseValue;
        headLight.GetComponent<Light>().spotAngle -= LightAngleDecrease;
        MoveSpeed *= 1.02f;
        if( Vector3.Distance(this.gameObject.transform.position,destination.transform.position) < 1.0f ) {
            print("도착");
            TurnOffMap();
            TurnOnCanvas();
        }
    }
    void TurnOffMap() {
        map.SetActive(false);
    }
    void TurnOnCanvas() {
        isCanvasOn = true;
        canvas.SetActive(true);
    }
    //프롤로그 이미지의 페이드인, 일정시간 보여주기, 페이드아웃
    void CanvasLoop() {
        switch( eCanvasState ) {
            case ECanvasState.ein: {
                    if( animeFadeInOut.IsPlaying("fade_in_prologue") == false ) {
                        animeFadeInOut.Play("fade_in_prologue");
                        eCanvasState = ECanvasState.eplay;
                        print("페이드인");
                    }
                }
                break;
            case ECanvasState.eplay: {
                    if( animeFadeInOut.IsPlaying("fade_in_prologue") == false ) {
                        fTime += Time.deltaTime;
                        if( fTime >= fShowImageTime ) {
                            animeFadeInOut.Play("fade_out_prologue");
                            eCanvasState = ECanvasState.eout;
                            fTime = 0;
                        }
                    }
                }
                break;
            case ECanvasState.eout: {
                    //페이드 아웃의 재생이 끝났다면
                    if( animeFadeInOut.IsPlaying("fade_out_prologue") == false ) {
                        index++;
                        if( index >= prologueImage.Length )
                            print("끝");
                        else {
                            canvas.transform.GetChild(1).GetComponent<Image>().sprite = prologueImage[index];
                            eCanvasState = ECanvasState.ein;
                        }
                    }
                }
                break;
        }
    }
}
