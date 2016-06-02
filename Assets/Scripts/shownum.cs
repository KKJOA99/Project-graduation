using UnityEngine;
using System.Collections;

/// <summary>
/// Find_Key scene's password set & show script(160602)
/// </summary>

public class shownum : MonoBehaviour
{
    public int index = 1;
    public Sprite[] numSprite = new Sprite[9];
    SpriteRenderer mySR;

    public void SetShowNum()    // 보여줄 스프라이트 세팅(160602)
    {
        mySR = this.GetComponent<SpriteRenderer>();
        print(numSprite[CNextStage.Password[index - 1] - 1]);
        mySR.sprite = numSprite[CNextStage.Password[index - 1] - 1];
    }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
