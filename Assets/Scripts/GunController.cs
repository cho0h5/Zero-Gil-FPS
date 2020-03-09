using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject player;
    public GameObject Bullet;
    public GameObject position;
    Transform position_tr;
    public float BulletSpeed;

    bool canFire = true;

    public bool mouseDown = false;

    //photon
    PlayerManager player_pm;

    // Start is called before the first frame update
    void Start()
    {
        position_tr = position.GetComponent<Transform>();
        player_pm = player.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!player_pm.isMine) return;

        if (mouseDown && canFire)
        {
            GameObject bullet = PhotonNetwork.Instantiate(Bullet.name, position_tr.position, Quaternion.identity);
            Rigidbody bullet_rd = bullet.GetComponent<Rigidbody>();
            bullet_rd.AddForce(PlayerManager.direction * BulletSpeed);

            canFire = false;
            StartCoroutine("InitializeCanFire");
        }
    }

    IEnumerator InitializeCanFire()
    {
        //Debug.Log("Coroutine Start");
        yield return new WaitForSeconds(0.1f);
        canFire = true;
        //Debug.Log("Coroutine Done");
    }

}
