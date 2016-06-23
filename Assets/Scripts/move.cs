using UnityEngine;
using System.Collections;

public class move : MonoBehaviour
{
    public static Vector3 ini_pos;
    Vector3 ret_pos;
    //걸음 효과음을 위한 오디오클립(160620)
    public AudioClip walk_1;
    public AudioClip walk_2;
    private AudioSource source;

    int timer = 0;

    // Use this for initialization
    void Start()
    {
        ret_pos = transform.position;
        ini_pos = transform.position;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.position = ini_pos;
        }

        //걸음 효과음 2가지 토글 시키는 코드(160620)
        if (GroundingTrigger.isGround == true && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) )
        {
            timer = timer + (10 + Mathf.FloorToInt(Time.deltaTime));
            print(timer);
            if (timer % 500 == 0)
                source.PlayOneShot(walk_1);
            else if (timer % 250 == 0)
                source.PlayOneShot(walk_2);

        }

        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * 3 * Time.deltaTime);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * 3 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Vector3 zero = new Vector3(1.0f, 0.2f, 1.0f);
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

        //점프 방식 변경, Translate -> AddForce(160620)
        if (Input.GetKeyDown(KeyCode.Space) && GroundingTrigger.isGround == true)
        {
            timer = 0;
            Vector3 up = new Vector3(0.0f, 500.0f, 0.0f);
            Vector3 zero = new Vector3(1.0f, 1.0f, 1.0f);
            //this.transform.Translate(0.0f, 10.0f * Time.deltaTime, 0.0f);
            this.GetComponent<Rigidbody>().AddForce(up);
            this.transform.GetChild(1).GetComponent<BoxCollider>().size = zero;
        }
    }
}