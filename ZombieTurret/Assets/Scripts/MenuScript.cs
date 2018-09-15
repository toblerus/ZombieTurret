using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.UI_Scripts;
using Unity.Linq;
using System.Linq;

public class MenuScript : MonoBehaviour
{
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
        SceneManager.LoadScene("Main");
    }

    public void ContinueGameAfterShop()
    {
        FindObjectOfType<PlayerScript>().enabled = true;
        FindObjectOfType<ShopController>().gameObject.Child("ShopUI").gameObject.SetActive(false);
        FindObjectOfType<Timer>().Reset();
        FindObjectOfType<EnemySpawner>().StartSpawning();


    }
}
