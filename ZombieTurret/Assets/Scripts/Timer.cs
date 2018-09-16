using UnityEngine;
using System.Collections;
using TMPro;
using Assets.Scripts.UI_Scripts;
using Unity.Linq;
using System.Linq;
using Enemy;
using UniRx;
using System;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timer;
    public float minutes = 0;
    public float seconds = 5;
    public float milliseconds = 0;

    public void Start()
    {
        MessageBroker.Default.Receive<PlayerDiedEvent>().Subscribe(_ => {
            OnEndGame();
            Debug.Log("Timer");
            }).AddTo(gameObject);
    }

    public void OnEndGame()
    {
        PauseGameAfterTimerRunOut();
        FindObjectOfType<ShopController>().gameObject.Child("GameOverScreen").gameObject.SetActive(true);


    }

    public void Reset()
    {
        minutes = 1;
        seconds = 20;
        milliseconds = 0;
        this.enabled = true;
    }

    void Update()
    {

        if (milliseconds <= 0)
        {
            if (seconds <= 0)
            {
                minutes--;
                seconds = 59;
            }
            else if (seconds >= 0)
            {
                seconds--;
            }

            milliseconds = 100;
        }

        milliseconds -= Time.deltaTime * 100;

        timer.text = "Timeleft: " + string.Format("{0}:{1}:{2}", minutes, seconds, (int)milliseconds);
        if(minutes <= 0.0f && seconds <= 0.0f && milliseconds <= 1.0f)
        {
            PauseGameAfterTimerRunOut();
            FindObjectOfType<ShopController>().gameObject.Child("ShopUI").gameObject.SetActive(true);
            //var EnemyList = FindObjectsOfTypeAll(typeof(AbstractEnemy)).Cast<AbstractEnemy>().ToList();
            //EnemyList.ForEach(x =>
            //{
            //    DestroyImmediate(x.gameObject);
            //});
            MessageBroker.Default.Publish(new DestroyGameObjectsOfTypeEvent(){ObjectTypeToDestroy = ObjectType.All});
        }

    }
        void PauseGameAfterTimerRunOut()
        {
            this.enabled = false;
            FindObjectOfType<EnemySpawner>().SpawnerDisposable.Dispose();
            FindObjectOfType<PlayerScript>().enabled = false;
            
        }

        
}