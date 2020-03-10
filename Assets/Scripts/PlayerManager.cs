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

    //control
    public JoystickController JoystickMove;
    public JoystickController JoystickFire;
    Vector3 Move;
    Vector3 Fire;
    public GunController gunController;
    Vector3 preFire;

    //syncNameTag
    public int viewID;
    public string name;

    void Start()
    {
        isMine = photonView.IsMine;

        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        nameTag = PhotonNetwork.Instantiate(NameTag.name, Vector3.zero, Quaternion.identity);
        nameTag.GetComponent<NameTagController>().Target = gameObject;

        gameManager = GameObject.Find("GameManager");
        gameManager_gm = gameManager.GetComponent<GameManager>();

        //joystick initialize
        JoystickMove = GameObject.Find("JoystickMove/Background").GetComponent<JoystickController>();
        JoystickFire = GameObject.Find("JoystickFire/Background").GetComponent<JoystickController>();

        preFire = Vector3.zero;


        //syncNameTag
        viewID = photonView.ViewID;
        name = photonView.Owner.NickName;
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        if (HP <= 0) Die();

        /*
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
        //*/

        //move
        Move = JoystickMove.JoystickDirection;
        moveDirection = new Vector3(Move.x, 0, Move.y);
        moveDirection = moveDirection.normalized;
        rigidbody.AddForce(moveDirection * speed);

        //fire
        Fire = JoystickFire.JoystickDirection;
        if (Fire != Vector3.zero) preFire = Fire;
        gunController.mouseDown = JoystickFire.mouseDown;
        direction = new Vector3(preFire.x, 0, preFire.y);
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

    [PunRPC]
    public void RPCUpdateHP(int targetID, int targetHP)
    {
        Debug.Log(targetID.ToString() + " : " + targetHP.ToString());
    }
}
