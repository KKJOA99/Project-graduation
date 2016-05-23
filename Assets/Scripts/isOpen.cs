using UnityEngine;
using System.Collections;

public class isOpen : MonoBehaviour
{
    private bool b_isOpen;
    public bool move;
    Vector3 initial_pos;
    Vector3 end_pos;
    public string Code_Key = "0";   //prepare for compare which string type Keycode, bagic Keycode is (string type) "0" (160519)

    //function of compare keycode "You got the wrong number"(160519)
    public bool YGTWN(ArrayList Code_Key)
    {
        for (int i = 0; i < Code_Key.Count; i++)
            if (this.Code_Key.CompareTo(Code_Key[i]) == 0)
            {
                return false;
            }
        return true;
    }

    public bool return_isOpen()
    {
        return b_isOpen;
    }

    public void cnt_isOpen()
    {
        b_isOpen = !b_isOpen;
    }

    // Use this for initialization
    void Start()
    {
        b_isOpen = false;
        move = false;
        initial_pos = transform.localPosition;
        end_pos = transform.localPosition;
        end_pos.z = end_pos.z + 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            if (b_isOpen == false)
            {
                //어째서인지 전역좌표계의 x축을 바꾸기 위해선 y값부분을 건드려야하고
                //그거를 지역좌표계에선 y축인데 z값이 변경됨...ㅠㅠㅠㅠㅠㅠㅠㅠ유니티 나빠요
                transform.Translate(0.0f, -1.0f * Time.deltaTime, 0.0f); 
                //Debug.Log(transform.localPosition);
                if (transform.localPosition.z >= end_pos.z)
                {
                    transform.localPosition = end_pos;
                    move = false;
                    cnt_isOpen();
                }
            }
            else
            {
                transform.Translate(0.0f, 1.0f * Time.deltaTime, 0.0f);
                //Debug.Log(transform.localPosition);
                if (transform.localPosition.z <= initial_pos.z + 0.01f)
                {
                    transform.localPosition = initial_pos;
                    move = false;
                    cnt_isOpen();
                }
            }
        }

    }
}
