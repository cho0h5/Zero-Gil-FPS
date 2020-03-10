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
        //if (!PhotonNetwork.IsMasterClient) return;
        if (!photonView.IsMine) return;

        string col_tag = col.collider.tag;
        //Debug.Log($"OnTriggerEnter : {col_tag}");

        if(col_tag == "Player")
        {
            col.gameObject.GetComponent<PlayerManager>().OnDamage(Damage);
            PhotonNetwork.Destroy(gameObject);
        }

    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.Destroy(gameObject);
    }
}
