using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletController : MonoBehaviour
{
    void OnCollisionEnter(Collider col)
    {
        Debug.Log($"OnTriggerEnter : {col.tag}");
    }
}
