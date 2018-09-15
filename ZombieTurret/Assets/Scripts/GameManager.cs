using Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int MaxHealth;
    public int BaseHealth = 100;
    public int Health;
    public int Cash;
    public int HealthUpgradeCost = 20;
    public int HealthPerUpgrade = 20;
    public int NumberOfHealthUpgrades = 0;

    public int Damage = 1;
    public int DamagePerUpgrade = 1;
    public int NumberOfDamageUpgrades = 0;
    public int DamageUpgradeCost = 20;

    public int TurretLevel = 1;
    public int MaxTurretLevel = 3;
    public int TurretUpgradeCost = 100;

    public int HealCost = 30;

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
        MaxHealth = BaseHealth;
        Health = MaxHealth;
        MessageBroker.Default.Receive<PlayerLifeUpdatedEvent>().Subscribe(evt => { Health = evt.Life; })
            .AddTo(gameObject);
        MessageBroker.Default.Receive<EnemyDiedEvent>().Subscribe(evt => { Cash += evt.Gold; }).AddTo(gameObject);
    }

    public void OnUpgradeHealth()
    {
        NumberOfHealthUpgrades++;
        Cash -= HealthUpgradeCost * NumberOfHealthUpgrades;
        MaxHealth = BaseHealth + (HealthPerUpgrade * NumberOfHealthUpgrades);
    }

    public void OnUpgradeDamage()
    {
        NumberOfDamageUpgrades++;
        Cash -= DamageUpgradeCost * NumberOfDamageUpgrades;
        Damage = DamagePerUpgrade * NumberOfDamageUpgrades;
    }

    public void OnTurretUpgrade()
    {
        Cash -= TurretUpgradeCost * TurretLevel;
        TurretLevel++;
        UpgradeTurret(TurretLevel);
    }

    public void OnHeal()
    {
        Cash -= HealCost;
        Health = MaxHealth;
    }

    private void UpgradeTurret(int turretLevel)
    {

    }
}