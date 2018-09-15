using Enemy;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int MaxHealth = 100;
    public int Health;
    public int Cash;

    public int GetHealth()
    {
        return Health;
    }

    public int GetCash()
    {
        return Cash;
    }

    void Start()
    {
        Health = MaxHealth;
        MessageBroker.Default.Receive<PlayerLifeUpdatedEvent>().Subscribe(evt => { Health = evt.Life; })
            .AddTo(gameObject);
        MessageBroker.Default.Receive<EnemyDiedEvent>().Subscribe(evt => { Cash += evt.Gold; }).AddTo(gameObject);
    }

    public void UpgradeHealth()
    {

    }
}