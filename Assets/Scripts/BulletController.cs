using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int Damage;

    void Start()
    {
        StartCoroutine("DestroyBullet");
    }

    void OnCollisionEnter(Collision col)
    {
        string col_tag = col.collider.tag;
        //Debug.Log($"OnTriggerEnter : {col_tag}");

        if (col_tag == "Ballet" || col_tag == "Gun" || col_tag == "Wall") return;
        if(col_tag == "Player")
        {
            col.gameObject.GetComponent<PlayerManager>().HP -= Damage;
        }
        Destroy(gameObject);
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
