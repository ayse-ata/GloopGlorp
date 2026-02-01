using System;
using System.Collections;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    //death screen
    public GameObject gameOverUI;
    public bool isGameOverActive = false;

    //win screen
    public GameObject WinUI;
    public bool isWinUIActive = false;

    public Coroutine coroutine;
    
    public AudioSource AudioSource;
    public AudioClip WinClip;
    public AudioClip FailClip;

    public int currentHealth;
    public PlayerHealth playerHealth;
    public LevelHandler levelHandler;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        currentHealth = playerHealth.currentHealth;

        if (isGameOverActive == false)
        {
            gameOverUI.SetActive(false);
        }
        if (isWinUIActive == false)
        {
            WinUI.SetActive(false);
        }
        if (currentHealth <= 0)
        {
            if (coroutine == null)
            {
                coroutine = StartCoroutine(GameFail());

            }
            
        }

        if (levelHandler!=null&&levelHandler.GetCustomerNum() > 3){

            if (coroutine == null)
            {
                coroutine = StartCoroutine(GameWon());

            }
            

        }
    }

  
    IEnumerator GameFail()
    {
        AudioSource.PlayOneShot(FailClip);
        yield return new WaitForSeconds(2);
        isGameOverActive = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }



    IEnumerator GameWon()
    {
        AudioSource.PlayOneShot(WinClip);
        yield return new WaitForSeconds(4);
        isWinUIActive = true;
        WinUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("You Won");
    }

}
