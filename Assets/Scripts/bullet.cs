using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float bulletspeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward *  bulletspeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(this.gameObject);
    }
}
