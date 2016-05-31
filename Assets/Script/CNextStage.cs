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
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Password_input.End == true && Application.loadedLevelName == "Password_input")
        {
            test_pass();
        }
	} 

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
            OnPlay = false;
            SceneManager.LoadScene(SceneName);
    }

    //유저가 문을 클릭했을 때 비밀번호 입력씬으로 넘어가는 함수
    public void ask_pass(Vector3 position)
    {
        Password_input.cnt = 0;
        Password_input.End = false;
        before_pos = position;
        Application.LoadLevel(SceneName);
    }

    public void test_pass()
    {
        Application.LoadLevel(SceneName);
    }
}
