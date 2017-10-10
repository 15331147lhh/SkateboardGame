using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateboardController : MonoBehaviour {

    public float g_horizontalSpeed = 2.0f;
    public float g_verticalSpeed = 0.1f;

    public float maxBackSpeed = -0.1f;
    public float maxFwdSpeed = 20f;

    public float maxRotateSpeed = 5;

    public bool g_grounded;

    private float h = 0;
    private float v = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButton(0))
        {
            FindCorrectSwipeInput(Input.GetAxis("Mouse X"), ref h);
            FindCorrectSwipeInput(Input.GetAxis("Mouse Y"), ref v);
        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    transform.GetChild(0).gameObject.GetComponent<Rigidbody>().AddRelativeForce(100 * new Vector3(-v * g_verticalSpeed, -h * g_horizontalSpeed, 0));
        //    print("h = " + h + ", v = " + v);
        //}


        transform.Translate(new Vector3(-(g_verticalSpeed + Mathf.Clamp(v * 20, maxBackSpeed, maxFwdSpeed)) * Time.deltaTime, 0, 0));
        print("h = " + h + ", v = " + v);
        transform.Rotate(new Vector3(0, h * 0.7f, 0));

    }

    void FindCorrectSwipeInput(float input, ref float prevInput)
    {
        if (prevInput > 0 && input > 0)
            prevInput = Mathf.Max(h, input);
        else if (prevInput < 0 && input < 0)
                prevInput = Mathf.Min(h, input);
        else
            prevInput = input;
    }
}
