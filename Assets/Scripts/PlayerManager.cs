using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun
{
    public int HP = 100;

    public float speed = 200f;
    Vector3 moveDirection;
    Rigidbody rigidbody;
    Transform transform;
    Vector3 worldPosition;
    public static Vector3 direction;
    public GameObject NameTag;
    GameObject nameTag;

    //photon
    public bool isMine;

    //onDie
    GameObject gameManager;
    GameManager gameManager_gm;

    void Start()
    {
        isMine = photonView.IsMine;

        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        nameTag = PhotonNetwork.Instantiate(NameTag.name, Vector3.zero, Quaternion.identity);
        nameTag.GetComponent<NameTagController>().Target = gameObject;

        gameManager = GameObject.Find("GameManager");
        gameManager_gm = gameManager.GetComponent<GameManager>();
    }
    
    void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        if (HP <= 0) Die();

        //player position
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = moveDirection.normalized;
        rigidbody.AddForce(moveDirection * speed);

        //player rotation
        Plane plane = new Plane(Vector3.up, Vector3.up);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
            //Debug.Log($"worldPosition : {worldPosition}");
        }

        Vector3 direction_tmp = worldPosition - transform.position;
        direction = direction_tmp.normalized;
        //Debug.Log($"direction : {direction}");

        transform.rotation = Quaternion.LookRotation(direction);


    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.collider.tag);
        if (col.collider.tag == "DeadZone") HP = 0;
    }

    public void Die()
    {
        PhotonNetwork.Destroy(nameTag);
        PhotonNetwork.Destroy(gameObject);
        gameManager_gm.OnDie();
    }
}
