using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterManager : MonoBehaviourPunCallbacks
{
    public static string name = "Blank";
    public Button joinButton;
    public InputField inputField;

    //photon
    readonly string gameVersion = "1";

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        //joinButton.interactable = true;

        if (!PhotonNetwork.IsConnected)
        {
            joinButton.interactable = true;
            return;
        }

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //base.OnDisconnected(cause);
        
        //PhotonNetwork.ConnectUsingSettings();
    }

    public void OnClickEnter()
    {

        joinButton.interactable = false;
        name = inputField.text;

        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = name;
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //base.OnJoinRandomFailed(returnCode, message);
        Debug.Log($"On join Random Failed {message}");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 10 });
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("FieldScene");
    }

}
