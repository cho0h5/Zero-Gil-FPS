﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameTagController : MonoBehaviour
{
    public GameObject Target;
    Transform Target_tr;
    Transform tr;
    Text txt;

    void Start()
    {
        Target_tr = Target.GetComponent<Transform>();
        tr = GetComponent<Transform>();
        GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
        txt = GetComponent<Text>();
    }

    void LateUpdate()
    {
        Vector3 Screen_ps = Camera.main.WorldToScreenPoint(Target_tr.position);
        tr.position = new Vector3(Screen_ps.x, Screen_ps.y+80, tr.position.z);
        int HP = Target.GetComponent<PlayerManager>().HP;
        txt.text = EnterManager.name + " : " + HP.ToString();
    }
}
