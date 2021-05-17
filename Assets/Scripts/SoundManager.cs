using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region public field
    public static SoundManager instance = null;
    public AudioSource sound;
    public AudioClip[] soundclip;
   
    #endregion
    // Start is called before the first frame update
    #region monobehaviour callback
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    #endregion
    #region public method
    public void PlaySound(int i)
    {
       sound.clip = soundclip[i];
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }

    #endregion
    #region private method
  
    #endregion
}
