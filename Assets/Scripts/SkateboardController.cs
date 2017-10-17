using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}

public class SkateboardController : MonoBehaviour {

    public List<AxleInfo> axleInfos; // the information about each individual axle

    //public float g_horizontalSpeed = 2.0f;
    public float g_verticalSpeed = 20;
    public float maxBackSpeed = -0.1f;
    public float maxFwdSpeed = 20f;

    public float maxSteeringAngle = 0.7f;

    public bool g_grounded;

    private float h = 0;
    private float v = 0;

    private Quaternion initRotation;

    [SerializeField]
    private Transform m_centreOfMass;

    private Rigidbody m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.centerOfMass = m_centreOfMass.localPosition;
        initRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
            RestoreRotation();
        

    }

    // finds the corresponding visual wheel
    // correctly applies the transform

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            FindCorrectSwipeInput(Input.GetAxis("Mouse X"), ref h);
            FindCorrectSwipeInput(Input.GetAxis("Mouse Y"), ref v);
        }

        float momentum = Mathf.Clamp(v * g_verticalSpeed, maxBackSpeed, maxFwdSpeed);
        float steering = h * maxSteeringAngle;

        print("h = " + h + ", v = " + v);

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = momentum;
                axleInfo.rightWheel.motorTorque = momentum;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    void FindCorrectSwipeInput(float input, ref float prevInput)
    {
        if (prevInput > 0 && input > 0)
            prevInput = Mathf.Max(prevInput, input);
        else if (prevInput < 0 && input < 0)
                prevInput = Mathf.Min(prevInput, input);
        else
            prevInput = input;
    }

    void RestoreRotation()
    {
        transform.rotation = initRotation;
    }
}

