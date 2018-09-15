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
    //public int Cash;
    public ReactiveProperty<int> CashReactive;
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
        return CashReactive.Value;
    }

    void Awake()
    {
        CashReactive = new ReactiveProperty<int>(50);
        MaxHealth = BaseHealth;
        Health = MaxHealth;
        MessageBroker.Default.Receive<PlayerLifeUpdatedEvent>().Subscribe(evt => { Health = evt.Life; })
            .AddTo(gameObject);
        MessageBroker.Default.Receive<EnemyDiedEvent>().Subscribe(evt => { CashReactive.Value += evt.Gold; }).AddTo(gameObject);
    }

    public void OnUpgradeHealth()
    {
        NumberOfHealthUpgrades++;
        CashReactive.Value -= HealthUpgradeCost * NumberOfHealthUpgrades;
        MaxHealth = BaseHealth + (HealthPerUpgrade * NumberOfHealthUpgrades);
    }

    public void OnUpgradeDamage()
    {
        NumberOfDamageUpgrades++;
        CashReactive.Value -= DamageUpgradeCost * NumberOfDamageUpgrades;
        Damage = DamagePerUpgrade * NumberOfDamageUpgrades;
    }

    public void OnTurretUpgrade()
    {
        CashReactive.Value -= TurretUpgradeCost * TurretLevel;
        TurretLevel++;
        UpgradeTurret(TurretLevel);
    }

    public void OnHeal()
    {
        CashReactive.Value -= HealCost;
        Health = MaxHealth;
    }

    private void UpgradeTurret(int turretLevel)
    {

    }

    public int CurrentHealthUpgradeCost()
    {
        return HealthUpgradeCost * (NumberOfHealthUpgrades +1);
    }
}