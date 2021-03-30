using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{   
    public Transform rayright,rayleft;
    public Transform player;
    private Animator E_anim;
   [SerializeField]  private Vector3 walkingposition;
    private NavMeshAgent Enemy_agent;
    public bool fire;
    public bool followplayer;
    public bool walkrandomly;
    public Transform bullet;
    public Transform firepos;
    public LayerMask shoot;
    public float firerate;
    float nextfire;
    [SerializeField] float dis;
    public int DistanceToCapturePlayer;

    //Another script reference;
    Playercontroller playercontroller;

    // Start is called before the first frame update
    void Start()
    {
        playercontroller = GameObject.Find("Player").GetComponent<Playercontroller>();
        E_anim = GetComponentInChildren<Animator>();
        Enemy_agent = GetComponent<NavMeshAgent>();
           // StartCoroutine(WalkTowardRamdompos());
        InvokeRepeating("WalkRandomly", 1, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float DisBTWEnemy_An_killedenemy = Vector3.Distance(this.transform.position, playercontroller.enemydeathpos);
        Debug.Log(DisBTWEnemy_An_killedenemy);
        if (DisBTWEnemy_An_killedenemy < 50)
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
            Debug.Log(dis);
        }
        else
        {
            E_anim.SetInteger("walk", 0);
        }
        // Calling mehtods;
        Fire();
        CastRay();
    }
    public void CastRay()
    {
        RaycastHit hit;

        if (Vector3.Distance(transform.position, player.position) > 10) //checking the position of player and eneymy ;
        {
            followplayer = false;
            walkrandomly = true;
            fire = false;
        }
        if (Physics.Raycast(rayright.position, rayright.transform.TransformDirection(Vector3.forward), out hit, DistanceToCapturePlayer,shoot)|| Physics.Raycast(rayleft.position, rayleft.transform.TransformDirection(Vector3.forward), out hit, DistanceToCapturePlayer,shoot)|| Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,DistanceToCapturePlayer,shoot))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.DrawLine(rayright.position, hit.point, Color.green);
            Debug.DrawLine(rayleft.position, hit.point, Color.blue);
             fire = true;
            followplayer = true;
            walkrandomly = false;
        }
        else
        {
            fire = false;
        }
        if (followplayer)
        {
            Enemy_agent.SetDestination(player.position);
        }
    }
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
            yield return new WaitForSeconds(5f);
        }
       
    }
   public void Fire()
    {
        if (fire)
        {
            if (Time.time > nextfire)
            {
                nextfire = firerate + Time.time;
                 Instantiate(bullet, firepos.position, firepos.rotation);
               
            }
          
        }
    }
}
