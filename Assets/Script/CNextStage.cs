using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CNextStage : MonoBehaviour {
    public string SceneName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if( col.tag == "Player" )
            SceneManager.LoadScene(SceneName);
    }
}
