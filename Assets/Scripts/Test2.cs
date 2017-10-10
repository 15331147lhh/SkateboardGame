using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Test2 : MonoBehaviour {

    [SerializeField]
    private Vector3 m_customPosition;
    [SerializeField]
    private Renderer rendererer;
	// Use this for initialization
	void Start () {
        rendererer = GetComponent<Renderer>();
        rendererer.sharedMaterial.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = m_customPosition;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rendererer.sharedMaterial.color == Color.white)
                rendererer.sharedMaterial.color = Color.red;
            else
                rendererer.sharedMaterial.color = Color.white;
        }
	}
}
