using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform tr;
    Transform Camera_tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        Camera_tr = Camera.main.GetComponent<Transform>();
    }

    void LateUpdate()
    {
        Camera_tr.position = tr.position + new Vector3(0, 13, -2);
        Camera_tr.LookAt(tr);
    }
}
