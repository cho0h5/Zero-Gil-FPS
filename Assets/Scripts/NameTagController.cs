using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameTagController : MonoBehaviour
{
    public GameObject Target;
    Transform Target_tr;
    Transform tr;
    Text txt;
    PlayerManager Target_pm;

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
        tr.position = new Vector3(Screen_ps.x, Screen_ps.y+100, tr.position.z);
        Target_pm = Target.GetComponent<PlayerManager>();
        txt.text = Target_pm.name + " : " + Target_pm.HP.ToString();
    }

}
