using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectplayer : MonoBehaviour
{
    #region public field
    public int hitrange;
    public float firerate;
    public Transform firepos;
    public ParticleSystem muzzleflash;
    public GameObject hitParticle;
    public Transform bullet;
    [HideInInspector] public bool isPlyaerfound;
    public bool iswalk = true,isfollow;

    #endregion
    #region private field
    private float nexttimetofire;
    UIManager uimanager;
    #endregion

    #region monobehaviour callback
    // Start is called before the first frame update
    void Start()
    {
        uimanager = GameObject.Find("UImanager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fireAbuttlet();
            isfollow = true;
            iswalk = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fireAbuttlet();
            isfollow = true;
            iswalk = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlyaerfound = false;
            isfollow = false;
            iswalk = true ;
        }
    }
    #endregion

    #region public method
    public void fireAbuttlet()
    {
        RaycastHit hit;
        if (Physics.Raycast(firepos.position, firepos.transform.forward, out hit, hitrange))
        {

            //if (Time.time>=nexttimetofire)
            //{
            //    nexttimetofire = Time.time + firerate;
            //    muzzleflash.Play();

            //}
            if (hit.collider.tag == "Player" && Time.time >= nexttimetofire)
            {


                nexttimetofire = Time.time + firerate;
              
                muzzleflash.Play();
                GameObject hitimpact = Instantiate(hitParticle, hit.collider.transform.position, Quaternion.identity);
                uimanager.PlayerHealth_point-=.1f;                                       // to show the Health of Player through UI
                hit.collider.gameObject.GetComponent<Playercontroller>().lifepoint--; //to check when the player out of health 
                SoundManager.instance.PlaySound(0);
                //  Instantiate(bullet, firepos.position, firepos.rotation);
                Destroy(hitimpact, 1f);
                Debug.Log(hit.collider.name);
            }
            if (hit.collider.tag == "Player")
            {
                isPlyaerfound = true;
            }
        }

    }
    #endregion
}
