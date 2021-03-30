using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public Quaternion Rot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = player.position+offset;
        this.gameObject.transform.rotation = Rot;
    }
}
