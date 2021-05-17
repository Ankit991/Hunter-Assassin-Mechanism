using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Playercontroller : MonoBehaviour
{
    #region public field
    public GameObject[] enemy;
    public LineRenderer pathinline;
    //int ,float ,bool,vector etc
    public int lifepoint;
    public bool hitenemy;
    public bool enemykilled;
    public bool playerdied;
    public bool isgamestart;
    public Vector3 enemydeathpos;
    public Vector3 enemypos;
    #endregion

    #region private field
    NavMeshAgent agent;
    private Animator anim;
    NavMeshPath path;
    private AudioSource walksound;
    //int ,float ,bool,vector etc
   
    int enemycount;
    private bool iswalking;
    Vector3 pos;
    private List<Vector3> point;
    #endregion

    //script reference
    UIManager uimanager;
  
    #region monobehaviour callback
    // Start is called before the first frame update

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.isStopped = true;
        enemypos = this.transform.position;
        walksound = GetComponent<AudioSource>();
        uimanager = GameObject.Find("UImanager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isgamestart = uimanager.isstartgame;  //to check we started game or not
        if (isgamestart)
        {
         
            NavAgentMovement();
           
        }
       
        pathinline.positionCount = agent.path.corners.Length;
        pathinline.SetPositions(agent.path.corners);
        Dead();
        PLayerSound();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            iswalking = false;
            collision.gameObject.SetActive(false);
            enemykilled = true;
            enemydeathpos = collision.gameObject.transform.position;
            agent.isStopped = true;
            anim.SetBool("Run", false);
            uimanager.killpoint++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            lifepoint--;
            if (lifepoint <= 0)
            {
                playerdied = true;
                this.gameObject.SetActive(false);
            }
        }
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            SoundManager.instance.PlaySound(2);
            uimanager.Coinpoint++;
        }
        if (other.gameObject.tag == "Diamond")
        {
            Destroy(other.gameObject);
            uimanager.Diamondpoint++;
            SoundManager.instance.PlaySound(2);
        }
    }
    #endregion

    #region private method
    private void NavAgentMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            agent.isStopped = false;
            iswalking = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                hitenemy = false;
                if (!hitenemy && hit.collider.tag != "Wall")// making work only when raycast hit ground not wall or anything else;
                {
                    anim.SetBool("Run", true);
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
        if (Vector3.Distance(transform.position, pos) <= 1f)
        {
            anim.SetBool("Run", false);
            iswalking = false;
        }
        if (Vector3.Distance(transform.position, pos) <= .5f /*|| Vector3.Distance(transform.position, enemy[enemycount].transform.position) < .9f*/)
        {

            anim.SetBool("Run", false);
            agent.isStopped = true;
            iswalking = false;

        }
        // Rotation Navmesh_Agent instantly toward the Destination.
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }
    #endregion

    #region public method
    public void Dead()
    {
        if (lifepoint <= 0)
        {
            playerdied = true;
            this.gameObject.SetActive(false);
        }
    }
    public void PLayerSound()
    {
        if (!walksound.isPlaying&&iswalking)
        {
            walksound.Play();
        }
    }
    #endregion
}
