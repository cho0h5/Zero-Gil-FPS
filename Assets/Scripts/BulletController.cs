using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviourPun
{
    public int Damage;

    void Start()
    {
        StartCoroutine("DestroyBullet");
    }

    void OnCollisionEnter(Collision col)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        string col_tag = col.collider.tag;
        //Debug.Log($"OnTriggerEnter : {col_tag}");

        if (col_tag == "Ballet" || col_tag == "Gun" || col_tag == "Wall") return;

        if(col_tag == "Player")
        {
            col.gameObject.GetComponent<PlayerManager>().HP -= Damage;
        }

        int targetHP = col.gameObject.GetComponent<PlayerManager>().HP;
        int targetID = col.gameObject.GetComponent<PhotonView>().ViewID;
        photonView.RPC("RPCUpdateHP", RpcTarget.All, targetID, targetHP);
        PhotonNetwork.Destroy(gameObject);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.Destroy(gameObject);
    }

    /*
    [PunRPC]
    public void RPCUpdateHP(int targetID, int targetHP)
    {
        if (targetID == Target_pm.viewID)
        {
            HP = targetHP;
        }
    }
    */
}
