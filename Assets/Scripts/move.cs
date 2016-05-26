using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
    public static Vector3 ini_pos;

	// Use this for initialization
	void Start () {
        ini_pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.R))
        {
            this.transform.position = ini_pos;
        }
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * 3 * Time.deltaTime);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * 3 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Vector3 zero = new Vector3(1.0f, 0.0f, 1.0f);
            this.transform.GetChild(1).GetComponent<BoxCollider>().size = zero;
        }
        //if (Input.GetKey(KeyCode.A))
        //{
        //    this.transform.Translate(-5.0f * Time.deltaTime, 0.0f, 0.0f);
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    this.transform.Translate(5.0f * Time.deltaTime, 0.0f, 0.0f);
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    this.transform.Translate( 0.0f, 0.0f, 5.0f * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    this.transform.Translate(0.0f, 0.0f, -5.0f * Time.deltaTime);
        //}
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 zero = new Vector3(1.0f, 1.0f, 1.0f);
            this.transform.Translate(0.0f, 5.0f * Time.deltaTime, 0.0f);
            this.transform.GetChild(1).GetComponent<BoxCollider>().size = zero;
        }
	}
}
