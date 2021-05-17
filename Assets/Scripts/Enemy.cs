using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    #region public field
    public Rigidbody EnemyRB;
    public Transform rayright, rayleft;
    public Transform player;
    public Transform bullet;
    public Transform firepos;
    public LayerMask shoot;
    // int ,float ,bool ,vector etc
    public bool fire;
    public bool followplayer;
    public bool walkrandomly;
    public float firerate;
    public int DistanceToCapturePlayer;
    #endregion


    #region private field
    private Animator E_anim;
    private NavMeshAgent Enemy_agent;

    //int ,float ,bool ,vector etc
    float nextfire;
    [SerializeField] float dis;
    private float enemyspeed;
    [SerializeField] private Vector3 walkingposition;
    #endregion

    //Another script reference;
    Playercontroller playercontroller;
    EnemyDetectplayer enemyDetectplayer;


    #region monobehaviour callback
    // Start is called before the first frame update
    void Start()
    {
        playercontroller = GameObject.Find("Player").GetComponent<Playercontroller>();
        enemyDetectplayer = GetComponentInChildren<EnemyDetectplayer>();
        E_anim = GetComponentInChildren<Animator>();
       
        Enemy_agent = GetComponent<NavMeshAgent>();
      
        {
            InvokeRepeating("WalkRandomly", Random.Range(1, 3), Random.Range(2.5f, 6f));
        }
       
        EnemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playercontroller.isgamestart)
        {
            enemyspeed = Enemy_agent.speed;
            Enemy_agent.speed = 0;
        }
        else
        {
            Enemy_agent.speed = 2;
        }
            float DisBTWEnemy_An_killedenemy = Vector3.Distance(this.transform.position, playercontroller.enemydeathpos);
       
        if (DisBTWEnemy_An_killedenemy < 10)
        {
            if (playercontroller.enemykilled)
            {
                walkrandomly = false;
              
                // Enemy_agent.SetDestination(playercontroller.enemydeathpos);
                walkingposition = playercontroller.enemydeathpos;
            }
        }
        if (DisBTWEnemy_An_killedenemy <= 1)
        {
            playercontroller.enemykilled = false;
            walkrandomly = true;
        }

        Enemy_agent.SetDestination(walkingposition);
        dis = Vector3.Distance(transform.position, walkingposition);
        if (dis > 1f)
        {
            E_anim.SetInteger("walk", 1);
         
        }
        else
        {
            E_anim.SetInteger("walk", 0);
        }
        // Calling mehtods;
        Fire();
        CastRay();
    }
    #endregion


    #region public method
    public void CastRay()
    {


        if (Vector3.Distance(transform.position, player.position) > 10) //checking the position of player and eneymy ;
        {
            followplayer = false;
            walkrandomly = true;
            fire = false;
        }

        #region casting rays
        //if (Physics.Raycast(rayright.position, rayright.transform.TransformDirection(Vector3.forward), out hit, DistanceToCapturePlayer)|| Physics.Raycast(rayleft.position, rayleft.transform.TransformDirection(Vector3.forward), out hit, DistanceToCapturePlayer)|| Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,DistanceToCapturePlayer))
        //{
        //    //Debug.DrawLine(transform.position, hit.point, Color.red);
        //Debug.DrawLine(rayright.position, hit.point, Color.green);
        //Debug.DrawLine(rayleft.position, hit.point, Color.blue);
        //if (!playercontroller.playerdied&&hit.collider.tag=="Player")
        //{
        //    fire = true;
        //    Debug.Log(hit.collider.name);
        //}
        //else
        //{
        //    fire = false;
        //}
        #endregion
        followplayer = enemyDetectplayer.isfollow;
        walkrandomly = enemyDetectplayer.iswalk;
        if (/*Vector3.Distance(transform.position, player.position) < 1*/enemyDetectplayer.isPlyaerfound) //if enemy is to close to the player then stop and fire 
        {
            transform.rotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);

            Enemy_agent.isStopped = true;
            E_anim.SetInteger("walk", 0);
        }
        else if (!enemyDetectplayer.isPlyaerfound)
        {
            Enemy_agent.isStopped = false;

            E_anim.SetInteger("walk", 1);
        }




        if (followplayer)
        {
            Enemy_agent.SetDestination(player.position);
        }
    }
    public void Fire()
    {
        //if (fire)
        //{
        //    if (Time.time > nextfire)
        //    {
        //        nextfire = firerate + Time.time;
        //         Instantiate(bullet, firepos.position, firepos.rotation);

        //    }

        //}
    }
    #endregion
    #region private method
    void WalkRandomly()
    {
        if (walkrandomly)
        {
            float x = Random.Range(-14f, 14f);
            float z = Random.Range(-14f, 14f);
            walkingposition = new Vector3(x, transform.position.y, z);

        }

    }
    IEnumerator WalkTowardRamdompos()
    {

        while (walkrandomly)
        {
            float x = Random.Range(-14f, 14f);
            float z = Random.Range(-14f, 14f);
            walkingposition = new Vector3(x, transform.position.y, z);
            yield return new WaitForSeconds(2f);
        }

    }
    #endregion

}
