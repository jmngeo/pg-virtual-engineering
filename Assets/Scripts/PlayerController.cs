using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{
    public Transform attackpoint;
    public int damage;
    public float attackRange;
    public float attackDelay;
    public float lastAttackTime;
    [HideInInspector]
    public int id;
    public Animator playerAnim;
    public Rigidbody2D rig;
    public Player photonPlayer;
    public SpriteRenderer sr;
    //public HeaderInfo headerInfo;
    public float moveSpeed;
    public int gold;
    public int currentHP;
    public int maxHP;
    public bool dead;

    public static PlayerController me;
    //public float time;//just for showing time in Unity editor realtime

    [PunRPC]
    public void Initialized(Player player)
    {
        id = player.ActorNumber;
        photonPlayer = player;
        GameManager.instance.players[id - 1] = this;

        if (player.IsLocal) //check if the local player is me
            me = this;
        else
            rig.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        //time = Time.time; //just for showing time in Unity editor realtime
        if (!photonView.IsMine)
            return;
        // Move();

        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackDelay)
        {
            // Attack();
        }
    }
}
