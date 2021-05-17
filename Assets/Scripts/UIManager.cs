using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
public class UIManager : MonoBehaviour
{
    #region public field
   
    [HideInInspector] public float PlayerHealth_point = 1;
    public GameObject Gameoverui;
    [HideInInspector] public int killpoint,Coinpoint,Diamondpoint;
    public CinemachineVirtualCamera cam1;
    public bool isstartgame;
    #region UI
    public Image PlayerHealth;
    public Text killText, coinText, diamondText;
    public Text WinORlose;
    public GameObject start_button;
    #endregion

    #endregion


    // Start is called before the first frame update
    void Start()
    {
      Gameoverui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth.fillAmount = PlayerHealth_point;
        GameoverUI();
        Win();
    }
    #region private method
    private void GameoverUI()
    {
        if ( PlayerHealth_point<=0)
        {
            WinORlose.text = "Try Again";
            killText.text = killpoint.ToString();
            coinText.text = Coinpoint.ToString();
            diamondText.text = Diamondpoint.ToString();
            StartCoroutine(Spawntime_for_winORlose());
            cam1.Priority = 8;
        }
    }
    private void Win()
    {
        if (killpoint >= 5)
        {
            WinORlose.text = "You Win";
            killText.text = killpoint.ToString();
            coinText.text = Coinpoint.ToString();
            diamondText.text = Diamondpoint.ToString();
            StartCoroutine(Spawntime_for_winORlose());
            cam1.Priority = 8;

        }
    }
    #endregion
    #region public method
    public void Restart()
    {
        SceneManager.LoadScene(0);
        Debug.Log("work");
    }
    public void startGame()
    {
        isstartgame = true;
        cam1.Priority = 10;
        start_button.SetActive(false);
      
    }
    #endregion
    #region ienumerators methods
    IEnumerator Spawntime_for_winORlose()
    {
        yield return new WaitForSeconds(.5f);
       
        Gameoverui.SetActive(true);
    }
    #endregion

}
