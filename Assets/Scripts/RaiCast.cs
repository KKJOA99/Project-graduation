using UnityEngine;
using System.Collections;

public class RaiCast : MonoBehaviour
{
    private GameObject target; //Hit된 GameObject가 들어갈 변수
    public GameObject GuiText; //GuiText(상호작용 키 안내 메세지)를 적용할 변수(?)
    public Texture2D aimTexture;
    public Texture2D actionTexture;
    GUIText text;
    Rect aim;
    RaycastHit hit;
    Ray ray;
    bool isGrab;
    ArrayList Code_Key = new ArrayList(new string[] { }); //주운 열쇠의 코드값을 기억하기 위한 배열(160519)

    //Raycast 할 Vector3 좌표 ScreenPointToRay 좌표용
    //Vector3 ScreenPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    Vector3 ViewportPosition = new Vector3(0.5f, 0.5f, 0.0f);
    public float MAX_RAY_DIS = 0.8f;
    Transform headTrans;
    float fDist_Epsilon;
    const float fEpsilon = 2.0f;


    private GameObject GetObject() //target에 현재 RaiCast에 Hit된 gameObject 저장하는 함수
    {
        GameObject tmp_target = null;
        tmp_target = hit.transform.gameObject;
        return tmp_target;
    }

    void OnGUI()
    {
        if (text.enabled == true)
        {
            GUI.DrawTexture(aim, actionTexture);
        }
        else
        {
            GUI.DrawTexture(aim, aimTexture);
        }
    }

    void Start()
    {

        // 비밀번호 랜덤 설정, FindKey 씬에서만, 처음 한번만 작동(160525)
        if (CNextStage.OnPlay == false && Application.loadedLevelName == "FindKey")
        {

            for (int i = 0; i < 4; i++)
            {
                CNextStage.Password[i] = Random.Range(1, 9);
            }
            /*
            CNextStage.Password[0] = 4;
            CNextStage.Password[1] = 8;
            CNextStage.Password[2] = 8;
            CNextStage.Password[3] = 2;
            */
        }

        //스테이지 비밀번호 확인 코드(160525)
        print(CNextStage.Password[0] + "," + CNextStage.Password[1] + "," +
            CNextStage.Password[2] + "," + CNextStage.Password[3]);

        print("OnPlay =" + CNextStage.OnPlay);
        //Password_input Scene에서 되돌아 올 때, 넘어가기 전 플레이어 위치값 불러오기
        if (CNextStage.OnPlay == true)
        {
            this.transform.position = CNextStage.before_pos;
        }
        DontDestroyOnLoad(this); //다른 씬으로 넘어갈 때 this(유저) 파괴 금지(160525)




        Code_Key.Add("열림");
        isGrab = false;
        float left = (Screen.width - aimTexture.width) / 2;
        float top = (Screen.height - aimTexture.height) / 2;
        float width = aimTexture.width;
        float height = aimTexture.height;

        aim = new Rect(left, top, width, height);
        headTrans = transform.GetChild(0).transform.GetChild(0).transform;

        text = GuiText.GetComponent<GUIText>();
    }
    void Update()
    {
        //ray = Camera.main.ScreenPointToRay(ScreenPosition);
        ray = Camera.main.ViewportPointToRay(ViewportPosition);
        //hit.point = new Vector3(0.0f, 0.0f, 0.0f);
        printHeadAngle();
        text.enabled = false;


        ray.origin = this.transform.position;
        hit.point = this.transform.position + hit.point;
        //실제 Raycast 계산
        /// 태그가 Untagged가 아닐 때에만 작동
        if (Physics.Raycast(ray, out hit, MAX_RAY_DIS + fDist_Epsilon) && hit.transform.gameObject.tag != "Untagged")
        {
            print(hit.transform.gameObject.tag);    //타겟의 태그가 무엇인지 프린트함(160519)
            text.enabled = true;    //Action이 가능한 상태라는 메세지 띄어줌
            //Debug.Log(hit.point);
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            if (Input.GetMouseButtonDown(0))
            {
                //Raycast의 hit된 대상을 target으로 지정
                target = GetObject();
                /// 태그가 GrabObject 일 때
                if (target.tag == "GrabObject")
                {
                    if (isGrab == true && hit.transform.parent == this.transform)
                    {
                        hit.transform.SetParent(null);
                    }
                    else
                    {
                        //hit.transform.gameObject.transform.Rotate(0, 10, 0);
                        //hit.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        hit.transform.SetParent(this.transform);
                    }
                    isGrab = !isGrab;
                }
                /// 태그가 Door 일 때
                if (target.tag == "Door")
                {
                    Animation dAni = hit.transform.gameObject.GetComponent<Animation>();
                    dAni.Play("opendoor");
                }
                /// 태그가 OutDoor 일 때 (160525)
                if (target.tag == "OutDoor")
                {
                    print(CNextStage.OnPlay);
                    //비밀번호를 맞추고 돌아오면 문이 열림(160526)
                    if (CNextStage.OnPlay == true)
                    {
                        Animation dAni = hit.transform.gameObject.GetComponent<Animation>();
                        dAni.Play("opendoor");
                    }
                    //아니라면 Password_input Scene호출(160526)
                    else
                        target.GetComponent<CNextStage>().ask_pass(transform.position);


                }
                /// 태그가 OpenObjectR 일 때
                if (target.tag == "OpenObjectR")
                {
                    isOpen tmp = target.GetComponent<isOpen>();
                    Animation dAni = hit.transform.gameObject.GetComponent<Animation>();
                    bool tmp_isOpen = tmp.return_isOpen();
                    bool tmp_YGTWN = tmp.YGTWN(Code_Key);
                    if (tmp_YGTWN == false)
                    {
                        if (tmp_isOpen == false)
                        {
                            dAni.Play("cabinetOpen01");
                            target.GetComponent<isOpen>().cnt_isOpen();
                        }
                        else
                        {
                            dAni.Play("cabinetClose01");
                            target.GetComponent<isOpen>().cnt_isOpen();
                        }
                    }
                }
                /// 태그가 OpenObjectL 일 때
                if (target.tag == "OpenObjectL")
                {
                    if (target.transform.FindChild("ShowNum"))
                    {
                        target.GetComponentInChildren<shownum>().SetShowNum(); // 타겟의 shownum 세팅(160602)
                    }
                    isOpen tmp = target.GetComponent<isOpen>();
                    Animation dAni = hit.transform.gameObject.GetComponent<Animation>();
                    bool tmp_isOpen = tmp.return_isOpen();
                    bool tmp_YGTWN = tmp.YGTWN(Code_Key);
                    if (tmp_YGTWN == false)
                    {
                        if (tmp_isOpen == false)
                        {
                            dAni.Play("cabinetOpen02");
                            target.GetComponent<isOpen>().cnt_isOpen();
                        }
                        else
                        {
                            dAni.Play("cabinetClose02");
                            target.GetComponent<isOpen>().cnt_isOpen();
                        }
                    }
                }
                /// 태그가 SlideObject
                if (target.tag == "SlideObject")
                {
                    target.GetComponent<isOpen>().move = true;
                }
                /// 태그가 Keys(160519)
                if (target.tag == "Keys")
                {
                    Code_Key.Add(target.GetComponent<KeySet>().Get_Code_Key());
                    target.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.GetPoint(MAX_RAY_DIS + fDist_Epsilon), Color.red);
            text.enabled = false;

            //잡고있던 물체를 쳐다보지 않으면 나에게서 놓은것으로 간주함
            if (isGrab == true)
            {
                target.transform.SetParent(null);
                isGrab = !isGrab;
            }
        }
    }

    void printHeadAngle()
    {
        float headAngle = headTrans.rotation.x;
        headAngle = headAngle > 0 ? headAngle : -headAngle;
        fDist_Epsilon = Mathf.Sin(headAngle) * fEpsilon;
    }
}
