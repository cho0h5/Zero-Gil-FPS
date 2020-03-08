using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject StartPosition;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            LeftRoom();
        }
    }

    void SpawnPlayer()
    {
        Transform StartPosition_tr = StartPosition.GetComponent<Transform>();
        PhotonNetwork.Instantiate(Player.name, StartPosition_tr.position, Quaternion.identity);
    }

    public override void OnLeftRoom()
    {
        //base.OnLeftRoom();
        SceneManager.LoadScene("EnterScene");
    }

    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
