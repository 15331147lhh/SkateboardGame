using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
            transform.parent.GetComponent<SkateboardController>().g_grounded = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ground"))
            transform.parent.GetComponent<SkateboardController>().g_grounded = false;
    }
}
