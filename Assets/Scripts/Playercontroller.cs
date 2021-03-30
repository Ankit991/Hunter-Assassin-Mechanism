using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Playercontroller : MonoBehaviour
{
    NavMeshAgent agent;
    private  Animator anim;
    Vector3 pos;
    public Vector3 enemydeathpos;
    public  Vector3 enemypos;
    public GameObject[] enemy;
    public bool hitenemy;
    public bool enemykilled;
     int enemycount;
    public int lifepoint;
    public LineRenderer pathinline;
    NavMeshPath path;
    private List<Vector3> point;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemypos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        NavAgentMovement();
        pathinline.positionCount = agent.path.corners.Length;
        pathinline.SetPositions(agent.path.corners);
    }

    private void NavAgentMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            agent.isStopped = false;
            anim.SetBool("Run", true);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                hitenemy = false;
                if (!hitenemy)
                {
                    agent.SetDestination(hit.point);
                }
                enemycount = 0;
                if (hit.collider.tag == "Enemy")
                {
                    for (int i = 0; i < enemy.Length; i++)
                    {
                        if (enemy[i] == hit.collider.gameObject)
                        {
                            Debug.Log(hit.collider.name);
                            enemycount = i;
                            break;
                        }

                    }
                    hitenemy = true;
                }

            }
            pos = hit.point;

        }
        if (hitenemy)
        {
           
            agent.SetDestination(enemy[enemycount].transform.position);
        }
        if (Vector3.Distance(transform.position, pos) < 1.5f)
        {
            anim.SetBool("Run", false);

        }
        if (Vector3.Distance(transform.position, pos) < .5f || Vector3.Distance(transform.position, enemy[enemycount].transform.position) < .9f)
        {
            anim.SetBool("Run", false);
            agent.isStopped = true;

        }
        // Rotation Navmesh_Agent instantly toward the Destination.
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            enemykilled = true;
            enemydeathpos = collision.gameObject.transform.position;
            agent.isStopped = true;
            anim.SetBool("Run", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            lifepoint--;
            if (lifepoint <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
  
    
}
