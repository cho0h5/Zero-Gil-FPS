using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject StartPosition;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Transform StartPosition_tr = StartPosition.GetComponent<Transform>();
        Instantiate(Player, StartPosition_tr.position, Quaternion.identity);
    }
}
