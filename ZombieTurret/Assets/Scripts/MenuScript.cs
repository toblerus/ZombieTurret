using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.UI_Scripts;
using Unity.Linq;
using System.Linq;

public class MenuScript : MonoBehaviour
{ 
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            {
                if (!FindObjectOfType<MenuScript>().isPaused)
                {
                    FindObjectOfType<MenuScript>().PauseGame();
                }
                else
                {
                    FindObjectOfType<MenuScript>().UnPauseGame();
                }
            }
        }
    }

    public bool isPaused = false;

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }

    public void ContinueGameAfterShop()
    {
        FindObjectOfType<PlayerScript>().enabled = true;
        FindObjectOfType<ShopController>().gameObject.Child("ShopUI").gameObject.SetActive(false);
        FindObjectOfType<Timer>().Reset();
        FindObjectOfType<EnemySpawner>().StartSpawning();
    }

    public void PauseGame()
    {  
        isPaused = true;
        Time.timeScale = 0;
        FindObjectOfType<ShopController>().gameObject.Child("PauseMenu").gameObject.SetActive(true);
        FindObjectOfType<PlayerScript>().enabled = false;
        
    }

    public void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        FindObjectOfType<ShopController>().gameObject.Child("PauseMenu").gameObject.SetActive(false);
        FindObjectOfType<PlayerScript>().enabled = true;
        
    }
}
