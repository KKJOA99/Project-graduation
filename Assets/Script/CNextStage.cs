using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CNextStage : MonoBehaviour {
    public string SceneName;

    // 비밀번호를 위한 정적변수(160525)
    public static int[] Password = new int[4];
    public static bool OnPlay;
    public static Vector3 before_pos;
    
	// Use this for initialization
	void Start () {
        // 비밀번호 랜덤 설정, FindKey 씬에서만, 처음 한번만 작동(160525)
        if (OnPlay == false && Application.loadedLevelName == "FindKey")
        {
            for (int i = 0; i < 4; i++)
            {
                Password[i] = Random.Range(0, 9);
            }
        }

        if (SceneName == "FindKey")
        {
            test_pass();
        }
	}
	
	// Update is called once per frame
	void Update () {
	} 

    void OnTriggerEnter(Collider col)
    {
        if( col.tag == "Player" )
            SceneManager.LoadScene(SceneName);
    }

    //유저가 문을 클릭했을 때 비밀번호 입력씬으로 넘어가는 함수
    public void ask_pass(Vector3 position)
    {
        before_pos = position;
        Application.LoadLevel(SceneName);
    }

    public void test_pass()
    {

        //Application.LoadLevel(SceneName);
    }
}
