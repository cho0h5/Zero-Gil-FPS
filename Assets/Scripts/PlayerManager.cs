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
    public GameObject gameManager;

    void Start()
    {
        isMine = photonView.IsMine;

        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        nameTag = PhotonNetwork.Instantiate(NameTag.name, Vector3.zero, Quaternion.identity);
        nameTag.GetComponent<NameTagController>().Target = gameObject;
    }
    
    void FixedUpdate()
    {
        if (!photonView.IsMine) return;

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

        if (HP <= 0) Die();

    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.collider.tag);
        if (col.collider.tag == "DeadZone") Die();
    }

    public void Die()
    {
        PhotonNetwork.Destroy(nameTag);
        PhotonNetwork.Destroy(gameObject);
    }
}
